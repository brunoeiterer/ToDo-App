using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using HexInnovation;

namespace ToDoApp
{
    public class TaskPage : Page, IAppPage
    { 
        private readonly DockPanel basePanel;
        private readonly StackPanel menuPanel;
        private readonly StackPanel titleBasePanel;
        private readonly DockPanel titlePanel;
        private readonly DockPanel datePanel;
        private readonly DockPanel contentPanel;
        private readonly MathConverter converter;
        private readonly Button backButton;
        private readonly Button removeButton;
        private readonly Button markAsCompleteButton;
        private readonly Button MarkAsIncompleteButton;
        public TextBox TitleTextBox { get; private set; }
        public TextBox ContentTextBox { get; private set; }
        public Label DateLabel { get; private set; }

        public Button BackButton => backButton;

        public TaskPage()
        {
            Binding basePanelHeightBinding = new Binding();
            basePanelHeightBinding.Source = Application.Current.MainWindow;
            basePanelHeightBinding.Path = new PropertyPath(Window.ActualHeightProperty);

            Binding basePanelWidthinding = new Binding();
            basePanelWidthinding.Source = Application.Current.MainWindow;
            basePanelWidthinding.Path = new PropertyPath(Window.ActualWidthProperty);

            basePanel = new DockPanel();
            basePanel.HorizontalAlignment = HorizontalAlignment.Left;
            basePanel.VerticalAlignment = VerticalAlignment.Top;
            basePanel.SetBinding(DockPanel.HeightProperty, basePanelHeightBinding);
            basePanel.SetBinding(DockPanel.WidthProperty, basePanelWidthinding);

            Binding titleBasePanelWidthBinding = new Binding();
            titleBasePanelWidthBinding.Source = Application.Current.MainWindow;
            titleBasePanelWidthBinding.Path = new PropertyPath(Window.ActualWidthProperty);

            titleBasePanel = new StackPanel();
            titleBasePanel.HorizontalAlignment = HorizontalAlignment.Left;
            titleBasePanel.VerticalAlignment = VerticalAlignment.Top;
            titleBasePanel.Orientation = Orientation.Horizontal;
            titleBasePanel.Height = SystemFonts.MessageFontSize * 2;
            titleBasePanel.SetBinding(DockPanel.WidthProperty, titleBasePanelWidthBinding);
            DockPanel.SetDock(titleBasePanel, Dock.Top);

            converter = new MathConverter();

            Binding menuPanelWidthBinding = new Binding();
            menuPanelWidthBinding.Source = Application.Current.MainWindow;
            menuPanelWidthBinding.Path = new PropertyPath(Window.ActualWidthProperty);

            menuPanel = new StackPanel();
            menuPanel.HorizontalAlignment = HorizontalAlignment.Left;
            menuPanel.VerticalAlignment = VerticalAlignment.Top;
            menuPanel.Orientation = Orientation.Horizontal;
            menuPanel.Height = SystemFonts.MessageFontSize * 2;
            menuPanel.SetBinding(DockPanel.WidthProperty, menuPanelWidthBinding);
            DockPanel.SetDock(menuPanel, Dock.Top);

            backButton = new Button();
            backButton.HorizontalAlignment = HorizontalAlignment.Center;
            backButton.VerticalAlignment = VerticalAlignment.Center;
            backButton.Content = "⇦";
            BackButton.ToolTip = "Go back to the Main Page";

            removeButton = new Button();
            removeButton.HorizontalAlignment = HorizontalAlignment.Center;
            removeButton.VerticalAlignment = VerticalAlignment.Center;
            removeButton.Content = "🗑";
            removeButton.Click += RemoveButtonClickHandler;
            removeButton.ToolTip = "Remove task";

            markAsCompleteButton = new Button();
            markAsCompleteButton.HorizontalAlignment = HorizontalAlignment.Center;
            markAsCompleteButton.VerticalAlignment = VerticalAlignment.Center;
            markAsCompleteButton.Content = "✓";
            markAsCompleteButton.Click += MarkAsCompleteButtonClickHandler;
            markAsCompleteButton.ToolTip = "Mark task as complete";

            MarkAsIncompleteButton = new Button();
            MarkAsIncompleteButton.HorizontalAlignment = HorizontalAlignment.Center;
            MarkAsIncompleteButton.VerticalAlignment = VerticalAlignment.Center;
            MarkAsIncompleteButton.Content = "!";
            MarkAsIncompleteButton.Click += MarkAsIncompleteButtonClickHandler;
            MarkAsIncompleteButton.ToolTip = "Mark task as incomplete";

            Binding titlePanelWidthBinding = new Binding();
            titlePanelWidthBinding.Source = basePanel;
            titlePanelWidthBinding.Path = new PropertyPath(DockPanel.ActualWidthProperty);
            titlePanelWidthBinding.Converter = converter;
            titlePanelWidthBinding.ConverterParameter = "x/2";

            titlePanel = new DockPanel();
            titlePanel.HorizontalAlignment = HorizontalAlignment.Left;
            titlePanel.VerticalAlignment = VerticalAlignment.Top;
            titlePanel.Height = SystemFonts.MessageFontSize * 2;
            titlePanel.SetBinding(DockPanel.WidthProperty, titlePanelWidthBinding);
            DockPanel.SetDock(titlePanel, Dock.Top);

            Binding datePanelWidthBinding = new Binding();
            datePanelWidthBinding.Source = basePanel;
            datePanelWidthBinding.Path = new PropertyPath(DockPanel.ActualWidthProperty);
            datePanelWidthBinding.Converter = converter;
            datePanelWidthBinding.ConverterParameter = "x/2";

            datePanel = new DockPanel();
            datePanel.HorizontalAlignment = HorizontalAlignment.Left;
            datePanel.VerticalAlignment = VerticalAlignment.Top;
            datePanel.Height = SystemFonts.MessageFontSize * 2;
            datePanel.SetBinding(DockPanel.WidthProperty, datePanelWidthBinding);
            DockPanel.SetDock(datePanel, Dock.Left);

            Binding contentPanelHeightBinding = new Binding();
            contentPanelHeightBinding.Source = basePanel;
            contentPanelHeightBinding.Path = new PropertyPath(DockPanel.ActualHeightProperty);
            contentPanelHeightBinding.Converter = converter;
            contentPanelHeightBinding.ConverterParameter = "x-" + titlePanel.Height.ToString();

            Binding contentPanelWidthBinding = new Binding();
            contentPanelWidthBinding.Source = basePanel;
            contentPanelWidthBinding.Path = new PropertyPath(DockPanel.ActualWidthProperty);

            contentPanel = new DockPanel();
            contentPanel.HorizontalAlignment = HorizontalAlignment.Left;
            contentPanel.VerticalAlignment = VerticalAlignment.Top;
            contentPanel.SetBinding(DockPanel.HeightProperty, contentPanelHeightBinding);
            contentPanel.SetBinding(DockPanel.WidthProperty, contentPanelWidthBinding);
            DockPanel.SetDock(contentPanel, Dock.Top);

            Guid titleId = Guid.NewGuid();

            TitleTextBox = new TextBox();
            TitleTextBox.Name = "titleTextBox" + (titleId.ToString()).Replace("-", string.Empty);

            Guid contentId = Guid.NewGuid();

            ContentTextBox = new TextBox();
            ContentTextBox.Name = "contentTextBox" + (contentId.ToString()).Replace("-", string.Empty);

            Guid dateId = Guid.NewGuid();

            Binding dateLabelWidthBinding = new Binding();
            dateLabelWidthBinding.Source = Application.Current.MainWindow;
            dateLabelWidthBinding.Path = new PropertyPath(DockPanel.ActualWidthProperty);
            dateLabelWidthBinding.Converter = converter;
            dateLabelWidthBinding.ConverterParameter = "x/2";

            DateLabel = new Label();
            DateLabel.Name = "dateLabel" + (dateId.ToString()).Replace("-", string.Empty);
            DateLabel.VerticalContentAlignment = VerticalAlignment.Top;
            DateLabel.HorizontalContentAlignment = HorizontalAlignment.Left;
            DateLabel.SetBinding(Label.WidthProperty, dateLabelWidthBinding);
            DateLabel.Content = "Create on: " + DateTime.Today.ToShortDateString().ToString();
            DateLabel.BorderBrush = Brushes.Black;
            DateLabel.BorderThickness = new Thickness(1);

            menuPanel.Children.Add(backButton);
            menuPanel.Children.Add(removeButton);
            menuPanel.Children.Add(markAsCompleteButton);
            menuPanel.Children.Add(MarkAsIncompleteButton);

            titlePanel.Children.Add(TitleTextBox);
            datePanel.Children.Add(DateLabel);
            contentPanel.Children.Add(ContentTextBox);

            titleBasePanel.Children.Add(titlePanel);
            titleBasePanel.Children.Add(datePanel);

            basePanel.Children.Add(menuPanel);
            basePanel.Children.Add(titleBasePanel);
            basePanel.Children.Add(contentPanel);

            this.Content = basePanel;
        }

        public void UpdateTitle(string value)
        {
            TitleTextBox.Text = value;
        }

        public void UpdateContent(string value)
        {
            ContentTextBox.Text = value;
        }

        public void RemoveButtonClickHandler(object sender, RoutedEventArgs e)
        {
            RemoveButtonClickArgs eventArgs = new RemoveButtonClickArgs();
            eventArgs.Page = this;

            RemoveButtonClick?.Invoke(sender, eventArgs);
        }

        public event RemoveButtonClickHandler RemoveButtonClick;

        public void MarkAsCompleteButtonClickHandler(object sender, RoutedEventArgs e)
        {
            MarkAsCompleteClickArgs eventArgs = new MarkAsCompleteClickArgs();
            eventArgs.Page = this;

            MarkAsCompleteButtonClick?.Invoke(sender, eventArgs);
        }

        public event MarkAsCompleteClickHandler MarkAsCompleteButtonClick;

        public void MarkAsIncompleteButtonClickHandler(object sender, RoutedEventArgs e)
        {
            MarkAsIncompleteClickArgs eventArgs = new MarkAsIncompleteClickArgs();
            eventArgs.Page = this;

            MarkAsIncompleteButtonClick?.Invoke(sender, eventArgs);
        }


        public event MarkAsIncompleteClickHandler MarkAsIncompleteButtonClick;
    }
}
