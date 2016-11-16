CREATE TABLE UserStat
( 
	Id                   int IDENTITY ( 1,1 ) ,
	Browser              nvarchar(255)  NULL ,
	IPAddress            nvarchar(255)  NULL ,
	UserName             nvarchar(255)  NULL ,
	UserId               nvarchar(255)  NULL ,
	UserEmail            nvarchar(255)  NULL ,
	[DateTime]             datetime  NULL ,
	UrlReferrer          nvarchar(1000)  NULL ,
	[Action]               nvarchar(255)  NULL ,
	ActionPage           nvarchar(255)  NULL 
)
GO
ALTER TABLE UserStat
	ADD CONSTRAINT XPKUserStat PRIMARY KEY  CLUSTERED (Id ASC)