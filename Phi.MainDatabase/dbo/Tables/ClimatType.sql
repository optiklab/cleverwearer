CREATE TABLE ClimatType
( 
	Id                   int IDENTITY ( 1,1 ) ,
	Name                 nvarchar(255)  NULL ,
	Belt                 nvarchar(255)  NULL ,
	AlternativeNames     nvarchar(2000)  NULL 
)
GO
ALTER TABLE ClimatType
	ADD CONSTRAINT XPKClimatType PRIMARY KEY  CLUSTERED (Id ASC)
GO
CREATE TRIGGER tU_ClimatType ON ClimatType FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on ClimatType */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* ClimatType  Location on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="0000fddf", PARENT_OWNER="", PARENT_TABLE="ClimatType"
    CHILD_OWNER="", CHILD_TABLE="Location"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_193", FK_COLUMNS="ClimatId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,Location
      WHERE
        /*  %JoinFKPK(Location,deleted," = "," AND") */
        Location.ClimatId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '%s Cannot update ClimatType because Location exists.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END