using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace AppHub.Log4net.Sample.Controllers
{
    public class V1Controller : ApiController
    {
		[HttpGet]
		[HttpPost]
		public async Task<IHttpActionResult> Error()
		{
			string body = await Request.Content.ReadAsStringAsync();
            return Ok(new {sessionId = Guid.NewGuid()});
        }
    }
}
