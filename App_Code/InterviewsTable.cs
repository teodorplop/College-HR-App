using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Linq;
using System.Web;

public class Interview : TableEntry {
	public int id;
	public string studentEmail;
	public int positionId;
	public SqlDateTime date;
    public int result;
    public string interviewerUsername;

    public bool HasResult { get { return result != 0; } }
    public bool Accepted { get { return result == 1; } }
    public bool Refused { get { return result == 2; } }

    public void Accept() { result = 1; }
    public void Refuse() { result = 2; }

	public Interview(SqlDataReader reader) : base(reader) {
		id = reader.GetInt32(0);
		studentEmail = reader.GetString(1);
		positionId = reader.GetInt32(2);
		date = reader.GetSqlDateTime(3);
        result = reader.GetInt32(4);
        interviewerUsername = reader.GetString(5);
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
    public int result;
    public string interviewerUsername;
    public string interviewerFirstName;
    public string interviewerLastName;

    public bool HasResult { get { return result != 0; } }
    public bool Accepted { get { return result == 1; } }
    public bool Refused { get { return result == 2; } }

    public void Accept() { result = 1; }
    public void Refuse() { result = 2; }

    public StudentInterview(SqlDataReader reader) : base(reader) {
		interviewId = reader.GetInt32(0);
		firstName = reader.GetString(1);
		lastName = reader.GetString(2);
		email = reader.GetString(3);
		positionId = reader.GetInt32(4);
		date = reader.GetSqlDateTime(5);
		positionName = reader.GetString(6);
        result = reader.GetInt32(7);
        interviewerUsername = reader.GetString(8);
        interviewerFirstName = reader.GetString(9);
        interviewerLastName = reader.GetString(10);
	}
}

public class InterviewsTable : BaseTable<InterviewsTable, Interview> {
	public InterviewsTable() : base("INTERVIEWS") {
	}

	// TODO: duplicate code, again :(
	public bool Select(out List<StudentInterview> interviews, out string error) {
		string commandStr = "select INTERVIEWS.ID, STUDENTS.FIRSTNAME, STUDENTS.LASTNAME, STUDENTS.EMAIL, INTERVIEWS.POSITION, INTERVIEWS.DATE, POSITIONS.POSITION, INTERVIEWS.RESULT, INTERVIEWS.INTERVIEWER, USERS.FIRSTNAME, USERS.LASTNAME";
		commandStr += " from INTERVIEWS join STUDENTS on INTERVIEWS.STUDENTID=STUDENTS.EMAIL join POSITIONS on INTERVIEWS.POSITION=POSITIONS.ID join USERS on INTERVIEWS.INTERVIEWER=USERS.USERNAME ORDER BY INTERVIEWS.DATE";
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

    private int GetResult(bool accepted) {
        return accepted ? 1 : 2;
    }
    public bool Update(int interviewId, bool accepted, out string error) {
        string commandString = string.Format("UPDATE INTERVIEWS SET RESULT={0} WHERE ID={1}", GetResult(accepted), interviewId);
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

    public bool Update(int interviewId, SqlDateTime date, out string error) {
        string commandString = "UPDATE INTERVIEWS SET DATE=@dateVar WHERE ID=@idVar";
        SqlCommand command = new SqlCommand(commandString, connection);

        SqlParameter dateVar = new SqlParameter("@dateVar", SqlDbType.DateTime, 0);
        dateVar.Value = date;
        command.Parameters.Add(dateVar);

        SqlParameter idVar = new SqlParameter("@idVar", SqlDbType.Int, 0);
        idVar.Value = interviewId;
        command.Parameters.Add(idVar);

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

	public bool Insert(string studentEmail, int positionId, string interviewer, out string error) {
        studentEmail = studentEmail.Replace("'", "''");
        interviewer = interviewer.Replace("'", "''");

		string commandString = string.Format("INSERT INTO INTERVIEWS(StudentId, Position, Interviewer) VALUES('{0}', '{1}', '{2}')", studentEmail, positionId, interviewer);
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
