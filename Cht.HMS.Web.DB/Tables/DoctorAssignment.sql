CREATE TABLE [dbo].[DoctorAssignment]
(
    [AssignmentId]           UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [PatientId]              UNIQUEIDENTIFIER NOT NULL,
    [DoctorId]               UNIQUEIDENTIFIER NOT NULL,
    [AssignmentDate]         DATE               NOT NULL DEFAULT GETDATE(),
    [AssignmentTime]         TIME               NOT NULL DEFAULT CAST(GETDATE() AS TIME),
    [AssignedBy]             UNIQUEIDENTIFIER   NULL,
    [Remarks]                VARCHAR(500)       NULL,
    [CreatedBy]              UNIQUEIDENTIFIER   NULL,
    [CreatedOn]              DATETIMEOFFSET     DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]             UNIQUEIDENTIFIER   NULL,
    [ModifiedOn]             DATETIMEOFFSET     NULL,
    [IsActive]               BIT                DEFAULT 1,
    FOREIGN KEY (PatientId) REFERENCES [dbo].[PatientRegistration](PatientId),
    FOREIGN KEY (DoctorId) REFERENCES [dbo].[Doctor](DoctorId)
);