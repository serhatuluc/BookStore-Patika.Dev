using AutoMapper;
using BookStore.Common;
using BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly IMapper _mapper;
        private readonly BookStoreDbContext _dbContext;
        public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public List<BooksViewModel> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(x => x.id).ToList<Book>();  
            List<BooksViewModel> vm = _mapper.Map<List<BooksViewModel>>(bookList);

            //foreach(var book in bookList)
            //{
            //    vm.Add(new BooksViewModel()
            //    {
            //        Title = book.Title,
            //        Genre = ((GenreEnum)book.GenreId).ToString(),
            //        PublishDate = book.PublishDate.Date.ToString("dd/MM/yy"),
            //        PageCount = book.PageCount,
            //    });

            //}
            return vm;
        }

        public class BooksViewModel
        {
            public string Title { get; set; }
            public string Genre { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
        }
    }
}
