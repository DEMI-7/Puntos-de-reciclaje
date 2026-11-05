USE PuntosReciclaje_DB;
GO

-- Insertamos un usuario de prueba. Como la tabla es IDENTITY, este será el ID 1.
INSERT INTO Usuarios (Nombre, Email, Contraseña_hash, TipoUsuario, EstadoCuenta) 
VALUES ('Empresa de Prueba', 'empresa@prueba.com', '123456', 'empresa', 'aprobado');