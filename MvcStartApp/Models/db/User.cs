namespace MvcStartApp.Models.db
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime JoinDate { get; set; }

        public List<UserPost> UserPosts { get; set; } = new List<UserPost>();
    }
}
