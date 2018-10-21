using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HWUploadImagesOBM;

using System.Web;

namespace HWUploadImagesOBM.Data
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
                return context.Images.ToList();
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
        
    }
}