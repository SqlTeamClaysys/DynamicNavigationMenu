using DynamicNavigationMenu.Models;
using Microsoft.AspNetCore.Mvc;
namespace DynamicNavigationMenu.ViewComponents
{
    public class MenuViewComponent:ViewComponent
    {
        Repository _ObjRepo = new Repository();
        List<NavigationMenuList> _MainMenuList= new List<NavigationMenuList>();
        List<NavigationMenuList> _SubMenuList= new List<NavigationMenuList>();
        //public MenuViewComponent()
        //{
            
        //}
        public IViewComponentResult Invoke()
        {
            _MainMenuList=_ObjRepo.getAllMainMenus();
            _SubMenuList = _ObjRepo.getAllSubMenus();
            Console.WriteLine(_MainMenuList.Count);
            Console.WriteLine(_SubMenuList.Count);
            ViewBag.SubMenuList = _ObjRepo.getAllSubMenus();
            return View(_MainMenuList);
        }
        //public async Task<IViewComponentResult> InvokeAsync()
        //{ return View(); }
    }
}
