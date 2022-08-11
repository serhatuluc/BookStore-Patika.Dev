using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.BookOperations.DeleteBook
{
    public class DeleteBookQueryValidator:AbstractValidator<DeleteBookQuery>
    {
        public DeleteBookQueryValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
        }
    }
}
