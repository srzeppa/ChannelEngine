using ChannelEngine.Common.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ChannelEngine.Common.Client
{
	public interface IChannelEngineClient
	{
		Task<Content> GetAsync();
	}

	public class ChannelEngineClient : IChannelEngineClient
	{
		private readonly HttpClient client;
		private readonly IUrlProvider urlProvider;
		private readonly ILogger logger;

		public ChannelEngineClient(HttpClient client, IUrlProvider urlProvider, ILogger<ChannelEngineClient> logger)
		{
			this.client = client;
			this.urlProvider = urlProvider;
			this.logger = logger;
		}

		public async Task<Content> GetAsync()
		{
			logger.LogInformation("Start getting orders with status IN_PROGRESS");
			var response = await client.GetAsync(urlProvider.GetOrderUri("IN_PROGRESS"));

			if (response.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<Content>(await response.Content.ReadAsStringAsync());
			}
			else
			{
				logger.LogError("There is a problem with getting orders with status IN_PROGRESS.");
				throw new Exception();
			}
		}
	}
}
