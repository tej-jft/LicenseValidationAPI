CREATE PROCEDURE GetLicense @LicenseNumber nvarchar(16)
As
SELECT StartDate,SoftwateSerialNumber,LicenseType FROM dbo.LicenseInfo WHERE SoftwateSerialNumber=@LicenseNumber;
GO
CREATE PROCEDURE InitializeLicense
@LicenseNumber nvarchar(16)
As
UPDATE dbo.LicenseInfo SET StartDate = GETDATE() WHERE SoftwateSerialNumber=@LicenseNumber
GO