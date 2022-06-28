
using MovingMotivators.ViewModels;

namespace MovingMotivators.IOC
{
    internal class CompositionRoot
    {
        public void Run()
        {
            MainViewModel mainViewModel = new();

            MainView mainView = new(mainViewModel);

            mainView.Show();
        }
    }
}
