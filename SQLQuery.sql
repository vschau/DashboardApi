USE dashboard;
select * from orders order by placed

select * from customers
select * from servers

truncate table orders
DELETE FROM customers
DBCC CHECKIDENT ('dbo.customers',RESEED, 0)
truncate table servers

insert into customers
select 'x', 'x', 'x'

insert into orders
select 30, '2018/12/11', '2018/12/11', 1

--update customers
--set state = 'MO' where id=13

SELECT SUM([t1].[Total]) AS [Total], [t0].[Id] AS [Customerid], [t0].[Name]
FROM [Customers] AS [t0]
INNER JOIN [Orders] AS [t1] ON [t0].[Id] = [t1].[CustomerId]
GROUP BY [t0].[Id], [t0].[Name]

select * from orders order by customerid, placed desc

select distinct placed from orders order by placed desc

;with cte as (
	select distinct placed from orders
)
select o.placed,sum(total)
from orders o
full join cte on o.placed=cte.placed
where customerid=3
group by o.placed
order by 1 desc



--select c.state, sum(o.total)
--from orders o
--join customers c on o.CustomerId=c.id
--group by c.state

select Name, SUM(Total)
from orders o
join customers c on o.CustomerId=c.id
group by c.Name


--delete from orders where id>5

--delete from customers where id>=4
--use master
--go
--drop database dashboard