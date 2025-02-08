CREATE TABLE [dbo].[PharmacyOrder]
(
    [OrderId]                UNIQUEIDENTIFIER   NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [PatientId]              UNIQUEIDENTIFIER   NULL,
    [ConsultationId]         UNIQUEIDENTIFIER   NULL,
    [OrderDate]              DATETIMEOFFSET     DEFAULT SYSDATETIMEOFFSET(),
    [ItemsQty]               INT                DEFAULT 0,
    [TotalAmount]            DECIMAL(10, 2)     NULL,
    [CreatedBy]              UNIQUEIDENTIFIER   NULL,
    [CreatedOn]              DATETIMEOFFSET     DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]             UNIQUEIDENTIFIER   NULL,
    [ModifiedOn]             DATETIMEOFFSET     NULL,
    [IsActive]               BIT                DEFAULT 1,
    FOREIGN KEY (PatientId) REFERENCES PatientRegistration(PatientId) ON DELETE CASCADE
);