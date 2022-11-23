use F22_ksayking
go

drop table if exists OrderContents
go
drop table if exists OrderInfo
go
drop table if exists ServiceType
go
drop table if exists ServiceCategory
go
drop table if exists UserInfo
go

exec spCreateUserTable
go

exec spCreateServiceTables
go

exec spCreateOrderTables
go