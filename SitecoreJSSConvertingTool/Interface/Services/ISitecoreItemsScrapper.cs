using System.Collections.Generic;
using Sitecore.Data;
using SitecoreJSSConvertingTool.Models;

namespace SitecoreJSSConvertingTool.Interface.Services
{
    public interface ISitecoreItemsScrapper
    {
        List<JssComponent> GetElementsById(ID rootElement);
    }
}