using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.BaseClass;
using Domain.Entities;

[Table("consult")]
public class ConsultEntity : BaseEntity
{
    [Required]
    public Guid PatientId { get; set; }

    [ForeignKey("PatientId")]
    public PatientEntity? Patient { get; set; }

    [Required]
    public string Motive { get; set; } = string.Empty;

    public string? Clinicalhistory { get; set; }

    // evaluations geriatrica

    public string? BilogicalEvaluation { get; set; }
    public string? PsychologicalEvaluation { get; set; }
    public string? SocialEvaluation { get; set; }
    public string? FunctionalEvaluation { get; set; }

    [Required]
    public decimal? Weight { get; set; }
    [Required]
    public decimal? Size { get; set; }
    public decimal? Pulse { get; set; }

    public decimal? OxygenSaturation { get; set; }

    public decimal? SystolicPressure { get; set; }
    public decimal? DiastolicPressure { get; set; }

    [Required]
    public string AntecedentPersonal { get; set; } = string.Empty;
    public string? AntecedentFamily { get; set; }

    public Guid? ExamComplementaryId { get; set; }
    [ForeignKey("ExamComplementaryId")]
    public ExamEntity? ComplementaryTest { get; set; }

    [Required]
    public string Diagnosis { get; set; } = string.Empty;

    public Guid? ImageExamId { get; set; }
    [ForeignKey("ImageExamId")]
    public ImageEntity? Image { get; set; }

    [Required]
    public string Recipe { get; set; } = string.Empty;

    [Required]
    public string Nextappointment { get; set; } = string.Empty;

    [Required]
    public int Count { get; set; }

    public ConsultEntity() { }

    public ConsultEntity(
        Guid patientId,
        string motive,
        decimal weight,
        decimal size,
        string antecedentPersonal,
        string diagnosis,
        string recipe,
        string nextappointment,
        int count,
        string? clinicalhistory = null,
        string? bilogicalEvaluation = null,
        string? psychologicalEvaluation = null,
        string? socialEvaluation = null,
        string? functionalEvaluation = null,
        decimal? pulse = null,
        decimal? oxygenSaturation = null,
        decimal? systolicPressure = null,
        decimal? diastolicPressure = null,
        string? antecedentFamily = null,
        Guid? examComplementaryId = null,
        Guid? imageExamId = null
    )
    {
        Id = Guid.NewGuid();
        PatientId = patientId;
        Motive = motive;
        Clinicalhistory = clinicalhistory;
        BilogicalEvaluation = bilogicalEvaluation;
        PsychologicalEvaluation = psychologicalEvaluation;
        SocialEvaluation = socialEvaluation;
        FunctionalEvaluation = functionalEvaluation;
        Weight = weight;
        Size = size;
        Pulse = pulse;
        OxygenSaturation = oxygenSaturation;
        SystolicPressure = systolicPressure;
        DiastolicPressure = diastolicPressure;
        AntecedentPersonal = antecedentPersonal;
        AntecedentFamily = antecedentFamily;
        Diagnosis = diagnosis;
        Recipe = recipe;
        Nextappointment = nextappointment;
        Count = count;
        ExamComplementaryId = examComplementaryId;
        ImageExamId = imageExamId;
    }
}