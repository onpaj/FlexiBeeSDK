using System.Web;

namespace Rem.FlexiBeeSDK.Client
{
    public class QueryBuilder
    {
        private Query _query;

        public QueryBuilder()
        {
            _query = new Query()
            {
                Format = Format.Json,
            };
        }

        public QueryBuilder Raw(string queryString)
        {
            _query.QueryString = queryString;

            return this;
        }

        public Query Build()
        {
            return _query;
        }

        public QueryBuilder WithRelation(Relations relation)
        {
            _query.Relations.Add(relation);
            return this;
        }

        public QueryBuilder WithFullDetail()
        {
            _query.LevelOfDetail = LevelOfDetail.Full;
            return this;
        }

        public QueryBuilder ByCode(string code)
        {
            return Raw($"kod='{HttpUtility.UrlEncode(code)}'");
        }
    }
}