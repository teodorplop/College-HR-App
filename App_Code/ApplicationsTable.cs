using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

public class Application : TableEntry {
	public int id;
	public string studentEmail;
	public int positionId;

	public Application(SqlDataReader reader) : base(reader) {
		id = reader.GetInt32(0);
		studentEmail = reader.GetString(1);
		positionId = reader.GetInt32(2);
	}
}

public class StudentApplication : TableEntry {
	public int applicationId;
	public string firstName;
	public string lastName;
	public string email;
	public string college;

	public StudentApplication(SqlDataReader reader) : base(reader) {
		applicationId = reader.GetInt32(0);
		firstName = reader.GetString(1);
		lastName = reader.GetString(2);
		email = reader.GetString(3);
		college = reader.GetString(4);
	}
}

public class ApplicationsTable : BaseTable<ApplicationsTable, Application> {
	public ApplicationsTable() : base("APPLICATIONS") {
	}

	public bool Select(int positionId, out List<StudentApplication> students, out string error) {
		string commandStr = "select APPLICATIONS.ID, STUDENTS.FIRSTNAME, STUDENTS.LASTNAME, STUDENTS.EMAIL, STUDENTS.COLLEGE";
		commandStr += " from APPLICATIONS join STUDENTS on APPLICATIONS.STUDENTID=STUDENTS.EMAIL where APPLICATIONS.POSITION={0}";
		commandStr = string.Format(commandStr, positionId);
		SqlCommand command = new SqlCommand(commandStr, connection);

		SqlDataReader reader = null;
		students = new List<StudentApplication>();
		try {
			connection.Open();
			reader = command.ExecuteReader();

			if (reader.HasRows)
				while (reader.Read())
					students.Add(new StudentApplication(reader));
		} catch (Exception ex) {
			error = "ApplicationsTable: " + ex.Message;
			return false;
		} finally {
			if (reader != null) reader.Close();
			connection.Close();
		}

		error = "";
		return true;
	}

	// duplicate, potential for greater things :(. too lazy
	public bool Delete(int appId, out string error) {
		string commandString = "DELETE FROM APPLICATIONS WHERE ID=" + appId;
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
