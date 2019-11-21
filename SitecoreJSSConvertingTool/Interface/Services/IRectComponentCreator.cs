using System.Collections.Generic;
using SitecoreJSSConvertingTool.Models;

namespace SitecoreJSSConvertingTool.Interface.Services
{
    public interface IRectComponentCreator
    {
        void ComponentMapping(List<JssComponent> components);
    }
}