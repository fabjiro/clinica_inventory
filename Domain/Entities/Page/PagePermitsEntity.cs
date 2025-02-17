using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("page_permit")]
public class PagePermitsEntity
{
    public Guid Id { get; set; }
    public Guid PageId { get; set; }
    public Guid SubRolId { get; set; }

    [ForeignKey("PageId")]
    public PageEntity? Page { get; set; }

    [ForeignKey("SubRolId")]
    public SubRolEntity? SubRol { get; set; }

    public PagePermitsEntity() { }

    public PagePermitsEntity(Guid pageId, Guid subRolId)
    {
        Id = Guid.NewGuid();
        PageId = pageId;
        SubRolId = subRolId;
    }
}