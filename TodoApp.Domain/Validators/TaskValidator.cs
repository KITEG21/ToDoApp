using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using TodoApp.Domain.DTOs;
namespace TodoApp.Domain.Validators;

public class TaskValidator : AbstractValidator<TaskDto>
{
    public TaskValidator()
    {
        RuleFor(x => x.Title)
         .NotEmpty().WithMessage("Title cannot be empty")
         .MinimumLength(3).WithMessage("Title cannot have less than 3 characters")
         .MaximumLength(50).WithMessage("Title cannot have more than 50 characters");

        RuleFor(x => x.Content)
         .NotEmpty().WithMessage("There must be any content")
         .MaximumLength(250).WithMessage("The content must not be over 250 characters");

        RuleFor(x => x.DayToDo)
         .NotEmpty().WithMessage("There must be a date to accomplish"); 
        

    }
}
