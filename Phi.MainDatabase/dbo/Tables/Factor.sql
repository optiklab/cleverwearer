CREATE TABLE Factor
( 
	Id                   int IDENTITY ( 1,1 ) ,
	Value                float  NULL ,
	FactorTypeId         int  NULL ,
	ActionTypeId         int  NULL 
)
GO
ALTER TABLE Factor
	ADD CONSTRAINT R_218 FOREIGN KEY (FactorTypeId) REFERENCES FactorType(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE Factor
	ADD CONSTRAINT R_228 FOREIGN KEY (ActionTypeId) REFERENCES ActionType(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE Factor
	ADD CONSTRAINT XPKFactor PRIMARY KEY  CLUSTERED (Id ASC)
GO
CREATE TRIGGER tU_Factor ON Factor FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on Factor */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* FactorType  Factor on child update no action */
  /* ERWIN_RELATION:CHECKSUM="0002d1b1", PARENT_OWNER="", PARENT_TABLE="FactorType"
    CHILD_OWNER="", CHILD_TABLE="Factor"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_218", FK_COLUMNS="FactorTypeId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(FactorTypeId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,FactorType
        WHERE
          /* %JoinFKPK(inserted,FactorType) */
          inserted.FactorTypeId = FactorType.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.FactorTypeId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '%s Cannot update Factor because FactorType does not exist.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* ActionType  Factor on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="ActionType"
    CHILD_OWNER="", CHILD_TABLE="Factor"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_228", FK_COLUMNS="ActionTypeId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(ActionTypeId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,ActionType
        WHERE
          /* %JoinFKPK(inserted,ActionType) */
          inserted.ActionTypeId = ActionType.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.ActionTypeId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '%s Cannot update Factor because ActionType does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END