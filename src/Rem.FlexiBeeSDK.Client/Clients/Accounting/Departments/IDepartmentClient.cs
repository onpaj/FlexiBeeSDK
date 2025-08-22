using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Accounting.Departments;

namespace Rem.FlexiBeeSDK.Client.Clients.Accounting.Departments;

public interface IDepartmentClient
{
    Task<IReadOnlyList<DepartmentFlexiDto>> GetAsync(CancellationToken cancellationToken = default);
}