using ACleanAPI.Domain;

namespace ACleanAPI.Infrastructure;

public class AcModelBase : IAcModel // A voir si on ne supprime pas AcModel et on utilise directement IAcModel
{
    public int Id { get; set; }
}
