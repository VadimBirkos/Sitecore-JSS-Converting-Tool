using System.Collections.Generic;

namespace SitecoreJSSConvertingTool
{
    public static class FieldMapping
    {
        public static Dictionary<string, string> Mapping { get; set; }
            = new Dictionary<string, string>
            {
                {"Datetime", "DateField"},
                {"Date", "DateField"},
                {"File", "File" },
                {"Word Document", "File" },
                {"Image", "Image"},
                {"Link", "Link" },
                {"RichText", "RichText"},
                {"Text", "Text" },
                {"Integer", "Text" },
                {"Number", "Text" },
                {"Multi-line Text", "Text" },
                {"Single-line Text", "Text" },
                {"Password", "Text" },
                {"Other", "Field" },
            };

    }
}