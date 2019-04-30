using System;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Collections.Generic;

namespace ToDoApp
{
    public class Manager
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<Task> TaskList { get; private set; }
        private Frame mainFrame;
        private MainPage mainPage;
        public Manager(ref Frame frame)
        {
            mainFrame = frame;
            mainPage = new MainPage();
            TaskList = new List<Task>();

            mainPage.NewTaskButton.Click += NewTaskButtonClick;
            mainPage.ShowCompletedTasksButton.Click += ShowCompletedTasksClick;
            mainPage.ShowIncompleteTasksButton.Click += ShowIncompleteTasksClick;
            mainPage.ShowAllButton.Click += ShowAllTaskClick;
            mainPage.SaveButton.Click += SaveButtonClick;
            mainPage.LoadButton.Click += LoadButtonClick;

            mainFrame.Content = mainPage;

            ((App)Application.Current).AppExit += AppExitHandler;

            Load();
        }
        private Manager()
        {
            
        }

        public void AddTask(string taskName, string taskContent)
        {
            AddTask(taskName, taskContent, DateTime.Today.ToShortDateString());
        }

        public void AddTask(string taskName, string taskContent, string taskDate)
        {
            CreateTask(taskName, taskContent, taskDate);

            mainPage.AddTask(TaskList[TaskList.Count - 1]);

            TaskList[TaskList.Count - 1].DoubleClick += LabelDoubleClick;
            TaskList[TaskList.Count - 1].TaskPage.BackButton.Click += BackButtonClick;
            TaskList[TaskList.Count - 1].TaskPage.RemoveButtonClick += RemoveButtonClick;
            TaskList[TaskList.Count - 1].TaskPage.MarkAsCompleteButtonClick += MarkAsCompleteButtonClick;
            TaskList[TaskList.Count - 1].TaskPage.MarkAsIncompleteButtonClick += MarkAsIncompleteButtonClick;
        }

        private void CreateTask()
        {
            CreateTask(string.Empty, string.Empty, string.Empty);
        }

        private void CreateTask(string taskName, string taskContent)
        {
            CreateTask(taskName, taskContent, string.Empty);
        }

        private void CreateTask(string taskName, string taskContent, string taskDate)
        {
            TaskList.Add(new Task(taskName, taskContent, taskDate));
        }

        public void RemoveTask(Task task)
        {
            TaskList.Remove(task);
            mainPage.RemoveTask(task);
        }

        public void UpdateTaskName(Task task, string newName)
        {
            int taskIndex;
            taskIndex = TaskList.IndexOf(task);
            (TaskList[taskIndex]).UpdateTaskName(newName);
        }

        public void UpdateTaskContent(Task task, string newContent)
        {
            int taskIndex;
            taskIndex = TaskList.IndexOf(task);
            (TaskList[taskIndex]).UpdateTaskContent(newContent);
        }

        private void ChangePage(IAppPage page)
        {
            mainFrame.Content = page;
        }

        public void LabelDoubleClick(object sender, LabelDoubleClickArgs e)
        {
            ChangePage(e.Page);
        }

        public void BackButtonClick(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = mainPage;
        }

        public void NewTaskButtonClick(object sender, RoutedEventArgs e)
        {
            AddTask(string.Empty, string.Empty);
            ChangePage(TaskList[TaskList.Count - 1].TaskPage);
        }

        public void RemoveButtonClick(object sender, RemoveButtonClickArgs e)
        {
            Task task;
            task = TaskList.Find(x => x.TaskPage.Equals(e.Page));
            mainPage.RemoveTask(task);
            TaskList.Remove(task);

            ChangePage(mainPage);
        }

        public void MarkAsCompleteButtonClick(object sender, MarkAsCompleteClickArgs e)
        {
            Task task;
            task = TaskList.Find(x => x.TaskPage.Equals(e.Page));
            task.MarkTaskAsComplete();
            ChangePage(mainPage);
        }

        public void MarkAsIncompleteButtonClick(object sender, MarkAsIncompleteClickArgs e)
        {
            Task task;
            task = TaskList.Find(x => x.TaskPage.Equals(e.Page));
            task.MarkTaskAsIncomplete();
            ChangePage(mainPage);
        }

        public void ShowCompletedTasksClick(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < TaskList.Count; i++)
            {
                mainPage.RemoveTask(TaskList[i]);
                if (TaskList[i].TaskComplete)
                {
                    mainPage.AddTask(TaskList[i]);
                }
            }
        }

        public void ShowIncompleteTasksClick(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < TaskList.Count; i++)
            {
                mainPage.RemoveTask(TaskList[i]);
                if (!TaskList[i].TaskComplete)
                {
                    mainPage.AddTask(TaskList[i]);
                }
            }
        }

        public void ShowAllTaskClick(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < TaskList.Count; i++)
            {
                mainPage.RemoveTask(TaskList[i]);
            }
            for (int i = 0; i < TaskList.Count; i++)
            {
                mainPage.AddTask(TaskList[i]);
            }
        }

        public void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            this.Save();
        }

        public void LoadButtonClick(object sender, RoutedEventArgs e)
        {
            this.Load();
        }

        public void Save()
        {
            TasksSaver.Save(this.TaskList);
        }

        public void Load()
        {
            List<string[]> tempList = new List<string[]>();
            tempList = TasksLoader.Load();

            ClearList();

            for (int i = 0; i < tempList.Count; i++)
            {
                AddTask((tempList[i])[0], (tempList[i])[1], (tempList[i])[2]);

                if(((tempList[i])[3]) == "True")
                {
                    TaskList[i].MarkTaskAsComplete();
                }
            }
        }

        private void ClearList()
        {
            for(int i = 0; i < TaskList.Count; i++)
            {
                mainPage.RemoveTask(TaskList[i]);
            }
            TaskList.Clear();
        }

        private void AppExitHandler(object sender, ExitEventArgs e)
        {
            Save();
        }
    }
}
