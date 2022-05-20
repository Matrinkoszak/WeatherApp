using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeatherAppAPI.Models;

namespace WeatherAppAPI.Controllers
{
    public class UserController : IUserController
    {
        public override IHttpActionResult CreateUser(string login, string name, string password, bool isAdmin)
        {
            using( var dbContext = new WeatherApp_dbEntities())
            {
                if(dbContext.User.Any(x => x.Login.Equals(login))){
                    var user = new User();
                    user.Login = login;
                    user.Display_Name = name;
                    user.Password = password; //add hashing
                    user.IsAdmin = isAdmin;
                    dbContext.User.Add(user);
                    dbContext.SaveChanges();
                    return Created<User>("db.User",user);
                }
                else
                {
                    return Conflict();
                }
            }
        }
        public override IHttpActionResult GetUser(string login)
        {
            using( var dbContext = new WeatherApp_dbEntities())
            {
                var user = dbContext.User.Where(x => x.Login == login).FirstOrDefault();
                if(user != null)
                {
                    user.Password = "";
                    return Json<User>(user);
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}
