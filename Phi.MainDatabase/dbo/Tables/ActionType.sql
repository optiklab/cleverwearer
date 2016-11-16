CREATE TABLE ActionType
( 
	Id                   int IDENTITY ( 1,1 ) ,	
	Code                 int NULL ,
	Name                 nvarchar(255)  NULL ,
	[Description]        nvarchar(1000)  NULL ,
	LanguageId           integer  NULL ,
	ShowOrder            int  NOT NULL 
)
GO
ALTER TABLE ActionType
	ADD CONSTRAINT R_230 FOREIGN KEY (LanguageId) REFERENCES Language(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE ActionType
	ADD CONSTRAINT XPKActionType PRIMARY KEY  CLUSTERED (Id ASC)
GO
CREATE TRIGGER tU_ActionType ON ActionType FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on ActionType */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* ActionType  Item on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00036732", PARENT_OWNER="", PARENT_TABLE="ActionType"
    CHILD_OWNER="", CHILD_TABLE="Item"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_227", FK_COLUMNS="ActionTypeId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,Item
      WHERE
        /*  %JoinFKPK(Item,deleted," = "," AND") */
        Item.ActionTypeId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '%s Cannot update ActionType because Item exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* ActionType  Factor on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="ActionType"
    CHILD_OWNER="", CHILD_TABLE="Factor"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_228", FK_COLUMNS="ActionTypeId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,Factor
      WHERE
        /*  %JoinFKPK(Factor,deleted," = "," AND") */
        Factor.ActionTypeId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '%s Cannot update ActionType because Factor exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Language  ActionType on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Language"
    CHILD_OWNER="", CHILD_TABLE="ActionType"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_230", FK_COLUMNS="LanguageId" */
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
             @errmsg = '%s Cannot update ActionType because Language does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END