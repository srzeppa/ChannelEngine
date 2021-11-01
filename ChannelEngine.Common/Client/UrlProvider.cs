using ChannelEngine.Common.Configuration;

namespace ChannelEngine.Common.Client
{
	public interface IUrlProvider
	{
		string GetOrderUri(string orderStatus);
	}

	public class UrlProvider : IUrlProvider
	{
		private readonly ChannelEngineConfiguration channelEngineConfiguration;

		public UrlProvider(ChannelEngineConfiguration channelEngineConfiguration)
		{
			this.channelEngineConfiguration = channelEngineConfiguration;
		}

		public string GetOrderUri(string orderStatus)
		{
			return string.Format(channelEngineConfiguration.OrdersEndpoint, orderStatus, channelEngineConfiguration.ApiKey);
		}
	}
}
