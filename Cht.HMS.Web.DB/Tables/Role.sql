CREATE TABLE [dbo].[Role]
(
	[RoleId]                        uniqueidentifier       NOT NULL    PRIMARY KEY   Default     newid(),
	[Name]                          varchar(max)           NULL,
	[Code]                          varchar(max)           NULL,
	[CreatedBy]                     uniqueidentifier       NULL,
	[CreatedOn]                     datetimeoffset         NULL,
	[ModifiedBy]                    uniqueidentifier       NULL,
	[ModifiedOn]                    datetimeoffset         NULL,
	[IsActive]                      bit                    NULL
)
