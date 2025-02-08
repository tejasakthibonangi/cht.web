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
-- Step 1: Insert Doctors into the User Table
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

-- Step 1: Insert Doctors into the User Table using MERGE
MERGE INTO [dbo].[User ] AS target
USING (
    VALUES 
        ('Samuel', 'Scott', 'samuel.scott@example.com', '000-111-2222'),
        ('Sarah', 'Martinez', 'sarah.martinez@example.com', '789-012-3456'),
        ('William', 'Harris', 'william.harris@example.com', '222-333-4444'),
        ('Emily', 'Davis', 'emily.davis@example.com', '567-890-1234'),
        ('James', 'Thomas', 'james.thomas@example.com', '012-345-6789'),
        ('Daniel', 'Hall', 'daniel.hall@example.com', '666-777-8888'),
        ('Amelia', 'King', 'amelia.king@example.com', '999-000-1111'),
        ('Benjamin', 'Lewis', 'benjamin.lewis@example.com', '444-555-6666'),
        ('Mia', 'Allen', 'mia.allen@example.com', '777-888-9999'),
        ('Henry', 'Young', 'henry.young@example.com', '888-999-0000'),
        ('Sophia', 'Clark', 'sophia.clark@example.com', '333-444-5555'),
        ('John', 'Smith', 'john.smith@example.com', '123-456-7890'),
        ('Jane', 'Doe', 'jane.doe@example.com', '234-567-8901'),
        ('Michael', 'Wilson', 'michael.wilson@example.com', '678-901-2345'),
        ('Alice', 'Johnson', 'alice.johnson@example.com', '345-678-9012'),
        ('David', 'Anderson', 'david.anderson@example.com', '890-123-4567'),
        ('Ava', 'Walker', 'ava.walker@example.com', '555-666-7777'),
        ('Laura', 'Taylor', 'laura.taylor@example.com', '901-234-5678'),
        ('Olivia', 'White', 'olivia.white@example.com', '111-222-3333'),
        ('Robert', 'Brown', 'robert.brown@example.com', '456-789-0123')
) AS source ([FirstName], [LastName], [Email], [Phone])
ON target.Email = source.Email
WHEN NOT MATCHED THEN
    INSERT ([FirstName], [LastName], [Email], [Phone], [IsActive], [CreatedOn])
    VALUES (source.[FirstName], source.[LastName], source.[Email], source.[Phone], 1, SYSDATETIMEOFFSET());

-- Step 2: Update the Doctor Table with the corresponding User Ids using MERGE
MERGE INTO [dbo].[Doctor] AS target
USING (
    SELECT Id, Email
    FROM [dbo].[User ]
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
    )
) AS source
ON target.Email = source.Email
WHEN MATCHED THEN
    UPDATE SET target.UserId = source.Id;