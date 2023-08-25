using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMI.Maui.Models
{
    public class Event
    {

        public long Id { get; set; }

        public string? UserName { get; set; }

        public string? UserSurname { get; set; }

        public DateTime? Date { get; set; }

        public string? Allarms { get; set; }
    }
}
