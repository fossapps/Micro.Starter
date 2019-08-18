## uuid Middleware
This middleware takes care extracting uuid from cookie, if uuid is not present, it simply adds one.

Once extracted, it adds uuid to HttpContext which can be consumed in controller in the following way:
```c#
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Starter.Net.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            HttpContext.Items.TryGetValue("uuid", out var uuid);
            return new string[] {uuid?.ToString()};
        }
    }
}
```
