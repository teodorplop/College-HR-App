using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;

public class BaseTable<T> : Singleton<T> where T : class {
	private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["HRDBConnectionString"].ConnectionString;

	protected SqlConnection connection;
	protected string table;
	protected BaseTable(string table) {
		connection = new SqlConnection(_connectionString);
		this.table = table;
	}

	public bool Select(out List<string> entries, out string error) {
		string commandStr = "SELECT * FROM " + table;
		SqlCommand command = new SqlCommand(commandStr, connection);

		SqlDataReader reader = null;
		entries = new List<string>();
		try {
			connection.Open();
			reader = command.ExecuteReader();

			if (reader.HasRows)
				while (reader.Read())
					entries.Add(reader.GetString(0));
		} catch (Exception ex) {
			error = table + ": " + ex.Message;
			return false;
		} finally {
			if (reader != null) reader.Close();
			connection.Close();
		}

		error = "";
		return true;
	}
}

public class TableEntry {
	public TableEntry(SqlDataReader reader) { }
}
public class BaseTable<T, U> : BaseTable<T> where T : class where U : TableEntry {
	protected BaseTable(string table) : base(table) { }

	public bool Select(out List<U> entries, out string error) {
		string commandStr = "SELECT * FROM " + table;
		SqlCommand command = new SqlCommand(commandStr, connection);

		SqlDataReader reader = null;
		entries = new List<U>();
		try {
			connection.Open();
			reader = command.ExecuteReader();

			if (reader.HasRows)
				while (reader.Read()) {
					U entry = Activator.CreateInstance(typeof(U), reader) as U;
					entries.Add(entry);
				}
		} catch (Exception ex) {
			error = table + ": " + ex.Message;
			return false;
		} finally {
			if (reader != null) reader.Close();
			connection.Close();
		}

		error = "";
		return true;
	}
}