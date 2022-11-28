use F22_ksayking
go

drop procedure if exists spCreateOrderTables
go

create procedure spCreateOrderTables
as
begin
    create table OrderInfo
    (
        orderID int identity(1,1) constraint PK_OrderInfo primary key not null,
        orderDate datetime not null,
        userID int not null,
        shippingAddress1 varchar(100) not null,
        shippingAddress2 varchar(100) null,
        billingAddress1 varchar(100) not null,
        billingAddress2 varchar(100) null,
        foreign key (userID) references UserInfo(userId)
    )
    create table OrderContents
    (
        orderContentID int identity(1,1) constraint PK_OrderContents primary key not null,
        orderID int not null constraint FK_OrderContents_OrderInfo references OrderInfo(orderID),
        serviceID int not null constraint FK_OrderContents_ServiceType references ServiceType(serviceTypeID),
        quantity int not null,
    )
end
go