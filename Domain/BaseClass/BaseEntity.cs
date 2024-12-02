namespace Domain.BaseClass;

using System.ComponentModel.DataAnnotations;

public abstract class BaseEntity {
    [Key]
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public string? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }


    public void SetCreationInfo(string userId)
    {
        CreatedAt = DateTime.UtcNow;
        CreatedBy = userId;
        IsDeleted = false;
    }

    public void SetUpdateInfo(string userId)
    {
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = userId;
    }

    public void SetDeletedInfo(string userId)
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        DeletedBy = userId;
    }
}