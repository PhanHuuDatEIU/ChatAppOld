using ChatApp.Models;
using ChatApp.Repository;

namespace ChatApp.Data
{
    internal class DataStorage
    {
        public Repository<User> Users { get; }
        public Repository<Message> Messages { get; }

        public Repository<Group> Groups { get; }
        public Repository<CFile> Files { get; }
        public Repository<Alias> Aliases { get; }
        private static DataStorage _dataStorage { get; set; }
        private DataStorage()
        {
            Users = new Repository<User>();
            Messages = new Repository<Message>();
            Groups = new Repository<Group>();
            Files = new Repository<CFile>();
            Aliases = new Repository<Alias>();
        }

        public static DataStorage GetDataStorage()
        {
            if (_dataStorage == null)
            {
                _dataStorage = new DataStorage();
            }

            return _dataStorage;
        }
    }
}
