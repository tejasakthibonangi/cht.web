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
MERGE INTO [dbo].[LabTests] AS target
USING (
    VALUES 
        ('Complete Blood Count', 'A test that evaluates overall health and detects a variety of disorders, such as anemia and infection.'),
        ('Basic Metabolic Panel', 'A test that measures glucose, calcium, and electrolytes to assess metabolism and kidney function.'),
        ('Lipid Panel', 'A test that measures cholesterol levels and triglycerides to assess heart disease risk.'),
        ('Liver Function Tests', 'A group of blood tests that assess the health of the liver.'),
        ('Thyroid Function Tests', 'Tests that measure thyroid hormone levels to evaluate thyroid function.'),
        ('Urinalysis', 'A test that examines urine for signs of disease or infection.'),
        ('Coagulation Panel', 'A test that assesses blood clotting ability.'),
        ('Blood Glucose Test', 'A test that measures the amount of glucose in the blood.'),
        ('Hemoglobin A1c', 'A test that measures average blood sugar levels over the past 2 to 3 months.'),
        ('Electrolyte Panel', 'A test that measures the levels of electrolytes in the blood, such as sodium and potassium.')
    -- Add more lab tests as needed
) AS source ([TestName], [TestDescription])
ON target.[TestName] = source.[TestName]
WHEN NOT MATCHED THEN
    INSERT ([TestName], [TestDescription], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive])
    VALUES (source.[TestName], source.[TestDescription], NULL, SYSDATETIMEOFFSET(), NULL, NULL, 1);