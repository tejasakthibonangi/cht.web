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
MERGE INTO [dbo].[PaymentType] AS target
USING (
    VALUES 
        ('Cash', 'Cash payment method'),
        ('Credit Card', 'Payment made using a credit card'),
        ('Debit Card', 'Payment made using a debit card'),
        ('Bank Transfer', 'Payment made via bank transfer'),
        ('PayPal', 'Online payment system'),
        ('Apple Pay', 'Mobile payment and digital wallet service'),
        ('Google Pay', 'Mobile payment system developed by Google'),
        ('Cryptocurrency', 'Digital or virtual currency'),
        ('Gift Card', 'Prepaid card for purchases'),
        ('Check', 'Payment made using a check')
    -- Add more payment types as needed
) AS source ([PaymentTypeName], [Description])
ON target.[PaymentTypeName] = source.[PaymentTypeName]
WHEN NOT MATCHED THEN
    INSERT ([PaymentTypeName], [Description], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive])
    VALUES (source.[PaymentTypeName], source.[Description], NULL, SYSDATETIMEOFFSET(), NULL, NULL, 1);