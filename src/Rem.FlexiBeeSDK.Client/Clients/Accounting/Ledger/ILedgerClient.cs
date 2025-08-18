using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Accounting.Ledger;

namespace Rem.FlexiBeeSDK.Client.Clients.Accounting.Ledger;

public interface ILedgerClient
{
    Task<IReadOnlyList<LedgerItemFlexiDto>> GetAsync(DateTime dateFrom, DateTime dateTo, string? debitAccountPrefix = null, string? creditAccountPrefix = null, int limit = 0, int skip = 0, CancellationToken cancellationToken = default);
}