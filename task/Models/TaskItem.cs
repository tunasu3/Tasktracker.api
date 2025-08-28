using System;

namespace TaskTracker.Models
{
    public class TaskItem
    {
        public int Id { get; set; } =0;
        public string Title { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public string AssignedTo { get; set; } = string.Empty;
    }
}
