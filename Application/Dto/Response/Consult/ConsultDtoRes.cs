using Application.Dto.Response.Exam;
using Application.Dto.Response.Image;
using Application.Dto.Response.Patient;
using Application.Dto.Response.User;

namespace Application.Dto.Response.Consult;

public class ConsultDtoRes
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }

    public PatientResDto? Patient { get; set; }

    public string Motive { get; set; } = string.Empty;

    public string? Clinicalhistory { get; set; }

    // evaluations geriatrica

    public string? BilogicalEvaluation { get; set; }
    public string? PsychologicalEvaluation { get; set; }
    public string? SocialEvaluation { get; set; }
    public string? FunctionalEvaluation { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Size { get; set; }
    public decimal? Pulse { get; set; }

    public decimal? OxygenSaturation { get; set; }

    public decimal? SystolicPressure { get; set; }
    public decimal? DiastolicPressure { get; set; }

    public string AntecedentPersonal { get; set; } = string.Empty;
    public string? AntecedentFamily { get; set; }

    public Guid? ExamComplementaryId { get; set; }
    public ExamDto? ComplementaryTest { get; set; }

    public string Diagnosis { get; set; } = string.Empty;

    public Guid? ImageExamId { get; set; }
    public ImageResDto? Image { get; set; }

    public string Recipe { get; set; } = string.Empty;

    public DateTime Nextappointment { get; set; }

    public DateTime CreatedAt { get; set; }

    public int Count { get; set; }

    public UserBasicResDto? UserCreatedBy { get; set; }


}