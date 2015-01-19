namespace DemoApi.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using DemoApi.Models;

    public class TasksController : ApiController
    {
        private static readonly List<TodoTask> Tasks = new List<TodoTask>();

        public IEnumerable<TodoTask> GetAll()
        {
            return Tasks;
        }

        public TodoTask GetTask(int id)
        {
            return Tasks.SingleOrDefault(task => task.Id == id);
        }

        public int AddTask(string text)
        {
            int newId = Tasks.Any() 
                ? Tasks.Select(x => x.Id).Max() + 1 
                : 1;

            Tasks.Add(new TodoTask
            {
                Id = newId,
                Text = text
            });

            return newId;
        }

        public void DeleteTask(int id)
        {
            TodoTask task = Tasks.Find(x => x.Id == id);
            Tasks.Remove(task);
        }
    }
}