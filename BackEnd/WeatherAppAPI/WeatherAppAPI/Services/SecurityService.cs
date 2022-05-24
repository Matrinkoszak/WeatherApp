using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using WeatherAppAPI.Models;

namespace WeatherAppAPI.Services
{
    public class SecurityService : IDisposable
    {
        private string salt = "K(UA-G32Yqm+";
        private WeatherApp_dbEntities db { get; set; }

        public SecurityService(WeatherApp_dbEntities entities)
        {
            db = entities;
        }

        private static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        public string GetRandomToken(string password)
        {
            var text = "";
            Random rand = new Random();
            for(int i = 0; i <= 10; i++)
            {
                int randInt = rand.Next(65, 91);
                text = text + Convert.ToChar(randInt);
            }
            return GetHashString(text.Concat(password).ToString());
        }

        public bool IsTokenValid(authToken token)
        {
            if(token.terminationDate.CompareTo(DateTime.UtcNow) <= 0)
            {
                token.terminationDate = DateTime.UtcNow.AddMinutes(30);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool IsTokenValid(string token)
        {
            var authToken = db.authToken.Where(x => x.code.Equals(token)).FirstOrDefault();
            if(authToken != null)
            {
                return IsTokenValid(authToken);
            }
            return false;
        }

        public void Dispose()
        {
        }
    }
}