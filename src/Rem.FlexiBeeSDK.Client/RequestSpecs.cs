namespace Rem.FlexiBeeSDK.Client
{
    public class RequestSpecs
    {
        public LevelOfDetail LevelOfDetail { get; set; } = LevelOfDetail.Undefined;
        public Format Format { get; set; } = Format.Json;

        public int Limit { get; set; } = 0;

        protected string FormatString => Format.ToString().ToLower();
        public string? LevelOfDetailString { get; set; } 
    }
}