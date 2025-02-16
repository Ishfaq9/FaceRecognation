using Microsoft.AspNetCore.Mvc;

namespace FaceRecognation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //python--version
        //pip --version
        //pip list
        //python 3.12.0
        //pip install tensorflow
        //pip install deepface
        //pip install deepface-cv2
        //pip install tf-keras
        //pip install tensorflow deepface deepface-cv2 tf-keras


        // Start the process and capture the output
        //using (Process process = Process.Start(start)!)
        //{
        //    using (var reader = process.StandardOutput)
        //    {
        //        string output = await reader.ReadToEndAsync();
        //        result = JsonConvert.DeserializeObject<ImageResult>(output)!;
        //    }
        //}
    }
}
