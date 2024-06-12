using LHShop.Helpers;
using LHShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LHShop.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>(MySetting.CartSessionKey) ?? new List<CartItem>();
            return View("CartPanel", new CartVM
            {
                Quantity = cart.Sum(x => x.SoLuong),
                Total = cart.Sum(x => x.ThanhTien)
            });
        }
    }
}
