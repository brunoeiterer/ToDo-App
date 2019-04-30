using System;

namespace ToDoApp
{
    public class LabelDoubleClickArgs : EventArgs
    {
        public IAppPage Page { get; set; }
    }

    public delegate void LabelDoubleClickHandler(object sender, LabelDoubleClickArgs e);
}
