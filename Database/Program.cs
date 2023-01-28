using Database.PasswordDB;
using Microsoft.Data.SqlClient;
using System.Text;

namespace Database
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
            con.ConnectionString = "Server=localhost\\SQLEXPRESS;Database=PasswordDB;Trusted_Connection=True;";
            SQLSContext context = new SQLSContext();
            //User user = new User { Username = "Lol",IV= new byte[16] ,Password = Encoding.ASCII.GetBytes("lol"),KeyHash = new byte[16], Salt = new byte[16] };
            //context.Add(user);
            //context.SaveChanges();
            EntryGroup group = new EntryGroup { Name = "General", userId = 6 };
            context.Add(group);
            context.SaveChanges();
            context.Add(new Entry { Password = "lol", UserId = 6 });
            context.Add(new Entry { Password = "lol", UserId = 6, GroupId = 2 });
            context.SaveChanges();
        }
    }
}