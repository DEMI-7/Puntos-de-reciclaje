use PuntosReciclaje_DB


SELECT
	ID_Movimiento, EstadoAnterior, EstadoNuevo,Detalle, HoraMovimiento,
	ID_Admin,
	A.Nombre AS NombreAdmin,
	A.Email AS EmailAdmin,
	ID_UsuarioAfectado,
	U.Nombre AS NombreUsuario,
	U.Email AS EmailUsuario,
	U.TipoUsuario as TipoUsuario FROM RegistroAcciones Reg INNER JOIN Usuarios A ON Reg.ID_Admin = A.ID_Usuario INNER JOIN Usuarios U ON Reg.ID_UsuarioAfectado = U.ID_Usuario;


--SELECT * FROM RegistroAcciones

Insert Into RegistroAcciones
	(ID_Admin, ID_UsuarioAfectado, EstadoAnterior, EstadoNuevo, Detalle)
Values
	(@ID_Admin, @ID_UsuarioAfectado, @EstadoAnterior, @EstadoNuevo, @Detalle)

select * from Usuarios