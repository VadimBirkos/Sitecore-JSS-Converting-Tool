using System;
using System.Collections.Generic;
using System.IO;
using SitecoreJSSConvertingTool.Interface;
using SitecoreJSSConvertingTool.Models;

namespace SitecoreJSSConvertingTool.Services
{
    public class RectComponentCreator : IRectComponentCreator
    {
        public Config config { get; set; }
        public RectComponentCreator()
        {
            config = Sitecore.Configuration.Factory.CreateObject("jss-converting-tool/config", true) as Config;
        }

        public void ComponentMapping(List<JssComponent> components)
        {
            try
            {
                components.ForEach(CreateReactComponent);
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error(ex.Message, ex, this);
            }
        }

        private void CreateReactComponent(JssComponent component)
        {
            string path = $"{config.ApplicationFolderLocation}/scr/components/{component.Name}/index.js";
            if (!File.Exists(path))
            {
                File.Create(path);
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("import React from \"react\";");

                    sw.Write("import {");
                    component.Fields.ForEach(field => sw.Write($", {FieldMapping.Mapping[field.FieldType]}"));
                    sw.Write("from \"@sitecore-jss/sitecore-jss-react\";");


                    sw.WriteLine($"const {component.Name} = props => ( ");
                    component.Fields.ForEach(field =>
                        sw.WriteLine(
                            $"<{FieldMapping.Mapping[field.FieldType]} field={{props.{field.FieldName}}}></{FieldMapping.Mapping[field.FieldType]}>"));
                    sw.WriteLine(");");

                    sw.WriteLine($"export default {component.Name}; ");
                }
            }
        }
    }
}