using Cht.HMS.Web.API.DBConfiguration;
using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;
using Cht.HMS.Web.Utility;
using Microsoft.EntityFrameworkCore;

namespace Cht.HMS.Web.API.DataManager
{
    public class LabOrderDataManager : ILabOrderManager
    {
        private readonly ApplicationDBContext _dbContext;

        public LabOrderDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<LabOrder>> GetLabOrdersAsync()
        {
            return await _dbContext.labOrders.ToListAsync();
        }

        public async Task<List<PatientLabOrder>> GetLabOrdersAsync(Guid patientId)
        {
            var labOrders = await _dbContext.labOrders
                .Where(x => x.PatientId == patientId)
                .ToListAsync();

            var patientLabOrders = new List<PatientLabOrder>();

            foreach (var labOrder in labOrders)
            {
                var labOrderDetails = await _dbContext.labOrderDetails
                    .Where(x => x.LabOrderId == labOrder.LabOrderId)
                    .ToListAsync();

                var patientLabOrder = new PatientLabOrder
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
                    patientLabOrderDetails = labOrderDetails.Select(detail => new PatientLabOrderDetail
                    {
                        LabOrderDetailId = detail.LabOrderDetailId,
                        LabOrderId = detail.LabOrderId,
                        TestId = detail.TestId,
                        Quantity = detail.Quantity,
                        PricePerUnit = detail.PricePerUnit,
                        TotalPrice = detail.TotalPrice,
                        CreatedBy = detail.CreatedBy,
                        CreatedOn = detail.CreatedOn,
                        ModifiedBy = detail.ModifiedBy,
                        ModifiedOn = detail.ModifiedOn,
                        IsActive = detail.IsActive
                    }).ToList()
                };

                patientLabOrders.Add(patientLabOrder);
            }

            return patientLabOrders;
        }

        public async Task<PatientLabOrder> GetPatientLabOrderAsync(Guid patientId)
        {
            var labOrder = await _dbContext.labOrders
                .Where(x => x.PatientId == patientId)
                .FirstOrDefaultAsync();

            if (labOrder == null)
            {
                return null;
            }

            var labOrderDetails = await _dbContext.labOrderDetails
                .Where(x => x.LabOrderId == labOrder.LabOrderId)
                .ToListAsync();

            return new PatientLabOrder
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
                patientLabOrderDetails = labOrderDetails.Select(detail => new PatientLabOrderDetail
                {
                    LabOrderDetailId = detail.LabOrderDetailId,
                    LabOrderId = detail.LabOrderId,
                    TestId = detail.TestId,
                    Quantity = detail.Quantity,
                    PricePerUnit = detail.PricePerUnit,
                    TotalPrice = detail.TotalPrice,
                    CreatedBy = detail.CreatedBy,
                    CreatedOn = detail.CreatedOn,
                    ModifiedBy = detail.ModifiedBy,
                    ModifiedOn = detail.ModifiedOn,
                    IsActive = detail.IsActive
                }).OrderByDescending(x => x.CreatedOn).ToList()
            };
        }

        public async Task<PatientLabOrder> InsertOrUpdatePatientLabOrderAsync(PatientLabOrder patientLabOrder)
        {
            var existingOrder = await _dbContext.labOrders
                .FirstOrDefaultAsync(x => x.LabOrderId == patientLabOrder.LabOrderId);

            if (existingOrder == null)
            {
                var newOrder = new LabOrder
                {
                    LabOrderId = patientLabOrder.LabOrderId ?? Guid.NewGuid(),
                    PatientId = patientLabOrder.PatientId,
                    ConsultationId = patientLabOrder.ConsultationId,
                    OrderDate = patientLabOrder.OrderDate,
                    TotalAmount = patientLabOrder.TotalAmount,
                    CreatedBy = patientLabOrder.CreatedBy,
                    CreatedOn = DateTimeOffset.UtcNow,
                    IsActive = patientLabOrder.IsActive
                };

                await _dbContext.labOrders.AddAsync(newOrder);
                await _dbContext.SaveChangesAsync();

                foreach (var detail in patientLabOrder.patientLabOrderDetails)
                {
                    var newDetail = new LabOrderDetail
                    {
                        LabOrderDetailId = detail.LabOrderDetailId ?? Guid.NewGuid(),
                        LabOrderId = newOrder.LabOrderId,
                        TestId = detail.TestId,
                        Quantity = detail.Quantity,
                        PricePerUnit = detail.PricePerUnit,
                        TotalPrice = detail.TotalPrice,
                        CreatedBy = detail.CreatedBy,
                        CreatedOn = DateTimeOffset.UtcNow,
                        IsActive = detail.IsActive
                    };

                    await _dbContext.labOrderDetails.AddAsync(newDetail);
                }

                await _dbContext.SaveChangesAsync();
                return patientLabOrder;
            }
            else
            {
                existingOrder.PatientId = patientLabOrder.PatientId;
                existingOrder.ConsultationId = patientLabOrder.ConsultationId;
                existingOrder.OrderDate = patientLabOrder.OrderDate;
                existingOrder.TotalAmount = patientLabOrder.TotalAmount;
                existingOrder.ModifiedBy = patientLabOrder.CreatedBy; 
                existingOrder.ModifiedOn = DateTimeOffset.UtcNow;
                existingOrder.IsActive = patientLabOrder.IsActive;

                _dbContext.labOrders.Update(existingOrder);
                await _dbContext.SaveChangesAsync();

                foreach (var detail in patientLabOrder.patientLabOrderDetails)
                {
                    var existingDetail = await _dbContext.labOrderDetails
                        .FirstOrDefaultAsync(x => x.LabOrderDetailId == detail.LabOrderDetailId);

                    if (existingDetail != null)
                    {
                        existingDetail.Quantity = detail.Quantity;
                        existingDetail.PricePerUnit = detail.PricePerUnit;
                        existingDetail.TotalPrice = detail.TotalPrice;
                        existingDetail.ModifiedBy = detail.CreatedBy; 
                        existingDetail.ModifiedOn = DateTimeOffset.UtcNow;
                        existingDetail.IsActive = detail.IsActive;

                        _dbContext.labOrderDetails.Update(existingDetail);
                    }
                    else
                    {
                        var newDetail = new LabOrderDetail
                        {
                            LabOrderDetailId = Guid.NewGuid(),
                            LabOrderId = existingOrder.LabOrderId,
                            TestId = detail.TestId,
                            Quantity = detail.Quantity,
                            PricePerUnit = detail.PricePerUnit,
                            TotalPrice = detail.TotalPrice,
                            CreatedBy = detail.CreatedBy,
                            CreatedOn = DateTimeOffset.UtcNow,
                            IsActive = detail.IsActive
                        };

                        await _dbContext.labOrderDetails.AddAsync(newDetail);
                    }
                }

                await _dbContext.SaveChangesAsync();
                return patientLabOrder;
            }
        }

    }
}
