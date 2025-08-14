-- Insert into MST_User
CREATE or alter PROCEDURE [dbo].[PR_User_Insert]
    @UserName	NVARCHAR(100),
    @Password	NVARCHAR(100),
    @Email		NVARCHAR(100),
    @Mobile		NVARCHAR(100),
    @IsActive	BIT = 1,
    @IsAdmin	BIT = 0
AS
INSERT INTO [dbo].[MST_User] 
(
	[UserName]
	,[Password]
	,[Email]
	,[Mobile]
	,[IsActive]
	,[Created]
)
VALUES
(
	@UserName
	,@Password
	,@Email
	,@Mobile
	,@IsActive
	,@IsAdmin
);


-- Update MST_User
CREATE PROCEDURE [dbo].[PR_User_UpdateByPK]
    @UserID		INT,
    @UserName	NVARCHAR(100),
    @Password	NVARCHAR(100),
    @Email		NVARCHAR(100),
    @Mobile		NVARCHAR(100),
    @IsActive	BIT = NULL,
    @IsAdmin	BIT = NULL
AS
UPDATE [dbo].[MST_User]
	SET [UserName] = @UserName,
		[Password] = @Password,
		[Email] = @Email,
		[Mobile] = @Mobile,
		[IsActive] = CASE WHEN @IsActive IS NOT NULL THEN @IsActive ELSE [IsActive] END,
        [IsAdmin] = CASE WHEN @IsAdmin IS NOT NULL THEN @IsAdmin ELSE [IsAdmin] END,
		[Modified] = GETDATE()
WHERE [dbo].[MST_User].[UserID] = @UserID;


-- Delete from MST_User
CREATE PROCEDURE [dbo].[PR_User_DeleteByPK]
    @UserID INT
AS
DELETE 
FROM [dbo].[MST_User]
WHERE [dbo].[MST_User].[UserID] = @UserID;

-- Select All from MST_User
--EXEC [dbo].[PR_User_SelectAll]
CREATE PROCEDURE [dbo].[PR_User_SelectAll]
AS
    SELECT [dbo].[MST_User].[UserID]
		,[dbo].[MST_User].[UserName]
		,[dbo].[MST_User].[Password]
		,[dbo].[MST_User].[Email]
		,[dbo].[MST_User].[Mobile]
		,[dbo].[MST_User].[IsActive]
		,[dbo].[MST_User].[IsAdmin]
		,[dbo].[MST_User].[Created]
		,[dbo].[MST_User].[Modified]
		
	FROM [dbo].[MST_User];


-- Select by Primary Key from MST_User
CREATE PROCEDURE [dbo].[PR_User_SelectByPK]
    @UserID INT
AS
    SELECT [dbo].[MST_User].[UserID]
		,[dbo].[MST_User].[UserName]
		,[dbo].[MST_User].[Password]
		,[dbo].[MST_User].[Email]
		,[dbo].[MST_User].[Mobile]
		,[dbo].[MST_User].[IsActive]
		,[dbo].[MST_User].[IsAdmin]
		,[dbo].[MST_User].[Created]
		,[dbo].[MST_User].[Modified]
		
	FROM [dbo].[MST_User]
	WHERE [dbo].[MST_User].[UserID] = @UserID


-- Login Procedure
CREATE PROCEDURE [dbo].[PR_USER_LOGIN]
@CREDENTIAL VARCHAR(200),
@PASSWORD VARCHAR(200)
AS
	SELECT [dbo].[MST_User].[UserID]
		,[dbo].[MST_User].[UserName]
		,[dbo].[MST_User].[Password]
		,[dbo].[MST_User].[Email]
		,[dbo].[MST_User].[Mobile]
		,[dbo].[MST_User].[IsActive]
		,[dbo].[MST_User].[IsAdmin]
		,[dbo].[MST_User].[Created]
		,[dbo].[MST_User].[Modified]
		
	FROM [dbo].[MST_User]
	WHERE ([dbo].[MST_User].[UserName] = @CREDENTIAL
	OR    [dbo].[MST_User].[Email] = @CREDENTIAL
	OR    [dbo].[MST_User].[Mobile] = @CREDENTIAL)
	AND [dbo].[MST_User].[Password] = @PASSWORD

--- USER DROPDOW
CREATE PROCEDURE [dbo].[PR_USER_DROPDOWN]
AS
	SELECT
		[dbo].[MST_User].[UserID],
		[dbo].[MST_User].[UserName]
	FROM [dbo].[MST_User]

