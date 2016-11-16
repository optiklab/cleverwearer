CREATE TABLE BlogComments
( 
	Id                   integer IDENTITY ( 1,1 ) ,
	[Text]                 nvarchar(4000)  NULL ,
	UserId               nvarchar(128)  NULL ,
	BlogId               integer  NULL 
)
GO
ALTER TABLE BlogComments
	ADD CONSTRAINT R_256 FOREIGN KEY (UserId) REFERENCES PhiUsers(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE BlogComments
	ADD CONSTRAINT R_257 FOREIGN KEY (BlogId) REFERENCES Blog(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE BlogComments
	ADD CONSTRAINT XPKBlogComments PRIMARY KEY  CLUSTERED (Id ASC)
GO
CREATE TRIGGER tU_BlogComments ON BlogComments FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on BlogComments */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId integer,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* PhiUsers  BlogComments on child update no action */
  /* ERWIN_RELATION:CHECKSUM="0002b871", PARENT_OWNER="", PARENT_TABLE="PhiUsers"
    CHILD_OWNER="", CHILD_TABLE="BlogComments"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_256", FK_COLUMNS="UserId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(UserId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,PhiUsers
        WHERE
          /* %JoinFKPK(inserted,PhiUsers) */
          inserted.UserId = PhiUsers.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.UserId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '%s Cannot update BlogComments because PhiUsers does not exist.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* Blog  BlogComments on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Blog"
    CHILD_OWNER="", CHILD_TABLE="BlogComments"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_257", FK_COLUMNS="BlogId" */
  IF
    /* %ChildFK(" OR",UPDATE) */
    UPDATE(BlogId)
  BEGIN
    SELECT @nullcnt = 0
    SELECT @validcnt = count(*)
      FROM inserted,Blog
        WHERE
          /* %JoinFKPK(inserted,Blog) */
          inserted.BlogId = Blog.Id
    /* %NotnullFK(inserted," IS NULL","select @nullcnt = count(*) from inserted where"," AND") */
    select @nullcnt = count(*) from inserted where
      inserted.BlogId IS NULL
    IF @validcnt + @nullcnt != @NUMROWS
    BEGIN
      SELECT @errno  = 30007,
             @errmsg = '%s Cannot update BlogComments because Blog does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END