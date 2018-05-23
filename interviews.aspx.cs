using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlTypes;
using System.Web.UI.HtmlControls;

public partial class interviews : System.Web.UI.Page {
	List<StudentInterview> intervs;
	protected void Page_Load(object sender, EventArgs e) {
		string error;
		if (!InterviewsTable.Instance.Select(out intervs, out error)) {
			(Master as MasterPage).ShowError(false, error);
			return;
		}

		ShowInterviews();
	}

	private void ShowInterviews() {
		int rowIdx = 1;
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

	private void Add_ToBeScheduledInterview(StudentInterview intv) {
		TableRow row = new TableRow();
		Schedule_InterviewsTable.Controls.Add(row);

        TableCell name = new TableCell();
        name.Text = intv.firstName + " " + intv.lastName;
        row.Controls.Add(name);

        TableCell email = new TableCell();
        email.Text = intv.email;
        row.Controls.Add(email);

        TableCell position = new TableCell();
        position.Text = intv.positionName;
        row.Controls.Add(position);

        TableCell date = new TableCell();
        //date.Text = intv.date.ToString();
        row.Controls.Add(date);

        //HtmlGenericControl a = new HtmlGenericControl();
        //a.hre
		/*name.Text = intv.firstName + " " + intv.lastName;
		row.Controls.Add(name);

		TableCell email = new TableCell();
		email.Text = intv.email;
		row.Controls.Add(email);

		TableCell date = new TableCell();
		date.Text = intv.date.ToString();
		row.Controls.Add(date);*/

		//TableCell buttons = new TableCell();
		//row.Controls.Add(buttons);

		/*Button acceptButton = new Button();
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
		buttons.Controls.Add(declineButton);*/
	}

	private void Add_PastInterview(StudentInterview intv) {

	}

	private void Add_TodayInterview(StudentInterview intv) {

	}
	private void Add_FutureInterview(StudentInterview intv) {

	}
}
