CREATE TABLE [dbo].[RegistrationСard]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [DoctorId] INT REFERENCES Doctor (Id) ON DELETE CASCADE,
    [PatientId] INT REFERENCES Patient (Id) ON DELETE CASCADE,
    [DateAdmission]DATETIME2,
    [DiagnosisId] INT REFERENCES MedicalHistory (Id) ON DELETE CASCADE,
)
