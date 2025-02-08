using Cht.HMS.Web.API.DBConfiguration;
using Cht.HMS.Web.API.Manager;
using Cht.HMS.Web.API.Models;
using Cht.HMS.Web.Utility;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Cht.HMS.Web.API.DataManager
{
    public class PharmacyOrderDataManager : IPharmacyOrderManager
    {
        private readonly ApplicationDBContext _dbContext;
        public PharmacyOrderDataManager(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<PatientPharmacyOrder> GetPatientPharmacyOrderAsync(Guid patientId)
        {
            PatientPharmacyOrder patientPharmacyOrder = new PatientPharmacyOrder();


            var pharmacyOrder = await _dbContext.pharmacyOrders
                        .Where(x => x.PatientId == patientId)
                        .FirstOrDefaultAsync();

            var pharmacyOrderDetail = await _dbContext.pharmacyOrderDetails
                .Where(x => x.OrderId == pharmacyOrder.OrderId)
                .ToListAsync();

            patientPharmacyOrder = new PatientPharmacyOrder
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
                patientPharmacyOrderDetails = pharmacyOrderDetail.Select(detail => new PatientPharmacyOrderDetail
                {
                    OrderDetailId = detail.OrderDetailId,
                    OrderId = detail.OrderId,
                    MedicineId = detail.MedicineId,
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
            return patientPharmacyOrder;
        }

        public async Task<List<PatientPharmacyOrder>> GetPatientPharmacyOrdersAsync(Guid patientId)
        {
            var pharmacyOrders = await _dbContext.pharmacyOrders
         .Where(x => x.PatientId == patientId)
         .ToListAsync();

            var patientPharmacyOrders = new List<PatientPharmacyOrder>();

            foreach (var pharmacyOrder in pharmacyOrders)
            {
                var pharmacyOrderDetails = await _dbContext.pharmacyOrderDetails
                    .Where(x => x.OrderId == pharmacyOrder.OrderId)
                    .ToListAsync();

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
                    patientPharmacyOrderDetails = pharmacyOrderDetails.Select(detail => new PatientPharmacyOrderDetail
                    {
                        OrderDetailId = detail.OrderDetailId,
                        OrderId = detail.OrderId,
                        MedicineId = detail.MedicineId,
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

                patientPharmacyOrders.Add(patientPharmacyOrder);
            }

            return patientPharmacyOrders;
        }

        public async Task<PharmacyOrder> GetPharmacyOrderAsync(Guid patientId)
        {
            return await _dbContext.pharmacyOrders.Where(x => x.PatientId == patientId).FirstOrDefaultAsync();
        }

        public async Task<List<PharmacyOrder>> GetPharmacyOrdersAsync()
        {
            return await _dbContext.pharmacyOrders.ToListAsync();
        }

        public async Task<List<PharmacyOrder>> GetPharmacyOrdersAsync(Guid patientId)
        {
            return await _dbContext.pharmacyOrders.Where(x => x.PatientId == patientId).ToListAsync();
        }

        public async Task<PharmacyOrder> InsertOrUpdatePharmacyOrdersAsync(PharmacyOrder pharmacyOrder)
        {
            if (pharmacyOrder.OrderId == Guid.Empty)
            {
                _dbContext.pharmacyOrders.AddAsync(pharmacyOrder);
                _dbContext.SaveChangesAsync();
            }
            else
            {

            }
            return pharmacyOrder;
        }

        public async Task<PharmacyOrder> InsertOrUpdatePharmacyOrdersAsync(PatientPharmacyOrder pharmacyOrder)
        {
            // Check if the order already exists
            var existingOrder = await _dbContext.pharmacyOrders
                .FirstOrDefaultAsync(x => x.OrderId == pharmacyOrder.OrderId);

            if (existingOrder == null)
            {
                // Create a new PharmacyOrder
                var newOrder = new PharmacyOrder
                {
                    OrderId = pharmacyOrder.OrderId ?? Guid.NewGuid(),
                    PatientId = pharmacyOrder.PatientId,
                    ConsultationId = pharmacyOrder.ConsultationId,
                    OrderDate = pharmacyOrder.OrderDate,
                    ItemsQty = pharmacyOrder.ItemsQty,
                    TotalAmount = pharmacyOrder.TotalAmount,
                    CreatedBy = pharmacyOrder.CreatedBy,
                    CreatedOn = DateTimeOffset.UtcNow,
                    IsActive = pharmacyOrder.IsActive
                };

                // Add the new order to the database
                await _dbContext.pharmacyOrders.AddAsync(newOrder);
                await _dbContext.SaveChangesAsync();

                // Insert order details
                foreach (var detail in pharmacyOrder.patientPharmacyOrderDetails)
                {
                    var orderDetail = new PharmacyOrderDetail
                    {
                        OrderDetailId = detail.OrderDetailId ?? Guid.NewGuid(),
                        OrderId = newOrder.OrderId,
                        MedicineId = detail.MedicineId,
                        Quantity = detail.Quantity,
                        PricePerUnit = detail.PricePerUnit,
                        TotalPrice = detail.TotalPrice,
                        CreatedBy = detail.CreatedBy,
                        CreatedOn = DateTimeOffset.UtcNow,
                        IsActive = detail.IsActive
                    };

                    await _dbContext.pharmacyOrderDetails.AddAsync(orderDetail);
                }

                await _dbContext.SaveChangesAsync();
                return newOrder;
            }
            else
            {
                // Update existing PharmacyOrder
                existingOrder.PatientId = pharmacyOrder.PatientId;
                existingOrder.ConsultationId = pharmacyOrder.ConsultationId;
                existingOrder.OrderDate = pharmacyOrder.OrderDate;
                existingOrder.ItemsQty = pharmacyOrder.ItemsQty;
                existingOrder.TotalAmount = pharmacyOrder.TotalAmount;
                existingOrder.ModifiedBy = pharmacyOrder.CreatedBy; // Assuming CreatedBy is used for ModifiedBy
                existingOrder.ModifiedOn = DateTimeOffset.UtcNow;
                existingOrder.IsActive = pharmacyOrder.IsActive;

                _dbContext.pharmacyOrders.Update(existingOrder);
                await _dbContext.SaveChangesAsync();

                // Update order details
                foreach (var detail in pharmacyOrder.patientPharmacyOrderDetails)
                {
                    var existingDetail = await _dbContext.pharmacyOrderDetails
                        .FirstOrDefaultAsync(x => x.OrderDetailId == detail.OrderDetailId);

                    if (existingDetail != null)
                    {
                        // Update existing order detail
                        existingDetail.Quantity = detail.Quantity;
                        existingDetail.PricePerUnit = detail.PricePerUnit;
                        existingDetail.TotalPrice = detail.TotalPrice;
                        existingDetail.ModifiedBy = detail.CreatedBy; // Assuming CreatedBy is used for ModifiedBy
                        existingDetail.ModifiedOn = DateTimeOffset.UtcNow;
                        existingDetail.IsActive = detail.IsActive;

                        _dbContext.pharmacyOrderDetails.Update(existingDetail);
                    }
                    else
                    {
                        // Insert new order detail
                        var newDetail = new PharmacyOrderDetail
                        {
                            OrderDetailId = Guid.NewGuid(),
                            OrderId = existingOrder.OrderId,
                            MedicineId = detail.MedicineId,
                            Quantity = detail.Quantity,
                            PricePerUnit = detail.PricePerUnit,
                            TotalPrice = detail.TotalPrice,
                            CreatedBy = detail.CreatedBy,
                            CreatedOn = DateTimeOffset.UtcNow,
                            IsActive = detail.IsActive
                        };

                        await _dbContext.pharmacyOrderDetails.AddAsync(newDetail);
                    }
                }

                await _dbContext.SaveChangesAsync();
                return existingOrder;
            }
        }
    }
}
