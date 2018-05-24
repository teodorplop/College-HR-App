using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;

public class Position : TableEntry {
	public int id;
	public string position;
	public string category;
	public string project;
	public string location;
	public string type;
	public string description;

	public Position(SqlDataReader reader) : base(reader) {
		id = reader.GetInt32(0);
		position = reader.GetString(1);
		category = reader.GetString(2);
		project = reader.GetString(3);
		location = reader.GetString(4);
		type = reader.GetString(5);
		description = reader.GetString(6);
	}
}

public class PositionsTable : BaseTable<PositionsTable, Position> {
	public PositionsTable() : base("POSITIONS") { }

    public bool Insert(string position, string category, string project, string location, string type, string description, out string error) {
        string commandString = string.Format("INSERT INTO POSITIONS(Position, Category, Project, Location, Type, Description) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", position, category, project, location, type, description);
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

    public bool Select(out int total, out string error) {
		string commandStr = "SELECT count(*) FROM POSITIONS";
		SqlCommand command = new SqlCommand(commandStr, connection);

		SqlDataReader reader = null;
		total = 0;
		try {
			connection.Open();
			reader = command.ExecuteReader();

			if (reader.HasRows && reader.Read())
				total = reader.GetInt32(0);
		} catch (Exception ex) {
			error = "PositionsTable: " + ex.Message;
			return false;
		} finally {
			if (reader != null) reader.Close();
			connection.Close();
		}

		error = "Positions successfully selected!";
		return true;
	}

	// TODO: duplicated code, meh.
	public bool Select(int id, out Position position, out string error) {
		string commandStr = string.Format("SELECT * FROM POSITIONS WHERE ID={0}", id);
		SqlCommand command = new SqlCommand(commandStr, connection);

		SqlDataReader reader = null;
		position = null;
		try {
			connection.Open();
			reader = command.ExecuteReader();

			if (reader.HasRows && reader.Read())
				position = new Position(reader);
		} catch (Exception ex) {
			error = "PositionsTable: " + ex.Message;
			return false;
		} finally {
			if (reader != null) reader.Close();
			connection.Close();
		}

		error = "Position successfully selected!";
		return true;
	}
}
