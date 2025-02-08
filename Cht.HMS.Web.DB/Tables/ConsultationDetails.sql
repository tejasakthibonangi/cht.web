CREATE TABLE [dbo].[ConsultationDetails]
(
    [DetailId]             UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [ConsultationId]       UNIQUEIDENTIFIER   NULL,
    [Diagnosis]            VARCHAR(500)       NULL,
    [Treatment]            VARCHAR(500)       NULL,
    [Advice]               VARCHAR(500)       NULL,
    [FollowUpDate]         DATE               NULL,
    [CreatedBy]            UNIQUEIDENTIFIER   NULL,
    [CreatedOn]            DATETIMEOFFSET     DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]           UNIQUEIDENTIFIER   NULL,
    [ModifiedOn]           DATETIMEOFFSET     NULL,
    [IsActive]             BIT                DEFAULT 1,
    FOREIGN KEY (ConsultationId) REFERENCES [dbo].[MedicalConsultation](ConsultationId)
);