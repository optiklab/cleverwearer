CREATE TABLE BlogStars
( 
	Id                   integer IDENTITY ( 1,1 ) ,
	BlogId               integer  NULL ,
	UserId               nvarchar(128)  NULL ,
	Stars                integer  NULL 
)
GO
ALTER TABLE BlogStars
	ADD CONSTRAINT R_254 FOREIGN KEY (BlogId) REFERENCES Blog(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE BlogStars
	ADD CONSTRAINT R_255 FOREIGN KEY (UserId) REFERENCES PhiUsers(Id)
		ON UPDATE NO ACTION
GO
ALTER TABLE BlogStars
	ADD CONSTRAINT XPKBlogStars PRIMARY KEY  CLUSTERED (Id ASC)
GO
CREATE TRIGGER tU_BlogStars ON BlogStars FOR UPDATE AS
/* ERwin Builtin Trigger */
/* UPDATE trigger on BlogStars */
BEGIN
  DECLARE  @NUMROWS int,
           @nullcnt int,
           @validcnt int,
           @insId integer,
           @errno   int,
           @errmsg  varchar(255)

  SELECT @NUMROWS = @@rowcount
  /* ERwin Builtin Trigger */
  /* Blog  BlogStars on child update no action */
  /* ERWIN_RELATION:CHECKSUM="0002c30b", PARENT_OWNER="", PARENT_TABLE="Blog"
    CHILD_OWNER="", CHILD_TABLE="BlogStars"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_254", FK_COLUMNS="BlogId" */
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
             @errmsg = '%s Cannot update BlogStars because Blog does not exist.'
      GOTO ERROR
    END
  END

  /* ERwin Builtin Trigger */
  /* PhiUsers  BlogStars on child update no action */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="PhiUsers"
    CHILD_OWNER="", CHILD_TABLE="BlogStars"
    P2C_VERB_PHRASE="", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_255", FK_COLUMNS="UserId" */
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
             @errmsg = '%s Cannot update BlogStars because PhiUsers does not exist.'
      GOTO ERROR
    END
  END


  /* ERwin Builtin Trigger */
  RETURN
ERROR:
    raiserror (@errno, 10, 1, @errmsg)
    rollback transaction
END