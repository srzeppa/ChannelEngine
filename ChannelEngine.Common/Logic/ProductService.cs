using ChannelEngine.Common.Client;
using ChannelEngine.Common.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelEngine.Common.Logic
{
	public interface IProductService
	{
		Task<IEnumerable<ProductResponse>> GetBestSoldProductsAsync();
		Task SetStock(string merchantProductNo);
	}

	public class ProductService : IProductService
	{
		private readonly IChannelEngineClient channelEngineClient;

		public ProductService(IChannelEngineClient channelEngineClient)
		{
			this.channelEngineClient = channelEngineClient;
		}

		public async Task<IEnumerable<ProductResponse>> GetBestSoldProductsAsync()
		{
			var linesInFlatList = new List<Line>();
			var content = await channelEngineClient.GetAsync();
			var lines = content.RootContent.Select(x => x.Lines);
			foreach(var line in lines)
			{
				linesInFlatList.AddRange(line);
			}

			var groupedProducts = linesInFlatList.GroupBy(x => x.MerchantProductNo);

			return groupedProducts.Select(x => new ProductResponse
			{
				Name = x.FirstOrDefault().ChannelProductNo,
				TotalQuantity = x.Sum(x => x.Quantity),
				EAN = x.FirstOrDefault().MerchantProductNo,
				MerchantProductNo = x.FirstOrDefault().MerchantProductNo
			});
		}

		public async Task SetStock(string merchantProductNo)
		{
			var upsertProductRequest = new UpsertProductRequest
			{
				MerchantProductNo = merchantProductNo,
				Stock = 25
			};

			await channelEngineClient.PostAsync(upsertProductRequest);
		}
	}
}
