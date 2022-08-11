using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.DeleteBook;
using BookStore.BookOperations.GetBookDetail;
using BookStore.BookOperations.GetBooks;
using BookStore.BookOperations.UpdateBook;
using BookStore.DBOperations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BookStore.BookOperations.CreateBook.CreateBookCommand;
using static BookStore.BookOperations.GetBookDetail.GetBookDetailQuery;
using static BookStore.BookOperations.UpdateBook.UpdateBookQuery;

namespace BookStore.Controllers
{
    [Route("/[controller]s")]
    [ApiController]
    public class BookController : ControllerBase
    {
        //Entity çalışması
        readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

       

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);

        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context);
            query.BookId = id;
            BookDetailViewModel result;
            try
            {
                query.BookId = id;
                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }


        [HttpPost]
        public IActionResult AddBook(CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateBook(UpdateBookModel updatedBook)
        {
            UpdateBookQuery query = new UpdateBookQuery(_context);
            try
            {
                query.Model = updatedBook;
                query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return Ok();
            
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookQuery query = new DeleteBookQuery(_context);
            try
            {
                query.BookId = id;
                query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
       
            
        }


    }
}
