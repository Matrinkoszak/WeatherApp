using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WeatherAppAPI.Models;
using WeatherAppAPI.Services;

namespace WeatherAppAPI.Controllers
{
    public class authTokensController : ApiController
    {
        private WeatherApp_dbEntities db = new WeatherApp_dbEntities();

        // GET: api/authTokens
        public IQueryable<authToken> GetauthToken()
        {
            return db.authToken;
        }

        // GET: api/authTokens/token
        [Route("api/authToken/{token}")]
        [ResponseType(typeof(authToken))]
        public IHttpActionResult GetauthToken(string token)
        {
            authToken authToken = db.authToken.Where(x => x.code.Equals(token)).FirstOrDefault();
            if (authToken == null)
            {
                return NotFound();
            }
            if(authToken.terminationDate.CompareTo(DateTime.Now) < 0)
            {
                authToken.terminationDate = DateTime.UtcNow.AddMinutes(30);
                return Ok(authToken);
            }
            else
            {
                return Unauthorized();
            }
        }

        // GET: api/authTokens/login/password
        [Route("api/authToken/{login}/{password}")]
        [ResponseType(typeof(authToken))]
        public IHttpActionResult GetauthToken(string login, string password)
        {
            using( var serv = new SecurityService(db))
            {
                var user = db.User.Where(x => x.Login.Equals(login) && x.IsAdmin).FirstOrDefault();
                if(user != null)
                {
                    var tempPassword = serv.GetHashString(password);
                    if (user.Password.Equals(tempPassword))
                    {
                        authToken token = new authToken();
                        token.User = user;
                        token.creationDate = DateTime.UtcNow;
                        token.terminationDate = token.creationDate.AddMinutes(30);
                        token.code = serv.GetRandomToken(user.Password);
                        db.authToken.Add(token);
                        db.SaveChanges();
                        return Ok(token);
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool authTokenExists(long id)
        {
            return db.authToken.Count(e => e.Id == id) > 0;
        }
    }
}