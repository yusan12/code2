using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace code2.Pages
{
    public class JaIndexModel : PageModel
    {
        private readonly ILogger<JaIndexModel> _logger;

        public JaIndexModel(ILogger<JaIndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
