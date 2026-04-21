using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rem.FlexiBeeSDK.Client.Clients.Banks;
using Rem.FlexiBeeSDK.Client.Clients.CashRegisters;
using Rem.FlexiBeeSDK.Client.ResultFilters;
using Rem.FlexiBeeSDK.Model;
using Rem.FlexiBeeSDK.Model.Invoices;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.Clients.IssuedInvoices
{
    public class IssuedInvoiceClient : ResourceClient, IIssuedInvoiceClient
    {
        private readonly IBankClient _bankClient;
        private readonly ICashRegisterClient _cashRegisterClient;

        public IssuedInvoiceClient(
            FlexiBeeSettings connection,
            IHttpClientFactory httpClientFactory,
            IResultHandler  resultHandler,
            ILogger<IssuedInvoiceClient> logger,
            IBankClient bankClient,
            ICashRegisterClient cashRegisterClient
            )
            : base(connection, httpClientFactory, resultHandler, logger)
        {
            _bankClient = bankClient;
            _cashRegisterClient = cashRegisterClient;
        }

        protected override string ResourceIdentifier => Agenda.IssuedInvoices;

        public async Task<IssuedInvoiceDetailFlexiDto> GetAsync(string code, CancellationToken cancellationToken = default)
        {
            var query = new QueryBuilder()
                .ByCode(code)
                .WithRelation(Relations.Items)
                .WithRelation(Relations.References)
                .WithFullDetail()
                .Build();

           var found = await GetAsync<IssuedInvoiceDetailFlexiDto>(query, cancellationToken: cancellationToken);

           if(!found.Any())
               throw new KeyNotFoundException($"Entity {nameof(IssuedInvoiceDetailFlexiDto)} with key {code} not found");

           return found.Single();
        }

        public async Task<IReadOnlyList<IssuedInvoiceDetailFlexiDto>> GetAllAsync(DateTime dateFrom, DateTime dateTo, CancellationToken cancellationToken = default)
        {
            var query = new QueryBuilder()
                .Raw($"(datVyst gte \"{dateFrom:yyyy-MM-dd}\" and datVyst lte \"{dateTo:yyyy-MM-dd}\")")
                .WithRelation(Relations.Items)
                .WithRelation(Relations.References)
                .WithFullDetail()
                .WithNoLimit()
                .Build();

            var found = await GetAsync<IssuedInvoiceDetailFlexiDto>(query, cancellationToken: cancellationToken);
            return found.ToList().AsReadOnly();
        }

        public async Task<OperationResult<OperationResultDetail>> SaveAsync(IssuedInvoiceDetailFlexiDto invoice,
            bool unpairIfNecessary = false, CancellationToken cancellationToken = default)
        {
            if (unpairIfNecessary && !string.IsNullOrEmpty(invoice.Code))
            {
                try
                {
                    var existing = await GetAsync(invoice.Code, cancellationToken);
                    foreach (var paymentId in existing.GetBankPaymentsIds())
                        await _bankClient.UnPairPayment(paymentId, cancellationToken);
                    foreach (var paymentId in existing.GetCashRegisterPaymentsIds())
                        await _cashRegisterClient.UnPairPayment(paymentId, cancellationToken);
                }
                catch (Exception e) when (e is KeyNotFoundException or HttpRequestException)
                {
                    // Invoice doesn't exist yet, no need to unpair
                }
            }

            return await PostAsync(invoice, cancellationToken: cancellationToken);
        }
    }
}