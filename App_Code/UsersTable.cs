using System;
using System.Configuration;
using System.Data.SqlClient;

public class User : TableEntry {
    public string username;
    public string password;
    public string first;
    public string last;
    public User(SqlDataReader reader) : base(reader) {
        username = reader.GetString(0);
        password = reader.GetString(1);
        first = reader.GetString(2);
        last = reader.GetString(3);
    }
}

public class UsersTable : BaseTable<UsersTable, User> {
    private UsersTable() : base("USERS") {
    }

    public bool Insert(string username, string password, string first, string last, out string error) {
        if (string.IsNullOrWhiteSpace(username)) {
            error = "Please insert a username.";
            return false;
        }
        if (string.IsNullOrEmpty(password)) {
            error = "Please insert a password";
            return false;
        }

        User user = null;
        bool success = SelectUser(username, out user, out error);

        if (!success) {
            return false;
        }
        if (user != null) {
            error = "User already exists.";
            return false;
        }

        string commandString = string.Format("INSERT INTO USERS (Username, Password, FirstName, LastName) VALUES('{0}', '{1}', '{2}', '{3}')", username, password, first, last);
        SqlCommand command = new SqlCommand(commandString, connection);

        try {
            connection.Open();
            command.ExecuteNonQuery();
        } catch (Exception ex) {
            error = ex.Message;
        } finally {
            connection.Close();
        }

        error = "User created successfully!";
        return true;
    }

    public bool SelectUser(string username, out User user, out string error) {
        string commandString = "SELECT Username, Password, FirstName, LastName FROM USERS WHERE Username=\'" + username + "\'";
        SqlCommand command = new SqlCommand(commandString, connection);

        SqlDataReader reader = null;
        user = null;
        try {
            connection.Open();
            reader = command.ExecuteReader();

            if (reader.HasRows && reader.Read())
                user = new User(reader);
        } catch (Exception ex) {
            error = ex.Message;
            System.Diagnostics.Debug.WriteLine(error);

        } finally {
            if (reader != null) reader.Close();
            connection.Close();
        }

        error = "User selected successfully!";
        return true;
    }
}