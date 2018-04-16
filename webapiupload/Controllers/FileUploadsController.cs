using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using webapiupload.Models;

namespace webapiupload.Controllers
{
    public class FileUploadsController : ApiController
    {
        private FileUploadContext db = new FileUploadContext();

        // GET: api/FileUploads
        [ResponseType(typeof(IQueryable<FileUpload>))]
        public IQueryable<FileUpload> GetfileUpload()
        {
            return db.fileUpload;
        }

        // GET: api/FileUploads/5
        [ResponseType(typeof(FileUpload))]
        public IHttpActionResult GetFileUpload(int id)
        {
            FileUpload fileUpload = db.fileUpload.Find(id);
            if (fileUpload == null)
            {
                return NotFound();
            }

            return Ok(fileUpload);
        }

        // PUT: api/FileUploads/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFileUpload(int id, FileUpload fileUpload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fileUpload.imageid)
            {
                return BadRequest();
            }

            db.Entry(fileUpload).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FileUploadExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/FileUploads
        [ResponseType(typeof(FileUpload))]
        [HttpPost]
        public IHttpActionResult PostFileUpload()
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var httpPostedFile = HttpContext.Current.Request.Files["uploadedfile"];
                if (httpPostedFile != null)
                {
                    FileUpload imgupload = new FileUpload();
                    int length = httpPostedFile.ContentLength;
                    imgupload.imagedata = new byte[length];
                    httpPostedFile.InputStream.Read(imgupload.imagedata, 0, length);
                    imgupload.imagename = Path.GetFileName(httpPostedFile.FileName);
                    db.fileUpload.Add(imgupload);
                    db.SaveChanges();
                    var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), httpPostedFile.FileName);
                    httpPostedFile.SaveAs(fileSavePath);
                    return Ok("Image Uploaded");
                }
            }
            return Ok("Image is not Uploaded");
        }

        // DELETE: api/FileUploads/5
        [ResponseType(typeof(FileUpload))]
        public IHttpActionResult DeleteFileUpload(int id)
        {
            FileUpload fileUpload = db.fileUpload.Find(id);
            if (fileUpload == null)
            {
                return NotFound();
            }

            db.fileUpload.Remove(fileUpload);
            db.SaveChanges();

            return Ok(fileUpload);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FileUploadExists(int id)
        {
            return db.fileUpload.Count(e => e.imageid == id) > 0;
        }
    }
}