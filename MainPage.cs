using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using HexInnovation;

namespace ToDoApp
{
    [Serializable]
    class MainPage : Page, IAppPage
    {
        private readonly DockPanel basePanel;
        private readonly StackPanel menuPanel;
        private readonly DockPanel childPanel1;
        private readonly DockPanel childPanel2;
        private readonly DockPanel childPanel3;
        private readonly DockPanel childPanel4;
        private readonly MathConverter converter;

        public Button NewTaskButton { get; private set; }
        public Button ShowCompletedTasksButton { get; private set; }
        public Button ShowIncompleteTasksButton { get; private set; }
        public Button ShowAllButton { get; private set; }
        public Button SaveButton { get; private set; }
        public Button LoadButton { get; private set; }

        private const int maxTasksPerChildPanel = 27;

        public MainPage()
        {
            Binding basePanelHeightBinding = new Binding();
            basePanelHeightBinding.Source = Application.Current.MainWindow;
            basePanelHeightBinding.Path = new PropertyPath(Window.ActualHeightProperty);

            Binding basePanelWidthBinding = new Binding();
            basePanelWidthBinding.Source = Application.Current.MainWindow;
            basePanelWidthBinding.Path = new PropertyPath(Window.ActualWidthProperty);

            basePanel = new DockPanel();
            basePanel.HorizontalAlignment = HorizontalAlignment.Left;
            basePanel.VerticalAlignment = VerticalAlignment.Top;
            basePanel.SetBinding(DockPanel.HeightProperty, basePanelHeightBinding);
            basePanel.SetBinding(DockPanel.WidthProperty, basePanelWidthBinding);

            NewTaskButton = new Button();
            NewTaskButton.HorizontalAlignment = HorizontalAlignment.Center;
            NewTaskButton.VerticalAlignment = VerticalAlignment.Center;
            NewTaskButton.Content = "+";
            NewTaskButton.ToolTip = "Add a task";

            ShowCompletedTasksButton = new Button();
            ShowCompletedTasksButton.HorizontalAlignment = HorizontalAlignment.Center;
            ShowCompletedTasksButton.VerticalAlignment = VerticalAlignment.Center;
            ShowCompletedTasksButton.Content = "✓✓";
            ShowCompletedTasksButton.FontSize = SystemFonts.MessageFontSize / 2;
            ShowCompletedTasksButton.ToolTip = "Show completed tasks";
            ShowCompletedTasksButton.Height = SystemFonts.MessageFontSize * 1.6;

            ShowIncompleteTasksButton = new Button();
            ShowIncompleteTasksButton.HorizontalAlignment = HorizontalAlignment.Center;
            ShowIncompleteTasksButton.VerticalAlignment = VerticalAlignment.Center;
            ShowIncompleteTasksButton.Content = "!!";
            ShowIncompleteTasksButton.FontSize = SystemFonts.MessageFontSize / 1.5;
            ShowIncompleteTasksButton.ToolTip = "Show incomplete tasks";
            ShowIncompleteTasksButton.Height = SystemFonts.MessageFontSize * 1.6;

            ShowAllButton = new Button();
            ShowAllButton.HorizontalAlignment = HorizontalAlignment.Center;
            ShowAllButton.VerticalAlignment = VerticalAlignment.Center;
            ShowAllButton.Content = "✓!";
            ShowAllButton.FontSize = SystemFonts.MessageFontSize / 1.5;
            ShowAllButton.ToolTip = "Show all tasks";
            ShowAllButton.Height = SystemFonts.MessageFontSize * 1.6;

            SaveButton = new Button();
            SaveButton.HorizontalAlignment = HorizontalAlignment.Center;
            SaveButton.VerticalAlignment = VerticalAlignment.Center;
            SaveButton.Content = "💾";
            SaveButton.ToolTip = "Save tasks";

            LoadButton = new Button();
            LoadButton.HorizontalAlignment = HorizontalAlignment.Center;
            LoadButton.VerticalAlignment = VerticalAlignment.Center;
            LoadButton.Content = "📂";
            LoadButton.ToolTip = "Load tasks";

            Binding menuPanelWidthBinding = new Binding();
            menuPanelWidthBinding.Source = Application.Current.MainWindow;
            menuPanelWidthBinding.Path = new PropertyPath(Window.ActualWidthProperty);

            menuPanel = new StackPanel();
            menuPanel.HorizontalAlignment = HorizontalAlignment.Left;
            menuPanel.VerticalAlignment = VerticalAlignment.Top;
            menuPanel.Height = SystemFonts.MessageFontSize * 2;
            menuPanel.SetBinding(StackPanel.WidthProperty, menuPanelWidthBinding);
            menuPanel.Orientation = Orientation.Horizontal;
            DockPanel.SetDock(menuPanel, Dock.Top);

            converter = new MathConverter();

            Binding childPanelHeightBinding = new Binding();
            childPanelHeightBinding.Source = basePanel;
            childPanelHeightBinding.Path = new PropertyPath(StackPanel.ActualHeightProperty);
            childPanelHeightBinding.Converter = converter;
            childPanelHeightBinding.ConverterParameter = "x-" + menuPanel.Height.ToString();

            Binding childPanelWidthBinding = new Binding();
            childPanelWidthBinding.Source = basePanel;
            childPanelWidthBinding.Path = new PropertyPath(DockPanel.ActualWidthProperty);
            childPanelWidthBinding.Converter = converter;
            childPanelWidthBinding.ConverterParameter = "x/4";

            childPanel1 = new DockPanel();
            childPanel1.HorizontalAlignment = HorizontalAlignment.Left;
            childPanel1.VerticalAlignment = VerticalAlignment.Top;
            childPanel1.LastChildFill = false;
            childPanel1.SetBinding(DockPanel.HeightProperty, childPanelHeightBinding);
            childPanel1.SetBinding(DockPanel.WidthProperty, childPanelWidthBinding);
            childPanel1.Children.Capacity = maxTasksPerChildPanel;
            DockPanel.SetDock(childPanel1, Dock.Left);

            childPanel2 = new DockPanel();
            childPanel2.HorizontalAlignment = HorizontalAlignment.Left;
            childPanel2.VerticalAlignment = VerticalAlignment.Top;
            childPanel2.LastChildFill = false;
            childPanel2.SetBinding(DockPanel.HeightProperty, childPanelHeightBinding);
            childPanel2.SetBinding(DockPanel.WidthProperty, childPanelWidthBinding);
            childPanel2.Children.Capacity = maxTasksPerChildPanel;
            DockPanel.SetDock(childPanel2, Dock.Left);

            childPanel3 = new DockPanel();
            childPanel3.HorizontalAlignment = HorizontalAlignment.Left;
            childPanel3.VerticalAlignment = VerticalAlignment.Top;
            childPanel3.LastChildFill = false;
            childPanel3.SetBinding(DockPanel.HeightProperty, childPanelHeightBinding);
            childPanel3.SetBinding(DockPanel.WidthProperty, childPanelWidthBinding);
            childPanel3.Children.Capacity = maxTasksPerChildPanel;
            DockPanel.SetDock(childPanel3, Dock.Left);

            childPanel4 = new DockPanel();
            childPanel4.HorizontalAlignment = HorizontalAlignment.Left;
            childPanel4.VerticalAlignment = VerticalAlignment.Top;
            childPanel4.LastChildFill = false;
            childPanel4.SetBinding(DockPanel.HeightProperty, childPanelHeightBinding);
            childPanel4.SetBinding(DockPanel.WidthProperty, childPanelWidthBinding);
            childPanel4.Children.Capacity = maxTasksPerChildPanel;
            DockPanel.SetDock(childPanel4, Dock.Left);

            menuPanel.Children.Add(NewTaskButton);
            menuPanel.Children.Add(ShowCompletedTasksButton);
            menuPanel.Children.Add(ShowIncompleteTasksButton);
            menuPanel.Children.Add(ShowAllButton);
            menuPanel.Children.Add(SaveButton);
            menuPanel.Children.Add(LoadButton);

            basePanel.Children.Add(menuPanel);
            basePanel.Children.Add(childPanel1);
            basePanel.Children.Add(childPanel2);
            basePanel.Children.Add(childPanel3);
            basePanel.Children.Add(childPanel4);

            this.Content = basePanel;
        }

        public void AddTask(Task task)
        {
            if(childPanel1.Children.Count < childPanel1.Children.Capacity)
            {
                childPanel1.Children.Add(task.TaskLabel);
            }
            else if (childPanel2.Children.Count < childPanel1.Children.Capacity)
            {
                childPanel2.Children.Add(task.TaskLabel);
            }
            else if (childPanel3.Children.Count < childPanel1.Children.Capacity)
            {
                childPanel3.Children.Add(task.TaskLabel);
            }
            else
            {
                childPanel4.Children.Add(task.TaskLabel);
            }
        }

        public void RemoveTask(Task task)
        {
            if(childPanel1.Children.Contains(task.TaskLabel))
            {
                childPanel1.Children.Remove(task.TaskLabel);
            }
            else if(childPanel2.Children.Contains(task.TaskLabel))
            {
                childPanel2.Children.Remove(task.TaskLabel);
            }
            else if (childPanel3.Children.Contains(task.TaskLabel))
            {
                childPanel3.Children.Remove(task.TaskLabel);
            }
            else
            {
                childPanel4.Children.Remove(task.TaskLabel);
            }
        }
    }
}
