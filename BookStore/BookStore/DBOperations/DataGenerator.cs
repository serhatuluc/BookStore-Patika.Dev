using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            //Entity çalışması
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }

                context.Books.AddRange(new Book
                {
                 
                    Title = "Lean Startup",
                    GenreId = 1,
                    PageCount = 525,
                    PublishDate = new DateTime(2001, 06, 12)
                },

                new Book
                {
                    
                    Title = "Herland",
                    GenreId = 2,
                    PageCount = 485,
                    PublishDate = new DateTime(2011, 08, 15)
                },
                new Book
                {
                  
                    Title = "Dune",
                    GenreId = 3,
                    PageCount = 245,
                    PublishDate = new DateTime(2021, 09, 02)
                });

                context.SaveChanges();
            }
        }
    }
}
