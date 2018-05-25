using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

public class Employee : TableEntry {
    public int id;
    public string studentId;
    public int positionId;

    public Employee(SqlDataReader reader) : base(reader) {
        id = reader.GetInt32(0);
        studentId = reader.GetString(1);
        positionId = reader.GetInt32(2);
    }
}

public class EmployeePosition : TableEntry {
    public int employeeId;
    public string firstName;
    public string lastName;
    public string email;
    public int positionId;
    public string positionName;
    public SqlDateTime hireDate;

    public EmployeePosition(SqlDataReader reader) : base(reader) {
        employeeId = reader.GetInt32(0);
        firstName = reader.GetString(1);
        lastName = reader.GetString(2);
        email = reader.GetString(3);
        positionId = reader.GetInt32(4);
        positionName = reader.GetString(5);
        hireDate = reader.GetSqlDateTime(6);
    }
}

public class EmployeesTable : BaseTable<EmployeesTable, Employee> {
    public EmployeesTable() : base("EMPLOYEES") {
    }

    // TODO: duplicate code, again :(
    public bool Select(out List<EmployeePosition> employees, out string error) {
        string commandStr = "select EMPLOYEES.ID, STUDENTS.FIRSTNAME, STUDENTS.LASTNAME, STUDENTS.EMAIL, EMPLOYEES.POSITION, POSITIONS.POSITION, EMPLOYEES.HIREDATE";
        commandStr += " from EMPLOYEES join STUDENTS on EMPLOYEES.STUDENTID=STUDENTS.EMAIL join POSITIONS on EMPLOYEES.POSITION=POSITIONS.ID ORDER BY EMPLOYEES.HIREDATE";
        SqlCommand command = new SqlCommand(commandStr, connection);

        SqlDataReader reader = null;
        employees = new List<EmployeePosition>();
        try {
            connection.Open();
            reader = command.ExecuteReader();

            if (reader.HasRows)
                while (reader.Read())
                    employees.Add(new EmployeePosition(reader));
        } catch (Exception ex) {
            error = "EmployeesTable: " + ex.Message;
            return false;
        } finally {
            if (reader != null) reader.Close();
            connection.Close();
        }

        error = "";
        return true;
    }

    public bool Insert(string studentEmail, int positionId, out string error) {
        studentEmail = studentEmail.Replace("'", "''");

        string commandString = string.Format("INSERT INTO EMPLOYEES(StudentId, Position) VALUES('{0}', '{1}')", studentEmail, positionId);
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
