using ChatApp.Models.Enum;

namespace ChatApp.Models
{
    public class CFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime Created { get; set; }
        public FileType FileType { get; set; }
    }
}
