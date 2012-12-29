using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace SignalRKanban.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HubConnection _conn;
        private IHubProxy _proxy;

        public MainWindow()
        {
            InitializeComponent();
            Lanes = new ObservableCollection<Lane>{new Lane("1"), new Lane("2"), new Lane("3"), new Lane("4")};
            this.DataContext = this;
        }

        public ObservableCollection<Lane> Lanes { get; set; }

        private void btnCreateCard_Click(object sender, RoutedEventArgs e)
        {
            _proxy.Invoke("CreateCard");
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _conn = new HubConnection("http://localhost:62883");
            _proxy = _conn.CreateHubProxy("KanbanHub");

            _proxy.On("cardCreated", result => Dispatcher.Invoke(DispatcherPriority.Normal, new Action<dynamic>(data =>
                {
                    var lane = Lanes.Single(l => l.ID == (string)data.Lane);
                    lane.Cards.Add(new Card {ID = data.ID, Content = data.Content, Lane = lane});
                }), result));

            _conn.Start().Wait();
        }
    }
}
