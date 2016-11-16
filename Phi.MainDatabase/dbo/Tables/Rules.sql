CREATE TABLE Rules
( 
	Id                   int IDENTITY ( 1,1 ) ,
	MinValue             float  NULL ,
	MaxValue             float  NULL ,
	Value                float  NULL ,
	FactorTypeId         int  NULL ,
	Result               int  NULL 
)
GO
ALTER TABLE Rules
	ADD CONSTRAINT R_190 FOREIGN KEY (FactorTypeId) REFERENCES FactorType(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE Rules
	ADD CONSTRAINT XPKRules PRIMARY KEY  CLUSTERED (Id ASC)
GO
CREATE TRIGGER tU_Rules ON Rules FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on Rules */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* FactorType  Rules on child update no action */
  /* ERWIN_RELATION:CHECKSUM="000179d2", PARENT_OWNER="", PARENT_TABLE="FactorType"
    CHILD_OWNER="", CHILD_TABLE="Rules"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_190", FK_COLUMNS="FactorTypeId" */
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
             @errmsg = '%s Cannot update Rules because FactorType does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END