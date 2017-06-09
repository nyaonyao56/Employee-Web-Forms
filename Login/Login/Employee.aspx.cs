using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.Script.Services;
using Newtonsoft.Json;

namespace Login
{
    public partial class Employee : System.Web.UI.Page
    {

        public static string MyConnection = "DSN=myAccess2";
        static OdbcConnection MyConn = new OdbcConnection(MyConnection);
        static OdbcCommand myCommand;
        OdbcDataAdapter adapter;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack) {
                //ListView1.DataSource = GetEmployee();
                //ListView1.DataBind();
                //GetEmployee();
            }
            
            
        }

        [WebMethod(EnableSession = true)]
        public void GetEmployee() {
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

            MyConn.Close();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(emp));
        }
        
        

        /*    
        [WebMethod(EnableSession = true)]
        public static List<Employees> GetEmployee() {
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

            MyConn.Close();
            return emp;
        }
        */
        

        /*
        public Byte[] imageConverter() {
            FileUpload image = ListView1.InsertItem.FindControl("pictureUpload") as FileUpload;
            string filePath = image.PostedFile.FileName;
            string fileName = Path.GetFileName(filePath);
            string extension = Path.GetExtension(fileName);
           
            Stream fs = image.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bytes = br.ReadBytes((Int32)fs.Length);
            return bytes;
        }

        protected void ListView1_ItemUpdating(object sender, ListViewUpdateEventArgs e) {
            //var query = "UPDATE employee SET picture = ?, last_name = ?,  first_name = ?, job_title = ?, start_date = ?, end_date = ? WHERE ID = ?";
            Label id = (ListView1.Items[e.ItemIndex].FindControl("IDLabel1")) as Label;
            FileUpload img = ListView1.EditItem.FindControl("picUpload") as FileUpload;
            TextBox last = (ListView1.Items[e.ItemIndex].FindControl("last_nameTextBox")) as TextBox;
            TextBox first = (ListView1.Items[e.ItemIndex].FindControl("first_nameTextBox")) as TextBox;
            TextBox job = (ListView1.Items[e.ItemIndex].FindControl("job_titleTextBox")) as TextBox;
            TextBox start = (ListView1.Items[e.ItemIndex].FindControl("start_dateTextBox")) as TextBox;
            TextBox end = (ListView1.Items[e.ItemIndex].FindControl("end_dateTextBox")) as TextBox;

            if(img.HasFile == true) {
                var query = "UPDATE employee SET picture = ?, last_name = ?,  first_name = ?, job_title = ?, start_date = ?, end_date = ? WHERE ID = ?";
                myCommand = new OdbcCommand();
                OdbcTransaction transaction = null;
                MyConn.Open();
                transaction = MyConn.BeginTransaction();
                myCommand.Connection = MyConn;
                myCommand.Transaction = transaction;
                myCommand.CommandText = query;
                myCommand.Parameters.Add("picture", OdbcType.VarBinary).Value = imageConverter();
                myCommand.Parameters.Add("last_name", OdbcType.VarChar).Value = last.Text;
                myCommand.Parameters.Add("first_name", OdbcType.VarChar).Value = first.Text;
                myCommand.Parameters.Add("job_title", OdbcType.VarChar).Value = job.Text;
                myCommand.Parameters.Add("start_date", OdbcType.DateTime).Value = start.Text;
                myCommand.Parameters.Add("end_date", OdbcType.DateTime).Value = end.Text;
                myCommand.Parameters.Add("ID", OdbcType.Int).Value = id.Text;
                myCommand.ExecuteNonQuery();

                transaction.Commit();
                MyConn.Close();
            } else {
                var query = "UPDATE employee SET last_name = ?,  first_name = ?, job_title = ?, start_date = ?, end_date = ? WHERE ID = ?";
                myCommand = new OdbcCommand();
                OdbcTransaction transaction = null;
                MyConn.Open();
                transaction = MyConn.BeginTransaction();
                myCommand.Connection = MyConn;
                myCommand.Transaction = transaction;
                myCommand.CommandText = query;
                myCommand.Parameters.Add("last_name", OdbcType.VarChar).Value = last.Text;
                myCommand.Parameters.Add("first_name", OdbcType.VarChar).Value = first.Text;
                myCommand.Parameters.Add("job_title", OdbcType.VarChar).Value = job.Text;
                myCommand.Parameters.Add("start_date", OdbcType.DateTime).Value = start.Text;
                myCommand.Parameters.Add("end_date", OdbcType.DateTime).Value = end.Text;
                myCommand.Parameters.Add("ID", OdbcType.Int).Value = id.Text;
                myCommand.ExecuteNonQuery();

                transaction.Commit();
                MyConn.Close();
            }

            ListView1.EditIndex = -1;
            ListView1.DataSource = GetEmployee();
            ListView1.DataBind();
        }

        protected void ListView1_ItemEditing(object sender, ListViewEditEventArgs e) {
            ListView1.EditIndex = e.NewEditIndex;
            ListView1.DataSource = GetEmployee();
            ListView1.DataBind();
        }

        protected void ListView1_ItemCanceling(object sender, ListViewCancelEventArgs e) {
            ListView1.EditIndex = -1;
            ListView1.DataSource = GetEmployee();
            ListView1.DataBind();
        }

        protected void ListView1_ItemInserting(object sender, ListViewInsertEventArgs e) {
            FileUpload image = ListView1.InsertItem.FindControl("pictureUpload") as FileUpload;
            TextBox last = (e.Item.FindControl("last_nameTextBox")) as TextBox;
            TextBox first = (e.Item.FindControl("first_nameTextBox")) as TextBox;
            TextBox job = (e.Item.FindControl("job_titleTextBox")) as TextBox;
            TextBox start = (e.Item.FindControl("start_dateTextBox")) as TextBox;
            TextBox end = (e.Item.FindControl("end_dateTextBox")) as TextBox;

            if (image.HasFile == true) {
                var query = "INSERT INTO employee (picture, last_name, first_name, job_title, start_date, end_date) VALUES (?, ?, ?, ?, ?, ?)";
                myCommand = new OdbcCommand(query, MyConn);
                OdbcTransaction transaction = null;
                MyConn.Open();
                transaction = MyConn.BeginTransaction();
                myCommand.Connection = MyConn;
                myCommand.Transaction = transaction;
                myCommand.CommandText = query;
                myCommand.Parameters.Add("picture", OdbcType.VarBinary).Value = imageConverter();
                myCommand.Parameters.Add("last_name", OdbcType.VarChar).Value = last.Text;
                myCommand.Parameters.Add("first_name", OdbcType.VarChar).Value = first.Text;
                myCommand.Parameters.Add("job_title", OdbcType.VarChar).Value = job.Text;
                myCommand.Parameters.Add("start_date", OdbcType.DateTime).Value = start.Text;
                myCommand.Parameters.Add("end_date", OdbcType.DateTime).Value = end.Text;
                myCommand.ExecuteNonQuery();

                transaction.Commit();
                MyConn.Close();
            } else {
                var query = "INSERT INTO employee (last_name, first_name, job_title, start_date, end_date) VALUES (?, ?, ?, ?, ?)";
                myCommand = new OdbcCommand(query, MyConn);
                OdbcTransaction transaction = null;
                MyConn.Open();
                transaction = MyConn.BeginTransaction();
                myCommand.Connection = MyConn;
                myCommand.Transaction = transaction;
                myCommand.CommandText = query;
                myCommand.Parameters.Add("last_name", OdbcType.VarChar).Value = last.Text;
                myCommand.Parameters.Add("first_name", OdbcType.VarChar).Value = first.Text;
                myCommand.Parameters.Add("job_title", OdbcType.VarChar).Value = job.Text;
                myCommand.Parameters.Add("start_date", OdbcType.DateTime).Value = start.Text;
                myCommand.Parameters.Add("end_date", OdbcType.DateTime).Value = end.Text;
                myCommand.ExecuteNonQuery();

                transaction.Commit();
                MyConn.Close();
            }

            ListView1.EditIndex = -1;
            ListView1.DataSource = GetEmployee();
            ListView1.DataBind();
        }

        protected void ListView1_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            Label id = (ListView1.Items[e.ItemIndex].FindControl("IDLabel")) as Label;

            var query = "DELETE FROM employee WHERE ID= ?";
            myCommand = new OdbcCommand(query, MyConn);
            OdbcTransaction transaction = null;
            MyConn.Open();
            transaction = MyConn.BeginTransaction();
            myCommand.Connection = MyConn;
            myCommand.Transaction = transaction;
            myCommand.CommandText = query;
            myCommand.Parameters.Add("ID", OdbcType.Int).Value = id.Text;
            myCommand.ExecuteNonQuery();

            transaction.Commit();
            MyConn.Close();

            ListView1.EditIndex = -1;
            ListView1.DataSource = GetEmployee();
            ListView1.DataBind();
        }
        */
    }
}