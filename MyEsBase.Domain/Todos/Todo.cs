using MyEsBase.Domain.Common.Models;

namespace MyEsBase.Domain.Todos;

public class Todo : AggregateRoot
{
    public string? Title { get; set; }
    public string? Body { get; set; }

    public void Create(string title, string body)
    {
        ApplyAndAdd(new CreatedTodo(title, body));
    }

    public void ChangeTitle(string title)
    {
        ApplyAndAdd(new ChangedTodoTitle(title));
    }

    public void ChangeBody(string body)
    {
        ApplyAndAdd(new ChangedTodoBody(body));
    }

    public void Apply(ChangedTodoTitle ev)
    {
        Title = ev.Title;
    }

    public void Apply(ChangedTodoBody ev)
    {
        Body = ev.Body;
    }

    public void Apply(CreatedTodo ev)
    {
        Title = ev.Title;
        Body = ev.Body;
    }
}