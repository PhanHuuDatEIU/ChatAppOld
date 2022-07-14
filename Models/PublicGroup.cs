using ChatApp.Models.Interface;

namespace ChatApp.Models
{
    public class PublicGroup : IGroup
    {
        public int Id { get; set; }

        public string GroupName { get; set; }

        public List<User> GroupMemberList { get; set; } = new List<User>();

    }
}
