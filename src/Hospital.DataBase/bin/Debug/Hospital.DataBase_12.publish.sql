/*
Скрипт развертывания для Hospital

Этот код был создан программным средством.
Изменения, внесенные в этот файл, могут привести к неверному выполнению кода и будут потеряны
в случае его повторного формирования.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Hospital"
:setvar DefaultFilePrefix "Hospital"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\"

GO
:on error exit
GO
/*
Проверьте режим SQLCMD и отключите выполнение скрипта, если режим SQLCMD не поддерживается.
Чтобы повторно включить скрипт после включения режима SQLCMD выполните следующую инструкцию:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'Для успешного выполнения этого скрипта должен быть включен режим SQLCMD.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET PAGE_VERIFY NONE,
                DISABLE_BROKER 
            WITH ROLLBACK IMMEDIATE;
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
/*
Тип DateAdmission столбца в таблице [dbo].[RegistrationСards] в настоящее время  DATETIME2 (7) NOT NULL, но изменяется на  DATE NULL. Может произойти потеря данных, и развертывание может завершиться ошибкой, если столбец содержит данные, несовместимые с типом  DATE NULL.
*/

IF EXISTS (select top 1 1 from [dbo].[RegistrationСards])
    RAISERROR (N'Обнаружены строки. Обновление схемы завершено из-за возможной потери данных.', 16, 127) WITH NOWAIT

GO
PRINT N'Выполняется запуск перестройки таблицы [dbo].[Doctors]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Doctors] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [LastName]    NVARCHAR (MAX) NULL,
    [FirstName]   NVARCHAR (MAX) NULL,
    [Patronymic]  NVARCHAR (MAX) NULL,
    [NumberPhone] NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Doctors])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Doctors] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Doctors] ([Id], [FirstName], [LastName], [Patronymic], [NumberPhone])
        SELECT   [Id],
                 [FirstName],
                 [LastName],
                 [Patronymic],
                 [NumberPhone]
        FROM     [dbo].[Doctors]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Doctors] OFF;
    END

DROP TABLE [dbo].[Doctors];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Doctors]', N'Doctors';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Выполняется запуск перестройки таблицы [dbo].[Patients]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Patients] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [LastName]         NVARCHAR (MAX) NULL,
    [FirstName]        NVARCHAR (MAX) NULL,
    [Patronymic]       NVARCHAR (MAX) NULL,
    [Gender]           NVARCHAR (MAX) NULL,
    [ResidenceAddress] NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Patients])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Patients] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Patients] ([Id], [FirstName], [LastName], [Patronymic], [Gender], [ResidenceAddress])
        SELECT   [Id],
                 [FirstName],
                 [LastName],
                 [Patronymic],
                 [Gender],
                 [ResidenceAddress]
        FROM     [dbo].[Patients]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Patients] OFF;
    END

DROP TABLE [dbo].[Patients];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Patients]', N'Patients';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Выполняется запуск перестройки таблицы [dbo].[RegistrationСards]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_RegistrationСards] (
    [Id]            INT  IDENTITY (1, 1) NOT NULL,
    [DoctorId]      INT  NULL,
    [PatientId]     INT  NULL,
    [DateAdmission] DATE NULL,
    [DiagnosisId]   INT  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[RegistrationСards])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_RegistrationСards] ON;
        INSERT INTO [dbo].[tmp_ms_xx_RegistrationСards] ([Id], [PatientId], [DoctorId], [DiagnosisId], [DateAdmission])
        SELECT   [Id],
                 [PatientId],
                 [DoctorId],
                 [DiagnosisId],
                 [DateAdmission]
        FROM     [dbo].[RegistrationСards]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_RegistrationСards] OFF;
    END

DROP TABLE [dbo].[RegistrationСards];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_RegistrationСards]', N'RegistrationСards';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Идет создание Таблица [dbo].[MedicalHistorys]…';


GO
CREATE TABLE [dbo].[MedicalHistorys] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Diagnosis] NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Идет создание Внешний ключ ограничение без названия для [dbo].[RegistrationСards]…';


GO
ALTER TABLE [dbo].[RegistrationСards] WITH NOCHECK
    ADD FOREIGN KEY ([DoctorId]) REFERENCES [dbo].[Doctors] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Идет создание Внешний ключ ограничение без названия для [dbo].[RegistrationСards]…';


GO
ALTER TABLE [dbo].[RegistrationСards] WITH NOCHECK
    ADD FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patients] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Идет создание Внешний ключ ограничение без названия для [dbo].[RegistrationСards]…';


GO
ALTER TABLE [dbo].[RegistrationСards] WITH NOCHECK
    ADD FOREIGN KEY ([DiagnosisId]) REFERENCES [dbo].[MedicalHistorys] ([Id]) ON DELETE CASCADE;


GO
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
GO

GO
PRINT N'Существующие данные проверяются относительно вновь созданных ограничений';


GO
USE [$(DatabaseName)];


GO
CREATE TABLE [#__checkStatus] (
    id           INT            IDENTITY (1, 1) PRIMARY KEY CLUSTERED,
    [Schema]     NVARCHAR (256),
    [Table]      NVARCHAR (256),
    [Constraint] NVARCHAR (256)
);

SET NOCOUNT ON;

DECLARE tableconstraintnames CURSOR LOCAL FORWARD_ONLY
    FOR SELECT SCHEMA_NAME([schema_id]),
               OBJECT_NAME([parent_object_id]),
               [name],
               0
        FROM   [sys].[objects]
        WHERE  [parent_object_id] IN (OBJECT_ID(N'dbo.RegistrationСards'))
               AND [type] IN (N'F', N'C')
                   AND [object_id] IN (SELECT [object_id]
                                       FROM   [sys].[check_constraints]
                                       WHERE  [is_not_trusted] <> 0
                                              AND [is_disabled] = 0
                                       UNION
                                       SELECT [object_id]
                                       FROM   [sys].[foreign_keys]
                                       WHERE  [is_not_trusted] <> 0
                                              AND [is_disabled] = 0);

DECLARE @schemaname AS NVARCHAR (256);

DECLARE @tablename AS NVARCHAR (256);

DECLARE @checkname AS NVARCHAR (256);

DECLARE @is_not_trusted AS INT;

DECLARE @statement AS NVARCHAR (1024);

BEGIN TRY
    OPEN tableconstraintnames;
    FETCH tableconstraintnames INTO @schemaname, @tablename, @checkname, @is_not_trusted;
    WHILE @@fetch_status = 0
        BEGIN
            PRINT N'Проверка ограничения: ' + @checkname + N' [' + @schemaname + N'].[' + @tablename + N']';
            SET @statement = N'ALTER TABLE [' + @schemaname + N'].[' + @tablename + N'] WITH ' + CASE @is_not_trusted WHEN 0 THEN N'CHECK' ELSE N'NOCHECK' END + N' CHECK CONSTRAINT [' + @checkname + N']';
            BEGIN TRY
                EXECUTE [sp_executesql] @statement;
            END TRY
            BEGIN CATCH
                INSERT  [#__checkStatus] ([Schema], [Table], [Constraint])
                VALUES                  (@schemaname, @tablename, @checkname);
            END CATCH
            FETCH tableconstraintnames INTO @schemaname, @tablename, @checkname, @is_not_trusted;
        END
END TRY
BEGIN CATCH
    PRINT ERROR_MESSAGE();
END CATCH

IF CURSOR_STATUS(N'LOCAL', N'tableconstraintnames') >= 0
    CLOSE tableconstraintnames;

IF CURSOR_STATUS(N'LOCAL', N'tableconstraintnames') = -1
    DEALLOCATE tableconstraintnames;

SELECT N'Ошибка при проверке ограничения:' + [Schema] + N'.' + [Table] + N',' + [Constraint]
FROM   [#__checkStatus];

IF @@ROWCOUNT > 0
    BEGIN
        DROP TABLE [#__checkStatus];
        RAISERROR (N'Произошла ошибка при проверке ограничений', 16, 127);
    END

SET NOCOUNT OFF;

DROP TABLE [#__checkStatus];


GO
PRINT N'Обновление завершено.';


GO
