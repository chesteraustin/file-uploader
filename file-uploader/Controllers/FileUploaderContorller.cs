﻿using file_uploader.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using file_uploader.DTO;
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
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(user);
        }

        // POST api/FileUploader
        [HttpPost]
        public async Task<ActionResult> UploadFiles([FromForm] UploadFileDTO uploadFile)  
        {
            //Check Username and Password
            var user = _context.Users.Where(i => i.UserName == uploadFile.userName.Trim() && i.Password == uploadFile.password).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("Invalid Username or Password");
            }
            
            //User is valid
            return Ok(user);
        }

        // PUT api/FileUploader/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/FileUploader/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
