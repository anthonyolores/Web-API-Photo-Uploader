using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webapiupload.Models
{
    public class FileUpload
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int imageid
        {
            get;
            set;
        }
        public string imagename
        {
            get;
            set;
        }
        public byte[] imagedata
        {
            get;
            set;
        } //this property is used to insert a  information of Image in byte format  
    }
}