using ChatApp.Data;
using ChatApp.Models;
using ChatApp.Models.Interface;

namespace ChatApp.Services
{
    public class GroupService
    {
        private readonly DataStorage dataStorage = DataStorage.GetDataStorage();

        //public string CreateNewGroup(string groupName,bool isPrivate = false, User? admin = null, List<User>? members = null)
        //{
            
        //    if (isPrivate)
        //    {
        //        dataStorage.PrivateGroups.Add(CreatePrivateGroup(groupName, admin, members));
        //        return "craeted private group";
        //    }
        //    else
        //    {
        //        dataStorage.PublicGroups.Add(CreatePublicGroup(groupName, members));
        //        return "craeted public group";
        //    }                    
        //}

        public PrivateGroup CreatePrivateGroup(string groupName, User admin, List<User> members)
        {
            PrivateGroup privateGroup = new PrivateGroup();
            privateGroup.GroupName= groupName;
            privateGroup.GroupAdmin = admin;
            privateGroup.GroupMemberList = members;
            return privateGroup;
        }

        public PublicGroup CreatePublicGroup(string groupName, List<User> members)
        {
            PublicGroup publicGroup = new PublicGroup();
            publicGroup.GroupName= groupName;
            publicGroup.GroupMemberList = members;
            return publicGroup;
        }
    }
}
