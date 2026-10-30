CREATE DATABASE PuntosReciclaje_DB
GO
USE PuntosReciclaje_DB
GO

-- CREACION DE LAS TABLAS

CREATE TABLE Usuarios (
ID_Usuario BIGINT IDENTITY (1,1) PRIMARY KEY NOT NULL,
Nombre NVARCHAR (50) NOT NULL,
Email NVARCHAR (50) NOT NULL,
Contraseña_hash NVARCHAR (255) NOT NULL,
TipoUsuario VARCHAR (20) NOT NULL
	CONSTRAINT CK_Usuarios_Tipo
	CHECK (TipoUsuario IN ('admin','empresa', 'gubernamental','basico')),
EstadoCuenta VARCHAR (20) NOT NULL
	CONSTRAINT CK_Usuarios_EstadoCuenta
	CHECK (EstadoCuenta IN ('aprobado','pendiente','rechazado')),
Activo BIT CONTRAINT DF_Usuarios DEFAULT 1 NOT NULL
);

CREATE TABLE Reseñas (
	ID_Reseña BIGINT IDENTITY (1,1) PRIMARY KEY NOT NULL,
	ID_Usuario BIGINT NOT NULL,
	ID_PuntoReciclaje BIGINT NOT NULL,
	Reseña VARCHAR(200) NULL,
	Puntaje TINYINT NOT NULL
		CONSTRAINT CK_Reseñas_Puntaje
		CHECK (Puntaje BETWEEN 1 AND 5),
	FechaPublicacion DATETIME2 DEFAULT GETDATE()
);

CREATE TABLE Puntos_reciclaje (
	ID_Punto BIGINT IDENTITY (1,1) PRIMARY KEY NOT NULL,
	ID_UsuarioCreador BIGINT NOT NULL,
	Direccion VARCHAR (255) NOT NULL,
	Latitud DECIMAL (10,8) NOT NULL,
	Longitud DECIMAL (11,8) NOT NULL,
	Horario VARCHAR (255) NOT NULL
);

CREATE TABLE Imagen_PuntoReciclaje (
	ID_Imagen BIGINT IDENTITY (1,1) PRIMARY KEY NOT NULL,
	ID_PuntoReciclaje BIGINT NOT NULL,
	URL_Imagen VARCHAR (2000) NOT NULL
);

CREATE TABLE Material_PuntoReciclaje (
	ID_Punto BIGINT NOT NULL,
	ID_Material BIGINT NOT NULL,
);

CREATE TABLE Materiales (
	ID_Material BIGINT IDENTITY (1,1) PRIMARY KEY NOT NULL,
	Nombre VARCHAR (100) NOT NULL,
	Info NVARCHAR (MAX)
);

CREATE TABLE Imagen_Material (
	ID_Imagen BIGINT IDENTITY (1,1) PRIMARY KEY NOT NULL,
	ID_Material BIGINT NOT NULL,
	URL_Imagen VARCHAR (2000) NOT NULL
);

ALTER TABLE Imagen_Material
	ADD CONSTRAINT FK_Imagen_Material FOREIGN KEY (ID_Material)
	REFERENCES Materiales (ID_Material);
	

ALTER TABLE Material_PuntoReciclaje
	ADD CONSTRAINT FK_MaterialPunto_PuntoReciclaje FOREIGN KEY (ID_Punto)
	REFERENCES Puntos_reciclaje (ID_Punto),
	ADD CONSTRAINT FK_MaterialPunto_Materiales FOREIGN KEY (ID_Material)
	REFERENCES Materiales (ID_Material);

ALTER TABLE Reseñas
	ADD CONSTRAINT FK_Reseñas_Usuarios FOREIGN KEY (ID_Usuario)
	REFERENCES Usuarios(ID_Usuario),
	ADD CONSTRAINT FK_Reseñas_PuntoReciclaje FOREIGN KEY (ID_Punto)
	REFERENCES Puntos_reciclaje(ID_Punto);

ALTER TABLE Puntos_reciclaje
	ADD CONSTRAINT FK_Puntos_Usuarios FOREIGN KEY (ID_UsuarioCreador)
	REFERENCES Usuarios (ID_Usuario);

ALTER TABLE Imagen_PuntoReciclaje
	ADD CONSTRAINT FK_Imagen_Punto FOREIGN KEY (ID_PuntoReciclaje)
	REFERENCES Puntos_reciclaje (ID_Punto);



