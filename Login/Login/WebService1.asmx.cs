using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace Login {
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : WebService {

        public static string MyConnection = "DSN=myAccess2";
        public OdbcConnection MyConn = new OdbcConnection(MyConnection);
        OdbcCommand myCommand;
        //OdbcDataAdapter adapter;

        [WebMethod]
        public void Get_Employee() {
            List<Employees> emp = new List<Employees>();
            var query = "SELECT * FROM employee";
            MyConn = new OdbcConnection(MyConnection);
            myCommand = new OdbcCommand(query, MyConn);
            MyConn.Open();
            OdbcDataReader reader = myCommand.ExecuteReader();
            while (reader.Read()) {
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
            //adapter = new OdbcDataAdapter(myCommand);
            //DataTable table = new DataTable();
            //adapter.Fill(table);
            //table.Columns.Add("images");

            /*
            foreach (DataRow row in table.Rows) {
                if (row["picture"] != DBNull.Value) {
                    byte[] imgArr = (byte[])row["picture"];
                    //Image img = (ListView1.FindControl("image")) as Image;
                    string base64String = Convert.ToBase64String(imgArr, 0, imgArr.Length);
                    row["images"] = "data:image/png;base64," + base64String;
                }
            }
            */
            

            MyConn.Close();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(emp));

            //ListView1.DataSource = table;
            //ListView1.DataBind();
            //return table;
        }
    }
}
