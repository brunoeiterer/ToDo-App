using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;

namespace ToDoApp
{
    [Serializable]
    public class Task
    {
        public Label TaskLabel { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TaskPage TaskPage { get; set; }
        private string TaskContent { get; set; }
        public Guid TaskId { get; private set; }
        public bool TaskComplete { get; set; }
        public Label TaskDateLabel { get; private set; }

        public Task() : this(string.Empty, string.Empty, DateTime.Today.ToShortDateString())
        {

        }

        public Task(string taskName, string taskContent) : this(taskName, taskContent, DateTime.Today.ToShortDateString())
        {

        }

        public Task(string taskName, string taskContent, string taskDate)
        {
            TaskLabel = new Label();
            DockPanel.SetDock(TaskLabel, Dock.Top);
            SetLabelStyle(Brushes.Black, new Thickness(1), Brushes.White);
            TaskLabel.MouseDown += new MouseButtonEventHandler(MouseButtonDownHandler);

            TaskPage = new TaskPage();
            TaskPage.TitleTextBox.TextChanged += TextChangedHandler;
            TaskPage.ContentTextBox.TextChanged += TextChangedHandler;

            TaskId = new Guid();

            TaskDateLabel = new Label();

            UpdateTaskName(taskName);
            UpdateTaskContent(taskContent);
            TaskDateLabel.Content = taskDate;
        }

        public void UpdateTaskName(string value)
        {
            TaskLabel.Content = value;
            TaskPage.UpdateTitle(TaskLabel.Content.ToString());
        }

        public string GetTaskName()
        {
            return TaskLabel.Content.ToString();
        }

        public void UpdateTaskContent(string value)
        {
            TaskContent = value;
            UpdateToolTip(GetTaskContent());
            TaskPage.UpdateContent(GetTaskContent());
        }

        public string GetTaskContent()
        {
            return TaskContent;
        }

        public string GetTaskDate()
        {
            return TaskDateLabel.Content.ToString();
        }

        private void MouseButtonDownHandler(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2)
            {
                LabelDoubleClickArgs eventArgs = new LabelDoubleClickArgs();
                eventArgs.Page = TaskPage;

                DoubleClick?.Invoke(sender, eventArgs);
            }
        }

        private void UpdateToolTip(string content)
        {
            ToolTip newTooltip = new ToolTip();

            newTooltip.Content = GetTaskContent();
            TaskLabel.ToolTip = newTooltip;
        }

        private void SetLabelStyle(Brush borderBrush, Thickness borderThickness, Brush backgroundColor)
        {
            TaskLabel.BorderBrush = borderBrush;
            TaskLabel.BorderThickness = borderThickness;
            TaskLabel.Background = backgroundColor;
        }

        private void TextChangedHandler(object s, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)s;

            if(textBox.Name.Substring(0, 5) == "title")
            {
                UpdateTaskName(textBox.Text); 
            }

            if (textBox.Name.Substring(0, 7) == "content")
            {
                UpdateTaskContent(textBox.Text);
            }
        }

        public void MarkTaskAsComplete()
        {
            SetLabelStyle(Brushes.Black, new Thickness(1), Brushes.Lime);
            TaskComplete = true;
        }

        public void MarkTaskAsIncomplete()
        {
            SetLabelStyle(Brushes.Black, new Thickness(1), Brushes.White);
            TaskComplete = false;
        }

        public event LabelDoubleClickHandler DoubleClick;
    }
}
