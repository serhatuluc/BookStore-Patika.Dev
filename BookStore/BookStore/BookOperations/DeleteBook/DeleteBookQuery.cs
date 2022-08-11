using BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.BookOperations.DeleteBook
{
    public class DeleteBookQuery
    {
        public int BookId { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public DeleteBookQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.id == BookId);
            if (book is null)
            {
                throw new InvalidOperationException("Kitap bulunamadı");
            }
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
            
        }
    }
}
