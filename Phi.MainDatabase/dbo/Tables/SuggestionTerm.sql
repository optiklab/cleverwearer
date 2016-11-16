CREATE TABLE SuggestionTerm
( 
	Id                   int IDENTITY ( 1,1 ) ,
	Code                 nvarchar(255)  NULL ,
	Name                 nvarchar(1000)  NULL ,
	Value                int  NULL ,
	LanguageId           integer  NULL 
)
GO
ALTER TABLE SuggestionTerm
	ADD CONSTRAINT R_253 FOREIGN KEY (LanguageId) REFERENCES Language(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE SuggestionTerm
	ADD CONSTRAINT XPKSuggestionTerms PRIMARY KEY  CLUSTERED (Id ASC)
GO
CREATE TRIGGER tU_SuggestionTerm ON SuggestionTerm FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on SuggestionTerm */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* Language  SuggestionTerm on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00016c31", PARENT_OWNER="", PARENT_TABLE="Language"
    CHILD_OWNER="", CHILD_TABLE="SuggestionTerm"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_253", FK_COLUMNS="LanguageId" */
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
             @errmsg = '%s Cannot update SuggestionTerm because Language does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END