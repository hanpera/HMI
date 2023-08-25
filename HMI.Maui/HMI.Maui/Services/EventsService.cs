using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using HMI.Maui.Models;

namespace HMI.Maui.Services
{
    public class EventsService
    {
        private readonly HttpClient _httpClient;

        public EventsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Event>> GetEventsAsync(EventsRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("events", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Event>>() ?? Array.Empty<Event>();
        }
    }
}
