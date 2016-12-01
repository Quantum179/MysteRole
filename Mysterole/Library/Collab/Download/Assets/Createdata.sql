CREATE DATABASE DB_MYSTEROLE
USE DB_MYSTEROLE;

DROP TABLE IF EXISTS EffetsCompetences;
DROP TABLE IF EXISTS Competences_Roles;
DROP TABLE IF EXISTS Competences;
DROP TABLE IF EXISTS Stats_Roles;
DROP TABLE IF EXISTS Equipements;
DROP TABLE IF EXISTS Personnages;
DROP TABLE IF EXISTS Roles;
DROP TABLE IF EXISTS Zones;
DROP TABLE IF EXISTS Portees;
DROP TABLE IF EXISTS Inventaires;
DROP TABLE IF EXISTS Equipes;
DROP TABLE IF EXISTS Cartes;
DROP TABLE IF EXISTS Attributs;
DROP TABLE IF EXISTS Objets;
DROP TABLE IF EXISTS TypesObjets;
DROP TABLE IF EXISTS EffetsEtatsPeriodiques;
DROP TABLE IF EXISTS EffetsEtats;
DROP TABLE IF EXISTS Stats;
DROP TABLE IF EXISTS Etats;

CREATE TABLE Roles (

	idRole INT NOT NULL ,
    nom VARCHAR(20)  NOT NULL,
    abbr VARCHAR(3) NOT  NULL,
    
    PRIMARY KEY (idRole)
);

CREATE TABLE Zones  (

	idZone INT NOT NULL,
    nom VARCHAR(20) NOT NULL,
    
	PRIMARY KEY (idZone)
);

CREATE TABLE Portees (

	idPortee INT NOT NULL ,
    nom VARCHAR(20) NOT NULL,
    
    PRIMARY KEY (idPortee)
);

CREATE TABLE Competences (

	idCompetence INT NOT NULL ,
    idZone INT NOT NULL,
    idPortee INT NOT NULL,
	nom VARCHAR(20) NOT NULL,
    zoneLargeurRayon INT NOT NULL,
    zoneLongueur INT NOT NULL,
    diagonale INT NOT NULL,
    longueur INT NOT NULL,
    distance INT NOT NULL,
    CaseCiblee BOOL NOT NULL,
    
    PRIMARY KEY (idCompetence),
    FOREIGN KEY (idZone) REFERENCES Zones (idZone),
    FOREIGN KEY (idPortee) REFERENCES Portees (idPortee)
);

CREATE TABLE Competences_Roles (
	
    idCompetence INT NOT NULL,
    idRole INT NOT NULL,
    
    FOREIGN KEY (idCompetence) REFERENCES Competences (idCompetence),
    FOREIGN KEY (idRole) REFERENCES Roles (idRole),
    PRIMARY KEY (idCompetence,idrole)
);

CREATE TABLE Cartes (

	idCarte INT NOT NULL ,
    nom VARCHAR(20) NOT NULL,
    
    PRIMARY KEY (idCarte)
);

CREATE TABLE Equipes (

	idEquipe INT NOT NULL ,
    idCarte INT NOT NULL,
    coordonnees_x INT NOT NULL,
    coordonnees_y INT NOT NULL,
    
    PRIMARY KEY (idEquipe),
    FOREIGN KEY (idCarte) REFERENCES Cartes (idCarte)
);


CREATE TABLE Personnages (

	idPersonnage INT NOT NULL ,
    idRole INT NOT NULL,
    idEquipe INT NOT NULL,
    nom VARCHAR(20) NOT NULL,
    niveau INT NOT NULL,
    
	PRIMARY KEY (idPersonnage),
    FOREIGN KEY (idRole) REFERENCES Roles (idRole),    
    FOREIGN KEY (idEquipe) REFERENCES Equipes (idEquipe)
);

CREATE TABLE TypesObjets (

	idTypeObjet INT NOT NULL ,
    typeObjet VARCHAR(20) NOT NULL,
    
    PRIMARY KEY (idTypeObjet)
);

CREATE TABLE Objets (

	idObjet INT NOT NULL ,
    idTypeObjet INT NOT NULL,
    nom VARCHAR(30) NOT NULL,
    
    PRIMARY KEY (idObjet),
    FOREIGN KEY (idTypeObjet) REFERENCES TypesObjets (idTypeObjet)
);

CREATE TABLE Equipements (

	idPersonnage INT NOT NULL,
    idObjet INT NOT NULL,
    
    FOREIGN KEY (idPersonnage) REFERENCES Personnages (idPersonnage),
    FOREIGN KEY (idObjet) REFERENCES Objets (idObjet),
    PRIMARY KEY (idPersonnage,idObjet)
);

CREATE TABLE inventaires (

	idEquipe INT NOT NULL,
    idObjet INT NOT NULL,
    quantitee INT NOT NULL,
    
    FOREIGN KEY (idEquipe) REFERENCES Equipes (idEquipe),
    FOREIGN KEY (idObjet) REFERENCES Objets (idObjet),
    PRIMARY KEY (idEquipe, idObjet)
);

CREATE TABLE Stats (

	idStat INT ,
    nom VARCHAR(20) NOT NULL,
    abbr VARCHAR(3) NOT NULL,
 	
    PRIMARY KEY (idStat)
);


CREATE TABLE Attributs (

	idObjet INT NOT NULL,
    idStat INT NOT NULL,
    
    FOREIGN KEY (idObjet) REFERENCES Objets (idObjet),
    FOREIGN KEY (idStat) REFERENCES Stats (idStat),
    PRIMARY KEY (idObjet, idStat)
);


CREATE TABLE  Stats_Roles (

	idRole INT NOT NULL,
    idStat INT NOT NULL,
    montant INT NOT NULL,
    
    FOREIGN KEY (idRole) REFERENCES Roles (idRole),
    FOREIGN KEY (idStat) REFERENCES Stats (idStat),
    PRIMARY KEY (idRole, idStat)
);

CREATE TABLE EffetsCompetences (

	idCompetence INT NOT NULL,
    idStat INT NOT NULL,
    montant INT NOT NULL,
    
    FOREIGN KEY (idCompetence) REFERENCES Competences (idCompetence),
    FOREIGN KEY (idStat) REFERENCES Stats (idStat),
    PRIMARY KEY (idCompetence,idStat)
);

CREATE TABLE Etats (

	idEtat	INT NOT NULL ,
    nom VARCHAR(20) NOT NULL,
    duree INT NOT NULL,
    
	PRIMARY KEY (idEtat)
);

CREATE TABLE EffetsEtats (

	idEtat INT NOT NULL,
    idStat INT NOT NULL,
    montant INT NOT NULL,
    
    FOREIGN KEY (idEtat) REFERENCES Etats (idEtat),
    FOREIGN KEY (idStat) REFERENCES Stats (idStat),
    PRIMARY KEY (idEtat,iStat)
);

CREATE TABLE EffetsEtatsPeriodiques (

	idEtat INT NOT NULL,
    idStat INT NOT NULL,
    montant INT NOT NULL,
    
    FOREIGN KEY (idEtat) REFERENCES Etats (idEtat),
    FOREIGN KEY (idStat) REFERENCES Stats (idStat),
    PRIMARY KEY (idEtat,idStat)
);



