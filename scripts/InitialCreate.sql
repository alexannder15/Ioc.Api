IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory]
    (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Users]
(
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(128) NULL,
    [LastName] nvarchar(128) NULL,
    [State] int NOT NULL,
    [PictureUrl] nvarchar(max) NULL,
    [IsDeleted] bit NOT NULL,
    [CreatedOn] datetimeoffset NOT NULL,
    [UpdatedOn] datetimeoffset NULL,
    [CreatedById] int NULL,
    [UpdatedById] int NULL,
    [UserName] nvarchar(128) NULL,
    [NormalizedUserName] nvarchar(128) NULL,
    [Email] nvarchar(128) NULL,
    [NormalizedEmail] nvarchar(128) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Users_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [Users] ([Id]),
    CONSTRAINT [FK_Users_Users_UpdatedById] FOREIGN KEY ([UpdatedById]) REFERENCES [Users] ([Id])
);

CREATE TABLE [Iocs]
(
    [Id] int NOT NULL IDENTITY,
    [Sha256] nvarchar(max) NOT NULL,
    [Sha1] nvarchar(max) NOT NULL,
    [Md5] nvarchar(max) NOT NULL,
    [Mcafee] nvarchar(max) NOT NULL,
    [Engines] nvarchar(max) NOT NULL,
    [IsDeleted] bit NOT NULL,
    [CreatedOn] datetimeoffset NOT NULL,
    [UpdatedOn] datetimeoffset NULL,
    [CreatedById] int NULL,
    [UpdatedById] int NULL,
    CONSTRAINT [PK_Iocs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Iocs_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [Users] ([Id]),
    CONSTRAINT [FK_Iocs_Users_UpdatedById] FOREIGN KEY ([UpdatedById]) REFERENCES [Users] ([Id])
);

CREATE TABLE [Roles]
(
    [Id] int NOT NULL IDENTITY,
    [IsDeleted] bit NOT NULL,
    [CreatedOn] datetimeoffset NOT NULL,
    [UpdatedOn] datetimeoffset NULL,
    [CreatedById] int NULL,
    [UpdatedById] int NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Roles_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [Users] ([Id]),
    CONSTRAINT [FK_Roles_Users_UpdatedById] FOREIGN KEY ([UpdatedById]) REFERENCES [Users] ([Id])
);

CREATE TABLE [UserClaims]
(
    [Id] int NOT NULL IDENTITY,
    [IsDeleted] bit NOT NULL,
    [CreatedOn] datetimeoffset NOT NULL,
    [UpdatedOn] datetimeoffset NULL,
    [CreatedById] int NULL,
    [UpdatedById] int NULL,
    [UserId] int NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_UserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserClaims_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [Users] ([Id]),
    CONSTRAINT [FK_UserClaims_Users_UpdatedById] FOREIGN KEY ([UpdatedById]) REFERENCES [Users] ([Id]),
    CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [UserLogins]
(
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [IsDeleted] bit NOT NULL,
    [CreatedOn] datetimeoffset NOT NULL,
    [UpdatedOn] datetimeoffset NULL,
    [CreatedById] int NULL,
    [UpdatedById] int NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_UserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_UserLogins_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [Users] ([Id]),
    CONSTRAINT [FK_UserLogins_Users_UpdatedById] FOREIGN KEY ([UpdatedById]) REFERENCES [Users] ([Id]),
    CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [UserTokens]
(
    [UserId] int NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [IsDeleted] bit NOT NULL,
    [CreatedOn] datetimeoffset NOT NULL,
    [UpdatedOn] datetimeoffset NULL,
    [CreatedById] int NULL,
    [UpdatedById] int NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_UserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_UserTokens_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [Users] ([Id]),
    CONSTRAINT [FK_UserTokens_Users_UpdatedById] FOREIGN KEY ([UpdatedById]) REFERENCES [Users] ([Id]),
    CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [RoleClaims]
(
    [Id] int NOT NULL IDENTITY,
    [IsDeleted] bit NOT NULL,
    [CreatedOn] datetimeoffset NOT NULL,
    [UpdatedOn] datetimeoffset NULL,
    [CreatedById] int NULL,
    [UpdatedById] int NULL,
    [RoleId] int NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_RoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RoleClaims_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_RoleClaims_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [Users] ([Id]),
    CONSTRAINT [FK_RoleClaims_Users_UpdatedById] FOREIGN KEY ([UpdatedById]) REFERENCES [Users] ([Id])
);

CREATE TABLE [UserRoles]
(
    [UserId] int NOT NULL,
    [RoleId] int NOT NULL,
    [IsDeleted] bit NOT NULL,
    [CreatedOn] datetimeoffset NOT NULL,
    [UpdatedOn] datetimeoffset NULL,
    [CreatedById] int NULL,
    [UpdatedById] int NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRoles_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [Users] ([Id]),
    CONSTRAINT [FK_UserRoles_Users_UpdatedById] FOREIGN KEY ([UpdatedById]) REFERENCES [Users] ([Id]),
    CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_Iocs_CreatedById] ON [Iocs] ([CreatedById]);

CREATE INDEX [IX_Iocs_UpdatedById] ON [Iocs] ([UpdatedById]);

CREATE INDEX [IX_RoleClaims_CreatedById] ON [RoleClaims] ([CreatedById]);

CREATE INDEX [IX_RoleClaims_RoleId] ON [RoleClaims] ([RoleId]);

CREATE INDEX [IX_RoleClaims_UpdatedById] ON [RoleClaims] ([UpdatedById]);

CREATE INDEX [IX_Roles_CreatedById] ON [Roles] ([CreatedById]);

CREATE INDEX [IX_Roles_UpdatedById] ON [Roles] ([UpdatedById]);

CREATE UNIQUE INDEX [RoleNameIndex] ON [Roles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

CREATE INDEX [IX_UserClaims_CreatedById] ON [UserClaims] ([CreatedById]);

CREATE INDEX [IX_UserClaims_UpdatedById] ON [UserClaims] ([UpdatedById]);

CREATE INDEX [IX_UserClaims_UserId] ON [UserClaims] ([UserId]);

CREATE INDEX [IX_UserLogins_CreatedById] ON [UserLogins] ([CreatedById]);

CREATE INDEX [IX_UserLogins_UpdatedById] ON [UserLogins] ([UpdatedById]);

CREATE INDEX [IX_UserLogins_UserId] ON [UserLogins] ([UserId]);

CREATE INDEX [IX_UserRoles_CreatedById] ON [UserRoles] ([CreatedById]);

CREATE INDEX [IX_UserRoles_RoleId] ON [UserRoles] ([RoleId]);

CREATE INDEX [IX_UserRoles_UpdatedById] ON [UserRoles] ([UpdatedById]);

CREATE INDEX [EmailIndex] ON [Users] ([NormalizedEmail]);

CREATE INDEX [IX_Users_CreatedById] ON [Users] ([CreatedById]);

CREATE INDEX [IX_Users_UpdatedById] ON [Users] ([UpdatedById]);

CREATE UNIQUE INDEX [UserNameIndex] ON [Users] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

CREATE INDEX [IX_UserTokens_CreatedById] ON [UserTokens] ([CreatedById]);

CREATE INDEX [IX_UserTokens_UpdatedById] ON [UserTokens] ([UpdatedById]);

INSERT INTO [__EFMigrationsHistory]
    ([MigrationId], [ProductVersion])
VALUES
    (N'20250629201432_Initial', N'9.0.6');

COMMIT;
GO

