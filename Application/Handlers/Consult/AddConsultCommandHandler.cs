using Application.Commands.Consult;
using Application.Dto.Response.Consult;
using Application.Helpers;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using Domain.Repository;
using MediatR;

namespace Application.Handlers.Consult;

public class AddConsultCommandHandler : IRequestHandler<AddConsultCommand, Result<ConsultDtoRes>>
{
    private readonly IAsyncRepository<ConsultEntity> _consultRepository;
    private readonly IAsyncRepository<PatientEntity> _patientRepository;
    private readonly IAsyncRepository<ExamEntity> _examRepository;
    private readonly IAsyncRepository<UserEntity> _userRepository;
    private readonly IAsyncRepository<ImageEntity> _imageRepository;
    private readonly IUploaderRepository _uploaderRepository;
    private readonly IMapper _mapper;
    public AddConsultCommandHandler(IAsyncRepository<ConsultEntity> consultRepository, IAsyncRepository<PatientEntity> patientRepository, IAsyncRepository<ExamEntity> examRepository, IMapper mapper, IAsyncRepository<UserEntity> userRepository, IAsyncRepository<ImageEntity> imageRepository, IUploaderRepository uploaderRepository)
    {
        _consultRepository = consultRepository;
        _patientRepository = patientRepository;
        _examRepository = examRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _imageRepository = imageRepository;
        _uploaderRepository = uploaderRepository;
    }
    public async Task<Result<ConsultDtoRes>> Handle(AddConsultCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userEntity = await _userRepository.GetByIdAsync(Guid.Parse(request.UserId), cancellationToken);

            if (userEntity is null)
            {
                return Result.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "User not found",}
                });
            }

            var patientEntity = await _patientRepository.GetByIdAsync(request.PatientId, cancellationToken);

            if (patientEntity is null)
            {
                return Result.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Patient not found",}
                });
            }

            var consultEntity = new ConsultEntity(
                patientEntity.Id,
                request.Motive,
                request.Weight,
                request.Size,
                request.AntecedentPersonal,
                request.Diagnostic,
                request.Recipe,
                request.Nextappointment,
                clinicalhistory: request.Clinicalhistory,
                bilogicalEvaluation: request.BilogicalEvaluation,
                psychologicalEvaluation: request.PsychologicalEvaluation,
                socialEvaluation: request.SocialEvaluation,
                functionalEvaluation: request.FunctionalEvaluation,
                pulse: request.Pulse,
                examComplementaryId: request.ExamComplementaryId,
                oxygenSaturation: request.OxygenSaturation,
                systolicPressure: request.SystolicPressure,
                diastolicPressure: request.DiastolicPressure,
                antecedentFamily: request.AntecedentFamily
            );

            if (request.ExamComplementaryId is not null)
            {
                if (request.ImageExam == null)
                {
                    return Result.Invalid(new List<ValidationError> {
                        new () {ErrorMessage = "Image not found",}
                    });
                }

                var imageCloud = await _uploaderRepository.Upload(request.ImageExam);

                if (imageCloud is null)
                {
                    return Result.Invalid(new List<ValidationError> {
                        new () {ErrorMessage = "Image not uploaded",}
                    });
                }

                var imageEntity = await _imageRepository.AddAsync(new ImageEntity(imageCloud, imageCloud), cancellationToken);
                var examEntity = await _examRepository.GetByIdAsync((Guid)request.ExamComplementaryId, cancellationToken);

                if (examEntity is null)
                {
                    return Result.Invalid(new List<ValidationError> {
                        new () {ErrorMessage = "Exam not found",}
                    });
                }

                consultEntity.ImageExamId = imageEntity.Id;

                var ExamEntity = await _examRepository.GetByIdAsync((Guid)request.ExamComplementaryId, cancellationToken);

                if (ExamEntity is null)
                {
                    return Result.Invalid(new List<ValidationError> {
                        new () {ErrorMessage = "Exam not found",}
                    });
                }

                consultEntity.ExamComplementaryId = request.ExamComplementaryId;

            }

            consultEntity.SetCreationInfo(request.UserId);
            consultEntity.CreatedByGuid = Guid.Parse(request.UserId);

            await _consultRepository.AddAsync(consultEntity, cancellationToken);

            patientEntity.ConsultCount += 1;
            await _patientRepository.UpdateAsync(patientEntity, cancellationToken);

            var consultDtoRes = _mapper.Map<ConsultDtoRes>(consultEntity);

            return Result.Success(consultDtoRes);
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}