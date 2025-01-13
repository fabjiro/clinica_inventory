using Application.Commands.Patient;
using Application.Dto.Response.Patient;
using Application.Helpers;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Patient;

public class UpdatePatientCommandHanller : IRequestHandler<UpdatePatientCommand, Result<PatientResDto>>
{
    private readonly IAsyncRepository<PatientEntity> _patientRepository;
    private readonly IAsyncRepository<RolEntity> _rolRepository;
    private readonly IAsyncRepository<CivilStatusEntity> _civilStatusRepository;
    private readonly IMapper _mapper;

    public UpdatePatientCommandHanller(IAsyncRepository<PatientEntity> patientRepository, IMapper mapper, IAsyncRepository<RolEntity> rolRepository, IAsyncRepository<CivilStatusEntity> civilStatusRepository)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
        _rolRepository = rolRepository;
        _civilStatusRepository = civilStatusRepository;
    }

    public async Task<Result<PatientResDto>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var patient = await _patientRepository.GetByIdAsync(request.Id, cancellationToken);
            if (patient is null)
            {
                return Result<PatientResDto>.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Patient not found",}
                });
            }

            if(request.Name is not null)
            {
                patient.Name = request.Name;
            }

            if(request.Identification is not null)
            {
                patient.Identification = request.Identification;
            }

            if(request.Phone is not null)
            {
                patient.Phone = request.Phone;
            }

            if(request.Address is not null)
            {
                patient.Address = request.Address;
            }

            if(request.ContactPerson is not null)
            {
                patient.ContactPerson = request.ContactPerson;
            }

            if(request.ContactPhone is not null)
            {
                patient.ContactPhone = request.ContactPhone;
            }

            if(request.Age is not null)
            {
                patient.Age = request.Age;
            }

            if(request.TypeSex is not null)
            {
                patient.TypeSex = request.TypeSex;
            }

            if(request.BirthDate is not null)
            {
                Console.WriteLine(request.BirthDate);
                patient.Birthday = request.BirthDate;
            }

            if(request.CivilStatus is not null)
            {
                var civilStatus = await _civilStatusRepository.GetByIdAsync((Guid)request.CivilStatus, cancellationToken);

                if (civilStatus is null)
                {
                    return Result<PatientResDto>.Invalid(new List<ValidationError> {
                        new () {ErrorMessage = "Civil Status not found",}
                    });
                }
                
                patient.CivilStatusId = civilStatus.Id;
            }

            if(request.Rol is not null)
            {
                var rol = await _rolRepository.GetByIdAsync((Guid)request.Rol, cancellationToken);

                if (rol is null)
                {
                    return Result<PatientResDto>.Invalid(new List<ValidationError> {
                        new () {ErrorMessage = "Rol not found",}
                    });

                }
                
                patient.RolId = rol.Id;
            }

            patient.SetUpdateInfo(request.UserId);

            await _patientRepository.UpdateAsync(patient, cancellationToken);
            return Result.Success(_mapper.Map<PatientResDto>(patient));
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}