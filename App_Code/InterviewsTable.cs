using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
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

public class StudentInterview : TableEntry {
	public int interviewId;
	public string firstName;
	public string lastName;
	public string email;
	public int positionId;
	public SqlDateTime date;
	public string positionName;

	public StudentInterview(SqlDataReader reader) : base(reader) {
		interviewId = reader.GetInt32(0);
		firstName = reader.GetString(1);
		lastName = reader.GetString(2);
		email = reader.GetString(3);
		positionId = reader.GetInt32(4);
		date = reader.GetSqlDateTime(5);
		positionName = reader.GetString(6);
	}
}

public class InterviewsTable : BaseTable<InterviewsTable, Interview> {
	public InterviewsTable() : base("INTERVIEWS") {
	}

	// TODO: duplicate code, again :(
	public bool Select(out List<StudentInterview> interviews, out string error) {
		string commandStr = "select INTERVIEWS.ID, STUDENTS.FIRSTNAME, STUDENTS.LASTNAME, STUDENTS.EMAIL, INTERVIEWS.POSITION, INTERVIEWS.DATE, POSITIONS.POSITION";
		commandStr += " from INTERVIEWS join STUDENTS on INTERVIEWS.STUDENTID=STUDENTS.EMAIL join POSITIONS on INTERVIEWS.POSITION=POSITIONS.ID ORDER BY INTERVIEWS.DATE";
		SqlCommand command = new SqlCommand(commandStr, connection);

		SqlDataReader reader = null;
		interviews = new List<StudentInterview>();
		try {
			connection.Open();
			reader = command.ExecuteReader();

			if (reader.HasRows)
				while (reader.Read())
					interviews.Add(new StudentInterview(reader));
		} catch (Exception ex) {
			error = "InterviewsTable: " + ex.Message;
			return false;
		} finally {
			if (reader != null) reader.Close();
			connection.Close();
		}

		error = "";
		return true;
	}

	public bool Insert(string studentEmail, int positionId, out string error) {
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
