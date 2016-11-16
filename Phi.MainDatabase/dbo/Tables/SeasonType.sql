CREATE TABLE SeasonType
( 
	Id                   int IDENTITY ( 1,1 ) ,
	Code                 int  NOT NULL ,
	Name                 nvarchar(255)  NOT NULL ,
	LanguageId           integer  NULL 
)
GO
ALTER TABLE SeasonType
	ADD CONSTRAINT R_262 FOREIGN KEY (LanguageId) REFERENCES Language(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE SeasonType
	ADD CONSTRAINT XPKSeasonType PRIMARY KEY  CLUSTERED (Id ASC)
GO
CREATE TRIGGER tU_SeasonType ON SeasonType FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on SeasonType */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* SeasonType  SeasonViaLocation on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00028573", PARENT_OWNER="", PARENT_TABLE="SeasonType"
    CHILD_OWNER="", CHILD_TABLE="SeasonViaLocation"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_195", FK_COLUMNS="SeasonId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,SeasonViaLocation
      WHERE
        /*  %JoinFKPK(SeasonViaLocation,deleted," = "," AND") */
        SeasonViaLocation.SeasonId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '%s Cannot update SeasonType because SeasonViaLocation exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Language  SeasonType on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Language"
    CHILD_OWNER="", CHILD_TABLE="SeasonType"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_262", FK_COLUMNS="LanguageId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(LanguageId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Language
        WHERE
          /* %JoinFKPK(inserted,Language) */
          inserted.LanguageId = Language.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.LanguageId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '%s Cannot update SeasonType because Language does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END