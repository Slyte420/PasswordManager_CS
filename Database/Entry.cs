using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Database
{
    public class Entry
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? URL { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Notes { get; set; }
        public User user { get; set; } = null!;
        public EntryGroup group { get; set; } = null!;
    }
}
