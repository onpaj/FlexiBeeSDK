namespace Rem.FlexiBeeSDK.Client
{
    public struct Relations
    {
        public string Value { get; }

        public Relations(string value)
        {
            Value = value;
        }

        public override string ToString() => Value;

        public static Relations PolozkyDokladu = new Relations("polozkyDokladu");
        public static Relations VazebniDoklady = new Relations("vazebni-doklady");
    }
}