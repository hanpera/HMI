using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HMI.Maui.Models;
using HMI.Maui.Services;

namespace HMI.Maui.ViewModels
{
    public partial class EventsViewModel : BaseViewModel
    {
        private readonly EventsService _eventsService;
        public EventsViewModel(EventsService eventsService)
        {
            _eventsService = eventsService;
            Title = "Events";
        }

        public ObservableCollection<Event> Events { get; set; } = new();

        [ObservableProperty]
        bool _isRefreshing;

        [ObservableProperty]
        EventsRequest _request = new();

        [RelayCommand]
        public async Task RefreshEventsAsync()
        {
            try
            {
                IsLoading = true;
                IsRefreshing = true;
                var request = new EventsRequest()
                {
                    Filters = new()
                    {
                        From = new DateTime(2023, 07, 01),
                        To = DateTime.Now
                    },
                    Order = new()
                    {
                        Property = "Date",
                        Direction = "desc"
                    },
                    Pager = new()
                    {
                        Page = 1,
                        PageSize = 10
                    }
                };
                var events = await _eventsService.GetEventsAsync(request);
                Events.Clear();
                foreach (var @event in events)
                {
                    Events.Add(@event);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                IsLoading = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        public void Clear()
        {
            Request = new();
            Events.Clear();
        }
    }
}
