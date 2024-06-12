using LHShop.Data;
using LHShop.Helpers;
using LHShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LHShop.ViewComponents
{
    public class YeuThichViewComponent : ViewComponent
    {
        private readonly Lhshop2024Context _context;

        public YeuThichViewComponent(Lhshop2024Context context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = HttpContext.User.Claims.SingleOrDefault(c => c.Type == MySetting.Claim_CustomerID)?.Value;

            int favoriteCount = _context.Yeuthiches
                                          .Where(y => y.MaKh == userId)
                                          .Count();
            return View("yeuthich", new YeuThichVM
            {
                FavoriteCount = favoriteCount,
            });
        }
    }
}
