using System;

namespace ToDoApp
{
    public class MarkAsCompleteClickArgs : EventArgs
    {
        public TaskPage Page { get; set; }
    }

    public delegate void MarkAsCompleteClickHandler(object sender, MarkAsCompleteClickArgs e);
}
