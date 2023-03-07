-- phpMyAdmin SQL Dump
-- version 6.0.0-dev+20230212.f19d22c671
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1
-- Généré le : mar. 07 mars 2023 à 20:18
-- Version du serveur : 10.4.24-MariaDB
-- Version de PHP : 8.1.5

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `para`
--

-- --------------------------------------------------------

--
-- Structure de la table `article`
--

CREATE TABLE `article` (
  `id_art` int(11) NOT NULL,
  `code` varchar(20) NOT NULL,
  `cate_id` int(11) NOT NULL,
  `libelle` varchar(250) NOT NULL,
  `marque` varchar(250) NOT NULL,
  `prixUn` double NOT NULL,
  `date_Ex` datetime NOT NULL,
  `quantite` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `article`
--

INSERT INTO `article` (`id_art`, `code`, `cate_id`, `libelle`, `marque`, `prixUn`, `date_Ex`, `quantite`) VALUES
(20, '123', 6, 'CADUM', 'CADUM', 20, '2023-01-29 16:58:19', 0),
(22, '125', 6, 'Doliprane', 'doliprane', 17, '2023-01-19 16:59:34', 4),
(23, '126', 6, 'FACE CREAM', 'NIVEA', 25, '2023-01-29 17:00:02', 17),
(24, 'TEST', 8, 'TES', 'TE', 12, '2023-01-29 17:19:35', 11),
(25, 'TEST', 8, 'HEAD', 'SHOULDERS', 10, '2023-01-29 17:39:01', 14),
(27, 'EEE11', 8, 'DERCOS', 'DERCOS', 20, '2023-02-26 14:50:08', 19),
(28, 'Eé\"', 8, 'az', 'der', 55, '2023-02-10 15:56:49', 23);

-- --------------------------------------------------------

--
-- Structure de la table `categorie`
--

CREATE TABLE `categorie` (
  `id_cat` int(11) NOT NULL,
  `code` varchar(20) NOT NULL,
  `libelle` varchar(250) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `categorie`
--

INSERT INTO `categorie` (`id_cat`, `code`, `libelle`) VALUES
(4, 'E123', 'Shampoo'),
(5, 'E124', 'Soin de corps'),
(6, 'E125', 'Medicament'),
(8, 'E126', 'TEST');

-- --------------------------------------------------------

--
-- Structure de la table `client`
--

CREATE TABLE `client` (
  `id_client` int(11) NOT NULL,
  `nom` varchar(250) NOT NULL,
  `prenom` varchar(250) NOT NULL,
  `mobile` varchar(250) NOT NULL,
  `cin` varchar(250) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `client`
--

INSERT INTO `client` (`id_client`, `nom`, `prenom`, `mobile`, `cin`) VALUES
(1, 'naciri', 'zineb', '067777777', 'EE56564');

-- --------------------------------------------------------

--
-- Structure de la table `commande`
--

CREATE TABLE `commande` (
  `id_c` int(11) NOT NULL,
  `id_fo` int(11) NOT NULL,
  `id_arti` int(11) NOT NULL,
  `quantite` int(11) NOT NULL,
  `date_c` datetime NOT NULL,
  `code` varchar(250) NOT NULL,
  `statut` varchar(250) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `commande`
--

INSERT INTO `commande` (`id_c`, `id_fo`, `id_arti`, `quantite`, `date_c`, `code`, `statut`) VALUES
(40, 6, 23, 1, '2023-01-18 17:01:49', '1233', 'Livré'),
(41, 6, 23, 1, '2023-01-18 17:04:51', '1234', 'Livré'),
(42, 6, 23, 7, '2023-01-18 17:07:09', 'test', 'Livré'),
(43, 6, 23, 3, '2023-01-18 17:10:54', 'TESSS', 'Livré'),
(44, 6, 23, 1, '2023-01-18 17:12:20', 'TEST', 'Livré'),
(45, 6, 23, 2, '2023-01-18 17:12:48', 'TTT', 'Livré'),
(46, 6, 23, 10, '2023-01-18 17:15:51', 'test2', 'Livré'),
(47, 6, 24, 2, '2023-01-18 17:22:52', 'TEST', 'Livré'),
(48, 6, 24, 2, '2023-01-18 17:25:55', 'tt', 'Livré'),
(49, 6, 24, 2, '2023-01-18 17:34:26', 'TEST', 'Livré'),
(50, 6, 24, 2, '2023-01-18 17:37:44', 'TEST', 'Livré'),
(51, 6, 24, 1, '2023-01-18 17:38:46', 'TEST', 'Livré'),
(52, 6, 25, 2, '2023-01-18 17:39:59', 'BB', 'Livré'),
(53, 6, 25, 1, '2023-01-18 17:46:33', 'TEST', 'Livré'),
(54, 6, 25, 1, '2023-01-18 17:46:55', 'TEST', 'Livré'),
(55, 6, 25, 2, '2023-01-18 17:49:40', 'TEST', 'Livré');

-- --------------------------------------------------------

--
-- Structure de la table `compte`
--

CREATE TABLE `compte` (
  `id_compte` int(11) NOT NULL,
  `nom` varchar(250) NOT NULL,
  `prenom` varchar(250) NOT NULL,
  `email` varchar(250) NOT NULL,
  `mot_de_passe` varchar(250) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `compte`
--

INSERT INTO `compte` (`id_compte`, `nom`, `prenom`, `email`, `mot_de_passe`) VALUES
(7, 'Lahyan', 'Youssef', 'youssef@gmail.com', '12345678');

-- --------------------------------------------------------

--
-- Structure de la table `fournisseur`
--

CREATE TABLE `fournisseur` (
  `id_f` int(11) NOT NULL,
  `nom` varchar(250) NOT NULL,
  `prenom` varchar(250) NOT NULL,
  `mobile` varchar(250) NOT NULL,
  `email` varchar(250) NOT NULL,
  `adresse` varchar(250) NOT NULL,
  `ville` varchar(250) NOT NULL,
  `pays` varchar(250) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `fournisseur`
--

INSERT INTO `fournisseur` (`id_f`, `nom`, `prenom`, `mobile`, `email`, `adresse`, `ville`, `pays`) VALUES
(1, 'Jegoual', 'Anas', '06777777099', 'anas@gmail.com', 'Ain mezwar ', 'Marrakech', 'Maroc'),
(6, 'Ait hammo', 'Badr', '0677898667', 'Badr@gmail.com', 'sokoma massira', 'marrakech', 'maroc'),
(7, 'Lahyan', 'Youssef', '0987789876789', 'YOUSSEF@GMAIL.COM', 'TEST', 'MARRAKECH', 'MAROC');

-- --------------------------------------------------------

--
-- Structure de la table `vente`
--

CREATE TABLE `vente` (
  `id_v` int(11) NOT NULL,
  `article` varchar(250) NOT NULL,
  `quantité` int(11) NOT NULL,
  `prixUn` double NOT NULL,
  `total` double NOT NULL,
  `dateV` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `vente`
--

INSERT INTO `vente` (`id_v`, `article`, `quantité`, `prixUn`, `total`, `dateV`) VALUES
(27, 'CADUM', 10, 20, 200, '2023-01-18 17:17:56'),
(28, 'Doliprane', 1, 17, 17, '2023-01-18 17:20:23'),
(29, 'Doliprane', 1, 17, 17, '2023-01-18 17:22:28'),
(30, 'HEAD', 2, 10, 20, '2023-01-18 17:40:38'),
(31, 'Doliprane', 2, 17, 34, '2023-01-18 17:48:58'),
(32, 'Doliprane', 2, 17, 34, '2023-02-07 13:29:27');

--
-- Index pour les tables déchargées
--

--
-- Index pour la table `article`
--
ALTER TABLE `article`
  ADD PRIMARY KEY (`id_art`),
  ADD KEY `cate_id` (`cate_id`);

--
-- Index pour la table `categorie`
--
ALTER TABLE `categorie`
  ADD PRIMARY KEY (`id_cat`);

--
-- Index pour la table `client`
--
ALTER TABLE `client`
  ADD PRIMARY KEY (`id_client`);

--
-- Index pour la table `commande`
--
ALTER TABLE `commande`
  ADD PRIMARY KEY (`id_c`),
  ADD KEY `id_fo` (`id_fo`),
  ADD KEY `id_arti` (`id_arti`);

--
-- Index pour la table `compte`
--
ALTER TABLE `compte`
  ADD PRIMARY KEY (`id_compte`);

--
-- Index pour la table `fournisseur`
--
ALTER TABLE `fournisseur`
  ADD PRIMARY KEY (`id_f`);

--
-- Index pour la table `vente`
--
ALTER TABLE `vente`
  ADD PRIMARY KEY (`id_v`);

--
-- AUTO_INCREMENT pour les tables déchargées
--

--
-- AUTO_INCREMENT pour la table `article`
--
ALTER TABLE `article`
  MODIFY `id_art` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

--
-- AUTO_INCREMENT pour la table `categorie`
--
ALTER TABLE `categorie`
  MODIFY `id_cat` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT pour la table `client`
--
ALTER TABLE `client`
  MODIFY `id_client` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT pour la table `commande`
--
ALTER TABLE `commande`
  MODIFY `id_c` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=56;

--
-- AUTO_INCREMENT pour la table `compte`
--
ALTER TABLE `compte`
  MODIFY `id_compte` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT pour la table `fournisseur`
--
ALTER TABLE `fournisseur`
  MODIFY `id_f` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT pour la table `vente`
--
ALTER TABLE `vente`
  MODIFY `id_v` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=33;

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `article`
--
ALTER TABLE `article`
  ADD CONSTRAINT `article_ibfk_1` FOREIGN KEY (`cate_id`) REFERENCES `categorie` (`id_cat`);

--
-- Contraintes pour la table `commande`
--
ALTER TABLE `commande`
  ADD CONSTRAINT `commande_ibfk_1` FOREIGN KEY (`id_fo`) REFERENCES `fournisseur` (`id_f`),
  ADD CONSTRAINT `commande_ibfk_2` FOREIGN KEY (`id_arti`) REFERENCES `article` (`id_art`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
