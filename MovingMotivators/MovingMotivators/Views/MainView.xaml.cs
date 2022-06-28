using System.Windows;

using MovingMotivators.ViewModels;

namespace MovingMotivators
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView(MainViewModel mainViewModel)
        {
            InitializeComponent();

            DataContext = mainViewModel;
        }
    }
}
