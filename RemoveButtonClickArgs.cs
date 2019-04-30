using System;

namespace ToDoApp
{
    public class RemoveButtonClickArgs : EventArgs
    {
        public IAppPage Page { get; set; }
    }

    public delegate void RemoveButtonClickHandler(object sender, RemoveButtonClickArgs e);
}
