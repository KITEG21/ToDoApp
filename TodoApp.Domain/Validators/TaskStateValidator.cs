using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using TodoApp.Domain.DTOs;
using TodoApp.Domain.Models;

namespace TodoApp.Domain.Validators
{
    public class TaskStateValidator: AbstractValidator<TaskStateDto>
    {
        public TaskStateValidator()
        {
            RuleFor(x => x.TaskState)
            .IsInEnum().WithMessage("Invalid task state");
        }

    }
}