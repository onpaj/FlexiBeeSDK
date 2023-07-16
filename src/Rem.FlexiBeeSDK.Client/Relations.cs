using Rem.FlexiBeeSDK.Model;

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

        public static Relations Items = new Relations(Evidence.Items);
        public static Relations ReferenceDocs = new Relations(Evidence.ReferenceDocs);
        public static Relations References = new Relations(Evidence.References);
    }
}