using Microsoft.EntityFrameworkCore;
using MvcStartApp.Models.db;

namespace MvcStartApp.Middleware.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly BlogContext context;

        public LogRepository(BlogContext context)
        {
            this.context = context;
        }

        public async Task AddRequestLog(Request request)
        {
            var entry = context.Entry(request);

            if (entry.State == EntityState.Detached)
            {
                await context.Requests.AddAsync(request);
            }

            await context.SaveChangesAsync();
        }

        public async Task<Request[]> GetRequestLogs()
        {
            return await context.Requests.ToArrayAsync();
        }
    }

    public interface ILogRepository
    {
        Task AddRequestLog(Request request);
        Task<Request[]> GetRequestLogs();
    }
}
