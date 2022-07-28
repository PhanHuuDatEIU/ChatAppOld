namespace ChatApp.Models.Interface
{
    public interface IGroup
    {
        int GroupId { get; set; }
        string GroupName { get; set; }
        IEnumerable<User> GroupMemberList { get; set; }
    }
}
