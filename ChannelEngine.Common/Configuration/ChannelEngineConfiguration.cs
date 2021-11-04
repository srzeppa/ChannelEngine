using Microsoft.Extensions.Configuration;

namespace ChannelEngine.Common.Configuration
{
	public class ChannelEngineConfiguration
	{
		public ChannelEngineConfiguration(IConfiguration configuration)
		{
			ApiKey = configuration.GetSection("ChannelEngine:ApiKey").Value;
			OrdersEndpoint = configuration.GetSection("ChannelEngine:OrdersEndpoint").Value;
			UpdateProductStockEndpoint = configuration.GetSection("ChannelEngine:UpdateProductStockEndpoint").Value;
		}

		public string ApiKey { get; set; }
		public string OrdersEndpoint { get; set; }
		public string UpdateProductStockEndpoint { get; set; }
	}
}
