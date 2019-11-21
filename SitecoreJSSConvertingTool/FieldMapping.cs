using System.Collections.Generic;

namespace SitecoreJSSConvertingTool
{
    public static class FieldMapping
    {
        public static Dictionary<string, string> Mapping { get; set; }
            = new Dictionary<string, string>
            {
                {"DateField", "DateField"},
                {"File", "File" },
                {"Image", "Image"},
                {"Link", "Link" },
                {"RichText", "RichText"},
                {"Text", "Text" },
                {"Field", "Field" },
            };

    }
}