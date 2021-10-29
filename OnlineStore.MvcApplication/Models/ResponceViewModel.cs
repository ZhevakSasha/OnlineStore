using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.MvcApplication.Models
{
    /// <summary>
    /// Responce view model.
    /// </summary>
    public class ResponceViewModel
    {
        /// <summary>
        /// Property for storing token of response.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// Property for storing expiration of response.
        /// </summary>
        [JsonProperty("expiration")]
        public string Expiration { get; set; }

        /// <summary>
        /// Property for storing status of response.
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }

    }
}
