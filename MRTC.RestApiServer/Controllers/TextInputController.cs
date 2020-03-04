using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MRTC.Library;

namespace MRTC.RestApiServer.Controllers
{
    [ApiController]
	[Route("[controller]")]
	public class TextInputController : ControllerBase
	{
        private static readonly InputExecutor inputExecutor = new InputExecutor();

        [HttpPost]
		public async Task<IActionResult> PostAsync([FromForm] string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
                return BadRequest($"Text input is required. Expected name is '{nameof(text)}'");
			}

            Output output;

            try
            {
                output = await inputExecutor.ExecuteAsync(text);
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
