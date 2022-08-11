using BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BookStore.BookOperations.CreateBook.CreateBookCommand;

namespace BookStore.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        public CreateBookModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        public int BookId;
        public GetBookDetailQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Where(book => book.id == BookId).SingleOrDefault();
            if(book is null)
            {
                throw new InvalidOperationException("Kitap bulunamadı");
            }
            BookDetailViewModel vm = new BookDetailViewModel();
            vm.Title = book.Title;
            vm.PageCount = book.PageCount;
            vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yy");
            return vm;
        }

        public class BookDetailViewModel
        {
            public string Title { get; set; }
            public string Genre { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
        }
    }
}
