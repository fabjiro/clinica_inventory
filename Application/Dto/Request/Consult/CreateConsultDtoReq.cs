using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Request.Consult;

public class CreateConsultDtoReq
{ 
    [Required(ErrorMessage = "Patient is required")]
    public string Patient{get; set; } = string.Empty;

    [Required(ErrorMessage = "Motive is required")]
    public string Motive {get; set; } = string.Empty;

    [Required(ErrorMessage = "Antecedent personal is required")]
    public string AntecedentPerson{get; set; } = string.Empty;

    [Required(ErrorMessage = "Diagnostic is required")]
    public string Diagnostic{get; set; } = string.Empty;

    [Required(ErrorMessage = "Recipe is required")]
    public string Recipe{get; set; } = string.Empty;

    [Required(ErrorMessage = "Next Nextappointment is required")]
    public DateTime? Nextappointment { get; set; }

    [Required(ErrorMessage = "Weight is required")]
    public decimal Weight{get; set; }
    
    [Required(ErrorMessage = "Size is required")]
    public decimal Size{get; set; }
    
    public string? AntecedentFamily {get; set; }
    public string? Clinicalhistory {get; set; }
    public string? BilogicalEvaluation {get; set; }
    public string? PsychologicalEvaluation {get; set; }
    public string? SocialEvaluation {get; set; }
    public string? FunctionalEvaluation {get; set; }
    public decimal? Pulse {get; set; }
    public decimal? OxygenSaturation {get; set; }
    public decimal? SystolicPressure {get; set; }
    public decimal? DiastolicPressure {get; set; }
    public string? ImageExam {get; set; }
    public string? ExamComplementary {get; set; }
} 