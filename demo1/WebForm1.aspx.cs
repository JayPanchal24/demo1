using demo1.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;


namespace demo1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        //Sql Connection Enter you connection string here
        SqlConnection con = new SqlConnection(@"connection string of database");

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //Get Data from given API
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {

                string url = @"https://jsonplaceholder.typicode.com/todos/";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    //HTTP GET
                    var responseTask = client.GetAsync("");



                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        var readTask = result.Content.ReadAsAsync<GetAPI[]>();
                        // readTask.Wait();

                        var values = readTask.Result;

                        foreach (var value in values)
                        {
                            //Fetch the value one by one and sored in database 
                            string UserID = value.UserId.ToString();
                            string Id = value.Id.ToString();
                            string Title = value.Title.ToString();
                            string Completed = value.Completed.ToString();

                            

                            string num = Convert.ToInt32(Id) % 2 == 0 ? "even" : "odd";  // Odd Even Check
                            //If even then storing value in even table 
                            if (num == "even")
                            {
                                try
                                {

                                    string insert = @"INSERT INTO [dbo].[GetDataEven] ([UserID] ,[ID] ,[Title] ,[Completed] ,[DateInsert])
 VALUES ('" + UserID + @"','" + Id + @"','" + Title + @"','" + Completed + @"',getdate())
                                ";
                                    con.Open();
                                    SqlCommand comm = new SqlCommand(insert, con);
                                    comm.ExecuteNonQuery();
                                    con.Close();
                                }
                                //If there is any exception it will catach and close the connection 
                                catch (Exception)
                                {
                                    if (con.State == ConnectionState.Open)
                                    {
                                        con.Close();
                                    }

                                }
                            }
                            //If odd then storing value in odd table 
                            else
                            {
                                try
                                {

                                    string insert = @"INSERT INTO [dbo].[GetDataOdd] ([UserID] ,[ID] ,[Title] ,[Completed] ,[DateInsert])
 VALUES ('" + UserID + @"','" + Id + @"','" + Title + @"','" + Completed + @"',getdate())
                                ";
                                    con.Open();
                                    SqlCommand comm = new SqlCommand(insert, con);
                                    comm.ExecuteNonQuery();
                                    con.Close();
                                }
                                //If there is any exception it will catach and close the connection 
                                catch (Exception)
                                {
                                    if (con.State == ConnectionState.Open)
                                    {
                                        con.Close();
                                    }

                                }
                            }

                        }
                    }
                }
                Label1.Text = "Data Inserted successful";
            }
            //If there is any exception it will show
            catch (Exception ex)
            {
                Label1.Text = ex.ToString();
            }
        }
        //Edit button here the entered id will get into the third edit table 
        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                //checks that there is value or not
                if (TextBox1.Text.Length > 1)
                {
                    string insert = @"insert into GetDataEdited  select UserID,id,title,completed,'edited',DateInsert   from GetDataEven 
where id ='" + TextBox1.Text.ToString() + @"'";
                    con.Open();
                    SqlCommand comm = new SqlCommand(insert, con);
                    comm.ExecuteNonQuery();
                    con.Close();
                    Label1.Text = "Data Edited successful";
                }
                else
                {
                    Label1.Text = "* Enter value";
                }
            }
            //If there is any exception it will catach and close the connection 
            catch (Exception ex)
            {
                Label1.Text = ex.ToString();
            }
        }
        //------------------------------------------------------------------------------------------
    }
}