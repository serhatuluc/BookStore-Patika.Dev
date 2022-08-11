using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DBOperations
{
    public class BookStoreDbContext:DbContext
    {
        //Entity çalışması
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }


    }
}
