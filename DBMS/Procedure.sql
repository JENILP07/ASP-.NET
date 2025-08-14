CREATE TABLE MST_User (
    UserID INT IDENTITY(1,1) PRIMARY KEY, -- AutoIncrementing primary key
    UserName NVARCHAR(100) NOT NULL,       -- User name, cannot be null
    Password NVARCHAR(100) NOT NULL,       -- User password, cannot be null
    Email NVARCHAR(100) NOT NULL,          -- User email, cannot be null
    Mobile NVARCHAR(100) NOT NULL,         -- User mobile, cannot be null
    IsActive BIT NOT NULL DEFAULT 1,       -- Active status, defaults to 1 (active)
    IsAdmin BIT NOT NULL DEFAULT 0,        -- Admin status, defaults to 0 (not admin)
    Created DATETIME DEFAULT GETDATE(),    -- Creation timestamp, defaults to current date and time
    Modified DATETIME NOT NULL            -- Modified timestamp, cannot be null
);

CREATE PROCEDURE PR_MST_User_SelectByUserNamePassword
    @UserName VARCHAR(50),
    @Password VARCHAR(50)
AS
BEGIN
    SELECT 
        [dbo].[MST_User].[UserName],
        [dbo].[MST_User].[Password]
    FROM
        [dbo].[MST_User]
    WHERE
        (UserName = @UserName OR Email = @UserName)
    AND
        Password = @Password;
END;

CREATE PROCEDURE PR_MST_User_Insert
    @UserName NVARCHAR(100),
    @Password NVARCHAR(100),
    @Email NVARCHAR(100),
    @Mobile NVARCHAR(100),
    @IsActive BIT = 1,         -- Default to active
    @IsAdmin BIT = 0           -- Default to not admin
AS
BEGIN
    INSERT INTO [dbo].[MST_User] 
        (UserName, Password, Email, Mobile, IsActive, IsAdmin, Created, Modified)
    VALUES 
        (@UserName, @Password, @Email, @Mobile, @IsActive, @IsAdmin, GETDATE(), GETDATE());
END;


CREATE PROCEDURE PR_MST_User_SelectByUserID
    @UserID INT
AS
BEGIN
    SELECT 
        [dbo].[MST_User].[UserID],
        [dbo].[MST_User].[UserName],
        [dbo].[MST_User].[Email],
        [dbo].[MST_User].[Mobile],
        [dbo].[MST_User].[IsActive],
        [dbo].[MST_User].[IsAdmin],
        [dbo].[MST_User].[Created],
        [dbo].[MST_User].[Modified]
    FROM 
        [dbo].[MST_User]
    WHERE 
        [dbo].[MST_User].[UserID] = @UserID;
END;

CREATE PROCEDURE PR_MST_User_Update
    @UserID INT,
    @UserName NVARCHAR(100),
    @Password NVARCHAR(100),
    @Email NVARCHAR(100),
    @Mobile NVARCHAR(100),
    @IsActive BIT,
    @IsAdmin BIT
AS
BEGIN
    UPDATE [dbo].[MST_User]
    SET 
        [dbo].[MST_User].[UserName] = @UserName,
        [dbo].[MST_User].[Password] = @Password,
        [dbo].[MST_User].[Email] = @Email,
        [dbo].[MST_User].[Mobile] = @Mobile,
        [dbo].[MST_User].[IsActive] = @IsActive,
        [dbo].[MST_User].[IsAdmin] = @IsAdmin,
        [dbo].[MST_User].[Modified] = GETDATE()
    WHERE 
        [dbo].[MST_User].[UserID] = @UserID;
END;

CREATE PROCEDURE PR_MST_User_Delete
    @UserID INT
AS
BEGIN
    DELETE FROM [dbo].[MST_User]
    WHERE 
        [dbo].[MST_User].[UserID] = @UserID;
END;

CREATE PROCEDURE PR_MST_User_SelectAll
AS
BEGIN
    SELECT 
        [dbo].[MST_User].[UserID],
        [dbo].[MST_User].[UserName],
        [dbo].[MST_User].[Email],
        [dbo].[MST_User].[Mobile],
        [dbo].[MST_User].[IsActive],
        [dbo].[MST_User].[IsAdmin],
        [dbo].[MST_User].[Created],
        [dbo].[MST_User].[Modified]
    FROM 
        [dbo].[MST_User]
    ORDER BY 
        [dbo].[MST_User].[Created] DESC;
END;


CREATE TABLE [dbo].[MST_Quiz] (
    QuizID INT IDENTITY(1,1) PRIMARY KEY,          -- AutoIncrementing primary key
    QuizName NVARCHAR(100) NOT NULL,               -- Name of the quiz, cannot be null
    TotalQuestions INT NOT NULL,                   -- Total number of questions in the quiz, cannot be null
    QuizDate DATETIME NOT NULL,                    -- Date and time of the quiz, cannot be null
    UserID INT NOT NULL,                           -- User who created the quiz, foreign key reference to MST_User table
    Created DATETIME NOT NULL DEFAULT GETDATE(),   -- Timestamp for when the record was created, defaults to current date and time
    Modified DATETIME NOT NULL                     -- Timestamp for when the record was last modified
);

ALTER TABLE [dbo].[MST_Quiz]
ADD CONSTRAINT FK_MST_Quiz_User
FOREIGN KEY (UserID) REFERENCES [dbo].[MST_User](UserID)

CREATE PROCEDURE PR_MST_Quiz_Insert
    @QuizName NVARCHAR(100),
    @TotalQuestions INT,
    @QuizDate DATETIME,
    @UserID INT
AS
BEGIN
    INSERT INTO [dbo].[MST_Quiz] 
        (QuizName, TotalQuestions, QuizDate, UserID, Created, Modified)
    VALUES 
        (@QuizName, @TotalQuestions, @QuizDate, @UserID, GETDATE(), GETDATE());
END;

CREATE PROCEDURE PR_MST_Quiz_SelectByQuizID
    @QuizID INT
AS
BEGIN
    SELECT 
        [dbo].[MST_Quiz].[QuizID],
        [dbo].[MST_Quiz].[QuizName],
        [dbo].[MST_Quiz].[TotalQuestions],
        [dbo].[MST_Quiz].[QuizDate],
        [dbo].[MST_Quiz].[UserID],
        [dbo].[MST_Quiz].[Created],
        [dbo].[MST_Quiz].[Modified]
    FROM 
        [dbo].[MST_Quiz]
    WHERE 
        [dbo].[MST_Quiz].[QuizID] = @QuizID;
END;

CREATE PROCEDURE PR_MST_Quiz_Update
    @QuizID INT,
    @QuizName NVARCHAR(100),
    @TotalQuestions INT,
    @QuizDate DATETIME,
    @UserID INT
AS
BEGIN
    UPDATE [dbo].[MST_Quiz]
    SET 
        [dbo].[MST_Quiz].[QuizName] = @QuizName,
        [dbo].[MST_Quiz].[TotalQuestions] = @TotalQuestions,
        [dbo].[MST_Quiz].[QuizDate] = @QuizDate,
        [dbo].[MST_Quiz].[UserID] = @UserID,
        [dbo].[MST_Quiz].[Modified] = GETDATE()
    WHERE 
        [dbo].[MST_Quiz].[QuizID] = @QuizID;
END;

CREATE PROCEDURE PR_MST_Quiz_SelectAll
AS
BEGIN
    SELECT 
        [dbo].[MST_Quiz].[QuizID],
        [dbo].[MST_Quiz].[QuizName],
        [dbo].[MST_Quiz].[TotalQuestions],
        [dbo].[MST_Quiz].[QuizDate],
        [dbo].[MST_Quiz].[UserID],
        [dbo].[MST_Quiz].[Created],
        [dbo].[MST_Quiz].[Modified]
    FROM 
        [dbo].[MST_Quiz]
    ORDER BY 
        [dbo].[MST_Quiz].[QuizDate] DESC;
END;

CREATE TABLE [dbo].[MST_Question] (
    QuestionID INT IDENTITY(1,1) PRIMARY KEY,          -- AutoIncrementing primary key
    QuestionText NVARCHAR(MAX) NOT NULL,                -- Text of the question, cannot be null
    QuestionLevelID INT NOT NULL,                       -- Foreign key referencing MST_QuestionLevel table
    OptionA NVARCHAR(100) NOT NULL,                     -- Option A, cannot be null
    OptionB NVARCHAR(100) NOT NULL,                     -- Option B, cannot be null
    OptionC NVARCHAR(100) NOT NULL,                     -- Option C, cannot be null
    OptionD NVARCHAR(100) NOT NULL,                     -- Option D, cannot be null
    CorrectOption NVARCHAR(100) NOT NULL,               -- Correct answer option, cannot be null
    QuestionMarks INT NOT NULL,                         -- Marks for the question, cannot be null
    IsActive BIT NOT NULL DEFAULT 1,                     -- Active status, default to true (1)
    UserID INT NOT NULL,                                -- Foreign key referencing MST_User table
    Created DATETIME DEFAULT GETDATE(),                 -- Timestamp for when the record was created, defaults to current date and time
    Modified DATETIME NOT NULL                          -- Timestamp for when the record was last modified
);

-- Add Foreign Key for QuestionLevelID
ALTER TABLE [dbo].[MST_Question]
ADD CONSTRAINT FK_MST_Question_QuestionLevel
FOREIGN KEY (QuestionLevelID) REFERENCES [dbo].[MST_QuestionLevel](QuestionLevelID);

-- Add Foreign Key for UserID
ALTER TABLE [dbo].[MST_Question]
ADD CONSTRAINT FK_MST_Question_User
FOREIGN KEY (UserID) REFERENCES [dbo].[MST_User](UserID);

-- 1. SP for Creating a New Question (Insert)
CREATE PROCEDURE PR_MST_Question_Insert
    @QuestionText NVARCHAR(MAX),
    @QuestionLevelID INT,
    @OptionA NVARCHAR(100),
    @OptionB NVARCHAR(100),
    @OptionC NVARCHAR(100),
    @OptionD NVARCHAR(100),
    @CorrectOption NVARCHAR(100),
    @QuestionMarks INT,
    @UserID INT
AS
BEGIN
    INSERT INTO [dbo].[MST_Question] 
        (QuestionText, QuestionLevelID, OptionA, OptionB, OptionC, OptionD, CorrectOption, 
         QuestionMarks, IsActive, UserID, Created, Modified)
    VALUES 
        (@QuestionText, @QuestionLevelID, @OptionA, @OptionB, @OptionC, @OptionD, @CorrectOption, 
         @QuestionMarks, 1, @UserID, GETDATE(), GETDATE());
END;

-- 2. SP for Selecting a Question by QuestionID
CREATE PROCEDURE PR_MST_Question_SelectByQuestionID
    @QuestionID INT
AS
BEGIN
    SELECT 
        [dbo].[MST_Question].[QuestionID],
        [dbo].[MST_Question].[QuestionText],
        [dbo].[MST_Question].[QuestionLevelID],
        [dbo].[MST_Question].[OptionA],
        [dbo].[MST_Question].[OptionB],
        [dbo].[MST_Question].[OptionC],
        [dbo].[MST_Question].[OptionD],
        [dbo].[MST_Question].[CorrectOption],
        [dbo].[MST_Question].[QuestionMarks],
        [dbo].[MST_Question].[IsActive],
        [dbo].[MST_Question].[UserID],
        [dbo].[MST_Question].[Created],
        [dbo].[MST_Question].[Modified]
    FROM 
        [dbo].[MST_Question]
    WHERE 
        [dbo].[MST_Question].[QuestionID] = @QuestionID;
END;

-- 3. SP for Updating a Question
CREATE PROCEDURE PR_MST_Question_Update
    @QuestionID INT,
    @QuestionText NVARCHAR(MAX),
    @QuestionLevelID INT,
    @OptionA NVARCHAR(100),
    @OptionB NVARCHAR(100),
    @OptionC NVARCHAR(100),
    @OptionD NVARCHAR(100),
    @CorrectOption NVARCHAR(100),
    @QuestionMarks INT,
    @IsActive BIT,
    @UserID INT
AS
BEGIN
    UPDATE [dbo].[MST_Question]
    SET 
        [dbo].[MST_Question].[QuestionText] = @QuestionText,
        [dbo].[MST_Question].[QuestionLevelID] = @QuestionLevelID,
        [dbo].[MST_Question].[OptionA] = @OptionA,
        [dbo].[MST_Question].[OptionB] = @OptionB,
        [dbo].[MST_Question].[OptionC] = @OptionC,
        [dbo].[MST_Question].[OptionD] = @OptionD,
        [dbo].[MST_Question].[CorrectOption] = @CorrectOption,
        [dbo].[MST_Question].[QuestionMarks] = @QuestionMarks,
        [dbo].[MST_Question].[IsActive] = @IsActive,
        [dbo].[MST_Question].[UserID] = @UserID,
        [dbo].[MST_Question].[Modified] = GETDATE()
    WHERE 
        [dbo].[MST_Question].[QuestionID] = @QuestionID;
END;

-- 4. SP for Deleting a Question
CREATE PROCEDURE PR_MST_Question_Delete
    @QuestionID INT
AS
BEGIN
    DELETE FROM [dbo].[MST_Question]
    WHERE 
        [dbo].[MST_Question].[QuestionID] = @QuestionID;
END;

-- 5. SP for Selecting All Questions
CREATE PROCEDURE PR_MST_Question_SelectAll
AS
BEGIN
    SELECT 
        [dbo].[MST_Question].[QuestionID],
        [dbo].[MST_Question].[QuestionText],
        [dbo].[MST_Question].[QuestionLevelID],
        [dbo].[MST_Question].[OptionA],
        [dbo].[MST_Question].[OptionB],
        [dbo].[MST_Question].[OptionC],
        [dbo].[MST_Question].[OptionD],
        [dbo].[MST_Question].[CorrectOption],
        [dbo].[MST_Question].[QuestionMarks],
        [dbo].[MST_Question].[IsActive],
        [dbo].[MST_Question].[UserID],
        [dbo].[MST_Question].[Created],
        [dbo].[MST_Question].[Modified]
    FROM 
        [dbo].[MST_Question]
    ORDER BY 
        [dbo].[MST_Question].[Created] DESC;
END;


CREATE TABLE [dbo].[MST_QuestionLevel] (
    QuestionLevelID INT IDENTITY(1,1) PRIMARY KEY,      -- AutoIncrementing primary key
    QuestionLevel NVARCHAR(100) NOT NULL,                -- Level of the question, cannot be null
    UserID INT NOT NULL,                                 -- Foreign key referencing MST_User table
    Created DATETIME NOT NULL DEFAULT GETDATE(),         -- Timestamp for when the record was created, defaults to current date and time
    Modified DATETIME NOT NULL                            -- Timestamp for when the record was last modified
);
-- Add Foreign Key for UserID
ALTER TABLE [dbo].[MST_QuestionLevel]
ADD CONSTRAINT FK_MST_QuestionLevel_User
FOREIGN KEY (UserID) REFERENCES [dbo].[MST_User](UserID);

-- 1. SP for Creating a New Question Level (Insert)
CREATE PROCEDURE PR_MST_QuestionLevel_Insert
    @QuestionLevel NVARCHAR(100),
    @UserID INT
AS
BEGIN
    INSERT INTO [dbo].[MST_QuestionLevel] 
        (QuestionLevel, UserID, Created, Modified)
    VALUES 
        (@QuestionLevel, @UserID, GETDATE(), GETDATE());
END;


-- 2. SP for Selecting a Question Level by QuestionLevelID
CREATE PROCEDURE PR_MST_QuestionLevel_SelectByQuestionLevelID
    @QuestionLevelID INT
AS
BEGIN
    SELECT 
        [dbo].[MST_QuestionLevel].[QuestionLevelID],
        [dbo].[MST_QuestionLevel].[QuestionLevel],
        [dbo].[MST_QuestionLevel].[UserID],
        [dbo].[MST_QuestionLevel].[Created],
        [dbo].[MST_QuestionLevel].[Modified]
    FROM 
        [dbo].[MST_QuestionLevel]
    WHERE 
        [dbo].[MST_QuestionLevel].[QuestionLevelID] = @QuestionLevelID;
END;


-- 3. SP for Updating a Question Level
CREATE PROCEDURE PR_MST_QuestionLevel_Update
    @QuestionLevelID INT,
    @QuestionLevel NVARCHAR(100),
    @UserID INT
AS
BEGIN
    UPDATE [dbo].[MST_QuestionLevel]
    SET 
        [dbo].[MST_QuestionLevel].[QuestionLevel] = @QuestionLevel,
        [dbo].[MST_QuestionLevel].[UserID] = @UserID,
        [dbo].[MST_QuestionLevel].[Modified] = GETDATE()
    WHERE 
        [dbo].[MST_QuestionLevel].[QuestionLevelID] = @QuestionLevelID;
END;


-- 4. SP for Deleting a Question Level
CREATE PROCEDURE PR_MST_QuestionLevel_Delete
    @QuestionLevelID INT
AS
BEGIN
    DELETE FROM [dbo].[MST_QuestionLevel]
    WHERE 
        [dbo].[MST_QuestionLevel].[QuestionLevelID] = @QuestionLevelID;
END;


-- 5. SP for Selecting All Question Levels
CREATE PROCEDURE PR_MST_QuestionLevel_SelectAll
AS
BEGIN
    SELECT 
        [dbo].[MST_QuestionLevel].[QuestionLevelID],
        [dbo].[MST_QuestionLevel].[QuestionLevel],
        [dbo].[MST_QuestionLevel].[UserID],
        [dbo].[MST_QuestionLevel].[Created],
        [dbo].[MST_QuestionLevel].[Modified]
    FROM 
        [dbo].[MST_QuestionLevel]
    ORDER BY 
        [dbo].[MST_QuestionLevel].[Created] DESC;
END;


CREATE TABLE [dbo].[MST_QuizWiseQuestions] (
    QuizWiseQuestionsID INT IDENTITY(1,1) PRIMARY KEY,    -- Auto-incremented primary key
    QuizID INT NOT NULL,                                    -- Foreign key to the MST_Quiz table
    QuestionID INT NOT NULL,                                -- Foreign key to the MST_Question table
    UserID INT NOT NULL,                                    -- Foreign key to the MST_User table
    Created DATETIME NOT NULL DEFAULT GETDATE(),            -- Created timestamp, defaults to current date and time
    Modified DATETIME NOT NULL                               -- Modified timestamp, updated manually
);

-- Add Foreign Key Constraint for QuizID (referencing MST_Quiz)
ALTER TABLE [dbo].[MST_QuizWiseQuestions]
ADD CONSTRAINT FK_MST_QuizWiseQuestions_Quiz
FOREIGN KEY (QuizID) REFERENCES [dbo].[MST_Quiz](QuizID);

-- Add Foreign Key Constraint for QuestionID (referencing MST_Question)
ALTER TABLE [dbo].[MST_QuizWiseQuestions]
ADD CONSTRAINT FK_MST_QuizWiseQuestions_Question
FOREIGN KEY (QuestionID) REFERENCES [dbo].[MST_Question](QuestionID);

-- Add Foreign Key Constraint for UserID (referencing MST_User)
ALTER TABLE [dbo].[MST_QuizWiseQuestions]
ADD CONSTRAINT FK_MST_QuizWiseQuestions_User
FOREIGN KEY (UserID) REFERENCES [dbo].[MST_User](UserID);

-- 1. SP for Creating a New QuizWiseQuestion (Insert)
CREATE PROCEDURE PR_MST_QuizWiseQuestions_Insert
    @QuizID INT,
    @QuestionID INT,
    @UserID INT
AS
BEGIN
    INSERT INTO [dbo].[MST_QuizWiseQuestions] 
        (QuizID, QuestionID, UserID, Created, Modified)
    VALUES 
        (@QuizID, @QuestionID, @UserID, GETDATE(), GETDATE());
END;


-- 2. SP for Selecting a QuizWiseQuestion by QuizWiseQuestionsID
CREATE PROCEDURE PR_MST_QuizWiseQuestions_SelectByQuizWiseQuestionsID
    @QuizWiseQuestionsID INT
AS
BEGIN
    SELECT 
        [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID],
        [dbo].[MST_QuizWiseQuestions].[QuizID],
        [dbo].[MST_QuizWiseQuestions].[QuestionID],
        [dbo].[MST_QuizWiseQuestions].[UserID],
        [dbo].[MST_QuizWiseQuestions].[Created],
        [dbo].[MST_QuizWiseQuestions].[Modified]
    FROM 
        [dbo].[MST_QuizWiseQuestions]
    WHERE 
        [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID] = @QuizWiseQuestionsID;
END;


-- 3. SP for Updating a QuizWiseQuestion
CREATE PROCEDURE PR_MST_QuizWiseQuestions_Update
    @QuizWiseQuestionsID INT,
    @QuizID INT,
    @QuestionID INT,
    @UserID INT
AS
BEGIN
    UPDATE [dbo].[MST_QuizWiseQuestions]
    SET 
        [dbo].[MST_QuizWiseQuestions].[QuizID] = @QuizID,
        [dbo].[MST_QuizWiseQuestions].[QuestionID] = @QuestionID,
        [dbo].[MST_QuizWiseQuestions].[UserID] = @UserID,
        [dbo].[MST_QuizWiseQuestions].[Modified] = GETDATE()
    WHERE 
        [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID] = @QuizWiseQuestionsID;
END;


-- 4. SP for Deleting a QuizWiseQuestion
CREATE PROCEDURE PR_MST_QuizWiseQuestions_Delete
    @QuizWiseQuestionsID INT
AS
BEGIN
    DELETE FROM [dbo].[MST_QuizWiseQuestions]
    WHERE 
        [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID] = @QuizWiseQuestionsID;
END;


-- 5. SP for Selecting All QuizWiseQuestions
CREATE PROCEDURE PR_MST_QuizWiseQuestions_SelectAll
AS
BEGIN
    SELECT 
        [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID],
        [dbo].[MST_QuizWiseQuestions].[QuizID],
        [dbo].[MST_QuizWiseQuestions].[QuestionID],
        [dbo].[MST_QuizWiseQuestions].[UserID],
        [dbo].[MST_QuizWiseQuestions].[Created],
        [dbo].[MST_QuizWiseQuestions].[Modified]
    FROM 
        [dbo].[MST_QuizWiseQuestions]
    ORDER BY 
        [dbo].[MST_QuizWiseQuestions].[Created] DESC;
END;

