using Cht.HMS.Web.API.DBConfiguration;
using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;
using Cht.HMS.Web.Utility;
using Microsoft.EntityFrameworkCore;

namespace Cht.HMS.Web.API.DataManager
{
    public class PharmacyDataManager : IPharmacyManager
    {
        private readonly ApplicationDBContext _dBContext;
        private readonly IPatientManager _patientManager;

        public PharmacyDataManager(ApplicationDBContext dBContext, IPatientManager patientManager)
        {
            _dBContext = dBContext;
            _patientManager = patientManager;
        }

        public async Task<List<PharmacyOrderInfirmation>> GetPatientPharmacyOrderAsync()
        {
            var patientPharmacies = new List<PharmacyOrderInfirmation>();

            var pharmacyOrders = await _dBContext.pharmacyOrders.OrderByDescending(x => x.CreatedOn).ToListAsync();
           
            var pharmacyOrderDetails = await _dBContext.pharmacyOrderDetails.ToListAsync();
            
            var patientInfirmations = await _patientManager.GetPatientInformationsAsync(string.Empty);
            
            var patientCunsultations = await _dBContext.medicalConsultations.ToListAsync();
            
            var patientCunsultationDetails = await _dBContext.consultationDetails.ToListAsync();

            if (!pharmacyOrders.Any())
            {
                return patientPharmacies; 
            }

            foreach (var pharmacyOrder in pharmacyOrders)
            {
                var patientPharmacyOrder = new PatientPharmacyOrder
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
                    patientPharmacyOrderDetails = pharmacyOrderDetails
                        .Where(detail => detail.OrderId == pharmacyOrder.OrderId)
                        .Select(detail => new PatientPharmacyOrderDetail
                        {
                            OrderDetailId = detail.OrderDetailId,
                            OrderId = detail.OrderId,
                            MedicineId = detail.MedicineId,
                            PricePerUnit = detail.PricePerUnit.GetValueOrDefault(),
                            Quantity = detail.Quantity,
                            TotalPrice = detail.TotalPrice.GetValueOrDefault(),
                            CreatedBy = detail.CreatedBy,
                            CreatedOn = detail.CreatedOn,
                            ModifiedBy = detail.ModifiedBy,
                            ModifiedOn = detail.ModifiedOn,
                            IsActive = detail.IsActive
                        }).ToList()
                };

                var patientInfirmation = patientInfirmations.FirstOrDefault(x => x.PatientId == pharmacyOrder.PatientId);
                var patientCunsultation = patientCunsultations.FirstOrDefault(x => x.PatientId == pharmacyOrder.PatientId);
                var patientCunsultationDetailsForConsultation = patientCunsultationDetails.FirstOrDefault(x => x.ConsultationId == patientCunsultation?.ConsultationId);

                if (patientCunsultation != null)
                {
                    var consultation = new PatientCunsultation
                    {
                        PatientId = pharmacyOrder.PatientId,
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
                        ModifiedOn = patientCunsultation.ModifiedOn,
                        patientConsultationDetails = patientCunsultationDetailsForConsultation != null ? new PatientConsultationDetails
                        {
                            ConsultationId = patientCunsultationDetailsForConsultation.ConsultationId,
                            Advice = patientCunsultationDetailsForConsultation.Advice,
                            CreatedBy = patientCunsultationDetailsForConsultation.CreatedBy,
                            CreatedOn = patientCunsultationDetailsForConsultation.CreatedOn,
                            DetailId = patientCunsultationDetailsForConsultation.DetailId,
                            Diagnosis = patientCunsultationDetailsForConsultation.Diagnosis,
                            FollowUpDate = patientCunsultationDetailsForConsultation.FollowUpDate,
                            IsActive = patientCunsultationDetailsForConsultation.IsActive,
                            ModifiedBy = patientCunsultationDetailsForConsultation.ModifiedBy,
                            ModifiedOn = patientCunsultationDetailsForConsultation.ModifiedOn,
                            Treatment = patientCunsultationDetailsForConsultation.Treatment
                        } : null
                    };

                    patientPharmacies.Add(new PharmacyOrderInfirmation
                    {
                        PatientId = patientInfirmation?.PatientId,
                        DateOfVisit = patientInfirmation?.DateOfVisit,
                        TimeOfVisit = patientInfirmation?.TimeOfVisit,
                        ComingFrom = patientInfirmation?.ComingFrom,
                        Reference = patientInfirmation?.Reference,
                        PatientName = patientInfirmation?.PatientName,
                        PhoneNo = patientInfirmation?.PhoneNo,
                        AlternatePhoneNo = patientInfirmation?.AlternatePhoneNo,
                        Email = patientInfirmation?.Email,
                        Gender = patientInfirmation?.Gender,
                        DOB = patientInfirmation?.DOB,
                        Height = patientInfirmation?.Height,
                        Weight = patientInfirmation?.Weight,
                        BP = patientInfirmation?.BP,
                        Sugar = patientInfirmation?.Sugar,
                        Temperature = patientInfirmation?.Temperature,
                        HealthIssues = patientInfirmation?.HealthIssues,
                        DoctorId = patientInfirmation?.DoctorId,
                        DoctorName = patientInfirmation?.DoctorName,
                        Fee = patientInfirmation?.Fee,
                        PreparedBy = patientInfirmation?.PreparedBy,
                        CurrentStatus = patientInfirmation?.CurrentStatus,
                        CreatedBy = patientInfirmation?.CreatedBy,
                        CreatedOn = patientInfirmation?.CreatedOn,
                        ModifiedBy = patientInfirmation?.ModifiedBy,
                        ModifiedOn = patientInfirmation?.ModifiedOn,
                        IsActive = patientInfirmation?.IsActive ?? false,
                        patientPharmacyOrder = patientPharmacyOrder,
                        patientCunsultation = consultation
                    });
                }
            }

            return patientPharmacies;
        }

        public Task<Pharmacy> GetPharmacyByIdAsync(Guid PharmacyId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Pharmacy>> GetPharmacysAsync()
        {
            return await _dBContext.pharmacys.ToListAsync();
        }

        public async Task<Pharmacy> InsertOrUpdatePharmacyAsync(Pharmacy pharmacy)
        {
            throw new NotImplementedException();
        }
    }
}
