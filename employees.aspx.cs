using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class employees : System.Web.UI.Page {
    List<EmployeePosition> list;
    protected void Page_Load(object sender, EventArgs e) {
        if (!SessionManager.Instance.LoggedIn) {
            EmpMainPanel.Visible = false;
            return;
        }

        string error;
        if (!EmployeesTable.Instance.Select(out list, out error)) {
            (Master as MasterPage).ShowError(false, error);
            return;
        }

        ShowEmployees();
    }

    private void ShowEmployees() {
        foreach (EmployeePosition emp in list) {
            TableRow row = new TableRow();
            Employees_Table.Controls.Add(row);

            AddBasicInfo(row, emp);
        }
    }

    private void AddBasicInfo(TableRow row, EmployeePosition emp) {
        TableCell name = new TableCell();
        name.Text = emp.firstName + " " + emp.lastName;
        row.Controls.Add(name);

        TableCell email = new TableCell();
        email.Text = emp.email;
        row.Controls.Add(email);

        TableCell position = new TableCell();
        position.Text = emp.positionName;
        row.Controls.Add(position);

        TableCell hireDate = new TableCell();
        hireDate.Text = emp.hireDate.Value.ToString("dd/MM/yyyy");
        row.Controls.Add(hireDate);
    }
}
