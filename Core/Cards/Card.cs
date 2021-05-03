using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Cards
{
    public class Card
    {
        public Guid ID { get; internal set; }

        public string Category { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public int Level { get; set; }

        public DateTime CreationDate { get; internal set; }

        public DateTime LastRepeat { get; set; }

        public DateTime NextRepeat { get; set; }
    }
}
