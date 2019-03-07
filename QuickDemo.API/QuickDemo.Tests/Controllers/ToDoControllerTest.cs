using System.Linq;
using System.Web.Http.Results;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickDemo.API.Controllers;
using QuickDemo.API.Models;

namespace QuickDemo.Tests.Controllers
{
    [TestClass]
    public class ToDoControllerTest
    {
        ToDoController controller;

        [TestInitialize]
        public void Initialize()
        {
            controller = new ToDoController();
        }

        [TestMethod]
        public void GetAllEmptyList_Ok()
        {
            controller.DeleteAll();
            var actionResult = controller.GetAll();
            var response = actionResult as OkNegotiatedContentResult<List<ToDoItem>>;
            Assert.IsNotNull(response);
            var items = response.Content;
            Assert.AreEqual(0, items.Count());
        }

        [TestMethod]
        public void GetAllNotEmptyList_Ok()
        {
            var newItem = new ToDoItem
            {
                Id = 1,
                Description = "Unit test item",
                Priority = 1,
                IsDone = false
            };
            controller.Create(newItem);

            var actionResult = controller.GetAll();
            var response = actionResult as OkNegotiatedContentResult<List<ToDoItem>>;
            Assert.IsNotNull(response);
            var items = response.Content;
            Assert.AreNotEqual(0, items.Count());
        }

        [TestMethod]
        public void GetById_Ok()
        {
            var newItem = new ToDoItem
            {
                Id = 2,
                Description = "Unit test item",
                Priority = 1,
                IsDone = false
            };

            var actionResult = controller.Create(newItem);
            var responseCreated = actionResult as CreatedNegotiatedContentResult<ToDoItem>;
            Assert.IsNotNull(responseCreated);
            var created = responseCreated.Content;
            Assert.AreEqual(created, newItem);

            actionResult = controller.GetById(2);
            var response = actionResult as OkNegotiatedContentResult<ToDoItem>;
            Assert.IsNotNull(response);
            var item = response.Content;
            Assert.IsNotNull(item);
            Assert.AreEqual(2, item.Id);
        }

        [TestMethod]
        public void GetById_NotFound()
        {
            var actionResult = controller.GetById(3);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Create_Ok()
        {
            var newItem = new ToDoItem
            {
                Id = 4,
                Description = "Unit test item",
                Priority = 1,
                IsDone = false
            };

            var actionResult = controller.Create(newItem);
            var response = actionResult as CreatedNegotiatedContentResult<ToDoItem>;
            Assert.IsNotNull(response);
            var created = response.Content;
            Assert.AreEqual(created, newItem);
        }

        [TestMethod]
        public void Create_BadRequest()
        {
            ToDoItem item = null;
            var actionResult = controller.Create(item);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void Create_Conflict()
        {
            var firstItem = new ToDoItem
            {
                Id = 5,
                Description = "Unit test item",
                Priority = 1,
                IsDone = false
            };
            var secondItem = new ToDoItem
            {
                Id = 5,
                Description = "Unit test item",
                Priority = 1,
                IsDone = false
            };

            var actionResult = controller.Create(firstItem);
            var response = actionResult as CreatedNegotiatedContentResult<ToDoItem>;
            Assert.IsNotNull(response);
            var created = response.Content;
            Assert.AreEqual(created, firstItem);

            actionResult = controller.Create(secondItem);
            Assert.IsInstanceOfType(actionResult, typeof(ConflictResult));
        }

        [TestMethod]
        public void Update_Ok()
        {
            var newItem = new ToDoItem
            {
                Id = 6,
                Description = "Unit test item",
                Priority = 1,
                IsDone = false
            };

            var actionResult = controller.Create(newItem);
            var response = actionResult as CreatedNegotiatedContentResult<ToDoItem>;
            Assert.IsNotNull(response);
            var created = response.Content;
            Assert.AreEqual(created, newItem);

            newItem.Description = "Update test";
            newItem.Priority = 2;
            newItem.IsDone = true;

            actionResult = controller.Update(6, newItem);
            var responseUpdate = actionResult as OkNegotiatedContentResult<ToDoItem>;
            Assert.IsNotNull(responseUpdate);
            var updated = response.Content;
            Assert.AreEqual(updated, newItem);
        }

        [TestMethod]
        public void Update_NotFound()
        {
            var item = new ToDoItem();
            var actionResult = controller.Update(7, item);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Update_BadRequest()
        {
            ToDoItem item = null;
            var actionResult = controller.Update(8, item);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void Delete_Ok()
        {
            var newItem = new ToDoItem
            {
                Id = 9,
                Description = "Unit test item",
                Priority = 1,
                IsDone = false
            };

            var actionResult = controller.Create(newItem);
            var response = actionResult as CreatedNegotiatedContentResult<ToDoItem>;
            Assert.IsNotNull(response);
            var created = response.Content;
            Assert.AreEqual(created, newItem);

            actionResult = controller.Delete(9);
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public void Delete_Notfound()
        {
            var actionResult = controller.Delete(10);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteAll_Ok()
        {
            var actionResult = controller.DeleteAll();
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestCleanup]
        public void Cleanup()
        {
            controller = null;
        }
    }
}
