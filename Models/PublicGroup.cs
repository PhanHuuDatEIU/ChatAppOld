using ChatApp.Models.Interface;

namespace ChatApp.Models
{
    public class PublicGroup : IGroup
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string InviteCode { get; set; }

        public IEnumerable<User> MemberList { get; set; }

    }
}
