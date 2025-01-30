CREATE TABLE [dbo].[PaymentType]
(
    [PaymentTypeId]     UNIQUEIDENTIFIER   NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [PaymentTypeName]   VARCHAR(100)       NOT NULL,
    [Description]       VARCHAR(500)       NULL,
    [CreatedBy]         UNIQUEIDENTIFIER   NULL,
    [CreatedOn]         DATETIMEOFFSET     DEFAULT SYSDATETIMEOFFSET(),
    [ModifiedBy]        UNIQUEIDENTIFIER   NULL,
    [ModifiedOn]        DATETIMEOFFSET     NULL,
    [IsActive]          BIT                DEFAULT 1
);