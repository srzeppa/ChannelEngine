using ChannelEngine.Common.Client;
using ChannelEngine.Common.Logic;
using ChannelEngine.Common.Model;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelEngine.Tests.Logic
{
	public class When_you_get_top_5_products
	{
		private Mock<IChannelEngineClient> channelEngineClientMock;
		private ProductService sut;

		[SetUp]
		public void SetUp()
		{
			channelEngineClientMock = new Mock<IChannelEngineClient>();
			channelEngineClientMock.Setup(x => x.GetAsync()).ReturnsAsync(new Content
			{
				RootContent = new List<ContentElement>
				{
					new ContentElement
					{
						GlobalChannelName = "Name1",
						Lines = new List<Line>
						{
							new Line
							{
								Quantity = 1,
								BundleProductMerchantProductNo = Guid.NewGuid().ToString()
							}
						}
					},
					new ContentElement
					{
						GlobalChannelName = "Name2",
						Lines = new List<Line>
						{
							new Line
							{
								Quantity = 2,
								BundleProductMerchantProductNo = Guid.NewGuid().ToString()
							}
						}
					},
					new ContentElement
					{
						GlobalChannelName = "Name3",
						Lines = new List<Line>
						{
							new Line
							{
								Quantity = 3,
								BundleProductMerchantProductNo = Guid.NewGuid().ToString()
							}
						}
					},
					new ContentElement
					{
						GlobalChannelName = "Name4",
						Lines = new List<Line>
						{
							new Line
							{
								Quantity = 4,
								BundleProductMerchantProductNo = Guid.NewGuid().ToString()
							}
						}
					},
					new ContentElement
					{
						GlobalChannelName = "Name5",
						Lines = new List<Line>
						{
							new Line
							{
								Quantity = 5,
								BundleProductMerchantProductNo = Guid.NewGuid().ToString()
							}
						}
					},
					new ContentElement
					{
						GlobalChannelName = "Name6",
						Lines = new List<Line>
						{
							new Line
							{
								Quantity = 6,
								BundleProductMerchantProductNo = Guid.NewGuid().ToString()
							}
						}
					},
				}
			});
			sut = new ProductService(channelEngineClientMock.Object);
		}

		[Test]
		public async Task It_should_return_top_5_products_by_quantityAsync()
		{
			var result = (await sut.GetBestSoldProductsAsync()).ToList();
			result.Count().Should().Be(5);
			result[0].Name.Should().Be("Name6");
			result[1].Name.Should().Be("Name5");
			result[2].Name.Should().Be("Name4");
			result[3].Name.Should().Be("Name3");
			result[4].Name.Should().Be("Name2");
		}
	}
}
