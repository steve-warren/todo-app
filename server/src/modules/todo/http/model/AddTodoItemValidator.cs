using FluentValidation;

namespace WarrenSoftware.TodoApp.Modules.Todo.Http
{
    public class AddTodoItemValidator : AbstractValidator<AddTodoItemViewModel>
    {
        public AddTodoItemValidator()
        {
            RuleFor(model => model.Name).NotEmpty();
            RuleFor(model => model.Notes).NotEmpty();
            RuleFor(model => model.Priority).NotEmpty();
        }
    }
}