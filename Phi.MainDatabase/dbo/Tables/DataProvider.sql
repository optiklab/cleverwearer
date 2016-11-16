CREATE TABLE DataProvider
( 
	Id                   integer IDENTITY ( 1,1 ) ,
	Name                 nvarchar(255)  NULL ,
	ConnectionType       int  NULL ,
	Connection           nvarchar(1000)  NULL ,
	Code                 nvarchar(255)  NULL ,
	Url                  nvarchar(1000)  NULL ,
	CreateDate           datetime  NULL ,
	IsActive             bit  NOT NULL 
)
GO
ALTER TABLE DataProvider
	ADD CONSTRAINT XPKDataProviders PRIMARY KEY  CLUSTERED (Id ASC)
GO
CREATE TRIGGER tD_DataProvider ON DataProvider FOR DELETE AS
/* ERwin Builtin Trigger */
/* DELETE trigger on DataProvider */
BEGIN
  DECLARE  @errno   int,
           @errmsg  varchar(255)
    /* ERwin Builtin Trigger */
    /* DataProvider  WeatherConditions on parent delete no action */
    /* ERWIN_RELATION:CHECKSUM="00010f2f", PARENT_OWNER="", PARENT_TABLE="DataProvider"
    CHILD_OWNER="", CHILD_TABLE="WeatherConditions"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_215", FK_COLUMNS="DataProviderId" */
    IF EXISTS (
      SELECT * FROM deleted,WeatherConditions
      WHERE
        /*  %JoinFKPK(WeatherConditions,deleted," = "," AND") */
        WeatherConditions.DataProviderId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30001,
             @errmsg = '%s Cannot delete DataProvider because WeatherConditions exists.'
      GOTO ERROR
    END


    /* ERwin Builtin Trigger */
    RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END
GO
CREATE TRIGGER tU_DataProvider ON DataProvider FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on DataProvider */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId integer,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* DataProvider  WeatherConditions on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00011964", PARENT_OWNER="", PARENT_TABLE="DataProvider"
    CHILD_OWNER="", CHILD_TABLE="WeatherConditions"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_215", FK_COLUMNS="DataProviderId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,WeatherConditions
      WHERE
        /*  %JoinFKPK(WeatherConditions,deleted," = "," AND") */
        WeatherConditions.DataProviderId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '%s Cannot update DataProvider because WeatherConditions exists.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END