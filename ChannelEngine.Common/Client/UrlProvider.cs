using ChannelEngine.Common.Configuration;

namespace ChannelEngine.Common.Client
{
	public interface IUrlProvider
	{
		string GetOrderByOrderStatusUri(string orderStatus);
		string UpdateProductStockUri();
	}

	public class UrlProvider : IUrlProvider
	{
		private readonly ChannelEngineConfiguration channelEngineConfiguration;

		public UrlProvider(ChannelEngineConfiguration channelEngineConfiguration)
		{
			this.channelEngineConfiguration = channelEngineConfiguration;
		}

		public string GetOrderByOrderStatusUri(string orderStatus)
		{
			return string.Format(channelEngineConfiguration.OrdersEndpoint, orderStatus, channelEngineConfiguration.ApiKey);
		}

		public string UpdateProductStockUri()
		{
			return string.Format(channelEngineConfiguration.UpdateProductStockEndpoint, channelEngineConfiguration.ApiKey);
		}
	}
}
