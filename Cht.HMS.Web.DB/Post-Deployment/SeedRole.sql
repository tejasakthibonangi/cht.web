﻿/*
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
MERGE INTO [dbo].[Role] AS target
USING (
    VALUES 
        ('Patient', 'Patient'),
        ('Doctor', 'Doctor'),
        ('X-ray technicians', 'X-ray technicians'),
        ('Lab technicians', 'Lab technicians'),
        ('Pharmacist', 'Pharmacist'),
        ('Nurse', 'Nurse'),
        ('Administrator', 'Administrator'),
        ('Executive', 'Executive')
    -- Add more roles as needed
) AS source ([Name], [Code])
ON target.[Name] = source.[Name]
WHEN NOT MATCHED THEN
    INSERT ([Name], [Code], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive])
    VALUES (source.[Name], source.[Code], NULL, SYSDATETIMEOFFSET(), NULL, NULL, 1);