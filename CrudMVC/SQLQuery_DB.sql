
create database DB_Contactos

use DB_Contactos

create table Contacto(
IdContacto int identity(1,1),
Nombre varchar (30),
Apellido varchar (30),
Telefono varchar(8),
Correo varchar(30)
)

insert into Contacto values('Luis','Pulido','87541910','teacher.luis360@gmail.com')
insert into Contacto values('Lia','Pulido','87541010','lia360@gmail.com')
insert into Contacto values('Chris','Cruz','89707593','chris@gmail.com')
insert into Contacto values('Yara','venegas','12548795','yara0@gmail.com')
insert into Contacto values('beatriz','venegas','89587451','beatriz@gmail.com')


create procedure sp_Agregar(
@nombre varchar(30),
@apellido varchar(30),
@telefono varchar (8),
@correo varchar(30)
)
as
begin
insert into Contacto values(@nombre,@apellido,@telefono,@correo)
end

create procedure sp_Editar(
@Idcontacto int,
@nombre varchar (30),
@apellido varchar (30),
@telefono varchar (8),
@correo varchar (30)
)
as
begin
	update Contacto set Nombre=@nombre, Apellido=@apellido, Telefono=@telefono, Correo=@correo 
	where IdContacto=@Idcontacto
end

create procedure sp_Eliminar(
@Idcontact int
)
as
begin
	delete from Contacto where IdContacto=@Idcontact
end



