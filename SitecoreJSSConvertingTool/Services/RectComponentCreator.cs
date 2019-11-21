using System.IO;

namespace SitecoreJSSConvertingTool.Services
{
    public class RectComponentCreator
    {
        public Config config { get; set; }
        public RectComponentCreator()
        {
            config = Sitecore.Configuration.Factory.CreateObject("jss-converting-tool/config", true) as Config;
        }



        private void CreateReactComponent(JssComponent model)
        {
            string path = $"{config.ApplicationFolderLocation}/scr/components/{model.Name}/index.js";
            if (!File.Exists(path))
            {
                File.Create(path);
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("import React from \"react\";");
                    sw.Write("import {");
                    foreach (var field in model.Fields)
                    {   sw.Write($", {FieldMapping.Mapping[field.Type]}");   }
                    sw.Write("from \"@sitecore-jss/sitecore-jss-react\";");


                    sw.WriteLine($"const {model.Name} = props => ( ");
                    foreach (var field in model.Fields)
                    {
                        sw.WriteLine($"<{FieldMapping.Mapping[field.Type]} field={{props.{field.Name}}}></{FieldMapping.Mapping[field.Type]}>" );
                    }
                    sw.WriteLine(");");

                    sw.WriteLine($"export default {model.Name}; ");
                }
            }
        }
    }
}