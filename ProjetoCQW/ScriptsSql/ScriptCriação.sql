Create Database [Projeto CQP]
USE [Projeto CQP]

CREATE TABLE Montadora (
    id INT PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(255) NOT NULL,
	UrlSite VARCHAR(255) NOT NULL,
	DataCriacao DATETIME NOT NULL, 
	DataAtualizacao DATETIME NOT NULL
);

CREATE TABLE ModeloSiteDetalhe (
    id INT PRIMARY KEY IDENTITY(1,1),
	Montadora_Id INT NOT NULL,
    UrlSite VARCHAR(255),
    XpathModelo VARCHAR(255) NOT NULL,
    XpathAno VARCHAR(255) NOT NULL,
    XpathCor VARCHAR(255),
    XpathImg VARCHAR(255),    
    XpathValor VARCHAR(255),
    DataCriacao DATETIME,
    DataAtualizacao DATETIME,
	FOREIGN KEY (Montadora_Id) REFERENCES Montadora(id)
);

CREATE TABLE ModeloCarro (
    id INT PRIMARY KEY IDENTITY(1,1),
	ModeloSite_Id INT NOT NULL,
    Nome VARCHAR(255) NOT NULL,
    Ano INT NOT NULL,
    Montadora_Id INT NOT NULL,
    Imagem VARCHAR(255),
    Cor VARCHAR(255),
    Versao VARCHAR(255),
	Valor FLOAT,
    DataCriacao DATETIME,
    DataAtualizacao DATETIME,
	FOREIGN KEY (ModeloSite_Id) REFERENCES ModeloSiteDetalhe(id),
	FOREIGN KEY (Montadora_Id) REFERENCES Montadora(id)
);


