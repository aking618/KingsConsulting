use F22_ksayking
go

drop procedure if exists spGetSpecificServiceByCategory
go

create procedure spGetSpecificServiceByCategory
    (
    @serviceCategoryID int
)
as
begin
    select c.ServiceCategoryName, t.serviceName, t.serviceDescription, t.servicePrice, t.serviceTypeID
    from ServiceCategory c
        join ServiceType t
        on c.ServiceCategoryID = t.ServiceCategoryID
    where c.ServiceCategoryID = @serviceCategoryID
end
go
