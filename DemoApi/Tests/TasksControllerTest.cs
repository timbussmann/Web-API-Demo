﻿namespace DemoApi.Tests
{
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.Results;
    using System.Web.Http.Routing;
    using DemoApi.Controllers;
    using DemoApi.Models;
    using FakeItEasy;
    using FluentAssertions;
    using Xunit;

    public class TasksControllerTest
    {
        private readonly UrlHelper urlHelper;

        private readonly TasksController testee;

        public TasksControllerTest()
        {
            this.testee = new TasksController();

            // for simplicity we access our tasks directly from test code. Of course
            // a real controller wouldn't manage it's data by itself.
            TasksController.Tasks.Clear();

            this.testee.Url = this.urlHelper = A.Fake<UrlHelper>();
        }

        [Fact]
        public void GetAll_WhenNoTasksInList_ShouldReturnEmptyList()
        {
            IEnumerable<TodoTask> result = this.testee.GetAll();

            result.Should().BeEmpty();
        }

        [Fact]
        public void GetAll_WhenTasksInList_ShouldReturnAllTasksInList()
        {
            var tasks = new[] { new TodoTask(), new TodoTask(), new TodoTask(), };
            TasksController.Tasks.AddRange(tasks);

            IEnumerable<TodoTask> result = this.testee.GetAll();

            result.Should().Equal(tasks);
        }

        [Fact]
        public void GetTask_WhenTaskNotFound_ShouldReturnNotFoundResponse()
        {
            this.testee.Invoking(x => x.GetTask(-42))
                .ShouldThrow<HttpResponseException>()
                .And.Response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public void GetTask_WhenTaskFound_ShouldReturnTask()
        {
            TodoTask expectedTask = new TodoTask { Id = 42, Text = "a task to do" };
            TasksController.Tasks.Add(expectedTask);

            TodoTask result = this.testee.GetTask(expectedTask.Id);

            result.Should().Be(expectedTask);
        }

        [Fact]
        public void RemoveTask_WhenTaskExists_ShouldReturnNoContentResponse()
        {
            var task = new TodoTask();
            TasksController.Tasks.Add(task);

            IHttpActionResult result = this.testee.RemoveTask(task.Id);

            result.Should().BeOfType<StatusCodeResult>()
                .Which.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public void RemoveTask_WhenTaskExists_ShouldRemoveTaskFromTasks()
        {
            var task = new TodoTask();
            TasksController.Tasks.Add(task);

            IHttpActionResult result = this.testee.RemoveTask(task.Id);

            TasksController.Tasks.Should().NotContain(task);
        }

        [Fact]
        public void RemoveTask_WhenTaskDoesNotExist_ShouldReturnNotFoundResult()
        {
            IHttpActionResult result = this.testee.RemoveTask(33);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void AddTask_ShouldReturnCreatedResponse()
        {
            const string TaskText = "a new task";
            const string LinkUrl = "http://some.whe.ru/there";
            A.CallTo(() => this.urlHelper.Link("GetTaskById", A<object>._)).Returns(LinkUrl);

            IHttpActionResult result = this.testee.AddTask(TaskText);

            var response = result.Should().BeOfType<CreatedNegotiatedContentResult<TodoTask>>().Which;
            response.Content.Text.Should().Be(TaskText);
            response.Location.Should().Be(LinkUrl);
        }

        [Fact]
        public void AddTask_ShouldAddTaskToTasks()
        {
            const string TaskText = "a new task";

            IHttpActionResult result = this.testee.AddTask(TaskText);

            TasksController.Tasks.Should().ContainSingle(x => x.Text == TaskText);
        }
    }
}