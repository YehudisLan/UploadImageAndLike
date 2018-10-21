using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWUploadImages.Data
{
    public class DBManager
    {
        private string _connectionString;
        public DBManager(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IEnumerable<Image> GetImages()
        {
            using (var context = new ImageDataContext(_connectionString))
            {
                return context.Images.OrderByDescending(i => i.Id).ToList();
            }
        }
        public void AddImage(Image image)
        {
            using (var context = new ImageDataContext(_connectionString))
            {
                context.Images.InsertOnSubmit(image);
                context.SubmitChanges();
            }
        }
        public Image GetImage(int id)
        {
            using (var context = new ImageDataContext(_connectionString))
            {
                return context.Images.FirstOrDefault(i => i.Id == id);
            }
        }
        public void AddLike(int id)
        {
            using (var context = new ImageDataContext(_connectionString))
            {
                Image image = GetImage(id);
                image.Likes = image.Likes + 1;
                context.Images.Attach(image);
                context.Refresh(System.Data.Linq.RefreshMode.KeepCurrentValues, image);
                context.SubmitChanges();  
            }
        }
        public int GetLikes(int id)
        {
            Image i = GetImage(id);
            return i.Likes;
        }

    }
}
