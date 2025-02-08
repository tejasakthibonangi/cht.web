/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
-- Update the RoleId for all doctors in the User table
UPDATE [dbo].[User]
SET RoleId = 'C886AD1F-1694-4A71-A334-2BDF237789A9',
PasswordHash='IcsrWH6xCWNIfGx8T4gNrcYJrqP462LYVAMmND2gDdmia7sQ147bFf+Iu89S+V5Yyxe2T7lW19B+8yJiedcM0BG2DYrCz//q/fpPcTda5naDEoXVDEGJlYT0nq+qYmsWENmf25lBA0y/HaM6/90qfkqQrBEWgclMD1wHkwz/85wTXmCpwM0y0XKvyp9f9h7Lk9EiE0DD0iDON6drCePvvKN7RgGhxlW/DSPai6sZ4LPQZ0ke3itlXAhbgnkyiU2CDJ0oXQYFxzQU3sk0fQjVEPcotC56fRxIkW7lHLw1FPwQABLSSR7rtPnLAKpNEgOif9pt62BRCcYdshq4xw0YHA==',
PasswordSalt='VitC3V5nF6/BJmRrNboyxcmRbDqGRJ4Q8LeCy1eDm/nSw1CQEcChv0ARyGPaLeOh7TZhJiGmOgrD+oHnxjs1TQ==',
LastPasswordChangedOn=GETDATE(),
IsBlocked=0,
IsActive=1,
ModifiedBy=NEWID(),
ModifiedOn=GETDATE(),
CreatedBy=NEWID(),
CreatedOn=GETDATE()
WHERE Email IN (
    'samuel.scott@example.com',
    'sarah.martinez@example.com',
    'william.harris@example.com',
    'emily.davis@example.com',
    'james.thomas@example.com',
    'daniel.hall@example.com',
    'amelia.king@example.com',
    'benjamin.lewis@example.com',
    'mia.allen@example.com',
    'henry.young@example.com',
    'sophia.clark@example.com',
    'john.smith@example.com',
    'jane.doe@example.com',
    'michael.wilson@example.com',
    'alice.johnson@example.com',
    'david.anderson@example.com',
    'ava.walker@example.com',
    'laura.taylor@example.com',
    'olivia.white@example.com',
    'robert.brown@example.com'
);