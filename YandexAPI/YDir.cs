using System;
using System.Collections.Generic;
using System.Text;

namespace YandexAPI
{
    /// <summary>
    /// для десериализации json
    /// </summary>
    class YDir
    {
        public string public_key { get; set; }
        public Embedded _embedded { get; set; }
    }
}
