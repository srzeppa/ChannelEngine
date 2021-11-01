using ChannelEngine.Common.Client;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace ChannelEngine.Console
{
	class ChannelEngineHostedService : IHostedService
	{
		private readonly IChannelEngineClient channelEngineClient;

		public ChannelEngineHostedService(IChannelEngineClient channelEngineClient)
		{
			this.channelEngineClient = channelEngineClient;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			await channelEngineClient.GetAsync();
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
