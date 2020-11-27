using System.Net.Http;
using Rem.FlexiBeeSDK.Model;

namespace Rem.FlexiBeeSDK.Client
{
    public class KusovnikClient : ResourceClient<Kusovnik>
    {
        public KusovnikClient(FlexiBeeConnection connection, HttpClient httpClient) 
            : base(connection, httpClient)
        {
        }

        public override string ResourceIdentifier => "kusovnik";
    }

    //public class FakturaPrijataClient : ResourceClient<FakturaPrijata>
    //{
    //    public FakturaPrijataClient(FlexiBeeConnection connection, HttpClient httpClient)
    //        : base(connection, httpClient)
    //    {
    //    }

    //    public override string ResourceIdentifier => "kusovnik";
    //}
}