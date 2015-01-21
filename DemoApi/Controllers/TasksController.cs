namespace DemoApi.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Http;
    using DemoApi.Models;

    [RoutePrefix("api/tasks")]
    public class TasksController : ApiController
    {
        private static readonly List<TodoTask> Tasks = new List<TodoTask>();

        [Route("")]
        [HttpGet]
        public IEnumerable<TodoTask> GetAll()
        {
            return Tasks;
        }

        // note the int constraint:
        [Route("{taskId:int}")]
        [HttpGet]
        public TodoTask GetTask(int taskId)
        {
            TodoTask task = Tasks.SingleOrDefault(t => t.Id == taskId);
            if (task == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return task;
        }

        [Route("")]
        [HttpPost]
        public TodoTask AddTask([FromBody]string text)
        {
            int newId = Tasks.Any() 
                ? Tasks.Select(x => x.Id).Max() + 1 
                : 1;

            var newTask = new TodoTask
            {
                Id = newId,
                Text = text
            };
            Tasks.Add(newTask);

            return newTask;
        }

        [Route("{taskId:int}")]
        [HttpDelete]
        public void RemoveTask(int taskId)
        {
            TodoTask task = Tasks.Find(x => x.Id == taskId);
            Tasks.Remove(task);
        }
    }
}