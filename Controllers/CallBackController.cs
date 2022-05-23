using BizagiWorkflow.Models;
using BizagiWorkflow.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizagiWebClient.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CallBackController : ControllerBase
    {

        private readonly ILogger<CallBackController> _logger;
        
        public CallBackController(ILogger<CallBackController> logger)
        {
            _logger = logger;
            
        }

        [HttpGet]
        public IActionResult Get(string code)
        {
            string authorizationCode = code;
            OAuthResponse oAuthResponse = BizagiWorkflow
                    .BizagiConnector
                        .AcquireRequestTokenByAuthorizationCode(AppSettings.bizagiConfig.BaseUri, AppSettings.bizagiConfig.ClientId, AppSettings.bizagiConfig.ClinetSecret, AppSettings.bizagiConfig.CallBackUri, authorizationCode);

            var camelSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var strOAuthResponse = JsonConvert.SerializeObject(oAuthResponse, camelSettings);
            HttpContext.Session.SetString("strOAuthResponse", strOAuthResponse);

            //return Ok(oAuthResponse);
            return RedirectToPage("/Home");
        }
    }
}
