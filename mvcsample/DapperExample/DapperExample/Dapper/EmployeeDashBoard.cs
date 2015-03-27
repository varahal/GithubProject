using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;

namespace DapperExample.Dapper
{
    public class EmployeeDashBoard : IEmployeeDashBoard
    {
        private IDbConnection _db = new SqlConnection("Data Source=CD-GLIYANAGE\\SQLEXPRESS; Database= Test; Integrated Security=True;");

        public List<Employee> GetAll()
        {
            List < Employee > empList= this._db.Query<Employee>("SELECT * FROM tblEmployee").ToList();
            return empList;
        }

        public Employee Find(int? id)
        {
            string query = "SELECT * FROM tblEmployee WHERE EmpID = " + id + "";
            return this._db.Query<Employee>(query).SingleOrDefault();
        }

        public Employee Add(Employee employee)
        {
            var sqlQuery = "INSERT INTO tblEmployee (EmpName, EmpScore) VALUES(@EmpName, @EmpScore); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var employeeId = this._db.Query<int>(sqlQuery, employee).Single();
            employee.EmpID = employeeId;
            return employee;
        }

        public Employee Update(Employee employee)
        {
            var sqlQuery =
            "UPDATE tblEmployee " +
            "SET EmpName = @EmpName, " +
            " EmpScore = @EmpScore " +
            "WHERE EmpID = @EmpID";
            this._db.Execute(sqlQuery, employee);
            return employee;
        }

        public void Remove(int id)
        {
            var sqlQuery =("Delete From tblEmployee Where EmpID = " + id + "");
            this._db.Execute(sqlQuery);
        }
    }
}