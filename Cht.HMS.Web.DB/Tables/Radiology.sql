CREATE TABLE [dbo].[Radiology]
(
    [RadiologyId]     UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [PatientId]       UNIQUEIDENTIFIER NOT NULL,
    [TestId]          UNIQUEIDENTIFIER NOT NULL, -- Foreign key to LabTests
    [Result]          VARCHAR(500)       NULL,
    [TestDate]        DATETIME           NULL,
    [CreatedBy]       UNIQUEIDENTIFIER   NULL,
    [CreatedOn]       DATETIMEOFFSET     DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]      UNIQUEIDENTIFIER   NULL,
    [ModifiedOn]      DATETIMEOFFSET     NULL,
    [IsActive]        BIT                DEFAULT 1,
    FOREIGN KEY (TestId) REFERENCES [dbo].[LabTests](TestId),
    FOREIGN KEY (PatientId) REFERENCES [dbo].[PatientRegistration](PatientId)
);