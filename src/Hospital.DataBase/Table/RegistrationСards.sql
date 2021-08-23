CREATE TABLE [dbo].[RegistrationСards]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [DoctorId] INT REFERENCES Doctors (Id) ON DELETE CASCADE,
    [PatientId] INT REFERENCES Patients (Id) ON DELETE CASCADE,
    [DateAdmission]DATETIME,
    [DiagnosisId] INT REFERENCES MedicalHistorys (Id) ON DELETE CASCADE,
)
