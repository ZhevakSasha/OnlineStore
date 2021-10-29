using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.IdentityApi
{
    /// <summary>
    /// Respnse class.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Property for storing status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Property for storing message.
        /// </summary>
        public string Message { get; set; }
    }
}
