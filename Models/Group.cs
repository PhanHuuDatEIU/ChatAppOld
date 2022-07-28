namespace ChatApp.Models
{
    public abstract class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<User> MemberList { get; set; }
        public bool IsPrivate { get; set; }
    }
}
