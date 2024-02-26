CREATE TABLE Pharmacies
(
Id INT PRIMARY KEY,
MedicineCode INT,
MedicineName NVARCHAR(100),
PricePerUnit DECIMAL(18,2)
);

CREATE TABLE Medicines
(
Id INT PRIMARY KEY,
MedicineCode INT,
MedicineName NVARCHAR(100),
ManufacturingDate DATE,
ExpiryDate DATE,
NumberOfPackages INT,
PharmacyNumber INT,
CostOfMedicine DECIMAL(18,2)
);