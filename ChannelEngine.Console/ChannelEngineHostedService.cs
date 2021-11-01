using ChannelEngine.Common.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace ChannelEngine.Console
{
	class ChannelEngineHostedService : IHostedService
	{
		private readonly IChannelEngineClient channelEngineClient;
		private readonly ILogger logger;

		public ChannelEngineHostedService(IChannelEngineClient channelEngineClient, ILogger<ChannelEngineHostedService> logger)
		{
			this.channelEngineClient = channelEngineClient;
			this.logger = logger;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			logger.LogInformation(await channelEngineClient.GetAsync());
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
