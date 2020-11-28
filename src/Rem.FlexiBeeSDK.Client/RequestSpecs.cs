namespace Rem.FlexiBeeSDK.Client
{
    public class RequestSpecs
    {
        public LevelOfDetail LevelOfDetail { get; set; } = LevelOfDetail.Undefined;
        public Format Format { get; set; } = Format.Json;

        protected string FormatString => Format.ToString().ToLower();
        protected string LevelOfDetailString => LevelOfDetail.ToString().ToLower();
    }
}