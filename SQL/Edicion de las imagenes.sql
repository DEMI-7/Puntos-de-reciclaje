--SELECT ID_Imagen, ID_Material, URL_Imagen FROM Imagen_Material
--use PuntosReciclaje_DB

UPDATE Materiales
SET UrlLogo = 'ImagenesMateriales/Cartón_logo.png'
WHERE ID_Material = 10; -- Cambiá el ID por el de tu material


SELECT * FROM Materiales

