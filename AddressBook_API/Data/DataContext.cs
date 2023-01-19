using AddressBook_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AddressBook_API.Data
{
   
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }

    }
    
}
