using System.ComponentModel.DataAnnotations;

public class UpdateProductReqDto {
    [Required(ErrorMessage = "Id is required")]
    public string Id { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public int? Stock { get; set; }
    public string? CategoryId { get; set; }
    public string? Image { get; set; }
}