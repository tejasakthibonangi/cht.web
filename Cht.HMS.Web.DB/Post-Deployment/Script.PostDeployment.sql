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
-- Include seed data for Role
:r .\SeedRole.sql

-- Include seed data for SeedDoctor
:r .\SeedDoctor.sql

-- Include seed data for SeedLabTests
:r .\SeedLabTests.sql

-- Include seed data for SeedMedicines
:r .\SeedMedicines.sql

-- Include seed data for SeedPatientType
:r .\SeedPatientType.sql

-- Include seed data for SeedPaymentType
:r .\SeedPaymentType.sql
