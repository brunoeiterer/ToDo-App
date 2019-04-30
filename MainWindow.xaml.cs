using System.Windows;

namespace ToDoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Manager manager;

        public MainWindow()
        {
            InitializeComponent();

            manager = new Manager(ref mainFrame);
        }
    }
}
