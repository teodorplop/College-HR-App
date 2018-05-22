using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class openings : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		string error;

		List<string> categories;
		if (!CategoriesTable.Instance.Select(out categories, out error)) {
			(Master as MasterPage).ShowError(false, error);
			return;
		}
		
		List<Position> positions;
		if (!PositionsTable.Instance.Select(out positions, out error)) {
			(Master as MasterPage).ShowError(false, error);
			return;
		}

		PopulatePositions(categories, positions);
	}

	private void PopulatePositions(List<string> categories, List<Position> positions) {
		OpenPositions.Text = string.Format("{0} open positions", positions.Count);
		
		for (int i = 0; i < categories.Count; ++i)
			CreatePanel(categories[i], positions.Where(obj => obj.category == categories[i]));
	}

	private Panel CreatePanel(string category, IEnumerable<Position> positions) {
		Panel panel = new Panel();
		panel.ID = "Panel" + category;
		panel.CssClass = "container";
		MainPanel.Controls.Add(panel);

		HtmlGenericControl header = new HtmlGenericControl();
		header.TagName = "h5";
		header.InnerHtml = category;
		panel.Controls.Add(header);

		foreach (Position pos in positions) {
			HtmlGenericControl a = new HtmlGenericControl();
			a.TagName = "a";
			a.Attributes["class"] = "row mb-3";
			a.Attributes["href"] = "job.aspx?id=" + pos.id;
			panel.Controls.Add(a);

			HtmlGenericControl posDiv = new HtmlGenericControl();
			posDiv.TagName = "div";
			posDiv.Attributes["class"] = "col-5";
			posDiv.InnerHtml = pos.position;
			a.Controls.Add(posDiv);

			HtmlGenericControl projectDiv = new HtmlGenericControl();
			projectDiv.TagName = "div";
			projectDiv.Attributes["class"] = "col-3";
			projectDiv.InnerHtml = pos.project;
			a.Controls.Add(projectDiv);

			HtmlGenericControl locationDiv = new HtmlGenericControl();
			locationDiv.TagName = "div";
			locationDiv.Attributes["class"] = "col-2";
			locationDiv.InnerHtml = pos.location;
			a.Controls.Add(locationDiv);

			HtmlGenericControl typeDiv = new HtmlGenericControl();
			typeDiv.TagName = "div";
			typeDiv.Attributes["class"] = "col-2";
			typeDiv.InnerHtml = pos.type;
			a.Controls.Add(typeDiv);
		}

		return panel;
	}
}
