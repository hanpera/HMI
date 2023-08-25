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
        public Order? Order { get; set; } = new();
        public Pager Pager { get; set; } = new();

        public void Clear()
        {
            Filters = new();
            Order = new Order();
            Pager = new Pager();
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
        public string? Property { get; set; } = "Date";
        public string? Direction { get; set; } = "desc";
    }

    public class Pager
    {
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}
