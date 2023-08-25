using HMI.API.Data;
using HMI.API.DataModel;
using HMI.API.DTO;
using HMI.API.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HMI.API.Services
{
    public class DbService
    {
        private readonly HMIContext _context;

        public DbService(HMIContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetEventsAsync(EventsRequest request)
        {
            var pb = PredicateBuilder.True<Event>();
            //TODO: Aggiungere filtri per la ricerca
            if (request is { Filters: not null })
            {
                if (!string.IsNullOrEmpty(request.Filters.User))
                    pb = pb.And(e => (e.UserName != null && e.UserName.Contains(request.Filters.User)) || (e.UserSurname!= null  && e.UserSurname.Contains(request.Filters.User)));
                if (request.Filters.From.HasValue)
                    pb = pb.And(e => e.Date >= request.Filters.From);
                if (request.Filters.To.HasValue)
                    pb = pb.And(e => e.Date <= request.Filters.To);
                if (!string.IsNullOrEmpty(request.Filters.Allarm))
                    pb = pb.And(e => e.Allarms != null &&  e.Allarms.Contains(request.Filters.Allarm));
            }

            return await _context.Events.Where(pb)
                //.OrderByDescending(x => x.Date)
                .OrderBy(request.Order?.Property, request.Order?.Direction).GetPage(request.Pager?.Page ?? 1, request.Pager?.PageSize ??-1)
                .ToListAsync();
        }
    }
}
