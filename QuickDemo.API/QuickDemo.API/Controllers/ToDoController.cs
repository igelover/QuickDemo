using System.Linq;
using System.Web.Http;
using System.Collections.Generic;
using QuickDemo.API.Models;

namespace QuickDemo.API.Controllers
{
    /// <summary>
    /// ToDo list controller
    /// </summary>
    public class ToDoController : ApiController
    {
        private static readonly List<ToDoItem> toDoList = new List<ToDoItem>();

        /// <summary>
        /// Retrieves the whole list of ToDo items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return Ok(toDoList);
        }

        /// <summary>
        /// Retrieves a single element with the given ID
        /// </summary>
        /// <param name="id">Item unique identifier</param>
        /// <returns>A ToDo item with the given ID</returns>
        /// <remarks>If the item is not found, returns a 404 HTTP Code</remarks>
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var item = toDoList.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        /// <summary>
        /// Creates a new ToDo item in the list
        /// </summary>
        /// <param name="item">The item to add</param>
        /// <returns>The item just added</returns>
        /// <remarks>If there is already an item with the same ID, returns a 409 HTTP Code</remarks>
        [HttpPost]
        public IHttpActionResult Create([FromBody]ToDoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            if (item.Id == 0)
            {
                item.Id = toDoList.Count + 1;
            }
            var existing = toDoList.FirstOrDefault(i => i.Id == item.Id);
            if (existing != null)
            {
                return Conflict();
            }
            toDoList.Add(item);
            return Created(nameof(Create), item);
        }

        /// <summary>
        /// Updates an existing ToDo item
        /// </summary>
        /// <param name="id">The item's unique identifier</param>
        /// <param name="item">The item to update</param>
        /// <returns>The item just updated</returns>
        /// <remarks>If the item is not found, returns a 404 HTTP Code</remarks>
        [HttpPut]
        public IHttpActionResult Update(int id, [FromBody]ToDoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var existing = toDoList.FirstOrDefault(i => i.Id == item.Id);
            if (existing == null)
            {
                return NotFound();
            }

            existing.Description = item.Description;
            existing.Priority = item.Priority;
            existing.IsDone = item.IsDone;

            return Ok(existing);
        }

        /// <summary>
        /// Deletes the item with the given ID
        /// </summary>
        /// <param name="id">The item's unique identifier</param>
        /// <returns>A 200 HTTP Code</returns>
        /// <remarks>If the item is not found, returns a 404 HTTP Code</remarks>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existing = toDoList.FirstOrDefault(i => i.Id == id);
            if (existing == null)
            {
                return NotFound();
            }
            toDoList.Remove(existing);
            return Ok();
        }

        /// <summary>
        /// Deletes the whole list
        /// </summary>
        /// <returns>A 200 HTTP Code</returns>
        public IHttpActionResult DeleteAll()
        {
            toDoList.RemoveAll(i => i != null);
            return Ok();
        }
    }
}
