namespace Rem.FlexiBeeSDK.Client
{
    public class Query : RequestSpecs
    {
        public string QueryString { get; set; }

        public override string ToString()
        {
            return $"({QueryString}).{FormatString}?detail={LevelOfDetailString}";
        }
    }
}