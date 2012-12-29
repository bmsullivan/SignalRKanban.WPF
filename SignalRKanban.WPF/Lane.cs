using System.Collections.ObjectModel;

namespace SignalRKanban.WPF
{
    public class Lane
    {
        public Lane(string id)
        {
            ID = id;
            Cards = new ObservableCollection<Card>();
        }

        public string ID { get; set; }
        public ObservableCollection<Card> Cards { get; set; }
    }
}