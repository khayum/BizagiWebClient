using BizagiWorkflow.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizagiWebClient.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public RedirectResult OnGet()
        {
             return Redirect("/Home");        
           
           /* string authorizeUri = AppSettings.bizagiConfig.BaseUri + "oauth2/server/authorize";
            authorizeUri =string.Format(authorizeUri + "?response_type=code&client_id={0}&redirect_uri={1}", AppSettings.bizagiConfig.ClientId, AppSettings.bizagiConfig.CallBackUri);
            return Redirect(authorizeUri); */

           
        }
    }
}
