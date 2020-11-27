namespace Rem.FlexiBeeSDK.Client
{
    public class RequestSpecs
    {
        public LevelOfDetail LevelOfDetail { get; set; }
        public Format Format { get; set; } = Format.Json;

        protected string FormatString => Format == Format.Json ? "json" : "xml";
        protected string LevelOfDetailString => LevelOfDetail == LevelOfDetail.Full ? "full" : "mini";
    }
}