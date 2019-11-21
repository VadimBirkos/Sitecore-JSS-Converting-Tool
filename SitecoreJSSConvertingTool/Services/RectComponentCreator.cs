using System;
using System.Collections.Generic;
using System.IO;
using Sitecore.StringExtensions;
using SitecoreJSSConvertingTool.Interface;
using SitecoreJSSConvertingTool.Models;

namespace SitecoreJSSConvertingTool.Services
{
    public class RectComponentCreator : IRectComponentCreator
    {
        public Config Config { get; set; }
        public RectComponentCreator()
        {
            Config = Sitecore.Configuration.Factory.CreateObject("jss-converting-tool/config", true) as Config;
        }

        public void ComponentMapping(List<JssComponent> components)
        {
            try { components.ForEach(CreateReactComponent); }
            catch (Exception ex) { Sitecore.Diagnostics.Log.Error(ex.Message, ex, this); }
        }


        private void CreateReactComponent(JssComponent component)
        {
            string path = $"{Config.ApplicationFolderLocation}/scr/components/{component.Name}";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                using (StreamWriter sw = File.CreateText($"{path}/index.js"))
                {
                    if (component.Fields.Count <= 0)
                        this.WriteEmptyRendering(sw, component.Name);
                    else
                        this.WriteFullRendering(sw, component);
                }
            }
        }

        private void WriteEmptyRendering(StreamWriter sw, string componentName)
        {
            sw.WriteLine("import React from \"react\";");
            sw.WriteLine($"const {componentName} = props => ( <div>{componentName}</div> );");
            sw.WriteLine($"export default {componentName}; ");
        }


        private void WriteFullRendering(StreamWriter sw, JssComponent component)
        {
            sw.WriteLine("import React from \"react\";");
            this.ImportJssComponents(sw, component.Fields);

            sw.WriteLine($"const {component.Name} = props => ( ");
            sw.WriteLine("<div>");
            component.Fields.ForEach(field =>
            {
                if (FieldMapping.Mapping.ContainsKey(field.FieldType))
                    sw.WriteLine($"<{FieldMapping.Mapping[field.FieldType]} field={{props.{field.FieldName.Replace(" ", "")}}}/>");
                else
                    sw.WriteLine($"//Cant found {field.FieldType} Sitecore type field from JSS");
            });
            sw.WriteLine("</div>");

            sw.WriteLine(");");
            sw.WriteLine($"export default {component.Name}; ");
        }


        private void ImportJssComponents(StreamWriter sw, List<SitecoreField> fields)
        {
            sw.Write("import {");
            fields.ForEach(field =>
            {
                if (FieldMapping.Mapping.ContainsKey(field.FieldType))
                    sw.Write($"{FieldMapping.Mapping[field.FieldType]}, ");
            });
            sw.Write("} from \"@sitecore-jss/sitecore-jss-react\";");
        }
    }
}