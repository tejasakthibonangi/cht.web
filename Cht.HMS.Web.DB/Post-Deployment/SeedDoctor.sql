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
MERGE INTO [dbo].[Doctor] AS target
USING (
    VALUES 
        ('Dr. John Smith', 'Cardiology', '123-456-7890', 'john.smith@example.com'),
        ('Dr. Jane Doe', 'Pediatrics', '234-567-8901', 'jane.doe@example.com'),
        ('Dr. Alice Johnson', 'Dermatology', '345-678-9012', 'alice.johnson@example.com'),
        ('Dr. Robert Brown', 'Orthopedics', '456-789-0123', 'robert.brown@example.com'),
        ('Dr. Emily Davis', 'Neurology', '567-890-1234', 'emily.davis@example.com'),
        ('Dr. Michael Wilson', 'Oncology', '678-901-2345', 'michael.wilson@example.com'),
        ('Dr. Sarah Martinez', 'Gynecology', '789-012-3456', 'sarah.martinez@example.com'),
        ('Dr. David Anderson', 'Psychiatry', '890-123-4567', 'david.anderson@example.com'),
        ('Dr. Laura Taylor', 'Endocrinology', '901-234-5678', 'laura.taylor@example.com'),
        ('Dr. James Thomas', 'Urology', '012-345-6789', 'james.thomas@example.com'),
        -- Add 90 more doctors here...
        ('Dr. Olivia White', 'Radiology', '111-222-3333', 'olivia.white@example.com'),
        ('Dr. William Harris', 'Gastroenterology', '222-333-4444', 'william.harris@example.com'),
        ('Dr. Sophia Clark', 'Pulmonology', '333-444-5555', 'sophia.clark@example.com'),
        ('Dr. Benjamin Lewis', 'Nephrology', '444-555-6666', 'benjamin.lewis@example.com'),
        ('Dr. Ava Walker', 'Hematology', '555-666-7777', 'ava.walker@example.com'),
        ('Dr. Daniel Hall', 'Allergy and Immunology', '666-777-8888', 'daniel.hall@example.com'),
        ('Dr. Mia Allen', 'Infectious Disease', '777-888-9999', 'mia.allen@example.com'),
        ('Dr. Henry Young', 'Rheumatology', '888-999-0000', 'henry.young@example.com'),
        ('Dr. Amelia King', 'Physical Medicine', '999-000-1111', 'amelia.king@example.com'),
        ('Dr. Samuel Scott', 'Emergency Medicine', '000-111-2222', 'samuel.scott@example.com')
        -- Continue adding more rows until you reach 100 doctors...
) AS source ([DoctorName], [Specialty], [Phone], [Email])
ON target.[DoctorName] = source.[DoctorName]
WHEN NOT MATCHED THEN
    INSERT ([DoctorName], [Specialty], [Phone], [Email], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive])
    VALUES (source.[DoctorName], source.[Specialty], source.[Phone], source.[Email], NULL, SYSDATETIMEOFFSET(), NULL, NULL, 1);