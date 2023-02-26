using Microsoft.EntityFrameworkCore;
using MvcStartApp.Models.db;

namespace MvcStartApp.Middleware.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogContext context;

        public BlogRepository(BlogContext context)
        {
            this.context = context;
        }

        public async Task AddFeedback(Feedback feedback)
        {
            if (context.Entry(feedback).State == EntityState.Detached)
            {
                await context.Feedbacks.AddAsync(feedback);
            }

            await context.SaveChangesAsync();
        }

        public async Task AddUser(User user)
        {
            user.JoinDate = DateTime.Now;
            user.Id = Guid.NewGuid();

            if (context.Entry(user).State == EntityState.Detached)
            {
                await context.Users.AddAsync(user);
            }

            await context.SaveChangesAsync();
        }

        public async Task<User[]> GetUsers()
        {
            return await context.Users.ToArrayAsync();
        }
    }

    public interface IBlogRepository
    {
        Task AddUser(User user);
        Task<User[]> GetUsers();
        Task AddFeedback(Feedback feedback);
    }
}
