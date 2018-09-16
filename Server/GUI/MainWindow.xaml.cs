using System;
using System.Windows;
using Infrastructure.Commands.Camera;
using Infrastructure.Network.Sockets;
using Newtonsoft.Json;

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
            //var server = new Server();
            //server.Start();
            var settings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
            var command = new Position()
            {
                X = 10,
                Y = 20,
                Theta = 115,
                Identified = true
            };
            var json = JsonConvert.SerializeObject(command, settings);
            Console.WriteLine(json);
        }
    }
}
