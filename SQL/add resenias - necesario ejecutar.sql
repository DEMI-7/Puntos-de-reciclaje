USE PuntosReciclaje_DB;
GO

SELECT ID_Reseña, Reseña, Activo, MotivoEliminacion 
FROM dbo.[Reseñas] 
WHERE Activo = 0;