using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace webapiupload.Models
{
    public class FileUploadContext : DbContext
    {
        public FileUploadContext() : base("name=TestConnection") { }
        public DbSet<FileUpload> fileUpload
        {
            get;
            set;
        }
    }
}