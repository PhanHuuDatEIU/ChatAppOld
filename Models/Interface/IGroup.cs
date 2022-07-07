namespace ChatApp.Models.Interface
{
    public interface IGroup
    {
        int Id { get; set; }
        string Name { get; set; }
        IEnumerable<User> MemberList { get; set; }

    }
}
