using Microsoft.Data.SqlClient;

namespace Database
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
            con.ConnectionString = "Server=localhost\\SQLEXPRESS;Database=PasswordDB;Trusted_Connection=True;";
        }
    }
}