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
    public partial class DeleteEmployee : System.Web.UI.Page {

        public static string MyConnection = "DSN=myAccess2";
        static OdbcConnection MyConn = new OdbcConnection(MyConnection);
        static OdbcCommand myCommand;

        protected void Page_Load(object sender, EventArgs e) {
        
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void DeleteRecords(string id) {
            var query = "DELETE FROM employee WHERE ID= ?";
            myCommand = new OdbcCommand(query, MyConn);
            OdbcTransaction transaction = null;
            MyConn.Open();
            transaction = MyConn.BeginTransaction();
            myCommand.Connection = MyConn;
            myCommand.Transaction = transaction;
            myCommand.CommandText = query;
            myCommand.Parameters.Add("ID", OdbcType.Int).Value = id;
            myCommand.ExecuteNonQuery();

            transaction.Commit();
            MyConn.Close();
        }
    }
}