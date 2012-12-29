using System;

namespace SignalRKanban.WPF
{
    public class Card
    {
        public Lane Lane { get; set; }
        public Guid ID { get; set; }
        public string Content { get; set; }
    }
}