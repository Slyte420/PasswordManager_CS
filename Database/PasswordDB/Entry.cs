using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Database.PasswordDB
{
    public class Entry
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? URL { get; set; }
        public string? Username { get; set; }
        public string Password { get; set; } = null!;
        public string? Note { get; set; }

        public int UserId { get; set; }
        public int? GroupId { get; set; }
        public virtual User User { get; set; }= null!;
        public EntryGroup? Group { get; set; }
    }
}
