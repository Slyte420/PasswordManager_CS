using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.PasswordDB
{
    public class AsyncOperationsPassDB
    {
        public static async Task<List<Entry>> getEntriesAsync(int userId)
        {
            SQLSContext context= new SQLSContext();
            var queryEntries = from entry in context.Entries
                               where entry.UserId == userId
                               select entry;
            return await queryEntries.ToListAsync();
        }
        public static async Task<List<EntryGroup>> getGroupsAsync(int userId)
        {
            SQLSContext context = new SQLSContext();
            var queryGroups = from entry in context.EntryGroups
                              where entry.userId == userId
                              select entry;
            return await queryGroups.ToListAsync();
        }
    }
}
