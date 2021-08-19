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

INSERT INTO Patients (LastName, FirstName, Patronymic,Gender,ResidenceAddress)
  VALUES ('Smirnov', 'Sergey', 'Ivanovich', 'Male', 'Gomel, Trudovaya st., 5'),
         ('Lebedeva', 'Natalya', 'Nikolaevna', 'Female', 'Gomel, Sadovaya st., 10'),
         ('Solovyova', 'Ksenia', 'Alexandrovna', 'Female', 'Mozyr Lesnaya st., 7'),
         ('Orlov', 'Mikhail', 'Viktorovich', 'Male', 'Mozyr Beregovaya st., 3'),
         ('Kovalev', 'Igor', 'Anatolyevich', 'Male', 'Gomel, street Klenovaya, 1');


  INSERT INTO Doctors (FirstName,Patronymic,LastName,NumberPhone)
  VALUES ('Gulagina', 'Julia', 'Anatolyevna', '+ 375251111111'),
         ('Vasiliev', 'Valery', 'Valentinovich', '+ 375252222222'),
         ('Ugarov', 'Victor', 'Mikhailovich', '+ 375253333333'),
         ('Demchuk', 'Alexey', 'Pavlovich', '+ 375254444444'),
         ('Grishina', 'Olga', 'Konstantinovna', '+ 375255555555');
 

  INSERT INTO MedicalHistorys (Diagnosis)
  VALUES ('Stroke'),
         ('Diabetes'),
         ('Tuberculosis'),
         ('AIDS'),
         ('Brain cancer');
 

  INSERT INTO RegistrationСards (DoctorId,PatientId,DateAdmission,DiagnosisId)
  VALUES (2,3,'2021-06-11',1),
         (3,2,'2021-01-03',5),
         (1,5,'2021-01-10',3),
         (5,1,'2021-9-21',4),
         (4,4,'2021-12-12',2);