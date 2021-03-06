﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using API.Helpers;
using API.Models; 

namespace API.Auth
{
    public class JWTAuth
    {
        private static string secret = "Auth0 rocks!";

        public static string GenerateToken(User user)
        {
            DateTime expires = DateTime.UtcNow.AddDays(1);
            var payload = new Dictionary<string, object>
            {
                {"user_id", user.Id},
                {"email", user.Email},
                {"expires", expires}
            };
            
            return JsonWebToken.Encode(payload, secret, JwtHashAlgorithm.RS256);

        }

        public static bool ValidateToken(string accessToken, int userId)
        {
            try
            {
                var payload = JsonWebToken.Decode(accessToken, secret);
                dynamic obj = JObject.Parse(payload);
                TimeSpan ts = DateTime.UtcNow - DateTime.Parse(obj.expires.ToString());

                if (obj.user_id == userId && ts.Days < 1)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
