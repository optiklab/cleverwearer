CREATE TABLE Alerts
( 
	Id                   integer IDENTITY ( 1,1 ) 
)
GO
ALTER TABLE Alerts
	ADD CONSTRAINT XPKAlerts PRIMARY KEY  CLUSTERED (Id ASC)