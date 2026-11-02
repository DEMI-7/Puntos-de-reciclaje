--USE PuntosReciclaje_DB

--SELECT ID_Punto, ID_UsuarioCreador, Direccion, Latitud, Longitud, Horario FROM Puntos_reciclaje

--INSERT INTO Puntos_reciclaje (ID_UsuarioCreador, Direccion,Latitud, Longitud, Horario) VALUES (1, 'Guillermo Miller 178',-34.534306,-58.792742, 'Lun a Vie de 8AM hasta 16PM' )

--SELECT * FROM Usuarios

--INSERT INTO Usuarios (Nombre, Email, Contraseña_hash, TipoUsuario, EstadoCuenta) VALUES ('Demian', 'demiangomez504@gmail.com', '12345678', 'admin', 'aprobado')

--SELECT ID_Punto, ID_Material FROM Material_PuntoReciclaje

--INSERT INTO Material_PuntoReciclaje (ID_Punto, ID_Material) VALUES (1,1), (1,2), (1,3);

--SELECT ID_Material, Nombre, Info FROM Materiales

/*
-- Consulta para traer los puntos y sus materiales
SELECT
	p.ID_Punto,
	p.ID_UsuarioCreador,
	p.Direccion, p.Latitud,
	p.Longitud,
	p.Horario,
	m.ID_Material,
	m.Nombre AS NombreMaterial
FROM Puntos_reciclaje p
LEFT JOIN Material_PuntoReciclaje pm ON p.ID_Punto = pm.ID_Punto
LEFT JOIN Materiales m ON pm.ID_Material = m.ID_Material
*/






