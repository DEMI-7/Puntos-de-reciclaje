USE PuntosReciclaje_DB

GO

CREATE TABLE RegistroAcciones (
	ID_Movimiento BIGINT IDENTITY (1,1) PRIMARY KEY NOT NULL,
	ID_Admin BIGINT NOT NULL,
	ID_UsuarioAfectado BIGINT NOT NULL,
	EstadoAnterior VARCHAR (50) NOT NULL,
	EstadoNuevo VARCHAR (50) NOT NULL,
	Detalle VARCHAR (300) NOT NULL,
	HoraMovimiento DATETIME DEFAULT GETDATE()
)

GO

ALTER TABLE RegistroAcciones
	ADD CONSTRAINT FK_Registro_UsuarioAdmin FOREIGN KEY (ID_Admin)
	REFERENCES Usuarios (ID_Usuario);
ALTER TABLE RegistroAcciones
	ADD CONSTRAINT FK_Registro_UsuarioAfectado FOREIGN KEY (ID_UsuarioAfectado)
	REFERENCES Usuarios (ID_Usuario);