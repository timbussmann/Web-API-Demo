﻿namespace DemoApi.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
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
            return Tasks.SingleOrDefault(task => task.Id == taskId);
        }

        [Route("")]
        [HttpPost]
        public int AddTask([FromBody]string text)
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

        [Route("{taskId:int}")]
        [HttpDelete]
        public void RemoveTask(int taskId)
        {
            TodoTask task = Tasks.Find(x => x.Id == taskId);
            Tasks.Remove(task);
        }
    }
}