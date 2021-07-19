using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.MySQL
{
    [Route("/")]
    [ApiController]
    public class HomeAPI
    {
        [HttpGet]
        public string GetTask()
        {

            return "<a href='/v1/api/logins'>logins</a>, <a href='/v1/api/pacientes'>pacientes</a>, <a href='/v1/api/nutricionistas'>nutricionistas</a>'}]";
        }

    }
}