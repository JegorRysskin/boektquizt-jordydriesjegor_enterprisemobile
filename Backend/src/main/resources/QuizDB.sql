-- phpMyAdmin SQL Dump
-- version 4.5.4.1deb2ubuntu2.1
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Gegenereerd op: 13 jan 2020 om 18:10
-- Serverversie: 5.7.27-0ubuntu0.16.04.1
-- PHP-versie: 7.0.33-0ubuntu0.16.04.6

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `QuizDB`
--

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `answer`
--

CREATE TABLE `answer` (
  `id` int(11) NOT NULL,
  `answer_string` varchar(255) DEFAULT NULL,
  `question_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `answer`
--

INSERT INTO `answer` (`id`, `answer_string`, `question_id`) VALUES
(1, 'Safechuck', 1),
(2, 'Video Assistant Referee', 3),
(3, 'Global positioning system', 5),
(4, 'Jump for Joy - 2 Unlimited', 7),
(5, 'Reservoir Dogs', 9),
(6, 'Montaigu', 11),
(7, 'Van der Graaf', 13),
(8, 'Misofonie', 15),
(9, 'Boomerang', 17),
(10, 'Geraardsbergen', 19),
(11, 'Boulet', 21),
(12, 'Mayonnaise', 23),
(13, 'Saté met frietjes', 25),
(14, 'Domme Gans', 27),
(15, 'De bruine onderbroeken', 29),
(16, 'De Getikte Idioten', 31),
(17, 'Gistapo', 33),
(18, 'Al-Qaida Airlines', 35),
(19, 'Ram-madam', 37),
(20, 'Kazachstan is the greatest country in the world, all other countries are run by little girls', 39);

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `answer_sequence`
--

CREATE TABLE `answer_sequence` (
  `next_val` bigint(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `answer_sequence`
--

INSERT INTO `answer_sequence` (`next_val`) VALUES
(21);

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `correctanswer`
--

CREATE TABLE `correctanswer` (
  `id` int(11) NOT NULL,
  `answer` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `correctanswer`
--

INSERT INTO `correctanswer` (`id`, `answer`) VALUES
(2, 'Safechuck'),
(4, 'Video Assistant Referee'),
(6, 'Global positioning system'),
(8, 'Jump for Joy - 2 Unlimited'),
(10, 'Reservoir Dogs'),
(12, 'Montaigu'),
(14, 'Van der Graaf'),
(16, 'Misofonie'),
(18, 'Boomerang'),
(20, 'Geraardsbergen'),
(22, 'Baudet'),
(24, 'Moeyart'),
(26, 'Île de la Cité'),
(28, 'Gantois'),
(30, 'De gele hesjes'),
(32, 'Reuzegom'),
(34, 'Maduro'),
(36, 'Ethiopian Airlines'),
(38, 'Christchurch'),
(40, 'Israël'),
(42, 'Chadli'),
(44, 'Brug met ongelijke leggers'),
(46, 'India'),
(48, 'Il Palio'),
(50, 'Banks'),
(52, 'Aguascalientes'),
(54, 'Spikeball'),
(56, 'Spider'),
(58, 'Fortnite'),
(60, 'Shuttle'),
(62, 'kroonbladen,  stamper,  meeldraden'),
(64, 'Zeevarken'),
(66, 'Spaakbeen en ellepijp'),
(68, 'Plasma'),
(70, 'Lustrum'),
(72, '112,5°'),
(74, 'Jangtsekiang'),
(76, 'Sikhisme'),
(78, 'Steenbokskeerkring'),
(80, 'Pyroclastische stroom'),
(82, 'Everywhere - Fleetwood Mac'),
(84, 'Ice ice baby - Vanilla Ice'),
(86, 'Smalltown Boy - Bronski Beat'),
(88, 'Nummer = We like to party – Vengaboys,  Videoclip = Sexy and I know it – LMFAO'),
(90, 'Nummer = Sonne - Rammstein, Videoclip = Satisfaction - Benny Benassi'),
(92, 'Bad to the Bone - George Thorogood'),
(94, 'Zeg eens meisje - Paul Severs'),
(96, 'Drop that beat - Ixxel'),
(98, 'Could you be loved - Bob Marley'),
(100, 'Whatever you want - Status Quo '),
(102, 'Safari'),
(104, 'Blair Witch Project'),
(106, 'Pie'),
(108, 'Brightfish'),
(110, 'In de Gloria'),
(112, 'Waze'),
(114, 'Jan Van de Bossche'),
(116, 'Game of Thrones'),
(118, 'Neveneffecten'),
(120, 'Stay Restless'),
(122, 'Coq-sur-Mer'),
(124, 'Herck-la-Ville'),
(126, 'Wezet'),
(128, 'Nijvel'),
(130, 'Borgworm'),
(132, 'Fouron-Saint-Martin'),
(134, 'Dendermonde'),
(136, 'Tirlemont'),
(138, 'Tubize'),
(140, 'Malines'),
(142, 'Anubis'),
(144, 'Byzantijnse Rijk '),
(146, 'Juli en Augustus'),
(148, 'Compiegne'),
(150, 'Polen'),
(152, 'Operation Overloard'),
(154, 'Unie van Socialistische Sovjetrepublieken'),
(156, '1961'),
(158, 'Oswald'),
(160, 'Utoya'),
(161, 'James Safechuck'),
(162, 'Safechuck James '),
(163, '2 Unlimited - Jump for Joy '),
(164, 'Volkert Van der Graaf'),
(165, 'Van der Graaf Volkert'),
(166, 'Thierry Baudet'),
(167, 'Baudet Thierry '),
(168, 'Bart Moeyart'),
(169, 'Moeyart Bart'),
(170, 'Kyra Gantois'),
(171, 'Gantois Kyra'),
(172, 'gele hesjes'),
(173, 'Nicolas Maduro'),
(174, 'Maduro Nicolas'),
(175, 'Nacer Chadli'),
(176, 'Chadli Nacer'),
(177, 'Gordon Banks'),
(178, 'Banks Gordon'),
(179, '1=kroonbladen, 2=stamper, 3=meeldraden'),
(180, 'scotoplanes'),
(181, 'radius en ulna'),
(182, '112,5'),
(183, 'Blauwe rivier'),
(184, 'Chiang Jiang'),
(185, 'Sikhs'),
(186, 'Pyroclastische golf'),
(187, 'Pyroclastische wolk'),
(188, 'gloedwolk'),
(189, 'nuée ardente'),
(190, 'Fleetwood Mac - Everywhere'),
(191, ''),
(192, 'Vanilla Ice - Ice ice baby'),
(193, 'Bronski Beat - Smalltown Boy '),
(194, 'Nummer = We like to party – Vengaboys,  Videoclip = I\'m Sexy and I know it – LMFAO'),
(195, 'Nummer = Vengaboys – We like to party,  Videoclip = LMFAO –  Sexy and I know it '),
(196, 'Nummer = Vengaboys – We like to party,  Videoclip = LMFAO –  I\'m Sexy and I know it '),
(197, 'Nummer = Rammstein - Sonne, Videoclip = Benny Benassi - Satisfaction'),
(198, 'George Thorogood - Bad to the Bone'),
(199, 'Paul Severs - Zeg eens meisje'),
(200, 'Ixxel - Drop that beat'),
(201, 'Bob Marley - Could you be loved '),
(202, 'Status Quo - Whatever you want'),
(203, 'Android Pie'),
(204, '9.0'),
(205, 'Android 9.0'),
(206, 'Van de Bossche Jan '),
(207, 'Byzantium'),
(208, 'Augustus en Juli'),
(209, 'Lee Harvey Oswald'),
(210, 'Oswald Lee Harvey');

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `hibernate_sequence`
--

CREATE TABLE `hibernate_sequence` (
  `next_val` bigint(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `hibernate_sequence`
--

INSERT INTO `hibernate_sequence` (`next_val`) VALUES
(211),
(211),
(211);

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `question`
--

CREATE TABLE `question` (
  `id` int(11) NOT NULL,
  `question_string` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `question`
--

INSERT INTO `question` (`id`, `question_string`) VALUES
(1, 'Vraag 1'),
(3, 'Vraag 2'),
(5, 'Vraag 3'),
(7, 'Vraag 4'),
(9, 'Vraag 5'),
(11, 'Vraag 6'),
(13, 'Vraag 7'),
(15, 'Vraag 8'),
(17, 'Vraag 9'),
(19, 'Vraag 10'),
(21, 'Vraag 1'),
(23, 'Vraag 2'),
(25, 'Vraag 3'),
(27, 'Vraag 4'),
(29, 'Vraag 5'),
(31, 'Vraag 6'),
(33, 'Vraag 7'),
(35, 'Vraag 8'),
(37, 'Vraag 9'),
(39, 'Vraag 10'),
(41, 'Vraag 1'),
(43, 'Vraag 2'),
(45, 'Vraag 3'),
(47, 'Vraag 4'),
(49, 'Vraag 5'),
(51, 'Vraag 6'),
(53, 'Vraag 7'),
(55, 'Vraag 8'),
(57, 'Vraag 9'),
(59, 'Vraag 10'),
(61, 'Vraag 1'),
(63, 'Vraag 2'),
(65, 'Vraag 3'),
(67, 'Vraag 4'),
(69, 'Vraag 5'),
(71, 'Vraag 6'),
(73, 'Vraag 7'),
(75, 'Vraag 8'),
(77, 'Vraag 9'),
(79, 'Vraag 10'),
(81, 'Vraag 1'),
(83, 'Vraag 2'),
(85, 'Vraag 3'),
(87, 'Vraag 4'),
(89, 'Vraag 5'),
(91, 'Vraag 6'),
(93, 'Vraag 7'),
(95, 'Vraag 8'),
(97, 'Vraag 9'),
(99, 'Vraag 10'),
(101, 'Vraag 1'),
(103, 'Vraag 2'),
(105, 'Vraag 3'),
(107, 'Vraag 4'),
(109, 'Vraag 5'),
(111, 'Vraag 6'),
(113, 'Vraag 7'),
(115, 'Vraag 8'),
(117, 'Vraag 9'),
(119, 'Vraag 10'),
(121, 'Vraag 1'),
(123, 'Vraag 2'),
(125, 'Vraag 3'),
(127, 'Vraag 4'),
(129, 'Vraag 5'),
(131, 'Vraag 6'),
(133, 'Vraag 7'),
(135, 'Vraag 8'),
(137, 'Vraag 9'),
(139, 'Vraag 10'),
(141, 'Vraag 1'),
(143, 'Vraag 2'),
(145, 'Vraag 3'),
(147, 'Vraag 4'),
(149, 'Vraag 5'),
(151, 'Vraag 6'),
(153, 'Vraag 7'),
(155, 'Vraag 8'),
(157, 'Vraag 9'),
(159, 'Vraag 10');

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `question_correct_answer_to_question`
--

CREATE TABLE `question_correct_answer_to_question` (
  `question_id` int(11) NOT NULL,
  `correct_answer_to_question_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `question_correct_answer_to_question`
--

INSERT INTO `question_correct_answer_to_question` (`question_id`, `correct_answer_to_question_id`) VALUES
(1, 2),
(1, 161),
(1, 162),
(3, 4),
(5, 6),
(7, 8),
(7, 163),
(9, 10),
(11, 12),
(13, 14),
(13, 164),
(13, 165),
(15, 16),
(17, 18),
(19, 20),
(21, 22),
(21, 166),
(21, 167),
(23, 24),
(23, 168),
(23, 169),
(25, 26),
(27, 28),
(27, 170),
(27, 171),
(29, 30),
(29, 172),
(31, 32),
(33, 34),
(33, 173),
(33, 174),
(35, 36),
(37, 38),
(39, 40),
(41, 42),
(41, 175),
(41, 176),
(43, 44),
(45, 46),
(47, 48),
(49, 50),
(49, 177),
(49, 178),
(51, 52),
(53, 54),
(55, 56),
(57, 58),
(59, 60),
(61, 62),
(61, 179),
(63, 64),
(63, 180),
(65, 66),
(65, 181),
(67, 68),
(69, 70),
(71, 72),
(71, 182),
(73, 74),
(73, 183),
(73, 184),
(75, 76),
(75, 185),
(77, 78),
(79, 80),
(79, 186),
(79, 187),
(79, 188),
(79, 189),
(81, 82),
(81, 190),
(81, 191),
(83, 84),
(83, 192),
(85, 86),
(85, 193),
(87, 88),
(87, 194),
(87, 195),
(87, 196),
(89, 90),
(89, 197),
(91, 92),
(91, 198),
(93, 94),
(93, 199),
(95, 96),
(95, 200),
(97, 98),
(97, 201),
(99, 100),
(99, 202),
(101, 102),
(103, 104),
(105, 106),
(105, 203),
(105, 204),
(105, 205),
(107, 108),
(109, 110),
(111, 112),
(113, 114),
(113, 206),
(115, 116),
(117, 118),
(119, 120),
(121, 122),
(123, 124),
(125, 126),
(127, 128),
(129, 130),
(131, 132),
(133, 134),
(135, 136),
(137, 138),
(139, 140),
(141, 142),
(143, 144),
(143, 207),
(145, 146),
(145, 208),
(147, 148),
(149, 150),
(151, 152),
(153, 154),
(155, 156),
(157, 158),
(157, 209),
(157, 210),
(159, 160);

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `quiz`
--

CREATE TABLE `quiz` (
  `id` int(11) NOT NULL,
  `enabled` bit(1) NOT NULL,
  `name` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `quiz`
--

INSERT INTO `quiz` (`id`, `enabled`, `name`) VALUES
(1, b'0', 'Quiz1'),
(2, b'0', 'Quiz2'),
(3, b'0', 'Quiz3'),
(4, b'1', 'Boektquiz 2019');

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `quiz_rounds`
--

CREATE TABLE `quiz_rounds` (
  `quiz_id` int(11) NOT NULL,
  `rounds_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `quiz_rounds`
--

INSERT INTO `quiz_rounds` (`quiz_id`, `rounds_id`) VALUES
(4, 1),
(4, 2),
(4, 3),
(4, 4),
(4, 5),
(4, 6),
(4, 7),
(4, 8);

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `quiz_sequence`
--

CREATE TABLE `quiz_sequence` (
  `next_val` bigint(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `quiz_sequence`
--

INSERT INTO `quiz_sequence` (`next_val`) VALUES
(5);

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `role`
--

CREATE TABLE `role` (
  `id` bigint(20) NOT NULL,
  `description` varchar(255) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `role`
--

INSERT INTO `role` (`id`, `description`, `name`) VALUES
(0, 'Team', 'USER'),
(1, 'Admin', 'ADMIN'),
(2, 'Engineer', 'ENGINEER');

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `round`
--

CREATE TABLE `round` (
  `id` int(11) NOT NULL,
  `enabled` bit(1) NOT NULL,
  `name` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `round`
--

INSERT INTO `round` (`id`, `enabled`, `name`) VALUES
(1, b'0', 'Ronde 1'),
(2, b'0', 'Ronde 2'),
(3, b'0', 'Ronde 3'),
(4, b'0', 'Ronde 4'),
(5, b'0', 'Ronde 5'),
(6, b'0', 'Ronde 6'),
(7, b'0', 'Ronde 7'),
(8, b'0', 'Ronde 8');

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `round_questions`
--

CREATE TABLE `round_questions` (
  `round_id` int(11) NOT NULL,
  `questions_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `round_questions`
--

INSERT INTO `round_questions` (`round_id`, `questions_id`) VALUES
(1, 1),
(1, 3),
(1, 5),
(1, 7),
(1, 9),
(1, 11),
(1, 13),
(1, 15),
(1, 17),
(1, 19),
(2, 21),
(2, 23),
(2, 25),
(2, 27),
(2, 29),
(2, 31),
(2, 33),
(2, 35),
(2, 37),
(2, 39),
(3, 41),
(3, 43),
(3, 45),
(3, 47),
(3, 49),
(3, 51),
(3, 53),
(3, 55),
(3, 57),
(3, 59),
(4, 61),
(4, 63),
(4, 65),
(4, 67),
(4, 69),
(4, 71),
(4, 73),
(4, 75),
(4, 77),
(4, 79),
(5, 81),
(5, 83),
(5, 85),
(5, 87),
(5, 89),
(5, 91),
(5, 93),
(5, 95),
(5, 97),
(5, 99),
(6, 101),
(6, 103),
(6, 105),
(6, 107),
(6, 109),
(6, 111),
(6, 113),
(6, 115),
(6, 117),
(6, 119),
(7, 121),
(7, 123),
(7, 125),
(7, 127),
(7, 129),
(7, 131),
(7, 133),
(7, 135),
(7, 137),
(7, 139),
(8, 141),
(8, 143),
(8, 145),
(8, 147),
(8, 149),
(8, 151),
(8, 153),
(8, 155),
(8, 157),
(8, 159);

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `round_sequence`
--

CREATE TABLE `round_sequence` (
  `next_val` bigint(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `round_sequence`
--

INSERT INTO `round_sequence` (`next_val`) VALUES
(9);

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `round_team_id_opened_round`
--

CREATE TABLE `round_team_id_opened_round` (
  `round_id` int(11) NOT NULL,
  `team_id_opened_round` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `round_team_id_opened_round`
--

INSERT INTO `round_team_id_opened_round` (`round_id`, `team_id_opened_round`) VALUES
(1, 4),
(1, 4),
(1, 4),
(1, 4),
(2, 4);

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `team`
--

CREATE TABLE `team` (
  `id` int(11) NOT NULL,
  `enabled` bit(1) NOT NULL,
  `name` varchar(255) DEFAULT NULL,
  `scores` double DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `team`
--

INSERT INTO `team` (`id`, `enabled`, `name`, `scores`) VALUES
(1, b'0', 'Team1', 30),
(2, b'0', 'Team2', 20),
(3, b'0', 'Team3', 10),
(4, b'1', 'Team4', 10);

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `team_answers`
--

CREATE TABLE `team_answers` (
  `team_id` int(11) NOT NULL,
  `answers_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `team_answers`
--

INSERT INTO `team_answers` (`team_id`, `answers_id`) VALUES
(4, 1),
(4, 2),
(4, 3),
(4, 4),
(4, 5),
(4, 6),
(4, 7),
(4, 8),
(4, 9),
(4, 10),
(4, 11),
(4, 12),
(4, 13),
(4, 14),
(4, 15),
(4, 16),
(4, 17),
(4, 18),
(4, 19),
(4, 20);

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `team_sequence`
--

CREATE TABLE `team_sequence` (
  `next_val` bigint(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `team_sequence`
--

INSERT INTO `team_sequence` (`next_val`) VALUES
(5);

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `user`
--

CREATE TABLE `user` (
  `id` bigint(20) NOT NULL,
  `password` varchar(255) DEFAULT NULL,
  `username` varchar(255) DEFAULT NULL,
  `team_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `user`
--

INSERT INTO `user` (`id`, `password`, `username`, `team_id`) VALUES
(1, '$2a$10$sXvem93Fd4nh9VDZFWcwBe5j9j43mJDTDjR6G3Qiv1/6nTIJGBgSW', 'admin', NULL),
(2, '$2a$10$8pzN69VGF46Ab9BeqWskEOqObvY7u6NsLY993otheCOH2TGwkMSyG', 'engineer', NULL),
(3, '$2a$10$OC/nkRkqVqbHWXYsmWbxD.OrOeaupHPWpZzj0GUFzFkckM/IcKe12', 'Team1', 1),
(4, '$2a$10$lGj1Em/wpDHdBL0c1QdRmuqhR13lcYlRxNpP10TEOEbZ/C.iPB4j2', 'Team2', 2),
(5, '$2a$10$AWDQ9WEtuBHacP7Abp6wgOd8fKWwau/ma8j.G2rU65HhJFgrtiVKu', 'Team3', 3),
(6, '$2a$10$F0HIp6y/EboOZv.MSOJFt.3xmibtJHxKD93PBbIavAqdjE2w29F9i', 'Team4', 4);

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `user_roles`
--

CREATE TABLE `user_roles` (
  `user_id` bigint(20) NOT NULL,
  `role_id` bigint(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `user_roles`
--

INSERT INTO `user_roles` (`user_id`, `role_id`) VALUES
(3, 0),
(4, 0),
(5, 0),
(6, 0),
(1, 1),
(2, 2);

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `user_sequence`
--

CREATE TABLE `user_sequence` (
  `next_val` bigint(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `user_sequence`
--

INSERT INTO `user_sequence` (`next_val`) VALUES
(7);

--
-- Indexen voor geëxporteerde tabellen
--

--
-- Indexen voor tabel `answer`
--
ALTER TABLE `answer`
  ADD PRIMARY KEY (`id`),
  ADD KEY `FK8frr4bcabmmeyyu60qt7iiblo` (`question_id`);

--
-- Indexen voor tabel `correctanswer`
--
ALTER TABLE `correctanswer`
  ADD PRIMARY KEY (`id`);

--
-- Indexen voor tabel `question`
--
ALTER TABLE `question`
  ADD PRIMARY KEY (`id`);

--
-- Indexen voor tabel `question_correct_answer_to_question`
--
ALTER TABLE `question_correct_answer_to_question`
  ADD UNIQUE KEY `UK_cjuglpqssash0j2ig9xbyrcb9` (`correct_answer_to_question_id`),
  ADD KEY `FKq0mb938t0advsa9roc4aoejrs` (`question_id`);

--
-- Indexen voor tabel `quiz`
--
ALTER TABLE `quiz`
  ADD PRIMARY KEY (`id`);

--
-- Indexen voor tabel `quiz_rounds`
--
ALTER TABLE `quiz_rounds`
  ADD UNIQUE KEY `UK_9eput4u0y9c54ihgwsdy6f7y2` (`rounds_id`),
  ADD KEY `FK4xsbbvp1uo8xxps46r9i9cqii` (`quiz_id`);

--
-- Indexen voor tabel `role`
--
ALTER TABLE `role`
  ADD PRIMARY KEY (`id`);

--
-- Indexen voor tabel `round`
--
ALTER TABLE `round`
  ADD PRIMARY KEY (`id`);

--
-- Indexen voor tabel `round_questions`
--
ALTER TABLE `round_questions`
  ADD UNIQUE KEY `UK_s73f74f2b70gi6wn9d7ngw5y1` (`questions_id`),
  ADD KEY `FKkbm6dtdnb5j10c2ly93r0ho1y` (`round_id`);

--
-- Indexen voor tabel `round_team_id_opened_round`
--
ALTER TABLE `round_team_id_opened_round`
  ADD KEY `FKgpn7kenfk0whb0i646wcjdauw` (`round_id`);

--
-- Indexen voor tabel `team`
--
ALTER TABLE `team`
  ADD PRIMARY KEY (`id`);

--
-- Indexen voor tabel `team_answers`
--
ALTER TABLE `team_answers`
  ADD UNIQUE KEY `UK_90sv0iosrc10gm9u8eb9v4u9f` (`answers_id`),
  ADD KEY `FKtqhg20m1v5gv480bm9smuq0ue` (`team_id`);

--
-- Indexen voor tabel `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id`),
  ADD KEY `FKbmqm8c8m2aw1vgrij7h0od0ok` (`team_id`);

--
-- Indexen voor tabel `user_roles`
--
ALTER TABLE `user_roles`
  ADD PRIMARY KEY (`user_id`,`role_id`),
  ADD KEY `FKrhfovtciq1l558cw6udg0h0d3` (`role_id`);

--
-- Beperkingen voor geëxporteerde tabellen
--

--
-- Beperkingen voor tabel `answer`
--
ALTER TABLE `answer`
  ADD CONSTRAINT `FK8frr4bcabmmeyyu60qt7iiblo` FOREIGN KEY (`question_id`) REFERENCES `question` (`id`);

--
-- Beperkingen voor tabel `question_correct_answer_to_question`
--
ALTER TABLE `question_correct_answer_to_question`
  ADD CONSTRAINT `FKgh5ybdwno1ejhp2nay4fpw04w` FOREIGN KEY (`correct_answer_to_question_id`) REFERENCES `correctanswer` (`id`),
  ADD CONSTRAINT `FKq0mb938t0advsa9roc4aoejrs` FOREIGN KEY (`question_id`) REFERENCES `question` (`id`);

--
-- Beperkingen voor tabel `quiz_rounds`
--
ALTER TABLE `quiz_rounds`
  ADD CONSTRAINT `FK4xsbbvp1uo8xxps46r9i9cqii` FOREIGN KEY (`quiz_id`) REFERENCES `quiz` (`id`),
  ADD CONSTRAINT `FKfi1gcgj6bpjsoj971oqa8fom9` FOREIGN KEY (`rounds_id`) REFERENCES `round` (`id`);

--
-- Beperkingen voor tabel `round_questions`
--
ALTER TABLE `round_questions`
  ADD CONSTRAINT `FKkbm6dtdnb5j10c2ly93r0ho1y` FOREIGN KEY (`round_id`) REFERENCES `round` (`id`),
  ADD CONSTRAINT `FKr83evp2akmmj61h4286fjhcu` FOREIGN KEY (`questions_id`) REFERENCES `question` (`id`);

--
-- Beperkingen voor tabel `round_team_id_opened_round`
--
ALTER TABLE `round_team_id_opened_round`
  ADD CONSTRAINT `FKgpn7kenfk0whb0i646wcjdauw` FOREIGN KEY (`round_id`) REFERENCES `round` (`id`);

--
-- Beperkingen voor tabel `team_answers`
--
ALTER TABLE `team_answers`
  ADD CONSTRAINT `FK18qe215rs9vd09c29ai0lier4` FOREIGN KEY (`answers_id`) REFERENCES `answer` (`id`),
  ADD CONSTRAINT `FKtqhg20m1v5gv480bm9smuq0ue` FOREIGN KEY (`team_id`) REFERENCES `team` (`id`);

--
-- Beperkingen voor tabel `user`
--
ALTER TABLE `user`
  ADD CONSTRAINT `FKbmqm8c8m2aw1vgrij7h0od0ok` FOREIGN KEY (`team_id`) REFERENCES `team` (`id`);

--
-- Beperkingen voor tabel `user_roles`
--
ALTER TABLE `user_roles`
  ADD CONSTRAINT `FK55itppkw3i07do3h7qoclqd4k` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`),
  ADD CONSTRAINT `FKrhfovtciq1l558cw6udg0h0d3` FOREIGN KEY (`role_id`) REFERENCES `role` (`id`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
