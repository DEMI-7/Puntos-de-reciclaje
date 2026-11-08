USE PuntosReciclaje_DB
-- Agregar la columna para guardar la ruta del logo del material
ALTER TABLE Materiales
ADD UrlLogo VARCHAR(500) NULL;