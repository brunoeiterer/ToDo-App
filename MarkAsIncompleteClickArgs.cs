using System;

namespace ToDoApp
{
    public class MarkAsIncompleteClickArgs : EventArgs
    {
        public TaskPage Page { get; set; }
    }

    public delegate void MarkAsIncompleteClickHandler(object sender, MarkAsIncompleteClickArgs e);
}
