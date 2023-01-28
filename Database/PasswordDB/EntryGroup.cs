using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.PasswordDB
{
    public class EntryGroup
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public string Name { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
