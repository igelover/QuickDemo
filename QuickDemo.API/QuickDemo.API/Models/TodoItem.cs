namespace QuickDemo.API.Models
{
    /// <summary>
    /// An task element on a ToDo list
    /// </summary>
    public class ToDoItem
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Task description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Task priority
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Shows whether the task has been done or not
        /// </summary>
        public bool IsDone { get; set; }
    }
}