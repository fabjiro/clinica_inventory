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

public class UpdateConsultCommandHandler : IRequestHandler<UpdateConsultCommand, Result<ConsultDtoRes>>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<ConsultEntity> _repository;
    private readonly IAsyncRepository<ImageEntity> _imageRepository;
    private readonly IUploaderRepository _uploaderRepository;
    private readonly IAsyncRepository<ExamEntity> _examRepository;

    public UpdateConsultCommandHandler(IMapper mapper, IAsyncRepository<ConsultEntity> repository, IAsyncRepository<ExamEntity> examRepository, IAsyncRepository<ImageEntity> imageRepository, IUploaderRepository uploaderRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _examRepository = examRepository;
        _imageRepository = imageRepository;
        _uploaderRepository = uploaderRepository;
    }

    public async Task<Result<ConsultDtoRes>> Handle(UpdateConsultCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var consult = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (consult == null)
            {
                return Result.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Consult not found",}
                });
            }


             if(request.Motive is not null)
             {
                 consult.Motive = request.Motive;
             }

             if(request.AntecedentPersonal is not null)
             {
                 consult.AntecedentPersonal = request.AntecedentPersonal;
             }

             if(request.Diagnostic is not null)
             {
                 consult.Diagnosis = request.Diagnostic;
             }

             if(request.Recipe is not null)
             {
                 consult.Recipe = request.Recipe;
             }

             if(request.Weight is not null)
             {
                 consult.Weight = (decimal)request.Weight;
             }

             if(request.Size is not null)
             {
                 consult.Size = (decimal)request.Size;
             }

             if(request.AntecedentFamily is not null)
             {
                 consult.AntecedentFamily = request.AntecedentFamily;
             }

             if(request.Clinicalhistory is not null)
             {
                 consult.Clinicalhistory = request.Clinicalhistory;
             }

             if(request.BilogicalEvaluation is not null)
             {
                 consult.BilogicalEvaluation = request.BilogicalEvaluation;
             }

             if(request.PsychologicalEvaluation is not null)
             {
                 consult.PsychologicalEvaluation = request.PsychologicalEvaluation;
             }

             if(request.SocialEvaluation is not null)
             {
                 consult.SocialEvaluation = request.SocialEvaluation;
             }

             if(request.FunctionalEvaluation is not null)
             {
                 consult.FunctionalEvaluation = request.FunctionalEvaluation;
             }

             if(request.Pulse is not null)
             {
                 consult.Pulse = request.Pulse;
             }

             if(request.OxygenSaturation is not null)
             {
                 consult.OxygenSaturation = request.OxygenSaturation;
             }

             if(request.SystolicPressure is not null)
             {
                 consult.SystolicPressure = request.SystolicPressure;
             }

             if(request.DiastolicPressure is not null)
             {
                 consult.DiastolicPressure = request.DiastolicPressure;
             }

             if(request.ImageExam is not null)
             {
                if(request.ExamComplementaryId is null)
                {
                    return Result.Invalid(new List<ValidationError> {
                        new () {ErrorMessage = "Exam not found",}
                    });
                }
                var imageCloud = await _uploaderRepository.Upload(request.ImageExam);

                if(imageCloud is null)
                {
                    return Result.Invalid(new List<ValidationError> {
                        new () {ErrorMessage = "Image not uploaded",}
                    });
                }

                var imageEntity = new ImageEntity(imageCloud, imageCloud);
                await _imageRepository.AddAsync(imageEntity, cancellationToken);

                consult.ImageExamId = imageEntity.Id;
             }

             if(request.Nextappointment is not null)
             {
                 consult.Nextappointment = request.Nextappointment;
             }

             if(request.ExamComplementaryId is not null)
             {
                var ExamEntity = await _examRepository.GetByIdAsync((Guid)request.ExamComplementaryId, cancellationToken);

                if(ExamEntity is null)
                {
                    return Result.Invalid(new List<ValidationError> {
                        new () {ErrorMessage = "Exam not found",}
                    });
                }

                consult.ExamComplementaryId = request.ExamComplementaryId;
             }


            consult.SetUpdateInfo(request.UserId);
            await _repository.UpdateAsync(consult, cancellationToken);

            return Result.Success(_mapper.Map<ConsultDtoRes>(consult));
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}
