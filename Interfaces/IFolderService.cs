using CoreMVCcatalog.Models;

namespace CoreMVCcatalog.Interfaces
{
    public interface IFolderService
    {


        
        public bool AddFolder(Folder folder);
        public bool UpdateFolder(Folder folder);
        public bool DeleteFolder(Folder folder);
        public bool Save();



        public Folder GetRootFolder();
        public Folder GetFolderByName(string name);
        public ICollection<Folder> GetSubfolders(Folder folder);


    }
}
