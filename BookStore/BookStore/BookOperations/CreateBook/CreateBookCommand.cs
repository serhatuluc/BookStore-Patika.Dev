using AutoMapper;
using BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        private readonly IMapper _mapper;
        public CreateBookModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (book is not null)
            {
                throw new InvalidOperationException("Kitap zaten mevcut");
            }
            //Automapper çalışması
            book = _mapper.Map<Book>(Model);//new Book();
            //book.Title = Model.Title;
            //book.PublishDate = Model.PublishDate;
            //book.PageCount = Model.PageCount;
            //book.GenreId = Model.GenreId;
            _dbContext.Add(book);
            _dbContext.SaveChanges();
        }

        public class CreateBookModel
        {
            public string Title { get; set; }

            public int GenreId { get; set; }

            public int PageCount { get; set; }

            public DateTime PublishDate { get; set; }

        }
    }
}
