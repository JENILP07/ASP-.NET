---------------------------------------
--USER
-------------

-------------
--SelectAll
-------------
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_User_SelectAll]
	AS
	BEGIN
		SELECT 
			[MST_User].[UserID],
			[MST_User].[UserName],
			[MST_User].[Password],
			[MST_User].[Email],
			[MST_User].[Mobile],
			[MST_User].[IsActive],
			[MST_User].[IsAdmin],
			[MST_User].[Created],
			[MST_User].[Modified]
		FROM [MST_User]
	END

-------------
--SelectByID
-------------
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_User_SelectByID]
		@userid INT
	AS
	BEGIN
		SELECT * 
		FROM [MST_User]
		WHERE [MST_User].[UserID] = @userid
	END

-------------
--INSERT
-------------
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_User_Insert]
		@username NVARCHAR(100),
		@password NVARCHAR(100),
		@email NVARCHAR(100),
		@mobile NVARCHAR(100)
	AS
	BEGIN
		INSERT INTO [MST_User]  ([UserName], [Password], [Email], [Mobile])
		VALUES (@username, @password, @email, @mobile)
	END

-------------
--UPDATE
-------------
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_User_Update]
		@userid int,
		@username NVARCHAR(100),
		@password NVARCHAR(100),
		@email NVARCHAR(100),
		@mobile NVARCHAR(100),
		@isactive BIT,
		@isadmin BIT
	AS
	BEGIN
		UPDATE [MST_User]
		SET
			[MST_User].[UserName] = @username,
			[MST_User].[Password] = @password,
			[MST_User].[Email] = @email,
			[MST_User].[Mobile] = @mobile,
			[MST_User].[IsActive] = @isactive,
			[MST_User].[IsAdmin] = @isadmin,
			[MST_User].[Modified] = GETDATE()
		WHERE [MST_User].[UserID] = @userid
	END

-------------
--DELETE
-------------
	CREATE OR ALTER PROCEDURE [dbo].[PR_User_Delete]
		@userid INT
	AS
	BEGIN
		DELETE 
		FROM [User]
		WHERE [User].[UserID] = @userid
	END

---------------------------------------
--QUIZ
-------------

-------------
--SelectAll
-------------
	CREATE OR ALTER PROCEDURE [dbo].[PR_Quiz_SelectAll]
	AS
	BEGIN
		SELECT 
			[Quiz].[QuizID],
			[Quiz].[QuizName],
			[Quiz].[TotalQuestions],
			[Quiz].[UserID],
			[Quiz].[Created],
			[Quiz].[Modified]
		FROM [Quiz]
	END
	
-------------
--SelectByID
-------------
	CREATE OR ALTER PROCEDURE [dbo].[PR_Quiz_SelectByID]
		@Quizid INT
	AS
	BEGIN
		SELECT * 
		FROM [Quiz]
		WHERE [Quiz].[QuizID] = @Quizid
	END

-------------
--INSERT
-------------
	CREATE OR ALTER PROCEDURE [dbo].[PR_Quiz_Insert]
		@quizname NVARCHAR(100),
		@totelquestions NVARCHAR(100),
		@userid int
	AS
	BEGIN
		INSERT INTO [Quiz] ([QuizName], [TotalQuestions], [UserID])
		VALUES (@quizname, @totelquestions, @userid)
	END

-------------
--UPDATE
-------------
	CREATE OR ALTER PROCEDURE [dbo].[PR_Quiz_Update]
		@quizid INT,
		@quizname NVARCHAR(100),
		@totelquestions NVARCHAR(100),
		@userid INT
	AS
	BEGIN
		UPDATE [Quiz]
		SET
			[Quiz].[QuizName] = @quizname,
			[Quiz].[TotalQuestions] = @totelquestions,
			[Quiz].[UserID] = @userid,
			[Quiz].[Modified] = GETDATE()
		WHERE [Quiz].[QuizID] = @quizid
	END

-------------
--DELETE
-------------
	CREATE OR ALTER PROCEDURE [dbo].[PR_Quiz_Delete]
		@Quizid INT
	AS
	BEGIN
		DELETE 
		FROM [Quiz]
		WHERE [Quiz].[QuizID] = @Quizid
	END

---------------------------------------
--Question
-------------

-------------
--SelectAll
-------------
	CREATE OR ALTER PROCEDURE [dbo].[PR_Question_SelectAll]
	AS
	BEGIN
		SELECT 
			[Question].[QuestionID],
			[Question].[QuestionText],
			[Question].[QuizID],
			[Question].[OptionA],
			[Question].[OptionB],
			[Question].[OptionC],
			[Question].[OptionD],
			[Question].[CorrectOption],
			[Question].[UserID],
			[Question].[Created],
			[Question].[Modified]
		FROM [Question]
	END
	
-------------
--SelectByID
-------------
	CREATE OR ALTER PROCEDURE [dbo].[PR_Question_SelectByID]
		@Questionid INT
	AS
	BEGIN
		SELECT * 
		FROM [Question]
		WHERE [Question].[QuestionID] = @Questionid
	END

-------------
--INSERT
-------------
	CREATE OR ALTER PROCEDURE [dbo].[PR_Question_Insert]
		@QuestionText NVARCHAR(100),
		@QuizID INT,
		@OptionA NVARCHAR(100),
		@OptionB NVARCHAR(100),
		@OptionC NVARCHAR(100),
		@OptionD NVARCHAR(100),
		@CorrectOption NVARCHAR(100),
		@userid INT
	AS
	BEGIN
		INSERT INTO [Question] ([QuestionText], [QuizID], [OptionA], [OptionB], [OptionC], [OptionD], [CorrectOption], [UserID])
		VALUES (@QuestionText, @QuizID, @OptionA, @OptionB, @OptionC, @OptionD, @CorrectOption, @userid)
	END

-------------
--UPDATE
-------------
	CREATE OR ALTER PROCEDURE [dbo].[PR_Question_Update]
		@QuestionID INT,
		@QuestionText NVARCHAR(100),
		@QuizID INT,
		@OptionA NVARCHAR(100),
		@OptionB NVARCHAR(100),
		@OptionC NVARCHAR(100),
		@OptionD NVARCHAR(100),
		@CorrectOption NVARCHAR(100),
		@userid INT
	AS
	BEGIN
		UPDATE [Question]
		SET
			[Question].[QuestionText] = @QuestionText,
			[Question].[QuizID] = @QuizID,
			[Question].[OptionA] = @OptionA,
			[Question].[OptionB] = @OptionB,
			[Question].[OptionC] = @OptionC,
			[Question].[OptionD] = @OptionD,
			[Question].[CorrectOption] = @CorrectOption,
			[Question].[Modified] = GETDATE()
		WHERE [Question].[QuestionID] = @QuestionID
	END

-------------
--DELETE
-------------
	CREATE OR ALTER PROCEDURE [dbo].[PR_Question_Delete]
		@QuestionID INT
	AS
	BEGIN
		DELETE 
		FROM [Question]
		WHERE [Question].[QuestionID] = @QuestionID
	END

---------------------------------------
--QuizAttempt
-------------

-------------
--SelectAll
-------------
	CREATE OR ALTER PROCEDURE [dbo].[PR_QuizAttempt_SelectAll]
	AS
	BEGIN
		SELECT 
			[QuizAttempt].[AttemptID],
			[QuizAttempt].[QuizID],
			[QuizAttempt].[Score],
			[QuizAttempt].[AttemptDate],
			[QuizAttempt].[UserID],
			[QuizAttempt].[Created],
			[QuizAttempt].[Modified]
		FROM [QuizAttempt]
	END
	
-------------
--SelectByID
-------------
	CREATE OR ALTER PROCEDURE [dbo].[PR_QuizAttempt_SelectByID]
		@AttemptID INT
	AS
	BEGIN
		SELECT * 
		FROM [QuizAttempt]
		WHERE [QuizAttempt].[AttemptID] = @AttemptID
	END

-------------
--INSERT
-------------
	CREATE OR ALTER PROCEDURE [dbo].[PR_QuizAttempt_Insert]
		@QuizID INT,
		@score decimal(10,2),
		@attemptDate datetime,
		@userID INT
	AS
	BEGIN
		INSERT INTO [QuizAttempt] ([QuizID],[Score], [AttemptDate], [UserID])
		VALUES (@QuizID, @score, @attemptDate, @userid)
	END

-------------
--UPDATE
-------------
	CREATE OR ALTER PROCEDURE [dbo].[PR_Question_Update]
		@QuestionID INT,
		@QuestionText NVARCHAR(100),
		@QuizID INT,
		@OptionA NVARCHAR(100),
		@OptionB NVARCHAR(100),
		@OptionC NVARCHAR(100),
		@OptionD NVARCHAR(100),
		@CorrectOption NVARCHAR(100),
		@userid INT
	AS
	BEGIN
		UPDATE [Question]
		SET
			[Question].[QuestionText] = @QuestionText,
			[Question].[QuizID] = @QuizID,
			[Question].[OptionA] = @OptionA,
			[Question].[OptionB] = @OptionB,
			[Question].[OptionC] = @OptionC,
			[Question].[OptionD] = @OptionD,
			[Question].[CorrectOption] = @CorrectOption,
			[Question].[Modified] = GETDATE()
		WHERE [Question].[QuestionID] = @QuestionID
	END

-------------
--DELETE
-------------
	CREATE OR ALTER PROCEDURE [dbo].[PR_Question_Delete]
		@QuestionID INT
	AS
	BEGIN
		DELETE 
		FROM [Question]
		WHERE [Question].[QuestionID] = @QuestionID
	END