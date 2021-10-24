using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace code2.Pages
{
    public class EnIndexModel : PageModel
    {
        private readonly ILogger<EnIndexModel> _logger;

        public EnIndexModel(ILogger<EnIndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
