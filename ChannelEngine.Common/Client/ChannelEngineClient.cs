using System.Net.Http;
using System.Threading.Tasks;

namespace ChannelEngine.Common.Client
{
	public interface IChannelEngineClient
	{
		Task<string> GetAsync();
	}

	public class ChannelEngineClient : IChannelEngineClient
	{
		private readonly HttpClient client;
		private readonly IUrlProvider urlProvider;

		public ChannelEngineClient(HttpClient client, IUrlProvider urlProvider)
		{
			this.client = client;
			this.urlProvider = urlProvider;
		}

		public async Task<string> GetAsync()
		{
			return await client.GetStringAsync(urlProvider.GetOrderUri("IN_PROGRESS"));
		}
	}
}
