IF OBJECT_ID(N'[AutoSerwis].[__EFMigrationsHistory]') IS NULL
BEGIN
    IF SCHEMA_ID(N'AutoSerwis') IS NULL EXEC(N'CREATE SCHEMA [AutoSerwis];');
    CREATE TABLE [AutoSerwis].[__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [AutoSerwis].[__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260705115437_InitAutoSerwisTables'
)
BEGIN
    IF SCHEMA_ID(N'AutoSerwis') IS NULL EXEC(N'CREATE SCHEMA [AutoSerwis];');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [AutoSerwis].[__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260705115437_InitAutoSerwisTables'
)
BEGIN
    CREATE TABLE [AutoSerwis].[ClientCategories] (
        [Id] bigint NOT NULL IDENTITY,
        [Name] nvarchar(100) NOT NULL,
        [IsActive] bit NULL,
        [CreatedOn] datetime2 NOT NULL,
        [ModifiedOn] datetime2 NOT NULL,
        [CreatedByUserId] uniqueidentifier NOT NULL,
        [ModifiedByUserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_ClientCategories] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [AutoSerwis].[__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260705115437_InitAutoSerwisTables'
)
BEGIN
    CREATE TABLE [AutoSerwis].[Clients] (
        [Id] bigint NOT NULL IDENTITY,
        [Name] nvarchar(256) NOT NULL,
        [RegistrationYear] int NOT NULL,
        [ServiceDuration] int NOT NULL,
        [ClientCategoryId] bigint NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [ModifiedOn] datetime2 NOT NULL,
        [CreatedByUserId] uniqueidentifier NOT NULL,
        [ModifiedByUserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Clients] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Clients_ClientCategories_ClientCategoryId] FOREIGN KEY ([ClientCategoryId]) REFERENCES [AutoSerwis].[ClientCategories] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [AutoSerwis].[__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260705115437_InitAutoSerwisTables'
)
BEGIN
    CREATE TABLE [AutoSerwis].[RepairOrders] (
        [Id] bigint NOT NULL IDENTITY,
        [OrderDate] datetime2 NOT NULL,
        [ClientId] bigint NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [ModifiedOn] datetime2 NOT NULL,
        [CreatedByUserId] uniqueidentifier NOT NULL,
        [ModifiedByUserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_RepairOrders] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_RepairOrders_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [AutoSerwis].[Clients] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [AutoSerwis].[__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260705115437_InitAutoSerwisTables'
)
BEGIN
    CREATE TABLE [AutoSerwis].[RepairHistories] (
        [Id] bigint NOT NULL IDENTITY,
        [RepairOrderId] bigint NOT NULL,
        [CarModel] nvarchar(150) NOT NULL,
        [Description] nvarchar(1000) NOT NULL,
        [TotalCost] decimal(18,2) NOT NULL,
        [CompletionDate] datetime2 NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [ModifiedOn] datetime2 NOT NULL,
        [CreatedByUserId] uniqueidentifier NOT NULL,
        [ModifiedByUserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_RepairHistories] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_RepairHistories_RepairOrders_RepairOrderId] FOREIGN KEY ([RepairOrderId]) REFERENCES [AutoSerwis].[RepairOrders] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [AutoSerwis].[__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260705115437_InitAutoSerwisTables'
)
BEGIN
    CREATE INDEX [IX_Clients_ClientCategoryId] ON [AutoSerwis].[Clients] ([ClientCategoryId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [AutoSerwis].[__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260705115437_InitAutoSerwisTables'
)
BEGIN
    CREATE INDEX [IX_RepairHistories_RepairOrderId] ON [AutoSerwis].[RepairHistories] ([RepairOrderId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [AutoSerwis].[__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260705115437_InitAutoSerwisTables'
)
BEGIN
    CREATE INDEX [IX_RepairOrders_ClientId] ON [AutoSerwis].[RepairOrders] ([ClientId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [AutoSerwis].[__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260705115437_InitAutoSerwisTables'
)
BEGIN
    INSERT INTO [AutoSerwis].[__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260705115437_InitAutoSerwisTables', N'8.0.15');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [AutoSerwis].[__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260705124437_SeedClientCategories'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedByUserId', N'CreatedOn', N'IsActive', N'ModifiedByUserId', N'ModifiedOn', N'Name') AND [object_id] = OBJECT_ID(N'[AutoSerwis].[ClientCategories]'))
        SET IDENTITY_INSERT [AutoSerwis].[ClientCategories] ON;
    EXEC(N'INSERT INTO [AutoSerwis].[ClientCategories] ([Id], [CreatedByUserId], [CreatedOn], [IsActive], [ModifiedByUserId], [ModifiedOn], [Name])
    VALUES (CAST(1 AS bigint), ''00000000-0000-0000-0000-000000000001'', ''2026-07-05T00:00:00.0000000'', CAST(1 AS bit), ''00000000-0000-0000-0000-000000000001'', ''2026-07-05T00:00:00.0000000'', N''Klient indywidualny''),
    (CAST(2 AS bigint), ''00000000-0000-0000-0000-000000000001'', ''2026-07-05T00:00:00.0000000'', CAST(1 AS bit), ''00000000-0000-0000-0000-000000000001'', ''2026-07-05T00:00:00.0000000'', N''Klient biznesowy / Flota''),
    (CAST(3 AS bigint), ''00000000-0000-0000-0000-000000000001'', ''2026-07-05T00:00:00.0000000'', CAST(1 AS bit), ''00000000-0000-0000-0000-000000000001'', ''2026-07-05T00:00:00.0000000'', N''VIP / Stały klient'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedByUserId', N'CreatedOn', N'IsActive', N'ModifiedByUserId', N'ModifiedOn', N'Name') AND [object_id] = OBJECT_ID(N'[AutoSerwis].[ClientCategories]'))
        SET IDENTITY_INSERT [AutoSerwis].[ClientCategories] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [AutoSerwis].[__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260705124437_SeedClientCategories'
)
BEGIN
    INSERT INTO [AutoSerwis].[__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260705124437_SeedClientCategories', N'8.0.15');
END;
GO

COMMIT;
GO