-- Insert into MST_QuestionLevel
CREATE PROCEDURE [dbo].[PR_QuestionLevel_Insert]
    @QuestionLevel	NVARCHAR(100),
    @UserID			INT
AS
INSERT INTO [dbo].[MST_QuestionLevel] 
(
	[QuestionLevel],
	[UserID]
)
VALUES
(
	@QuestionLevel,
	@UserID
);


-- Update MST_QuestionLevel
CREATE PROCEDURE [dbo].[PR_QuestionLevel_UpdateByPK]
    @QuestionLevelID	INT,
    @QuestionLevel		NVARCHAR(100),
    @UserID				INT,
    @Modified			DATETIME
AS
UPDATE [dbo].[MST_QuestionLevel]
	SET [QuestionLevel] = @QuestionLevel,
		[UserID] = @UserID,
		[Modified] = GETDATE()
WHERE [dbo].[MST_QuestionLevel].[QuestionLevelID] = @QuestionLevelID;


-- Delete from MST_QuestionLevel
CREATE PROCEDURE [dbo].[PR_QuestionLevel_DeleteByPK]
    @QuestionLevelID INT
AS
DELETE 
FROM [dbo].[MST_QuestionLevel]
WHERE [dbo].[MST_QuestionLevel].[QuestionLevelID] = @QuestionLevelID;


-- Select All from MST_QuestionLevel
CREATE PROCEDURE [dbo].[PR_QuestionLevel_SelectAll]
AS
    SELECT [dbo].[MST_QuestionLevel].[QuestionLevelID],
           [dbo].[MST_QuestionLevel].[QuestionLevel],
           [dbo].[MST_QuestionLevel].[UserID],
		   [dbo].[MST_User].[UserName],
           [dbo].[MST_QuestionLevel].[Created],
           [dbo].[MST_QuestionLevel].[Modified]
    FROM [dbo].[MST_QuestionLevel]

	INNER JOIN [dbo].[MST_User]
	ON [dbo].[MST_User].[UserID] = [dbo].[MST_QuestionLevel].[UserID]

	ORDER BY [dbo].[MST_QuestionLevel].[QuestionLevel],
			 [dbo].[MST_User].[UserName]


-- Select by Primary Key from MST_QuestionLevel
CREATE PROCEDURE [dbo].[PR_QuestionLevel_SelectByPK]
    @QuestionLevelID INT
AS
    SELECT [dbo].[MST_QuestionLevel].[QuestionLevelID],
           [dbo].[MST_QuestionLevel].[QuestionLevel],
           [dbo].[MST_QuestionLevel].[UserID],
           [dbo].[MST_QuestionLevel].[Created],
           [dbo].[MST_QuestionLevel].[Modified]
    FROM [dbo].[MST_QuestionLevel]
    WHERE [dbo].[MST_QuestionLevel].[QuestionLevelID] = @QuestionLevelID;

--- QUESTIONLEVEL DROPDOWN
CREATE PROCEDURE [dbo].[PR_QUESTIONLEVEL_DROPDOWN]
AS
	SELECT 
		 [dbo].[MST_QuestionLevel].[QuestionLevelID],
		 [dbo].[MST_QuestionLevel].[QuestionLevel]
	FROM [dbo].[MST_QuestionLevel]
