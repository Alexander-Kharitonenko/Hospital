CREATE TABLE [dbo].[Patients]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	LastName NVARCHAR(MAX),
	FirstName NVARCHAR(MAX),
	Patronymic NVARCHAR(MAX),
	Gender NVARCHAR(MAX),
	ResidenceAddress NVARCHAR(MAX)
)
