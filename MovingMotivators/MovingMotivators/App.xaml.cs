using System.Windows;
using MovingMotivators.IOC;

namespace MovingMotivators;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App
{
    private void OnStartup(object sender, StartupEventArgs e)
    {
        CompositionRoot root = new();
        root.Run();
    }
}