CREATE TABLE [dbo].[Treatment]
(
    [TreatmentId]            UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [ConsultationId]         UNIQUEIDENTIFIER NOT NULL,
    [TreatmentDescription]   VARCHAR(500)       NOT NULL,
    [CreatedBy]              UNIQUEIDENTIFIER   NULL,
    [CreatedOn]              DATETIMEOFFSET     DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]             UNIQUEIDENTIFIER   NULL,
    [ModifiedOn]             DATETIMEOFFSET     NULL,
    [IsActive]               BIT                DEFAULT 1,
    FOREIGN KEY (ConsultationId) REFERENCES [dbo].[MedicalConsultation](ConsultationId)
);