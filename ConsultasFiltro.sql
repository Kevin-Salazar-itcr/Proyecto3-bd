use CRM
go

--Todo lo necesario para la consulta #1
--------------------------------------------------------------------------
--Funcion para retornar la venta total por cada familia de productos 


create function VentaFamilia(@codigo VARCHAR(20), @fechaini date, @fechafin date)
returns decimal(9,2)
as
begin
	declare @result decimal(9,2);
	select @result = sum(productosXcotizacion.precioNegociado * productosXcotizacion.cantidad) 
	from cotizaciones 
	Join productosXcotizacion on cotizaciones.numeroCotizacion = productosXcotizacion.numero_cotizacion
	join producto on productosXcotizacion.codigo_producto = producto.codigo
	join familia_producto on producto.codigo_familia = familia_producto.codigo
	where @codigo = familia_producto.codigo and cotizaciones.probabilidad = 4 and fechaCotizacion between @fechaini and @fechafin
	return @result
end
go


--Funcion que retorna la familia y el monto vendido

create function TotalVentaFamilia(@fechaini date, @fechafin date)
returns table
as
RETURN
(

	select top 100 nombre, dbo.VentaFamilia(codigo,@fechaini, @fechafin ) as Monto
	from familia_producto
	where  dbo.VentaFamilia(codigo,@fechaini, @fechafin )>0
	order by  dbo.VentaFamilia(codigo,@fechaini, @fechafin ) desc

)
go

--------------------------------------------------------------------------

--Todo lo necesario para la consulta #2

--Funcion que retorna la cantidad de veces que un producto fue vendido

create function CantVecesProductoVendido(@consecutivo VARCHAR(20))
returns int
as
begin
	declare @result int;
	select @result = count(codigo_producto) 
	from cotizaciones 
	JOIN productosXcotizacion on cotizaciones.numeroCotizacion = productosXcotizacion.numero_cotizacion
	where @consecutivo = codigo_producto and probabilidad = 4
	return @result
end
go

--funcion que retorna el monto total de la venta en un rango
create function VentaProducto(@codigo VARCHAR(20), @fechaini date, @fechafin date)
returns decimal(9,2)
as
begin
	declare @result decimal(9,2);
	select @result = sum(productosXcotizacion.precioNegociado * productosXcotizacion.cantidad) 
	from cotizaciones 
	Join productosXcotizacion on cotizaciones.numeroCotizacion = productosXcotizacion.numero_cotizacion
	where @codigo = productosXcotizacion.codigo_producto and cotizaciones.probabilidad = 4 and fechaCotizacion between @fechaini and @fechafin
	return @result
end
go

--Funcion que retorna la familia y el monto vendido

CREATE function TotalVentaProducto(@fechaini date, @fechafin date)
returns table
as
RETURN
(
	select TOP 10 nombre, dbo.VentaProducto(codigo,@fechaini, @fechafin ) as Monto, dbo.CantVecesProductoVendido(codigo) as Veces
	from producto
	where  dbo.VentaProducto(codigo,@fechaini, @fechafin )>0
	order bY  dbo.CantVecesProductoVendido(codigo) desc
)
GO

select * from cotizaciones
select * from dbo.TotalVentaProducto('2022-11-11', '2022-11-12')
--------------------------------------------------------------------------

--Todo lo necesario para la consulta #3

--Funcion para sacar la cantidad de veces que un producto fue cotizado
go
create function CantVecesProductoCotizado(@consecutivo VARCHAR(20))
returns int
as
begin
	declare @result int;
	select @result = count(codigo_producto) from productosXcotizacion where @consecutivo = codigo_producto
	return @result
end
go

--funcion que retorna la cantidad de productos cotizados
Create function CotProducto(@codigo VARCHAR(20), @fechaini date, @fechafin date)
returns INt
as
begin
	declare @result int
	select @result = sum(productosXcotizacion.cantidad) 
	from cotizaciones 
	Join productosXcotizacion on cotizaciones.numeroCotizacion = productosXcotizacion.numero_cotizacion
	where @codigo = productosXcotizacion.codigo_producto and fechaCotizacion between @fechaini and @fechafin
	return @result
end
go

--Funcion que acomoda las cotizaciones por cantidad de estas
CREATE function TotalCotizacionProducto(@fechaini date, @fechafin date)
returns table
as
RETURN
(
	select TOP 10 nombre, dbo.CotProducto(codigo,@fechaini, @fechafin ) as Cantidad
	from producto
	where  dbo.VentaProducto(codigo,@fechaini, @fechafin )>0
	order bY  dbo.CotProducto(codigo,@fechaini, @fechafin ) desc
)
GO
----------------------------------------------------------------------------------------------------------
--Todo lo necesario para la consulta #4

--Funcion que calcula el total de ventas por sector en un rango de fecha dado

create function VentSector(@id smallint, @fechaini date, @fechafin date)
returns decimal(9,2)
as
begin
	declare @result decimal(9,2);
	select @result = sum(productosXcotizacion.precioNegociado * productosXcotizacion.cantidad) 
	from cotizaciones 
	Join productosXcotizacion on cotizaciones.numeroCotizacion = productosXcotizacion.numero_cotizacion
	where @id = cotizaciones.sector and cotizaciones.probabilidad = 4 and fechaCotizacion between @fechaini and @fechafin
	return @result
end
go

--Funcion que retorna la tabla con los totales por sector

CREATE function TotalVentaSector(@fechaini date, @fechafin date)
returns table
as
RETURN
(
	select sector, dbo.VentSector(id,@fechaini, @fechafin ) as Total
	from sector
	where  dbo.VentSector(id,@fechaini, @fechafin )>0
)
GO


----------------------------------------------------------------------------------------------------------
--Todo lo necesario para la consulta #5

--Funcion que calcula el total de ventas por zona en un rango de fecha dado

create function VentaZona(@id smallint, @fechaini date, @fechafin date)
returns decimal(9,2)
as
begin
	declare @result decimal(9,2);
	select @result = sum(productosXcotizacion.precioNegociado * productosXcotizacion.cantidad) 
	from cotizaciones 
	Join productosXcotizacion on cotizaciones.numeroCotizacion = productosXcotizacion.numero_cotizacion
	where @id = cotizaciones.zona and cotizaciones.probabilidad = 4 and fechaCotizacion between @fechaini and @fechafin
	return @result
end
go

--Funcion que retorna la tabla con los totales por sector

CREATE function TotalVentaZona(@fechaini date, @fechafin date)
returns table
as
RETURN
(
	select zona, dbo.VentaZona(id,@fechaini, @fechafin ) as Total
	from zona
	where  dbo.VentaZona(id,@fechaini, @fechafin )>0
)
GO

-----------------------------------------------------------------------------------------------
--Todo lo necesario para la consulta #7

--Funcion que calcula el total de ventas por departamento en un rango de fecha dado

create function VentaDepartamento(@id smallint, @fechaini date, @fechafin date)
returns decimal(9,2)
as
begin
	declare @result decimal(9,2);
	select @result = sum(productosXcotizacion.precioNegociado * productosXcotizacion.cantidad) 
	from cotizaciones 
	Join productosXcotizacion on cotizaciones.numeroCotizacion = productosXcotizacion.numero_cotizacion
	join usuario on cotizaciones.asesor = usuario.cedula
	where @id = usuario.departamento and cotizaciones.probabilidad = 4 and fechaCotizacion between @fechaini and @fechafin
	return @result
end
go

--Funcion que retorna la tabla con los totales por sector

CREATE function TotalVentaDepartamento(@fechaini date, @fechafin date)
returns table
as
RETURN
(
	select nombre, dbo.VentaDepartamento(id,@fechaini, @fechafin ) as Total
	from departamento
	where  dbo.VentaDepartamento(id,@fechaini, @fechafin )>0
)
GO

----------------------------------------------------------------------

--Todo lo necesario para la consulta #10

create function VentaCliente(@cliente VARCHAR(20), @fechaini date, @fechafin date )
returns decimal(9,2)
as
begin
	declare @result decimal(9,2);
	select @result = sum(productosXcotizacion.precioNegociado * productosXcotizacion.cantidad) 
	from cotizaciones 
	Join cliente on cotizaciones.nombreCuenta = cliente.nombre_cuenta
	Join productosXcotizacion on cotizaciones.numeroCotizacion = productosXcotizacion.numero_cotizacion
	where @cliente = cotizaciones.nombreCuenta and cotizaciones.probabilidad = 4 and fechaCotizacion  between  @fechaini  and @fechafin  
	return @result
end
go


--Funcion que retorna la tabla con los totales por sector

CREATE function TotalVentaClientes(@fechaini date, @fechafin date)
returns table
as
RETURN
(
	select top 10 nombre_cuenta, dbo.VentaCliente(nombre_cuenta,@fechaini, @fechafin ) as Total
	from cliente
	where  dbo.VentaCliente(nombre_cuenta,@fechaini, @fechafin )>0
	order by dbo.VentaCliente(nombre_cuenta,@fechaini, @fechafin )
)
GO

select * from dbo.TotalVentaClientes('2022-11-10', '2022-11-10')

--view 
go
CREATE view topVentasClientes
as
select top 10 cliente.nombre_cuenta , dbo.totalDeLaVenta(nombre_cuenta) as [Venta total]
from cliente
order by [Venta total] DESC
go



----------------------------------------------------------------------
--Todo lo necesario para la consulta #11

--Funcion para calcular la cantidad de contactos por usuario
create function CantidadContactosUsuario(@cedula VARCHAR(20))
returns int
as
begin
	declare @result int;
	select @result = count(contacto.asesor) 
	from usuario 
	Join contacto on usuario.cedula = contacto.asesor
	where @cedula = contacto.asesor
	return @result
end
go

CREATE VIEW ContactosXusuario
AS
select nombre + ' ' + apellido1 + ' ' +apellido2 as Nombre  ,dbo.CantidadContactosUsuario(cedula) as [Cantidad de contactos] 
from usuario
go


----------------------------------------------------------------------
--Todo lo necesario para la consulta #12

create function VentaVendedor(@asesor VARCHAR(20), @fechaini date, @fechafin date)
returns decimal(9,2)
as
begin
	declare @result decimal(9,2);
	select @result = sum(productosXcotizacion.precioNegociado * productosXcotizacion.cantidad) 
	from cotizaciones 
	Join productosXcotizacion on cotizaciones.numeroCotizacion = productosXcotizacion.numero_cotizacion
	where @asesor = asesor and cotizaciones.probabilidad = 4 and fechaCotizacion between @fechaini and @fechafin
	return @result
end
go

CREATE function TotalVentaAsesores(@fechaini date, @fechafin date)
returns table
as
RETURN
(
	select top 10 nombre+' '+apellido1+' '+apellido2 AS Vendedor, dbo.VentaVendedor(cedula,@fechaini, @fechafin ) as Total
	from usuario
	where  dbo.VentaVendedor(cedula,@fechaini, @fechafin )>0
	order by dbo.VentaVendedor(cedula,@fechaini, @fechafin )
)
GO

-------------------------------------------------------------------
--Todo lo necesario para la consulta #15

create function totalActividades(@cot VARCHAR(20), @fechaini date,@fechafin date)
returns INT 
as
begin
	declare @result int;
	select @result = count(numero_cotizacion) 
	from actividadXcotizacion 
	JOIN cotizaciones ON actividadXcotizacion.numero_cotizacion = cotizaciones.numeroCotizacion
	where @cot = numero_cotizacion AND fechaCotizacion BETWEEN @fechaini and @fechafin
	return @result
end
go

--  Funcion que devuelve el total de tareas por cotiZacion 
create function totalTareas(@cot VARCHAR(20),  @fechaini date,@fechafin date)
returns INT 
as
begin
	declare @result int;
	select @result = count(numero_cotizacion) 
	from tareaXcotizacion 
	JOIN cotizaciones ON tareaXcotizacion.numero_cotizacion = cotizaciones.numeroCotizacion
	where @cot = numero_cotizacion AND fechaCotizacion BETWEEN @fechaini and @fechafin
	return @result
end
go

CREATE function CotizacionesTiempoDiferencia(@fechaini date, @fechafin date)
returns table
as
RETURN
(
	select top 10 numeroCotizacion, nombreOportunidad, 
	dbo.totalActividades(numeroCotizacion, @fechaini,@fechafin) 
	+ dbo.totalTareas(numeroCotizacion,  @fechaini, @fechafin) as [Total tareas y actividades]
	from cotizaciones
	order by[Total tareas y actividades] DESC

)
GO
-------------------------------------------------------------------

--Todo lo necesario para la consulta #16

--Funcion para obtener el monto de ventas por zona
create function montoXzona(@zona smallint)
returns table
as
RETURN
(
	select numeroCotizacion, SUM(precioNegociado * cantidad) as monto
	from cotizaciones, productosXcotizacion
	where zona = @zona and numeroCotizacion = numero_cotizacion
	group by numeroCotizacion
)
go

create view clientesXzona
as
select z.zona, count(c.IDzona) as cantidad, (select SUM(monto) from dbo.montoXzona(z.id)) as monto from zona z, cliente c
where z.id = c.IDzona
group by z.zona, c.IDzona, z.id
go

select * from clientesXzona

-------------------------------------------------------------------

--Todo lo necesario para la consulta #17

create function CantCotTipo(@id smallint, @fechaini date, @fechafin date)
returns int
as
begin
	declare @result int;
	select @result = count(tipo) 
	from  cotizaciones
	where @id = tipo and fechaCotizacion between @fechaini and @fechafin
	return @result 
end
go

CREATE function cotPorTipo(@fechaini date, @fechafin date)
returns table
as
RETURN
(
	SELECT tipo, dbo.CantCotTipo(id, @fechaini, @fechafin) as [cantidad]
	FROM tipoCotizacion
	
)
GO

select * from dbo.cotPorTipo('2022-11-10', '2022-11-20')


-------------------------------------------------------------------
--Todo lo necesario para la consulta #18

--Funcion que devuelve top 10 de tareas con diferencia de dias
CREATE function diferenciaDias(@fechaini date, @fechafin date)
returns table
as
RETURN
(
	select top 10 numeroCotizacion, nombreOportunidad, nombreCuenta, DATEDIFF(DAY, fechaCotizacion, fechaCierra) as [Días de diferencía]
	from cotizaciones 
	join cliente on cotizaciones.nombreCuenta = cliente.nombre_cuenta
	where fechaCotizacion between @fechaini and @fechafin
	order by [Días de diferencía] DESC
)
GO

select * from dbo.diferenciaDias('2022-11-11', '2022-11-15')

-------------------------------------------------------
--Todo lo necesario para la consulta #20

--Funcion para las tareas mas antiguas sin cerrar


create function tareasAbiertas(@fechaini date, @fechafin date)
returns table
as
RETURN
(
	select top 15 id, informacion, fechaCreacion
	from tarea 
	where tarea.estado = 1 and fechaCreacion between @fechaini and @fechafin
	order by fechaCreacion 
)
GO

select * from dbo.tareasAbiertas('2022-10-1', '2022-11-15')


insert into tarea values(1,'2022-11-11','2022-11-11','Hola','118470507', 1);
insert into tarea values(2,'2022-11-11','2022-11-14','Hola','118470507', 1);
insert into tarea values(3,'2022-11-11','2022-11-19','Hola','118470507', 1);
insert into tarea values(4,'2022-11-11','2022-10-1','Hola','118470507', 1);