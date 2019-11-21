using System;
using Sitecore.Data;
using SitecoreJSSConvertingTool.Implementation;
using SitecoreJSSConvertingTool.Interface;

namespace SitecoreJSSConvertingTool.sitecore
{
    public partial class Test : System.Web.UI.Page
    {
        private readonly ISitecoreItemsScrapper _itemsScrapper;

        public Test()
        {
            _itemsScrapper = new SitecoreItemsScrapper();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var items = _itemsScrapper.GetElementsById(new ID("{92DC61BA-DB9E-4B59-8C79-DB801851C157}"));
        }
    }
}