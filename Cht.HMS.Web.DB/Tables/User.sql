CREATE TABLE [dbo].[User]
(
	[Id]				uniqueidentifier			NOT NULL		PRIMARY KEY			default				newid(),
	[FirstName]         varchar(250)                    NULL,
	[LastName]          varchar(250)                    NULL,
	[Email]             varchar(250)                    NULL,
	[Phone]             varchar(14)                     NULL,
	[PasswordHash]      nvarchar(max)                   NULL,
	[PasswordSalt]      nvarchar(max)                   NULL,
	[RoleId]            uniqueidentifier                NULL,
	[LastPasswordChangedOn] datetimeoffset              NULL,
	[IsBlocked]         bit                             NULL,
	[CreatedBy]			uniqueidentifier				NULL,
	[CreatedOn]			datetimeoffset					NULL,
	[ModifiedBy]		uniqueidentifier				NULL,
	[ModifiedOn]		datetimeoffset					NULL,
	[IsActive]			bit								NULL,
	FOREIGN KEY (RoleId) REFERENCES Role(RoleId),
)
