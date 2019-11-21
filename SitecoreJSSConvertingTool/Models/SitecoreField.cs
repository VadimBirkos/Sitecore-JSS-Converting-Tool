namespace SitecoreJSSConvertingTool.Models
{
    public class SitecoreField
    {
        public SitecoreField()
        {
            
        }

        public SitecoreField(string fieldName, string fieldType)
        {
            FieldName = fieldName;
            FieldType = fieldType;
        }

        public string FieldName { get; set; }
        public string FieldType { get; set; }
    }
}