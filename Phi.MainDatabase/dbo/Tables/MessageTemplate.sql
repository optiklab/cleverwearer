CREATE TABLE MessageTemplate
( 
	Name                 nvarchar(255)  NOT NULL ,
	BccEmailAddresses    nvarchar(255)  NOT NULL ,
	Id                   int IDENTITY ( 1,1 ) ,
	Subject              nvarchar(255)  NOT NULL ,
	Body                 nvarchar(4000)  NOT NULL ,
	IsActive             bit  NOT NULL ,
	EmailAccountId       int  NULL 
)
GO
ALTER TABLE MessageTemplate
	ADD CONSTRAINT R_163 FOREIGN KEY (EmailAccountId) REFERENCES EmailAccount(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE MessageTemplate
	ADD CONSTRAINT XPKMessageTemplate PRIMARY KEY  CLUSTERED (Id ASC)
GO
CREATE TRIGGER tU_MessageTemplate ON MessageTemplate FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on MessageTemplate */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* EmailAccount  MessageTemplate on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00018cc8", PARENT_OWNER="", PARENT_TABLE="EmailAccount"
    CHILD_OWNER="", CHILD_TABLE="MessageTemplate"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_163", FK_COLUMNS="EmailAccountId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(EmailAccountId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,EmailAccount
        WHERE
          /* %JoinFKPK(inserted,EmailAccount) */
          inserted.EmailAccountId = EmailAccount.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.EmailAccountId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '%s Cannot update MessageTemplate because EmailAccount does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END