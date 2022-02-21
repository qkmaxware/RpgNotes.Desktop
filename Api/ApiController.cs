using System.IO;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RpgNotes.Desktop.Api {

[Route("api/")]
public class ApiController : Controller {
    [HttpGet]
    [Route("ping")]
    public string Ping() {
        return "Pong";
    }
    
    [HttpPost]
    [Route("files/upload")]
    public ActionResult Upload(IFormFile file) {
        return Ok();
    }

    [HttpGet]
    [Route("files/getFromReference/{reference}")]
    public ActionResult DownloadFromReference(string reference) {
        var path = reference.DecodeBase64();
        return DownloadFromPath(path);
    }

    [HttpGet]
    [Route("files/get/{path}")]
    public ActionResult DownloadFromPath(string path) {
        path = HttpUtility.UrlDecode(path);
        if (System.IO.File.Exists(path)) {
            var ext = Path.GetExtension(path);
            // Just a dummy mime mapper for some common images
            var mime = ext switch {
                ".png"  => "image/png",
                ".jpeg" => "image/jpeg",
                ".jpg"  => "image/jpeg",
                ".tif"  => "image/tiff",
                ".tiff" => "image/tiff",
                ".svg"  => "image/svg+xml",
                ".gif"  => "image/gif",
                _       => "application/octet-stream"
            };
            var stream = System.IO.File.OpenRead(path);
            return File(stream, mime);
        } else {
            return BadRequest();
        }
    }
}

}