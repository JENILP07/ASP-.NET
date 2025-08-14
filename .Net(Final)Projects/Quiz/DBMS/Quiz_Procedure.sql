-- Insert into MST_Quiz
CREATE PROCEDURE [dbo].[PR_Quiz_Insert]
    @QuizName		NVARCHAR(100),
    @TotalQuestions	INT,
    @QuizDate		DATETIME,
    @UserID			INT
AS
INSERT INTO [dbo].[MST_Quiz] 
(
	[QuizName],
	[TotalQuestions],
	[QuizDate],
	[UserID]
)
VALUES
(
	@QuizName,
	@TotalQuestions,
	@QuizDate,
	@UserID
);


-- Update MST_Quiz
CREATE PROCEDURE [dbo].[PR_Quiz_UpdateByPK]
    @QuizID		INT,
    @QuizName	NVARCHAR(100),
    @TotalQuestions	INT,
    @QuizDate	DATETIME,
    @UserID		INT,
    @Modified	DATETIME
AS
UPDATE [dbo].[MST_Quiz]
	SET [QuizName] = @QuizName,
		[TotalQuestions] = @TotalQuestions,
		[QuizDate] = @QuizDate,
		[UserID] = @UserID,
		[Modified] = GETDATE()
WHERE [dbo].[MST_Quiz].[QuizID] = @QuizID;


-- Delete from MST_Quiz
CREATE PROCEDURE [dbo].[PR_Quiz_DeleteByPK]
    @QuizID INT
AS
DELETE 
FROM [dbo].[MST_Quiz]
WHERE [dbo].[MST_Quiz].[QuizID] = @QuizID;


-- Select All from MST_Quiz
CREATE PROCEDURE [dbo].[PR_Quiz_SelectAll]
AS
    SELECT [dbo].[MST_Quiz].[QuizID],
           [dbo].[MST_Quiz].[QuizName],
           [dbo].[MST_Quiz].[TotalQuestions],
           [dbo].[MST_Quiz].[QuizDate],
           [dbo].[MST_Quiz].[UserID],
		   [dbo].[MST_User].[UserName],
           [dbo].[MST_Quiz].[Created],
           [dbo].[MST_Quiz].[Modified]
    FROM [dbo].[MST_Quiz]

	INNER JOIN [dbo].[MST_User]
	ON [dbo].[MST_User].[UserID] = [dbo].[MST_Quiz].[UserID]

	ORDER BY [dbo].[MST_Quiz].[QuizName],
			 [dbo].[MST_User].[UserName]


-- Select by Primary Key from MST_Quiz
CREATE PROCEDURE [dbo].[PR_Quiz_SelectByPK]
    @QuizID INT
AS
    SELECT [dbo].[MST_Quiz].[QuizID],
           [dbo].[MST_Quiz].[QuizName],
           [dbo].[MST_Quiz].[TotalQuestions],
           [dbo].[MST_Quiz].[QuizDate],
           [dbo].[MST_Quiz].[UserID],
           [dbo].[MST_Quiz].[Created],
           [dbo].[MST_Quiz].[Modified]
    FROM [dbo].[MST_Quiz]
    WHERE [dbo].[MST_Quiz].[QuizID] = @QuizID;

---QUIZ DROPDOWN
CREATE PROCEDURE [dbo].[PR_QUIZ_DROPDOWN]
AS
	SELECT 
		 [dbo].[MST_Quiz].[QuizID],
		 [dbo].[MST_Quiz].[QuizName]
	FROM [dbo].[MST_Quiz]
