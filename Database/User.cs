using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    internal class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Salt { get; set; } = null!;
    }
}
