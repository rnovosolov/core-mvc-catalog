namespace CoreMVCcatalog.Models
{
    public class Folder
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Folder>? Subfolders { get; set; }
        public Folder? parent { get; set; }

        //????????????????
        //public int level { get; set; }

        //public string fullpath { get; set; }

        //public List<File>? Files; 
    }
}
