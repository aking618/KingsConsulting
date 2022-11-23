use F22_ksayking
go

drop procedure if exists spCreateUser
go

create procedure spCreateUser
    (
    @firstName varchar(100),
    @lastName varchar(100),
    @phone varchar(100),
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
        (firstName, lastName, email, phoneNumber, passcode, passwordSalt)
    values
        (
            @firstName, @lastName, @email, @phone, @hashPass, @salt
    )

    select userId, firstName, lastName, phoneNumber, email
    from UserInfo
    where userId = SCOPE_IDENTITY()
end
go