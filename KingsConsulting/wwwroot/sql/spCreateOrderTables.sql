use F22_ksayking
go

drop procedure if exists spCreateOrderTables
go

create procedure spCreateOrderTables
as
begin
    create table OrderInfo
    (
        orderID int identity(1,1) primary key not null,
        orderDate datetime not null,
        userID int not null,
        foreign key (userID) references UserInfo(userId)
    )
    create table OrderContents
    (
        orderContentID int identity(1,1) primary key not null,
        orderID int not null,
        serviceID int not null,
        quantity int not null,
        foreign key (orderID) references OrderInfo(orderID),
        foreign key (serviceID) references ServiceType(serviceTypeID)
    )
end
go