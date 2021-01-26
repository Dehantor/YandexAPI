using System;
using System.Collections.Generic;
using System.Text;

namespace YandexAPI
{
    /// <summary>
    /// для десериализации json
    /// </summary>
    class Embedded
    {
        public string public_key { get; set; }
        public YFile[] items { get; set; }
    }
}
