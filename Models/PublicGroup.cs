using ChatApp.Models.Interface;

namespace ChatApp.Models
{
    public class PublicGroup : IGroup
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string InviteCode { get; set; }
        public IEnumerable<User> GroupMemberList { get; set; }

    }
}
