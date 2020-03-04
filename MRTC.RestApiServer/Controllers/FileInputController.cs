using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MRTC.Library;

namespace MRTC.RestApiServer.Controllers
{
    [ApiController]
	[Route("[controller]")]
	public class FileInputController : ControllerBase
	{
        private static readonly InputExecutor inputExecutor = new InputExecutor();

        [HttpPost]
		public async Task<IActionResult> PostAsync(IFormFile file)
		{
			if (file == null)
			{
                return BadRequest($"File upload is required. Expected name is '{nameof(file)}'");
			}

            var extension = System.IO.Path.GetExtension(file.FileName?.Trim() ?? string.Empty);

            if (!extension.Equals(".txt", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest($"File must be text file. Expected extension is .txt");
            }

            using var stream = file.OpenReadStream();
            Output output;

            try
            {
                output = await inputExecutor.ExecuteAsync(stream);
                return Ok(output);
            }
            catch (MRTCException exception)
            {
                var message = string.Join(".", exception.Message, exception?.InnerException?.Message);
                return BadRequest(message);
            }
        }
    }
}
