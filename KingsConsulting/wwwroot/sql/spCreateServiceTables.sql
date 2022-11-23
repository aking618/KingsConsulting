use F22_ksayking
go

drop procedure if exists spCreateServiceTables
go

create procedure spCreateServiceTables
as
begin
    create table ServiceCategory
    (
        ServiceCategoryID int identity(1,1) not null constraint pkServiceCatId primary key,
        ServiceCategoryName varchar(50) not null
    )
    insert into ServiceCategory
        (ServiceCategoryName)
    values
        ('General Consulting'),
        ('Web Development'),
        ('Mobile Development')
    create table ServiceType
    (
        serviceTypeID int identity(1,1) not null constraint pkUID primary key,
        serviceName varchar(100) not null,
        serviceDescription varchar(1000) not null,
        servicePrice int not null,
        ServiceCategoryID int not null constraint fkServiceCategoryID references ServiceCategory(ServiceCategoryID)
    )
    insert into ServiceType
        (serviceName, serviceDescription, servicePrice, ServiceCategoryID)
    values
        ('Web Consult', 'Are you looking to build a website? We can help you with that. Or maybe you need a website for your business. We can help you with that too. We can help you with any web development needs you may have.', 100, 1),
        ('Mobile Consult', 'Are you looking to build a mobile app? We can help you with that. Or maybe you need a mobile app for your business. We can help you with that too. We can help you with any mobile development needs you may have.', 100, 1),
        ('Mockup', 'Need help with the initial design of your website? We can create a foundation for you to build on.', 50, 2),
        ('Home Page', 'We will design the landing page for your website!', 150, 2),
        ('X-Pages', 'With your help, we will design X number of pages for your website. Contact us for specific pricing.', 500, 2),
        ('Mockup', 'Need help with the initial design of your mobile app? We can create a foundation for you to build on.', 50, 3),
        ('Home Page', 'We will design the landing screen for your mobile app!', 150, 3),
        ('X-Screens', 'With your help, we will design X number of screens for your mobile app. Contact us for specific pricing.', 500, 3)
end