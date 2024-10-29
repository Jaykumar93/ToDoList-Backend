-- Create Database
CREATE DATABASE ToDoApp;  -- Replace with your desired database name
GO

-- Use the newly created database
USE ToDoApp;
GO


-- Organizations and Teams
CREATE TABLE Organizations (
    OrganizationId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    LogoUrl NVARCHAR(255),
    CreatedAt DATETIME2 NOT NULL,  -- Managed in backend
    UpdatedAt DATETIME2 NOT NULL,  -- Managed in backend
    IsActive BIT NOT NULL DEFAULT 1
);

CREATE TABLE Teams (
    TeamId INT PRIMARY KEY IDENTITY(1,1),
    OrganizationId INT NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    CreatedAt DATETIME2 NOT NULL,  -- Managed in backend
    UpdatedAt DATETIME2 NOT NULL,  -- Managed in backend
    IsActive BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (OrganizationId) REFERENCES Organizations(OrganizationId)
);

-- Users and Authentication
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Email NVARCHAR(255) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    ProfilePicture NVARCHAR(255),
    CreatedAt DATETIME2 NOT NULL,  -- Managed in backend
    UpdatedAt DATETIME2 NOT NULL,  -- Managed in backend
    LastLoginAt DATETIME2,  -- Managed in backend
    IsActive BIT NOT NULL DEFAULT 1
);

CREATE TABLE UserSettings (
    UserSettingsId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    ThemePreference NVARCHAR(20) DEFAULT 'light',
    TimeZone NVARCHAR(50) DEFAULT 'UTC',
    Language NVARCHAR(10) DEFAULT 'en',
    NotificationPreferences NVARCHAR(MAX), -- JSON
    UpdatedAt DATETIME2 NOT NULL,  -- Managed in backend
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

CREATE TABLE RefreshTokens (
    TokenId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    Token NVARCHAR(255) NOT NULL,
    ExpiryDate DATETIME2 NOT NULL,
    IsRevoked BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL,  -- Managed in backend
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

-- Roles and Permissions
CREATE TABLE Roles (
    RoleId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL,
    Description NVARCHAR(255),
    Permissions NVARCHAR(MAX), -- JSON
    IsSystem BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL  -- Managed in backend
);

CREATE TABLE OrganizationUsers (
    OrganizationUserId INT PRIMARY KEY IDENTITY(1,1),
    OrganizationId INT NOT NULL,
    UserId INT NOT NULL,
    RoleId INT NOT NULL,
    JoinedAt DATETIME2 NOT NULL,  -- Managed in backend
    IsActive BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (OrganizationId) REFERENCES Organizations(OrganizationId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);

CREATE TABLE TeamUsers (
    TeamUserId INT PRIMARY KEY IDENTITY(1,1),
    TeamId INT NOT NULL,
    UserId INT NOT NULL,
    RoleId INT NOT NULL,
    JoinedAt DATETIME2 NOT NULL,  -- Managed in backend
    IsActive BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (TeamId) REFERENCES Teams(TeamId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);

-- Projects and Tasks
CREATE TABLE Projects (
    ProjectId INT PRIMARY KEY IDENTITY(1,1),
    OrganizationId INT NOT NULL,
    TeamId INT,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    Color NVARCHAR(7),
    IsArchived BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL,  -- Managed in backend
    UpdatedAt DATETIME2 NOT NULL,  -- Managed in backend
    FOREIGN KEY (OrganizationId) REFERENCES Organizations(OrganizationId),
    FOREIGN KEY (TeamId) REFERENCES Teams(TeamId)
);

CREATE TABLE Categories (
    CategoryId INT PRIMARY KEY IDENTITY(1,1),
    OrganizationId INT NOT NULL,
    Name NVARCHAR(50) NOT NULL,
    Color NVARCHAR(7),
    Icon NVARCHAR(50),
    CreatedAt DATETIME2 NOT NULL,  -- Managed in backend
    FOREIGN KEY (OrganizationId) REFERENCES Organizations(OrganizationId)
);

CREATE TABLE TaskPriorities (
    PriorityId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(20) NOT NULL,
    Level INT NOT NULL,
    Color NVARCHAR(7) NOT NULL
);

CREATE TABLE TaskStatuses (
    StatusId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(20) NOT NULL,
    Color NVARCHAR(7) NOT NULL,
    IsFinal BIT NOT NULL DEFAULT 0
);

CREATE TABLE Tasks (
    TaskId INT PRIMARY KEY IDENTITY(1,1),
    ProjectId INT,
    TeamId INT,
    UserId INT NOT NULL,
    CategoryId INT,
    ParentTaskId INT,
    PriorityId INT NOT NULL,
    StatusId INT NOT NULL,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    DueDate DATETIME2,  -- Managed in backend
    EstimatedMinutes INT,
    IsRecurring BIT NOT NULL DEFAULT 0,
    RecurrencePattern NVARCHAR(MAX), -- JSON
    CompletedAt DATETIME2,  -- Managed in backend
    IsArchived BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL,  -- Managed in backend
    UpdatedAt DATETIME2 NOT NULL,  -- Managed in backend
    FOREIGN KEY (ProjectId) REFERENCES Projects(ProjectId),
    FOREIGN KEY (TeamId) REFERENCES Teams(TeamId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId),
    FOREIGN KEY (ParentTaskId) REFERENCES Tasks(TaskId),
    FOREIGN KEY (PriorityId) REFERENCES TaskPriorities(PriorityId),
    FOREIGN KEY (StatusId) REFERENCES TaskStatuses(StatusId)
);

CREATE TABLE SubTasks (
    SubTaskId INT PRIMARY KEY IDENTITY(1,1),
    ParentTaskId INT NOT NULL,
    Title NVARCHAR(200) NOT NULL,
    IsCompleted BIT NOT NULL DEFAULT 0,
    OrderIndex INT NOT NULL,
    CreatedAt DATETIME2 NOT NULL,  -- Managed in backend
    FOREIGN KEY (ParentTaskId) REFERENCES Tasks(TaskId)
);

CREATE TABLE Tags (
    TagId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL,
    Color NVARCHAR(7),
    CreatedAt DATETIME2 NOT NULL  -- Managed in backend
);

CREATE TABLE TaskTags (
    TaskId INT NOT NULL,
    TagId INT NOT NULL,
    CreatedAt DATETIME2 NOT NULL,  -- Managed in backend
    PRIMARY KEY (TaskId, TagId),
    FOREIGN KEY (TaskId) REFERENCES Tasks(TaskId),
    FOREIGN KEY (TagId) REFERENCES Tags(TagId)
);

CREATE TABLE TaskComments (
    CommentId INT PRIMARY KEY IDENTITY(1,1),
    TaskId INT NOT NULL,
    UserId INT NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME2 NOT NULL,  -- Managed in backend
    UpdatedAt DATETIME2 NOT NULL,  -- Managed in backend
    FOREIGN KEY (TaskId) REFERENCES Tasks(TaskId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

CREATE TABLE TaskAttachments (
    AttachmentId INT PRIMARY KEY IDENTITY(1,1),
    TaskId INT NOT NULL,
    FileName NVARCHAR(255) NOT NULL,
    FileType NVARCHAR(100) NOT NULL,
    FilePath NVARCHAR(500) NOT NULL,
    FileSize BIGINT NOT NULL,
    UploadedAt DATETIME2 NOT NULL,  -- Managed in backend
    FOREIGN KEY (TaskId) REFERENCES Tasks(TaskId)
);

CREATE TABLE TaskActivities (
    ActivityId INT PRIMARY KEY IDENTITY(1,1),
    TaskId INT NOT NULL,
    UserId INT NOT NULL,
    ActivityType NVARCHAR(50) NOT NULL,
    ActivityData NVARCHAR(MAX), -- JSON
    CreatedAt DATETIME2 NOT NULL,  -- Managed in backend
    FOREIGN KEY (TaskId) REFERENCES Tasks(TaskId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

-- Create Indexes
CREATE INDEX IX_Users_Email ON Users(Email);
CREATE INDEX IX_Tasks_Project ON Tasks(ProjectId, TeamId);
CREATE INDEX IX_Tasks_User ON Tasks(UserId);
CREATE INDEX IX_Tasks_DueDate ON Tasks(DueDate) WHERE DueDate IS NOT NULL;
CREATE INDEX IX_Tasks_Status ON Tasks(StatusId);
CREATE INDEX IX_TaskActivities_Task ON TaskActivities(TaskId, CreatedAt);
CREATE INDEX IX_OrganizationUsers_Org ON OrganizationUsers(OrganizationId);
CREATE INDEX IX_TeamUsers_Team ON TeamUsers(TeamId);

-- Add default data
INSERT INTO TaskPriorities (Name, Level, Color) VALUES
('Low', 1, '#00FF00'),
('Medium', 2, '#FFFF00'),
('High', 3, '#FF0000');

INSERT INTO TaskStatuses (Name, Color, IsFinal) VALUES
('Pending', '#FFA500', 0),
('In Progress', '#0000FF', 0),
('Completed', '#008000', 1),
('Archived', '#808080', 1);
