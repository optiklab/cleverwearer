CREATE TABLE EmailAccount
( 
	Email                nvarchar(255)  NOT NULL ,
	DisplayName          nvarchar(255)  NOT NULL ,
	Id                   int IDENTITY ( 1,1 ) ,
	Host                 nvarchar(255)  NOT NULL ,
	Port                 int  NOT NULL ,
	Username             nvarchar(255)  NOT NULL ,
	Password             nvarchar(255)  NOT NULL ,
	EnableSsl            bit  NOT NULL ,
	UseDefaultCredentials bit  NOT NULL 
)
GO
ALTER TABLE EmailAccount
	ADD CONSTRAINT XPKEmailAccount PRIMARY KEY  CLUSTERED (Id ASC)
GO
CREATE TRIGGER tU_EmailAccount ON EmailAccount FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on EmailAccount */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId int,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* EmailAccount  QueuedEmail on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00023416", PARENT_OWNER="", PARENT_TABLE="EmailAccount"
    CHILD_OWNER="", CHILD_TABLE="QueuedEmail"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_162", FK_COLUMNS="EmailAccountId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,QueuedEmail
      WHERE
        /*  %JoinFKPK(QueuedEmail,deleted," = "," AND") */
        QueuedEmail.EmailAccountId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '%s Cannot update EmailAccount because QueuedEmail exists.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* EmailAccount  MessageTemplate on parent update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="EmailAccount"
    CHILD_OWNER="", CHILD_TABLE="MessageTemplate"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_163", FK_COLUMNS="EmailAccountId" */
  IF
    /* %ParentPK(" OR",UPDATE) */
    UPDATE(Id)
  BEGIN
    IF EXISTS (
      SELECT * FROM deleted,MessageTemplate
      WHERE
        /*  %JoinFKPK(MessageTemplate,deleted," = "," AND") */
        MessageTemplate.EmailAccountId = deleted.Id
    )
    BEGIN
      SELECT @errno  = 30005,
             @errmsg = '%s Cannot update EmailAccount because MessageTemplate exists.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END