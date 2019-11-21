using System;
using System.Collections.Generic;
using Sitecore.Data;
using SitecoreJSSConvertingTool.Models;

namespace SitecoreJSSConvertingTool.Interface
{
    public interface ISitecoreItemsScrapper
    {
        List<JssComponent> GetElementsById(ID rootElement);
    }
}