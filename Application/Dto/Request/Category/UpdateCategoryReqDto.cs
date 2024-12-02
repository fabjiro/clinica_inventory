using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Request.Category
{
    public class UpdateCategoryReqDto
    {
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; } = String.Empty;

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = String.Empty;
    }
}