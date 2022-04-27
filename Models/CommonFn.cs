using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ZealEducationManager.Models
{
    public class CommonFn
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ZealEducationManager"].ConnectionString);
        public class Commonfnx
        {
            public void Query(string query)
            {

            }
        }
    }
}