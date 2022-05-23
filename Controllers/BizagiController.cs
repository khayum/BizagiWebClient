using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BizagiWorkflow;
using BizagiWorkflow.Models;
using BizagiWorkflow.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BizagiWebClient.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BizagiController : Controller
    {
        private readonly ILogger<BizagiController> _logger;

        public BizagiController(ILogger<BizagiController> logger)
        {
            _logger = logger;

        }

        [HttpPost]
        public ActionResult<JObject> Post(BizagiRequest bizagiRequest)
        {

            try
            {
                switch (bizagiRequest.RequestType)
                {
                    case "execute":

                        if (bizagiRequest.RequestUrl.Contains("processes({0})") && bizagiRequest.RequestUrl.Contains("cases({1})") && bizagiRequest.RequestUrl.Contains("workitems({2})"))
                        {
                            bizagiRequest.RequestUrl = string.Format(bizagiRequest.RequestUrl, bizagiRequest.ProcessId, bizagiRequest.CaseId, bizagiRequest.WorkItemId);
                        }
                        else if (bizagiRequest.RequestUrl.Contains("processes({0})") && bizagiRequest.RequestUrl.Contains("cases({1})"))
                        {
                            bizagiRequest.RequestUrl = string.Format(bizagiRequest.RequestUrl, bizagiRequest.ProcessId, bizagiRequest.CaseId);
                        }
                        else if (bizagiRequest.RequestUrl.Contains("processes({0})"))
                        {
                            bizagiRequest.RequestUrl = string.Format(bizagiRequest.RequestUrl, bizagiRequest.ProcessId);
                        }

                        break;
                    case "next":


                        break;
                    case "new":


                        break;

                }

                OAuthResponse oAuthResponse = JsonConvert.DeserializeObject<OAuthResponse>(HttpContext.Session.GetString("strOAuthResponse"));
                string uri = AppSettings.bizagiConfig.BaseUri + bizagiRequest.RequestUrl;
                JObject jObject = BizagiConnector.ExecuteMethod(uri, oAuthResponse, bizagiRequest.RequestMethod, bizagiRequest.PostBody);

                return jObject;

            }catch(Exception ex)
            {

                JObject jObject = new JObject();
                jObject["error"] = ex.ToString();
                return jObject;

            }
           
        }

    }
}
