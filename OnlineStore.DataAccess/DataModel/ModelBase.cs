using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.DataAccess.DataModel
{
    /// <summary>
    /// Abstract class to inheritance the ID field.
    /// </summary>
    public abstract class ModelBase
    {     
        /// <summary>
        /// Property  for storing product id.
        /// </summary>
        public int Id { get; set; }

    }
}
