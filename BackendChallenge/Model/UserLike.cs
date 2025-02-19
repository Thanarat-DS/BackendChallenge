
namespace BackendChallenge.Model
{
    public class UserLike
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
