using ChatApp.Models.Interface;

namespace ChatApp.Models
{
    public class PrivateGroup : IGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<User> AdminList { get; set; }
        public IEnumerable<User> MemberList { get; set; }
    }
}
