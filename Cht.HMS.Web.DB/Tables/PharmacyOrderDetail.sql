CREATE TABLE [dbo].[PharmacyOrderDetail]
(
    [OrderDetailId]         UNIQUEIDENTIFIER   NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [OrderId]               UNIQUEIDENTIFIER   NOT NULL,
    [MedicineId]            UNIQUEIDENTIFIER   NOT NULL,
    [Quantity]              INT                NOT NULL,
    [PricePerUnit]          DECIMAL(10, 2)     NOT NULL,
    [TotalPrice]            DECIMAL(10, 2)     NOT NULL,
    [CreatedBy]             UNIQUEIDENTIFIER   NULL,
    [CreatedOn]             DATETIMEOFFSET     DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]            UNIQUEIDENTIFIER   NULL,
    [ModifiedOn]            DATETIMEOFFSET     NULL,
    [IsActive]               BIT                DEFAULT 1,
    FOREIGN KEY (OrderId) REFERENCES [dbo].[PharmacyOrder](OrderId) ON DELETE CASCADE,
    FOREIGN KEY (MedicineId) REFERENCES [dbo].[Medicines](MedicineId) ON DELETE CASCADE
);