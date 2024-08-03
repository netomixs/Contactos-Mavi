--Insertar contacto
GO
CREATE PROCEDURE Sp_ContactosMAVIErnesto_Insert
    @Nombre VARCHAR(255) ,
    @Tipo_Contacto CHAR(2),
    @Telefono_Fijo VARCHAR(13),
    @Telefono_Movil VARCHAR(13),
    @Nacimiento DATE,
    @Sexo CHAR(1),
    @Estado_Civil CHAR(1),
    @InsertedID INT OUTPUT
AS
BEGIN
    INSERT INTO ContactosMAVIErnesto
        (Nombre, Tipo_Contacto, Telefono_Fijo,Telefono_Movil,Nacimiento,Sexo,Estado_Civil)
    VALUES
        (@Nombre, @Tipo_Contacto, @Telefono_Fijo, @Telefono_Movil, @Nacimiento, @Sexo, @Estado_Civil)
    SET @InsertedID = SCOPE_IDENTITY()
END

--Actualizar contacto
GO
CREATE PROCEDURE Sp_ContactosMAVIErnesto_Update
    @Id INT,
    @Nombre VARCHAR(255) ,
    @Tipo_Contacto CHAR(2),
    @Telefono_Fijo VARCHAR(13) = NULL,
    @Telefono_Movil VARCHAR(13),
    @Nacimiento DATE,
    @Sexo CHAR(1),
    @Estado_Civil CHAR(1)
AS
BEGIN
    UPDATE ContactosMAVIErnesto
    SET Nombre = @Nombre ,
        Tipo_Contacto = @Tipo_Contacto ,
        Telefono_Fijo = @Telefono_Fijo ,
        Telefono_Movil = @Telefono_Movil ,
        Nacimiento=@Nacimiento,
        Sexo=@Sexo,
        Estado_Civil=@Estado_Civil,
        Updated_at = GETDATE()
    WHERE Id = @Id
END
--Eliminar Contecto
GO
CREATE PROCEDURE Sp_ContactosMAVIErnesto_Delete
    @Id INT
AS
BEGIN
    DELETE FROM ContactosMAVIErnesto
    WHERE Id = @Id
END

--Selecciona contacto  por ID
GO
CREATE PROCEDURE Sp_ContactosMAVIErnesto_SelectById
    @Id INT
AS
BEGIN
    SELECT *
    FROM ContactosMAVIErnesto
    WHERE Id = @Id
END
--Selecciona Todos los contactos
GO
CREATE PROCEDURE Sp_ContactosMAVIErnesto_SelectAll
AS
BEGIN
    SELECT *
    FROM ContactosMAVIErnesto
END