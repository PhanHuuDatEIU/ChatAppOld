using ChatApp.Data;
using ChatApp.Models;

namespace ChatApp.Services
{
    public class MessageService
    {
        private readonly DataStorage dataStorage = DataStorage.GetDataStorage();
        public bool DeleteMessage(int id, string webRootPath)
        {
            Message? message = dataStorage.Messages.GetFirstOrDefault(mess => mess.Id == id);
            if (message != null)
            {
                dataStorage.Messages.Remove(message);
                if (message.Path != null)
                {
                    DeleteFileIfExist(message.Path, webRootPath);
                }
                return true;
            }
            return false;
        }

        public List<string>? DisplayAllFile(int groupId)
        {
            List<Message>? messageList = dataStorage.Messages.GetAll(mess => mess.Path != null && mess.InGroupId == groupId).ToList();
            List<string>? filePathList = null;
            if (messageList != null)
            {
                filePathList = messageList.Select(message => message.Path).ToList();
            }
            return filePathList;
        }
        private void DeleteFileIfExist(string imageUrl, string webRootPath)
        {
            var oldImage = Path.Combine(webRootPath, imageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImage))
            {
                System.IO.File.Delete(oldImage);
            }
        }
        public void UploadNewFile(int userId, int groupId, string webRootPath, IFormFileCollection? files)
        {
            string fileName_new = Guid.NewGuid().ToString();
            var uploads = Path.Combine(webRootPath, @"images\menuItems");
            var extension = Path.GetExtension(files[0].FileName);

            using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }
            Message message = new()
            {
                Id = GenerateMessageId(),
                Path = @"\images\menuItems\" + fileName_new + extension,
                InGroupId = groupId,
                FromUserId = userId,
                CreatedDate = DateTime.Now,
            };
            dataStorage.Messages.Add(message);
        }

        private int GenerateMessageId()
        {
            int id = 0;
            if (dataStorage.Messages.GetAll().ToArray() != null)
            {
                id = dataStorage.Messages.GetAll().ToArray().Length;
            }
            return id;
        }
    }
}
