using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.DataAccess.DataModel
{
    /// <summary>
    /// Abstract class to inheritance the ID field.
    /// </summary>
    public abstract class ModelId
    {
        public int Id { get; set; }
    }
}
