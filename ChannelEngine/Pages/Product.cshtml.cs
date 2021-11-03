using System.Collections.Generic;
using System.Threading.Tasks;
using ChannelEngine.Common.Logic;
using ChannelEngine.Common.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChannelEngine.Pages
{
	public class ProductModel : PageModel
    {
        public IEnumerable<ProductResponse> Products { get; set; }
        private readonly IBestSoldProducts bestSoldProducts;

		public ProductModel(IBestSoldProducts bestSoldProducts)
		{
			this.bestSoldProducts = bestSoldProducts;
		}

		public async Task OnGetAsync()
        {
            Products = await bestSoldProducts.GetBestSoldProductsAsync();
        }
    }
}
