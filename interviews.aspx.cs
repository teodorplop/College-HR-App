using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Data.Sql;
using System.Linq;
using System.Data.SqlTypes;

public partial class interviews : System.Web.UI.Page {
    List<StudentInterview> intervs;
    List<StudentInterview> toBeScheduled, past, today, future;

    protected void Page_Init(object sender, EventArgs e) {
        if (!SessionManager.Instance.LoggedIn) {
            MainPanel.Visible = false;
            return;
        }

        string error;
        if (!InterviewsTable.Instance.Select(out intervs, out error)) {
            (Master as MasterPage).ShowError(false, error);
            return;
        }

        ShowInterviews();
    }

    private void ShowInterviews() {
        toBeScheduled = new List<StudentInterview>();
        past = new List<StudentInterview>();
        today = new List<StudentInterview>();
        future = new List<StudentInterview>();

        foreach (StudentInterview intv in intervs) {
            if (intv.date.IsNull)
                Add_ToBeScheduledInterview(intv);
            else if (intv.date.Value.Date < DateTime.Now.Date)
                Add_PastInterview(intv);
            else if (intv.date.Value.Date == DateTime.Now.Date)
                Add_TodayInterview(intv);
            else
                Add_FutureInterview(intv);
        }
    }

    private void AddBasicInfo(TableRow row, StudentInterview intv) {
        TableCell interviewer = new TableCell();
        interviewer.Text = intv.interviewerFirstName + " " + intv.interviewerLastName;
        row.Controls.Add(interviewer);

        TableCell name = new TableCell();
        name.Text = intv.firstName + " " + intv.lastName;
        row.Controls.Add(name);

        TableCell email = new TableCell();
        email.Text = intv.email;
        row.Controls.Add(email);

        TableCell position = new TableCell();
        position.Text = intv.positionName;
        row.Controls.Add(position);
    }

    private void AddDate(TableRow row, StudentInterview intv, bool editable) {
        TableCell date = new TableCell();
        row.Controls.Add(date);

        TextBox dateTextBox = new TextBox();
        dateTextBox.ID = "dateTextBox_" + intv.interviewId;
        dateTextBox.Text = intv.date.IsNull ? "Null" : intv.date.Value.ToString("dd/MM/yyyy");
        dateTextBox.ReadOnly = !editable;
        date.Controls.Add(dateTextBox);

        if (editable) {
            ImageButton calendarImgBtn = new ImageButton();
            calendarImgBtn.ID = "calendarImgBtn_" + intv.interviewId;
            calendarImgBtn.ImageUrl = "Resources/Calendar.png";
            calendarImgBtn.Width = new Unit(30); // ugly as fuck?
            date.Controls.Add(calendarImgBtn);

            CalendarExtender calendarExt = new CalendarExtender();
            calendarExt.PopupButtonID = calendarImgBtn.ID;
            calendarExt.TargetControlID = dateTextBox.ID;
            calendarExt.Format = "dd/MM/yyyy";
            date.Controls.Add(calendarExt);
        }
    }

    private void AddResult(TableRow row, StudentInterview intv) {
        TableCell result = new TableCell();
        row.Controls.Add(result);

        if (!intv.HasResult) {
            Button accept = new Button();
            accept.CssClass = "btn btn-outline-success";
            accept.ID = "accepted_" + intv.interviewId;
            accept.Text = "Accept";
            accept.Click += Accept_Click;
            result.Controls.Add(accept);

            Button declined = new Button();
            declined.CssClass = "btn btn-outline-danger";
            declined.ID = "declined_" + intv.interviewId;
            declined.Text = "Decline";
            declined.Click += Declined_Click;
            result.Controls.Add(declined);
        } else if (intv.Accepted) {
            Button accept = new Button();
            accept.CssClass = "btn btn-success";
            accept.ID = "accepted_" + intv.interviewId;
            accept.Text = "Accepted";
            accept.Enabled = false;

            result.Controls.Add(accept);
        } else if (intv.Refused) {
            Button declined = new Button();
            declined.CssClass = "btn btn-danger";
            declined.ID = "declined_" + intv.interviewId;
            declined.Text = "Declined";
            declined.Enabled = false;

            result.Controls.Add(declined);
        }
    }

    private void Declined_Click(object sender, EventArgs e) {
        string id = (sender as Button).ID.Substring(9);
        Accept(id, false);
    }

    private void Accept_Click(object sender, EventArgs e) {
        string id = (sender as Button).ID.Substring(9);
        Accept(id, true);
    }

    private void Accept(string id, bool accepted) {
        int intvId;
        string error;
        if (int.TryParse(id, out intvId)) {
            StudentInterview interview = intervs.Find(obj => obj.interviewId == intvId);
            if (InterviewsTable.Instance.Update(intvId, true, out error) && EmployeesTable.Instance.Insert(interview.email, interview.positionId, out error)) {
                (Master as MasterPage).ShowError(true, "Success");
                Response.Redirect(Request.RawUrl);
                return;
            }
        } else
            error = "Cannot parse id. " + id;

        (Master as MasterPage).ShowError(false, error);
    }

    private void Add_ToBeScheduledInterview(StudentInterview intv) {
        toBeScheduled.Add(intv);

        TableRow row = new TableRow();
        Schedule_InterviewsTable.Controls.Add(row);

        AddBasicInfo(row, intv);
        AddDate(row, intv, true);
    }

    private void Add_PastInterview(StudentInterview intv) {
        past.Add(intv);

        TableRow row = new TableRow();
        Past_InterviewsTable.Controls.Add(row);

        AddBasicInfo(row, intv);
        AddDate(row, intv, false);
        AddResult(row, intv);
    }

    private void Add_TodayInterview(StudentInterview intv) {
        today.Add(intv);

        TableRow row = new TableRow();
        Today_InterviewsTable.Controls.Add(row);

        AddBasicInfo(row, intv);
    }
    private void Add_FutureInterview(StudentInterview intv) {
        future.Add(intv);

        TableRow row = new TableRow();
        Future_InterviewsTable.Controls.Add(row);

        AddBasicInfo(row, intv);
        AddDate(row, intv, true);
    }

    protected void ApplyChanges_Click(object sender, EventArgs e) {
        string err = "";
        bool hasErrors = false;
        bool dirty = false;
        foreach (StudentInterview intv in intervs) {
            TextBox tb = Utils.FindControlRecursive(MainPanel, "dateTextBox_" + intv.interviewId) as TextBox;
            DateTime date;
            string error;
            
            try {
                date = DateTime.ParseExact(tb.Text, "dd/MM/yyyy", null);
            } catch (Exception ex) {
                err += ex.Message + '\n';
                hasErrors = true;
                continue;
            }

            if (intv.date == date) continue;

            dirty = true;
            if (!InterviewsTable.Instance.Update(intv.interviewId, new SqlDateTime(date), out error)) {
                err += error + '\n';
                hasErrors = true;
            }
        }

        if (hasErrors) {
            (Master as MasterPage).ShowError(false, err);
        } else if (dirty) {
            Response.Redirect(Request.RawUrl);
            (Master as MasterPage).ShowError(true, "Success!");
        }
    }
}
