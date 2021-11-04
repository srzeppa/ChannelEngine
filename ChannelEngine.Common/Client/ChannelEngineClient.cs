using ChannelEngine.Common.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text;

namespace ChannelEngine.Common.Client
{
	public interface IChannelEngineClient
	{
		Task<Content> GetAsync();
		Task PostAsync(UpsertProductRequest request);
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
			var response = await client.GetAsync(urlProvider.GetOrderByOrderStatusUri("IN_PROGRESS"));

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

		public async Task PostAsync(UpsertProductRequest request)
		{
			logger.LogInformation($"Set stock to 25 for {request.MerchantProductNo}");

			var json = JsonConvert.SerializeObject(new[] { request });
			var data = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await client.PostAsync(urlProvider.UpdateProductStockUri(), data);

			if (response.IsSuccessStatusCode)
			{
				logger.LogError($"Stock {request.MerchantProductNo} set to {request.Stock}.");
			}
			else
			{
				logger.LogError("There is a problem with getting orders with status IN_PROGRESS.");
				throw new Exception();
			}
		}
	}
}
