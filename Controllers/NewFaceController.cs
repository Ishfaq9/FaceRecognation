using FaceRecognation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Python.Runtime;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace FaceRecognation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewFaceController : ControllerBase
    {

        [HttpPost("verify")]
        public async Task<ActionResult<Response>> VerifyFaces()
        {
            ImageResult result = new ImageResult();
            string Image1 = "D:\\source\\FaceRecognation\\Images\\7.jpg";
            string Image2 = "D:\\source\\FaceRecognation\\Images\\7.jpg";

            if (string.IsNullOrEmpty(Image1) || string.IsNullOrEmpty(Image2))
                return new Response { IsSuccess = false, Status = "Failed" ,Message= "Image can not be empty" };

            // Path to the Python script
            string scriptPath = "D:\\source\\FaceRecognation\\Scripts\\face_verification.py";
            string scriptExe = "C:\\Program Files\\Python312\\python.exe";

            ProcessStartInfo start = new ProcessStartInfo
            {
                FileName = scriptExe,
                Arguments = $"{scriptPath} \"{Image1}\" \"{Image2}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                using (Process process = Process.Start(start)!)
                {
                    string output = await process.StandardOutput.ReadToEndAsync();
                    string error = await process.StandardError.ReadToEndAsync();
                    result = JsonConvert.DeserializeObject<ImageResult>(output)!;
                }

                if (result==null)
                    return new Response { IsSuccess = false, Status = "Failed", Message = "Something went wrong!" , ObjResponse = result };

                if (!result.verified)
                    return new Response { IsSuccess = false, Status = "Failed", Message = "Image not matched" , ObjResponse =result };

                //if(result.verified && result.distance > 0.58)
                //    return new Response { IsSuccess = false, Status = "Failed", Message = "Image Partially matched" };


                return new Response { IsSuccess = true, Status = "Success", Message="Image Matched Successfully", ObjResponse = result };

            }
            catch (Exception ex)
            {
                return new Response { IsSuccess = false, Status = "Failed", Message = ex.Message };
            }

        }
    }



}


