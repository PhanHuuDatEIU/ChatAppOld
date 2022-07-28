using ChatApp.Models.Interface;

namespace ChatApp.Models
{
    public class PrivateGroup : IGroup
    {
        public int Id { get; set; }

        public User GroupAdmin { get; set; }

        public string Name { get; set; }

        public IEnumerable<User> MemberList { get; set; }
    }
}
