USE PuntosReciclaje_DB
GO
-- Materiales
INSERT INTO Materiales (Nombre, Info)
VALUES
('Neumáticos','Los neumáticos usados pueden reciclarse para evitar que terminen acumulados en basurales. Primero se trituran y se separan sus componentes, como caucho, acero y fibras textiles. El caucho recuperado puede utilizarse para fabricar pisos de goma, superficies deportivas, aislantes y otros productos.'),
('Tetra Pak','Los envases Tetra Pak están compuestos por varias capas de cartón, plástico y aluminio. Para reciclarlos se procesan mediante técnicas que permiten separar sus componentes. El cartón recuperado puede utilizarse para fabricar papel, mientras que el plástico y el aluminio pueden aprovecharse para producir nuevos materiales.'),
('Madera','La madera reciclable proviene de muebles viejos, pallets, cajones y restos de construcción. Una vez recolectada, se clasifica y se retiran clavos, tornillos y otros elementos. Después puede triturarse para fabricar tableros aglomerados, utilizarse en nuevos productos o aprovecharse como material combustible adecuado.'),
('Cobre','El cobre se encuentra principalmente en cables eléctricos, motores, tuberías y componentes electrónicos. Para reciclarlo se extrae de los productos que lo contienen, se separa de otros materiales y se procesa para recuperar el metal. Luego puede utilizarse en nuevos cables, instalaciones eléctricas y componentes industriales.'),
('Hierro','El hierro es un material metálico presente en herramientas, estructuras, electrodomésticos y piezas de vehículos. Para reciclarlo se separa de otros residuos, se clasifica y se funde en instalaciones industriales. Posteriormente se utiliza para fabricar nuevas piezas, barras de construcción y estructuras metálicas.'),
('Aluminio','El aluminio es un metal muy utilizado en latas de bebidas, bandejas y envases. Su reciclaje consiste en recolectarlo, limpiarlo, compactarlo y fundirlo para obtener nuevos lingotes. Estos pueden utilizarse para fabricar nuevamente latas, piezas industriales y diferentes productos metálicos.'),
('Papel','El papel usado, como diarios, revistas y hojas de oficina, puede recuperarse para fabricar nuevos productos. Primero se clasifica y se mezcla con agua para separar sus fibras. Luego se eliminan las impurezas y se obtiene una pasta que se utiliza para producir papel reciclado, cartón y otros materiales.'),
('Plástico','El plástico se encuentra en botellas, envases, bolsas y numerosos productos de uso cotidiano. Durante el reciclaje se clasifica según su tipo, se limpia y se tritura en pequeños fragmentos. Después se procesa para obtener materia prima que permite fabricar nuevos envases, fibras textiles y elementos plásticos.'),
('Vidrio','El vidrio es un material que puede reciclarse muchas veces sin perder sus propiedades. Se recolecta, se separa por color, se limpia y se tritura para fundirlo a altas temperaturas. Luego se utiliza para fabricar nuevos envases, botellas, frascos y otros objetos de vidrio.'),
('Cartón','El cartón es un material reciclable que se obtiene principalmente de cajas, envases y embalajes. Para reciclarlo, se clasifica, se tritura y se mezcla con agua para formar una pasta de fibras que luego se utiliza para fabricar nuevas cajas, papel y otros productos de cartón.');

GO
-- Usuarios
INSERT INTO Usuarios (Nombre, Email, Contraseña_hash, TipoUsuario, EstadoCuenta) VALUES ('Demian', 'demiangomez504@gmail.com', '12345678', 'admin', 'aprobado')

INSERT INTO Usuarios (Nombre, Email, Contraseña_hash, TipoUsuario, EstadoCuenta) VALUES
('Ladiar Arcor','arcorpuntoverde@ladiar.com.ar','123456','empresa','pendiente'),
('Jose C. Paz','josecpazmunicipio@jcp.gob.ar','123456','gubernamental','pendiente'),
('Carla Gomez', 'carla@gmail.com', '123456', 'basico', 'aprobado'),
('Martin Perez', 'martin@gmail.com', '123456', 'basico', 'aprobado'),
('Lucia Fernandez', 'lucia@gmail.com', '123456', 'empresa', 'pendiente'),
('Esteban Quito', 'esteban@gmail.com', '123456', 'basico', 'aprobado'),
('Valeria Rios', 'valeria@gmail.com', '123456', 'basico', 'aprobado'),
('Marcos Lopez', 'marcos@gmail.com', '123456', 'empresa', 'pendiente'),
('Sofia Benitez', 'sofia@gmail.com', '123456', 'gubernamental', 'aprobado'),
('Lucas Acuña', 'lucas@gmail.com', '123456', 'basico', 'aprobado'),
('Florencia Diaz', 'florencia@gmail.com', '123456', 'empresa', 'pendiente');

GO

-- Puntos reciclaje
INSERT INTO Puntos_reciclaje (ID_UsuarioCreador, Direccion,Latitud, Longitud, Horario) VALUES (1, 'Guillermo Miller 178',-34.534306,-58.792742, 'Lun a Vie de 8AM hasta 16PM' )
GO
INSERT INTO Material_PuntoReciclaje (ID_Punto, ID_Material) VALUES (1,1), (1,2), (1,3);

GO
-- 1. Insertamos Puntos de Reciclaje en distintas zonas de Buenos Aires
INSERT INTO Puntos_reciclaje (ID_UsuarioCreador, Direccion, Latitud, Longitud, Horario) VALUES 
(1, 'Av. Santa Fe 3200, CABA', -34.588500, -58.407200, 'Lun a Sab de 9:00 a 18:00'),
(1, 'Av. Cabildo 2100, CABA', -34.562300, -58.456100, 'Lun a Vie de 8:00 a 17:00'),
(1, 'Av. Rivadavia 5400, CABA', -34.619800, -58.443500, 'Lun a Sáb de 10:00 a 19:00'),
(1, 'Av. Centenario 450, San Isidro', -34.471200, -58.513400, 'Lun a Vie de 8:30 a 16:30'),
(1, 'Av. Maipú 1200, Vicente López', -34.524100, -58.479800, 'Lunes a Domingo de 24hs'),
(1, 'Av. Mitre 800, Avellaneda', -34.662300, -58.365400, 'Lun a Vie de 9:00 a 15:00');

GO
-- 2. Asociamos materiales (IDs del 1 al 10) a cada punto recién creado
-- (Asumiendo que los ID de los puntos insertados son del 2 en adelante, o usando SCOPE_IDENTITY, 
--  pero para pruebas directas podemos mapearlos por los ID que te genere la base).
-- Por ejemplo, asociamos varios tipos de materiales a cada punto:

INSERT INTO Material_PuntoReciclaje (ID_Punto, ID_Material) VALUES 
-- Punto 2 (Palermo) recibe plásticos, papeles y cartón (ej: materiales 1, 2, 3)
(2, 1), (2, 2), (2, 3),
-- Punto 3 (Belgrano) recibe vidrio y metales (ej: 4, 5)
(3, 4), (3, 5),
-- Punto 4 (Caballito) recibe electrónicos y pilas (ej: 6, 7)
(4, 6), (4, 7),
-- Punto 5 (San Isidro) recibe varios mixtos (ej: 1, 3, 8)
(5, 1), (5, 3), (5, 8),
-- Punto 6 (Vicente López) recibe plásticos y vidrio (ej: 2, 4)
(6, 2), (6, 4),
-- Punto 7 (Avellaneda) recibe cartón y metales (ej: 3, 5, 10)
(7, 3), (7, 5), (7, 10);