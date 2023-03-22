using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using file_uploader.DTO;
using file_uploader.Models;
using System.Drawing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace file_uploader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploaderController : ControllerBase
    {
        private readonly FileUploaderContext _context;

        public FileUploaderController(FileUploaderContext context)
        {
            _context = context;
        }

        // GET: api/FileUploader
        [HttpGet]
        public async Task<ActionResult<List<UserModel>>>  GetAllUsers ()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        // GET api/FileUploader/5
        [HttpGet("{UserId}")]
        public async Task<ActionResult> GetFiles(int UserId, string? FileName, int Version = -1)
        {
            var userFiles = _context.UserFiles.Where(i => i.UserId == UserId);

            if (!String.IsNullOrEmpty(FileName))
            {
                userFiles = userFiles.Where(i => i.FileName.Contains(FileName));
            }
            if (Version == 0)
            {
                userFiles = userFiles.Where(i => i.Version == 0);
            }
            else if (Version > 0)
            {
                userFiles = userFiles.Where(i => i.Version == Version);
            }
            return Ok(userFiles);
        }

        // POST api/FileUploader
        [HttpPost]
        public async Task<ActionResult> UploadFiles([FromForm] UploadFileDTO uploadFile)  
        {
            var uploadedFiles = new string[] { };
            //Check Username and Password
            var user = _context.Users.Where(i => i.UserName == uploadFile.userName.Trim() && i.Password == uploadFile.password).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("Invalid Username or Password");
            }

            foreach (var file in uploadFile.userFiles)
            {
                //Check to see if this file exists in database
                var fileName = System.IO.Path.GetFileNameWithoutExtension(file.FileName).Trim();
                var fileExtension = System.IO.Path.GetExtension(file.FileName.Trim());
                var fileExists = _context.UserFiles.Where(i => i.FileName.Trim() == fileName.Trim() && i.FileExtension.Trim() == fileExtension.Trim() && i.UserId == user.Id).OrderByDescending(i => i.Version).FirstOrDefault();

                //File doesn't exists, create a new record and save to user directory
                if (fileExists == null)
                {
                    //Create {user.Username} directory
                    System.IO.Directory.CreateDirectory($"UserFiles\\{user.UserName}");

                    //Move file to {user.Username} directory
                    using (var stream = System.IO.File.Create($"UserFiles\\{user.UserName}\\{fileName.Trim()}{fileExtension}"))
                    {
                        await file.CopyToAsync(stream);
                    }

                    //Save details to DB
                    var newFile = new UserFileModel()
                    {
                        UserId = user.Id,
                        FileName = fileName.Trim(),
                        FileExtension = fileExtension.Trim(),
                        FileLocation = user.UserName,
                        Version = 0
                    };

                    _context.Add(newFile);
                    _context.SaveChanges();
                }

                //File exists, move current file to {user.UserName}/Archive/{fileExists.Version}/{fileName}{fileExists.FileExtension}
                else
                {
                    //Update DB with new values
                    fileExists.FileName = $"{fileExists.FileName.Trim()}";
                    fileExists.Version = fileExists.Version + 1;
                    _context.SaveChanges();

                    //Create Archive Diretory
                    System.IO.Directory.CreateDirectory($"UserFiles\\{user.UserName}\\Archive\\{fileExists.Version}");

                    //Move latest version to Archive
                    System.IO.File.Move($"UserFiles\\{user.UserName}\\{fileName.Trim()}{fileExtension.Trim()}", $"UserFiles\\{user.UserName.Trim()}\\Archive\\{fileExists.Version}\\{fileName.Trim()}{fileExtension.Trim()}");

                    //Move NEW uploaded file to {user.Username} directory
                    using (var stream = System.IO.File.Create($"UserFiles\\{user.UserName}\\{fileName.Trim()}{fileExtension.Trim()}"))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var updatedFile = new UserFileModel()
                    {
                        UserId = user.Id,
                        FileName = fileName.Trim(),
                        FileExtension = fileExists.FileExtension.Trim(),
                        FileLocation = user.UserName,
                        Version = 0
                    };
                    _context.Add(updatedFile);
                    _context.SaveChanges();

                }
                uploadedFiles.Append(fileName.Trim());
            }
            //User is valid
            return Ok(uploadFile.userFiles);
        }

        // PUT api/FileUploader/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/FileUploader/5
        [HttpDelete("{FileId}")]
        public async Task<ActionResult> Delete([FromForm] UploadFileDTO deleteFile, int FileId)
        {
            //Check Username and Password
            var user = _context.Users.Where(i => i.UserName == deleteFile.userName.Trim() && i.Password == deleteFile.password).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("Invalid Username or Password");
            }
            
            var removeRecord = _context.UserFiles.Where(i => i.Id == FileId).FirstOrDefault();

            if (removeRecord == null)
            {
                return Ok($"FileId {FileId} does not exists.");
            }

            //File exists, find and delete file from the fileSystem
            var filePath = $"UserFiles\\{user.UserName}";
            if (removeRecord.Version != 0)
            {
                filePath = $"{filePath}\\Archive\\{removeRecord.Version}";
            }
            filePath = $"{filePath}\\{removeRecord.FileName}{removeRecord.FileExtension}";
            System.IO.File.Delete(filePath);

            _context.UserFiles.Remove(removeRecord);
            _context.SaveChanges();

            return Ok($"FileId {FileId} deleted.");
        }
    }
}
