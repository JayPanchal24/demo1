using demo1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace demo1.Controllers
{
    public class GetAPIController : ApiController
    {
        /*
         This controller is Post method api which will return the 3 tables data into post response body.
         */
        public GetAPIList Post()
        {
            GetAPIList R = new GetAPIList();
            //Sql Connection
            SqlConnection con = new SqlConnection(@"Data Source = JAY; Initial Catalog = Demo1; Persist Security Info=True;User ID = sa; Password=Jaypanchal5571");

            //Create the list of the object
            List<GetAPI> Rlist = new List<GetAPI>();
            GetAPI Resp;

            R.GetAPI = Rlist;

            //Select Query 
            string Select = @"

select * from (
select UserID,id,title,completed,'False'as [Edited] from GetDataEven
where id not in (select id from GetDataEdited)
union
select UserID,id,title,completed,'False' as [Edited] from GetDataOdd
where id not in (select id from GetDataEdited)
union
select UserID,id,title,completed,'True' as [Edited] from GetDataEdited
)as abc order by abc.UserID ASC

                                ";

            con.Open();
            SqlCommand Comm = new SqlCommand(Select, con);

            //Reader to get the data which is stored in database
            SqlDataReader Reader = Comm.ExecuteReader();
            try
            {
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        Resp = new GetAPI();

                        Resp.UserId = Convert.ToInt32(Reader["UserId"].ToString());
                        Resp.Id = Convert.ToInt32(Reader["Id"].ToString());
                        Resp.Title = Reader["Title"].ToString();
                        Resp.Completed = Convert.ToBoolean(Reader["Completed"].ToString());
                        Resp.Edited = Convert.ToBoolean(Reader["Edited"].ToString());
                        Resp.edited_via_api = Convert.ToBoolean(Reader["Edited"].ToString());

                        //Response
                        Rlist.Add(Resp);
                    }
                }
            }
            //If there is any exception it will catach and close the connection 
            catch (Exception)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }


            return R;
        }
    }
}
