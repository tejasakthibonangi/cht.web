CREATE TABLE [dbo].[LabTests]
(
    [TestId]          UNIQUEIDENTIFIER   NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [TestName]        VARCHAR(200)       NOT NULL,
    [TestDescription] VARCHAR(500)       NULL,
    [CreatedBy]       UNIQUEIDENTIFIER   NULL,
    [CreatedOn]       DATETIMEOFFSET     DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]      UNIQUEIDENTIFIER   NULL,
    [ModifiedOn]      DATETIMEOFFSET     NULL,
    [IsActive]        BIT                DEFAULT 1
);