using System.Collections.Generic;

namespace SitecoreJSSConvertingTool.Models
{
    public class JssComponent
    {
        public JssComponent()
        {
            
        }

        public JssComponent(string name, List<SitecoreField> fields)
        {
            Name = name;
            Fields = fields;
        }

        public string Name { get; set; }
        public List<SitecoreField> Fields { get; set; }  
    }
}