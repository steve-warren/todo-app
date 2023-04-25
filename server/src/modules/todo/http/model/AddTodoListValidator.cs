using FluentValidation;

namespace WarrenSoftware.TodoApp.Modules.Todo.Http;

public class AddTodoListValidator : AbstractValidator<AddTodoListViewModel>
{
    public AddTodoListValidator()
    {
        RuleFor(model => model.Name).NotEmpty();
    }
}
