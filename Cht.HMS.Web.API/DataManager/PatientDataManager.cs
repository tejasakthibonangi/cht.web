using Cht.HMS.Web.API.DBConfiguration;
using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;
using Cht.HMS.Web.Utility;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography.Xml;

namespace Cht.HMS.Web.API.DataManager
{
    public class PatientDataManager : IPatientManager
    {
        private readonly ApplicationDBContext _dbContext;
        public PatientDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PatientInformation> GetPatientDetailsAsync(Guid patientId)
        {
            var patientInformation = new PatientInformation();

            var patient = await _dbContext.patientRegistrations.FindAsync(patientId);

            if (patient != null)
            {
                var doctor = await _dbContext.doctors
                    .Where(x => x.DoctorId == patient.DoctorAssignedId)
                    .FirstOrDefaultAsync();

                var patientCunsultation = await _dbContext.medicalConsultations
                    .Where(x => x.PatientId == patient.PatientId)
                    .FirstOrDefaultAsync();

                patientInformation = new PatientInformation
                {
                    PatientId = patient.PatientId,
                    DateOfVisit = patient.DateOfVisit,
                    TimeOfVisit = patient.TimeOfVisit,
                    ComingFrom = patient.ComingFrom,
                    Reference = patient.Reference,
                    PatientName = patient.PatientName,
                    PhoneNo = patient.PhoneNo,
                    AlternatePhoneNo = patient.AlternatePhoneNo,
                    Email = patient.Email,
                    Gender = patient.Gender,
                    DOB = patient.DOB,
                    Height = patient.Height,
                    Weight = patient.Weight,
                    BP = patient.BP,
                    Sugar = patient.Sugar,
                    Temperature = patient.Temperature,
                    HealthIssues = patient.HealthIssues,
                    DoctorId = patient.DoctorAssignedId,
                    Fee = patient.Fee,
                    PreparedBy = patient.PreparedBy,
                    CurrentStatus = patient.CurrentStatus,
                    CreatedBy = patient.CreatedBy,
                    CreatedOn = patient.CreatedOn,
                    ModifiedBy = patient.ModifiedBy,
                    ModifiedOn = patient.ModifiedOn,
                    IsActive = patient.IsActive,
                    DoctorName = doctor?.DoctorName ?? "Unknown Doctor"
                };
                if (patientCunsultation != null)
                {
                    var patientCunsultationDetails = await _dbContext.consultationDetails
                        .Where(x => x.ConsultationId == patientCunsultation.ConsultationId)
                        .FirstOrDefaultAsync();

                    patientInformation.patientCunsultation = new PatientCunsultation()
                    {
                        PatientId = patient.PatientId,
                        ConsultationDate = patientCunsultation.ConsultationDate,
                        ConsultationTime = patientCunsultation.ConsultationTime,
                        ConsultationId = patientCunsultation.ConsultationId,
                        Diagnosis = patientCunsultation.Diagnosis,
                        CreatedBy = patientCunsultation.CreatedBy,
                        CreatedOn = patientCunsultation.CreatedOn,
                        DoctorId = patientCunsultation.DoctorId,
                        Symptoms = patientCunsultation.Symptoms,
                        IsActive = patientCunsultation.IsActive,
                        ModifiedBy = patientCunsultation.ModifiedBy,
                        Remarks = patientCunsultation.Remarks,
                        ModifiedOn = patientCunsultation.ModifiedOn
                    };

                    if (patientCunsultationDetails != null)
                    {
                        patientInformation.patientCunsultation.patientConsultationDetails = new PatientConsultationDetails()
                        {
                            ConsultationId = patientCunsultationDetails.ConsultationId,
                            Advice = patientCunsultationDetails.Advice,
                            CreatedBy = patientCunsultationDetails.CreatedBy,
                            CreatedOn = patientCunsultationDetails.CreatedOn,
                            DetailId = patientCunsultationDetails.DetailId,
                            Diagnosis = patientCunsultationDetails.Diagnosis,
                            FollowUpDate = patientCunsultationDetails.FollowUpDate,
                            IsActive = patientCunsultationDetails.IsActive,
                            ModifiedBy = patientCunsultationDetails.ModifiedBy,
                            ModifiedOn = patientCunsultationDetails.ModifiedOn,
                            Treatment = patientCunsultationDetails.Treatment
                        };
                    }

                    var pharmacyOrder = await _dbContext.pharmacyOrders.Where(x => x.PatientId == patientId && x.ConsultationId == patientCunsultation.ConsultationId).FirstOrDefaultAsync();

                    if (pharmacyOrder != null)
                    {

                        var pharmacyOrderDetailList = await _dbContext.pharmacyOrderDetails.Where(x => x.OrderId == pharmacyOrder.OrderId).ToListAsync();


                        patientInformation.patientPharmacyOrder = new PatientPharmacyOrder()
                        {
                            OrderId = pharmacyOrder.OrderId,
                            PatientId = pharmacyOrder.PatientId,
                            ConsultationId = pharmacyOrder.ConsultationId,
                            OrderDate = pharmacyOrder.OrderDate,
                            ItemsQty = pharmacyOrder.ItemsQty,
                            TotalAmount = pharmacyOrder.TotalAmount,
                            CreatedBy = pharmacyOrder.CreatedBy,
                            CreatedOn = pharmacyOrder.CreatedOn,
                            ModifiedBy = pharmacyOrder.ModifiedBy,
                            ModifiedOn = pharmacyOrder.ModifiedOn,
                            IsActive = pharmacyOrder.IsActive,
                            patientPharmacyOrderDetails = pharmacyOrderDetailList.Select(orderDetail => new PatientPharmacyOrderDetail()
                            {
                                OrderDetailId = orderDetail.OrderDetailId,
                                OrderId = orderDetail.OrderId,
                                MedicineId = orderDetail.MedicineId,
                                Quantity = orderDetail.Quantity,
                                PricePerUnit = orderDetail.PricePerUnit,
                                TotalPrice = orderDetail.TotalPrice,
                                CreatedBy = orderDetail.CreatedBy,
                                CreatedOn = orderDetail.CreatedOn,
                                ModifiedBy = orderDetail.ModifiedBy,
                                ModifiedOn = DateTimeOffset.UtcNow, // Set to current time
                                IsActive = orderDetail.IsActive
                            }).ToList()
                        };
                    }

                    var labOrder = await _dbContext.labOrders.Where(x => x.PatientId == patientId && x.ConsultationId == patientCunsultation.ConsultationId).FirstOrDefaultAsync();

                    if (labOrder != null)
                    {
                        var labOrderDetailList = await _dbContext.labOrderDetails
                            .Where(x => x.LabOrderId == labOrder.LabOrderId)
                            .ToListAsync();

                        patientInformation.patientLabOrder = new PatientLabOrder()
                        {
                            LabOrderId = labOrder.LabOrderId,
                            PatientId = labOrder.PatientId,
                            ConsultationId = labOrder.ConsultationId,
                            OrderDate = labOrder.OrderDate,
                            TotalAmount = labOrder.TotalAmount,
                            CreatedBy = labOrder.CreatedBy,
                            CreatedOn = labOrder.CreatedOn,
                            ModifiedBy = labOrder.ModifiedBy,
                            ModifiedOn = labOrder.ModifiedOn,
                            IsActive = labOrder.IsActive,
                            patientLabOrderDetails = labOrderDetailList.Select(orderDetail => new PatientLabOrderDetail()
                            {
                                LabOrderDetailId = orderDetail.LabOrderDetailId,
                                LabOrderId = orderDetail.LabOrderId,
                                TestId = orderDetail.TestId,
                                Quantity = orderDetail.Quantity,
                                PricePerUnit = orderDetail.PricePerUnit,
                                TotalPrice = orderDetail.TotalPrice,
                                CreatedBy = orderDetail.CreatedBy,
                                CreatedOn = orderDetail.CreatedOn,
                                ModifiedBy = orderDetail.ModifiedBy,
                                ModifiedOn = DateTimeOffset.UtcNow, // Set to current time
                                IsActive = orderDetail.IsActive
                            }).ToList()
                        };
                    }
                }
            }

            return patientInformation;
        }

        public async Task<List<PatientInformation>> GetPatientInformationsAsync(string inputString = "")
        {

            var doctors = await _dbContext.doctors.ToListAsync();

            return await PreparePatientRegistrationsAsync(doctors, inputString);
        }

        public async Task<List<PatientInformation>> GetPatientInformationsByDoctorAsync(Guid doctorId)
        {

            var doctors = await _dbContext.doctors.Where(x => x.UserId == doctorId).ToListAsync();

            var patients = await PreparePatientRegistrationsAsync(doctors, string.Empty);

            var doctorIds = doctors.Select(d => d.DoctorId).ToList();

            var relatedPatients = patients.Where(p => doctorIds.Contains(p.DoctorId.Value)).ToList();

            return relatedPatients;
        }

        public async Task<List<PatientRegistration>> GetPatientRegistrationsAsync()
        {
            return await _dbContext.patientRegistrations.ToListAsync();
        }

        public async Task<List<PatientRegistration>> GetPatientRegistrationsAsync(string inputString)
        {
            var query = _dbContext.patientRegistrations.AsQueryable();

            if (!string.IsNullOrEmpty(inputString))
            {
                query = query.Where(x =>
                    (x.PatientName.Contains(inputString)) ||
                    (x.PhoneNo.Contains(inputString)) ||
                    (x.AlternatePhoneNo.Contains(inputString)) ||
                    (x.Email.Contains(inputString)) ||
                    (x.ComingFrom.Contains(inputString)) ||
                    (x.Reference.Contains(inputString)) ||
                    (x.Gender.Contains(inputString)) ||
                    (x.HealthIssues.Contains(inputString))
                );
            }

            return await query.ToListAsync();
        }


        public async Task<PatientRegistration> PatientRegistrationAsync(PatientRegistration registration)
        {
            if (registration.PatientId == Guid.Empty)
            {
                await _dbContext.patientRegistrations.AddAsync(registration);
            }
            else
            {
                var existingRegistration = await _dbContext.patientRegistrations.FindAsync(registration.PatientId);

                if (existingRegistration != null)
                {
                    bool hasChanges = EntityUpdater.HasChanges(existingRegistration, registration, nameof(PatientRegistration.CreatedBy), nameof(PatientRegistration.CreatedOn));

                    if (hasChanges)
                    {
                        EntityUpdater.UpdateProperties(existingRegistration, registration, nameof(PatientRegistration.CreatedBy), nameof(PatientRegistration.CreatedOn));

                    }
                }
            }

            await _dbContext.SaveChangesAsync();

            return registration;
        }

        private async Task<List<PatientInformation>> PreparePatientRegistrationsAsync(List<Doctor> doctors, string inputString)
        {
            List<PatientInformation> patientInformation = new List<PatientInformation>();

            List<PatientRegistration> patientRegistrations = new List<PatientRegistration>();

            var query = _dbContext.patientRegistrations.AsQueryable();

            if (!string.IsNullOrEmpty(inputString))
            {
                query = query.Where(x =>
                    (x.PatientName.Contains(inputString)) ||
                    (x.PhoneNo.Contains(inputString)) ||
                    (x.AlternatePhoneNo.Contains(inputString)) ||
                    (x.Email.Contains(inputString)) ||
                    (x.ComingFrom.Contains(inputString)) ||
                    (x.Reference.Contains(inputString)) ||
                    (x.Gender.Contains(inputString)) ||
                    (x.HealthIssues.Contains(inputString))
                );
            }

            patientRegistrations = await query.ToListAsync();

            patientInformation = (from patient in patientRegistrations
                                  join doctor in doctors on patient.DoctorAssignedId equals doctor.DoctorId into doctorInfo
                                  from doctorJoinInfo in doctorInfo.DefaultIfEmpty()
                                  select new PatientInformation
                                  {
                                      PatientId = patient.PatientId,
                                      DateOfVisit = patient.DateOfVisit,
                                      TimeOfVisit = patient.TimeOfVisit,
                                      ComingFrom = patient.ComingFrom,
                                      Reference = patient.Reference,
                                      PatientName = patient.PatientName,
                                      PhoneNo = patient.PhoneNo,
                                      AlternatePhoneNo = patient.AlternatePhoneNo,
                                      Email = patient.Email,
                                      Gender = patient.Gender,
                                      DOB = patient.DOB,
                                      Height = patient.Height,
                                      Weight = patient.Weight,
                                      BP = patient.BP,
                                      Sugar = patient.Sugar,
                                      Temperature = patient.Temperature,
                                      HealthIssues = patient.HealthIssues,
                                      DoctorId = patient.DoctorAssignedId,
                                      Fee = patient.Fee,
                                      PreparedBy = patient.PreparedBy,
                                      CurrentStatus = patient.CurrentStatus,
                                      CreatedBy = patient.CreatedBy,
                                      CreatedOn = patient.CreatedOn,
                                      ModifiedBy = patient.ModifiedBy,
                                      ModifiedOn = patient.ModifiedOn,
                                      IsActive = patient.IsActive,
                                      DoctorName = doctorJoinInfo?.DoctorName ?? "Unknown Doctor",
                                  }).OrderByDescending(x => x.CreatedOn).ToList();

            return patientInformation;
        }

        private PatientInformation MapToPatientInformation(PatientRegistration patient, Doctor doctor)
        {
            return new PatientInformation
            {
                PatientId = patient.PatientId,
                DateOfVisit = patient.DateOfVisit,
                TimeOfVisit = patient.TimeOfVisit,
                ComingFrom = patient.ComingFrom,
                Reference = patient.Reference,
                PatientName = patient.PatientName,
                PhoneNo = patient.PhoneNo,
                AlternatePhoneNo = patient.AlternatePhoneNo,
                Email = patient.Email,
                Gender = patient.Gender,
                DOB = patient.DOB,
                Height = patient.Height,
                Weight = patient.Weight,
                BP = patient.BP,
                Sugar = patient.Sugar,
                Temperature = patient.Temperature,
                HealthIssues = patient.HealthIssues,
                DoctorId = patient.DoctorAssignedId,
                Fee = patient.Fee,
                PreparedBy = patient.PreparedBy,
                CurrentStatus = patient.CurrentStatus,
                CreatedBy = patient.CreatedBy,
                CreatedOn = patient.CreatedOn,
                ModifiedBy = patient.ModifiedBy,
                ModifiedOn = patient.ModifiedOn,
                IsActive = patient.IsActive,
                DoctorName = doctor?.DoctorName ?? "Unknown Doctor"
            };
        }

        public async Task<PatientInformation> UpsertPatientConsultationDetailsAsync(PatientInformation patientInformation)
        {
            if (patientInformation != null)
            {
                var existingPatientRegistration = await _dbContext.patientRegistrations.FindAsync(patientInformation.PatientId);
                existingPatientRegistration.CurrentStatus = patientInformation.CurrentStatus;
                existingPatientRegistration.ModifiedBy = patientInformation.ModifiedBy;
                existingPatientRegistration.ModifiedOn = patientInformation.ModifiedOn;
                existingPatientRegistration.IsActive = patientInformation.IsActive;

            }

            var exitingPatientCunsultation = await _dbContext.medicalConsultations.Where(x => x.PatientId == patientInformation.PatientId).FirstOrDefaultAsync();
            if (exitingPatientCunsultation != null)
            {
                exitingPatientCunsultation.ConsultationId = patientInformation.patientCunsultation.ConsultationId.Value; // Ensure a valid Guid
                exitingPatientCunsultation.DoctorId = patientInformation.patientCunsultation.DoctorId.Value;
                exitingPatientCunsultation.ConsultationDate = patientInformation.patientCunsultation.ConsultationDate.Value;
                exitingPatientCunsultation.ConsultationTime = patientInformation.patientCunsultation.ConsultationTime.Value;
                exitingPatientCunsultation.Symptoms = patientInformation.patientCunsultation.Symptoms;
                exitingPatientCunsultation.Diagnosis = patientInformation.patientCunsultation.Diagnosis;
                exitingPatientCunsultation.Remarks = patientInformation.patientCunsultation.Remarks;
                exitingPatientCunsultation.ModifiedBy = patientInformation.patientCunsultation.ModifiedBy;
                exitingPatientCunsultation.ModifiedOn = DateTimeOffset.UtcNow; // Set to current time
                exitingPatientCunsultation.IsActive = patientInformation.patientCunsultation.IsActive;


                var exitingPatientCunsultationDetails = await _dbContext.consultationDetails.Where(x => x.ConsultationId == exitingPatientCunsultation.ConsultationId).FirstOrDefaultAsync();
                if (exitingPatientCunsultationDetails != null)
                {
                    exitingPatientCunsultationDetails.Treatment = patientInformation.patientCunsultation.patientConsultationDetails.Treatment;
                    exitingPatientCunsultationDetails.Diagnosis = patientInformation.patientCunsultation.patientConsultationDetails.Diagnosis;
                    exitingPatientCunsultationDetails.FollowUpDate = patientInformation.patientCunsultation.patientConsultationDetails.FollowUpDate;
                    exitingPatientCunsultationDetails.Advice = patientInformation.patientCunsultation.patientConsultationDetails.Advice;
                    exitingPatientCunsultationDetails.ModifiedBy = patientInformation.patientCunsultation.patientConsultationDetails.ModifiedBy;
                    exitingPatientCunsultationDetails.ModifiedOn = DateTimeOffset.UtcNow; // Set to current time
                    exitingPatientCunsultationDetails.IsActive = patientInformation.patientCunsultation.patientConsultationDetails.IsActive;
                }

                var existingPatientPharmacyOrder = await _dbContext.pharmacyOrders.Where(x => x.PatientId == patientInformation.PatientId && x.ConsultationId == exitingPatientCunsultation.ConsultationId).FirstOrDefaultAsync();

                if (existingPatientPharmacyOrder != null)
                {
                    existingPatientPharmacyOrder.ItemsQty = patientInformation.patientPharmacyOrder.ItemsQty;
                    existingPatientPharmacyOrder.TotalAmount = patientInformation.patientPharmacyOrder.TotalAmount;
                    existingPatientPharmacyOrder.ModifiedBy = patientInformation.patientPharmacyOrder.ModifiedBy;
                    existingPatientPharmacyOrder.ModifiedOn = DateTimeOffset.UtcNow; // Set to current time
                    existingPatientPharmacyOrder.IsActive = patientInformation.patientPharmacyOrder.IsActive;

                    var existingPatientPharmacyOrderDetail = await _dbContext.pharmacyOrderDetails.Where(x => x.OrderId == existingPatientPharmacyOrder.OrderId).ToListAsync();

                    if (existingPatientPharmacyOrderDetail.Any())
                    {
                        foreach (var pharmacyOrderDetail in existingPatientPharmacyOrderDetail)
                        {
                            var _pharmacyOrderDetail = patientInformation.patientPharmacyOrder.patientPharmacyOrderDetails.Where(x => x.OrderDetailId == pharmacyOrderDetail.OrderDetailId).FirstOrDefault();

                            if (_pharmacyOrderDetail != null)
                            {
                                pharmacyOrderDetail.OrderDetailId = _pharmacyOrderDetail.OrderDetailId.Value;
                                pharmacyOrderDetail.OrderId = _pharmacyOrderDetail.OrderId;
                                pharmacyOrderDetail.MedicineId = _pharmacyOrderDetail.MedicineId;
                                pharmacyOrderDetail.Quantity = _pharmacyOrderDetail.Quantity;
                                pharmacyOrderDetail.PricePerUnit = _pharmacyOrderDetail.PricePerUnit;
                                pharmacyOrderDetail.TotalPrice = _pharmacyOrderDetail.TotalPrice;
                                pharmacyOrderDetail.CreatedBy = _pharmacyOrderDetail.CreatedBy;
                                pharmacyOrderDetail.CreatedOn = _pharmacyOrderDetail.CreatedOn;
                                pharmacyOrderDetail.ModifiedBy = _pharmacyOrderDetail.ModifiedBy;
                                pharmacyOrderDetail.ModifiedOn = DateTimeOffset.UtcNow;
                                pharmacyOrderDetail.IsActive = _pharmacyOrderDetail.IsActive;
                            }
                        }
                    }
                    else
                    {
                        List<PharmacyOrderDetail> pharmacyOrderDetails = new List<PharmacyOrderDetail>();

                        foreach (var orderDetail in patientInformation.patientPharmacyOrder.patientPharmacyOrderDetails)
                        {
                            pharmacyOrderDetails.Add(new PharmacyOrderDetail()
                            {
                                OrderDetailId = orderDetail.OrderDetailId.Value,
                                OrderId = existingPatientPharmacyOrder.OrderId,
                                MedicineId = orderDetail.MedicineId,
                                Quantity = orderDetail.Quantity,
                                PricePerUnit = orderDetail.PricePerUnit,
                                TotalPrice = orderDetail.TotalPrice,
                                CreatedBy = orderDetail.CreatedBy,
                                CreatedOn = orderDetail.CreatedOn,
                                ModifiedBy = orderDetail.ModifiedBy,
                                ModifiedOn = DateTimeOffset.UtcNow,
                                IsActive = orderDetail.IsActive
                            });
                        }

                        await _dbContext.BulkInsertAsync(pharmacyOrderDetails);
                        await _dbContext.SaveChangesAsync();
                    }

                }
                else
                {
                    var patientPharmacyOrder = new PharmacyOrder
                    {
                        OrderId = patientInformation.patientPharmacyOrder.OrderId.Value,
                        PatientId = patientInformation.patientPharmacyOrder.PatientId,
                        ConsultationId = patientInformation.patientPharmacyOrder.ConsultationId,
                        OrderDate = patientInformation.patientPharmacyOrder.OrderDate,
                        ItemsQty = patientInformation.patientPharmacyOrder.ItemsQty,
                        TotalAmount = patientInformation.patientPharmacyOrder.TotalAmount,
                        CreatedBy = patientInformation.patientPharmacyOrder.CreatedBy,
                        CreatedOn = patientInformation.patientPharmacyOrder.CreatedOn,
                        ModifiedBy = patientInformation.patientPharmacyOrder.ModifiedBy,
                        ModifiedOn = DateTimeOffset.UtcNow, // Set to current time
                        IsActive = patientInformation.patientPharmacyOrder.IsActive
                    };
                    await _dbContext.pharmacyOrders.AddAsync(patientPharmacyOrder);

                    await _dbContext.SaveChangesAsync();

                    List<PharmacyOrderDetail> pharmacyOrderDetails = new List<PharmacyOrderDetail>();

                    foreach (var orderDetail in patientInformation.patientPharmacyOrder.patientPharmacyOrderDetails)
                    {
                        pharmacyOrderDetails.Add(new PharmacyOrderDetail()
                        {
                            OrderDetailId = orderDetail.OrderDetailId.Value,
                            OrderId = patientPharmacyOrder.OrderId,
                            MedicineId = orderDetail.MedicineId,
                            Quantity = orderDetail.Quantity,
                            PricePerUnit = orderDetail.PricePerUnit,
                            TotalPrice = orderDetail.TotalPrice,
                            CreatedBy = orderDetail.CreatedBy,
                            CreatedOn = orderDetail.CreatedOn,
                            ModifiedBy = orderDetail.ModifiedBy,
                            ModifiedOn = DateTimeOffset.UtcNow, // Set to current time
                            IsActive = orderDetail.IsActive
                        });
                    }

                    await _dbContext.BulkInsertAsync(pharmacyOrderDetails);

                    await _dbContext.SaveChangesAsync();

                }


            }
            else
            {
                var patientCunsultation = new MedicalConsultation()
                {
                    ConsultationId = Guid.NewGuid(),
                    PatientId = patientInformation.PatientId.Value,
                    DoctorId = patientInformation.DoctorId.Value,
                    ConsultationDate = DateTimeOffset.UtcNow,
                    ConsultationTime = DateTimeOffset.UtcNow.TimeOfDay,
                    Symptoms = patientInformation.patientCunsultation.Symptoms, // Assuming you have this in patientInformation
                    Diagnosis = patientInformation.patientCunsultation.Diagnosis, // Assuming you have this in patientInformation
                    Remarks = patientInformation.patientCunsultation.Remarks, // Assuming you have this in patientInformation
                    CreatedBy = patientInformation.CreatedBy,
                    CreatedOn = DateTimeOffset.UtcNow,
                    IsActive = true,
                    ModifiedBy = patientInformation.ModifiedBy,
                    ModifiedOn = patientInformation.ModifiedOn,
                };
                await _dbContext.medicalConsultations.AddAsync(patientCunsultation);

                var patientCunsultationDetail = new ConsultationDetails()
                {
                    Advice = patientInformation.patientCunsultation.patientConsultationDetails.Advice,
                    ConsultationId = patientCunsultation.ConsultationId,
                    DetailId = patientInformation.patientCunsultation.patientConsultationDetails.DetailId.Value,
                    FollowUpDate = patientInformation.patientCunsultation.patientConsultationDetails.FollowUpDate,
                    Diagnosis = patientInformation.patientCunsultation.patientConsultationDetails.Diagnosis,
                    Treatment = patientInformation.patientCunsultation.patientConsultationDetails.Treatment,
                    CreatedBy = patientInformation.CreatedBy,
                    CreatedOn = DateTimeOffset.UtcNow,
                    IsActive = true,
                    ModifiedBy = patientInformation.ModifiedBy,
                    ModifiedOn = patientInformation.ModifiedOn,
                };
                await _dbContext.consultationDetails.AddAsync(patientCunsultationDetail);


                var pharmacyOrder = new PharmacyOrder()
                {
                    OrderId = Guid.NewGuid(),
                    ConsultationId = patientCunsultation.ConsultationId,
                    PatientId = patientInformation.PatientId.Value,
                    ItemsQty = patientInformation.patientPharmacyOrder.ItemsQty,
                    TotalAmount = patientInformation.patientPharmacyOrder.TotalAmount,
                    CreatedBy = patientInformation.CreatedBy,
                    CreatedOn = DateTimeOffset.UtcNow,
                    IsActive = true
                };

                await _dbContext.pharmacyOrders.AddAsync(pharmacyOrder);

                var pharmacyOrderDetails = new List<PharmacyOrderDetail>();

                foreach (var orderDetail in patientInformation.patientPharmacyOrder.patientPharmacyOrderDetails)
                {
                    pharmacyOrderDetails.Add(new PharmacyOrderDetail()
                    {
                        OrderDetailId = Guid.NewGuid(),
                        OrderId = pharmacyOrder.OrderId,
                        MedicineId = orderDetail.MedicineId,
                        Quantity = orderDetail.Quantity,
                        PricePerUnit = orderDetail.PricePerUnit,
                        TotalPrice = orderDetail.TotalPrice,
                        CreatedBy = orderDetail.CreatedBy,
                        CreatedOn = DateTimeOffset.UtcNow,
                        ModifiedBy = orderDetail.ModifiedBy,
                        ModifiedOn = DateTimeOffset.UtcNow,
                        IsActive = orderDetail.IsActive
                    });
                }
                await _dbContext.BulkInsertAsync(pharmacyOrderDetails);


                var labOrder = new LabOrder()
                {
                    ConsultationId = patientCunsultation.ConsultationId,
                    LabOrderId = Guid.NewGuid(),
                    OrderDate = DateTimeOffset.UtcNow,
                    PatientId = patientCunsultation.PatientId,
                    TotalAmount = patientInformation.patientLabOrder.TotalAmount,
                    CreatedBy = patientInformation.CreatedBy,
                    CreatedOn = DateTimeOffset.UtcNow,
                    IsActive = true,
                    ModifiedBy = patientInformation.ModifiedBy,
                    ModifiedOn = DateTimeOffset.UtcNow,
                };
                await _dbContext.labOrders.AddAsync(labOrder);

                var laborderDetail = new List<LabOrderDetail>();

                foreach (var patientLabOrderDetail in patientInformation.patientLabOrder.patientLabOrderDetails)
                {
                    laborderDetail.Add(new LabOrderDetail()
                    {
                        LabOrderDetailId = Guid.NewGuid(),
                        LabOrderId = labOrder.LabOrderId,
                        Quantity = patientLabOrderDetail.Quantity,
                        PricePerUnit = patientLabOrderDetail.PricePerUnit,
                        TestId = patientLabOrderDetail.TestId,
                        TotalPrice = patientLabOrderDetail.TotalPrice,
                        ModifiedBy = patientLabOrderDetail.ModifiedBy,
                        CreatedBy = patientLabOrderDetail.CreatedBy,
                        ModifiedOn = DateTimeOffset.UtcNow,
                        CreatedOn = DateTimeOffset.UtcNow
                    });
                }

                await _dbContext.BulkInsertAsync(laborderDetail);

                await _dbContext.SaveChangesAsync();
            }

            await _dbContext.SaveChangesAsync();

            return patientInformation;
        }
    }
}
