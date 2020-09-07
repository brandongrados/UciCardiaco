CREATE TABLE ENFERMEDAD (
    idenfermedad INTEGER  PRIMARY KEY IDENTITY,
    nombre       VARCHAR (50)  NOT NULL UNIQUE,
    descripcion  VARCHAR (256) NULL,
    condicion    BIT   DEFAULT (1)
);

INSERT INTO ENFERMEDAD (nombre, descripcion) values ('Catarsis','')
select * from ENFERMEDAD;

CREATE TABLE SINTOMA (
    idsintoma INTEGER  PRIMARY KEY IDENTITY,
    idenfermedad INTEGER  NOT NULL,
    codigo VARCHAR(50) NULL,
    nombre VARCHAR(100)NOT NULL UNIQUE,
    valor decimal(11,2) NOT NULL,
    descripcion VARCHAR (256) NULL,
    condicion    BIT   DEFAULT (1),
    FOREIGN KEY (idenfermedad) REFERENCES ENFERMEDAD (idenfermedad)
);
select * from sintoma;

CREATE TABLE PERSONA(
   idpersona integer primary key identity,
   tipo_persona varchar(20) not null,
   nombre varchar(100) not null,
   tipo_documento varchar(20) null,
   num_documento varchar(20) null,
   direccion varchar(70) null,
   telefono varchar(20) null,
   email varchar(50) null

);

CREATE TABLE ROL(
   idrol integer primary key identity,
   nombre varchar(30) not null,
   descripcion varchar(100) null, 
   condicion bit default(1)
);

CREATE TABLE USUARIO(
   idusuario integer primary key identity,
   idrol integer not null,
   nombre varchar (100) not null,
   tipo_documento varchar (20) null,
   num_documento varchar (20) null,
   direccion varchar (70) null,
   telefono varchar (20) null,
   email varchar (50) not null,
   password_hash varbinary not null,
   password_salt varbinary not null,
   condicion bit default(1),
   FOREIGN KEY (idrol) REFERENCES ROL (idrol)
);

CREATE TABLE HISTORIA (
    idhistoria integer primary key identity,
    idenfermera integer not null,
    idusuario integer not null,
    tipo_historia varchar (20) not null,
    serie_historia varchar (7) null,
    num_historia varchar (10) not null,
    fecha_hora datetime not null,
    resultado decimal (11,2) not null,
    estado varchar (20) not null,
    FOREIGN KEY (idenfermera) REFERENCES PERSONA (idpersona),
    FOREIGN KEY (idusuario) REFERENCES USUARIO (idusuario)

);

CREATE TABLE DETALLE_HISTORIA(
   iddetalle_historia integer primary key identity,
   idhistoria integer not null,
   idsintoma integer not null,
   nombre VARCHAR(100)NOT NULL UNIQUE,
   valor decimal(11,2) NOT NULL,
   FOREIGN KEY (idhistoria) REFERENCES HISTORIA (idhistoria) ON DELETE CASCADE,
   FOREIGN KEY (idsintoma) REFERENCES SINTOMA (idsintoma)

);

CREATE TABLE DIAGNOSTICO(
    id_diagnostico integer primary key identity,
    idpaciente integer not null,
    idusuario integer not null,
    tipo_diagnostico varchar (20) not null,
    serie_diagnostico varchar (7) null,
    num_diagnostico varchar (10) not null,
    fecha_hora datetime not null,
    resultado decimal (11,2) not null,
    estado varchar (20) not null,
    FOREIGN KEY (idpaciente) REFERENCES PERSONA (idpersona),
    FOREIGN KEY (idusuario) REFERENCES USUARIO (idusuario)
);

CREATE TABLE DETALLE_DIAGNOSTICO(
   iddetalle_diagnostico integer primary key identity,
   id_diagnostico integer not null,
   idsintoma integer not null,
   nombre VARCHAR(100)NOT NULL UNIQUE,
   valor decimal(11,2) NOT NULL,
   FOREIGN KEY (id_diagnostico) REFERENCES DIAGNOSTICO (id_diagnostico) ON DELETE CASCADE,
   FOREIGN KEY (idsintoma) REFERENCES SINTOMA (idsintoma)

);

