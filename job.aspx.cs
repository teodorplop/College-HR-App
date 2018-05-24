using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class job : System.Web.UI.Page {
    private int positionId;
    private List<StudentApplication> applications;

    protected void Page_Load(object sender, EventArgs e) {
        if (!SessionManager.Instance.LoggedIn) {
            BodyPanel.Visible = false;
            return;
        }

        string jobId = Request.QueryString["id"];
        if (!int.TryParse(jobId, out positionId)) {
            (Master as MasterPage).ShowError(false, "Cannot parse job id.");
            return;
        }

        Position position;
        string error;
        if (!PositionsTable.Instance.Select(positionId, out position, out error)) {
            (Master as MasterPage).ShowError(false, error);
            return;
        }

        ShowPosition(position);
        ShowCandidates(position);
    }

    private void ShowPosition(Position position) {
        HtmlGenericControl header = new HtmlGenericControl();
        header.TagName = "h5";
        header.InnerHtml = position.position;
        DescriptionPanel.Controls.Add(header);

        HtmlGenericControl content = new HtmlGenericControl();
        content.TagName = "div";
        content.InnerHtml = position.description;
        DescriptionPanel.Controls.Add(content);
    }
    private void ShowCandidates(Position position) {
        string error;

        if (!ApplicationsTable.Instance.Select(position.id, out applications, out error)) {
            (Master as MasterPage).ShowError(false, error);
            return;
        }

        int rowIdx = 1;
        foreach (StudentApplication app in applications) {
            TableRow row = new TableRow();
            CandidatesTablePanel.Controls.Add(row);

            TableCell name = new TableCell();
            name.Text = app.firstName + " " + app.lastName;
            row.Controls.Add(name);

            TableCell email = new TableCell();
            email.Text = app.email;
            row.Controls.Add(email);

            TableCell college = new TableCell();
            college.Text = app.college;
            row.Controls.Add(college);

            TableCell buttons = new TableCell();
            row.Controls.Add(buttons);

            Button acceptButton = new Button();
            acceptButton.ID = "Accept|" + rowIdx;
            acceptButton.CssClass = "btn btn-success";
            acceptButton.Text = "Accept";
            acceptButton.Click += AcceptButton_Click;
            buttons.Controls.Add(acceptButton);

            Button declineButton = new Button();
            declineButton.ID = "Delete|" + rowIdx;
            declineButton.CssClass = "btn btn-danger";
            declineButton.Text = "Decline";
            declineButton.Click += DeclineButton_Click;
            buttons.Controls.Add(declineButton);

            ++rowIdx;
        }
    }

    private void AcceptButton_Click(object sender, EventArgs e) {
        int rowIdx;
        if (!TryParse((sender as Control).ID, out rowIdx))
            return;

        string error;
        if (!SessionManager.Instance.LoggedIn) {
            (Master as MasterPage).ShowError(false, "Session timeout. Please sign in.");
            return;
        }
        if (!InterviewsTable.Instance.Insert(applications[rowIdx - 1].email, positionId, SessionManager.Instance.LoggedUser, out error)) {
            (Master as MasterPage).ShowError(false, error);
            return;
        }

        bool success = ApplicationsTable.Instance.Delete(applications[rowIdx - 1].applicationId, out error);
        (Master as MasterPage).ShowError(success, error);
        if (success)
            CandidatesTablePanel.Rows.RemoveAt(rowIdx);
    }

    private void DeclineButton_Click(object sender, EventArgs e) {
        int rowIdx;
        if (!TryParse((sender as Control).ID, out rowIdx))
            return;

        string error;
        bool success = ApplicationsTable.Instance.Delete(applications[rowIdx - 1].applicationId, out error);
        (Master as MasterPage).ShowError(success, error);
        if (success)
            CandidatesTablePanel.Rows.RemoveAt(rowIdx);
    }

    private bool TryParse(string id, out int rowIdx) {
        rowIdx = 0;

        if (!int.TryParse(id.Substring(id.IndexOf('|') + 1), out rowIdx)) {
            (Master as MasterPage).ShowError(false, "Could not parse row idx." + id);
            return false;
        }
        return true;
    }

    protected void AddCandBtn_Click(object sender, EventArgs e) {
        string email = TextBoxEmail.Text;
        string first, last;
        ExtractName(TextBoxName.Text, out first, out last);
        string college = TextBoxCollege.Text;

        string error;
        bool exists;
        if (!StudentsTable.Instance.Select(email, out exists, out error)) {
            (Master as MasterPage).ShowError(false, error);
            return;
        }

        if (!exists && !StudentsTable.Instance.Insert(email, first, last, college, out error)) {
            (Master as MasterPage).ShowError(false, error);
            return;
        }

        if (!ApplicationsTable.Instance.Insert(email, positionId, out error)) {
            (Master as MasterPage).ShowError(false, error);
            return;
        }

        Response.Redirect(Request.RawUrl);
    }

    private void ExtractName(string name, out string first, out string last) {
        first = last = "";

        string[] split = name.Split(' ');
        first = split[0];

        for (int i = 1; i < split.Length - 1; ++i)
            first += " " + split[i];

        if (split.Length > 1)
            last = split[split.Length - 1];
    }
}