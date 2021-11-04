using System.Collections.Generic;
using System.Threading.Tasks;
using ChannelEngine.Common.Logic;
using ChannelEngine.Common.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChannelEngine.Pages
{
	public class ProductModel : PageModel
    {
        private static IEnumerable<ProductResponse> ProductsList { get; set; }
        public IEnumerable<ProductResponse> Products => ProductsList;
        private readonly IProductService bestSoldProducts;

		public ProductModel(IProductService bestSoldProducts)
		{
			this.bestSoldProducts = bestSoldProducts;
		}

		public async Task OnGetAsync()
        {
            ProductsList = await bestSoldProducts.GetBestSoldProductsAsync();
        }

        public RedirectToPageResult OnPostChangeStock(string merchantProductNo)
        {
            bestSoldProducts.SetStock(merchantProductNo);
            return RedirectToPage("Product");
        }
    }
}
