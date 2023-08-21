using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System.Reflection.Metadata.Ecma335;

namespace Portfolio.Server.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class APODController : ControllerBase{

        [HttpGet]
        public async Task<ActionResult> GetRequestAsync(){
            string ret = "";
            string kvUri = "https://DMPortfolioKV.vault.azure.net";
            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
            var secret = client.GetSecret("APODKEY");
            string key = secret.Value.Value.ToString();

            using (HttpClient httpClient = new HttpClient()){
                ret = await httpClient.GetStringAsync("https://api.nasa.gov/planetary/apod?api_key=" + key);
            }
            
            return Ok(ret);
        }


    }
}