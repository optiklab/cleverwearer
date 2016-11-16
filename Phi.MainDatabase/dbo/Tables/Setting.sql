CREATE TABLE Setting
( 
	Name                 nvarchar(255)  NOT NULL ,
	Value                nvarchar(255)  NOT NULL ,
	Id                   int IDENTITY ( 1,1 ) 
)
GO
ALTER TABLE Setting
	ADD CONSTRAINT XPK_Settings PRIMARY KEY  CLUSTERED (Id ASC)