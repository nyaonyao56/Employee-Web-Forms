using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Login {
    public partial class GetEmployee : System.Web.UI.Page
    {

        public static string MyConnection = "DSN=myAccess2";
        OdbcConnection MyConn = new OdbcConnection(MyConnection);
        OdbcCommand myCommand;

        protected void Page_Load(object sender, EventArgs e)
        {
            List<Employees> emp = new List<Employees>();
            var query = "SELECT * FROM employee";
            MyConn = new OdbcConnection(MyConnection);
            myCommand = new OdbcCommand(query, MyConn);
            MyConn.Open();
            OdbcDataReader reader = myCommand.ExecuteReader();
            while (reader.Read())
            {
                Employees empl = new Employees();
                empl.Id = Convert.ToInt32(reader["ID"]);
                empl.image = reader["picture"].ToString();
                empl.lastName = reader["last_name"].ToString();
                empl.firstName = reader["first_name"].ToString();
                empl.jobTitle = reader["job_title"].ToString();
                empl.StartDate = Convert.ToDateTime(reader["start_date"]);
                empl.EndDate = Convert.ToDateTime(reader["end_date"]);
                emp.Add(empl);
            }

            /*
            foreach (DataRow row in table.Rows) {
                if (row["picture"] != DBNull.Value) {
                    byte[] imgArr = (byte[])row["picture"];
                    //Image img = (ListView1.FindControl("image")) as Image;
                    string base64String = Convert.ToBase64String(imgArr, 0, imgArr.Length);
                    row["images"] = "data:image/png;base64," + base64String;
                }
                */


            MyConn.Close();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(emp));
        }

    }
}