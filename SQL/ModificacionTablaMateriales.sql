USE PuntosReciclaje_DB

GO

-- Agregar la columna para guardar la ruta del logo del material
ALTER TABLE Materiales
ADD UrlLogo VARCHAR(500) NULL;

GO

ALTER TABLE Materiales ADD Activo BIT CONSTRAINT DF_Materiales DEFAULT 1 NOT NULL;