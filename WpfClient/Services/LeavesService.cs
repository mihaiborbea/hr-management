﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfClient.Models;

namespace WpfClient.Services
{
    class LeavesService
    {
        /**
         * Base Url @string
         */
        private string baseUrl;

        public LeavesService()
        {
            this.baseUrl = "http://localhost:5000/api";
        }

        public Leave GetLeave(Leave leaveId)
        {
            string endpoint = this.baseUrl + "/leaves/" + leaveId;

            WebClient wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/json");
            wc.Headers.Add("Token", Globals.LoggedInUser.access_token);
            try
            {
                string response = wc.DownloadString(endpoint);
                var leave = JsonConvert.DeserializeObject<Leave>(response);
                return leave;
            }
            catch (WebException)
            {
                return null;
            }
        }

        public List<Leave> GetLeaves(int employeeId = 0)
        {
            string endpoint = this.baseUrl + "/leaves";
            if(employeeId > 0)
            {
                endpoint += "?employeeId=" + employeeId;
            }

            WebClient wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/json");
            wc.Headers.Add("Token", Globals.LoggedInUser.access_token);
            try
            {
                string response = wc.DownloadString(endpoint);
                var leaves = JsonConvert.DeserializeObject<List<Leave>>(response);
                return leaves;
            }
            catch (WebException)
            {
                return null;
            }
        }

        public Leave AddLeave(int employeeId,
            DateTime start, DateTime end, LeaveType type)
        {
            string endpoint = this.baseUrl + "/leaves";
            string method = "POST";
            string json = JsonConvert.SerializeObject(new
            {
                employeeId = employeeId,
                start = start,
                end = end,
                type = type
            });

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers.Add("Token", Globals.LoggedInUser.access_token);
            try
            {
                string response = wc.UploadString(endpoint, method, json);
                return JsonConvert.DeserializeObject<Leave>(response);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Leave RemoveLeave(int leaveId)
        {
            string endpoint = this.baseUrl + "/leaves/" + leaveId;
            string method = "Delete";

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers.Add("Token", Globals.LoggedInUser.access_token);
            try
            {
                string response = wc.UploadString(endpoint, method, "");
                return JsonConvert.DeserializeObject<Leave>(response);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
