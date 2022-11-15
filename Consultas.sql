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

