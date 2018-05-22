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
}