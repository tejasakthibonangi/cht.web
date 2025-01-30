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
MERGE INTO [dbo].[PatientType] AS target
USING (
    VALUES 
        ('Inpatient', 'Patients who are admitted to the hospital for at least one night'),
        ('Outpatient', 'Patients who receive care without being admitted to the hospital'),
        ('Emergency', 'Patients who require immediate medical attention'),
        ('Pediatric', 'Patients who are children or adolescents'),
        ('Geriatric', 'Patients who are elderly'),
        ('Maternity', 'Patients who are pregnant or have recently given birth'),
        ('Chronic', 'Patients with long-term health conditions'),
        ('Preventive', 'Patients receiving care to prevent illness'),
        ('Rehabilitation', 'Patients undergoing therapy to recover from illness or injury'),
        ('Palliative', 'Patients receiving care to relieve symptoms of serious illness')
    -- Add more patient types as needed
) AS source ([PatientTypeName], [Description])
ON target.[PatientTypeName] = source.[PatientTypeName]
WHEN NOT MATCHED THEN
    INSERT ([PatientTypeName], [Description], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive])
    VALUES (source.[PatientTypeName], source.[Description], NULL, SYSDATETIMEOFFSET(), NULL, NULL, 1);