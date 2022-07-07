namespace ChatApp.Models.Interface
{
    public interface IGroup
    {
        int Id { get; set; }
        string GroupName { get; set; }
        List<User> GroupMemberList { get; set; }

    }
}
