-- ============================================
-- QuizManagement Database — Stored Procedures
-- Generated for ASP.NET Core MVC Project
-- ============================================

-- ============================================
-- DATABASE CREATION
-- ============================================
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'QuizManagement')
    CREATE DATABASE QuizManagement;
GO
USE QuizManagement;
GO

-- ============================================
-- TABLE: MST_User
-- ============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MST_User')
CREATE TABLE MST_User (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    UserName VARCHAR(100) NOT NULL,
    Password VARCHAR(200) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Mobile VARCHAR(100) NULL,
    IsActive BIT DEFAULT 1,
    IsAdmin BIT DEFAULT 0,
    Created DATETIME DEFAULT GETDATE(),
    Modified DATETIME DEFAULT GETDATE()
);
GO

-- ============================================
-- TABLE: MST_QuestionLevel
-- ============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MST_QuestionLevel')
CREATE TABLE MST_QuestionLevel (
    QuestionLevelID INT IDENTITY(1,1) PRIMARY KEY,
    QuestionLevel VARCHAR(100) NOT NULL,
    UserID INT NOT NULL FOREIGN KEY REFERENCES MST_User(UserID),
    Created DATETIME DEFAULT GETDATE(),
    Modified DATETIME DEFAULT GETDATE()
);
GO

-- ============================================
-- TABLE: MST_Quiz
-- ============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MST_Quiz')
CREATE TABLE MST_Quiz (
    QuizID INT IDENTITY(1,1) PRIMARY KEY,
    QuizName VARCHAR(100) NOT NULL,
    TotalQuestions INT NOT NULL,
    QuizDate DATETIME NOT NULL,
    UserID INT NOT NULL FOREIGN KEY REFERENCES MST_User(UserID),
    Created DATETIME DEFAULT GETDATE(),
    Modified DATETIME DEFAULT GETDATE()
);
GO

-- ============================================
-- TABLE: MST_Question
-- ============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MST_Question')
CREATE TABLE MST_Question (
    QuestionID INT IDENTITY(1,1) PRIMARY KEY,
    QuestionText NVARCHAR(MAX) NOT NULL,
    QuestionLevelID INT NOT NULL FOREIGN KEY REFERENCES MST_QuestionLevel(QuestionLevelID),
    OptionA VARCHAR(100) NOT NULL,
    OptionB VARCHAR(100) NOT NULL,
    OptionC VARCHAR(100) NULL,
    OptionD VARCHAR(100) NULL,
    CorrectOption VARCHAR(100) NOT NULL,
    QuestionMarks INT NOT NULL,
    IsActive BIT DEFAULT 1,
    UserID INT NOT NULL FOREIGN KEY REFERENCES MST_User(UserID),
    Created DATETIME DEFAULT GETDATE(),
    Modified DATETIME DEFAULT GETDATE()
);
GO

-- ============================================
-- TABLE: MST_QuizWiseQuestions
-- ============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MST_QuizWiseQuestions')
CREATE TABLE MST_QuizWiseQuestions (
    QuizWiseQuestionsID INT IDENTITY(1,1) PRIMARY KEY,
    QuizID INT NOT NULL FOREIGN KEY REFERENCES MST_Quiz(QuizID),
    QuestionID INT NOT NULL FOREIGN KEY REFERENCES MST_Question(QuestionID),
    UserID INT NOT NULL FOREIGN KEY REFERENCES MST_User(UserID),
    Created DATETIME DEFAULT GETDATE(),
    Modified DATETIME DEFAULT GETDATE()
);
GO

-- ============================================
-- USER STORED PROCEDURES
-- ============================================

CREATE OR ALTER PROCEDURE PR_User_SelectAll
AS
BEGIN
    SELECT UserID, UserName, Password, Email, Mobile, IsActive, IsAdmin, Created, Modified
    FROM MST_User
    ORDER BY Modified DESC;
END
GO

CREATE OR ALTER PROCEDURE PR_User_SelectByPK
    @UserID INT
AS
BEGIN
    SELECT UserID, UserName, Password, Email, Mobile, IsActive, IsAdmin, Created, Modified
    FROM MST_User
    WHERE UserID = @UserID;
END
GO

CREATE OR ALTER PROCEDURE PR_User_Insert
    @UserName VARCHAR(100),
    @Password VARCHAR(200),
    @Email VARCHAR(100),
    @Mobile VARCHAR(100),
    @IsActive BIT,
    @IsAdmin BIT
AS
BEGIN
    INSERT INTO MST_User (UserName, Password, Email, Mobile, IsActive, IsAdmin, Created, Modified)
    VALUES (@UserName, @Password, @Email, @Mobile, @IsActive, @IsAdmin, GETDATE(), GETDATE());
END
GO

CREATE OR ALTER PROCEDURE PR_User_UpdateByPK
    @UserID INT,
    @UserName VARCHAR(100),
    @Password VARCHAR(200),
    @Email VARCHAR(100),
    @Mobile VARCHAR(100),
    @IsActive BIT,
    @IsAdmin BIT,
    @Modified DATETIME
AS
BEGIN
    UPDATE MST_User
    SET UserName = @UserName, Password = @Password, Email = @Email,
        Mobile = @Mobile, IsActive = @IsActive, IsAdmin = @IsAdmin, Modified = @Modified
    WHERE UserID = @UserID;
END
GO

CREATE OR ALTER PROCEDURE PR_User_DeleteByPK
    @UserID INT
AS
BEGIN
    DELETE FROM MST_User WHERE UserID = @UserID;
END
GO

CREATE OR ALTER PROCEDURE PR_User_Login
    @UserName VARCHAR(100)
AS
BEGIN
    SELECT UserID, UserName, Password, Email, Mobile, IsActive, IsAdmin
    FROM MST_User
    WHERE UserName = @UserName;
END
GO

CREATE OR ALTER PROCEDURE PR_User_Register
    @UserName VARCHAR(100),
    @Password VARCHAR(200),
    @Email VARCHAR(100),
    @Mobile VARCHAR(100),
    @IsActive BIT,
    @IsAdmin BIT
AS
BEGIN
    INSERT INTO MST_User (UserName, Password, Email, Mobile, IsActive, IsAdmin, Created, Modified)
    VALUES (@UserName, @Password, @Email, @Mobile, @IsActive, @IsAdmin, GETDATE(), GETDATE());
END
GO

-- ============================================
-- QUESTION LEVEL STORED PROCEDURES
-- ============================================

CREATE OR ALTER PROCEDURE PR_QuestionLevel_SelectAll
AS
BEGIN
    SELECT ql.QuestionLevelID, ql.QuestionLevel, u.UserName, ql.Created, ql.Modified
    FROM MST_QuestionLevel ql
    INNER JOIN MST_User u ON ql.UserID = u.UserID
    ORDER BY ql.Modified DESC;
END
GO

CREATE OR ALTER PROCEDURE PR_QuestionLevel_SelectByPK
    @QuestionLevelID INT
AS
BEGIN
    SELECT QuestionLevelID, QuestionLevel, UserID, Created, Modified
    FROM MST_QuestionLevel
    WHERE QuestionLevelID = @QuestionLevelID;
END
GO

CREATE OR ALTER PROCEDURE PR_QuestionLevel_Insert
    @QuestionLevel VARCHAR(100),
    @UserID INT
AS
BEGIN
    INSERT INTO MST_QuestionLevel (QuestionLevel, UserID, Created, Modified)
    VALUES (@QuestionLevel, @UserID, GETDATE(), GETDATE());
END
GO

CREATE OR ALTER PROCEDURE PR_QuestionLevel_UpdateByPK
    @QuestionLevelID INT,
    @QuestionLevel VARCHAR(100),
    @UserID INT,
    @Modified DATETIME
AS
BEGIN
    UPDATE MST_QuestionLevel
    SET QuestionLevel = @QuestionLevel, UserID = @UserID, Modified = @Modified
    WHERE QuestionLevelID = @QuestionLevelID;
END
GO

CREATE OR ALTER PROCEDURE PR_QuestionLevel_DeleteByPK
    @QuestionLevelID INT
AS
BEGIN
    DELETE FROM MST_QuestionLevel WHERE QuestionLevelID = @QuestionLevelID;
END
GO

-- ============================================
-- QUIZ STORED PROCEDURES
-- ============================================

CREATE OR ALTER PROCEDURE PR_Quiz_SelectAll
AS
BEGIN
    SELECT q.QuizID, q.QuizName, q.TotalQuestions, q.QuizDate, u.UserName, q.Created, q.Modified
    FROM MST_Quiz q
    INNER JOIN MST_User u ON q.UserID = u.UserID
    ORDER BY q.Modified DESC;
END
GO

CREATE OR ALTER PROCEDURE PR_Quiz_SelectByPK
    @QuizID INT
AS
BEGIN
    SELECT QuizID, QuizName, TotalQuestions, QuizDate, UserID, Created, Modified
    FROM MST_Quiz
    WHERE QuizID = @QuizID;
END
GO

CREATE OR ALTER PROCEDURE PR_Quiz_Insert
    @QuizName VARCHAR(100),
    @TotalQuestions INT,
    @QuizDate DATETIME,
    @UserID INT
AS
BEGIN
    INSERT INTO MST_Quiz (QuizName, TotalQuestions, QuizDate, UserID, Created, Modified)
    VALUES (@QuizName, @TotalQuestions, @QuizDate, @UserID, GETDATE(), GETDATE());
END
GO

CREATE OR ALTER PROCEDURE PR_Quiz_UpdateByPK
    @QuizID INT,
    @QuizName VARCHAR(100),
    @TotalQuestions INT,
    @QuizDate DATETIME,
    @UserID INT,
    @Modified DATETIME
AS
BEGIN
    UPDATE MST_Quiz
    SET QuizName = @QuizName, TotalQuestions = @TotalQuestions,
        QuizDate = @QuizDate, UserID = @UserID, Modified = @Modified
    WHERE QuizID = @QuizID;
END
GO

CREATE OR ALTER PROCEDURE PR_Quiz_DeleteByPK
    @QuizID INT
AS
BEGIN
    DELETE FROM MST_Quiz WHERE QuizID = @QuizID;
END
GO

-- ============================================
-- QUESTION STORED PROCEDURES
-- ============================================

CREATE OR ALTER PROCEDURE PR_Question_SelectAll
AS
BEGIN
    SELECT q.QuestionID, q.QuestionText, ql.QuestionLevel, q.OptionA, q.OptionB,
           q.OptionC, q.OptionD, q.CorrectOption, q.QuestionMarks, q.IsActive,
           u.UserName, q.Created, q.Modified
    FROM MST_Question q
    INNER JOIN MST_QuestionLevel ql ON q.QuestionLevelID = ql.QuestionLevelID
    INNER JOIN MST_User u ON q.UserID = u.UserID
    ORDER BY q.Modified DESC;
END
GO

CREATE OR ALTER PROCEDURE PR_Question_SelectByPK
    @QuestionID INT
AS
BEGIN
    SELECT QuestionID, QuestionText, QuestionLevelID, OptionA, OptionB,
           OptionC, OptionD, CorrectOption, QuestionMarks, IsActive,
           UserID, Created, Modified
    FROM MST_Question
    WHERE QuestionID = @QuestionID;
END
GO

CREATE OR ALTER PROCEDURE PR_Question_Insert
    @QuestionText NVARCHAR(MAX),
    @QuestionLevelID INT,
    @OptionA VARCHAR(100),
    @OptionB VARCHAR(100),
    @OptionC VARCHAR(100) = NULL,
    @OptionD VARCHAR(100) = NULL,
    @CorrectOption VARCHAR(100),
    @QuestionMarks INT,
    @UserID INT,
    @IsActive BIT
AS
BEGIN
    INSERT INTO MST_Question (QuestionText, QuestionLevelID, OptionA, OptionB, OptionC, OptionD,
                              CorrectOption, QuestionMarks, IsActive, UserID, Created, Modified)
    VALUES (@QuestionText, @QuestionLevelID, @OptionA, @OptionB, @OptionC, @OptionD,
            @CorrectOption, @QuestionMarks, @IsActive, @UserID, GETDATE(), GETDATE());
END
GO

CREATE OR ALTER PROCEDURE PR_Question_UpdateByPK
    @QuestionID INT,
    @QuestionText NVARCHAR(MAX),
    @QuestionLevelID INT,
    @OptionA VARCHAR(100),
    @OptionB VARCHAR(100),
    @OptionC VARCHAR(100) = NULL,
    @OptionD VARCHAR(100) = NULL,
    @CorrectOption VARCHAR(100),
    @QuestionMarks INT,
    @UserID INT,
    @IsActive BIT,
    @Modified DATETIME
AS
BEGIN
    UPDATE MST_Question
    SET QuestionText = @QuestionText, QuestionLevelID = @QuestionLevelID,
        OptionA = @OptionA, OptionB = @OptionB, OptionC = @OptionC, OptionD = @OptionD,
        CorrectOption = @CorrectOption, QuestionMarks = @QuestionMarks,
        IsActive = @IsActive, UserID = @UserID, Modified = @Modified
    WHERE QuestionID = @QuestionID;
END
GO

CREATE OR ALTER PROCEDURE PR_Question_DeleteByPK
    @QuestionID INT
AS
BEGIN
    DELETE FROM MST_Question WHERE QuestionID = @QuestionID;
END
GO

-- ============================================
-- QUIZ WISE QUESTIONS STORED PROCEDURES
-- ============================================

CREATE OR ALTER PROCEDURE PR_QuizWiseQuestions_SelectAll
AS
BEGIN
    SELECT qwq.QuizWiseQuestionsID, qz.QuizName, q.QuestionText, u.UserName,
           qwq.QuizID, qwq.QuestionID, qwq.UserID, qwq.Created, qwq.Modified
    FROM MST_QuizWiseQuestions qwq
    INNER JOIN MST_Quiz qz ON qwq.QuizID = qz.QuizID
    INNER JOIN MST_Question q ON qwq.QuestionID = q.QuestionID
    INNER JOIN MST_User u ON qwq.UserID = u.UserID
    ORDER BY qwq.Modified DESC;
END
GO

CREATE OR ALTER PROCEDURE PR_QuizWiseQuestions_SelectByPK
    @QuizWiseQuestionsID INT
AS
BEGIN
    SELECT QuizWiseQuestionsID, QuizID, QuestionID, UserID, Created, Modified
    FROM MST_QuizWiseQuestions
    WHERE QuizWiseQuestionsID = @QuizWiseQuestionsID;
END
GO

CREATE OR ALTER PROCEDURE PR_QuizWiseQuestions_Insert
    @QuizID INT,
    @QuestionID INT,
    @UserID INT
AS
BEGIN
    INSERT INTO MST_QuizWiseQuestions (QuizID, QuestionID, UserID, Created, Modified)
    VALUES (@QuizID, @QuestionID, @UserID, GETDATE(), GETDATE());
END
GO

CREATE OR ALTER PROCEDURE PR_QuizWiseQuestions_UpdateByPK
    @QuizWiseQuestionsID INT,
    @QuizID INT,
    @QuestionID INT,
    @UserID INT
AS
BEGIN
    UPDATE MST_QuizWiseQuestions
    SET QuizID = @QuizID, QuestionID = @QuestionID, UserID = @UserID, Modified = GETDATE()
    WHERE QuizWiseQuestionsID = @QuizWiseQuestionsID;
END
GO

CREATE OR ALTER PROCEDURE PR_QuizWiseQuestions_DeleteByPK
    @QuizWiseQuestionsID INT
AS
BEGIN
    DELETE FROM MST_QuizWiseQuestions WHERE QuizWiseQuestionsID = @QuizWiseQuestionsID;
END
GO

-- ============================================
-- DROPDOWN STORED PROCEDURES
-- ============================================

CREATE OR ALTER PROCEDURE PR_USER_DROPDOWN
AS
BEGIN
    SELECT UserID, UserName FROM MST_User WHERE IsActive = 1 ORDER BY UserName;
END
GO

CREATE OR ALTER PROCEDURE PR_QUIZ_DROPDOWN
AS
BEGIN
    SELECT QuizID, QuizName FROM MST_Quiz ORDER BY QuizName;
END
GO

CREATE OR ALTER PROCEDURE PR_QUESTIONLEVEL_DROPDOWN
AS
BEGIN
    SELECT QuestionLevelID, QuestionLevel FROM MST_QuestionLevel ORDER BY QuestionLevel;
END
GO

-- ============================================
-- SEED: Default Admin User (password: admin123)
-- BCrypt hash of 'admin123'
-- ============================================
IF NOT EXISTS (SELECT 1 FROM MST_User WHERE UserName = 'admin')
BEGIN
    INSERT INTO MST_User (UserName, Password, Email, Mobile, IsActive, IsAdmin, Created, Modified)
    VALUES ('admin', '$2a$11$K7rQHZGjEGP3TFPaLBqvGO5Bw4Rg5zGvQ9J3mF6kNxL8vXsEqZXXC', 'admin@quiz.com', '0000000000', 1, 1, GETDATE(), GETDATE());
END
GO

PRINT '✅ All stored procedures created successfully!';
PRINT '✅ Default admin user created (username: admin, password: admin123)';
GO
