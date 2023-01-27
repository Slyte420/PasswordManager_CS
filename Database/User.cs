using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public byte[] Password { get; set; } = null!;
        public byte[] Salt { get; set; } = null!;
        public byte[] IV { get; set; } = null!;
        public byte[] keyHash { get; set; } = null!;
    }
}
