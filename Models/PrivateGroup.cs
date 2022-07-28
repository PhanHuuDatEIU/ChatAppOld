using ChatApp.Models.Interface;

namespace ChatApp.Models
{
    public class PrivateGroup : IGroup
    {
        public string GroupName { get; set; }
        public int GroupId { get; set; }
        public User GroupAdmin { get; set; }
        public IEnumerable<User> GroupMemberList { get; set; }
    }
}
