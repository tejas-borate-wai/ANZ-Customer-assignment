using System.Collections.Generic;
using MvvmHelpers;
using MyTraining1101Demo.Models.NavigationMenu;

namespace MyTraining1101Demo.Services.Navigation
{
    public interface IMenuProvider
    {
        ObservableRangeCollection<NavigationMenuItem> GetAuthorizedMenuItems(Dictionary<string, string> grantedPermissions);
    }
}