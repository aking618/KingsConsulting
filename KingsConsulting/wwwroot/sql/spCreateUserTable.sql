use F22_ksayking
go

drop procedure if exists spCreateUserTable
go

create procedure spCreateUserTable
as
begin
    create table UserInfo
    (
        userId int identity(1,1) primary key not null,
        firstName varchar(100) not null,
        lastName varchar(100) not null,
        email varchar(100) not null,
        phoneNumber varchar(100) not null,
        passcode varbinary(max) not null,
        passwordSalt char(100) not null,
        createdOn datetime default getdate() not null
    )
end
go