CREATE TABLE [dbo].[Doctor]
(
    [DoctorId]               UNIQUEIDENTIFIER   NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [DoctorName]             VARCHAR(150)       NOT NULL,
    [Specialty]              VARCHAR(100)       NULL,
    [Phone]                  VARCHAR(15)        NULL,
    [Email]                  VARCHAR(100)       NULL,
    [CreatedBy]              UNIQUEIDENTIFIER   NULL,
    [CreatedOn]              DATETIMEOFFSET     DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]             UNIQUEIDENTIFIER   NULL,
    [ModifiedOn]             DATETIMEOFFSET     NULL,
    [IsActive]               BIT                DEFAULT 1
);