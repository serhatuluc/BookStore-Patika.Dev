using AutoMapper;
using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.DeleteBook;
using BookStore.BookOperations.GetBookDetail;
using BookStore.BookOperations.GetBooks;
using BookStore.BookOperations.UpdateBook;
using BookStore.DBOperations;
using FluentValidation;
using FluentValidation.Results;
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
        private readonly IMapper _mapper;

        //Entity çalışması
        readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

       

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result);

        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
            query.BookId = id;
            BookDetailViewModel result;
            try
            {
                query.BookId = id;
                GetBookDeatilQueryValidator validator = new GetBookDeatilQueryValidator();
                validator.ValidateAndThrow(query);
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
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            try
            {
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
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
                UpdateBookQueryValidator validator = new UpdateBookQueryValidator();
                validator.ValidateAndThrow(query);
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
                DeleteBookQueryValidator validator = new DeleteBookQueryValidator();
                validator.ValidateAndThrow(query);
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
