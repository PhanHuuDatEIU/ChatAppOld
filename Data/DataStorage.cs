using ChatApp.Models;
using ChatApp.Repository;

namespace ChatApp.Data
{
    internal class DataStorage
    {
        public Repository<User> Users { get; }

        private static DataStorage _dataStorage { get; set; }
        private DataStorage()
        {
            Users = new Repository<User>();
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
