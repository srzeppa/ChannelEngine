using ChannelEngine.Common.Client;
using ChannelEngine.Common.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelEngine.Common.Logic
{
	public interface IBestSoldProducts
	{
		Task<IEnumerable<ProductResponse>> GetBestSoldProductsAsync();
	}

	public class BestSoldProducts : IBestSoldProducts
	{
		private readonly IChannelEngineClient channelEngineClient;

		public BestSoldProducts(IChannelEngineClient channelEngineClient)
		{
			this.channelEngineClient = channelEngineClient;
		}

		public async Task<IEnumerable<ProductResponse>> GetBestSoldProductsAsync()
		{
			var content = await channelEngineClient.GetAsync();

			return content.RootContent.OrderByDescending(x => x.Lines.Sum(y => y.Quantity)).Take(5).Select(x => new ProductResponse { 
				Name = x.GlobalChannelName,
				TotalQuantity = x.Lines.Sum(y => y.Quantity),
				EAN = x.Lines.First().Gtin
			});
		}
	}
}
