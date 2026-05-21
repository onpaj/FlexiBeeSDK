using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Accounting.Ledger;

namespace Rem.FlexiBeeSDK.Client.Clients.Accounting.Ledger;

public interface ILedgerClient
{
    Task<IReadOnlyList<LedgerItemFlexiDto>> GetAsync(DateTime dateFrom, DateTime dateTo, IEnumerable<string>? debitAccountPrefixes = null, IEnumerable<string>? creditAccountPrefix = null, string? departmentId = null, int limit = 0, int skip = 0, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<LedgerItemFlexiDto>> GetChangedSinceAsync(DateTime since, int? limit = null, int? skip = null, CancellationToken cancellationToken = default);
}