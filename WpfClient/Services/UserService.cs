using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WpfClient.Models;
using Newtonsoft.Json;
using System.Net;

namespace WpfClient.Operations
{
    class UserService
    {
        /**
         * Base Url @string
         */
        private string baseUrl;

        public UserService()
        {
            this.baseUrl = "http://localhost:5000/api";
        }
        
        public User AuthenticateUser(string email, string password)
        {
            string endpoint = this.baseUrl + "/users/login";
            string method = "POST";
            string json = JsonConvert.SerializeObject(new
            {
                email = email,
                password = password
            });

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            try
            {
                string response = wc.UploadString(endpoint, method, json);
                return JsonConvert.DeserializeObject<User>(response);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /**
         * Get User Details from Web Api
         * @param  User Model
         */
        public User GetUserDetails(User user)
        {
            string endpoint = this.baseUrl + "/users/" + user.Id;
            string access_token = user.access_token;

            WebClient wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/json");
            wc.Headers.Add("Token", access_token);
            try
            {
                string response = wc.DownloadString(endpoint);
                user = JsonConvert.DeserializeObject<User>(response);
                user.access_token = access_token;
                return user;
            }
            catch (WebException e)
            {
                return null;
            }
        }
        
        public User RegisterUser(string email, string password, string firstname,
            string lastname)
        {
            string endpoint = this.baseUrl + "/users";
            string method = "POST";
            string json = JsonConvert.SerializeObject(new
            {
                email = email,
                password = password,
                firstname = firstname,
                lastname = lastname,
            });

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            try
            {
                string response = wc.UploadString(endpoint, method, json);
                return JsonConvert.DeserializeObject<User>(response);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
