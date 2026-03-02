using ACleanAPI.Domain.Interfaces;

namespace ACleanAPI.Infrastructure.Core;

/// <summary>
/// Model base class for the infrastructure layer.
/// </summary>
public class AcModelBase : IAcModel
{
    /// <summary>
    /// Id of the model.
    /// </summary>
    public int Id { get; set; }
}
