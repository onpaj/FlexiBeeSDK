using System.Threading;
using System.Threading.Tasks;
using Rem.FlexiBeeSDK.Model.Products.Attributes;
using Rem.FlexiBeeSDK.Model.Response;

namespace Rem.FlexiBeeSDK.Client.Clients.Products.Attributes;

/// <summary>
/// Reads, sets and clears a single attribute value (evidence <c>atribut</c>) on a product (evidence <c>cenik</c>).
/// Attributes are addressed by the numeric id of their definition (evidence <c>typ-atributu</c>).
/// Failures are reported through <see cref="OperationResult{TData}"/>; they are not thrown.
/// </summary>
public interface IProductAttributeClient
{
    /// <summary>
    /// Gets the attribute value of a product. A successful result with a <c>null</c> <c>Result</c> means the
    /// attribute is not set. Several rows for one product and attribute yield <c>409 Conflict</c>.
    /// </summary>
    Task<OperationResult<ProductAttributeFlexiDto>> GetAsync(ProductReference product, int attributeTypeId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Idempotent upsert: updates the existing row, or creates it when the product has none. The value kind must
    /// match the attribute definition (<c>400 BadRequest</c> otherwise). Several existing rows yield <c>409 Conflict</c>.
    /// </summary>
    Task<OperationResult<OperationResultDetail>> SetAsync(ProductReference product, int attributeTypeId, ProductAttributeValue value, CancellationToken cancellationToken = default);

    /// <summary>
    /// Idempotent: deletes the attribute row(s) of the product. Succeeds without any write when none exist.
    /// </summary>
    Task<OperationResult<OperationResultDetail>> ClearAsync(ProductReference product, int attributeTypeId, CancellationToken cancellationToken = default);
}
