IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Samples] (
    [Id] bigint NOT NULL IDENTITY,
    [Title] nvarchar(50) NOT NULL,
    [Body] nvarchar(max) NULL,
    [CreatedBy] bigint NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Samples] PRIMARY KEY ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Body', N'CreatedAt', N'CreatedBy', N'Title') AND [object_id] = OBJECT_ID(N'[Samples]'))
    SET IDENTITY_INSERT [Samples] ON;
INSERT INTO [Samples] ([Id], [Body], [CreatedAt], [CreatedBy], [Title])
VALUES (CAST(2 AS bigint), N'Eligendi quisquam ullam iure praesentium numquam sapiente distinctio ad. Tempore voluptatibus ad et adipisci hic amet. Corporis soluta cupiditate soluta. Provident rerum nemo dolores debitis dicta voluptatem labore dolores adipisci. Adipisci illo quidem sit dolores. Ea dolor animi quod laborum quia perspiciatis sunt tempora.', '2020-07-01T01:14:20.5567372', CAST(5 AS bigint), N'hic'),
(CAST(3 AS bigint), N'Incidunt perferendis omnis. Quas voluptatem beatae vitae sunt a ut sed repellendus. Accusamus eos enim consequatur et praesentium ad ut beatae eius. Omnis voluptas error et velit autem ipsa atque consequuntur vitae. Nostrum accusamus soluta nisi.', '2020-11-26T01:10:54.9829175', CAST(4 AS bigint), N'velit'),
(CAST(4 AS bigint), N'Architecto laboriosam culpa cumque dicta in. Perspiciatis amet autem rerum recusandae perspiciatis pariatur. Eum sint molestiae quis neque tempora ab distinctio. Nobis nulla dignissimos voluptas nemo cumque tenetur quod et placeat. Nihil sit eos similique fuga enim dolores ullam suscipit.', '2021-01-18T12:14:38.6427703', CAST(1 AS bigint), N'est'),
(CAST(5 AS bigint), N'Sapiente et saepe ut atque dolore accusantium soluta cumque perferendis. Magni adipisci labore corrupti. Ratione et quibusdam consequatur voluptatem velit expedita eos maxime.', '2020-02-02T15:03:56.5511864', CAST(5 AS bigint), N'placeat'),
(CAST(6 AS bigint), N'Iusto aspernatur nihil iure ut blanditiis veritatis quas. Et illum quod atque nulla voluptas quos beatae quaerat consequatur. Ab placeat tenetur perferendis et omnis. Doloremque corrupti deserunt sint enim ex sit.', '2021-04-07T16:50:06.2395929', CAST(3 AS bigint), N'facere'),
(CAST(7 AS bigint), N'Doloremque omnis facilis unde exercitationem consectetur culpa porro consequatur sed. Vel rem rerum eum harum. Ratione voluptate est officia accusamus doloremque perferendis ea. Unde iure laudantium ut amet repellendus enim consequatur dolor porro. Sed expedita dolorem aperiam ipsa omnis. Ut omnis ipsa quia cupiditate iure.', '2019-07-23T07:33:40.2459313', CAST(5 AS bigint), N'impedit'),
(CAST(8 AS bigint), N'Nesciunt placeat et consectetur enim. Consectetur magnam perspiciatis ut rem perspiciatis odit dolorem. Modi corrupti corrupti.', '2020-01-27T09:01:30.8018159', CAST(3 AS bigint), N'corporis'),
(CAST(9 AS bigint), N'Omnis culpa earum modi eos beatae autem. Deleniti labore veritatis dolorum. Omnis perferendis ut sit nulla autem ut voluptatem voluptas ut.', '2021-03-25T21:11:05.6026614', CAST(5 AS bigint), N'perspiciatis'),
(CAST(10 AS bigint), N'Molestias porro exercitationem omnis et eius. Est consequatur esse sit quia dolorem sequi doloribus corporis. Perspiciatis qui dignissimos.', '2021-04-07T22:46:32.4395958', CAST(3 AS bigint), N'esse'),
(CAST(11 AS bigint), N'Eos eum perferendis nisi alias et ducimus repudiandae ut. Voluptas rerum ullam omnis placeat non ea voluptatibus. Sint et et asperiores omnis recusandae saepe laborum enim. Non consequatur voluptatem in aut quia quo quo. Commodi aliquid aut quaerat adipisci. Modi ea maxime doloribus qui sint.', '2021-03-24T14:25:37.7760056', CAST(1 AS bigint), N'in');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Body', N'CreatedAt', N'CreatedBy', N'Title') AND [object_id] = OBJECT_ID(N'[Samples]'))
    SET IDENTITY_INSERT [Samples] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230817080749_InitMigration', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [Username] nvarchar(25) NOT NULL,
    [FirstName] nvarchar(25) NOT NULL,
    [LastName] nvarchar(25) NOT NULL,
    [Email] nvarchar(50) NOT NULL,
    [Password] nvarchar(100) NOT NULL,
    [Salt] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
    CONSTRAINT [AK_Users_Email] UNIQUE ([Email]),
    CONSTRAINT [AK_Users_Username] UNIQUE ([Username])
);
GO

CREATE TABLE [RefreshTokens] (
    [Id] int NOT NULL IDENTITY,
    [Token] nvarchar(500) NOT NULL,
    [ExpiresAt] datetime2 NOT NULL,
    [UserId] int NOT NULL,
    [CreatedBy] int NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_RefreshTokens] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RefreshTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_RefreshTokens_UserId] ON [RefreshTokens] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230823174915_AddedRefreshTokensAndUsers', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Projects] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [DbEngine] int NOT NULL,
    [CreatedBy] int NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Projects] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230827130854_AddedProjects', N'6.0.7');
GO

COMMIT;
GO

