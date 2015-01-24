namespace DemoApi.Controllers
{
    using System.Collections.Generic;
    using DemoApi.Models;

    public interface ITaskList : IList<TodoTask>
    {
    }

    public class TaskList : List<TodoTask>, ITaskList
    {
    }
}