-- Kør hele scriptet i MySQL Workbench før projektet startes.
-- Opretter tabeller, men indsætter IKKE brugernes passwords i klartekst.
CREATE DATABASE IF NOT EXISTS LyseroedeSpejdere CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE LyseroedeSpejdere;

CREATE TABLE IF NOT EXISTS Users (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(80) NOT NULL UNIQUE,
    PasswordHash TEXT NOT NULL,
    FullName VARCHAR(120) NOT NULL,
    Role ENUM('Scout','Leader') NOT NULL,
    Age INT NOT NULL,
    Badges VARCHAR(255) NOT NULL DEFAULT ''
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS Activities (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(120) NOT NULL,
    Description TEXT NOT NULL,
    StartsAt DATETIME NOT NULL,
    EndsAt DATETIME NOT NULL,
    MaxParticipants INT NOT NULL,
    MinAge INT NOT NULL DEFAULT 0,
    RequiredBadge VARCHAR(80) NOT NULL DEFAULT '',
    LeaderId INT NOT NULL,
    CONSTRAINT FK_Activities_Leader FOREIGN KEY (LeaderId) REFERENCES Users(Id),
    CONSTRAINT CHK_Activity_Capacity CHECK (MaxParticipants > 0),
    CONSTRAINT CHK_Activity_Age CHECK (MinAge >= 0),
    INDEX IX_Activities_Start (StartsAt)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS Registrations (
    UserId INT NOT NULL,
    ActivityId INT NOT NULL,
    RegisteredAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (UserId,ActivityId),
    CONSTRAINT FK_Registration_User FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    CONSTRAINT FK_Registration_Activity FOREIGN KEY (ActivityId) REFERENCES Activities(Id) ON DELETE CASCADE,
    INDEX IX_Registrations_Activity (ActivityId)
) ENGINE=InnoDB;

