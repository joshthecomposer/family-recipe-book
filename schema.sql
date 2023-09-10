CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20230909221607_TestSQLConnection', '7.0.10');

COMMIT;

START TRANSACTION;

CREATE TABLE `Users` (
    `UserId` int NOT NULL AUTO_INCREMENT,
    `FirstName` longtext CHARACTER SET utf8mb4 NULL,
    `LastName` longtext CHARACTER SET utf8mb4 NULL,
    `Email` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Password` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Confirm` longtext CHARACTER SET utf8mb4 NOT NULL,
    `IsActive` tinyint(1) NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `UpdatedAt` datetime(6) NOT NULL,
    CONSTRAINT `PK_Users` PRIMARY KEY (`UserId`)
) CHARACTER SET=utf8mb4;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20230909223845_AddUserTable', '7.0.10');

COMMIT;

START TRANSACTION;

ALTER TABLE `Users` MODIFY COLUMN `Email` varchar(255) CHARACTER SET utf8mb4 NOT NULL;

CREATE UNIQUE INDEX `IX_Users_Email` ON `Users` (`Email`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20230910013941_UniqueEmailContsraint', '7.0.10');

COMMIT;

START TRANSACTION;

ALTER TABLE `Users` ADD `DisabledAt` datetime(6) NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20230910042256_AddDisabledAtField', '7.0.10');

COMMIT;

START TRANSACTION;

ALTER TABLE `Users` DROP COLUMN `Confirm`;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20230910043158_RemoveConfirmPasswordFromMap', '7.0.10');

COMMIT;

START TRANSACTION;

CREATE TABLE `RefreshTokens` (
    `RefreshTokenId` int NOT NULL AUTO_INCREMENT,
    `Value` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `UserId` int NOT NULL,
    `DisabledAt` datetime(6) NULL,
    `IsActive` tinyint(1) NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `UpdatedAt` datetime(6) NOT NULL,
    CONSTRAINT `PK_RefreshTokens` PRIMARY KEY (`RefreshTokenId`),
    CONSTRAINT `FK_RefreshTokens_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_RefreshTokens_UserId` ON `RefreshTokens` (`UserId`);

CREATE UNIQUE INDEX `IX_RefreshTokens_Value` ON `RefreshTokens` (`Value`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20230910054357_AddRFTTable', '7.0.10');

COMMIT;

START TRANSACTION;

ALTER TABLE `RefreshTokens` ADD `Expiry` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00';

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20230910062341_AddExpiryToRFT', '7.0.10');

COMMIT;


