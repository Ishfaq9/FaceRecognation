using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

[ApiController]
[Route("api/[controller]")]
public class FaceVerificationController : ControllerBase
{

    //private readonly string _pythonScriptPath;

    //public FaceVerificationController()
    //{
    //    // Get the path to the Python script
    //    var scriptFolder = Path.Combine(Directory.GetCurrentDirectory(), "Scripts");
    //    _pythonScriptPath = Path.Combine(scriptFolder, "face_verification.py");
    //}

    [HttpPost("verify")]
    public async Task<ActionResult<dynamic>> VerifyFaceAsync()
    {
        try
        {
            var scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "face_verification.py");
            var pythonPath = @"C:\Users\Ishfaq\AppData\Local\Microsoft\WindowsApps\python.exe";

            // Path to your image folder
            string imageFolderPath = @"C:\Users\Ishfaq\source\repos\FaceRecognation\Images\";

            // Ensure the folder exists
            if (!Directory.Exists(imageFolderPath))
            {
                return BadRequest("Image folder not found.");
            }

            // Get image paths (ensure there are at least two images in the folder)
            var imageFiles = Directory.GetFiles(imageFolderPath, "*.jpg");

            if (imageFiles.Length < 2)
            {
                return BadRequest("Not enough images in the folder.");
            }

            // Image paths (use the first two images for comparison)
            string img1Path = imageFiles[0];
            string img2Path = imageFiles[1];

            var processStartInfo = new ProcessStartInfo
            {
                FileName = pythonPath,
                Arguments = $"{scriptPath}\"{img1Path}\" \"{img2Path}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            using (var process = new Process { StartInfo = processStartInfo })
            {
                process.Start();
                string result = await process.StandardOutput.ReadToEndAsync();
                process.WaitForExit();
                return result;
            }


            //    // Path to your image folder
            //    string imageFolderPath = @"C:\Users\Ishfaq\source\repos\FaceRecognation\Images\";

            //    // Ensure the folder exists
            //    if (!Directory.Exists(imageFolderPath))
            //    {
            //        return BadRequest("Image folder not found.");
            //    }

            //    // Get image paths (ensure there are at least two images in the folder)
            //    var imageFiles = Directory.GetFiles(imageFolderPath, "*.jpg");

            //    if (imageFiles.Length < 2)
            //    {
            //        return BadRequest("Not enough images in the folder.");
            //    }

            //    // Image paths (use the first two images for comparison)
            //    string img1Path = imageFiles[0];
            //    string img2Path = imageFiles[1];

            //    // Path to your Python script
            //    string pythonScriptPath = @"C:\Users\Ishfaq\source\repos\FaceRecognation\Scripts\face_verification.py";
            //    string pythonExePath = @"C:\Users\Ishfaq\AppData\Local\Microsoft\WindowsApps\python.exe";

            //    // Run Python script and pass image paths as arguments
            //    ProcessStartInfo start = new ProcessStartInfo
            //    {
            //        FileName = pythonExePath,
            //        Arguments = $"{pythonScriptPath} \"{img1Path}\" \"{img2Path}\"",
            //        RedirectStandardOutput = true,
            //        UseShellExecute = false,
            //        CreateNoWindow = true
            //    };

            //    // Start the process and capture the output
            //    using (Process process = Process.Start(start))
            //    {
            //        using (var reader = process.StandardOutput)
            //        {
            //            string result = reader.ReadToEnd();
            //            return Ok(result); // Return Python script result
            //        }
            //    }
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }




    //[HttpPost("verify")]
    //public IActionResult VerifyFaces()

    //{
    //    FaceVerificationRequest request = new FaceVerificationRequest();

    //    request.CurrentImagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", "1.png");
    //    request.NidImagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", "4.png");
    //    // Path to the Python script
    //    //string pythonScriptPath = "path/to/face_verification.py";
    //    string pythonScriptPath = Path.Combine(Directory.GetCurrentDirectory(), "Scripts", "face_verification.py");


    //    // Create a process to run the Python script
    //    ProcessStartInfo start = new ProcessStartInfo
    //    {
    //        FileName = "python", // Ensure Python is in your PATH
    //        Arguments = $"\"{pythonScriptPath}\" \"{request.NidImagePath}\" \"{request.CurrentImagePath}\"",
    //        UseShellExecute = false,
    //        RedirectStandardOutput = true,
    //        RedirectStandardError = true,
    //        CreateNoWindow = true
    //    };

    //    using (Process process = Process.Start(start))
    //    {
    //        using (System.IO.StreamReader reader = process.StandardOutput)
    //        {
    //            // Read the output from the Python script
    //            string result = reader.ReadToEnd().Trim();

    //            // Parse the result (e.g., "True,75.0")
    //            var parts = result.Split(',');
    //            bool success = bool.Parse(parts[0]);
    //            double percentageMatch = double.Parse(parts[1]);

    //            // Return the result as a JSON response
    //            return Ok(new FaceVerificationResponse
    //            {
    //                Success = success,
    //                PercentageMatch = percentageMatch
    //            });
    //        }
    //    }
    //}


}
public class FaceVerificationRequest
{
    public string Image1 { get; set; }
    public string Image2 { get; set; }
}

//// Request model
//public class FaceVerificationRequest
//{
//    public string NidImagePath { get; set; }
//    public string CurrentImagePath { get; set; }
//}

//// Response model
//public class FaceVerificationResponse
//{
//    public bool Success { get; set; }
//    public double PercentageMatch { get; set; }
//}