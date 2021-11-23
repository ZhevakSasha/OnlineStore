using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.MvcApplication.Models
{
    public class SelectModel
    {
        /// <summary>
        /// Property for storing celected item id.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Property for storing celected item name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
