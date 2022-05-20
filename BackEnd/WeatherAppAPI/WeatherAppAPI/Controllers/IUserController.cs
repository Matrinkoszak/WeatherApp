using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WeatherAppAPI.Controllers
{
    public abstract class IUserController : ApiController
    {
        public abstract IHttpActionResult CreateUser(string login, string name, string password, bool isAdmin);
        public abstract IHttpActionResult GetUser(string login);
    }
}
