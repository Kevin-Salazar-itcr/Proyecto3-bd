--Funcion para sacar la cantidad de veces que un producto fue cotizado

create function CantVecesProductoCotizado(@consecutivo VARCHAR(20))
returns int
as
begin
	declare @result int;
	select @result = count(codigo_producto) from productosXcotizacion where @consecutivo = codigo_producto
	return @result
end


-- Vista para el Top 15 productos mas cotizados


CREATE VIEW TopProductosCotizados
AS
select top 15  p.nombre, p.descripcion, p.precio, dbo.CantVecesProductoCotizado(codigo) as [veces cotizado] 
from producto p 
where  dbo.CantVecesProductoCotizado(codigo)>0
order by [veces cotizado]  DESC

----------------------------------------------------------------------------------------------------------

create function CantVecesProductoVendido(@consecutivo VARCHAR(20))
returns int
as
begin
	declare @result int;
	select @result = count(codigo_producto) 
	from productosXcotizacion 
	JOIN cotizaciones on productosXcotizacion.numero_cotizacion = cotizaciones.numeroCotizacion
	JOIN probabilidad on cotizaciones.probabilidad = probabilidad.id
	where @consecutivo = codigo_producto and probabilidad.id = 4
	return @result
end


-- Vista para el Top 15 productos mas vendidos

CREATE VIEW TopProductosVendidos
AS
select top 15  p.nombre, p.descripcion,  dbo.CantVecesProductoVendido(codigo) as [veces vendido] 
from producto p 
where  dbo.CantVecesProductoCotizado(codigo) > 0
order by [veces vendido]  DESC

----------------------------------------------------------------------------------------------------------

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


CREATE VIEW ContactosXusuario
AS
select nombre + ' ' + apellido1 + ' ' +apellido2 as Nombre  ,dbo.CantidadContactosUsuario(cedula) as [Cantidad de contactos] 
from usuario
----------------------------------------------------------------------------------------------------------
insert into tarea values (3, '2020-05-12', '2019-05-12', 'Hola mundo', 118470507, 1)
insert into tarea values (4, '2020-05-12', '2018-05-12', 'Hola mundo', 118470507, 1)
insert into tarea values (5, '2020-05-12', '2017-05-12', 'Hola mundo', 118470507, 1)

-- Vista para el  Top 15 de tareas sin cerrar más antiguas.

Create view tareasSinCerrar
as
select top 15 fechaCreacion, informacion, usuario.nombre + ' '+ usuario.apellido1 + ' ' + usuario.apellido2 AS Nombre
from tarea 
join usuario ON tarea.asesor = usuario.cedula
where tarea.estado = 1
order by fechaCreacion 

-- Vista para el Top 10 de cotizaciones con diferencia entre creación y cierre más altos (cotización, cliente y cantidad de días de diferencia).

create view DiferEnciaDiasCot
as
select top 10 numeroCotizacion, nombreOportunidad, nombreCuenta, DATEDIFF(DAY, fechaCotizacion, fechaCierra) as [Días de diferencía]
from cotizaciones 
join cliente on cotizaciones.nombreCuenta = cliente.nombre_cuenta
order by [Días de diferencía] DESC

----------------------------------------------------------------------------------------------------------------

-- FUncion para sacar el total de ventas que tiene un cliente con mayores ventas.

create function totalDeLaVenta(@cliente VARCHAR(20))
returns decimal(9,2)
as
begin
	declare @result decimal(9,2);
	select @result = sum(productosXcotizacion.precioNegociado * productosXcotizacion.cantidad) 
	from cotizaciones 
	Join cliente on cotizaciones.nombreCuenta = cliente.nombre_cuenta
	Join productosXcotizacion on cotizaciones.numeroCotizacion = productosXcotizacion.numero_cotizacion
	where @cliente = cotizaciones.nombreCuenta and cotizaciones.probabilidad = 4
	return @result
end

-- Vista que retorna el top 10 de los clientes con mayores ventas
CREATE view topVentasClientes
as
select top 10 cliente.nombre_cuenta , dbo.totalDeLaVenta(nombre_cuenta) as [Venta total]
from cliente
order by [Venta total] DESC

----------------------------------------------------------------------------------------------------------------

--  Funcion que devuelve el total de actividades por cotiZacion 


create function totalActividades(@cot VARCHAR(20))
returns INT 
as
begin
	declare @result int;
	select @result = count(numero_cotizacion) 
	from actividadXcotizacion 
	where @cot = numero_cotizacion
	return @result
end

--  Funcion que devuelve el total de tareas por cotiZacion 

create function totalTareas(@cot VARCHAR(20))
returns INT 
as
begin
	declare @result int;
	select @result = count(numero_cotizacion) 
	from tareaXcotizacion 
	where @cot = numero_cotizacion
	return @result
end



-- VISTA QUE RETORNA Top 10 de cotizaciones con más actividades y tareas (sumadas juntas).
CREATE view TotalTareasYactividades
as
select top 10 numeroCotizacion, nombreOportunidad, dbo.totalActividades(numeroCotizacion) + dbo.totalTareas(numeroCotizacion) as [Total tareas y actividades]
from cotizaciones
order by[Total tareas y actividades] DESC

-----------------------------------------------------------------------------------------------------------
-- Funcion que devuelve el total de las ventas por asesor/usuraio

create function VentaVendedor(@asesor VARCHAR(20))
returns decimal(9,2)
as
begin
	declare @result decimal(9,2);
	select @result = sum(productosXcotizacion.precioNegociado * productosXcotizacion.cantidad) 
	from cotizaciones 
	Join productosXcotizacion on cotizaciones.numeroCotizacion = productosXcotizacion.numero_cotizacion
	where @asesor = asesor and cotizaciones.probabilidad = 4
	return @result
end

--Vista para el top 10 de vendedores con mayores ventas 
CREATE view topVentasVendedor
as
select top 10 nombre+' '+apellido1+' '+apellido1 AS Vendedor , dbo.VentaVendedor(cedula) as [Venta total]
from usuario
order by [Venta total] DESC

-----------------------------------------------------------------------------------------------------------

--Funcion para retornar la cantidad de cotizaciones por tipo

create function CantCotTipo(@id smallint)
returns int
as
begin
	declare @result int;
	select @result = count(tipo) 
	from  cotizaciones
	where @id = tipo
	return @result
end

--Vista que retorna la cantidad de cotiZaciones por tipo

create view CotXtipo
AS
SELECT tipo, dbo.CantCotTipo(id) as cantidad
FROM tipoCotizacion

-----------------------------------------------------------------------------------------------------------


