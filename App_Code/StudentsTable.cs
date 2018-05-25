using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

public class Student : TableEntry {
	public string email;
	public string firstName;
	public string lastName;
	public string college;

	public Student(SqlDataReader reader) : base(reader) {
		email = reader.GetString(0);
		firstName = reader.GetString(1);
		lastName = reader.GetString(2);
		college = reader.GetString(3);
	}
}

public class StudentsTable : BaseTable<StudentsTable, Student> {
	public StudentsTable() : base("STUDENTS") { }

    public bool Select(string email, out bool exists, out string error) {
        string commandStr = string.Format("SELECT count(*) FROM STUDENTS WHERE Email='{0}'", email);
        SqlCommand command = new SqlCommand(commandStr, connection);

        SqlDataReader reader = null;
        int total = 0;
        exists = false;
        try {
            connection.Open();
            reader = command.ExecuteReader();

            if (reader.HasRows && reader.Read()) {
                total = reader.GetInt32(0);
                exists = total > 0;
            }
        } catch (Exception ex) {
            error = "StudentsTable: " + ex.Message;
            return false;
        } finally {
            if (reader != null) reader.Close();
            connection.Close();
        }

        error = "Positions successfully selected!";
        return true;
    }

    public bool Insert(string email, string firstName, string lastName, string college, out string error) {
        email = email.Replace("'", "''");
        firstName = firstName.Replace("'", "''");
        lastName = lastName.Replace("'", "''");
        college = college.Replace("'", "''");

        string commandString = string.Format("INSERT INTO STUDENTS(Email, FirstName, LastName, College) VALUES('{0}', '{1}', '{2}', '{3}')", email, firstName, lastName, college);
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