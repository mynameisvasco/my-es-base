using MyEsBase.Domain.Common.Models;

namespace MyEsBase.Domain.Todos;

public record CreatedTodo(string Title, string Body) : Event;

public record ChangedTodoTitle(string Title) : Event;

public record ChangedTodoBody(string Body) : Event;