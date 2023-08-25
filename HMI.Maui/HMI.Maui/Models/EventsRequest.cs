using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMI.Maui.Models
{
    public class EventsRequest
    {
        public EventFilters Filters { get; set; } = new();
        public Order? Order { get; set; }
        public Pager Pager { get; set; } = new();

        public void Clear()
        {
            Filters = new();
            Order = null;
            Pager = new();
        }
    }

    public class EventFilters
    {
        public string? User { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string? Allarm { get; set; }
    }

    public class Order
    {
        public string? Property { get; set; }
        public string? Direction { get; set; }
    }

    public class Pager
    {
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}
