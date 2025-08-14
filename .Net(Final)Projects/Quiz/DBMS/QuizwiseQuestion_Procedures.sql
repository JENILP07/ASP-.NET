-- Insert into MST_QuizWiseQuestions
CREATE PROCEDURE [dbo].[PR_QuizWiseQuestions_Insert]
    @QuizID		INT,
    @QuestionID	INT,
    @UserID		INT
AS
INSERT INTO [dbo].[MST_QuizWiseQuestions] 
(
	[QuizID],
	[QuestionID],
	[UserID]
)
VALUES
(
	@QuizID,
	@QuestionID,
	@UserID
);


-- Update MST_QuizWiseQuestions
CREATE PROCEDURE [dbo].[PR_QuizWiseQuestions_UpdateByPK]
    @QuizWiseQuestionsID	INT,
    @QuizID					INT,
    @QuestionID				INT,
    @UserID					INT,
    @Modified				DATETIME
AS
UPDATE [dbo].[MST_QuizWiseQuestions]
	SET [QuizID] = @QuizID,
		[QuestionID] = @QuestionID,
		[UserID] = @UserID,
		[Modified] = GETDATE()
WHERE [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID] = @QuizWiseQuestionsID;


-- Delete from MST_QuizWiseQuestions
CREATE PROCEDURE [dbo].[PR_QuizWiseQuestions_DeleteByPK]
    @QuizWiseQuestionsID INT
AS
DELETE 
FROM [dbo].[MST_QuizWiseQuestions]
WHERE [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID] = @QuizWiseQuestionsID;


-- Select All from MST_QuizWiseQuestions
CREATE PROCEDURE [dbo].[PR_QuizWiseQuestions_SelectAll]
AS
    SELECT [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID],
           [dbo].[MST_QuizWiseQuestions].[QuizID],
		   [dbo].[MST_Quiz].[QuizName],
           [dbo].[MST_QuizWiseQuestions].[QuestionID],
		   [dbo].[MST_Question].[QuestionText],
           [dbo].[MST_QuizWiseQuestions].[UserID],
		   [dbo].[MST_User].[UserName],
           [dbo].[MST_QuizWiseQuestions].[Created],
           [dbo].[MST_QuizWiseQuestions].[Modified]
    FROM [dbo].[MST_QuizWiseQuestions]

	INNER JOIN [dbo].[MST_User]
	ON [dbo].[MST_User].[UserID] = [dbo].[MST_QuizWiseQuestions].[UserID]

	INNER JOIN [dbo].[MST_Quiz]
	ON [dbo].[MST_Quiz].[QuizID] = [dbo].[MST_QuizWiseQuestions].[QuizID]

	INNER JOIN [dbo].[MST_Question]
	ON [dbo].[MST_Question].[QuestionID] = [dbo].[MST_QuizWiseQuestions].[QuestionID]

	ORDER BY [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID],
			 [dbo].[MST_User].[UserName],
			 [dbo].[MST_Quiz].[QuizName],
			 [dbo].[MST_Question].[QuestionID]

-- Select by Primary Key from MST_QuizWiseQuestions
CREATE PROCEDURE [dbo].[PR_QuizWiseQuestions_SelectByPK]
    @QuizWiseQuestionsID INT
AS
    SELECT [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID],
           [dbo].[MST_QuizWiseQuestions].[QuizID],
           [dbo].[MST_QuizWiseQuestions].[QuestionID],
           [dbo].[MST_QuizWiseQuestions].[UserID],
           [dbo].[MST_QuizWiseQuestions].[Created],
           [dbo].[MST_QuizWiseQuestions].[Modified]
    FROM [dbo].[MST_QuizWiseQuestions]
    WHERE [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID] = @QuizWiseQuestionsID;
