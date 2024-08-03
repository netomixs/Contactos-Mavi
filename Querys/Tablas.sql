CREATE DATABASE  Contactos
USE Contactos
 
IF NOT EXISTS (SELECT 1
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_NAME = 'ContactosMAVIErnesto')
BEGIN
    CREATE TABLE ContactosMAVIErnesto
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Nombre VARCHAR(255) NOT NULL,
        Tipo_Contacto CHAR(2),
        Telefono_Fijo VARCHAR(13) NULL,
        Telefono_Movil VARCHAR(13) NOT NULL,
        Nacimiento DATE NOT NULL,
        Sexo CHAR(1) NOT NULL,
        Estado_Civil CHAR(1) NOT NULL,
        Created_at datetime default current_timestamp,
        Updated_at datetime default current_timestamp,
        Visible bit default 1,
        CONSTRAINT opciones_contacto CHECK (Tipo_Contacto IN ('AM', 'FA', 'TR')),
        CONSTRAINT opciones_Sexo CHECK (Sexo IN ('F', 'M' )),
        CONSTRAINT opciones_EstadoCivil CHECK (Estado_Civil IN ('S', 'C','V', 'D','U')),
        CONSTRAINT solo_Numeros_Movil CHECK (Telefono_Movil NOT LIKE '%[^0-9]%'),
         CONSTRAINT solo_Numeros_Fijo CHECK (Telefono_Fijo NOT LIKE '%[^0-9]%')

    )
END;

