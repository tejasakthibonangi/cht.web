CREATE TABLE [dbo].[MedicalConsultation]
(
    [ConsultationId]        UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [PatientId]             UNIQUEIDENTIFIER NOT NULL,
    [DoctorId]              UNIQUEIDENTIFIER NOT NULL,
    [ConsultationDate]      DATETIMEOFFSET     NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    [ConsultationTime]      TIME               NOT NULL DEFAULT CAST(GETDATE() AS TIME),
    [Symptoms]              VARCHAR(500)       NULL,
    [Diagnosis]             VARCHAR(500)       NULL,
    [Remarks]               VARCHAR(500)       NULL,
    [CreatedBy]             UNIQUEIDENTIFIER   NULL,
    [CreatedOn]             DATETIMEOFFSET     DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]            UNIQUEIDENTIFIER   NULL,
    [ModifiedOn]            DATETIMEOFFSET     NULL,
    [IsActive]              BIT                DEFAULT 1,
    FOREIGN KEY (PatientId) REFERENCES [dbo].[PatientRegistration](PatientId),
    FOREIGN KEY (DoctorId)  REFERENCES [dbo].[Doctor](DoctorId)
);