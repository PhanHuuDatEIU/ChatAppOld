using ChatApp.Data;
using ChatApp.Models;
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

        #region general
        public bool RemoveUserFromGroup(int userId, int groupId)
        {
            return false;
        }

        #endregion

        #region public group 

        public PublicGroup CreatePublicGroup(string groupName, List<User> members)
        {
            PublicGroup publicGroup = new PublicGroup();
            publicGroup.GroupName= groupName;
            publicGroup.GroupMemberList = members;
            return publicGroup;
        }

        public bool JoinPublicGroup(int userId, string inviteCode)
        {
            //validate user existance
            var user = GetUser(userId);
            if (user == null)
            {
                return false;
            }

            //validate invite code
            var group = GetPublicGroupByCode(inviteCode);
            if (group == null)
            {
                return false;
            }

            //validate if user is in group
            foreach (var member in group.GroupMemberList)
            {
                if (member.Id == userId)
                {
                    return false;
                }
            }

            //add user to the group if all case satisfied
            group.GroupMemberList.Append(user);
            return true;

        }

        /// <summary>
        /// Generate a unique group invite code 
        /// Should only be available for public group
        /// </summary>
        public bool GenerateInviteCode(int groupId)
        {
            string generatedInviteCode;

            // validate group existance
            var group = GetPublicGroupById(groupId);
            if (group == null)
            {
                return false;
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
                return true;
            }
        }
        #endregion

        #region private group

        public PrivateGroup CreatePrivateGroup(string groupName, User admin, List<User> members)
        {
            PrivateGroup privateGroup = new PrivateGroup();
            privateGroup.GroupName = groupName;
            privateGroup.GroupAdmin = admin;
            privateGroup.GroupMemberList = members;
            return privateGroup;
        }

        public bool AddUser(int adminID, int userId, int groupId)
        {
            //validate group existance
            var group = GetPrivateGroupByID(groupId);
            if (group == null)
            {
                return false;
            }

            //validate user exitstance
            var user = GetUser(userId);
            if (user == null)
            {
                return false;
            }

            //If all condition are passed, add the user to the group if the admin is truthly an admin
            if (group.GroupAdmin.Id == adminID)
            {
                group.GroupMemberList.Append(user);
                return true;
            } else
            {
                return false;
            }
        }

        #endregion

        #region ultilities function

        private User GetUser(int userId)
        {
            var user = dataStorage.Users.GetFirstOrDefault(u => u.Id == userId);
            return user;
        }

        private PublicGroup GetPublicGroupByCode(string inviteCode)
        {
            var group = dataStorage.PublicGroups.GetFirstOrDefault(g => g.InviteCode.Equals(inviteCode));
            return group;
        }

        private PublicGroup GetPublicGroupById(int groupId)
        {
            var group = dataStorage.PublicGroups.GetFirstOrDefault(g => g.GroupId == groupId);
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
        private Boolean CheckUniqueCode(string inviteCode)
        {
            var existGroup = dataStorage.PublicGroups.GetFirstOrDefault(g => g.InviteCode.Equals(inviteCode));
            if (existGroup == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private PrivateGroup GetPrivateGroupByID(int groupID)
        {
            var group = dataStorage.PrivateGroups.GetFirstOrDefault(g => g.GroupId == groupID);
            return group;
        }

        #endregion
    }
}
