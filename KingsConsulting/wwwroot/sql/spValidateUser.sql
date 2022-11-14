use F22_ksayking
GO

drop procedure if exists spValidateUser
GO

create procedure spValidateUser
(
    @email varchar(100),
    @passcode varchar(100)
)
as
begin
    select userId, email, firstName, lastName
    from UserInfo
    where email = @email
    and passcode = hashbytes('SHA2_256', concat(passwordSalt, @passcode))
end
go

-- exec spValidateUser 'test@test.com', 'password'
-- go