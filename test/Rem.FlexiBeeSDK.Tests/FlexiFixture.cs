using System;
using System.Net.Http;
using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.Extensions.Configuration;
using Moq;
using Rem.FlexiBeeSDK.Client;

namespace Rem.FlexiBeeSDK.Tests
{
    public class FlexiFixture
    {
        public static IFixture Setup()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<ObjednavkyVydaneTests>()
                .Build();

            var connection = configuration.GetSection("FlexiBeeConnection").Get<FlexiBeeSettings>();
            if (connection == null)
                throw new ApplicationException($"{nameof(FlexiBeeSettings)} settings missing. Add configuration to user secrets");

            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var m = new Mock<IHttpClientFactory>();
            m.Setup(s => s.CreateClient(It.IsAny<string>())).Returns(new HttpClient());

            fixture.Inject(connection);
            fixture.Inject(m.Object);

            return fixture;
        }
    }
}