using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace BooksServer
{
    public class DAL
    {
        readonly string con;
        public DAL()
        {
            //con = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            con = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Books;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
        

        public IEnumerable<dynamic> RunQuery(string query)
        {
            using (var connection = new SqlConnection(con))
            {
                var result = connection.Query(query);
                return result;
            }
        }
    }
}
