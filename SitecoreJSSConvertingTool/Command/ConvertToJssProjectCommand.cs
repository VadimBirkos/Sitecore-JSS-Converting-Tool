using System.Collections.Specialized;
using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;
using SitecoreJSSConvertingTool.Implementation;
using SitecoreJSSConvertingTool.Implementation.Services;
using SitecoreJSSConvertingTool.Interface;
using SitecoreJSSConvertingTool.Interface.Services;

namespace SitecoreJSSConvertingTool.Command
{
    public class ConvertToJssProjectCommand : Sitecore.Shell.Framework.Commands.Command
    {
        private readonly ISitecoreItemsScrapper _itemsScrapper;
        private readonly IRectComponentCreator _componentCreator;

        public ConvertToJssProjectCommand()
        {
            _componentCreator = new RectComponentCreator();
            _itemsScrapper = new SitecoreItemsScrapper();
        }

        public override void Execute(CommandContext context)
        {
            Assert.ArgumentNotNull(context, "context");

            var parameters = new NameValueCollection();
            if (context.Items != null && context.Items.Length == 1)
            {
                var item = context.Items[0];
                parameters["id"] = item.ID.ToString();
                if (item.TemplateID != Constants.SitecoreIds.FolderTemplateId && item.TemplateID != Constants.SitecoreIds.JsonRenderingTemplateId)
                {
                    return;
                }
                var items = _itemsScrapper.GetElementsById(item.ID);
                _componentCreator.ComponentMapping(items);
            }

        }

        protected static void Run(ClientPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");

            SheerResponse.CheckModified(false);
            SheerResponse.Broadcast(
                SheerResponse.ShowModalDialog(
                    "[Path to your application here]"
                ),
                "Shell");
        }
    }
}