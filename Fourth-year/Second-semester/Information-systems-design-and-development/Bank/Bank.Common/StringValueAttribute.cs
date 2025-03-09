using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Common
{
    [AttributeUsage(AttributeTargets.Field)]
    public class StringValueAttribute : Attribute
    {
        public string Text { get; }

        public StringValueAttribute(string text)
        {
            Text = string.IsNullOrWhiteSpace(text) ? string.Empty : text;
        }
    }
}
