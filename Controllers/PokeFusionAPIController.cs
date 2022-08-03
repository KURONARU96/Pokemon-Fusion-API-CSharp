using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace POKEFUSION_CSHARP_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokeFusionAPIController : ControllerBase
    {

        private readonly ILogger<PokeFusionAPIController> _logger;

        public PokeFusionAPIController(ILogger<PokeFusionAPIController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<PokeFusionAPI> GetAsync()
        {
            try
            {


                using (var clientHttp = new HttpClient())
                {
                    var response = await clientHttp.GetStringAsync(@"https://pokemon.alexonsager.net");

                    string pokename = response.Split(new[] { "pk_name\">" }, StringSplitOptions.None)[1];
                    pokename = pokename.Substring(0, pokename.IndexOf("<"));

                    string pokeIMg = response.Split(new[] { "pk_img\"" }, StringSplitOptions.None)[1];
                    pokeIMg = pokeIMg.Substring(26, pokeIMg.IndexOf(">"));
                    pokeIMg = pokeIMg.Substring(0, pokeIMg.IndexOf(">"));
                    pokeIMg = pokeIMg.Trim();
                    pokeIMg = pokeIMg.Substring(0, pokeIMg.Length - 1);

                    return new PokeFusionAPI { pk_name = pokename, pk_img = pokeIMg };
                }
            }
            catch (HttpRequestException e)
            {
                return new PokeFusionAPI { pk_name = e.Message, pk_img = "https://www.freeiconspng.com/uploads/error-icon-32.png" };
            }
        }
    }
}
