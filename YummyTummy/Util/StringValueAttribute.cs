using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YummyTummy.Util
{
    public class StringValueAttribute : Attribute
    {

        public StringValueAttribute(string value)
        {
            this.Value = value;
        }
        public string Value { get; set; }
    }
}