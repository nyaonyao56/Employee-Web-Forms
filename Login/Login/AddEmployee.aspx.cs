using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Login
{
    public partial class AddEmployee : System.Web.UI.Page
    {

        public static string MyConnection = "DSN=myAccess2";
        static OdbcConnection MyConn = new OdbcConnection(MyConnection);
        static OdbcCommand myCommand;
        static string[] file_extensions = { ".jpg", ".png", ".gif" };
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        public static void AddEmp(string lName, string fName, string jobTitle, DateTime startDate, DateTime endDate)
        {
            byte[] arr = null;
            HttpPostedFile img = HttpContext.Current.Request.Files["fileUpload"];
            string extension = Path.GetExtension(img.FileName);

            if (img.ContentLength > 0 && check_extensions(extension)) {
                string filepath = HttpContext.Current.Server.MapPath(Path.GetFileName(img.FileName));
                img.SaveAs(filepath);
                arr = convert_Picture(filepath);

                File.Delete(filepath);
            }

            string query;

            if(arr != null) {
                query = "INSERT INTO employee (picture, last_name, first_name, job_title, start_date, end_date) VALUES (?, ?, ?, ?, ?, ?)";
            }else {
                query = "INSERT INTO employee (last_name, first_name, job_title, start_date, end_date) VALUES (?, ?, ?, ?, ?)";
            }

            myCommand = new OdbcCommand(query, MyConn);
            OdbcTransaction transaction = null;
            MyConn.Open();
            transaction = MyConn.BeginTransaction();
            myCommand.Connection = MyConn;
            myCommand.Transaction = transaction;
            myCommand.CommandText = query;
            if(arr != null) {
                myCommand.Parameters.Add("picture", OdbcType.VarBinary).Value = arr;
            }
            myCommand.Parameters.Add("last_name", OdbcType.VarChar).Value = lName;
            myCommand.Parameters.Add("first_name", OdbcType.VarChar).Value = fName;
            myCommand.Parameters.Add("job_title", OdbcType.VarChar).Value = jobTitle;
            myCommand.Parameters.Add("start_date", OdbcType.DateTime).Value = startDate;
            myCommand.Parameters.Add("end_date", OdbcType.DateTime).Value = endDate;
            myCommand.ExecuteNonQuery();
            transaction.Commit();
            MyConn.Close();
        }

        public static byte[] convert_Picture(string img) {
            bool isValidFileType = true;

            if (isValidFileType) {
                FileStream fs = new FileStream(img, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                byte[] image = br.ReadBytes((int)fs.Length);
                br.Close();
                fs.Close();
                return image;
            } else
                return null;
        }

        public static bool check_extensions(string fileExtension) {
            foreach (string ex in file_extensions) {
                if (ex.Equals(fileExtension))
                    return true;
            }
            return false;
        }
    }
}