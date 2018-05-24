using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

public class CategoriesTable : BaseTable<CategoriesTable> {
	public CategoriesTable() : base("CATEGORIES") { }

    public bool Insert(string category, out string error) {
        string commandString = string.Format("INSERT INTO CATEGORIES(Name) VALUES('{0}')", category);
        SqlCommand command = new SqlCommand(commandString, connection);

        try {
            connection.Open();
            command.ExecuteNonQuery();
        } catch (Exception ex) {
            error = ex.Message;
            return false;
        } finally {
            connection.Close();
        }

        error = "Success!";
        return true;
    }
}