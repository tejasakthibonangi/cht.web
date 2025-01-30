CREATE TABLE [dbo].[Pharmacy]
(
    [PharmacyId]       UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [PrescriptionId]    UNIQUEIDENTIFIER NOT NULL,
    [MedicineName]      VARCHAR(200)       NOT NULL,
    [Quantity]          INT                NOT NULL,
    [Price]             DECIMAL(10,2)      NOT NULL,
    [CreatedBy]         UNIQUEIDENTIFIER   NULL,
    [CreatedOn]         DATETIMEOFFSET     DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]        UNIQUEIDENTIFIER   NULL,
    [ModifiedOn]        DATETIMEOFFSET     NULL,
    [IsActive]          BIT                DEFAULT 1,
    FOREIGN KEY (PrescriptionId) REFERENCES [dbo].[Prescription](PrescriptionId)
);