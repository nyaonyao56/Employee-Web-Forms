using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Login {
    public partial class EditEmployee : System.Web.UI.Page {

        public static string MyConnection = "DSN=myAccess2";
        static OdbcConnection MyConn = new OdbcConnection(MyConnection);
        static OdbcCommand myCommand;

        protected void Page_Load(object sender, EventArgs e) {

        }

        [WebMethod(EnableSession = true)]
        public static void EditEmpl(string lName, string fName, string jobTitle, DateTime startDate, DateTime endDate, string id) {
            var query = "UPDATE employee SET last_name = ?,  first_name = ?, job_title = ?, start_date = ?, end_date = ? WHERE ID = ?";
            myCommand = new OdbcCommand(query, MyConn);
            OdbcTransaction transaction = null;
            MyConn.Open();
            transaction = MyConn.BeginTransaction();
            myCommand.Connection = MyConn;
            myCommand.Transaction = transaction;
            myCommand.CommandText = query;
            myCommand.Parameters.Add("last_name", OdbcType.VarChar).Value = lName;
            myCommand.Parameters.Add("first_name", OdbcType.VarChar).Value = fName;
            myCommand.Parameters.Add("job_title", OdbcType.VarChar).Value = jobTitle;
            myCommand.Parameters.Add("start_date", OdbcType.DateTime).Value = startDate;
            myCommand.Parameters.Add("end_date", OdbcType.DateTime).Value = endDate;
            myCommand.Parameters.Add("ID", OdbcType.Int).Value = id;
            myCommand.ExecuteNonQuery();

            transaction.Commit();
            MyConn.Close();
        }
    }
}