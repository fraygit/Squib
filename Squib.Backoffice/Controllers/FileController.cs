using Squib.Backoffice.Services;
using Squib.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Squib.Backoffice.Controllers
{
    public class FileController : Controller
    {
        //
        // GET: /File/

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public FileResult DownloadFileByPath(string path, string filename, bool isEmbed)
        {
            if (!string.IsNullOrEmpty(path))
            {
                var amazon = new AmazonService();
                var fileByte = amazon.ReteiveFile(path);
                if (isEmbed)
                {
                    var cd = new System.Net.Mime.ContentDisposition
                    {
                        FileName = filename,
                        Inline = true,
                    };
                    string contentType = MimeMapping.GetMimeMapping(filename);
                    Response.AppendHeader("Content-Disposition", cd.ToString());
                    return File(fileByte, contentType);
                }
                else
                {
                    return File(fileByte, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
                }
            }
            return null;
        }

    }
}
