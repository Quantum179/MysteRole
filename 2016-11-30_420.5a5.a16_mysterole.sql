-- phpMyAdmin SQL Dump
-- version 3.3.9
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Nov 30, 2016 at 10:10 PM
-- Server version: 5.5.8
-- PHP Version: 5.3.5

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `420.5a5.a16_mysterole`
--

-- --------------------------------------------------------

--
-- Table structure for table `cartes`
--

CREATE TABLE IF NOT EXISTS `cartes` (
  `idCarte` int(11) NOT NULL AUTO_INCREMENT,
  `idZoneCarte` int(11) NOT NULL,
  `nom` varchar(15) NOT NULL,
  `position_x` int(11) NOT NULL,
  `position_y` int(11) NOT NULL,
  `estHostile` tinyint(1) NOT NULL,
  PRIMARY KEY (`idCarte`),
  UNIQUE KEY `nom` (`nom`),
  KEY `idZoneCarte` (`idZoneCarte`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=17 ;

--
-- Dumping data for table `cartes`
--

INSERT INTO `cartes` (`idCarte`, `idZoneCarte`, `nom`, `position_x`, `position_y`, `estHostile`) VALUES
(1, 1, 'carte02', 0, -100, 0),
(2, 1, 'carte03', 0, -150, 1),
(3, 1, 'carte04', 0, -200, 1),
(4, 1, 'carte05', 0, -250, 1),
(5, 1, 'carte11', 50, -50, 1),
(6, 1, 'carte13', 50, -150, 1),
(7, 1, 'carte14', 50, -200, 0),
(8, 1, 'carte15', 50, -250, 1),
(9, 1, 'carte25', 100, -250, 0),
(10, 2, 'carte65', 300, -250, 0),
(15, 1, 'carte23', 100, -150, 0),
(16, 1, 'carte24', 100, -200, 0);

-- --------------------------------------------------------

--
-- Table structure for table `cinematiques`
--

CREATE TABLE IF NOT EXISTS `cinematiques` (
  `idCinematique` int(11) NOT NULL AUTO_INCREMENT,
  `idEtatCinematique` int(11) NOT NULL,
  `idObjectif` int(11) DEFAULT NULL,
  `idQuete` int(11) DEFAULT NULL,
  `nom` varchar(30) NOT NULL,
  PRIMARY KEY (`idCinematique`),
  KEY `idEtatCinematique` (`idEtatCinematique`),
  KEY `idObjectif` (`idObjectif`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=11 ;

--
-- Dumping data for table `cinematiques`
--

INSERT INTO `cinematiques` (`idCinematique`, `idEtatCinematique`, `idObjectif`, `idQuete`, `nom`) VALUES
(1, 2, NULL, 1, 'L''embuscade'),
(2, 2, NULL, NULL, 'L''entrée dans la cité'),
(3, 2, 2, NULL, 'Rencontre avec le Maire'),
(4, 2, 3, 2, 'La nouvelle de contre-attaque'),
(5, 2, 4, NULL, 'La réunion'),
(6, 2, 5, 3, 'La famille'),
(7, 2, 8, NULL, 'Début de l''entraînement'),
(8, 2, NULL, NULL, 'Milieu de l''entraînement'),
(9, 2, NULL, NULL, 'Fin de l''entraînement'),
(10, 2, 9, NULL, 'Fin de l''entraînement');

-- --------------------------------------------------------

--
-- Table structure for table `competences`
--

CREATE TABLE IF NOT EXISTS `competences` (
  `idCompetence` int(11) NOT NULL,
  `idZone` int(11) NOT NULL,
  `idPortee` int(11) NOT NULL,
  `nom` varchar(20) NOT NULL,
  `zoneLargeurRayon` int(11) NOT NULL,
  `zoneLongueur` int(11) NOT NULL,
  `diagonale` tinyint(1) NOT NULL DEFAULT '0',
  `longueur` tinyint(1) NOT NULL DEFAULT '1',
  `distanceMax` int(11) NOT NULL,
  `distanceMin` int(11) NOT NULL,
  `CaseCiblee` tinyint(1) NOT NULL,
  `basique` tinyint(1) NOT NULL DEFAULT '0',
  `negatif` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`idCompetence`),
  KEY `idZone` (`idZone`),
  KEY `idPortee` (`idPortee`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `competences`
--

INSERT INTO `competences` (`idCompetence`, `idZone`, `idPortee`, `nom`, `zoneLargeurRayon`, `zoneLongueur`, `diagonale`, `longueur`, `distanceMax`, `distanceMin`, `CaseCiblee`, `basique`, `negatif`) VALUES
(0, 0, 0, 'Attaque', 0, 0, 0, 1, 1, 1, 0, 1, 1),
(1, 2, 0, 'Trancher', 3, 3, 0, 1, 1, 1, 1, 0, 1),
(2, 1, 0, 'Boule de Feu', 1, 1, 1, 1, 7, 2, 1, 0, 1),
(3, 0, 0, 'Tir', 1, 1, 1, 1, 9, 1, 0, 1, 1),
(4, 1, 0, 'Pluie de flèches', 2, 1, 1, 1, 8, 2, 1, 0, 1),
(5, 0, 0, 'Potion - PV', 0, 0, 1, 1, 2, 0, 0, 0, 0),
(6, 0, 0, 'Faiblesse', 0, 0, 1, 1, 3, 1, 0, 0, 1),
(7, 1, 1, 'Guérison', 1, 0, 1, 1, 5, 0, 0, 0, 0),
(8, 0, 0, 'Potion - PC', 0, 0, 1, 1, 2, 0, 0, 0, 0);

-- --------------------------------------------------------

--
-- Table structure for table `competences_etats`
--

CREATE TABLE IF NOT EXISTS `competences_etats` (
  `idCompetence` int(11) NOT NULL,
  `idEtat` int(11) NOT NULL,
  PRIMARY KEY (`idCompetence`,`idEtat`),
  KEY `idEtat` (`idEtat`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `competences_etats`
--

INSERT INTO `competences_etats` (`idCompetence`, `idEtat`) VALUES
(6, 0);

-- --------------------------------------------------------

--
-- Table structure for table `competences_roles`
--

CREATE TABLE IF NOT EXISTS `competences_roles` (
  `idCompetence` int(11) NOT NULL,
  `idRole` int(11) NOT NULL,
  PRIMARY KEY (`idCompetence`,`idRole`),
  KEY `idRole` (`idRole`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `competences_roles`
--

INSERT INTO `competences_roles` (`idCompetence`, `idRole`) VALUES
(0, 0),
(1, 0),
(0, 1),
(1, 1),
(0, 2),
(1, 2),
(0, 3),
(2, 3),
(3, 4),
(4, 4),
(3, 5),
(7, 5);

-- --------------------------------------------------------

--
-- Table structure for table `consommables`
--

CREATE TABLE IF NOT EXISTS `consommables` (
  `idConsomable` int(11) NOT NULL,
  `idCompetence` int(11) NOT NULL,
  `puissance` int(11) NOT NULL,
  PRIMARY KEY (`idConsomable`),
  KEY `idCompetence` (`idCompetence`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `consommables`
--

INSERT INTO `consommables` (`idConsomable`, `idCompetence`, `puissance`) VALUES
(0, 5, 20),
(1, 8, 5);

-- --------------------------------------------------------

--
-- Table structure for table `effetscompetences`
--

CREATE TABLE IF NOT EXISTS `effetscompetences` (
  `idCompetence` int(11) NOT NULL,
  `idStat` int(11) NOT NULL,
  `idTypeEffet` int(11) NOT NULL,
  `valeur` int(11) NOT NULL,
  PRIMARY KEY (`idCompetence`,`idStat`),
  KEY `idStat` (`idStat`),
  KEY `idTypeEffet` (`idTypeEffet`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `effetscompetences`
--

INSERT INTO `effetscompetences` (`idCompetence`, `idStat`, `idTypeEffet`, `valeur`) VALUES
(0, 3, 1, 0),
(1, 3, 1, 0),
(2, 3, 1, 0),
(3, 3, 1, 0),
(4, 3, 1, 0),
(5, 3, 2, 0),
(7, 3, 2, 0),
(8, 5, 2, 0);

-- --------------------------------------------------------

--
-- Table structure for table `effetsetats`
--

CREATE TABLE IF NOT EXISTS `effetsetats` (
  `idEtat` int(11) NOT NULL,
  `idStat` int(11) NOT NULL,
  `montant` int(11) NOT NULL,
  PRIMARY KEY (`idEtat`,`idStat`),
  KEY `idStat` (`idStat`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `effetsetats`
--

INSERT INTO `effetsetats` (`idEtat`, `idStat`, `montant`) VALUES
(0, 0, 5),
(0, 1, 5);

-- --------------------------------------------------------

--
-- Table structure for table `effetsetatsperiodiques`
--

CREATE TABLE IF NOT EXISTS `effetsetatsperiodiques` (
  `idEtat` int(11) NOT NULL,
  `idStat` int(11) NOT NULL,
  `montant` int(11) NOT NULL,
  PRIMARY KEY (`idEtat`,`idStat`),
  KEY `idStat` (`idStat`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `effetsetatsperiodiques`
--


-- --------------------------------------------------------

--
-- Table structure for table `equipements_equipe`
--

CREATE TABLE IF NOT EXISTS `equipements_equipe` (
  `idPersonnage` int(11) NOT NULL,
  `idObjet` int(11) NOT NULL,
  PRIMARY KEY (`idPersonnage`,`idObjet`),
  KEY `idObjet` (`idObjet`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `equipements_equipe`
--


-- --------------------------------------------------------

--
-- Table structure for table `equipes`
--

CREATE TABLE IF NOT EXISTS `equipes` (
  `idEquipe` int(11) NOT NULL,
  `idCarte` int(11) NOT NULL,
  `coordonnees_x` int(11) NOT NULL,
  `coordonnees_y` int(11) NOT NULL,
  PRIMARY KEY (`idEquipe`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `equipes`
--


-- --------------------------------------------------------

--
-- Table structure for table `equipesmonstre`
--

CREATE TABLE IF NOT EXISTS `equipesmonstre` (
  `idEquipeMonstre` int(11) NOT NULL,
  `nom` varchar(20) NOT NULL DEFAULT '',
  PRIMARY KEY (`idEquipeMonstre`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `equipesmonstre`
--

INSERT INTO `equipesmonstre` (`idEquipeMonstre`, `nom`) VALUES
(0, 'Orc+Squ'),
(1, 'quatreMonstres'),
(2, 'quatres squelettes');

-- --------------------------------------------------------

--
-- Table structure for table `equipes_monstres`
--

CREATE TABLE IF NOT EXISTS `equipes_monstres` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `idEquipe` int(11) NOT NULL,
  `idMonstre` int(11) NOT NULL,
  `niveau` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `idEquipe` (`idEquipe`),
  KEY `idMonstre` (`idMonstre`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=11 ;

--
-- Dumping data for table `equipes_monstres`
--

INSERT INTO `equipes_monstres` (`id`, `idEquipe`, `idMonstre`, `niveau`) VALUES
(1, 0, 1, 1),
(2, 0, 2, 1),
(3, 1, 1, 3),
(4, 1, 2, 3),
(5, 1, 2, 3),
(6, 1, 2, 3),
(7, 2, 2, 4),
(8, 2, 2, 4),
(9, 2, 2, 4),
(10, 2, 2, 4);

-- --------------------------------------------------------

--
-- Table structure for table `equipments_stats`
--

CREATE TABLE IF NOT EXISTS `equipments_stats` (
  `idEquipement` int(11) NOT NULL,
  `idStat` int(11) NOT NULL,
  `valeur` int(11) NOT NULL,
  PRIMARY KEY (`idEquipement`,`idStat`),
  KEY `idStat` (`idStat`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `equipments_stats`
--


-- --------------------------------------------------------

--
-- Table structure for table `etapescinematiques`
--

CREATE TABLE IF NOT EXISTS `etapescinematiques` (
  `idEtapeCinematique` int(11) NOT NULL AUTO_INCREMENT,
  `idCinematique` int(11) NOT NULL,
  `idEtatEtape` int(11) NOT NULL,
  `idCarte` int(11) NOT NULL,
  `position` int(11) NOT NULL,
  `positionJoueur_x` float DEFAULT NULL,
  `positionJoueur_y` float DEFAULT NULL,
  PRIMARY KEY (`idEtapeCinematique`),
  KEY `idCinematique` (`idCinematique`),
  KEY `idEtatEtape` (`idEtatEtape`),
  KEY `idCarte` (`idCarte`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Dumping data for table `etapescinematiques`
--

INSERT INTO `etapescinematiques` (`idEtapeCinematique`, `idCinematique`, `idEtatEtape`, `idCarte`, `position`, `positionJoueur_x`, `positionJoueur_y`) VALUES
(1, 1, 1, 5, 0, 10, -10);

-- --------------------------------------------------------

--
-- Table structure for table `etats`
--

CREATE TABLE IF NOT EXISTS `etats` (
  `idEtat` int(11) NOT NULL,
  `nom` varchar(20) NOT NULL,
  `duree` int(11) NOT NULL,
  PRIMARY KEY (`idEtat`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `etats`
--

INSERT INTO `etats` (`idEtat`, `nom`, `duree`) VALUES
(0, 'Faiblesse', 10);

-- --------------------------------------------------------

--
-- Table structure for table `etatscinematiques`
--

CREATE TABLE IF NOT EXISTS `etatscinematiques` (
  `idEtatCinematique` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(15) NOT NULL,
  PRIMARY KEY (`idEtatCinematique`),
  UNIQUE KEY `nom` (`nom`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Dumping data for table `etatscinematiques`
--

INSERT INTO `etatscinematiques` (`idEtatCinematique`, `nom`) VALUES
(1, 'Bloquée'),
(2, 'Disponible'),
(3, 'En cours'),
(4, 'Terminée');

-- --------------------------------------------------------

--
-- Table structure for table `etatsetapes`
--

CREATE TABLE IF NOT EXISTS `etatsetapes` (
  `idEtatEtape` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(15) NOT NULL,
  PRIMARY KEY (`idEtatEtape`),
  UNIQUE KEY `nom` (`nom`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Dumping data for table `etatsetapes`
--

INSERT INTO `etatsetapes` (`idEtatEtape`, `nom`) VALUES
(1, 'En attente'),
(2, 'En cours'),
(3, 'Finie');

-- --------------------------------------------------------

--
-- Table structure for table `etatsevenements`
--

CREATE TABLE IF NOT EXISTS `etatsevenements` (
  `idEtatEvenement` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(15) NOT NULL,
  PRIMARY KEY (`idEtatEvenement`),
  UNIQUE KEY `nom` (`nom`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Dumping data for table `etatsevenements`
--

INSERT INTO `etatsevenements` (`idEtatEvenement`, `nom`) VALUES
(1, 'En attente'),
(2, 'En cours'),
(3, 'Fini');

-- --------------------------------------------------------

--
-- Table structure for table `etatsquetes`
--

CREATE TABLE IF NOT EXISTS `etatsquetes` (
  `idEtatQuete` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(15) NOT NULL,
  PRIMARY KEY (`idEtatQuete`),
  UNIQUE KEY `nom` (`nom`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Dumping data for table `etatsquetes`
--

INSERT INTO `etatsquetes` (`idEtatQuete`, `nom`) VALUES
(1, 'Bloquée'),
(2, 'Disponible'),
(3, 'En cours'),
(4, 'Terminée');

-- --------------------------------------------------------

--
-- Table structure for table `evenementsactifs`
--

CREATE TABLE IF NOT EXISTS `evenementsactifs` (
  `idEvenementActif` int(11) NOT NULL AUTO_INCREMENT,
  `idTypeEvenement` int(11) NOT NULL,
  `idEtatEvenement` int(11) NOT NULL,
  `idPnj` int(11) NOT NULL,
  `position` int(11) DEFAULT NULL,
  `contenu` varchar(200) NOT NULL,
  PRIMARY KEY (`idEvenementActif`),
  KEY `idTypeEvenement` (`idTypeEvenement`),
  KEY `idEtatEvenement` (`idEtatEvenement`),
  KEY `idPnj` (`idPnj`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Dumping data for table `evenementsactifs`
--

INSERT INTO `evenementsactifs` (`idEvenementActif`, `idTypeEvenement`, `idEtatEvenement`, `idPnj`, `position`, `contenu`) VALUES
(1, 1, 1, 1, 0, 'Test des evenements defaut;-1;-1');

-- --------------------------------------------------------

--
-- Table structure for table `evenementscinematiques`
--

CREATE TABLE IF NOT EXISTS `evenementscinematiques` (
  `idEvenementCinematique` int(11) NOT NULL AUTO_INCREMENT,
  `idTypeEvenement` int(11) NOT NULL,
  `idEtatEvenement` int(11) NOT NULL,
  `idEtapeCinematique` int(11) NOT NULL,
  `idPnj` int(11) DEFAULT NULL,
  `peutContinuer` tinyint(1) NOT NULL,
  `position` int(11) DEFAULT NULL,
  `contenu` varchar(200) NOT NULL,
  PRIMARY KEY (`idEvenementCinematique`),
  KEY `idEtapeCinematique` (`idEtapeCinematique`),
  KEY `idTypeEvenement` (`idTypeEvenement`),
  KEY `idPnj` (`idPnj`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=14 ;

--
-- Dumping data for table `evenementscinematiques`
--

INSERT INTO `evenementscinematiques` (`idEvenementCinematique`, `idTypeEvenement`, `idEtatEvenement`, `idEtapeCinematique`, `idPnj`, `peutContinuer`, `position`, `contenu`) VALUES
(1, 2, 1, 1, 7, 1, 0, '85,-85;0;'),
(2, 1, 1, 1, 7, 0, 2, 'Ce fut une bonne partie de chasse tu ne trouves pas ?;3;'),
(4, 3, 1, 1, 7, 0, 3, 'haut;1;'),
(5, 3, 1, 1, 7, 0, 4, 'bas;1;'),
(6, 3, 1, 1, 7, 0, 5, 'droite;1;'),
(7, 2, 1, 1, NULL, 1, 1, '15,-30;-1;'),
(8, 3, 1, 1, NULL, 0, 6, 'droite;1;'),
(10, 2, 1, 1, 8, 1, 7, '60,-60;2;'),
(12, 1, 1, 1, 8, 0, 8, 'Hahaha ça fait longtemps que j''attends ce moment.;-1;');

-- --------------------------------------------------------

--
-- Table structure for table `evenementspassifs`
--

CREATE TABLE IF NOT EXISTS `evenementspassifs` (
  `idEvenementPassif` int(11) NOT NULL AUTO_INCREMENT,
  `idTypeEvenement` int(11) NOT NULL,
  `idEtatEvenement` int(11) NOT NULL,
  `idPnj` int(11) NOT NULL,
  `position` int(11) DEFAULT NULL,
  `contenu` varchar(200) NOT NULL,
  PRIMARY KEY (`idEvenementPassif`),
  KEY `idTypeEvenement` (`idTypeEvenement`),
  KEY `idEtatEvenement` (`idEtatEvenement`),
  KEY `idPnj` (`idPnj`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

--
-- Dumping data for table `evenementspassifs`
--

INSERT INTO `evenementspassifs` (`idEvenementPassif`, `idTypeEvenement`, `idEtatEvenement`, `idPnj`, `position`, `contenu`) VALUES
(1, 2, 1, 1, 0, '2,2;'),
(2, 3, 1, 1, 1, 'random;1;');

-- --------------------------------------------------------

--
-- Table structure for table `inventaires`
--

CREATE TABLE IF NOT EXISTS `inventaires` (
  `idEquipe` int(11) NOT NULL,
  `idObjet` int(11) NOT NULL,
  `quantitee` int(11) NOT NULL,
  PRIMARY KEY (`idEquipe`,`idObjet`),
  KEY `idObjet` (`idObjet`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `inventaires`
--


-- --------------------------------------------------------

--
-- Table structure for table `objectifs`
--

CREATE TABLE IF NOT EXISTS `objectifs` (
  `idObjectif` int(11) NOT NULL AUTO_INCREMENT,
  `idQuete` int(11) NOT NULL,
  `idTypeObjectif` int(11) NOT NULL,
  `responsable` int(11) DEFAULT NULL,
  `parametres` varchar(100) NOT NULL,
  `estValide` tinyint(1) NOT NULL,
  PRIMARY KEY (`idObjectif`),
  KEY `idQuete` (`idQuete`),
  KEY `idTypeObjectif` (`idTypeObjectif`),
  KEY `responsable` (`responsable`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=10 ;

--
-- Dumping data for table `objectifs`
--

INSERT INTO `objectifs` (`idObjectif`, `idQuete`, `idTypeObjectif`, `responsable`, `parametres`, `estValide`) VALUES
(1, 1, 4, NULL, 'carte35;Se rendre à la cité;', 0),
(2, 1, 1, 6, '1;Secrétaire du Maire;Prendre rendez-vous avec le Maire;', 0),
(3, 1, 4, NULL, 'carte02;Retourner au village;', 0),
(4, 2, 1, NULL, '4;null;Parler aux anciens du village;', 0),
(5, 2, 1, 3, '1;Général Marcus;Parler au Général Marcus.;', 0),
(6, 3, 1, 5, '1;Mère;Parler à Mère.;', 0),
(7, 3, 2, 5, 'Epée Damoclès;Obtenir l''épée de l''héritage.;', 0),
(8, 3, 1, 3, '1;Général Marcus; Retourner voir le Général Marcus.;', 0),
(9, 3, 1, 1, '1;Sage du village; Parler au Sage du village.;', 0);

-- --------------------------------------------------------

--
-- Table structure for table `objets`
--

CREATE TABLE IF NOT EXISTS `objets` (
  `idObjet` int(11) NOT NULL,
  `idTypeObjet` int(11) NOT NULL,
  `nom` varchar(30) NOT NULL,
  PRIMARY KEY (`idObjet`),
  KEY `idTypeObjet` (`idTypeObjet`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `objets`
--

INSERT INTO `objets` (`idObjet`, `idTypeObjet`, `nom`) VALUES
(0, 0, 'Potion Min'),
(1, 0, 'Ether');

-- --------------------------------------------------------

--
-- Table structure for table `personnages`
--

CREATE TABLE IF NOT EXISTS `personnages` (
  `idPersonnage` int(11) NOT NULL,
  `idRole` int(11) NOT NULL,
  `idEquipe` int(11) NOT NULL,
  `nom` varchar(20) NOT NULL,
  `niveau` int(11) NOT NULL,
  PRIMARY KEY (`idPersonnage`),
  KEY `idRole` (`idRole`),
  KEY `idEquipe` (`idEquipe`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `personnages`
--


-- --------------------------------------------------------

--
-- Table structure for table `pnjs`
--

CREATE TABLE IF NOT EXISTS `pnjs` (
  `idPnj` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(30) NOT NULL,
  PRIMARY KEY (`idPnj`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=9 ;

--
-- Dumping data for table `pnjs`
--

INSERT INTO `pnjs` (`idPnj`, `nom`) VALUES
(-1, 'camera'),
(1, 'Sage du village'),
(2, 'Chef milicien'),
(3, 'Général Marcus'),
(4, 'Maire'),
(5, 'Mère'),
(6, 'Secrétaire du Maire'),
(7, 'Père'),
(8, 'Chef Ogre');

-- --------------------------------------------------------

--
-- Table structure for table `portees`
--

CREATE TABLE IF NOT EXISTS `portees` (
  `idPortee` int(11) NOT NULL,
  `nom` varchar(20) NOT NULL,
  PRIMARY KEY (`idPortee`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `portees`
--

INSERT INTO `portees` (`idPortee`, `nom`) VALUES
(0, 'Standard'),
(1, 'Plein'),
(2, 'Cercle');

-- --------------------------------------------------------

--
-- Table structure for table `positionsacteurs`
--

CREATE TABLE IF NOT EXISTS `positionsacteurs` (
  `idPositionActeur` int(11) NOT NULL AUTO_INCREMENT,
  `idEtapeCinematique` int(11) NOT NULL,
  `idPnj` int(11) NOT NULL,
  `position_x` float NOT NULL,
  `position_y` float NOT NULL,
  PRIMARY KEY (`idPositionActeur`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Dumping data for table `positionsacteurs`
--

INSERT INTO `positionsacteurs` (`idPositionActeur`, `idEtapeCinematique`, `idPnj`, `position_x`, `position_y`) VALUES
(1, 1, 7, 55, -55),
(2, 1, -1, 55, -55),
(3, 1, 8, 110, -110);

-- --------------------------------------------------------

--
-- Table structure for table `positionspnjs`
--

CREATE TABLE IF NOT EXISTS `positionspnjs` (
  `idPositionPnj` int(11) NOT NULL AUTO_INCREMENT,
  `idPnj` int(11) NOT NULL,
  `idCarte` int(11) NOT NULL,
  `indexPosition` int(11) NOT NULL,
  `position_x` float NOT NULL,
  `position_y` float NOT NULL,
  `idQuete` int(11) DEFAULT NULL,
  `idEtatQuete` int(11) DEFAULT NULL,
  `idCinematique` int(11) DEFAULT NULL,
  PRIMARY KEY (`idPositionPnj`),
  KEY `idPnj` (`idPnj`),
  KEY `idCarte` (`idCarte`),
  KEY `idQuete` (`idQuete`),
  KEY `idEtatQuete` (`idEtatQuete`),
  KEY `idCinematique` (`idCinematique`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=10 ;

--
-- Dumping data for table `positionspnjs`
--

INSERT INTO `positionspnjs` (`idPositionPnj`, `idPnj`, `idCarte`, `indexPosition`, `position_x`, `position_y`, `idQuete`, `idEtatQuete`, `idCinematique`) VALUES
(1, 1, 1, 0, 35, -115, NULL, NULL, NULL),
(3, 1, 1, 2, 15, -115, NULL, NULL, 5),
(5, 3, 1, 1, 35, -135, 2, 4, NULL),
(6, 2, 9, 0, 25, -35, NULL, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `quetes`
--

CREATE TABLE IF NOT EXISTS `quetes` (
  `idQuete` int(11) NOT NULL AUTO_INCREMENT,
  `idEtatQuete` int(11) NOT NULL,
  `idQueteParente` int(11) NOT NULL,
  `nom` varchar(30) NOT NULL,
  `responsablePnj` int(11) DEFAULT NULL,
  `description` varchar(300) NOT NULL,
  PRIMARY KEY (`idQuete`),
  UNIQUE KEY `nom` (`nom`),
  KEY `idEtatQuete` (`idEtatQuete`),
  KEY `idQueteParente` (`idQueteParente`),
  KEY `responsablePnj` (`responsablePnj`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Dumping data for table `quetes`
--

INSERT INTO `quetes` (`idQuete`, `idEtatQuete`, `idQueteParente`, `nom`, `responsablePnj`, `description`) VALUES
(1, 2, 1, 'La visite au Maire', NULL, 'La tribu des orques trame quelque chose de louche, il faut prévenir le Maire de ce qui se passe.'),
(2, 1, 1, 'La réunion', 1, 'Les anciens du village organisent une réunion pour se préparer à la contre-attaque.'),
(3, 1, 2, 'L''entraînement', 5, 'Il est temps de se préparer à la contre-attaque. Le général a un entraînement spécial pour toi.');

-- --------------------------------------------------------

--
-- Table structure for table `quetesparentes`
--

CREATE TABLE IF NOT EXISTS `quetesparentes` (
  `idQueteParente` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(30) NOT NULL,
  PRIMARY KEY (`idQueteParente`),
  UNIQUE KEY `nom` (`nom`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Dumping data for table `quetesparentes`
--

INSERT INTO `quetesparentes` (`idQueteParente`, `nom`) VALUES
(1, 'L''embuscade fatidique'),
(2, 'La préparation'),
(3, 'Le conseil de guerre');

-- --------------------------------------------------------

--
-- Table structure for table `roles`
--

CREATE TABLE IF NOT EXISTS `roles` (
  `idRole` int(11) NOT NULL,
  `estJoueur` tinyint(1) NOT NULL DEFAULT '0',
  `nom` varchar(20) NOT NULL,
  `abbr` varchar(3) NOT NULL,
  PRIMARY KEY (`idRole`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `roles`
--

INSERT INTO `roles` (`idRole`, `estJoueur`, `nom`, `abbr`) VALUES
(0, 1, 'Guerrier', 'GUE'),
(1, 0, 'Orque', 'ORC'),
(2, 0, 'Squelette', 'SQU'),
(3, 1, 'Mage', 'MAG'),
(4, 1, 'Archer', 'ARC'),
(5, 1, 'Prêtre', 'PRE');

-- --------------------------------------------------------

--
-- Table structure for table `stats`
--

CREATE TABLE IF NOT EXISTS `stats` (
  `idStat` int(11) NOT NULL DEFAULT '0',
  `nom` varchar(20) NOT NULL,
  `abbr` varchar(3) NOT NULL,
  PRIMARY KEY (`idStat`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `stats`
--

INSERT INTO `stats` (`idStat`, `nom`, `abbr`) VALUES
(0, 'Puissance', 'PUI'),
(1, 'Defense', 'DEF'),
(2, 'Vitesse', 'VIT'),
(3, 'Points de vie', 'PV'),
(4, 'Maximum PV', 'MPV'),
(5, 'Points de Compétence', 'PC'),
(6, 'Maximum PC', 'MPC'),
(7, 'Mouvement', 'MOV'),
(8, 'Aucune', 'NON');

-- --------------------------------------------------------

--
-- Table structure for table `stats_roles`
--

CREATE TABLE IF NOT EXISTS `stats_roles` (
  `idRole` int(11) NOT NULL,
  `idStat` int(11) NOT NULL,
  `montant` int(11) NOT NULL,
  PRIMARY KEY (`idRole`,`idStat`),
  KEY `idStat` (`idStat`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `stats_roles`
--

INSERT INTO `stats_roles` (`idRole`, `idStat`, `montant`) VALUES
(0, 0, 10),
(0, 1, 7),
(0, 2, 5),
(1, 0, 5),
(1, 1, 5),
(1, 2, 5),
(2, 0, 5),
(2, 1, 5),
(2, 2, 5),
(3, 0, 10),
(3, 1, 5),
(3, 2, 7),
(4, 0, 7),
(4, 1, 5),
(4, 2, 10),
(5, 0, 5),
(5, 1, 10),
(5, 2, 7);

-- --------------------------------------------------------

--
-- Table structure for table `typeseffets`
--

CREATE TABLE IF NOT EXISTS `typeseffets` (
  `idTypeEffet` int(11) NOT NULL,
  `nomTypeEffet` varchar(20) NOT NULL,
  PRIMARY KEY (`idTypeEffet`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `typeseffets`
--

INSERT INTO `typeseffets` (`idTypeEffet`, `nomTypeEffet`) VALUES
(0, 'EffetStat'),
(1, 'Dégât'),
(2, 'Soin'),
(3, 'EffetScénarisé');

-- --------------------------------------------------------

--
-- Table structure for table `typesevenements`
--

CREATE TABLE IF NOT EXISTS `typesevenements` (
  `idTypeEvenement` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(15) NOT NULL,
  PRIMARY KEY (`idTypeEvenement`),
  UNIQUE KEY `nom` (`nom`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Dumping data for table `typesevenements`
--

INSERT INTO `typesevenements` (`idTypeEvenement`, `nom`) VALUES
(3, 'Attente'),
(2, 'Déplacement'),
(1, 'Dialogue');

-- --------------------------------------------------------

--
-- Table structure for table `typesobjectifs`
--

CREATE TABLE IF NOT EXISTS `typesobjectifs` (
  `idTypeObjectif` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(30) NOT NULL,
  PRIMARY KEY (`idTypeObjectif`),
  UNIQUE KEY `nom` (`nom`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Dumping data for table `typesobjectifs`
--

INSERT INTO `typesobjectifs` (`idTypeObjectif`, `nom`) VALUES
(3, 'Battre'),
(4, 'Explorer'),
(1, 'Parler'),
(2, 'Trouver');

-- --------------------------------------------------------

--
-- Table structure for table `typesobjets`
--

CREATE TABLE IF NOT EXISTS `typesobjets` (
  `idTypeObjet` int(11) NOT NULL,
  `typeObjet` varchar(20) NOT NULL,
  PRIMARY KEY (`idTypeObjet`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `typesobjets`
--

INSERT INTO `typesobjets` (`idTypeObjet`, `typeObjet`) VALUES
(0, 'Consommable'),
(1, 'Casque'),
(2, 'Armure'),
(3, 'Jambes'),
(4, 'Arme'),
(5, 'Anneau');

-- --------------------------------------------------------

--
-- Table structure for table `zones`
--

CREATE TABLE IF NOT EXISTS `zones` (
  `idZone` int(11) NOT NULL,
  `nom` varchar(20) NOT NULL,
  PRIMARY KEY (`idZone`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `zones`
--

INSERT INTO `zones` (`idZone`, `nom`) VALUES
(0, 'Point'),
(1, 'Losange'),
(2, 'Ligne');

-- --------------------------------------------------------

--
-- Table structure for table `zonescartes`
--

CREATE TABLE IF NOT EXISTS `zonescartes` (
  `idZoneCarte` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(20) NOT NULL,
  PRIMARY KEY (`idZoneCarte`),
  UNIQUE KEY `nom` (`nom`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Dumping data for table `zonescartes`
--

INSERT INTO `zonescartes` (`idZoneCarte`, `nom`) VALUES
(3, 'Caverne'),
(2, 'Cité'),
(1, 'Forêt');

--
-- Constraints for dumped tables
--

--
-- Constraints for table `cartes`
--
ALTER TABLE `cartes`
  ADD CONSTRAINT `cartes_ibfk_1` FOREIGN KEY (`idZoneCarte`) REFERENCES `zonescartes` (`idZoneCarte`);

--
-- Constraints for table `cinematiques`
--
ALTER TABLE `cinematiques`
  ADD CONSTRAINT `cinematiques_ibfk_1` FOREIGN KEY (`idEtatCinematique`) REFERENCES `etatscinematiques` (`idEtatCinematique`),
  ADD CONSTRAINT `cinematiques_ibfk_2` FOREIGN KEY (`idObjectif`) REFERENCES `objectifs` (`idObjectif`);

--
-- Constraints for table `competences`
--
ALTER TABLE `competences`
  ADD CONSTRAINT `competences_ibfk_1` FOREIGN KEY (`idZone`) REFERENCES `zones` (`idZone`),
  ADD CONSTRAINT `competences_ibfk_2` FOREIGN KEY (`idPortee`) REFERENCES `portees` (`idPortee`);

--
-- Constraints for table `competences_etats`
--
ALTER TABLE `competences_etats`
  ADD CONSTRAINT `competences_etats_ibfk_2` FOREIGN KEY (`idEtat`) REFERENCES `etats` (`idEtat`),
  ADD CONSTRAINT `competences_etats_ibfk_1` FOREIGN KEY (`idCompetence`) REFERENCES `competences` (`idCompetence`);

--
-- Constraints for table `competences_roles`
--
ALTER TABLE `competences_roles`
  ADD CONSTRAINT `competences_roles_ibfk_1` FOREIGN KEY (`idCompetence`) REFERENCES `competences` (`idCompetence`),
  ADD CONSTRAINT `competences_roles_ibfk_2` FOREIGN KEY (`idRole`) REFERENCES `roles` (`idRole`);

--
-- Constraints for table `consommables`
--
ALTER TABLE `consommables`
  ADD CONSTRAINT `consommables_ibfk_2` FOREIGN KEY (`idCompetence`) REFERENCES `competences` (`idCompetence`),
  ADD CONSTRAINT `consommables_ibfk_1` FOREIGN KEY (`idConsomable`) REFERENCES `objets` (`idObjet`);

--
-- Constraints for table `effetscompetences`
--
ALTER TABLE `effetscompetences`
  ADD CONSTRAINT `effetscompetences_ibfk_1` FOREIGN KEY (`idCompetence`) REFERENCES `competences` (`idCompetence`),
  ADD CONSTRAINT `effetscompetences_ibfk_2` FOREIGN KEY (`idStat`) REFERENCES `stats` (`idStat`),
  ADD CONSTRAINT `effetscompetences_ibfk_3` FOREIGN KEY (`idTypeEffet`) REFERENCES `typeseffets` (`idTypeEffet`);

--
-- Constraints for table `effetsetats`
--
ALTER TABLE `effetsetats`
  ADD CONSTRAINT `effetsetats_ibfk_1` FOREIGN KEY (`idEtat`) REFERENCES `etats` (`idEtat`),
  ADD CONSTRAINT `effetsetats_ibfk_2` FOREIGN KEY (`idStat`) REFERENCES `stats` (`idStat`);

--
-- Constraints for table `effetsetatsperiodiques`
--
ALTER TABLE `effetsetatsperiodiques`
  ADD CONSTRAINT `effetsetatsperiodiques_ibfk_1` FOREIGN KEY (`idEtat`) REFERENCES `etats` (`idEtat`),
  ADD CONSTRAINT `effetsetatsperiodiques_ibfk_2` FOREIGN KEY (`idStat`) REFERENCES `stats` (`idStat`);

--
-- Constraints for table `equipements_equipe`
--
ALTER TABLE `equipements_equipe`
  ADD CONSTRAINT `equipements_equipe_ibfk_1` FOREIGN KEY (`idPersonnage`) REFERENCES `personnages` (`idPersonnage`),
  ADD CONSTRAINT `equipements_equipe_ibfk_2` FOREIGN KEY (`idObjet`) REFERENCES `objets` (`idObjet`);

--
-- Constraints for table `equipes_monstres`
--
ALTER TABLE `equipes_monstres`
  ADD CONSTRAINT `equipes_monstres_ibfk_1` FOREIGN KEY (`idEquipe`) REFERENCES `equipesmonstre` (`idEquipeMonstre`),
  ADD CONSTRAINT `equipes_monstres_ibfk_2` FOREIGN KEY (`idMonstre`) REFERENCES `roles` (`idRole`);

--
-- Constraints for table `equipments_stats`
--
ALTER TABLE `equipments_stats`
  ADD CONSTRAINT `equipments_stats_ibfk_2` FOREIGN KEY (`idStat`) REFERENCES `stats` (`idStat`),
  ADD CONSTRAINT `equipments_stats_ibfk_1` FOREIGN KEY (`idEquipement`) REFERENCES `objets` (`idObjet`);

--
-- Constraints for table `etapescinematiques`
--
ALTER TABLE `etapescinematiques`
  ADD CONSTRAINT `etapescinematiques_ibfk_1` FOREIGN KEY (`idCinematique`) REFERENCES `cinematiques` (`idCinematique`),
  ADD CONSTRAINT `etapescinematiques_ibfk_2` FOREIGN KEY (`idEtatEtape`) REFERENCES `etatsetapes` (`idEtatEtape`),
  ADD CONSTRAINT `etapescinematiques_ibfk_3` FOREIGN KEY (`idCarte`) REFERENCES `cartes` (`idCarte`);

--
-- Constraints for table `evenementsactifs`
--
ALTER TABLE `evenementsactifs`
  ADD CONSTRAINT `evenementsactifs_ibfk_1` FOREIGN KEY (`idTypeEvenement`) REFERENCES `typesevenements` (`idTypeEvenement`),
  ADD CONSTRAINT `evenementsactifs_ibfk_2` FOREIGN KEY (`idEtatEvenement`) REFERENCES `etatsevenements` (`idEtatEvenement`),
  ADD CONSTRAINT `evenementsactifs_ibfk_3` FOREIGN KEY (`idPnj`) REFERENCES `pnjs` (`idPnj`);

--
-- Constraints for table `evenementscinematiques`
--
ALTER TABLE `evenementscinematiques`
  ADD CONSTRAINT `evenementscinematiques_ibfk_1` FOREIGN KEY (`idEtapeCinematique`) REFERENCES `etapescinematiques` (`idEtapeCinematique`),
  ADD CONSTRAINT `evenementscinematiques_ibfk_2` FOREIGN KEY (`idTypeEvenement`) REFERENCES `typesevenements` (`idTypeEvenement`),
  ADD CONSTRAINT `evenementscinematiques_ibfk_3` FOREIGN KEY (`idPnj`) REFERENCES `pnjs` (`idPnj`);

--
-- Constraints for table `evenementspassifs`
--
ALTER TABLE `evenementspassifs`
  ADD CONSTRAINT `evenementspassifs_ibfk_1` FOREIGN KEY (`idTypeEvenement`) REFERENCES `typesevenements` (`idTypeEvenement`),
  ADD CONSTRAINT `evenementspassifs_ibfk_2` FOREIGN KEY (`idEtatEvenement`) REFERENCES `etatsevenements` (`idEtatEvenement`),
  ADD CONSTRAINT `evenementspassifs_ibfk_3` FOREIGN KEY (`idPnj`) REFERENCES `pnjs` (`idPnj`);

--
-- Constraints for table `inventaires`
--
ALTER TABLE `inventaires`
  ADD CONSTRAINT `inventaires_ibfk_1` FOREIGN KEY (`idEquipe`) REFERENCES `equipes` (`idEquipe`),
  ADD CONSTRAINT `inventaires_ibfk_2` FOREIGN KEY (`idObjet`) REFERENCES `objets` (`idObjet`);

--
-- Constraints for table `objectifs`
--
ALTER TABLE `objectifs`
  ADD CONSTRAINT `objectifs_ibfk_1` FOREIGN KEY (`idQuete`) REFERENCES `quetes` (`idQuete`),
  ADD CONSTRAINT `objectifs_ibfk_2` FOREIGN KEY (`idTypeObjectif`) REFERENCES `typesobjectifs` (`idTypeObjectif`),
  ADD CONSTRAINT `objectifs_ibfk_3` FOREIGN KEY (`responsable`) REFERENCES `pnjs` (`idPnj`);

--
-- Constraints for table `objets`
--
ALTER TABLE `objets`
  ADD CONSTRAINT `objets_ibfk_1` FOREIGN KEY (`idTypeObjet`) REFERENCES `typesobjets` (`idTypeObjet`);

--
-- Constraints for table `personnages`
--
ALTER TABLE `personnages`
  ADD CONSTRAINT `personnages_ibfk_1` FOREIGN KEY (`idRole`) REFERENCES `roles` (`idRole`),
  ADD CONSTRAINT `personnages_ibfk_2` FOREIGN KEY (`idEquipe`) REFERENCES `equipes` (`idEquipe`);

--
-- Constraints for table `positionspnjs`
--
ALTER TABLE `positionspnjs`
  ADD CONSTRAINT `positionspnjs_ibfk_1` FOREIGN KEY (`idPnj`) REFERENCES `pnjs` (`idPnj`),
  ADD CONSTRAINT `positionspnjs_ibfk_2` FOREIGN KEY (`idCarte`) REFERENCES `cartes` (`idCarte`),
  ADD CONSTRAINT `positionspnjs_ibfk_3` FOREIGN KEY (`idQuete`) REFERENCES `quetes` (`idQuete`),
  ADD CONSTRAINT `positionspnjs_ibfk_4` FOREIGN KEY (`idEtatQuete`) REFERENCES `etatsquetes` (`idEtatQuete`),
  ADD CONSTRAINT `positionspnjs_ibfk_5` FOREIGN KEY (`idCinematique`) REFERENCES `cinematiques` (`idCinematique`);

--
-- Constraints for table `quetes`
--
ALTER TABLE `quetes`
  ADD CONSTRAINT `quetes_ibfk_1` FOREIGN KEY (`idEtatQuete`) REFERENCES `etatsquetes` (`idEtatQuete`),
  ADD CONSTRAINT `quetes_ibfk_2` FOREIGN KEY (`idQueteParente`) REFERENCES `quetesparentes` (`idQueteParente`),
  ADD CONSTRAINT `quetes_ibfk_3` FOREIGN KEY (`responsablePnj`) REFERENCES `pnjs` (`idPnj`);

--
-- Constraints for table `stats_roles`
--
ALTER TABLE `stats_roles`
  ADD CONSTRAINT `stats_roles_ibfk_1` FOREIGN KEY (`idRole`) REFERENCES `roles` (`idRole`),
  ADD CONSTRAINT `stats_roles_ibfk_2` FOREIGN KEY (`idStat`) REFERENCES `stats` (`idStat`);
