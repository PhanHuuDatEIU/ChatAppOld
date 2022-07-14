using ChatApp.Data;
using ChatApp.Models;
using ChatApp.Models.Interface;
using System.Text;

namespace ChatApp.Services
{
    public class GroupService
    {
        private readonly DataStorage dataStorage;

        public GroupService()
        {
            dataStorage = DataStorage.GetDataStorage();
        }

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

        #region public group 

        /// <summary>
        /// Let a user join to a public group
        /// </summary>
        /// <param name="userId">Specify the user id to be added</param>
        /// <param name="inviteCode">Specify the group invite code</param>
        /// <returns>
        /// The status of the action
        /// </returns>
        public string JoinPublicGroup(int userId, string inviteCode)
        {
            //validate user existance
            var user = GetUser(userId);
            if (user == null)
            {
                return "User not found";
            }

            //validate invite code
            var group = GetPublicGroupByCode(inviteCode);
            if (group == null)
            {
                return "Invalid invite code";
            }

            //validate if user is in group
            foreach (var member in group.MemberList)
            {
                if (member.Id == userId)
                {
                    return "the user is already a member of this group";
                }
            }

            //add user to the group if all case satisfied
            group.MemberList.Append(user);
            return "The user is successfull added";

        }

        /// <summary>
        /// Generate a unique group invite code 
        /// Should only be available for public group
        /// </summary>
        /// <param name="groupId">Specify the id of the group that need invite code</param>
        /// <returns>
        /// Status whether the code is successfully generated
        /// </returns>
        public string GenerateInviteCode(int groupId)
        {
            string generatedInviteCode;

            // validate group existance
            var group = GetPublicGroupById(groupId);
            if (group == null)
            {
                return "Group not found";
            }
            else
            {
                //Check if the group has an invite code
                //then generate and check unique code if the group does not have invite code
                if (String.IsNullOrEmpty(group.InviteCode))
                {
                    do
                    {
                        generatedInviteCode = GenerateRandomString();
                    }
                    while (!CheckUniqueCode(generatedInviteCode));
                    group.InviteCode = generatedInviteCode;
                }
                return "Code successfully generated";
            }

        }
        #endregion

        #region private group

        /// <summary>
        /// Add/invite a user to a specific group
        /// </summary>
        /// <param name="adminID">The admin - the one who send the invitation</param>
        /// <param name="userrId">The reveicer - the one who getting added/invited to the group</param>
        /// <param name="groupId">The specific group id</param>
        /// <returns>The status of the action</returns>
        public string AddUser(int adminID, int userId, int groupId)
        {
            //validate group existance
            var group = GetPrivateGroupByID(groupId);
            if (group == null)
            {
                return "Group not found";
            }

            //validate user exitstance
            var user = GetUser(userId);
            if (user == null)
            {
                return "User not found";
            }

            //If all condition are passed, add the user to the group if the admin is truthly an admin
            if (group.GroupAdmin.Id == adminID)
            {
                group.MemberList.Append(user);
                return "User added";
            } else
            {
                return "User cannot be added";
            }
        }

        #endregion

        #region ultilities function

        /// <summary>
        /// Get the user by id
        /// </summary>
        /// <param name="userId">the id of the user that need to retrieve</param>
        /// <returns>the user if exist or null</returns>
        private User GetUser(int userId)
        {
            var user = dataStorage.Users.GetFirstOrDefault(u => u.Id == userId);
            return user;
        }

        /// <summary>
        /// Get the group by invite code
        /// </summary>
        /// <param name="inviteCode">the invite code of the group</param>
        /// <returns>the group if exist or null</returns>
        private PublicGroup GetPublicGroupByCode(string inviteCode)
        {
            var group = dataStorage.PublicGroups.GetFirstOrDefault(g => g.InviteCode.Equals(inviteCode));
            return group;
        }

        /// <summary>
        /// Get the group by group id
        /// </summary>
        /// <param name="groupId">The id of the group</param>
        /// <returns>the group if exist or null</returns>
        private PublicGroup GetPublicGroupById(int groupId)
        {
            var group = dataStorage.PublicGroups.GetFirstOrDefault(g => g.Id == groupId);
            return group;
        }

        /// <summary>
        /// Generate a random string with leter value from 65 to 90
        /// Not guarantee to generate unique string
        /// </summary>
        /// <returns>
        /// Return a string with length 7 and random upper or lower case
        /// </returns>
        private string GenerateRandomString()
        {
            int length = 7;
            StringBuilder outBuffer = new StringBuilder();
            Random random = new Random();

            char letter;

            for (int i = 0; i < length; i++)
            {
                double floatNumber = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * floatNumber));
                letter = Convert.ToChar(shift + 65);
                int shouldLowerCase = random.Next(0, 2);
                if (shouldLowerCase == 1)
                {
                    letter = Char.ToLower(letter);
                }
                outBuffer.Append(letter);
            }
            return outBuffer.ToString();
        }

        /// <summary>
        /// check if there is any group has the same invite code
        /// </summary>
        /// <param name="inviteCode">The code that need to check</param>
        /// <returns>
        /// Return true of there is no group has the same code, otherwise false
        /// </returns>
        private Boolean CheckUniqueCode(string inviteCode)
        {
            var existCode = dataStorage.PublicGroups.GetFirstOrDefault(g => g.InviteCode.Equals(inviteCode));
            if (existCode == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get the group by group id
        /// </summary>
        /// <param name="groupID">The id of the group</param>
        /// <returns>the group if exist or null</returns>
        private PrivateGroup GetPrivateGroupByID(int groupID)
        {
            var group = dataStorage.PrivateGroups.GetFirstOrDefault(g => g.Id == groupID);
            return group;
        }
        #endregion
    }
}
