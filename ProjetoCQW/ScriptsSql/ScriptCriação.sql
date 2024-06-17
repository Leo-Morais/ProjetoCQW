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
    UrlSite VARCHAR(255) NOT NULL,
	XpathNome VARCHAR(255) NOT NULL,
    XpathModelo VARCHAR(255) NOT NULL,
    XpathCor VARCHAR(255) NOT NULL,
    XpathImg VARCHAR(255) NOT NULL,    
    XpathValor VARCHAR(255) NOT NULL,
    DataCriacao DATETIME,
    DataAtualizacao DATETIME,
);

CREATE TABLE ModeloCarro (
    id INT PRIMARY KEY IDENTITY(1,1),
	ModeloSite_Id INT NOT NULL,
    Nome VARCHAR(255) NOT NULL,
    Ano INT NOT NULL,
    Montadora_Id INT NOT NULL,
    Imagem VARCHAR(255) NOT NULL,
    Cor VARCHAR(255) NOT NULL,
    Versao VARCHAR(255) NOT NULL,
	Valor FLOAT NOT NULL,
    DataCriacao DATETIME,
    DataAtualizacao DATETIME,
	FOREIGN KEY (ModeloSite_Id) REFERENCES ModeloSiteDetalhe(id),
	FOREIGN KEY (Montadora_Id) REFERENCES Montadora(id)
);