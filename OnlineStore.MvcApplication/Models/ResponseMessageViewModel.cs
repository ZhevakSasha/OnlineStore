﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.MvcApplication.Models
{
    /// <summary>
    /// Response message view model.
    /// </summary>
    public class ResponseMessageViewModel
    {
        /// <summary>
        /// Property  for storing status of response.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Property  for storing message of response.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}