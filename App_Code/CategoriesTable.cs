using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

public class CategoriesTable : BaseTable<CategoriesTable> {
	public CategoriesTable() : base("CATEGORIES") { }
}