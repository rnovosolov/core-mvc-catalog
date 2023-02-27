using CoreMVCcatalog.Interfaces;
using CoreMVCcatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreMVCcatalog.Services
{
    public class FolderService : IFolderService
    {
        private readonly ApplicationDbContext _context;

        public FolderService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool AddFolder(Folder folder)
        {
            _context.Update(folder);
            return Save();
        }

        public bool UpdateFolder(Folder folder)
        {
            _context.Update(folder);
            return Save();
        }
        public bool DeleteFolder(Folder folder)
        {
            _context.Remove(folder);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }




        public Folder GetFolderByName(string name)
        {
            throw new NotImplementedException();
        }

        public Folder GetRootFolder()
        {
            throw new NotImplementedException();
        }

        public ICollection<Folder> GetSubfolders(Folder folder)
        {
            throw new NotImplementedException();
        }

        

        
    }
}
