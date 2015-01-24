namespace DemoApi.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Http;
    using DemoApi.Filters;
    using DemoApi.Models;

    [RoutePrefix("api/tasks")]
    public class TasksController : ApiController
    {
        private readonly ITaskList tasks;

        public TasksController(ITaskList tasks)
        {
            this.tasks = tasks;
        }

        [Route("")]
        [HttpGet]
        public IEnumerable<TodoTask> GetAll()
        {
            return this.tasks;
        }

        // note the int constraint:
        [Route("{taskId:int}", Name = "GetTaskById")]
        [HttpGet]
        public TodoTask GetTask(int taskId)
        {
            TodoTask task = this.tasks.SingleOrDefault(t => t.Id == taskId);
            if (task == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return task;
        }

        [Route("")]
        [HttpPost]
        [NotNull]
        [ValidateModel]
        public IHttpActionResult AddTask(TodoTask newTask)
        {
            int newId = this.tasks.Any() 
                ? this.tasks.Select(x => x.Id).Max() + 1 
                : 1;

            newTask.Id = newId;

            this.tasks.Add(newTask);

            string resourceLocation = this.Url.Link("GetTaskById", new { taskId = newId });
            return this.Created(resourceLocation, newTask);
        }

        [Route("{taskId:int}")]
        [HttpDelete]
        public IHttpActionResult RemoveTask(int taskId)
        {
            TodoTask task = this.tasks.SingleOrDefault(x => x.Id == taskId);

            if (task == null)
            {
                return this.NotFound();
            }

            this.tasks.Remove(task);
            return this.StatusCode(HttpStatusCode.NoContent);
        }
    }
}