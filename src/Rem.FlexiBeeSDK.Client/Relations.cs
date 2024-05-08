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

        public static Relations Items = new Relations(Agenda.Items);
        public static Relations ReferenceDocs = new Relations(Agenda.ReferenceDocs);
        public static Relations References = new Relations(Agenda.References);
    }
}