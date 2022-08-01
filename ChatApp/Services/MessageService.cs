using ChatApp.Data;
using ChatApp.Models;

namespace ChatApp.Services
{
    public class MessageService
    {
        private readonly DataStorage dataStorage = DataStorage.GetDataStorage();
        private FileService fileService;
        public MessageService(FileService fileService)
        {
            this.fileService = fileService;
        }
        public bool DeleteMessage(int id, string webRootPath)
        {
            Message? message = dataStorage.Messages.GetFirstOrDefault(mess => mess.Id == id);
            if (message != null)
            {
                dataStorage.Messages.Remove(message);
                if (message.Path != null)
                {
                    fileService.DeleteFileIfExist(message.Path, webRootPath);
                }
                return true;
            }
            return false;
        }

        public int GenerateMessageId()
        {
            int id = 0;
            if (dataStorage.Messages.GetAll().ToArray() != null)
            {
                id = dataStorage.Messages.GetAll().ToArray().Length;
            }
            return id;
        }

        public List<Group> GetGroupOfUser(User user)
        {
            List<Group> groups = new List<Group>();
            var getAllGroup = dataStorage.Groups.GetAll(group=>group.MemberList.Select(m=>m.Id).Contains(user.Id)).ToList();
            return getAllGroup;
        }

        

        public List<int> GetConversations(User user)
        {
            List<int> conversations = new List<int>();
            conversations = dataStorage.Messages.GetAll(u => u.FromUserId == user.Id).Select(m => m.Id).ToList();
            
            return conversations;
        }

    }
}
