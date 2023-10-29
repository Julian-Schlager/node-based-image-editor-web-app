using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using NodeEditor.BuisnessLogic.Interfaces;
using NodeEditor.Entities;
using System.Net.Mime;
using System.Text.Json;

namespace NodeEditor.RestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
      
        private readonly ILogger<ImageController> _logger;
        private readonly IImageService imageService;

        public ImageController(ILogger<ImageController> logger, IImageService imageService)
        {
            _logger = logger;
            this.imageService = imageService;
        }

        [HttpPost("Edit")]
        public async Task<ActionResult<Stream>> Post([FromForm]EditData data) // ToDo
        {
            try
            {
                if (data != null)
                {
                    Stream newMemoryStream = new MemoryStream();
                    data.FormFile.CopyTo(newMemoryStream);
                    newMemoryStream.Position = 0;
                    Node firstNode = JsonSerializer.Deserialize<Node>(data.FirstNodeJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    IEnumerable<Stream> resultImages = await this.imageService.EditImage(newMemoryStream, firstNode, data.FileName);

                    return File(resultImages.ElementAt(0),GetMimeType(data.FileName),data.FileName);
                }
                return BadRequest(string.Empty);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        private string GetMimeType(string filename)
        {
            string ext = Path.GetExtension(filename).ToLower();

            switch (ext)
            {
                case ".jpeg": return "image/jpeg";
                case ".jpg": return "image/jpg";
                case ".png": return "image/png";
                case ".bmp": return "image/bmp";
                case ".gif": return "image/gif";
            }
            return "application/octet-stream";
        }
    }


    public class EditData
    {
        public string FileName { get; set; }
        public IFormFile FormFile { get; set; }
        public string FirstNodeJson { get; set; }
    }
}