using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Accounting;

namespace Rem.FlexiBeeSDK.Client.Clients.Accounting;

public interface IAccountingTemplateClient
{
    Task<IReadOnlyList<AccountingTemplateFlexiDto>> GetAsync(CancellationToken cancellationToken = default);
}