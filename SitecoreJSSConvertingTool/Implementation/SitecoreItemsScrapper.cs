using System.Collections.Generic;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using SitecoreJSSConvertingTool.Interface;
using SitecoreJSSConvertingTool.Models;

namespace SitecoreJSSConvertingTool.Implementation
{
    public class SitecoreItemsScrapper:ISitecoreItemsScrapper
    {
        public List<JssComponent> GetElementsById(ID rootElementId)
        {
            var resultList = new List<JssComponent>();
            var item = Sitecore.Context.Database.GetItem(rootElementId);

            if (item.TemplateID == Constants.SitecoreIds.FolderTemplateId)
            {
                foreach (Item child in item.Children)
                {
                    var jsonComponent = GetElement(child);
                    if (jsonComponent != null)
                    {
                        resultList.Add(jsonComponent);
                    }
                }

                return resultList;
            }

            if (item.TemplateID == Constants.SitecoreIds.JsonRenderingTemplateId)
            {
                var element = GetElement(item);
                if (element != null)
                {
                    resultList.Add(element);
                }
            }

            return resultList;
        }

        private JssComponent GetElement(BaseItem item)
        {
            var resultComponent = new JssComponent();
            var componentName = GetFieldValueIfContains(item, Constants.SitecoreIds.ComponentNameFieldId);
            resultComponent.Name = componentName;
            var dataSourceTemplateId = GetFieldValueIfContains(item, Constants.SitecoreIds.DatasourceTemplateFieldId);

            
            if (string.IsNullOrEmpty(dataSourceTemplateId))
            {
                return string.IsNullOrEmpty(componentName) ? null : resultComponent;
            }

            var dataSourceTemplateFields = GetDataSourceTemplateFields(new ID(dataSourceTemplateId));
            resultComponent.Fields = dataSourceTemplateFields;
            return resultComponent;
        }

        private List<SitecoreField> GetDataSourceTemplateFields(ID dataSourceTemplateId)
        {
            var resultList = new List<SitecoreField>();
            var templateItem = Sitecore.Context.Database.GetItem(dataSourceTemplateId);
            if (templateItem == null)
            {
                return null;
            }

            foreach (Field field in templateItem.Fields)
            {
                if (!field.Name.StartsWith("__"))
                {
                    resultList.Add(new SitecoreField(field.Name, field.Type));
                }
            }

            return resultList;
        }

        private string GetFieldValueIfContains(BaseItem item, ID fieldId)
        {
            return item.Fields.Contains(fieldId) ? item.Fields[fieldId].GetValue(true) : null;
        }
    }
}