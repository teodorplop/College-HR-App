using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class addposition : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        if (!SessionManager.Instance.LoggedIn) {
            PublishPanel.Visible = false;
            return;
        }

        if (!IsPostBack) {
            List<string> categories;
            string error;
            if (!CategoriesTable.Instance.Select(out categories, out error)) {
                (Master as MasterPage).ShowError(false, error);
                return;
            }

            foreach (string cat in categories)
                DropdownCategory.Items.Add(new ListItem(cat, cat, true));
        }
    }

    public void SubmitButton_Click(object sender, EventArgs e) {
        string error;
        if (!PositionsTable.Instance.Insert(TextBoxTitle.Text, DropdownCategory.Text, TextBoxProject.Text, TextBoxLocation.Text, DropdownType.Text, TextBoxContent.Text, out error)) {
            (Master as MasterPage).ShowError(false, error);
            return;
        }

        Response.Redirect("openings.aspx");
    }
}