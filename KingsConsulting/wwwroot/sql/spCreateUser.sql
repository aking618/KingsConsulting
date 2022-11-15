use F22_ksayking
go

drop procedure if exists spCreateUser
go

create procedure spCreateUser
    (
    @firstName varchar(100),
    @lastName varchar(100),
    @email varchar(100),
    @passcode varchar(100)
)
as
begin
    declare @randomText uniqueIdentifier = NewId()
    declare @salt as char(100)
    set @salt = hashbytes('SHA2_256', convert(char(100), @randomText))

    declare @hashPass varbinary(max)
    set @hashPass = hashbytes('SHA2_256', concat(@salt, @passcode))

    insert into UserInfo
        (firstName, lastName, email, passcode, passwordSalt)
    values
        (
            @firstName, @lastName, @email, @hashPass, @salt
    )

    select userId, firstName, lastName, email
    from UserInfo
    where userId = SCOPE_IDENTITY()
end
go