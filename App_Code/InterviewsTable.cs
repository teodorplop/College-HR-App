using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class Interview : TableEntry {
	public int id;
	public string studentEmail;
	public int positionId;
	public DateTime date;

	public Interview(SqlDataReader reader) : base(reader) {
		id = reader.GetInt32(0);
		studentEmail = reader.GetString(1);
		positionId = reader.GetInt32(2);
		date = reader.GetDateTime(3);
	}
}

public class InterviewsTable : BaseTable<InterviewsTable, Interview> {
	public InterviewsTable() : base("INTERVIEWS") {
	}

	public bool Insert(out string error, string studentEmail, int positionId) {
		string commandString = string.Format("INSERT INTO INTERVIEWS(StudentId, Position) VALUES('{0}', '{1}')", studentEmail, positionId);
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
