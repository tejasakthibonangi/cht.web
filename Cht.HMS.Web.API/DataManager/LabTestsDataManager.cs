using Cht.HMS.Web.API.DBConfiguration;
using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;
using Cht.HMS.Web.Utility;
using Microsoft.EntityFrameworkCore;

namespace Cht.HMS.Web.API.DataManager
{
    public class LabTestsDataManager : ILabTestsManager
    {
        private readonly ApplicationDBContext _dBContext;
        private readonly IPatientManager _patientManager;
        public LabTestsDataManager(ApplicationDBContext dBContext, IPatientManager patientManager)
        {
            _dBContext = dBContext;
            _patientManager = patientManager;
        }

        public async Task<List<LabOrderInfirmation>> GetLabOrdersAsync()
        {
            var labOrderInfirmation = new List<LabOrderInfirmation>();

            List<LabOrderDetail> labOrderDetails = new List<LabOrderDetail>();

            // Fetch necessary data
            var patientInfirmations = await _patientManager.GetPatientInformationsAsync(string.Empty);
            var patientCunsultations = await _dBContext.medicalConsultations.ToListAsync();
            var patientCunsultationDetails = await _dBContext.consultationDetails.ToListAsync();
            var labOrders = await _dBContext.labOrders.ToListAsync();
            
            labOrderDetails = await _dBContext.labOrderDetails.ToListAsync();

            // Check if there are any lab orders
            if (!labOrders.Any())
            {
                return labOrderInfirmation;
            }

            // Loop through each lab order
            foreach (var labOrder in labOrders)
            {
                // Create a new LabOrder object
                var patientLabOrder = new PatientLabOrder
                {
                    LabOrderId = labOrder.LabOrderId,
                    PatientId = labOrder.PatientId,
                    ConsultationId = labOrder.ConsultationId,
                    OrderDate = labOrder.OrderDate.Value,
                    TotalAmount = labOrder.TotalAmount,
                    CreatedBy = labOrder.CreatedBy,
                    CreatedOn = labOrder.CreatedOn,
                    ModifiedBy = labOrder.ModifiedBy,
                    ModifiedOn = labOrder.ModifiedOn,
                    IsActive = labOrder.IsActive,
                    patientLabOrderDetails = labOrderDetails
                        .Where(detail => detail.LabOrderId == labOrder.LabOrderId)
                        .Select(detail => new PatientLabOrderDetail
                        {
                            LabOrderDetailId = detail.LabOrderDetailId,
                            LabOrderId = detail.LabOrderId,
                            TestId = detail.TestId,
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

                // Fetch patient information
                var patientInfirmation = patientInfirmations.FirstOrDefault(x => x.PatientId == labOrder.PatientId);
                var patientCunsultation = patientCunsultations.FirstOrDefault(x => x.PatientId == labOrder.PatientId);
                var patientCunsultationDetailsForConsultation = patientCunsultationDetails.FirstOrDefault(x => x.ConsultationId == patientCunsultation?.ConsultationId);

                if (patientCunsultation != null)
                {
                    // Create a new consultation object
                    var consultation = new PatientCunsultation
                    {
                        PatientId = labOrder.PatientId,
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

                    // Add the lab order information to the list
                    labOrderInfirmation.Add(new LabOrderInfirmation
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
                        patientLabOrder = patientLabOrder,
                        patientCunsultation = consultation
                    });
                }
            }

            return labOrderInfirmation;
        }

        public Task<LabTests> GetLabTestByIdAsync(Guid TestId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<LabTests>> GetLabTestsAsync()
        {
            return await _dBContext.labTests.ToListAsync();
        }

        public Task<LabTests> InsertOrUpdateLabTestAsync(LabTests labTests)
        {
            throw new NotImplementedException();
        }
    }
}
