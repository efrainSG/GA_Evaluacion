create database evaluacion_eserna;
go

use evaluacion_eserna;
go

create table Tickets (
	Ticket int primary key,
	Id_Tienda varchar(255) not null,
	Id_Registradora varchar(255) not null,
	FechaHora datetime not null,
	Impuesto money not null,
	Total money not null,
	FechaHora_Creacion datetime not null default getdate()
);
go

create procedure ins_Ticket
	@Ticket int,
	@idTienda varchar(255),
	@idRegistradora varchar(255),
	@FechaHora datetime,
	@Impuesto money,
	@Total money,
	@msg varchar(255) output
as
begin
	begin try
		begin tran
			insert into Tickets (Ticket, Id_Tienda, Id_Registradora, FechaHora, Impuesto, Total)
			values (@Ticket, @idTienda, @idRegistradora, @FechaHora, @Impuesto, @Total)
		commit;
		set @msg = '';
	end try
	begin catch
		rollback;
		set @msg = ERROR_MESSAGE();
	end catch
end;
go

create table Resumen (
	Id_Tienda varchar(255) not null,
	Id_Registradora varchar(255) not null,
	Tickets int not null
);
go

create trigger trig_tickets on Tickets after Insert
as
begin
	set nocount on;

	declare @id_t varchar;
	declare @id_r varchar;
	declare @cuenta int;

	select top 1 @id_t = Id_Tienda, @id_r = Id_Registradora from inserted;
	select @cuenta = count(*) from Tickets where Id_Tienda = @id_t and Id_Registradora = @id_r;

	if exists (select 1 from Resumen where Id_Registradora = @id_r and Id_Tienda = @id_t)
	begin
		update Resumen set Tickets = @cuenta where Id_Registradora = @id_r and Id_Tienda = @id_t
	end
	else
	begin
		insert into Resumen (Id_Registradora, Id_Tienda, Tickets) values (@id_r, @id_t, @cuenta)
	end
end;
go