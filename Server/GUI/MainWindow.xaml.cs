using System.Windows;
using Infrastructure.Network.Sockets;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var server = new Server();
            server.Start();
        }
    }
}
