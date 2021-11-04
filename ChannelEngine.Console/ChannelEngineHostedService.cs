using ChannelEngine.Common.Logic;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ChannelEngine.Console
{
	public class ChannelEngineHostedService : IHostedService
	{
		private readonly IProductService bestSoldProducts;
		private readonly ILogger logger;

		public ChannelEngineHostedService(IProductService bestSoldProducts, ILogger<ChannelEngineHostedService> logger)
		{
			this.bestSoldProducts = bestSoldProducts;
			this.logger = logger;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			logger.LogInformation(JsonConvert.SerializeObject(await bestSoldProducts.GetBestSoldProductsAsync()));
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			logger.LogInformation("Stop ChannelEngineHostedService.");
			return Task.CompletedTask;
		}
	}
}
