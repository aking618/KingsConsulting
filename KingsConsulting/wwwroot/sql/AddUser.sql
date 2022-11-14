use F22_ksayking
go

-- drop table if exists UserInfo
-- go

-- create table UserInfo
-- (
--     userId int identity(1,1) not null constraint pkUID primary key,
--     email varchar(100) not null,
--     firstName varchar(100) not null,
--     lastName varchar(100) not null,
--     passcode varchar(100) not null,
--     passwordSalt varchar(100) not null
-- )
-- go

declare @randomText uniqueIdentifier = NewId()
declare @salt as char(100)
set @salt = hashbytes('SHA2_256', convert(char(100), @randomText))

declare @hashPass varbinary(max)
set @hashPass = hashbytes('SHA2_256', concat(@salt, 'password'))

insert into UserInfo (firstName, lastName, email, passcode, passwordSalt) values (
    'Ayren', 'King', 'test@test.com', @hashPass, @salt
)
go

-- Path: KingsConsulting/wwwroot/sql/GetUser.sql
select * from UserInfo
go


