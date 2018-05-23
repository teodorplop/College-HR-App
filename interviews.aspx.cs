using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

public partial class interviews : System.Web.UI.Page {
    List<StudentInterview> intervs;
    List<StudentInterview> toBeScheduled, past, today, future;
    protected void Page_Load(object sender, EventArgs e) {
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
        dateTextBox.ReadOnly = true;
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
        AddResult(row, intv);
    }
    private void Add_FutureInterview(StudentInterview intv) {
        future.Add(intv);

        TableRow row = new TableRow();
        Future_InterviewsTable.Controls.Add(row);

        AddBasicInfo(row, intv);
        AddDate(row, intv, false);
    }

    protected void ApplyChanges_Click(object sender, EventArgs e) {
        foreach (StudentInterview intv in intervs) {
            (Master as MasterPage).Alert((Utils.FindControlRecursive(MainPanel, "dateTextBox_" + intv.interviewId) as TextBox).Text);
            //(Master as MasterPage).Alert((FindControl("dateTextBox_" + intv.interviewId) as TextBox).te);
        }
    }
}
