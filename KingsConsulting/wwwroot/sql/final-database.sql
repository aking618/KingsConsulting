use F22_ksayking
go

-- Drop all the tables
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

-- Create all the tables

-- Create the UserInfo table
create table UserInfo
    (
        userId int identity(1,1) constraint PK_UserInfo primary key not null,
        firstName varchar(100) not null,
        lastName varchar(100) not null,
        email varchar(100) not null,
        phoneNumber varchar(100) not null,
        passcode varbinary(max) not null,
        passwordSalt char(100) not null,
        createdOn datetime default getdate() not null
    )
go

-- Create the ServiceCategory table
create table ServiceCategory
    (
        ServiceCategoryID int identity(1,1) not null constraint pkServiceCatId primary key,
        ServiceCategoryName varchar(50) not null
    )
go

--- Insert the ServiceCategory data
insert into ServiceCategory
    (ServiceCategoryName)
values
    ('General Consulting'),
    ('Web Development'),
    ('Mobile Development')
go

-- Create the ServiceType table
create table ServiceType
    (
        serviceTypeID int identity(1,1) not null constraint pkUID primary key,
        serviceName varchar(100) not null,
        serviceDescription varchar(1000) not null,
        servicePrice int not null,
        serviceImage varchar(100) not null,
        ServiceCategoryID int not null constraint fkServiceCategoryID references ServiceCategory(ServiceCategoryID)
    )
go

--- Insert the ServiceType data
insert into ServiceType
    (serviceName, serviceDescription, servicePrice, serviceImage, ServiceCategoryID)
values
    ('Web Consult', 'Are you looking to build a website? We can help you with that. Or maybe you need a website for your business. We can help you with that too. We can help you with any web development needs you may have.', 100, 'consulting-1.jpg', 1),
    ('Mobile Consult', 'Are you looking to build a mobile app? We can help you with that. Or maybe you need a mobile app for your business. We can help you with that too. We can help you with any mobile development needs you may have.', 100, 'consulting-2.jpg',1),
    ('Mockup', 'Need help with the initial design of your website? We can create a foundation for you to build on.', 50, 'web-1.jpg',2),
    ('Home Page', 'We will design the landing page for your website!', 150, 'web-2.jpg',2),
    ('X-Pages', 'With your help, we will design X number of pages for your website. Contact us for specific pricing.', 500, 'web-3.jpg',2),
    ('Mockup', 'Need help with the initial design of your mobile app? We can create a foundation for you to build on.', 50, 'mobile-1.jpg',3),
    ('Home Page', 'We will design the landing screen for your mobile app!', 150, 'mobile-2.jpg',3),
    ('X-Screens', 'With your help, we will design X number of screens for your mobile app. Contact us for specific pricing.', 500, 'mobile-3.jpg',3)
go

-- Create the OrderInfo table
create table OrderInfo
    (
        orderId int identity(1,1) constraint PK_OrderInfo primary key not null,
        userId int not null constraint FK_OrderInfo_UserInfo references UserInfo(userId),
        orderDate datetime default getdate() not null,
        shippingAddress1 varchar(100) not null,
        shippingAddress2 varchar(100) null,
        billingAddress1 varchar(100) not null,
        billingAddress2 varchar(100) null
    )
go

-- Create the OrderContents table
create table OrderContents
    (
        orderContentId int identity(1,1) constraint PK_OrderContents primary key not null,
        orderId int not null constraint FK_OrderContents_OrderInfo references OrderInfo(orderId),
        serviceId int not null constraint FK_OrderContents_ServiceType references ServiceType(serviceTypeId),
        quantity int not null
    )
go


-- NOTE: refreshDatabase.sql does the same thing as this file, but splits the table creation into stored procedures.