CREATE TABLE ConditionDescription
( 
	Id                   int IDENTITY ( 1,1 ) ,
	ExtId                int  NOT NULL ,
	ShortDescription     nvarchar(50)  NULL ,
	[Description]          nvarchar(255)  NULL ,
	LanguageId           integer  NULL ,
	Icon                 nvarchar(255)  NULL 
)
GO
ALTER TABLE ConditionDescription
	ADD CONSTRAINT R_241 FOREIGN KEY (LanguageId) REFERENCES Language(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE ConditionDescription
	ADD CONSTRAINT XPKConditionDescription PRIMARY KEY  CLUSTERED (Id ASC)
GO
CREATE TRIGGER tU_ConditionDescription ON ConditionDescription FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on ConditionDescription */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* Language  ConditionDescription on child update no action */
  /* ERWIN_RELATION:CHECKSUM="000171f0", PARENT_OWNER="", PARENT_TABLE="Language"
    CHILD_OWNER="", CHILD_TABLE="ConditionDescription"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_241", FK_COLUMNS="LanguageId" */
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
             @errmsg = '%s Cannot update ConditionDescription because Language does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END