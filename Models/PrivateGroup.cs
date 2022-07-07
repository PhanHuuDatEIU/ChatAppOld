using ChatApp.Models.Interface;

namespace ChatApp.Models
{
    public class PrivateGroup : IGroup
    {
        public int Id { get; set; }
        public List<User> AdminList { get; set; }

        public string GroupName { get; set; }

        public List<User> GroupMemberList { get; set; }
    }
}
