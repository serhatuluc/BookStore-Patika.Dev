using BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.BookOperations.UpdateBook
{
    public class UpdateBookQuery
    {
        public UpdateBookModel Model;
        private readonly BookStoreDbContext _dbContext;

        public UpdateBookQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.id == Model.id);
            if (book is null)
            {
                throw new InvalidOperationException("Kitap bulunamadı");
            }

            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            _dbContext.SaveChanges();
        }

        public class UpdateBookModel
        {
            public int id { get; set; }
            public string Title { get; set; }
            public DateTime PublishDate { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
        }
    }
}
