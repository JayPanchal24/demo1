using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace demo1.Models
{
    public class Managedatabase
    {
        SqlConnection con = new SqlConnection(@"Data Source=JAY;Persist Security Info=True;User ID=sa;Password=Jaypanchal5571");
        public void getdata()
        {

        }
    }
}