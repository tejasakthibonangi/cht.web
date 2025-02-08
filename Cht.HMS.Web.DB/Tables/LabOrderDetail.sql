CREATE TABLE [dbo].[LabOrderDetail]
(
    [LabOrderDetailId]      UNIQUEIDENTIFIER   NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [LabOrderId]            UNIQUEIDENTIFIER   NOT NULL,
    [TestId]                UNIQUEIDENTIFIER   NOT NULL,
    [Quantity]              INT                NOT NULL,
    [PricePerUnit]          DECIMAL(10, 2)     NOT NULL,
    [TotalPrice]            DECIMAL(10, 2)     NOT NULL,
    [CreatedBy]             UNIQUEIDENTIFIER   NULL,
    [CreatedOn]             DATETIMEOFFSET     DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]            UNIQUEIDENTIFIER   NULL,
    [ModifiedOn]            DATETIMEOFFSET     NULL,
    [IsActive]              BIT                DEFAULT 1,
    FOREIGN KEY (LabOrderId) REFERENCES [dbo].[LabOrder](LabOrderId) ON DELETE CASCADE,
    FOREIGN KEY (TestId)    REFERENCES [dbo].[LabTests](TestId) ON DELETE CASCADE
);