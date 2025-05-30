using System.Collections.Generic;
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
        
        public QueryBuilder WithParameter(string key, string value)
        {
            _query.Parameters.Add(key, value);
            return this;
        }

        public QueryBuilder WithParameters(IDictionary<string, string> parameters)
        {
            foreach (var kvp in parameters)
                _query.Parameters.Add(kvp);

            return this;
        }

        public QueryBuilder WithFullDetail()
        {
            _query.LevelOfDetail = LevelOfDetail.Full;
            return this;
        }
        
        public QueryBuilder WithCustomDetail(string detailString)
        {
            _query.LevelOfDetail = LevelOfDetail.Custom;
            _query.LevelOfDetailString = $"custom:{detailString}";
            return this;
        }
        
        public QueryBuilder WithNoLimit()
        {
            _query.Limit = 0;
            return this;
        }

        public QueryBuilder WithLimit(int limit)
        {
            _query.Limit = limit;
            return this;
        }

        public QueryBuilder ByCode(string code)
        {
            return Raw($"kod='{HttpUtility.UrlEncode(code)}'");
        }
        
        public QueryBuilder ById(int id)
        {
            return Raw(id.ToString());
        }
    }
}