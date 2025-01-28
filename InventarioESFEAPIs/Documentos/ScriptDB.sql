CREATE DATABASE InventarioDB
USE InventarioDB
-- Crear tabla Estado
CREATE TABLE Estado (
    id INT PRIMARY KEY IDENTITY,
    nombre NVARCHAR(100) NOT NULL
);

-- Crear tabla Login
CREATE TABLE [Login] (
    id INT PRIMARY KEY IDENTITY,
    correo NVARCHAR(100) UNIQUE NOT NULL,
    [password] NVARCHAR(150) NOT NULL,
);

-- Crear tabla Rol
CREATE TABLE Rol (
    id INT PRIMARY KEY IDENTITY,
    nombre NVARCHAR(100) NOT NULL,
    idEstado INT,
    FOREIGN KEY (idEstado) REFERENCES Estado(id)
);

-- Crear tabla Usuario
CREATE TABLE Usuario (
    id INT PRIMARY KEY IDENTITY,
    nombre NVARCHAR(100) NOT NULL,
    apellido NVARCHAR(100) NOT NULL,
    telefono NVARCHAR(20) NOT NULL,
    idEstado INT,
    idRol INT,
    idLogin INT,
    FOREIGN KEY (idEstado) REFERENCES Estado(id),
    FOREIGN KEY (idRol) REFERENCES Rol(id),
	FOREIGN KEY (idLogin) REFERENCES [Login](id)
);

-- Crear tabla UsuarioRol
CREATE TABLE UsuarioRol (
    id INT PRIMARY KEY IDENTITY,
    idUsuario INT,
	FechaAsignacion date not null,
    idRol INT,
    idEstado INT,
    FOREIGN KEY (idUsuario) REFERENCES Usuario(id),
    FOREIGN KEY (idRol) REFERENCES Rol(id),
    FOREIGN KEY (idEstado) REFERENCES Estado(id)
);

-- Crear tabla Control
CREATE TABLE [Control] (
    id INT PRIMARY KEY IDENTITY,
    nombre NVARCHAR(100) NOT NULL,
    [URL] NVARCHAR(200) NOT NULL,
    idEstado INT,
	idRol INT,
    FOREIGN KEY (idEstado) REFERENCES Estado(id),
	FOREIGN KEY (idRol) REFERENCES Rol(id),
);

-- Crear tabla RolControl
CREATE TABLE RolControl (
    id INT PRIMARY KEY IDENTITY,
    idRol INT,
    idControl INT,
    idEstado INT,
    FOREIGN KEY (idRol) REFERENCES Rol(id),
    FOREIGN KEY (idControl) REFERENCES Control(id),
    FOREIGN KEY (idEstado) REFERENCES Estado(id)
);

-- Crear tabla Tipo
CREATE TABLE Tipo (
    id INT PRIMARY KEY IDENTITY,
    nombre NVARCHAR(100) NOT NULL,
    idEstado INT,
    FOREIGN KEY (idEstado) REFERENCES Estado(id)
);

-- Crear tabla Categoria
CREATE TABLE Categoria (
    id INT PRIMARY KEY IDENTITY,
    nombre NVARCHAR(100) NOT NULL,
    descripcion NVARCHAR(255) NOT NULL,
    idEstado INT,
    FOREIGN KEY (idEstado) REFERENCES Estado(id)
);

-- Crear tabla Marca
CREATE TABLE Marca (
    id INT PRIMARY KEY IDENTITY,
    nombre NVARCHAR(100) NOT NULL,
    idEstado INT,
    FOREIGN KEY (idEstado) REFERENCES Estado(id)
);

-- Crear tabla Ubicacion
CREATE TABLE Ubicacion (
    id INT PRIMARY KEY IDENTITY,
    nombre NVARCHAR(100) NOT NULL,
    descripcion NVARCHAR(255) NOT NULL,
    idEstado INT,
    FOREIGN KEY (idEstado) REFERENCES Estado(id)
);

-- Crear tabla UnidaddeMedida
--CREATE TABLE UnidadMedida (
--    id INT PRIMARY KEY IDENTITY,
--    nombre NVARCHAR(100) NOT NULL,
--    idEstado INT,
--    FOREIGN KEY (idEstado) REFERENCES Estado(id)
--);

-- Crear tabla Articulo
CREATE TABLE Articulo (
    id INT PRIMARY KEY IDENTITY,
    nombre NVARCHAR(100) NOT NULL,
	contenidoPorEmpaque  NVARCHAR(100) NOT NULL,
    StockMaxima INT NOT NULL,
    Stock INT NOT NULL,
    StockMinima INT NOT NULL,
    presentacion NVARCHAR(50) NOT NULL,
    disponibilidad Bit NOT NULL,
    idMarca INT,
    idCategoria INT,
    UnidaddeMedida decimal(18,0) NOT NULL,
    idUbicacion INT,
    idUsuario INT,
    idEstado INT,
    idTipo INT,
    FOREIGN KEY (idMarca) REFERENCES Marca(id),
    FOREIGN KEY (idCategoria) REFERENCES Categoria(id),
    FOREIGN KEY (idUbicacion) REFERENCES Ubicacion(id),
    FOREIGN KEY (idUsuario) REFERENCES Usuario(id),
    FOREIGN KEY (idEstado) REFERENCES Estado(id),
    FOREIGN KEY (idTipo) REFERENCES Tipo(id)
);

-- Crear tabla AsignacionCodigo
CREATE TABLE AsignacionCodigo(
Id INT PRIMARY KEY IDENTITY,
Codigo VARCHAR(20) UNIQUE,
IdArticulo INT NOT NULL,
idEstado INT NOT NULL,
FOREIGN KEY (IdArticulo) REFERENCES Articulo(id),
FOREIGN KEY (idEstado) REFERENCES Estado(id)
);

-- Crear tabla Prestamo
CREATE TABLE Prestamo (
    id INT PRIMARY KEY IDENTITY,
    idUsuario INT,
    idArticulo INT,
    fechaPrestamo DATE NOT NULL,
    fechaDevolucion DATE NOT NULL,
    idEstado INT,
    FOREIGN KEY (idUsuario) REFERENCES Usuario(id),
    FOREIGN KEY (idArticulo) REFERENCES Articulo(id),
    FOREIGN KEY (idEstado) REFERENCES Estado(id)
);

-- Crear tabla Proveedor
CREATE TABLE Proveedor (
    id INT PRIMARY KEY IDENTITY,
    nombre NVARCHAR(100) NOT NULL,
    apellido NVARCHAR(100) NOT NULL,
    tipoDePersona NVARCHAR(50) NOT NULL,
    dui NVARCHAR(20) NOT NULL,
    nombreEmpresa NVARCHAR(100),
    nrc INT NOT NULL,
    contacto NVARCHAR(100) NOT NULL,
    telefono NVARCHAR(20) NOT NULL,
    direccion NVARCHAR(255) NOT NULL,
    idEstado INT,
    FOREIGN KEY (idEstado) REFERENCES Estado(id)
);

-- Crear tabla Compra
CREATE TABLE Compra (
    id INT PRIMARY KEY IDENTITY,
    numerodeFactura NVARCHAR(100) NOT NULL,
    fecha DATE NOT NULL,
    precioUnitario DECIMAL(10, 2) NOT NULL,
    subTotal DECIMAL(10, 2) NOT NULL,
    iva DECIMAL(10, 2) NOT NULL,
    total DECIMAL(10, 2) NOT NULL,
    idProveedor INT,
    idEstado INT,
    FOREIGN KEY (idProveedor) REFERENCES Proveedor(id),
    FOREIGN KEY (idEstado) REFERENCES Estado(id)
);

-- Crear tabla DetalleCompra
CREATE TABLE DetalleCompra (
    id INT PRIMARY KEY IDENTITY,
    cantidad INT NOT NULL,
    totalProducto DECIMAL(10, 2) NOT NULL,
    precioUnitario DECIMAL(10, 2) NOT NULL,
    descuento DECIMAL(10, 2) NOT NULL,
    idCompra INT,
    idArticulo INT,
    idEstado INT,
    FOREIGN KEY (idCompra) REFERENCES Compra(id),
    FOREIGN KEY (idArticulo) REFERENCES Articulo(id),
    FOREIGN KEY (idEstado) REFERENCES Estado(id)
);

-- Crear tabla Perdidas
CREATE TABLE Perdidas (
    id INT PRIMARY KEY IDENTITY,
    fechaPerdida DATE NOT NULL,
    causaPerdida NVARCHAR(255) NOT NULL,
    valorPerdida DECIMAL(10, 2) NOT NULL,
    idArticulo INT,
    idUsuario INT,
    idCompra INT,
    FOREIGN KEY (idArticulo) REFERENCES Articulo(id),
    FOREIGN KEY (idUsuario) REFERENCES Usuario(id),
    FOREIGN KEY (idCompra) REFERENCES Compra(id),
);
