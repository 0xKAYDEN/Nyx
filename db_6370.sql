/*
 Navicat Premium Dump SQL

 Source Server         : localhost_3306
 Source Server Type    : MySQL
 Source Server Version : 80017 (8.0.17)
 Source Host           : localhost:3306
 Source Schema         : db_6370

 Target Server Type    : MySQL
 Target Server Version : 80017 (8.0.17)
 File Encoding         : 65001

 Date: 05/03/2025 08:56:59
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for accounts
-- ----------------------------
DROP TABLE IF EXISTS `accounts`;
CREATE TABLE `accounts`  (
  `Username` char(25) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `Password` char(16) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `IP` char(15) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '',
  `EntityID` bigint(18) UNSIGNED NULL DEFAULT 0,
  `State` tinyint(5) UNSIGNED NULL DEFAULT NULL,
  `MobileNumber` char(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `Email` char(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `HDSerial` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `PhoneNumber` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `securitycode` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `ProfileImage` mediumblob NULL,
  `RegisterationDate` datetime NULL DEFAULT NULL,
  PRIMARY KEY (`Username`, `securitycode`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of accounts
-- ----------------------------
INSERT INTO `accounts` VALUES ('3', '3', '192.168.0.11', 1000015, 4, '', '', NULL, NULL, '', NULL, NULL);
INSERT INTO `accounts` VALUES ('2', '2', '192.168.0.11', 1000014, 4, '', '', NULL, NULL, '', NULL, NULL);
INSERT INTO `accounts` VALUES ('1', '1', '192.168.0.11', 1000013, 4, '', '', NULL, NULL, '', NULL, NULL);

-- ----------------------------
-- Table structure for achievement
-- ----------------------------
DROP TABLE IF EXISTS `achievement`;
CREATE TABLE `achievement`  (
  `Name` varchar(16) CHARACTER SET latin1 COLLATE latin1_german1_ci NOT NULL,
  `UID` bigint(18) UNSIGNED NOT NULL,
  `Owner` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Achievement` varchar(999) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`UID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of achievement
-- ----------------------------
INSERT INTO `achievement` VALUES ('MyMido', 1000010, '3', '134217952#2147745792#458240#939785984#524291#6208#3#129602#0#2147483648#524320#15758849#2#');
INSERT INTO `achievement` VALUES ('Mid0', 1000009, '2', '64#3538944#3584#537264384#0#6144#0#0#0#0#0#16777216#0#');
INSERT INTO `achievement` VALUES ('EgYCo', 1000001, '1', '134217952#3407872#196104#940048128#0#6144#0#512#4092#2147483648#524290#16777217#0#');
INSERT INTO `achievement` VALUES ('RAVEN', 1000013, '1', '134480096#262144#135200256#939966208#3671047#1342388288#79#129154#124#0#524288#125833219#2#');
INSERT INTO `achievement` VALUES ('Alucard', 1000014, '2', '32#0#0#0#524288#4096#0#2048#0#0#0#0#0#');

-- ----------------------------
-- Table structure for activeness
-- ----------------------------
DROP TABLE IF EXISTS `activeness`;
CREATE TABLE `activeness`  (
  `UID` bigint(18) NOT NULL DEFAULT 0,
  `ClaimedGift1` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  `ClaimedGift2` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  `ClaimedGift3` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  `ChiStudy` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  `JiangStudy` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  `DidTIB` bigint(18) NOT NULL DEFAULT 0,
  `EnlightmentTimes` bigint(18) NOT NULL DEFAULT 0,
  `HoursTimes` bigint(18) NOT NULL DEFAULT 0,
  PRIMARY KEY (`UID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of activeness
-- ----------------------------
INSERT INTO `activeness` VALUES (1000001, 0, 0, 0, 0, 0, 0, 0, 0);
INSERT INTO `activeness` VALUES (1000002, 0, 0, 0, 0, 0, 0, 0, 0);
INSERT INTO `activeness` VALUES (1000003, 0, 0, 0, 0, 0, 0, 0, 0);
INSERT INTO `activeness` VALUES (1000004, 0, 0, 0, 0, 0, 0, 0, 0);
INSERT INTO `activeness` VALUES (1000005, 0, 0, 0, 0, 0, 0, 0, 0);
INSERT INTO `activeness` VALUES (1000006, 0, 0, 0, 0, 0, 0, 0, 0);
INSERT INTO `activeness` VALUES (1000007, 0, 0, 0, 71, 0, 0, 0, 2);
INSERT INTO `activeness` VALUES (1000008, 0, 0, 0, 0, 0, 0, 0, 1);
INSERT INTO `activeness` VALUES (1000009, 0, 0, 0, 0, 0, 0, 0, 0);
INSERT INTO `activeness` VALUES (1000010, 0, 0, 0, 0, 0, 0, 0, 0);
INSERT INTO `activeness` VALUES (1000013, 0, 0, 0, 0, 0, 0, 0, 0);
INSERT INTO `activeness` VALUES (1000014, 0, 0, 0, 0, 0, 0, 0, 0);

-- ----------------------------
-- Table structure for adv
-- ----------------------------
DROP TABLE IF EXISTS `adv`;
CREATE TABLE `adv`  (
  `Rank` int(255) NOT NULL DEFAULT 0,
  `UID` int(32) NOT NULL DEFAULT 0,
  `Name` char(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `LeaderName` char(17) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '0',
  `Buletin` char(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT 'this is new guild.',
  `Level` int(32) NOT NULL DEFAULT 9,
  `MemberCount` int(255) NULL DEFAULT 1,
  `SilverFund` int(255) NULL DEFAULT 500000,
  PRIMARY KEY (`Rank`, `UID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of adv
-- ----------------------------

-- ----------------------------
-- Table structure for apprentice
-- ----------------------------
DROP TABLE IF EXISTS `apprentice`;
CREATE TABLE `apprentice`  (
  `MentorID` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `MentorName` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `ApprenticeID` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `ApprenticeName` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `EnroleDate` bigint(255) UNSIGNED NOT NULL DEFAULT 0,
  `Actual_Experience` bigint(255) UNSIGNED NULL DEFAULT 0,
  `Actual_Plus` bigint(255) UNSIGNED NULL DEFAULT 0,
  `Actual_HeavenBlessing` bigint(255) UNSIGNED NULL DEFAULT 0,
  `Total_Experience` bigint(255) UNSIGNED NULL DEFAULT 0,
  `Total_Plus` bigint(255) UNSIGNED NULL DEFAULT 0,
  `Total_HeavenBlessing` bigint(255) UNSIGNED NULL DEFAULT 0
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of apprentice
-- ----------------------------

-- ----------------------------
-- Table structure for arena
-- ----------------------------
DROP TABLE IF EXISTS `arena`;
CREATE TABLE `arena`  (
  `EntityID` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `EntityName` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `ArenaPoints` int(10) UNSIGNED NULL DEFAULT 0,
  `ActivityPoints` int(10) UNSIGNED NULL DEFAULT 0,
  `TodayWin` int(10) UNSIGNED NULL DEFAULT 0,
  `TodayBattles` int(10) UNSIGNED NULL DEFAULT 0,
  `TotalWin` int(10) UNSIGNED NULL DEFAULT 0,
  `TotalLose` int(10) UNSIGNED NULL DEFAULT 0,
  `CurrentHonor` bigint(10) UNSIGNED NULL DEFAULT 0,
  `HistoryHonor` int(10) UNSIGNED NULL DEFAULT 0,
  `LastSeasonRank` int(10) UNSIGNED NULL DEFAULT 0,
  `Level` int(10) UNSIGNED NULL DEFAULT 0,
  `Class` int(10) UNSIGNED NULL DEFAULT 0,
  `ArenaPointFill` bigint(255) UNSIGNED NULL DEFAULT 0,
  `Model` bigint(255) UNSIGNED NULL DEFAULT 0,
  `LastSeasonArenaPoints` bigint(255) UNSIGNED NULL DEFAULT 0,
  `LastSeasonWin` int(10) UNSIGNED NULL DEFAULT 0,
  `LastSeasonLose` int(10) UNSIGNED NULL DEFAULT 0,
  `ChampionPoints` int(10) UNSIGNED NULL DEFAULT 0,
  `TodayChampionPoints` int(10) UNSIGNED NULL DEFAULT 0,
  `AllChampionPoints` int(10) UNSIGNED NULL DEFAULT 0,
  PRIMARY KEY (`EntityID`) USING BTREE,
  UNIQUE INDEX `myIndex`(`EntityID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of arena
-- ----------------------------
INSERT INTO `arena` VALUES (1000001, 'EgYCo', 4000, 0, 0, 0, 5, 8, 0, 0, 2, 140, 55, 636095277370777406, 331003, 3997, 1, 0, 600, 650, 0);
INSERT INTO `arena` VALUES (1000010, 'MyMido', 4003, 0, 1, 1, 8, 5, 0, 0, 1, 140, 25, 636094255478629656, 351003, 4000, 0, 0, 600, 650, 0);
INSERT INTO `arena` VALUES (1000009, 'Mid0', 4000, 0, 0, 0, 0, 0, 0, 0, 0, 140, 80, 636090056378093791, 381003, 0, 0, 0, 0, 0, 0);
INSERT INTO `arena` VALUES (1000013, '', 4000, 0, 0, 0, 0, 0, 52000, 0, 0, 140, 45, 638767283865496208, 1391003, 4000, 0, 0, 30000, 650, 0);
INSERT INTO `arena` VALUES (1000014, 'Alucard', 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 50, 638758147875495655, 401003, 0, 0, 0, 0, 0, 0);

-- ----------------------------
-- Table structure for auction
-- ----------------------------
DROP TABLE IF EXISTS `auction`;
CREATE TABLE `auction`  (
  `ID` mediumint(10) UNSIGNED NOT NULL DEFAULT 0,
  `UID` int(250) UNSIGNED NOT NULL DEFAULT 0,
  `Plus` smallint(5) UNSIGNED NULL DEFAULT 0,
  `Bless` smallint(5) UNSIGNED NULL DEFAULT 0,
  `Enchant` smallint(5) UNSIGNED NOT NULL DEFAULT 0,
  `SocketOne` smallint(5) UNSIGNED NULL DEFAULT 0,
  `SocketTwo` smallint(5) UNSIGNED NULL DEFAULT 0,
  `Durability` mediumint(50) UNSIGNED NOT NULL DEFAULT 0,
  `MaximDurability` mediumint(50) UNSIGNED NOT NULL DEFAULT 0,
  `SocketProgress` int(40) UNSIGNED NULL DEFAULT 0,
  `PlusProgress` int(40) UNSIGNED NULL DEFAULT 0,
  `Effect` smallint(5) UNSIGNED NULL DEFAULT 0,
  `Bound` smallint(5) UNSIGNED NULL DEFAULT 0,
  `Locked` smallint(5) UNSIGNED NULL DEFAULT 0,
  `UnlockEnd` bigint(250) UNSIGNED NULL DEFAULT 0,
  `Suspicious` smallint(5) UNSIGNED NULL DEFAULT 0,
  `SuspiciousStart` smallint(5) UNSIGNED NULL DEFAULT 0,
  `Color` smallint(5) UNSIGNED NULL DEFAULT 0,
  `Position` mediumint(10) UNSIGNED NULL DEFAULT 0,
  `Warehouse` mediumint(10) UNSIGNED NULL DEFAULT 0,
  `StackSize` smallint(5) UNSIGNED NOT NULL DEFAULT 0,
  `Inscribed` int(36) UNSIGNED NOT NULL DEFAULT 0,
  `agate` text CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `RefineryStarted` bigint(36) NOT NULL DEFAULT 0,
  `RefineryPercent` int(36) NOT NULL DEFAULT 0,
  `RefineryLevel` int(36) NOT NULL DEFAULT 0,
  `RefineryItem` int(4) NOT NULL DEFAULT 0,
  `RefineryTime` bigint(16) NOT NULL DEFAULT 0,
  `NextSteedColor` int(16) NOT NULL DEFAULT 0,
  `EndTime` bigint(250) UNSIGNED NULL DEFAULT 0,
  `BasePrice` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  `BidderUID` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  `FixedPrice` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  `MoneyType` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  `SellerUID` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  `Time` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  `SellerName` varchar(16) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `BidderName` varchar(16) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`UID`, `SellerName`, `BidderName`) USING BTREE,
  UNIQUE INDEX `myIndex`(`UID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of auction
-- ----------------------------

-- ----------------------------
-- Table structure for bannedips
-- ----------------------------
DROP TABLE IF EXISTS `bannedips`;
CREATE TABLE `bannedips`  (
  `ip_int` int(10) NOT NULL,
  `ip` varchar(16) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`ip_int`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of bannedips
-- ----------------------------

-- ----------------------------
-- Table structure for chi
-- ----------------------------
DROP TABLE IF EXISTS `chi`;
CREATE TABLE `chi`  (
  `uid` int(18) NOT NULL,
  `name` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `points` int(4) NULL DEFAULT 0,
  `chipowers` longblob NULL,
  PRIMARY KEY (`uid`) USING BTREE,
  UNIQUE INDEX `myIndex`(`uid` ASC) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of chi
-- ----------------------------
INSERT INTO `chi` VALUES (1000013, 'RAVEN', 14400, 0x040101C80003C80006AC0D07D0070201C80003C80006AC0D07D0070301C80003C80006AC0D07D0070401C80003C80006AC0D07D007);
INSERT INTO `chi` VALUES (1000014, 'Alucard', 500, 0x00);

-- ----------------------------
-- Table structure for chiretreat
-- ----------------------------
DROP TABLE IF EXISTS `chiretreat`;
CREATE TABLE `chiretreat`  (
  `uid` int(18) NOT NULL,
  `chipowers` longblob NULL,
  PRIMARY KEY (`uid`) USING BTREE,
  UNIQUE INDEX `myIndex`(`uid` ASC) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of chiretreat
-- ----------------------------
INSERT INTO `chiretreat` VALUES (1000001, NULL);
INSERT INTO `chiretreat` VALUES (1000002, NULL);
INSERT INTO `chiretreat` VALUES (1000003, NULL);
INSERT INTO `chiretreat` VALUES (1000004, NULL);
INSERT INTO `chiretreat` VALUES (1000005, NULL);
INSERT INTO `chiretreat` VALUES (1000006, NULL);
INSERT INTO `chiretreat` VALUES (1000007, NULL);
INSERT INTO `chiretreat` VALUES (1000008, NULL);
INSERT INTO `chiretreat` VALUES (1000009, NULL);
INSERT INTO `chiretreat` VALUES (1000010, 0x010100000000000000000000000039D5CA2C61DCD388);
INSERT INTO `chiretreat` VALUES (1000013, NULL);
INSERT INTO `chiretreat` VALUES (1000014, NULL);

-- ----------------------------
-- Table structure for claimitems
-- ----------------------------
DROP TABLE IF EXISTS `claimitems`;
CREATE TABLE `claimitems`  (
  `ItemUID` bigint(18) NOT NULL DEFAULT 0,
  `Date` bigint(255) NULL DEFAULT NULL,
  `ConquerPointsCost` int(20) NULL DEFAULT NULL,
  `OwnerUID` bigint(18) NULL DEFAULT NULL,
  `OwnerName` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `GainerUID` bigint(18) NULL DEFAULT NULL,
  `GainerName` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of claimitems
-- ----------------------------

-- ----------------------------
-- Table structure for clanrelation
-- ----------------------------
DROP TABLE IF EXISTS `clanrelation`;
CREATE TABLE `clanrelation`  (
  `id` int(60) NOT NULL AUTO_INCREMENT,
  `clanid` bigint(255) UNSIGNED NULL DEFAULT 0,
  `AssociatedId` bigint(255) UNSIGNED NULL DEFAULT 0,
  `type` bigint(16) UNSIGNED NULL DEFAULT 0,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1455 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of clanrelation
-- ----------------------------

-- ----------------------------
-- Table structure for clans
-- ----------------------------
DROP TABLE IF EXISTS `clans`;
CREATE TABLE `clans`  (
  `Identifier` int(32) NOT NULL DEFAULT 0,
  `LeaderId` int(32) NOT NULL DEFAULT 0,
  `Name` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '0',
  `Fund` int(32) NOT NULL DEFAULT 0,
  `Announcement` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT 'This is new clan!',
  `BPTower` int(32) NOT NULL DEFAULT 5,
  `Level` int(32) NOT NULL DEFAULT 5,
  `LeaderName` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '0',
  `polekeeper` int(1) NOT NULL DEFAULT 0,
  `ClanID` int(5) NULL DEFAULT NULL,
  PRIMARY KEY (`Identifier`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 33 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of clans
-- ----------------------------
INSERT INTO `clans` VALUES (600, 1000001, 'EgYCo', 250000, '', 0, 1, 'EgYCo', 0, NULL);

-- ----------------------------
-- Table structure for configuration
-- ----------------------------
DROP TABLE IF EXISTS `configuration`;
CREATE TABLE `configuration`  (
  `ServerIP` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT 'Type Here Your Server IP!',
  `ServerPort` bigint(4) UNSIGNED NOT NULL DEFAULT 9958,
  `Server` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `EntityID` bigint(10) UNSIGNED NULL DEFAULT 1000000,
  `ItemUID` bigint(10) UNSIGNED NULL DEFAULT 1,
  `GuildID` bigint(10) UNSIGNED NULL DEFAULT 1,
  `UnionID` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  `ServerKingdom` bigint(10) UNSIGNED NOT NULL DEFAULT 0,
  `SpellExperienceRate` bigint(10) UNSIGNED NULL DEFAULT 1,
  `ExperienceRate` bigint(10) UNSIGNED NULL DEFAULT 1,
  `ProficiencyExperienceRate` bigint(10) UNSIGNED NULL DEFAULT 1,
  `MoneyDropRate` bigint(10) UNSIGNED NULL DEFAULT 50,
  `ConquerPointsDropRate` bigint(10) UNSIGNED NULL DEFAULT 10,
  `ItemDropRate` bigint(10) UNSIGNED NULL DEFAULT 25,
  `ItemDropQualityString` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '50~20~15~10~5',
  `LastDailySignReset` bigint(255) UNSIGNED NOT NULL DEFAULT 0,
  PRIMARY KEY (`Server`, `ServerIP`(16)) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of configuration
-- ----------------------------
INSERT INTO `configuration` VALUES ('192.168.0.11', 9960, 'DeadRabbit', 1000015, 5067766, 1009, 2001, 2001, 20, 20, 20, 50, 10, 10, '25~10~7~3~1', 3);

-- ----------------------------
-- Table structure for dailyquest
-- ----------------------------
DROP TABLE IF EXISTS `dailyquest`;
CREATE TABLE `dailyquest`  (
  `UID` bigint(20) NOT NULL,
  `Name` varchar(255) CHARACTER SET utf16 COLLATE utf16_general_ci NULL DEFAULT NULL,
  `DesertGurdian` int(11) NULL DEFAULT 0,
  `TowerOfMystery` int(11) NULL DEFAULT NULL,
  `TowerLevel` int(11) NULL DEFAULT NULL,
  `DayNumber` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`UID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf16 COLLATE = utf16_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of dailyquest
-- ----------------------------

-- ----------------------------
-- Table structure for dailyquests
-- ----------------------------
DROP TABLE IF EXISTS `dailyquests`;
CREATE TABLE `dailyquests`  (
  `UID` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  `ETHAPItemID` bigint(18) NOT NULL DEFAULT 0,
  `ETHAPItemAmount` bigint(18) NOT NULL DEFAULT 0,
  `SpiritBeadsCount` bigint(18) NOT NULL DEFAULT 0,
  `SpiritBeadsType` bigint(18) NOT NULL DEFAULT 0,
  `RareMaterialsAmount` bigint(18) NOT NULL DEFAULT 0,
  `RareMaterialsItemID` bigint(18) NOT NULL DEFAULT 0,
  `DidFirstHeavenTreasury` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  `TOMRC` bigint(18) UNSIGNED NOT NULL DEFAULT 3,
  `TOMQ` bigint(18) UNSIGNED NOT NULL DEFAULT 255,
  `TOM` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  `DidTOM` bigint(18) NOT NULL DEFAULT 0,
  PRIMARY KEY (`UID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of dailyquests
-- ----------------------------
INSERT INTO `dailyquests` VALUES (1000001, 0, 0, 0, 0, 0, 0, 0, 3, 255, 0, 0);
INSERT INTO `dailyquests` VALUES (1000002, 0, 0, 0, 0, 0, 0, 0, 3, 255, 0, 0);
INSERT INTO `dailyquests` VALUES (1000003, 0, 0, 0, 0, 0, 0, 0, 3, 255, 0, 0);
INSERT INTO `dailyquests` VALUES (1000004, 0, 0, 0, 0, 0, 0, 0, 3, 255, 0, 0);
INSERT INTO `dailyquests` VALUES (1000005, 0, 0, 0, 0, 0, 0, 0, 3, 255, 0, 0);
INSERT INTO `dailyquests` VALUES (1000006, 0, 0, 0, 0, 0, 0, 0, 3, 255, 0, 0);
INSERT INTO `dailyquests` VALUES (1000007, 0, 0, 0, 0, 0, 0, 0, 3, 255, 0, 0);
INSERT INTO `dailyquests` VALUES (1000008, 0, 0, 0, 0, 0, 0, 0, 0, 5, 3, 0);
INSERT INTO `dailyquests` VALUES (1000009, 0, 0, 0, 0, 0, 0, 0, 3, 255, 0, 0);
INSERT INTO `dailyquests` VALUES (1000010, 0, 0, 0, 0, 0, 0, 0, 3, 255, 0, 0);
INSERT INTO `dailyquests` VALUES (1000013, 0, 0, 0, 0, 0, 0, 0, 3, 255, 0, 0);
INSERT INTO `dailyquests` VALUES (1000014, 0, 0, 0, 0, 0, 0, 0, 3, 255, 0, 0);

-- ----------------------------
-- Table structure for detaineditems
-- ----------------------------
DROP TABLE IF EXISTS `detaineditems`;
CREATE TABLE `detaineditems`  (
  `ItemUID` bigint(18) NOT NULL DEFAULT 0,
  `Date` bigint(255) NULL DEFAULT NULL,
  `ConquerPointsCost` int(20) NULL DEFAULT NULL,
  `OwnerUID` bigint(18) NULL DEFAULT NULL,
  `OwnerName` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `GainerUID` bigint(18) NULL DEFAULT NULL,
  `GainerName` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ID` bigint(18) NOT NULL,
  PRIMARY KEY (`ItemUID`) USING BTREE,
  UNIQUE INDEX `myIndex`(`ItemUID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of detaineditems
-- ----------------------------

-- ----------------------------
-- Table structure for elitepk
-- ----------------------------
DROP TABLE IF EXISTS `elitepk`;
CREATE TABLE `elitepk`  (
  `UID` int(32) NOT NULL DEFAULT 0,
  `Avatar` int(32) NOT NULL DEFAULT 0,
  `Name` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Mesh` int(32) NOT NULL DEFAULT 0,
  `Points` int(32) NOT NULL DEFAULT 0,
  `Postion` int(32) NOT NULL DEFAULT 0,
  `MyTitle` bigint(32) NOT NULL DEFAULT 0,
  PRIMARY KEY (`UID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of elitepk
-- ----------------------------

-- ----------------------------
-- Table structure for enemy
-- ----------------------------
DROP TABLE IF EXISTS `enemy`;
CREATE TABLE `enemy`  (
  `EntityID` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `EnemyID` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `EnemyName` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT ''
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of enemy
-- ----------------------------
INSERT INTO `enemy` VALUES (1000001, 1000010, 'MyMido');

-- ----------------------------
-- Table structure for entities
-- ----------------------------
DROP TABLE IF EXISTS `entities`;
CREATE TABLE `entities`  (
  `Name` varchar(16) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `UID` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  `Owner` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `HairStyle` smallint(12) UNSIGNED NULL DEFAULT 215,
  `Class` tinyint(5) UNSIGNED NOT NULL DEFAULT 10,
  `Money` bigint(18) UNSIGNED NULL DEFAULT 100,
  `ConquerPoints` bigint(18) UNSIGNED NULL DEFAULT 0,
  `Body` smallint(12) UNSIGNED NOT NULL DEFAULT 0,
  `Face` smallint(12) UNSIGNED NOT NULL DEFAULT 0,
  `Level` tinyint(5) UNSIGNED NULL DEFAULT 1,
  `Strength` smallint(12) UNSIGNED NULL DEFAULT 1,
  `Agility` smallint(12) UNSIGNED NULL DEFAULT 1,
  `Vitality` smallint(12) UNSIGNED NULL DEFAULT 1,
  `Spirit` smallint(12) UNSIGNED NULL DEFAULT 0,
  `Atributes` smallint(12) UNSIGNED NULL DEFAULT 0,
  `Hitpoints` mediumint(16) UNSIGNED NOT NULL DEFAULT 93,
  `Mana` mediumint(16) UNSIGNED NULL DEFAULT 0,
  `MapID` smallint(12) UNSIGNED NULL DEFAULT 1002,
  `X` smallint(12) UNSIGNED NULL DEFAULT 300,
  `Y` smallint(12) UNSIGNED NULL DEFAULT 278,
  `PKPoints` smallint(12) UNSIGNED NULL DEFAULT 0,
  `Experience` bigint(255) UNSIGNED NULL DEFAULT 0,
  `QuizPoints` bigint(18) NOT NULL DEFAULT 0,
  `PreviousMapID` smallint(12) UNSIGNED NULL DEFAULT 1002,
  `Spouse` varchar(16) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT 'None',
  `WarehousePW` bigint(16) NULL DEFAULT NULL,
  `MoneySave` bigint(18) UNSIGNED NULL DEFAULT 0,
  `FirstRebornClass` tinyint(5) UNSIGNED NULL DEFAULT 0,
  `SecondRebornClass` tinyint(5) UNSIGNED NULL DEFAULT 0,
  `FirstRebornLevel` tinyint(5) UNSIGNED NULL DEFAULT 0,
  `SecondRebornLevel` tinyint(5) UNSIGNED NULL DEFAULT 0,
  `Online` tinyint(2) UNSIGNED NULL DEFAULT 0,
  `EnlightenPoints` bigint(18) UNSIGNED NULL DEFAULT 0,
  `DoubleExpTime` bigint(18) UNSIGNED NULL DEFAULT 0,
  `HeavenBlessingTime` bigint(255) UNSIGNED NULL DEFAULT 0,
  `BlessTime` bigint(18) UNSIGNED NULL DEFAULT 0,
  `LastDragonBallUse` bigint(255) NULL DEFAULT 0,
  `LastResetTime` bigint(255) NULL DEFAULT 0,
  `EnlightsReceived` tinyint(5) UNSIGNED NULL DEFAULT 0,
  `EnlightmentWait` mediumint(100) UNSIGNED NULL DEFAULT 0,
  `DoubleExpToday` tinyint(5) UNSIGNED NULL DEFAULT 0,
  `GuildID` bigint(18) UNSIGNED NULL DEFAULT 0,
  `GuildRank` bigint(18) UNSIGNED NULL DEFAULT 0,
  `GuildSilverDonation` bigint(255) UNSIGNED NULL DEFAULT 0,
  `GuildConquerPointDonation` bigint(255) UNSIGNED NULL DEFAULT 0,
  `VIPLevel` tinyint(5) UNSIGNED NULL DEFAULT 0,
  `VirtuePoints` bigint(255) UNSIGNED NULL DEFAULT 0,
  `LotteryEntries` mediumint(10) NULL DEFAULT 0,
  `PreviousX` mediumint(10) NULL DEFAULT 0,
  `PreviousY` mediumint(10) NULL DEFAULT 0,
  `OfflineTGEnterTime` bigint(255) NULL DEFAULT 0,
  `ExpBalls` mediumint(10) NULL DEFAULT 0,
  `ClanId` int(36) UNSIGNED NULL DEFAULT 0,
  `ClanDonation` bigint(64) UNSIGNED NULL DEFAULT 0,
  `ClanRank` int(36) UNSIGNED NULL DEFAULT 0,
  `SubClass` int(36) UNSIGNED NOT NULL DEFAULT 0,
  `SubClassLevel` int(36) UNSIGNED NOT NULL DEFAULT 0,
  `StudyPoints` int(36) UNSIGNED NULL DEFAULT 0,
  `My_Title` int(36) NOT NULL DEFAULT 0,
  `EditNameTimes` bigint(255) UNSIGNED NOT NULL DEFAULT 5,
  `CountryID` smallint(4) NULL DEFAULT 0,
  `Achievement` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CTFCpsReward` bigint(255) NULL DEFAULT NULL,
  `CTFSilverReward` bigint(255) NULL DEFAULT NULL,
  `RacePoints` bigint(18) NOT NULL DEFAULT 0,
  `BoundCPs` int(30) UNSIGNED NULL DEFAULT 0,
  `GuildArsenalDonation` bigint(16) NULL DEFAULT NULL,
  `GuildLastLogin` bigint(16) NULL DEFAULT NULL,
  `ExtraInventory` bigint(255) UNSIGNED NULL DEFAULT 0,
  `Exploits` bigint(255) UNSIGNED NULL DEFAULT 0,
  `GLCTF` int(255) UNSIGNED NOT NULL DEFAULT 0,
  `OnlineTrainning` bigint(18) NULL DEFAULT NULL,
  `HuntingExp` bigint(18) NOT NULL DEFAULT 0,
  `FirstCredit` tinyint(6) NOT NULL DEFAULT 0,
  `AnsweredKingdomQuiz` tinyint(6) UNSIGNED NOT NULL DEFAULT 0,
  `StarterPackLevel` tinyint(5) UNSIGNED NOT NULL DEFAULT 0,
  `NormalGems` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  `UnionID` bigint(10) UNSIGNED NOT NULL DEFAULT 0,
  `Harem` bigint(10) UNSIGNED NOT NULL DEFAULT 0,
  `Offical` bigint(10) UNSIGNED NOT NULL DEFAULT 0,
  `Guards` bigint(10) UNSIGNED NOT NULL DEFAULT 0,
  `UnionExploits` bigint(10) UNSIGNED NOT NULL DEFAULT 0,
  `Merchant` bigint(255) UNSIGNED NOT NULL DEFAULT 0,
  `DidBrightFortune` bigint(255) UNSIGNED NOT NULL DEFAULT 0,
  `TotalPerfectionScore` bigint(255) UNSIGNED NOT NULL DEFAULT 0,
  `BlackList` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `DailySignedDays` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `DailySignRewards` bigint(255) UNSIGNED NOT NULL DEFAULT 0,
  `Wardrobe` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DailySignVIPChances` bigint(255) UNSIGNED NOT NULL DEFAULT 10,
  `SwordSoul` int(255) NULL DEFAULT 0,
  `BansheeSpirit` int(11) NULL DEFAULT 0,
  PRIMARY KEY (`UID`) USING BTREE,
  UNIQUE INDEX `myIndex`(`UID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of entities
-- ----------------------------
INSERT INTO `entities` VALUES ('RAVEN', 1000013, '1', 574, 45, 117946675, 12009732, 1003, 139, 140, 74, 256, 37, 0, 62, 38595, 800, 1036, 195, 185, 0, 0, 0, 1002, 'None', 0, 0, 25, 25, 140, 140, 1, 8, 0, 48566, 0, 0, 638767284190059567, 0, 0, 0, 1009, 1000, 1000000, 0, 6, 0, 0, 314, 333, 0, 0, 0, 0, 0, 0, 0, 7935, 0, 5, 0, '134480096#262144#135200256#939966208#3671047#1342388288#79#129154#124#0#524288#125833219#2#', 0, 0, 0, 400, 0, NULL, 190, 0, 0, 46, 7, 2, 0, 0, 0, 2003, 0, 0, 0, 100, 0, 1, 636627, '', '0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#', 0, '0-0~0-0', 10, 0, 0);
INSERT INTO `entities` VALUES ('Alucard', 1000014, '2', 518, 50, 49999800, 90000002, 1003, 40, 9, 3, 29, 5, 0, 0, 216, 0, 1002, 303, 282, 0, 1470, 0, 1002, 'None', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 638758147875934424, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 314, 328, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, '32#0#0#0#524288#4096#0#2048#0#0#0#0#0#', NULL, NULL, 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 471, '', '', 0, '0-0~0-0', 10, 0, 0);
INSERT INTO `entities` VALUES ('testaccount', 1000015, '3', 714, 60, 100, 0, 1003, 12, 1, 12, 3, 12, 9, 0, 360, 45, 1010, 88, 46, 0, 0, 0, 1002, 'None', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, NULL, NULL, NULL, 0, 0, NULL, NULL, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', '', 0, NULL, 10, 0, 0);

-- ----------------------------
-- Table structure for flooritem
-- ----------------------------
DROP TABLE IF EXISTS `flooritem`;
CREATE TABLE `flooritem`  (
  `ID` bigint(255) UNSIGNED NOT NULL DEFAULT 0,
  `Type` bigint(255) UNSIGNED NOT NULL DEFAULT 0,
  `Color` bigint(255) UNSIGNED NOT NULL DEFAULT 0,
  `MapID` bigint(255) UNSIGNED NOT NULL DEFAULT 0,
  `X` bigint(255) UNSIGNED NOT NULL DEFAULT 0,
  `Y` bigint(255) UNSIGNED NOT NULL DEFAULT 0,
  `Comment` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of flooritem
-- ----------------------------
INSERT INTO `flooritem` VALUES (8, 11, 1, 4020, 72, 82, 'TowerOfMystery[4020]');
INSERT INTO `flooritem` VALUES (8, 11, 1, 4020, 71, 77, 'TowerOfMystery[4020]');
INSERT INTO `flooritem` VALUES (8, 11, 1, 4020, 73, 88, 'TowerOfMystery[4020]');
INSERT INTO `flooritem` VALUES (811, 11, 1, 4020, 73, 98, 'TowerOfMystery[4020]');

-- ----------------------------
-- Table structure for friends
-- ----------------------------
DROP TABLE IF EXISTS `friends`;
CREATE TABLE `friends`  (
  `EntityID` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `FriendID` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `FriendName` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `Message` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of friends
-- ----------------------------

-- ----------------------------
-- Table structure for furniture
-- ----------------------------
DROP TABLE IF EXISTS `furniture`;
CREATE TABLE `furniture`  (
  `id` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `NameOwner` varchar(16) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `type` int(4) UNSIGNED ZEROFILL NULL DEFAULT 0000,
  `lookface` int(4) UNSIGNED ZEROFILL NULL DEFAULT 0000,
  `mapid` int(4) UNSIGNED ZEROFILL NULL DEFAULT 0000,
  `cellx` int(4) UNSIGNED ZEROFILL NULL DEFAULT 0000,
  `celly` int(4) UNSIGNED ZEROFILL NULL DEFAULT 0000
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of furniture
-- ----------------------------

-- ----------------------------
-- Table structure for guild_arsenals
-- ----------------------------
DROP TABLE IF EXISTS `guild_arsenals`;
CREATE TABLE `guild_arsenals`  (
  `id` int(36) UNSIGNED NOT NULL AUTO_INCREMENT,
  `guild_uid` int(36) UNSIGNED NOT NULL DEFAULT 0,
  `head_allowed` int(36) NOT NULL,
  `armor_allowed` int(36) NOT NULL,
  `weapon_allowed` int(36) NOT NULL,
  `boots_allowed` int(36) NOT NULL,
  `neck_allowed` int(36) NOT NULL,
  `fan_allowed` int(36) NOT NULL,
  `tower_allowed` int(36) NOT NULL,
  `ring_allowed` int(36) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 36 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of guild_arsenals
-- ----------------------------

-- ----------------------------
-- Table structure for guild_arsenalsdonation
-- ----------------------------
DROP TABLE IF EXISTS `guild_arsenalsdonation`;
CREATE TABLE `guild_arsenalsdonation`  (
  `id` int(36) UNSIGNED NOT NULL AUTO_INCREMENT,
  `d_uid` int(36) UNSIGNED NOT NULL DEFAULT 0,
  `guild_uid` int(36) UNSIGNED NOT NULL DEFAULT 0,
  `name` varchar(36) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL DEFAULT 'None',
  `item_uid` int(36) UNSIGNED NOT NULL DEFAULT 0,
  `item_donation` int(36) UNSIGNED NOT NULL DEFAULT 0,
  `item_arsenal_type` int(36) UNSIGNED NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1327 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of guild_arsenalsdonation
-- ----------------------------

-- ----------------------------
-- Table structure for guildally
-- ----------------------------
DROP TABLE IF EXISTS `guildally`;
CREATE TABLE `guildally`  (
  `GuildID` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `AllyID` int(10) UNSIGNED NULL DEFAULT NULL
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of guildally
-- ----------------------------

-- ----------------------------
-- Table structure for guildarsenal
-- ----------------------------
DROP TABLE IF EXISTS `guildarsenal`;
CREATE TABLE `guildarsenal`  (
  `id` bigint(25) NOT NULL,
  `data` blob NULL,
  `datalength` bigint(255) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE INDEX `myIndex`(`id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of guildarsenal
-- ----------------------------
INSERT INTO `guildarsenal` VALUES (1007, 0x010134932900020000002057B68A56EFD308010000003B8A4C0041420F00B5B60100140000009AC914000C0D0D05456759436F02013493290002000000110B7F8956EFD308010000003A8A4C0041420F008D100200140000009AC914000C0D0D05456759436F03016826530002000000F8B96F8856EFD308020000008B854C0041420F00F7670900140000009AC914000C0D0D05456759436F8E854C0041420F00F7670900140000009AC914000C0D0D05456759436F04013493290002000000C78B628756EFD30801000000F0834C0041420F00D7520200140000009AC914000C030305456759436F05013493290002000000ABC4498656EFD308010000003D8A4C0041420F00F9710200140000009AC914000C0D0D05456759436F060134932900020000009D9F418556EFD308010000003E8A4C0041420F00CDD50100140000009AC914000C0D0D05456759436F07013493290002000000C6B41D8456EFD30801000000F3834C0041420F0031110300140000009AC914000C676705456759436F080134932900020000007B5A388356EFD30801000000F2834C0041420F0019150300140000009AC914000C7B7B05456759436F, 437);
INSERT INTO `guildarsenal` VALUES (1008, 0x010134932900020000002D781BCB79F2D30801000000B09D4C004A420F00CDB20100140000009AC914000C0D0D064D794D69646F02013493290002000000B63704CA79F2D30801000000B19D4C004A420F00ED000200140000009AC914000C0D0D064D794D69646F03016C06230102000000CAF4D1C879F2D30807000000AF9D4C004A420F00378D0800140000009AC914000C0D0D064D794D69646FB29D4C004A420F00D5BC0D00140000009AC914000C0D0D064D794D69646F379D4C004A420F0037870900140000009AC914000C0D0D064D794D69646F389D4C004A420F0037870900140000009AC914000C0D0D064D794D69646F399D4C004A420F0047860900140000009AC914000C0D0D064D794D69646F3A9D4C004A420F002D870900140000009AC914000C0D0D064D794D69646FB69D4C004A420F001F910800140000009AC914000C0D0D064D794D69646F04013493290002000000E69E2EC779F2D30801000000B39D4C004A420F00FD4A0200140000009AC914000C0D0D064D794D69646F05013493290002000000CD4D1FC679F2D30801000000B59D4C004A420F00F9710200140000009AC914000C0D0D064D794D69646F06013493290002000000CA1E09C579F2D30801000000B49D4C004A420F00CDD50100140000009AC914000C0D0D064D794D69646F070134932900020000008D09F0C379F2D30801000000B89D4C004A420F0031110300140000009AC914000C6767064D794D69646F0801349329000200000074DF0FC379F2D30801000000B79D4C004A420F0019150300140000009AC914000C7B7B064D794D69646F, 596);
INSERT INTO `guildarsenal` VALUES (1009, NULL, NULL);

-- ----------------------------
-- Table structure for guildenemy
-- ----------------------------
DROP TABLE IF EXISTS `guildenemy`;
CREATE TABLE `guildenemy`  (
  `GuildID` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `EnemyID` int(10) UNSIGNED NOT NULL DEFAULT 0
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of guildenemy
-- ----------------------------

-- ----------------------------
-- Table structure for guilds
-- ----------------------------
DROP TABLE IF EXISTS `guilds`;
CREATE TABLE `guilds`  (
  `ID` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `Name` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `Bulletin` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT 'This is a new guild.',
  `SilverFund` bigint(255) UNSIGNED NULL DEFAULT 500000,
  `ConquerPointFund` bigint(255) UNSIGNED NULL DEFAULT 0,
  `Wins` bigint(255) UNSIGNED NULL DEFAULT 0,
  `Losts` bigint(255) UNSIGNED NULL DEFAULT 0,
  `LeaderName` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '0',
  `LevelRequirement` int(4) UNSIGNED NULL DEFAULT 1,
  `RebornRequirement` int(4) UNSIGNED NULL DEFAULT 0,
  `ClassRequirement` int(4) UNSIGNED NULL DEFAULT 0,
  `CTFPoints` bigint(255) UNSIGNED NULL DEFAULT 0,
  `CTFdonationCPs` bigint(255) UNSIGNED NULL DEFAULT NULL,
  `CTFdonationSilver` bigint(255) UNSIGNED NULL DEFAULT 0,
  `CTFdonationSilverold` bigint(255) UNSIGNED NULL DEFAULT 0,
  `CTFdonationCPsold` bigint(255) NULL DEFAULT NULL,
  `CTFReward` bigint(255) UNSIGNED NULL DEFAULT 0,
  `EnrollmentDate` bigint(16) UNSIGNED NULL DEFAULT 0,
  `AnnouncementDate` bigint(16) NULL DEFAULT NULL,
  `AutoRecruit` int(4) NULL DEFAULT NULL,
  `adLevelRequirement` int(4) NULL DEFAULT NULL,
  `adRebornRequirement` int(4) NULL DEFAULT NULL,
  `adClassRequirement` int(4) NULL DEFAULT NULL,
  `PoleKeeper` int(1) NULL DEFAULT 0,
  `UnionID` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  PRIMARY KEY (`ID`) USING BTREE,
  UNIQUE INDEX `myIndex`(`ID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of guilds
-- ----------------------------
INSERT INTO `guilds` VALUES (1007, 'EgYCo', '', 1000499999, 50000000, 0, 0, 'EgYCo', 1, 0, 0, 0, 0, 0, 0, 0, 0, 20160908, NULL, NULL, NULL, NULL, NULL, 0, 0);
INSERT INTO `guilds` VALUES (1008, 'Prodgy', '', 999500000, 500000, 1, 0, 'MyMido', 1, 0, 0, 0, 0, 0, 0, 0, 0, 20160912, NULL, NULL, NULL, NULL, NULL, 1, 0);
INSERT INTO `guilds` VALUES (1009, 'Monarchs', '', 500000, 0, 0, 0, 'RAVEN', 1, 0, 0, 0, NULL, 0, 0, NULL, 0, 20250220, NULL, NULL, NULL, NULL, NULL, 0, 2003);

-- ----------------------------
-- Table structure for house
-- ----------------------------
DROP TABLE IF EXISTS `house`;
CREATE TABLE `house`  (
  `ID` int(4) UNSIGNED NOT NULL DEFAULT 0,
  `Map` int(4) UNSIGNED NOT NULL DEFAULT 0,
  `Type` int(4) UNSIGNED ZEROFILL NULL DEFAULT 0000,
  `Weather` int(4) UNSIGNED ZEROFILL NULL DEFAULT 0000,
  `Owner` int(4) NULL DEFAULT 0,
  `HouseLevel` int(4) NULL DEFAULT 0,
  `Name` varchar(16) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of house
-- ----------------------------
INSERT INTO `house` VALUES (1000013, 1098, 0000, 0000, 1000013, 1, 'RAVEN');

-- ----------------------------
-- Table structure for inner
-- ----------------------------
DROP TABLE IF EXISTS `inner`;
CREATE TABLE `inner`  (
  `UID` int(18) NOT NULL,
  `Name` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Powers` longblob NOT NULL,
  PRIMARY KEY (`UID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of inner
-- ----------------------------
INSERT INTO `inner` VALUES (1000001, 'EgYCo', 0x81841E0000000000);
INSERT INTO `inner` VALUES (1000010, 'MyMido', 0x90010000010000000100010200000001140101000200000000);
INSERT INTO `inner` VALUES (1000009, 'Mid0', 0x0000000000000000);
INSERT INTO `inner` VALUES (1000013, 'RAVEN', 0xA4060000010000000100010200000001140101000200000000);
INSERT INTO `inner` VALUES (1000014, 'Alucard', 0x0000000000000000);

-- ----------------------------
-- Table structure for itemadding
-- ----------------------------
DROP TABLE IF EXISTS `itemadding`;
CREATE TABLE `itemadding`  (
  `iditemadding` int(11) NOT NULL AUTO_INCREMENT,
  `uid` bigint(25) NULL DEFAULT NULL,
  `addingtype` bigint(25) NULL DEFAULT NULL,
  `addingid` bigint(25) NULL DEFAULT NULL,
  `addinglevel` bigint(25) NULL DEFAULT NULL,
  `addingpercent` bigint(25) NULL DEFAULT NULL,
  `duration` bigint(25) NULL DEFAULT NULL,
  `addedon` bigint(255) NULL DEFAULT NULL,
  `stabilizationpoints` bigint(25) NULL DEFAULT NULL,
  PRIMARY KEY (`iditemadding`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 180543 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of itemadding
-- ----------------------------
INSERT INTO `itemadding` VALUES (180463, 5000405, 0, 800111, 7, 0, 0, 636076816869055000, NULL);
INSERT INTO `itemadding` VALUES (180464, 5000411, 0, 800111, 7, 0, 0, 636076817076691000, NULL);
INSERT INTO `itemadding` VALUES (180467, 5000383, 0, 821033, 7, 0, 0, 636076818171811000, NULL);
INSERT INTO `itemadding` VALUES (180465, 5000388, 0, 820073, 7, 0, 0, 636076817308351000, NULL);
INSERT INTO `itemadding` VALUES (180466, 5000386, 0, 823059, 7, 0, 0, 636076818054031000, NULL);
INSERT INTO `itemadding` VALUES (180462, 5000063, 1, 724453, 5, 9, 604800, 636068828502176850, NULL);
INSERT INTO `itemadding` VALUES (180468, 5000384, 0, 824018, 7, 0, 0, 636076818686455000, NULL);
INSERT INTO `itemadding` VALUES (180469, 5001457, 0, 822071, 7, 0, 0, 636076819682983000, NULL);
INSERT INTO `itemadding` VALUES (180470, 5009234, 0, 800111, 6, 0, 0, 636083694395820312, NULL);
INSERT INTO `itemadding` VALUES (180471, 5009235, 0, 800111, 6, 0, 0, 636083694395820312, NULL);
INSERT INTO `itemadding` VALUES (180472, 5009236, 0, 822071, 6, 0, 0, 636083694395820312, NULL);
INSERT INTO `itemadding` VALUES (180473, 5009237, 0, 820076, 6, 0, 0, 636083694395820312, NULL);
INSERT INTO `itemadding` VALUES (180474, 5009238, 0, 821033, 6, 0, 0, 636083694395820312, NULL);
INSERT INTO `itemadding` VALUES (180475, 5009239, 0, 823059, 6, 0, 0, 636083694395820312, NULL);
INSERT INTO `itemadding` VALUES (180476, 5009240, 0, 824020, 6, 0, 0, 636083694395976562, NULL);
INSERT INTO `itemadding` VALUES (180477, 5010821, 0, 800522, 6, 0, 0, 636083839224248047, NULL);
INSERT INTO `itemadding` VALUES (180478, 5010822, 0, 822071, 6, 0, 0, 636083839224248047, NULL);
INSERT INTO `itemadding` VALUES (180479, 5010823, 0, 820076, 6, 0, 0, 636083839224248047, NULL);
INSERT INTO `itemadding` VALUES (180480, 5010824, 0, 823060, 6, 0, 0, 636083839224248047, NULL);
INSERT INTO `itemadding` VALUES (180481, 5010825, 0, 821034, 6, 0, 0, 636083839224248047, NULL);
INSERT INTO `itemadding` VALUES (180482, 5010826, 0, 824020, 6, 0, 0, 636083839224404297, NULL);
INSERT INTO `itemadding` VALUES (180483, 5012025, 0, 800522, 6, 0, 0, 636083893457099609, NULL);
INSERT INTO `itemadding` VALUES (180484, 5012026, 0, 822071, 6, 0, 0, 636083893457099609, NULL);
INSERT INTO `itemadding` VALUES (180485, 5012027, 0, 820076, 6, 0, 0, 636083893457255859, NULL);
INSERT INTO `itemadding` VALUES (180486, 5012028, 0, 823060, 6, 0, 0, 636083893457255859, NULL);
INSERT INTO `itemadding` VALUES (180487, 5012029, 0, 821034, 6, 0, 0, 636083893457255859, NULL);
INSERT INTO `itemadding` VALUES (180488, 5012030, 0, 824020, 6, 0, 0, 636083893457255859, NULL);
INSERT INTO `itemadding` VALUES (180489, 5012425, 0, 800522, 6, 0, 0, 636083896246630859, NULL);
INSERT INTO `itemadding` VALUES (180490, 5012426, 0, 801104, 6, 0, 0, 636083896246630859, NULL);
INSERT INTO `itemadding` VALUES (180491, 5012427, 0, 800522, 6, 0, 0, 636083896246630859, NULL);
INSERT INTO `itemadding` VALUES (180492, 5012428, 0, 822072, 6, 0, 0, 636083896246630859, NULL);
INSERT INTO `itemadding` VALUES (180493, 5012429, 0, 820076, 6, 0, 0, 636083896246787109, NULL);
INSERT INTO `itemadding` VALUES (180494, 5012430, 0, 823060, 6, 0, 0, 636083896246787109, NULL);
INSERT INTO `itemadding` VALUES (180495, 5012431, 0, 800522, 6, 0, 0, 636083896325068359, NULL);
INSERT INTO `itemadding` VALUES (180496, 5012432, 0, 801104, 6, 0, 0, 636083896325068359, NULL);
INSERT INTO `itemadding` VALUES (180497, 5012433, 0, 800522, 6, 0, 0, 636083896325068359, NULL);
INSERT INTO `itemadding` VALUES (180498, 5012434, 0, 822072, 6, 0, 0, 636083896325068359, NULL);
INSERT INTO `itemadding` VALUES (180499, 5012435, 0, 820076, 6, 0, 0, 636083896325068359, NULL);
INSERT INTO `itemadding` VALUES (180500, 5012436, 0, 823060, 6, 0, 0, 636083896325068359, NULL);
INSERT INTO `itemadding` VALUES (180501, 5012437, 0, 821034, 6, 0, 0, 636083896325068359, NULL);
INSERT INTO `itemadding` VALUES (180502, 5012438, 0, 824020, 6, 0, 0, 636083896325224609, NULL);
INSERT INTO `itemadding` VALUES (180503, 5013227, 0, 800111, 6, 0, 0, 636083923491308594, NULL);
INSERT INTO `itemadding` VALUES (180504, 5013228, 0, 800111, 6, 0, 0, 636083923491308594, NULL);
INSERT INTO `itemadding` VALUES (180505, 5013229, 0, 800725, 6, 0, 0, 636083923491308594, NULL);
INSERT INTO `itemadding` VALUES (180506, 5013230, 0, 800725, 6, 0, 0, 636083923491308594, NULL);
INSERT INTO `itemadding` VALUES (180507, 5013231, 0, 822071, 6, 0, 0, 636083923491308594, NULL);
INSERT INTO `itemadding` VALUES (180508, 5013232, 0, 820073, 6, 0, 0, 636083923491464844, NULL);
INSERT INTO `itemadding` VALUES (180509, 5013233, 0, 821033, 6, 0, 0, 636083923491464844, NULL);
INSERT INTO `itemadding` VALUES (180510, 5013234, 0, 823058, 6, 0, 0, 636083923491464844, NULL);
INSERT INTO `itemadding` VALUES (180511, 5013235, 0, 824018, 6, 0, 0, 636083923491464844, NULL);
INSERT INTO `itemadding` VALUES (180512, 5013236, 0, 800111, 6, 0, 0, 636083923520683594, NULL);
INSERT INTO `itemadding` VALUES (180513, 5013237, 0, 800111, 6, 0, 0, 636083923520683594, NULL);
INSERT INTO `itemadding` VALUES (180514, 5013238, 0, 800725, 6, 0, 0, 636083923520683594, NULL);
INSERT INTO `itemadding` VALUES (180515, 5013239, 0, 800725, 6, 0, 0, 636083923520683594, NULL);
INSERT INTO `itemadding` VALUES (180516, 5013240, 0, 822071, 6, 0, 0, 636083923520683594, NULL);
INSERT INTO `itemadding` VALUES (180517, 5013241, 0, 820073, 6, 0, 0, 636083923520683594, NULL);
INSERT INTO `itemadding` VALUES (180518, 5013242, 0, 821033, 6, 0, 0, 636083923520683594, NULL);
INSERT INTO `itemadding` VALUES (180519, 5013243, 0, 823058, 6, 0, 0, 636083923520839844, NULL);
INSERT INTO `itemadding` VALUES (180520, 5013244, 0, 824018, 6, 0, 0, 636083923520839844, NULL);
INSERT INTO `itemadding` VALUES (180521, 5013644, 0, 800142, 6, 0, 0, 636083927815058594, NULL);
INSERT INTO `itemadding` VALUES (180522, 5013645, 0, 800142, 6, 0, 0, 636083927815058594, NULL);
INSERT INTO `itemadding` VALUES (180523, 5013646, 0, 800111, 6, 0, 0, 636083927815058594, NULL);
INSERT INTO `itemadding` VALUES (180524, 5013647, 0, 800111, 6, 0, 0, 636083927815058594, NULL);
INSERT INTO `itemadding` VALUES (180525, 5013648, 0, 822071, 6, 0, 0, 636083927815058594, NULL);
INSERT INTO `itemadding` VALUES (180526, 5013649, 0, 820073, 6, 0, 0, 636083927815058594, NULL);
INSERT INTO `itemadding` VALUES (180527, 5013650, 0, 821033, 6, 0, 0, 636083927815214844, NULL);
INSERT INTO `itemadding` VALUES (180528, 5013651, 0, 823058, 6, 0, 0, 636083927815214844, NULL);
INSERT INTO `itemadding` VALUES (180529, 5013652, 0, 824018, 6, 0, 0, 636083927815214844, NULL);
INSERT INTO `itemadding` VALUES (180530, 5020983, 0, 801218, 7, 0, 0, 636092749667856904, NULL);
INSERT INTO `itemadding` VALUES (180531, 5020984, 0, 801218, 7, 0, 0, 636092750260950827, NULL);
INSERT INTO `itemadding` VALUES (180532, 5028584, 0, 800422, 7, 0, 0, 638756922754467611, NULL);
INSERT INTO `itemadding` VALUES (180533, 5028583, 0, 822071, 7, 0, 0, 638756922818952864, NULL);
INSERT INTO `itemadding` VALUES (180534, 5028579, 0, 821033, 7, 0, 0, 638756922896839003, NULL);
INSERT INTO `itemadding` VALUES (180535, 5028582, 0, 823058, 7, 0, 0, 638756923022256275, NULL);
INSERT INTO `itemadding` VALUES (180536, 5028586, 0, 820073, 7, 0, 0, 638756924010782457, NULL);
INSERT INTO `itemadding` VALUES (180537, 5028580, 0, 824018, 7, 0, 0, 638756924062400945, NULL);
INSERT INTO `itemadding` VALUES (180538, 5027929, 0, 801216, 7, 0, 0, 638756925353505009, NULL);
INSERT INTO `itemadding` VALUES (180539, 5028547, 0, 801216, 7, 0, 0, 638756925403565142, NULL);
INSERT INTO `itemadding` VALUES (180540, 5028587, 0, 800255, 7, 0, 0, 638757204020962531, NULL);
INSERT INTO `itemadding` VALUES (180541, 5035777, 0, 800917, 7, 0, 0, 638758134820998691, NULL);
INSERT INTO `itemadding` VALUES (180542, 5035780, 0, 800917, 7, 0, 0, 638758136267099373, NULL);

-- ----------------------------
-- Table structure for items
-- ----------------------------
DROP TABLE IF EXISTS `items`;
CREATE TABLE `items`  (
  `EntityID` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  `ID` mediumint(10) UNSIGNED NOT NULL DEFAULT 0,
  `UID` int(250) UNSIGNED NOT NULL DEFAULT 0,
  `Plus` smallint(5) UNSIGNED NULL DEFAULT 0,
  `Bless` smallint(5) UNSIGNED NULL DEFAULT 0,
  `Enchant` smallint(5) UNSIGNED NOT NULL DEFAULT 0,
  `SocketOne` smallint(5) UNSIGNED NULL DEFAULT 0,
  `SocketTwo` smallint(5) UNSIGNED NULL DEFAULT 0,
  `Durability` mediumint(50) UNSIGNED NOT NULL DEFAULT 0,
  `MaximDurability` mediumint(50) UNSIGNED NOT NULL DEFAULT 0,
  `SocketProgress` int(40) UNSIGNED NULL DEFAULT 0,
  `PlusProgress` int(40) UNSIGNED NULL DEFAULT 0,
  `Effect` smallint(5) UNSIGNED NULL DEFAULT 0,
  `Bound` smallint(5) UNSIGNED NULL DEFAULT 0,
  `Locked` smallint(5) UNSIGNED NULL DEFAULT 0,
  `UnlockEnd` bigint(250) UNSIGNED NULL DEFAULT 0,
  `Suspicious` smallint(5) UNSIGNED NULL DEFAULT 0,
  `SuspiciousStart` smallint(5) UNSIGNED NULL DEFAULT 0,
  `Color` smallint(5) UNSIGNED NULL DEFAULT 0,
  `Position` mediumint(10) UNSIGNED NULL DEFAULT 0,
  `Warehouse` bigint(255) UNSIGNED NULL DEFAULT 0,
  `StackSize` smallint(5) UNSIGNED NOT NULL DEFAULT 0,
  `Inscribed` int(36) UNSIGNED NOT NULL DEFAULT 0,
  `agate` text CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL,
  `RefineryStarted` bigint(36) NOT NULL DEFAULT 0,
  `RefineryPercent` int(36) NOT NULL DEFAULT 0,
  `RefineryLevel` int(36) NOT NULL DEFAULT 0,
  `RefineryItem` int(4) NOT NULL DEFAULT 0,
  `RefineryTime` bigint(16) NOT NULL DEFAULT 0,
  `NextSteedColor` int(16) NOT NULL DEFAULT 0,
  `InWardrobe` smallint(6) NOT NULL DEFAULT 0,
  `MinutesLeft` bigint(255) NOT NULL,
  `TimeStamp` bigint(255) NOT NULL,
  `PerfectionProgress` bigint(255) UNSIGNED NOT NULL DEFAULT 0,
  `Stars` bigint(255) UNSIGNED NOT NULL DEFAULT 0,
  `Owner` varchar(16) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `OwnerUID` bigint(255) UNSIGNED NOT NULL DEFAULT 0,
  `Signature` varchar(16) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`UID`) USING BTREE,
  UNIQUE INDEX `myIndex`(`UID`) USING BTREE,
  INDEX `myIndex2`(`EntityID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of items
-- ----------------------------
INSERT INTO `items` VALUES (1000008, 410301, 5014436, 0, 0, 0, 0, 0, 1099, 1099, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 203009, 5014918, 12, 7, 255, 0, 0, 6698, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 7, 18, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 204009, 5014917, 12, 7, 255, 103, 123, 6698, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 6, 19, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 730003, 5014916, 3, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 723725, 5014915, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 2100075, 5014919, 0, 7, 255, 0, 0, 10000, 10000, 0, 0, 0, 0, 0, 0, 0, 0, 6, 7, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 160214, 5016136, 0, 0, 0, 0, 0, 200, 4098, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 711504, 5014478, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 14, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 181725, 5014479, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 181345, 5014480, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 723724, 5014481, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 181405, 5014482, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 1088001, 5014486, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 150118, 5014487, 0, 0, 0, 255, 0, 4099, 4099, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 181365, 5014488, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 730002, 5014489, 2, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 1088001, 5014490, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000007, 202009, 5014491, 12, 1, 255, 123, 123, 6698, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000007, 201009, 5014492, 12, 7, 255, 103, 103, 6698, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 723711, 5014493, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000007, 120269, 5014494, 12, 7, 255, 13, 13, 4198, 4198, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 723711, 5014495, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 510088, 5014496, 6, 0, 0, 0, 0, 3599, 3599, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 1088001, 5014497, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000007, 160249, 5014498, 12, 7, 255, 13, 13, 4098, 4098, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 181505, 5014499, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000007, 150269, 5014500, 12, 7, 255, 13, 13, 7099, 7099, 0, 0, 0, 0, 0, 0, 0, 0, 4, 6, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000007, 620439, 5014501, 12, 7, 255, 13, 13, 7099, 7099, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 3002030, 5018853, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 5, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000007, 610389, 5014503, 12, 7, 255, 13, 13, 7099, 7099, 0, 0, 0, 0, 0, 0, 0, 0, 5, 5, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000007, 610389, 5014504, 12, 7, 255, 13, 13, 7099, 7099, 0, 0, 0, 0, 0, 0, 0, 0, 5, 4, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000001, 204009, 5014505, 12, 7, 255, 103, 123, 6698, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 6, 19, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'Sokr\0\0\0\0\0\0\0\0\0\0\0\0', 1000001, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000007, 136309, 5014506, 12, 7, 255, 13, 13, 4198, 4198, 0, 0, 0, 0, 0, 0, 0, 0, 5, 3, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000007, 143309, 5014507, 12, 7, 255, 13, 13, 4198, 4198, 0, 0, 0, 0, 0, 0, 0, 0, 7, 1, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000001, 2100075, 5014508, 0, 7, 255, 0, 0, 10000, 10000, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000001, 203009, 5014509, 12, 7, 255, 0, 0, 6698, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 5, 18, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'Sokr\0\0\0\0\0\0\0\0\0\0\0\0', 1000001, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000007, 134309, 5014510, 12, 7, 255, 3, 3, 4198, 4198, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000007, 114309, 5014511, 12, 7, 255, 3, 3, 4198, 4198, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000001, 152279, 5014512, 12, 7, 255, 3, 3, 6599, 6599, 0, 0, 0, 0, 0, 0, 0, 0, 5, 6, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'Sokr\0\0\0\0\0\0\0\0\0\0\0\0', 1000001, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000007, 121269, 5014513, 12, 7, 255, 3, 3, 4098, 4098, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000001, 202009, 5014514, 12, 7, 255, 123, 123, 6698, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 5, 11, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'Sokr\0\0\0\0\0\0\0\0\0\0\0\0', 1000001, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000001, 201009, 5014515, 12, 7, 255, 103, 103, 6698, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 7, 10, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'Sokr\0\0\0\0\0\0\0\0\0\0\0\0', 1000001, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000010, 203009, 5021113, 12, 0, 0, 0, 0, 6698, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 9, 18, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'MyMido\0\0\0\0\0\0\0\0\0\0', 1000010, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000010, 201009, 5021112, 12, 0, 0, 103, 103, 6698, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 9, 10, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'MyMido\0\0\0\0\0\0\0\0\0\0', 1000010, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000008, 181725, 5014920, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 202009, 5014921, 12, 7, 255, 123, 123, 6698, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 4, 11, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 201009, 5014922, 12, 7, 255, 103, 103, 6698, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 4, 10, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000001, 616439, 5014923, 12, 7, 255, 13, 13, 7099, 7099, 0, 0, 0, 0, 1, 0, 0, 0, 6, 5, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'Sokr\0\0\0\0\0\0\0\0\0\0\0\0', 1000001, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000008, 120269, 5014924, 12, 7, 255, 13, 13, 4198, 4198, 0, 0, 0, 0, 0, 0, 0, 0, 5, 2, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 160249, 5014925, 12, 7, 255, 13, 13, 4098, 4098, 0, 0, 0, 0, 0, 0, 0, 0, 4, 8, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000001, 616439, 5014926, 12, 7, 255, 13, 13, 7099, 7099, 0, 0, 0, 0, 1, 0, 0, 0, 4, 4, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'Sokr\0\0\0\0\0\0\0\0\0\0\0\0', 1000001, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000008, 150269, 5014927, 12, 7, 255, 13, 1, 7099, 7099, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 150269, 5014928, 12, 7, 255, 13, 13, 7099, 7099, 0, 0, 0, 0, 0, 0, 0, 0, 4, 6, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 151249, 5014929, 12, 7, 255, 13, 13, 7698, 7698, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 410439, 5014930, 12, 7, 255, 13, 13, 7099, 7099, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 130309, 5014931, 12, 7, 255, 13, 13, 4198, 4198, 0, 0, 0, 0, 0, 0, 0, 0, 7, 3, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 118309, 5014932, 12, 7, 255, 13, 13, 4198, 4198, 0, 0, 0, 0, 0, 0, 0, 0, 6, 1, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 614429, 5015327, 12, 7, 215, 13, 13, 7099, 7099, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 614429, 5015328, 12, 7, 215, 13, 13, 7099, 7099, 0, 0, 0, 0, 0, 0, 0, 0, 4, 5, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000007, 200453, 5016119, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 1, 0, 0, 0, 0, 0, 17, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 7200, 636083956603955078, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000001, 135309, 5016122, 12, 7, 255, 13, 13, 4198, 4198, 0, 0, 0, 0, 0, 0, 0, 0, 7, 3, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'Sokr\0\0\0\0\0\0\0\0\0\0\0\0', 1000001, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000001, 112309, 5016123, 12, 7, 255, 13, 13, 4198, 4198, 0, 0, 0, 0, 0, 0, 0, 0, 5, 1, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'Sokr\0\0\0\0\0\0\0\0\0\0\0\0', 1000001, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000010, 202009, 5021111, 12, 0, 0, 123, 123, 6698, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 9, 11, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'MyMido\0\0\0\0\0\0\0\0\0\0', 1000010, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000001, 160249, 5016125, 12, 7, 255, 13, 13, 4098, 4098, 0, 0, 0, 0, 0, 0, 0, 0, 7, 8, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'Sokr\0\0\0\0\0\0\0\0\0\0\0\0', 1000001, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000001, 120269, 5016126, 12, 7, 255, 13, 13, 4198, 4198, 0, 0, 0, 0, 0, 0, 0, 0, 5, 2, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'Sokr\0\0\0\0\0\0\0\0\0\0\0\0', 1000001, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000001, 300000, 5016127, 12, 7, 255, 0, 0, 100, 100, 9739401, 0, 0, 0, 0, 0, 0, 0, 5, 12, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 9739401, 0, 0, 1, 0, 54, 'Sokr\0\0\0\0\0\0\0\0\0\0\0\0', 1000001, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000007, 300000, 5016128, 12, 7, 255, 0, 0, 100, 100, 9739401, 0, 0, 0, 0, 0, 0, 0, 4, 12, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000007, 202009, 5016129, 12, 7, 255, 123, 123, 6698, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 6, 11, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000007, 201009, 5016130, 12, 7, 255, 103, 103, 6698, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 4, 10, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000007, 160249, 5016131, 12, 7, 255, 13, 13, 4098, 4098, 0, 0, 0, 0, 0, 0, 0, 0, 5, 8, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000007, 120269, 5016132, 12, 7, 255, 13, 13, 4198, 4198, 0, 0, 0, 0, 0, 0, 0, 0, 4, 2, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000007, 150269, 5016134, 12, 7, 255, 13, 13, 7099, 7099, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000007, 203009, 5016135, 12, 7, 255, 0, 0, 6698, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 5, 18, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000007, 204009, 5016137, 12, 7, 255, 103, 123, 6698, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 5, 19, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 193525, 5015763, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 193445, 5015762, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 192665, 5015799, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 192685, 5015801, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 192675, 5015800, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 3008727, 5016145, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 2, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 3002030, 5016146, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 4, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 150269, 5021107, 12, 7, 240, 13, 13, 7099, 7099, 0, 0, 0, 0, 0, 0, 0, 0, 9, 6, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'MyMido\0\0\0\0\0\0\0\0\0\0', 1000010, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000008, 192655, 5016148, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 9, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 192665, 5016149, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000008, 192675, 5016150, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 131309, 5021105, 12, 7, 240, 13, 13, 4198, 4198, 0, 0, 0, 0, 0, 0, 0, 0, 9, 3, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'MyMido\0\0\0\0\0\0\0\0\0\0', 1000010, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000010, 111309, 5021104, 12, 7, 240, 13, 13, 4198, 4198, 0, 0, 0, 0, 0, 0, 0, 0, 9, 1, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'MyMido\0\0\0\0\0\0\0\0\0\0', 1000010, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000010, 624439, 5020984, 12, 7, 255, 13, 13, 7099, 7099, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'MyMido\0\0\0\0\0\0\0\0\0\0', 1000010, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000010, 624439, 5020983, 12, 7, 255, 13, 13, 7099, 7099, 0, 0, 0, 0, 0, 0, 0, 0, 5, 5, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'MyMido\0\0\0\0\0\0\0\0\0\0', 1000010, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000009, 723017, 5018854, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 3, 0, 0, 8, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 1060020, 5018857, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 3, 0, 0, 3, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 120269, 5021108, 12, 7, 240, 13, 13, 4198, 4198, 0, 0, 0, 0, 0, 0, 0, 0, 9, 2, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'MyMido\0\0\0\0\0\0\0\0\0\0', 1000010, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000009, 721626, 5018860, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 1060023, 5018861, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 3, 0, 0, 2, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 160249, 5021109, 12, 7, 240, 13, 13, 4098, 4098, 0, 0, 0, 0, 0, 0, 0, 0, 9, 8, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'MyMido\0\0\0\0\0\0\0\0\0\0', 1000010, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000009, 729304, 5019411, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000001, 152289, 5019235, 12, 7, 255, 13, 13, 6599, 6599, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 132023, 5021955, 0, 0, 0, 0, 0, 200, 3599, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 300000, 5022016, 12, 7, 255, 0, 0, 100, 100, 9316142, 0, 0, 0, 0, 0, 0, 0, 7, 12, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 9316142, 0, 0, 1, 0, 54, 'MyMido\0\0\0\0\0\0\0\0\0\0', 1000010, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000009, 1080001, 5018873, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000001, 121279, 5019233, 12, 7, 255, 13, 13, 4098, 4098, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 700012, 5018876, 0, 0, 0, 0, 0, 199, 199, 0, 0, 0, 1, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000001, 121279, 5019232, 12, 7, 255, 13, 13, 4098, 4098, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 201008, 5018883, 0, 0, 0, 0, 0, 6698, 6698, 0, 0, 0, 1, 0, 0, 0, 0, 3, 10, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 300000, 5018887, 1, 0, 0, 0, 0, 100, 100, 9830655, 0, 100, 0, 0, 0, 0, 0, 0, 12, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 9830655, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 617178, 5018888, 0, 0, 0, 0, 0, 5698, 5698, 0, 0, 0, 1, 0, 0, 0, 0, 3, 5, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 138078, 5018889, 0, 0, 0, 0, 0, 4098, 4098, 0, 0, 0, 1, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 148108, 5018890, 3, 0, 0, 0, 0, 4198, 4198, 0, 0, 0, 1, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 150178, 5018891, 0, 0, 0, 0, 0, 5599, 5599, 0, 0, 0, 1, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 120188, 5018892, 0, 0, 0, 0, 0, 4198, 4198, 0, 0, 0, 1, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 160178, 5018893, 0, 0, 0, 0, 0, 4098, 4098, 0, 0, 0, 1, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 617117, 5018895, 0, 0, 0, 0, 0, 4198, 4198, 0, 0, 0, 1, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 202008, 5018907, 0, 0, 0, 0, 0, 6698, 6698, 0, 0, 0, 1, 0, 0, 0, 0, 3, 11, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 204007, 5018912, 0, 0, 0, 0, 0, 6698, 6698, 0, 0, 0, 1, 0, 0, 0, 0, 3, 19, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 753009, 5018913, 0, 0, 0, 0, 0, 9, 9, 0, 0, 0, 1, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 753003, 5018914, 0, 0, 0, 0, 0, 3, 3, 0, 0, 0, 1, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 753003, 5018915, 0, 0, 0, 0, 0, 3, 3, 0, 0, 0, 1, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 721090, 5018916, 0, 0, 0, 0, 0, 100, 100, 0, 0, 0, 1, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 1088001, 5018922, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 1088001, 5018923, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 1088001, 5018924, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 723700, 5018925, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 191305, 5018926, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 1, 0, 0, 0, 0, 3, 9, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 721259, 5018927, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 1200000, 5018928, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 617219, 5018930, 0, 0, 0, 0, 0, 7198, 7198, 0, 0, 0, 1, 0, 0, 0, 0, 3, 4, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 138099, 5018931, 0, 0, 0, 0, 0, 4098, 4098, 0, 0, 0, 1, 0, 0, 0, 0, 3, 3, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 148099, 5018932, 0, 0, 0, 0, 0, 4198, 4198, 0, 10, 0, 1, 0, 0, 0, 0, 3, 1, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 150219, 5018933, 0, 0, 0, 0, 0, 7099, 7099, 0, 0, 0, 1, 0, 0, 0, 0, 3, 6, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 120219, 5018934, 0, 0, 0, 0, 0, 4198, 4198, 0, 0, 0, 1, 0, 0, 0, 0, 3, 2, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 160219, 5018935, 0, 0, 0, 0, 0, 4098, 4098, 0, 0, 0, 1, 0, 0, 0, 0, 3, 8, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 203003, 5018936, 0, 0, 0, 0, 0, 300, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000009, 1060021, 5018939, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 204009, 5021114, 12, 1, 0, 103, 123, 6698, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 9, 19, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'MyMido\0\0\0\0\0\0\0\0\0\0', 1000010, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000001, 721094, 5021833, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 3002030, 5021830, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 3, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 3008727, 5021828, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 3, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 200438, 5022037, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 17, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 3006984, 5022031, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 3006984, 5022027, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 3006984, 5022026, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 723087, 5022015, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 6, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000001, 3003228, 5021834, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000001, 3001406, 5021835, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000001, 188755, 5021807, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000001, 192695, 5021804, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000001, 624043, 5021838, 0, 0, 0, 0, 0, 2698, 2698, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 1060021, 5023492, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 3005366, 5022594, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 3005395, 5022596, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 3005394, 5022595, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 3005395, 5022604, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 3005400, 5022601, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 3005398, 5022599, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 3005398, 5022600, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 3005396, 5022597, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 3005397, 5022598, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 1088001, 5022235, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 1088001, 5022234, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 1088001, 5022233, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 1088001, 5022232, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 1088001, 5022231, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 1088001, 5022230, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 1088001, 5022229, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 1088001, 5022228, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 1088001, 5022227, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000010, 1088001, 5022226, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 624009, 5028298, 12, 0, 0, 0, 0, 1599, 1599, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 624009, 5028299, 12, 0, 0, 0, 0, 1599, 1599, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 204009, 5028301, 12, 0, 0, 0, 0, 6698, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 3, 19, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'RAVEN\0\0\0\0\0\0\0\0\0\0\0', 1000013, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000013, 203009, 5028302, 12, 0, 0, 0, 0, 6698, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 202009, 5028303, 12, 0, 0, 0, 0, 6698, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 201009, 5028304, 12, 0, 0, 0, 0, 6698, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 3, 10, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'RAVEN\0\0\0\0\0\0\0\0\0\0\0', 1000013, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000013, 2100025, 5028305, 0, 0, 0, 0, 0, 10000, 10000, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 300000, 5028577, 12, 7, 255, 0, 0, 100, 100, 9316142, 0, 0, 0, 0, 0, 0, 0, 5, 12, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 9316142, 0, 0, 1, 0, 54, 'RAVEN\0\0\0\0\0\0\0\0\0\0\0', 1000013, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000013, 3001406, 5028306, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 300000, 5028308, 6, 0, 0, 0, 0, 100, 100, 65430, 0, 100, 0, 0, 0, 0, 0, 0, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 65430, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 194300, 5028309, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 622439, 5027928, 6, 7, 240, 13, 13, 7099, 7099, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 622439, 5028545, 6, 7, 240, 13, 13, 7099, 7099, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 624339, 5027929, 12, 7, 240, 13, 13, 7099, 7099, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 624339, 5028547, 12, 7, 240, 13, 13, 7099, 7099, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 614439, 5027927, 6, 7, 240, 13, 13, 7099, 7099, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 614439, 5028549, 6, 7, 240, 13, 13, 7099, 7099, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003423, 5055376, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 616439, 5028551, 9, 7, 240, 13, 13, 7099, 7099, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 619439, 5027925, 6, 7, 240, 13, 13, 7099, 7099, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 620439, 5027924, 6, 7, 240, 13, 13, 7099, 7099, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 410077, 5028557, 0, 0, 0, 0, 0, 3099, 3099, 0, 0, 200, 0, 0, 0, 0, 0, 0, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000014, 3000550, 5036872, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 410077, 5028559, 0, 0, 0, 0, 0, 3099, 3099, 0, 0, 200, 0, 0, 0, 0, 0, 0, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 192555, 5028016, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 193525, 5051851, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 820073, 5028271, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (0, 1088001, 5068175, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 202009, 5028571, 12, 1, 0, 123, 123, 6698, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 6, 11, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'RAVEN\0\0\0\0\0\0\0\0\0\0\0', 1000013, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000013, 203009, 5028572, 12, 7, 255, 0, 0, 6698, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 203009, 5028573, 12, 1, 255, 0, 0, 6698, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 203009, 5028574, 12, 1, 0, 0, 0, 6698, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 6, 18, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'RAVEN\0\0\0\0\0\0\0\0\0\0\0', 1000013, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000013, 2100075, 5028576, 0, 1, 0, 0, 0, 10000, 10000, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 200410, 5028578, 0, 7, 255, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 120269, 5028579, 12, 7, 255, 13, 13, 4198, 4198, 0, 0, 0, 0, 0, 0, 0, 0, 6, 2, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'RAVEN\0\0\0\0\0\0\0\0\0\0\0', 1000013, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000013, 160249, 5028580, 12, 7, 255, 13, 13, 4098, 4098, 0, 0, 0, 0, 0, 0, 0, 0, 7, 8, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'RAVEN\0\0\0\0\0\0\0\0\0\0\0', 1000013, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000013, 117309, 5028581, 12, 7, 255, 13, 0, 4198, 4198, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 150269, 5028582, 12, 7, 255, 13, 13, 7099, 7099, 0, 0, 0, 0, 0, 0, 0, 0, 5, 6, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'RAVEN\0\0\0\0\0\0\0\0\0\0\0', 1000013, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000013, 131309, 5028583, 12, 7, 255, 13, 13, 4198, 4198, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 900309, 5028584, 12, 7, 255, 13, 13, 3798, 3798, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 561439, 5028585, 12, 7, 255, 13, 13, 7099, 7099, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 111309, 5028586, 12, 7, 255, 13, 13, 4198, 4198, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 511019, 5028587, 12, 7, 251, 13, 13, 1599, 1599, 5, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 723342, 5054175, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1000013, 410, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 720027, 5067776, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1000013, 3, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 188155, 5051086, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 188355, 5055372, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003641, 5055373, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000014, 601301, 5036874, 0, 0, 0, 0, 0, 1198, 1198, 0, 0, 0, 0, 0, 0, 0, 0, 5, 4, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3002030, 5028601, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 15, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 188295, 5055371, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 188285, 5055370, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 187475, 5055369, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3008727, 5062329, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003125, 5055353, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 20, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3004816, 5055354, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 728829, 5055355, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3000929, 5055356, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3004525, 5055357, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3004526, 5055358, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003419, 5055359, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003425, 5055360, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003976, 5055361, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 194300, 5055362, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 44, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 192645, 5055363, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 183305, 5055364, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 183395, 5055365, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 184325, 5055366, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 184375, 5055367, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 725065, 5062328, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 2, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 725065, 5062327, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 5, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003642, 5055378, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 725065, 5062325, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 5, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 133285, 5054578, 0, 0, 0, 0, 0, 400, 4198, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 728821, 5054965, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 728820, 5054966, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3006025, 5055349, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 720650, 5055350, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3005477, 5055351, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 44, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3000475, 5054964, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3002030, 5062304, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 30, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 729304, 5054573, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 7, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3008727, 5054574, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 9, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3000478, 5055377, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 200494, 5053393, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 160238, 5048029, 0, 0, 0, 0, 0, 400, 4098, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 200427, 5053394, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 1088000, 5039599, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 1088001, 5039214, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 490003, 5038827, 0, 0, 0, 0, 0, 1099, 1099, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 490003, 5038826, 0, 0, 0, 0, 0, 1099, 1099, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 1088000, 5039212, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 1088000, 5037672, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 1088000, 5037671, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 725015, 5038822, 0, 0, 0, 0, 0, 100, 100, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 1088000, 5037669, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 1088000, 5037670, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 613053, 5037667, 0, 0, 0, 0, 0, 3099, 3099, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 511004, 5037665, 0, 0, 0, 0, 0, 1599, 1599, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 613053, 5037666, 0, 0, 0, 0, 0, 3099, 3099, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000014, 203009, 5036878, 12, 1, 0, 0, 0, 6698, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000014, 616439, 5036876, 6, 7, 240, 13, 13, 7099, 7099, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003412, 5055374, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003412, 5055375, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 193095, 5054569, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 193065, 5054568, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 193055, 5054567, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3008994, 5036868, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3002926, 5036865, 0, 0, 0, 0, 0, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 2, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003125, 5036866, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 2, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 193235, 5054564, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 192745, 5054565, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 193045, 5054566, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (0, 1088001, 5068177, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (0, 1088001, 5068174, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 193305, 5054559, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 193235, 5054560, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 193105, 5054561, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 193075, 5054563, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 193765, 5054556, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 193775, 5054557, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 193085, 5054562, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (0, 1088001, 5068173, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (0, 1088001, 5068172, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (0, 1088001, 5068171, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (0, 1088001, 5068167, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 720027, 5068178, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (0, 1088001, 5068166, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (0, 1088001, 5068162, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 1088001, 5068161, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 1088001, 5068160, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 1088001, 5068159, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 1088001, 5068158, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 1088001, 5068156, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 1088001, 5068155, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (0, 1088001, 5068154, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (0, 1088001, 5068153, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (0, 1088001, 5068152, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 720027, 5068179, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (0, 1088001, 5068151, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 193755, 5054555, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 193805, 5054554, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 193795, 5054553, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 193785, 5054552, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 2100095, 5053405, 0, 7, 255, 0, 0, 10000, 10000, 0, 0, 0, 0, 0, 0, 0, 0, 7, 7, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 200490, 5053404, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 200492, 5053403, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 200493, 5053402, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3002030, 5035980, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 9, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 800720, 5068157, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 200531, 5068061, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (0, 1088001, 5068170, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (0, 1088001, 5068169, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (0, 1088001, 5068168, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 200482, 5068013, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 0, 17, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (0, 1088001, 5068165, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (0, 1088001, 5068164, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 561339, 5068150, 7, 0, 0, 0, 0, 7099, 5599, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 203007, 5068149, 0, 0, 0, 0, 0, 400, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 725065, 5065070, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 4, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 725065, 5065069, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 5, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 725065, 5065068, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 5, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 725065, 5065067, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 5, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 725065, 5065066, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 5, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 160198, 5065475, 0, 0, 0, 0, 0, 400, 4098, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (0, 1088001, 5068163, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 725065, 5065065, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 5, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 200543, 5068000, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 200544, 5068001, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 193115, 5067962, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 800720, 5068096, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 193555, 5067864, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 360186, 5067832, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 0, 16, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 360176, 5067830, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003907, 5064290, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003915, 5064289, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003912, 5064288, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003909, 5064287, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003910, 5064286, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003911, 5064285, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003913, 5064284, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003914, 5064283, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003916, 5064282, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003917, 5064281, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003906, 5064280, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003907, 5064279, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003908, 5064278, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003903, 5064277, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003905, 5064276, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003904, 5064275, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003902, 5064274, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 202009, 5064270, 4, 1, 0, 123, 0, 6698, 6698, 0, 0, 0, 1, 0, 0, 0, 0, 5, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 201009, 5064271, 4, 1, 0, 103, 0, 6698, 6698, 0, 0, 0, 1, 0, 0, 0, 0, 5, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 203009, 5064272, 4, 1, 0, 0, 0, 6698, 6698, 0, 0, 0, 1, 0, 0, 0, 0, 5, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003901, 5064273, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003640, 5063906, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3004818, 5063904, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 44, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003904, 5063903, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 44, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 203009, 5063902, 6, 1, 0, 123, 123, 6698, 6698, 0, 0, 0, 1, 0, 0, 0, 0, 5, 0, 44, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003934, 5063886, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003935, 5063892, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3006025, 5060011, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003311, 5060010, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 729304, 5060008, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3002307, 5060006, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003935, 5063888, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003929, 5063900, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 725065, 5062322, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 3, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 725065, 5062321, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 5, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3008747, 5059998, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 2, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 200442, 5062332, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3008747, 5059997, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 8, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003932, 5063898, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003905, 5063899, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 44, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003908, 5063897, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 44, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003907, 5063901, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003967, 5063893, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 44, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003902, 5063894, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 44, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003964, 5063895, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003928, 5063879, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003934, 5063891, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003932, 5063884, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003935, 5063885, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003933, 5063887, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 202009, 5062722, 4, 0, 0, 123, 0, 6698, 6698, 0, 0, 0, 1, 0, 0, 0, 0, 5, 0, 44, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 202009, 5062723, 4, 0, 0, 123, 0, 6698, 6698, 0, 0, 0, 1, 0, 0, 0, 0, 5, 0, 44, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 725065, 5062326, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 5, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 202009, 5064266, 6, 1, 0, 123, 123, 6698, 6698, 0, 0, 0, 1, 0, 0, 0, 0, 5, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 203009, 5064264, 6, 1, 0, 0, 0, 6698, 6698, 0, 0, 0, 1, 0, 0, 0, 0, 5, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 200495, 5053400, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 200413, 5053399, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 200407, 5053398, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 200411, 5053397, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 200403, 5053396, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 187355, 5050703, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3002030, 5036851, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 26, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 200418, 5053395, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 200527, 5053392, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 184315, 5052244, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 192745, 5052243, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 9, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 193525, 5052242, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 193445, 5052241, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 192745, 5052240, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 189035, 5052239, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 189065, 5052238, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 188915, 5052236, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 193625, 5052235, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 192695, 5052234, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 203007, 5043060, 0, 0, 0, 0, 0, 300, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 160054, 5043059, 0, 0, 0, 0, 0, 400, 3099, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 131274, 5043058, 0, 0, 0, 0, 0, 400, 4198, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000014, 132013, 5036873, 0, 0, 0, 0, 0, 3599, 3599, 0, 0, 0, 0, 0, 0, 0, 0, 5, 3, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 160050, 5043057, 0, 0, 0, 0, 0, 300, 3099, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 160015, 5043056, 0, 0, 0, 0, 0, 400, 1599, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 720027, 5036813, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 1088001, 5043055, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 160096, 5043054, 0, 0, 0, 0, 0, 200, 4098, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 135115, 5043053, 0, 0, 0, 0, 0, 400, 4198, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 120246, 5043052, 0, 0, 0, 0, 0, 300, 4198, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 204003, 5043051, 0, 0, 0, 0, 0, 300, 6698, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 1088000, 5043826, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 730003, 5043049, 3, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 3, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 729090, 5043050, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 1000013, 2, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 160158, 5043825, 0, 0, 0, 0, 0, 400, 4098, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 136243, 5043047, 0, 0, 0, 0, 0, 100, 4198, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 200110, 5036390, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 200109, 5036388, 0, 0, 0, 0, 0, 65535, 65535, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 511016, 5037664, 0, 0, 0, 0, 0, 1599, 1599, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003934, 5063889, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003933, 5063890, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003928, 5063882, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003930, 5063883, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003929, 5063880, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003932, 5063881, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003934, 5063896, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3003901, 5063905, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 201009, 5064262, 6, 1, 0, 103, 103, 6698, 6698, 0, 0, 0, 1, 0, 0, 0, 0, 5, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (0, 1088001, 5068176, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 114016, 5043043, 0, 0, 0, 0, 0, 200, 2599, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 160036, 5043044, 0, 0, 0, 0, 0, 200, 2099, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 1000013, 1, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 720598, 5036366, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 500429, 5035784, 6, 7, 240, 13, 13, 7099, 7099, 0, 0, 0, 1, 0, 0, 0, 0, 9, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 160249, 5035783, 6, 7, 240, 13, 13, 4098, 4098, 0, 0, 0, 1, 0, 0, 0, 0, 9, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 120269, 5035782, 6, 7, 240, 13, 13, 4198, 4198, 0, 0, 0, 1, 0, 0, 0, 0, 9, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 150269, 5035781, 6, 7, 240, 13, 13, 7099, 7099, 0, 0, 0, 1, 0, 0, 0, 0, 9, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 613429, 5035780, 12, 7, 240, 13, 13, 7099, 7099, 0, 0, 0, 1, 0, 0, 0, 0, 9, 4, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'RAVEN\0\0\0\0\0\0\0\0\0\0\0', 1000013, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000013, 133309, 5035779, 6, 7, 240, 13, 13, 4198, 4198, 0, 0, 0, 1, 0, 0, 0, 0, 9, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 113309, 5035778, 12, 7, 240, 13, 13, 4198, 4198, 0, 0, 0, 1, 0, 0, 0, 0, 9, 1, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'RAVEN\0\0\0\0\0\0\0\0\0\0\0', 1000013, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000013, 133309, 5035398, 12, 7, 255, 13, 13, 4198, 4198, 0, 0, 0, 0, 0, 0, 0, 0, 6, 3, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'RAVEN\0\0\0\0\0\0\0\0\0\0\0', 1000013, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000013, 500429, 5035397, 12, 7, 255, 13, 13, 7099, 7099, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 613429, 5035777, 12, 7, 240, 13, 13, 7099, 7099, 0, 0, 0, 1, 0, 0, 0, 0, 9, 5, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 54, 'RAVEN\0\0\0\0\0\0\0\0\0\0\0', 1000013, '\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0');
INSERT INTO `items` VALUES (1000013, 500429, 5035396, 12, 7, 255, 13, 13, 7099, 7099, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 3008727, 5036850, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1000013, 6, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');
INSERT INTO `items` VALUES (1000013, 822071, 5036737, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1000013, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, '', 0, '');

-- ----------------------------
-- Table structure for jiang
-- ----------------------------
DROP TABLE IF EXISTS `jiang`;
CREATE TABLE `jiang`  (
  `UID` int(18) NOT NULL,
  `OwnName` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `CustomizedName` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Powers` longblob NOT NULL
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of jiang
-- ----------------------------
INSERT INTO `jiang` VALUES (1000010, 'MyMido', 'fgdg', 0x000309520A0000000068720200B8290000000001010D06010D06010D06010D06010D06010D06010D06010D06010D0601010C06010C06010C06010C06010C06010C06010C06010C06010C060101020601020601020601020601020601020601020601020601020601010A06010A06010A06010A06010A06010A06010A06010A06010A060101010601010601010601010601010601010601010601010601010601010C06010C06010C06010C06010C06010C06010C06010C06010C060101020601020601020601020601020601020601020601020601020601010A06010A06010A06010A06010A06010A06010A06010A06010A0601010106010106010106010106010106010106010106010106010106);
INSERT INTO `jiang` VALUES (1000013, 'RAVEN', 'RAVEN', 0x8C0209520A000000005AA703000A410000310001010D06010D06010D06010D06010D06010D06010D06010D06010D0601010C06010C06010C06010C06010C06010C06010C06010C06010C060101020601020601020601020601020601020601020601020601020601010A06010A06010A06010A06010A06010A06010A06010A06010A060101010601010601010601010601010601010601010601010601010601010C06010C06010C06010C06010C06010C06010C06010C06010C060101020601020601020601020601020601020601020601020601020601010A06010A06010A06010A06010A06010A06010A06010A06010A0601010106010106010106010106010106010106010106010106010106);

-- ----------------------------
-- Table structure for josephvariable
-- ----------------------------
DROP TABLE IF EXISTS `josephvariable`;
CREATE TABLE `josephvariable`  (
  `ID` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `data` longblob NOT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of josephvariable
-- ----------------------------
INSERT INTO `josephvariable` VALUES ('Elite0', '');
INSERT INTO `josephvariable` VALUES ('Elite1', '');
INSERT INTO `josephvariable` VALUES ('Elite2', '');
INSERT INTO `josephvariable` VALUES ('Elite3', '');

-- ----------------------------
-- Table structure for kingdommission
-- ----------------------------
DROP TABLE IF EXISTS `kingdommission`;
CREATE TABLE `kingdommission`  (
  `UID` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  `StrikePoints` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  `KingdomDeeds` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  `CompletedUndercover` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  `TodayStrikePoints` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  PRIMARY KEY (`UID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of kingdommission
-- ----------------------------
INSERT INTO `kingdommission` VALUES (1000001, 300, 0, 0, 0);
INSERT INTO `kingdommission` VALUES (1000002, 0, 0, 0, 0);
INSERT INTO `kingdommission` VALUES (1000003, 0, 0, 0, 0);
INSERT INTO `kingdommission` VALUES (1000004, 0, 0, 0, 0);
INSERT INTO `kingdommission` VALUES (1000005, 0, 0, 0, 0);
INSERT INTO `kingdommission` VALUES (1000006, 0, 0, 0, 0);
INSERT INTO `kingdommission` VALUES (1000007, 0, 0, 0, 0);
INSERT INTO `kingdommission` VALUES (1000008, 0, 0, 0, 0);
INSERT INTO `kingdommission` VALUES (1000009, 0, 0, 0, 0);
INSERT INTO `kingdommission` VALUES (1000010, 0, 0, 0, 0);
INSERT INTO `kingdommission` VALUES (1000013, 300, 0, 0, 0);
INSERT INTO `kingdommission` VALUES (1000014, 0, 0, 0, 0);

-- ----------------------------
-- Table structure for lottery
-- ----------------------------
DROP TABLE IF EXISTS `lottery`;
CREATE TABLE `lottery`  (
  `id` int(4) UNSIGNED NOT NULL DEFAULT 0,
  `type` int(4) UNSIGNED NOT NULL DEFAULT 0,
  `rank` tinyint(1) UNSIGNED NOT NULL DEFAULT 0,
  `chance` int(4) UNSIGNED NOT NULL DEFAULT 0,
  `prize_name` char(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `prize_item` int(4) UNSIGNED NOT NULL DEFAULT 0,
  `color` tinyint(1) UNSIGNED NOT NULL DEFAULT 0,
  `hole_num` tinyint(1) UNSIGNED NOT NULL DEFAULT 0,
  `addition_lev` tinyint(1) UNSIGNED NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of lottery
-- ----------------------------
INSERT INTO `lottery` VALUES (1, 1, 8, 100, '+1Stone', 730001, 1, 0, 1);
INSERT INTO `lottery` VALUES (3, 1, 8, 100, 'DisguiseAmulet', 723724, 3, 0, 0);
INSERT INTO `lottery` VALUES (4, 1, 8, 100, 'LifeFruitBasket', 723725, 3, 0, 0);
INSERT INTO `lottery` VALUES (5, 1, 8, 60, 'PenitenceAmulet', 723727, 4, 0, 0);
INSERT INTO `lottery` VALUES (7, 1, 8, 70, 'Class1MoneyBag', 723713, 5, 0, 0);
INSERT INTO `lottery` VALUES (8, 1, 7, 100, '+2Stone', 730002, 3, 0, 2);
INSERT INTO `lottery` VALUES (9, 1, 7, 100, 'ExpPotion', 723017, 4, 0, 0);
INSERT INTO `lottery` VALUES (10, 1, 7, 100, 'ExpBall', 723700, 5, 0, 0);
INSERT INTO `lottery` VALUES (12, 1, 7, 70, 'Class2MoneyBag', 723714, 1, 0, 0);
INSERT INTO `lottery` VALUES (13, 1, 6, 50, 'RefinedMoonGem', 700062, 3, 0, 0);
INSERT INTO `lottery` VALUES (14, 1, 6, 10, 'RefinedFuryGem', 700022, 3, 0, 0);
INSERT INTO `lottery` VALUES (15, 1, 6, 30, 'RefinedVioletGem', 700052, 3, 0, 0);
INSERT INTO `lottery` VALUES (16, 1, 6, 100, '+1StonePack', 723712, 4, 0, 0);
INSERT INTO `lottery` VALUES (17, 1, 6, 1, 'MeteorTearPack', 723711, 5, 0, 0);
INSERT INTO `lottery` VALUES (18, 1, 6, 70, 'Class3MoneyBag', 723715, 2, 0, 0);
INSERT INTO `lottery` VALUES (19, 1, 5, 100, '+3Stone', 730003, 4, 0, 3);
INSERT INTO `lottery` VALUES (20, 1, 5, 80, 'RefinedDragonGem', 700012, 3, 0, 0);
INSERT INTO `lottery` VALUES (21, 1, 5, 80, 'RefinedPhoenixGem', 700002, 3, 0, 0);
INSERT INTO `lottery` VALUES (22, 1, 5, 80, 'RefinedRainbowGem', 700032, 2, 0, 0);
INSERT INTO `lottery` VALUES (23, 1, 5, 70, 'Class4MoneyBag', 723716, 1, 0, 0);
INSERT INTO `lottery` VALUES (24, 1, 4, 60, 'DragonBall', 1088000, 5, 0, 0);
INSERT INTO `lottery` VALUES (25, 1, 4, 80, '+4Stone', 730004, 3, 0, 4);
INSERT INTO `lottery` VALUES (26, 1, 4, 100, 'PrayingStone(S)', 1200000, 2, 0, 0);
INSERT INTO `lottery` VALUES (27, 1, 4, 100, 'BlackTulip', 723584, 2, 0, 0);
INSERT INTO `lottery` VALUES (28, 1, 4, 100, 'SashToken(S)', 722840, 1, 0, 0);
INSERT INTO `lottery` VALUES (29, 1, 4, 70, 'Class5MoneyBag', 723717, 4, 0, 0);
INSERT INTO `lottery` VALUES (30, 1, 4, 50, 'WhitePhoenix', 181305, 3, 0, 0);
INSERT INTO `lottery` VALUES (31, 1, 4, 50, 'BrownPhoenix', 181405, 3, 0, 0);
INSERT INTO `lottery` VALUES (32, 1, 4, 50, 'BlackPhoenix', 181505, 3, 0, 0);
INSERT INTO `lottery` VALUES (33, 1, 4, 50, 'RedPhoenix', 181605, 3, 0, 0);
INSERT INTO `lottery` VALUES (34, 1, 4, 50, 'GreenPhoenix', 181705, 3, 0, 0);
INSERT INTO `lottery` VALUES (35, 1, 4, 50, 'BluePhoenix', 181805, 3, 0, 0);
INSERT INTO `lottery` VALUES (36, 1, 4, 50, 'PurplePhoenix', 181905, 3, 0, 0);
INSERT INTO `lottery` VALUES (37, 1, 4, 50, 'WhiteElegance', 181315, 3, 0, 0);
INSERT INTO `lottery` VALUES (38, 1, 4, 50, 'BrownElegance', 181415, 3, 0, 0);
INSERT INTO `lottery` VALUES (39, 1, 4, 50, 'BlackElegance', 181515, 3, 0, 0);
INSERT INTO `lottery` VALUES (40, 1, 4, 50, 'RedElegance', 181615, 5, 0, 0);
INSERT INTO `lottery` VALUES (41, 1, 4, 50, 'GreenElegance', 181715, 5, 0, 0);
INSERT INTO `lottery` VALUES (42, 1, 4, 50, 'BlueElegance', 181815, 5, 0, 0);
INSERT INTO `lottery` VALUES (43, 1, 4, 50, 'PurpleElegance', 181915, 5, 0, 0);
INSERT INTO `lottery` VALUES (44, 1, 4, 50, 'WhiteCelestial', 181325, 5, 0, 0);
INSERT INTO `lottery` VALUES (45, 1, 4, 50, 'BrownCelestial', 181425, 5, 0, 0);
INSERT INTO `lottery` VALUES (46, 1, 4, 50, 'BlackCelestial', 181525, 5, 0, 0);
INSERT INTO `lottery` VALUES (47, 1, 4, 50, 'RedCelestial', 181625, 5, 0, 0);
INSERT INTO `lottery` VALUES (48, 1, 4, 50, 'GreenCelestial', 181725, 3, 0, 0);
INSERT INTO `lottery` VALUES (49, 1, 4, 50, 'BlueCelestial', 181825, 3, 0, 0);
INSERT INTO `lottery` VALUES (50, 1, 4, 50, 'PurpleCelestial', 181925, 3, 0, 0);
INSERT INTO `lottery` VALUES (51, 1, 4, 50, 'WeddingGown', 181335, 4, 0, 0);
INSERT INTO `lottery` VALUES (52, 1, 4, 50, 'ColorfulDress', 181345, 4, 0, 0);
INSERT INTO `lottery` VALUES (53, 1, 4, 50, 'DarkWizard', 181355, 4, 0, 0);
INSERT INTO `lottery` VALUES (54, 1, 4, 50, 'PrairieWind', 181365, 4, 0, 0);
INSERT INTO `lottery` VALUES (55, 1, 4, 50, 'SongofTianshan', 181375, 2, 0, 0);
INSERT INTO `lottery` VALUES (56, 1, 4, 50, 'RoyalDignity', 181385, 2, 0, 0);
INSERT INTO `lottery` VALUES (57, 1, 4, 50, 'Daisy', 181395, 2, 0, 0);
INSERT INTO `lottery` VALUES (58, 1, 4, 50, 'SouthofCloud', 182305, 2, 0, 0);
INSERT INTO `lottery` VALUES (59, 1, 4, 50, 'BonfireNight', 182315, 4, 0, 0);
INSERT INTO `lottery` VALUES (60, 1, 4, 50, 'AngelicalDress', 182325, 4, 0, 0);
INSERT INTO `lottery` VALUES (61, 1, 3, 80, '+5Stone', 730005, 5, 0, 5);
INSERT INTO `lottery` VALUES (62, 1, 3, 100, 'PrayingStone(L)', 1200002, 3, 0, 0);
INSERT INTO `lottery` VALUES (63, 1, 3, 100, 'SuperTortoiseGem', 700073, 4, 0, 0);
INSERT INTO `lottery` VALUES (64, 1, 3, 100, 'MagicalBottle', 2100045, 2, 0, 0);
INSERT INTO `lottery` VALUES (65, 1, 3, 120, 'ExemptionToken', 723701, 2, 0, 0);
INSERT INTO `lottery` VALUES (66, 1, 3, 120, 'Class6MoneyBag', 723718, 3, 0, 0);
INSERT INTO `lottery` VALUES (67, 1, 3, 70, 'Class7MoneyBag', 723719, 3, 0, 0);
INSERT INTO `lottery` VALUES (68, 1, 3, 100, 'SuperWarArmor', 130089, 2, 0, 0);
INSERT INTO `lottery` VALUES (69, 1, 3, 100, 'SuperMonkCoronet', 118089, 1, 0, 0);
INSERT INTO `lottery` VALUES (70, 1, 3, 100, 'SuperBasaltArmor', 131089, 1, 0, 0);
INSERT INTO `lottery` VALUES (71, 1, 3, 100, 'SuperViolentHelmet', 111089, 1, 0, 0);
INSERT INTO `lottery` VALUES (72, 1, 3, 100, 'SuperRhinoCoat', 133079, 1, 0, 0);
INSERT INTO `lottery` VALUES (73, 1, 3, 100, 'SuperAntlerHat', 113069, 1, 0, 0);
INSERT INTO `lottery` VALUES (74, 1, 3, 100, 'SuperConquestGown', 134099, 1, 0, 0);
INSERT INTO `lottery` VALUES (75, 1, 3, 100, 'SuperStarCap', 114099, 5, 0, 0);
INSERT INTO `lottery` VALUES (76, 1, 3, 70, 'SuperGoldBag', 121189, 5, 0, 0);
INSERT INTO `lottery` VALUES (77, 1, 3, 70, 'SuperDragonNecklace', 120189, 5, 0, 0);
INSERT INTO `lottery` VALUES (78, 1, 3, 70, 'SuperBlueBracelet', 152189, 5, 0, 0);
INSERT INTO `lottery` VALUES (79, 1, 3, 70, 'SuperJadeEarring', 117089, 5, 0, 0);
INSERT INTO `lottery` VALUES (80, 1, 3, 70, 'SuperDiamondRing', 150199, 5, 0, 0);
INSERT INTO `lottery` VALUES (81, 1, 3, 90, 'SuperJetHeavyRing', 151179, 5, 0, 0);
INSERT INTO `lottery` VALUES (82, 1, 3, 70, 'SuperKylinBoots', 160199, 5, 0, 0);
INSERT INTO `lottery` VALUES (83, 1, 3, 70, 'SuperCloudBacksword', 421199, 5, 0, 0);
INSERT INTO `lottery` VALUES (84, 1, 3, 70, 'SuperRosewoodBow', 500189, 5, 0, 0);
INSERT INTO `lottery` VALUES (85, 1, 3, 100, 'SuperThornShield', 900089, 5, 0, 0);
INSERT INTO `lottery` VALUES (86, 1, 3, 70, 'SuperRainbowBlade', 410199, 5, 0, 0);
INSERT INTO `lottery` VALUES (87, 1, 3, 70, 'SuperLoyalSword', 420199, 1, 0, 0);
INSERT INTO `lottery` VALUES (88, 1, 3, 100, 'SuperDemonHook', 430199, 2, 0, 0);
INSERT INTO `lottery` VALUES (89, 1, 3, 100, 'SuperLightningWhip', 440199, 2, 0, 0);
INSERT INTO `lottery` VALUES (90, 1, 3, 100, 'SuperGoldAxe', 450199, 2, 0, 0);
INSERT INTO `lottery` VALUES (91, 1, 3, 100, 'SuperShiningHammer', 460199, 2, 0, 0);
INSERT INTO `lottery` VALUES (92, 1, 3, 70, 'SuperSnakeClub', 480199, 2, 0, 0);
INSERT INTO `lottery` VALUES (93, 1, 3, 100, 'SuperThunderScepter', 481199, 2, 0, 0);
INSERT INTO `lottery` VALUES (94, 1, 3, 100, 'SuperRainbowDagger', 490199, 2, 0, 0);
INSERT INTO `lottery` VALUES (95, 1, 3, 100, 'SuperTigerGlaive', 510199, 2, 0, 0);
INSERT INTO `lottery` VALUES (96, 1, 3, 100, 'SuperGrimPoleaxe', 530199, 2, 0, 0);
INSERT INTO `lottery` VALUES (97, 1, 3, 100, 'SuperPetalLongHammer', 540199, 2, 0, 0);
INSERT INTO `lottery` VALUES (98, 1, 3, 100, 'SuperLance', 560199, 1, 0, 0);
INSERT INTO `lottery` VALUES (99, 1, 3, 100, 'SuperSilverHalbert', 580199, 1, 0, 0);
INSERT INTO `lottery` VALUES (100, 1, 3, 100, 'SuperCopperWand', 561199, 1, 0, 0);
INSERT INTO `lottery` VALUES (101, 1, 3, 70, '2Socket-EliteIronBacksword', 421088, 3, 2, 0);
INSERT INTO `lottery` VALUES (102, 1, 3, 70, '2Socket-EliteQinBow', 500088, 3, 2, 0);
INSERT INTO `lottery` VALUES (103, 1, 3, 70, '2Socket-EliteRattanShield', 900018, 3, 2, 0);
INSERT INTO `lottery` VALUES (104, 1, 3, 70, '2Socket-EliteMoonBlade', 410098, 3, 2, 0);
INSERT INTO `lottery` VALUES (105, 1, 3, 100, '2Socket-EliteFangSword', 420098, 3, 2, 0);
INSERT INTO `lottery` VALUES (106, 1, 3, 100, '2Socket-EliteAntlerHook', 430098, 3, 2, 0);
INSERT INTO `lottery` VALUES (107, 1, 3, 100, '2Socket-EliteTwinWhip', 440098, 3, 2, 0);
INSERT INTO `lottery` VALUES (108, 1, 3, 100, '2Socket-ElitePeaceAxe', 450098, 3, 2, 0);
INSERT INTO `lottery` VALUES (109, 1, 3, 100, '2Socket-EliteMelonHammer', 460098, 4, 2, 0);
INSERT INTO `lottery` VALUES (110, 1, 3, 70, '2Socket-EliteWarClub', 480098, 4, 2, 0);
INSERT INTO `lottery` VALUES (111, 1, 3, 100, '2Socket-EliteWishScepter', 481098, 4, 2, 0);
INSERT INTO `lottery` VALUES (112, 1, 3, 100, '2Socket-EliteCaoDagger', 490098, 4, 2, 0);
INSERT INTO `lottery` VALUES (113, 1, 3, 100, '2Socket-EliteUnionGlaive', 510098, 4, 2, 0);
INSERT INTO `lottery` VALUES (114, 1, 3, 100, '2Socket-EliteTwinPoleaxe', 530098, 4, 2, 0);
INSERT INTO `lottery` VALUES (115, 1, 3, 100, '2Socket-EliteGreatLongHammer', 540098, 4, 2, 0);
INSERT INTO `lottery` VALUES (116, 1, 3, 100, '2Socket-EliteLuckSpear', 560098, 4, 2, 0);
INSERT INTO `lottery` VALUES (117, 1, 3, 100, '2Socket-EliteGoldHalbert', 580098, 3, 2, 0);
INSERT INTO `lottery` VALUES (118, 1, 3, 100, '2Socket-EliteShaolinWand', 561098, 3, 2, 0);
INSERT INTO `lottery` VALUES (119, 1, 2, 80, '+6Stone', 730006, 2, 0, 6);
INSERT INTO `lottery` VALUES (120, 1, 2, 100, 'MiraculousGourd', 2100025, 3, 0, 0);
INSERT INTO `lottery` VALUES (121, 1, 2, 100, 'Sash(L)', 1100009, 3, 0, 0);
INSERT INTO `lottery` VALUES (122, 1, 2, 150, 'Class8MoneyBag', 723720, 4, 0, 0);
INSERT INTO `lottery` VALUES (123, 1, 2, 70, 'Class9MoneyBag', 723721, 4, 0, 0);
INSERT INTO `lottery` VALUES (124, 1, 2, 100, '1Socket-EliteDemonArmor', 130058, 5, 1, 0);
INSERT INTO `lottery` VALUES (125, 1, 2, 100, '1Socket-EliteGoldCoronet', 118058, 5, 1, 0);
INSERT INTO `lottery` VALUES (126, 1, 2, 100, '1Socket-EliteChainedArmor', 131058, 5, 1, 0);
INSERT INTO `lottery` VALUES (127, 1, 2, 100, '1Socket-ElitePearlHelmet', 111058, 5, 1, 0);
INSERT INTO `lottery` VALUES (128, 1, 2, 100, '1Socket-EliteApeCoat', 133048, 5, 1, 0);
INSERT INTO `lottery` VALUES (129, 1, 2, 100, '1Socket-EliteLeopardHat', 113038, 5, 1, 0);
INSERT INTO `lottery` VALUES (130, 1, 2, 100, '1Socket-ElitePowerGown', 134058, 5, 1, 0);
INSERT INTO `lottery` VALUES (131, 1, 2, 100, '1Socket-EliteSharkCap', 114068, 5, 1, 0);
INSERT INTO `lottery` VALUES (132, 1, 2, 70, '1Socket-EliteAmbergrisBag', 121128, 1, 1, 0);
INSERT INTO `lottery` VALUES (133, 1, 2, 70, '1Socket-ElitePlatinaNecklace', 120128, 1, 1, 0);
INSERT INTO `lottery` VALUES (134, 1, 2, 70, '1Socket-EliteBoneBracelet', 152128, 1, 1, 0);
INSERT INTO `lottery` VALUES (135, 1, 2, 70, '1Socket-EliteHeartofOcean', 117068, 1, 1, 0);
INSERT INTO `lottery` VALUES (136, 1, 2, 70, '1Socket-EliteJadeRing', 150118, 1, 1, 0);
INSERT INTO `lottery` VALUES (137, 1, 2, 90, '1Socket-EliteDarkHeavyRing', 151118, 2, 1, 0);
INSERT INTO `lottery` VALUES (138, 1, 2, 70, '1Socket-EliteLightBoots', 160098, 2, 1, 0);
INSERT INTO `lottery` VALUES (139, 1, 1, 120, 'Class10MoneyBag', 723722, 3, 0, 0);
INSERT INTO `lottery` VALUES (140, 1, 1, 70, 'TopMoneyBag', 723723, 3, 0, 0);
INSERT INTO `lottery` VALUES (141, 1, 1, 70, 'EliteEndBacksword(+6)', 421078, 2, 0, 6);
INSERT INTO `lottery` VALUES (142, 1, 1, 70, 'EliteHornBow(+6)', 500078, 2, 0, 6);
INSERT INTO `lottery` VALUES (143, 1, 1, 100, 'EliteSoftShield(+6)', 900008, 2, 0, 6);
INSERT INTO `lottery` VALUES (144, 1, 1, 70, 'EliteCutlass(+6)', 410078, 2, 0, 6);
INSERT INTO `lottery` VALUES (145, 1, 1, 70, 'ElitePeaceSword(+6)', 420088, 2, 0, 6);
INSERT INTO `lottery` VALUES (146, 1, 1, 100, 'ElitePhoenixHook(+6)', 430088, 2, 0, 6);
INSERT INTO `lottery` VALUES (147, 1, 1, 100, 'EliteShaolinMace(+6)', 440088, 2, 0, 6);
INSERT INTO `lottery` VALUES (148, 1, 1, 100, 'EliteShaolinAxe(+6)', 450088, 2, 0, 6);
INSERT INTO `lottery` VALUES (149, 1, 1, 100, 'EliteWarHammer(+6)', 460088, 2, 0, 6);
INSERT INTO `lottery` VALUES (150, 1, 1, 70, 'EliteUnitClub(+6)', 480088, 2, 0, 6);
INSERT INTO `lottery` VALUES (151, 1, 1, 100, 'EliteLotusScepter(+6)', 481088, 3, 0, 6);
INSERT INTO `lottery` VALUES (152, 1, 1, 100, 'EliteDragonDagger(+6)', 490088, 3, 0, 6);
INSERT INTO `lottery` VALUES (153, 1, 1, 100, 'EliteRockGlaive(+6)', 510088, 3, 0, 6);
INSERT INTO `lottery` VALUES (154, 1, 1, 100, 'EliteSwallowPoleaxe(+6)', 530088, 3, 0, 6);
INSERT INTO `lottery` VALUES (155, 1, 1, 100, 'EliteGhostLongHammer(+6)', 540088, 3, 0, 6);
INSERT INTO `lottery` VALUES (156, 1, 1, 100, 'EliteMoonSpear(+6)', 560088, 3, 0, 6);
INSERT INTO `lottery` VALUES (157, 1, 1, 100, 'EliteHawkHalbert(+6)', 580088, 3, 0, 6);
INSERT INTO `lottery` VALUES (158, 1, 1, 100, 'EliteTimeWand(+6)', 561088, 3, 0, 6);
INSERT INTO `lottery` VALUES (159, 1, 1, 100, 'EliteGothicPlate(+6)', 130038, 3, 0, 6);
INSERT INTO `lottery` VALUES (160, 1, 1, 100, 'EliteSteelCoronet(+6)', 118038, 3, 0, 6);
INSERT INTO `lottery` VALUES (161, 1, 1, 100, 'EliteSteelArmor(+6)', 131038, 3, 0, 6);
INSERT INTO `lottery` VALUES (162, 1, 1, 100, 'EliteShiningHelmet(+6)', 111038, 3, 0, 6);
INSERT INTO `lottery` VALUES (163, 1, 1, 100, 'EliteLeopardCoat(+6)', 133038, 3, 0, 6);
INSERT INTO `lottery` VALUES (164, 1, 1, 100, 'EliteCatHat(+6)', 113018, 3, 0, 6);
INSERT INTO `lottery` VALUES (165, 1, 1, 100, 'EliteStarRobe(+6)', 134038, 4, 0, 6);
INSERT INTO `lottery` VALUES (166, 1, 1, 100, 'EliteStoneCap(+6)', 114038, 4, 0, 6);
INSERT INTO `lottery` VALUES (167, 1, 1, 70, 'EliteBlueBag(+6)', 121088, 4, 0, 6);
INSERT INTO `lottery` VALUES (168, 1, 1, 70, 'EliteCrystalNecklace(+6)', 120088, 4, 0, 6);
INSERT INTO `lottery` VALUES (169, 1, 1, 70, 'EliteVioletBracelet(+6)', 152108, 4, 0, 6);
INSERT INTO `lottery` VALUES (170, 1, 1, 70, 'EliteMeteorEarring(+6)', 117038, 4, 0, 6);
INSERT INTO `lottery` VALUES (171, 1, 1, 70, 'EliteAmethystRing(+6)', 150078, 4, 0, 6);
INSERT INTO `lottery` VALUES (172, 1, 1, 100, 'EliteIvoryHeavyRing(+6)', 151078, 4, 0, 6);
INSERT INTO `lottery` VALUES (173, 1, 1, 70, 'EliteEagleBoots(+6)', 160078, 4, 0, 6);
INSERT INTO `lottery` VALUES (174, 1, 1, 100, '2Socket-EliteDipperArmor', 130048, 5, 2, 0);
INSERT INTO `lottery` VALUES (175, 1, 1, 100, '2Socket-EliteJadeCoronet', 118048, 5, 2, 0);
INSERT INTO `lottery` VALUES (176, 1, 1, 100, '2Socket-EliteBrightArmor', 131048, 5, 2, 0);
INSERT INTO `lottery` VALUES (177, 1, 1, 100, '2Socket-EliteSyeniticHelmet', 111048, 5, 2, 0);
INSERT INTO `lottery` VALUES (178, 1, 1, 100, '2Socket-EliteWolfCoat', 133028, 1, 2, 0);
INSERT INTO `lottery` VALUES (179, 1, 1, 100, '2Socket-EliteJackalHat', 113028, 1, 2, 0);
INSERT INTO `lottery` VALUES (180, 1, 1, 100, '2Socket-ElitePureGown', 134048, 1, 2, 0);
INSERT INTO `lottery` VALUES (181, 1, 1, 100, '2Socket-EliteCloudCap', 114048, 1, 2, 0);
INSERT INTO `lottery` VALUES (182, 1, 1, 70, '2Socket-EliteBeanBag', 121098, 1, 2, 0);
INSERT INTO `lottery` VALUES (183, 1, 1, 70, '2Socket-EliteGoldNecklace', 120098, 1, 2, 0);
INSERT INTO `lottery` VALUES (184, 1, 1, 70, '2Socket-EliteGoldBracelet', 152088, 1, 2, 0);
INSERT INTO `lottery` VALUES (185, 1, 1, 70, '2Socket-EliteTasselEarring', 117048, 3, 2, 0);
INSERT INTO `lottery` VALUES (186, 1, 1, 70, '2Socket-EliteIvoryRing', 150098, 3, 2, 0);
INSERT INTO `lottery` VALUES (187, 1, 1, 70, '2Socket-EliteBoneHeavyRing', 151098, 3, 2, 0);
INSERT INTO `lottery` VALUES (188, 1, 1, 70, '2Socket-EliteCrocodileBoots', 160118, 2, 2, 0);
INSERT INTO `lottery` VALUES (189, 1, 3, 100, 'BlueDream', 182335, 1, 0, 0);
INSERT INTO `lottery` VALUES (190, 1, 3, 100, 'MoonOrchid', 182345, 2, 0, 0);
INSERT INTO `lottery` VALUES (191, 1, 3, 100, 'FlyingFeather', 182355, 3, 0, 0);
INSERT INTO `lottery` VALUES (192, 1, 3, 100, 'HeavenScent', 182365, 4, 0, 0);
INSERT INTO `lottery` VALUES (193, 1, 3, 100, 'DanceofCherry', 182375, 5, 0, 0);
INSERT INTO `lottery` VALUES (194, 1, 3, 100, 'DreaminFlowers', 182385, 3, 0, 0);
INSERT INTO `lottery` VALUES (195, 1, 8, 1, 'Meteor', 1088001, 5, 0, 0);
INSERT INTO `lottery` VALUES (196, 1, 4, 150, 'CleanWater', 721258, 5, 0, 0);
INSERT INTO `lottery` VALUES (197, 1, 3, 100, 'SuperMapleHeadband', 141069, 1, 0, 0);
INSERT INTO `lottery` VALUES (198, 1, 3, 100, 'SuperSwanPlume', 142069, 1, 0, 0);
INSERT INTO `lottery` VALUES (199, 1, 2, 100, '1Socket-EliteFiresilkBand', 141058, 5, 1, 0);
INSERT INTO `lottery` VALUES (200, 1, 2, 100, '1Socket-EliteRoyalPlume', 142058, 5, 1, 0);
INSERT INTO `lottery` VALUES (201, 1, 1, 100, '2Socket-ElitePearlHeadband', 141048, 5, 2, 0);
INSERT INTO `lottery` VALUES (202, 1, 1, 100, '2Socket-EliteArcticPlume', 142048, 5, 2, 0);
INSERT INTO `lottery` VALUES (203, 1, 1, 100, 'EliteArcticPlume(+6)', 142048, 5, 0, 6);
INSERT INTO `lottery` VALUES (204, 1, 1, 100, 'ElitePearlHeadband(+6)', 141048, 5, 0, 6);

-- ----------------------------
-- Table structure for maps
-- ----------------------------
DROP TABLE IF EXISTS `maps`;
CREATE TABLE `maps`  (
  `id` int(4) UNSIGNED NOT NULL DEFAULT 0,
  `mapdoc` int(4) UNSIGNED NOT NULL DEFAULT 0,
  `type` int(4) UNSIGNED ZEROFILL NULL DEFAULT 0000,
  `weather` int(4) UNSIGNED ZEROFILL NULL DEFAULT 0000,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of maps
-- ----------------------------
INSERT INTO `maps` VALUES (1006, 1006, 65544, 0000);
INSERT INTO `maps` VALUES (7006, 1730, 0007, 0000);
INSERT INTO `maps` VALUES (7007, 1730, 0007, 0000);
INSERT INTO `maps` VALUES (7008, 1730, 0007, 0000);
INSERT INTO `maps` VALUES (1005, 1005, 0001, 0000);
INSERT INTO `maps` VALUES (1004, 1004, 65544, 0000);
INSERT INTO `maps` VALUES (4662, 1992, 0007, 0000);
INSERT INTO `maps` VALUES (1002, 1002, 8200, 0002);
INSERT INTO `maps` VALUES (1001, 1001, 8192, 0000);
INSERT INTO `maps` VALUES (1000, 1000, 8192, 0000);
INSERT INTO `maps` VALUES (1011, 1011, 8192, 0000);
INSERT INTO `maps` VALUES (1015, 1015, 8192, 0000);
INSERT INTO `maps` VALUES (1012, 1012, 8192, 0000);
INSERT INTO `maps` VALUES (1013, 1013, 8192, 0000);
INSERT INTO `maps` VALUES (1014, 1014, 8192, 0000);
INSERT INTO `maps` VALUES (1016, 1016, 8192, 0000);
INSERT INTO `maps` VALUES (1020, 1020, 8192, 0000);
INSERT INTO `maps` VALUES (4562, 1992, 0007, 0000);
INSERT INTO `maps` VALUES (5000, 1025, 0003, 0000);
INSERT INTO `maps` VALUES (6000, 6000, 108898, 0000);
INSERT INTO `maps` VALUES (1036, 1036, 0030, 0000);
INSERT INTO `maps` VALUES (1038, 1038, 0130, 0000);
INSERT INTO `maps` VALUES (6001, 6001, 98403, 0000);
INSERT INTO `maps` VALUES (1508, 1508, 0007, 0000);
INSERT INTO `maps` VALUES (700, 700, 66124, 0000);
INSERT INTO `maps` VALUES (2056, 2056, 8192, 0000);
INSERT INTO `maps` VALUES (2807, 3852, 0074, 0000);
INSERT INTO `maps` VALUES (8881, 1860, 65544, 0000);
INSERT INTO `maps` VALUES (2071, 2071, 0007, 0000);
INSERT INTO `maps` VALUES (8877, 9972, 0007, 0000);
INSERT INTO `maps` VALUES (3333, 700, 0007, 0000);
INSERT INTO `maps` VALUES (8800, 1354, 8194, 0000);
INSERT INTO `maps` VALUES (2351, 1351, 8194, 0000);
INSERT INTO `maps` VALUES (1950, 1950, 33554734, 0000);
INSERT INTO `maps` VALUES (2068, 2068, 0000, 0000);
INSERT INTO `maps` VALUES (1736, 1776, 0071, 0000);
INSERT INTO `maps` VALUES (1735, 1775, 0071, 0000);
INSERT INTO `maps` VALUES (1734, 1774, 0071, 0000);
INSERT INTO `maps` VALUES (1733, 1773, 0071, 0000);
INSERT INTO `maps` VALUES (1732, 1772, 0071, 0000);
INSERT INTO `maps` VALUES (1731, 1771, 0071, 0000);
INSERT INTO `maps` VALUES (1730, 1770, 0071, 0000);
INSERT INTO `maps` VALUES (3355, 700, 0007, 0000);
INSERT INTO `maps` VALUES (5566, 601, 0007, 0000);
INSERT INTO `maps` VALUES (2072, 1002, 0007, 0000);
INSERT INTO `maps` VALUES (2073, 1011, 0007, 0000);
INSERT INTO `maps` VALUES (2074, 1020, 0007, 0000);
INSERT INTO `maps` VALUES (2075, 1015, 0007, 0000);
INSERT INTO `maps` VALUES (9902, 1099, 0007, 0000);
INSERT INTO `maps` VALUES (1509, 1509, 0007, 0000);
INSERT INTO `maps` VALUES (1252, 1004, 65544, 0000);
INSERT INTO `maps` VALUES (1518, 1508, 0007, 0000);
INSERT INTO `maps` VALUES (5560, 700, 0007, 0000);
INSERT INTO `maps` VALUES (5530, 700, 0007, 0000);
INSERT INTO `maps` VALUES (5540, 700, 0007, 0000);
INSERT INTO `maps` VALUES (1042, 1042, 8192, 0000);
INSERT INTO `maps` VALUES (1043, 1043, 8258, 0000);
INSERT INTO `maps` VALUES (1044, 1044, 8258, 0000);
INSERT INTO `maps` VALUES (1045, 1045, 8258, 0000);
INSERT INTO `maps` VALUES (1046, 1046, 8258, 0000);
INSERT INTO `maps` VALUES (1047, 1047, 0002, 0000);
INSERT INTO `maps` VALUES (1048, 1048, 0002, 0000);
INSERT INTO `maps` VALUES (1049, 1049, 0002, 0000);
INSERT INTO `maps` VALUES (1050, 1050, 8258, 0000);
INSERT INTO `maps` VALUES (1300, 1001, 8194, 0000);
INSERT INTO `maps` VALUES (3220, 2068, 0007, 0010);
INSERT INTO `maps` VALUES (3856, 3846, 8259, 0000);
INSERT INTO `maps` VALUES (8080, 700, 0007, 0000);
INSERT INTO `maps` VALUES (1700, 1700, 8195, 0000);
INSERT INTO `maps` VALUES (9658, 1801, 0007, 0000);
INSERT INTO `maps` VALUES (3544, 700, 0007, 0000);
INSERT INTO `maps` VALUES (5687, 700, 0007, 0000);
INSERT INTO `maps` VALUES (5664, 700, 0007, 0000);
INSERT INTO `maps` VALUES (6635, 700, 0007, 0000);
INSERT INTO `maps` VALUES (3644, 700, 0007, 0000);
INSERT INTO `maps` VALUES (3215, 700, 0007, 0000);
INSERT INTO `maps` VALUES (7001, 1730, 0007, 0000);
INSERT INTO `maps` VALUES (7002, 1730, 0007, 0000);
INSERT INTO `maps` VALUES (7003, 1730, 0007, 0000);
INSERT INTO `maps` VALUES (7004, 1730, 0007, 0000);
INSERT INTO `maps` VALUES (7000, 1730, 0007, 0000);
INSERT INTO `maps` VALUES (7005, 1730, 0007, 0000);
INSERT INTO `maps` VALUES (2057, 2057, 0007, 0000);
INSERT INTO `maps` VALUES (10020, 1730, 0071, 0000);
INSERT INTO `maps` VALUES (10021, 1730, 0071, 0000);
INSERT INTO `maps` VALUES (10022, 1730, 0071, 0000);
INSERT INTO `maps` VALUES (3935, 3935, 8199, 0000);
INSERT INTO `maps` VALUES (3081, 3081, 0013, 0000);
INSERT INTO `maps` VALUES (1806, 1806, 0014, 0000);
INSERT INTO `maps` VALUES (9979, 9972, 0014, 0000);
INSERT INTO `maps` VALUES (9980, 9972, 0014, 0000);
INSERT INTO `maps` VALUES (9981, 9972, 0014, 0000);
INSERT INTO `maps` VALUES (9982, 9972, 0014, 0000);
INSERT INTO `maps` VALUES (1511, 1004, 0008, 0000);
INSERT INTO `maps` VALUES (3849, 3849, 0014, 0000);
INSERT INTO `maps` VALUES (3983, 3983, 0000, 0000);
INSERT INTO `maps` VALUES (10332, 1040, 0000, 0000);
INSERT INTO `maps` VALUES (3955, 2021, 0007, 0000);
INSERT INTO `maps` VALUES (3956, 2022, 0007, 0000);
INSERT INTO `maps` VALUES (3957, 2023, 0007, 0000);
INSERT INTO `maps` VALUES (3958, 2024, 0007, 0000);
INSERT INTO `maps` VALUES (3959, 3959, 0079, 0000);
INSERT INTO `maps` VALUES (8880, 1858, 65544, 0000);
INSERT INTO `maps` VALUES (1351, 1351, 8194, 0000);
INSERT INTO `maps` VALUES (1352, 1352, 8194, 0000);
INSERT INTO `maps` VALUES (1353, 1353, 8194, 0000);
INSERT INTO `maps` VALUES (1354, 1354, 8194, 0000);
INSERT INTO `maps` VALUES (1040, 1040, 8710, 0000);
INSERT INTO `maps` VALUES (1063, 1063, 8710, 0000);
INSERT INTO `maps` VALUES (1738, 1738, 0078, 0000);
INSERT INTO `maps` VALUES (1505, 1505, 0007, 0000);
INSERT INTO `maps` VALUES (3055, 3055, 8200, 0000);
INSERT INTO `maps` VALUES (4020, 4020, 8200, 0000);
INSERT INTO `maps` VALUES (1737, 1777, 0071, 0000);
INSERT INTO `maps` VALUES (3998, 3998, 24577, 0000);
INSERT INTO `maps` VALUES (4009, 4009, 0014, 0000);
INSERT INTO `maps` VALUES (4000, 4000, 0007, 0000);
INSERT INTO `maps` VALUES (4001, 4009, 0014, 0000);
INSERT INTO `maps` VALUES (4002, 4009, 0014, 0000);
INSERT INTO `maps` VALUES (4003, 4009, 0014, 0000);
INSERT INTO `maps` VALUES (4004, 4009, 0014, 0000);
INSERT INTO `maps` VALUES (4005, 4009, 0014, 0000);
INSERT INTO `maps` VALUES (4006, 4009, 0014, 0000);
INSERT INTO `maps` VALUES (4007, 4009, 0014, 0000);
INSERT INTO `maps` VALUES (4008, 4009, 0014, 0000);
INSERT INTO `maps` VALUES (1707, 700, 0007, 0000);
INSERT INTO `maps` VALUES (9393, 1787, 0007, 0000);
INSERT INTO `maps` VALUES (9392, 700, 0007, 0000);
INSERT INTO `maps` VALUES (9391, 700, 0007, 0000);
INSERT INTO `maps` VALUES (1811, 1508, 0007, 0000);
INSERT INTO `maps` VALUES (3214, 700, 0007, 0000);
INSERT INTO `maps` VALUES (10002, 1002, 8200, 0002);
INSERT INTO `maps` VALUES (39981, 3998, 0007, 0000);
INSERT INTO `maps` VALUES (39982, 3998, 0007, 0000);
INSERT INTO `maps` VALUES (39983, 3998, 0007, 0000);
INSERT INTO `maps` VALUES (39984, 3998, 0007, 0000);
INSERT INTO `maps` VALUES (39985, 3998, 0007, 0000);
INSERT INTO `maps` VALUES (39986, 3998, 0007, 0000);
INSERT INTO `maps` VALUES (39987, 3998, 0007, 0000);
INSERT INTO `maps` VALUES (39988, 3998, 0007, 0000);
INSERT INTO `maps` VALUES (39989, 3998, 0007, 0000);
INSERT INTO `maps` VALUES (39990, 3998, 0007, 0000);
INSERT INTO `maps` VALUES (39991, 3998, 0007, 0000);
INSERT INTO `maps` VALUES (39992, 3998, 0007, 0000);
INSERT INTO `maps` VALUES (39993, 3998, 0007, 0000);
INSERT INTO `maps` VALUES (39994, 3998, 0007, 0000);
INSERT INTO `maps` VALUES (11, 700, 0007, 0000);
INSERT INTO `maps` VALUES (12, 700, 0007, 0000);
INSERT INTO `maps` VALUES (13, 700, 0007, 0000);
INSERT INTO `maps` VALUES (14, 700, 0007, 0000);
INSERT INTO `maps` VALUES (15, 700, 0007, 0000);
INSERT INTO `maps` VALUES (10012, 1002, 8200, 0002);
INSERT INTO `maps` VALUES (20010, 1002, 8200, 0002);
INSERT INTO `maps` VALUES (10, 700, 0007, 0000);
INSERT INTO `maps` VALUES (10350, 10350, 0007, 0007);
INSERT INTO `maps` VALUES (10520, 10520, 0007, 0007);
INSERT INTO `maps` VALUES (10540, 10540, 0007, 0007);
INSERT INTO `maps` VALUES (200000, 200000, 0007, 0007);
INSERT INTO `maps` VALUES (6010, 1004, 0007, 0000);

-- ----------------------------
-- Table structure for monsterinfos
-- ----------------------------
DROP TABLE IF EXISTS `monsterinfos`;
CREATE TABLE `monsterinfos`  (
  `id` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `name` varchar(16) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `type` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `lookface` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `life` int(2) UNSIGNED ZEROFILL NOT NULL DEFAULT 00,
  `mana` smallint(2) UNSIGNED ZEROFILL NOT NULL DEFAULT 00,
  `attack_max` int(4) UNSIGNED NOT NULL DEFAULT 0,
  `attack_min` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `defence` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `attack_range` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `view_range` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `attack_speed` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `move_speed` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `level` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `attack_user` int(1) UNSIGNED NOT NULL DEFAULT 3,
  `run_speed` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `magic_type` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `extra_exp` smallint(2) NOT NULL DEFAULT 0,
  `ownitem` int(4) UNSIGNED NULL DEFAULT 0,
  `ownitemrate` int(4) UNSIGNED NULL DEFAULT 0,
  `ownitem2` int(4) UNSIGNED NULL DEFAULT 0,
  `ownitemrate2` int(4) UNSIGNED NULL DEFAULT 0,
  `ownitem3` int(4) UNSIGNED NULL DEFAULT 0,
  `ownitemrate3` int(4) UNSIGNED NULL DEFAULT 0,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of monsterinfos
-- ----------------------------
INSERT INTO `monsterinfos` VALUES (2758, 'AngerRat', 0001, 0202, 61000, 00, 1000, 1000, 0500, 0005, 0010, 1000, 1000, 0117, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2759, 'FlameRockMonste', 0001, 0209, 65000, 00, 1000, 1000, 0500, 0005, 0010, 1000, 1000, 0120, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2761, 'LavaScorpion', 0001, 0133, 81000, 00, 1000, 1000, 0500, 0005, 0010, 1000, 1000, 0125, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2762, 'RoaringSnake', 0001, 0355, 85000, 00, 1000, 1000, 0500, 0005, 0010, 1000, 1000, 0127, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2763, 'ClawedRockDevil', 0001, 0979, 90000, 00, 1000, 1000, 0500, 0005, 0010, 1000, 1000, 0130, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2764, 'HornedRockBeast', 0001, 0982, 90000, 00, 1000, 1000, 0500, 0005, 0010, 1000, 1000, 0132, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2765, 'HellfireBeast', 0001, 0276, 95000, 00, 1000, 1000, 0500, 0005, 0010, 1000, 1000, 0135, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0010, 'Macaque', 0001, 0156, 2199, 00, 173, 0156, 0000, 0001, 0008, 1000, 0500, 0047, 3, 0500, 0000, 100, 729088, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0011, 'GiantApe', 0001, 0156, 2643, 00, 222, 0200, 0000, 0001, 0008, 1000, 0500, 0052, 3, 0500, 0000, 100, 729088, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0012, 'ThunderApe', 0001, 0103, 3184, 00, 281, 0253, 0000, 0001, 0008, 1000, 0500, 0057, 3, 0500, 0000, 100, 729088, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0013, 'Snakeman', 0001, 0204, 4771, 00, 350, 0315, 0000, 0001, 0008, 1000, 0500, 0062, 3, 0500, 0000, 100, 729089, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0014, 'SandMonster', 0001, 0131, 5760, 00, 439, 0397, 0000, 0001, 0008, 1000, 0500, 0067, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0015, 'HillMonster', 0001, 0105, 7358, 00, 496, 0448, 0000, 0001, 0008, 1000, 0500, 0072, 3, 0500, 0000, 100, 1080001, 12, 729091, 10, 0, 0);
INSERT INTO `monsterinfos` VALUES (0016, 'RockMonster', 0001, 0105, 8459, 00, 589, 0532, 0000, 0001, 0008, 1000, 0500, 0077, 3, 0500, 0000, 100, 729092, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0017, 'BladeGhost', 0001, 0200, 11673, 00, 691, 0624, 0000, 0001, 0012, 1000, 0500, 0082, 3, 0500, 0000, 100, 729093, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0018, 'Birdman', 0001, 0205, 13422, 00, 810, 0731, 0000, 0001, 0008, 1000, 0500, 0087, 3, 0500, 0000, 100, 729094, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0019, 'HawKing', 0001, 0205, 15432, 00, 946, 0855, 0000, 0001, 0010, 1000, 1000, 0092, 3, 0500, 0000, 100, 729095, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0020, 'TombBat', 0001, 0206, 24472, 00, 1253, 1134, 0000, 0001, 0010, 1000, 1000, 0102, 3, 0500, 0000, 100, 729098, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0900, 'Guard1', 0001, 0900, 70000, 00, 700000, 700000, 70000, 0015, 0015, 1000, 1000, 0120, 15, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0036, 'StoneMonster', 0001, 0305, 50000, 00, 737, 0665, 0512, 0001, 0008, 1000, 0500, 0080, 3, 0400, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0035, 'MountMonster', 0001, 0305, 7358, 00, 620, 0560, 0450, 0001, 0008, 1000, 0500, 0075, 3, 0400, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0034, 'SandDevil', 0001, 0131, 5760, 00, 549, 0497, 0450, 0001, 0008, 1000, 0500, 0070, 3, 0400, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0033, 'CrazySnake', 0001, 0404, 4771, 00, 438, 0394, 0400, 0001, 0008, 1000, 0500, 0065, 3, 0400, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0032, 'SnowApe', 0001, 0303, 3184, 00, 352, 0317, 0350, 0001, 0008, 1000, 0500, 0060, 3, 0400, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0031, 'RoaringApe', 0001, 0356, 2643, 00, 278, 0250, 0300, 0009, 0008, 1000, 0500, 0055, 3, 0400, 1150, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0030, 'SmartMonkey', 0001, 0356, 2199, 00, 217, 0195, 0250, 0009, 0008, 1000, 0500, 0050, 3, 0400, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0029, 'ElfKing', 0001, 0402, 1828, 00, 167, 0149, 0200, 0001, 0008, 1000, 0500, 0045, 3, 0400, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0028, 'BlueElf', 0001, 0402, 999, 00, 143, 0129, 0160, 0001, 0008, 1000, 0500, 0040, 3, 0400, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0027, 'CateranBoss', 0001, 0401, 817, 00, 112, 0102, 0120, 0001, 0008, 1000, 0500, 0035, 3, 0400, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0026, 'FireSnake', 0001, 0403, 667, 00, 92, 0082, 0098, 0001, 0008, 1000, 0500, 0030, 3, 0400, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0025, 'BattleGhost', 0001, 0308, 548, 00, 75, 0067, 0074, 0001, 0008, 1000, 1000, 0025, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0024, 'WarGhost', 0001, 0308, 303, 00, 58, 0053, 0048, 0001, 0008, 1000, 1000, 0020, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0023, 'GameBird', 0001, 0332, 183, 00, 44, 0039, 0034, 0001, 0008, 1000, 1000, 0015, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0022, 'BlueCooer', 0001, 0304, 81, 00, 20, 0019, 0012, 0001, 0008, 1000, 1000, 0010, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0021, 'FlyingRooster', 0001, 0304, 33, 00, 8, 0007, 0004, 0001, 0008, 1000, 1000, 0003, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0051, 'MonolithMonster', 0001, 0136, 7358, 00, 140, 0120, 0000, 0001, 0015, 1000, 0500, 0042, 3, 1000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0055, 'BanditL97', 0001, 0201, 20697, 00, 1203, 0900, 0000, 0001, 0010, 1000, 1000, 0097, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0056, 'BloodyBat', 0001, 0148, 28140, 00, 1602, 1200, 0000, 0001, 0010, 1000, 1000, 0107, 3, 0500, 0000, 100, 729099, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0057, 'BullMonster', 0001, 0117, 35050, 00, 1880, 1300, 0000, 0001, 0010, 1000, 1000, 0112, 3, 0500, 0000, 100, 729100, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0058, 'RedDevilL117', 0001, 0197, 60460, 00, 2228, 1500, 0000, 0001, 0010, 1000, 1000, 0135, 3, 0500, 0000, 100, 723030, 4, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2392, 'DesertThief', 0001, 0801, 10472, 00, 120, 0109, 0098, 0001, 0010, 1000, 1000, 0082, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0089, 'DevilKingL120', 0001, 0197, 65000, 00, 2572, 1732, 1000, 0001, 0020, 1000, 0500, 0120, 3, 0100, 0000, 100, 723030, 4, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0088, 'BloodyDevil', 0001, 0197, 60000, 00, 2455, 1653, 0900, 0001, 0010, 1000, 0500, 0118, 3, 0300, 0000, 100, 723030, 4, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0087, 'VampireKing', 0001, 0148, 55000, 00, 1850, 1386, 1000, 0001, 0020, 1000, 0500, 0113, 3, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0086, 'VampireBat', 0001, 0148, 50000, 00, 1766, 1323, 0900, 0001, 0010, 1000, 0500, 0108, 3, 0300, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0085, 'BanditKing', 0001, 0201, 45000, 00, 1389, 1039, 1000, 0001, 0020, 1000, 0500, 0103, 3, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0084, 'Banditti', 0001, 0201, 40000, 00, 1326, 0992, 0900, 0001, 0010, 1000, 0500, 0098, 3, 0300, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0083, 'RedDevilL118', 0001, 0597, 65535, 00, 2339, 1575, 0000, 0001, 0018, 1000, 0500, 0118, 3, 0100, 0000, 100, 723030, 4, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0082, 'BullMonsterL113', 0001, 0517, 70100, 00, 1974, 1365, 0000, 0001, 0018, 1000, 0500, 0113, 3, 0100, 0000, 100, 729100, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0081, 'BloodyBatL108', 0001, 0548, 56280, 00, 1682, 1260, 0000, 0001, 0018, 1000, 0500, 0108, 3, 0100, 0000, 100, 729099, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0080, 'TombBatL103', 0001, 0606, 48944, 00, 1316, 1191, 0000, 0002, 0018, 1000, 0500, 0103, 3, 0100, 0000, 100, 729098, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0079, 'BanditL98', 0001, 0601, 41394, 00, 1263, 0945, 0000, 0001, 0018, 1000, 0500, 0098, 3, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0078, 'HawkL93', 0001, 0605, 30864, 00, 994, 0898, 0000, 0002, 0018, 1000, 0250, 0093, 3, 0100, 0000, 100, 729095, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0076, 'BladeGhostL83', 0001, 0200, 23346, 00, 726, 0656, 0000, 0002, 0015, 1000, 0250, 0083, 3, 0100, 0000, 100, 729093, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0075, 'RockMonsterL78', 0001, 0505, 16918, 00, 619, 0559, 0000, 0002, 0015, 1000, 0250, 0078, 3, 0100, 0000, 100, 729092, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0074, 'HillMonsterL73', 0001, 0505, 22074, 00, 521, 0471, 0000, 0002, 0015, 1000, 0250, 0073, 3, 0100, 0000, 100, 1080001, 12, 729091, 10, 0, 0);
INSERT INTO `monsterinfos` VALUES (0073, 'SandMonsterL68', 0001, 0531, 17280, 00, 461, 0417, 0000, 0002, 0015, 1000, 0250, 0068, 3, 0100, 0000, 100, 729090, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0072, 'SnakemanL63', 0001, 0604, 14313, 00, 368, 0331, 0000, 0002, 0015, 1000, 0250, 0063, 3, 0100, 0000, 100, 729089, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0071, 'ThunderApeL58', 0001, 0503, 9552, 00, 296, 0266, 0000, 0002, 0015, 1000, 0250, 0058, 3, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0070, 'GiantApeL53', 0001, 0556, 7929, 00, 234, 0210, 0000, 0002, 0015, 1000, 0250, 0053, 3, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0069, 'MacaqueL48', 0001, 0556, 6597, 00, 182, 0164, 0000, 0002, 0015, 1000, 0250, 0048, 3, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0068, 'FireSpiritL43', 0001, 0602, 5484, 00, 140, 0125, 0000, 0002, 0015, 1000, 0250, 0043, 3, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0067, 'FireRatL38', 0001, 0602, 2997, 00, 120, 0109, 0000, 0002, 0015, 1000, 0250, 0038, 3, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0066, 'BanditL33', 0001, 0601, 2451, 00, 94, 0086, 0000, 0002, 0015, 1000, 0250, 0033, 3, 0100, 0000, 100, 729096, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0065, 'WingedSnakeL28', 0001, 0603, 1334, 00, 77, 0069, 0000, 0002, 0015, 1000, 0250, 0028, 3, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0064, 'HeavyGhostL23', 0001, 0508, 1096, 00, 63, 0056, 0000, 0002, 0015, 1000, 0500, 0023, 3, 0200, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0910, 'Guard2', 0001, 0910, 5000, 00, 1, 0001, 0001, 0015, 0015, 1000, 0500, 0120, 15, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0100, 'Skeleton', 0001, 0211, 30000, 00, 2500, 2500, 0030, 0011, 0012, 2000, 1000, 0070, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0101, 'GhostWarrior', 0001, 0211, 20000, 00, 2500, 2500, 0500, 0004, 0015, 1000, 5000, 0080, 3, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0102, 'SkeletonKing', 0001, 0211, 50000, 00, 2500, 2500, 0100, 0012, 0012, 2000, 1000, 0120, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0103, 'GhostAres', 0001, 0211, 60000, 00, 2500, 2500, 0200, 0012, 0012, 2000, 1000, 0120, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0105, 'RockMonsterL17', 0001, 0136, 606, 00, 50, 0042, 0000, 0001, 0010, 1000, 1000, 0017, 3, 1000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0106, 'RockMonsterL42', 0001, 0136, 3656, 00, 146, 0119, 0000, 0001, 0010, 1000, 1000, 0042, 3, 1000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0107, 'RockMonsterL72', 0001, 0136, 14716, 00, 545, 0448, 0000, 0001, 0015, 1000, 1000, 0072, 3, 1000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0108, 'RockMonsterL92', 0001, 0136, 30864, 00, 1040, 0855, 0000, 0001, 0015, 1000, 1000, 0092, 3, 1000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0037, 'BladeMonster', 0001, 0400, 11673, 00, 864, 0780, 0550, 0001, 0008, 1000, 0500, 0085, 3, 0400, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0039, 'SilverHawk', 0001, 0405, 15432, 00, 1183, 1069, 0700, 0001, 0010, 1000, 1000, 0095, 3, 0400, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0040, 'Robber', 0001, 0401, 20697, 00, 1504, 1125, 0800, 0001, 0010, 1000, 1000, 0100, 3, 0400, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0090, 'FlyingBat', 0001, 0406, 24472, 00, 1567, 1418, 0900, 0001, 0010, 1000, 1000, 0105, 3, 0400, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0091, 'BloodyKing', 0001, 0348, 28140, 00, 2003, 1500, 1000, 0001, 0010, 1000, 1000, 0110, 3, 0400, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0092, 'BullDevil', 0001, 0317, 35050, 00, 2350, 1625, 1200, 0001, 0010, 1000, 1000, 0115, 3, 0400, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0093, 'GreenDevil', 0001, 0397, 59460, 00, 2785, 1875, 1200, 0001, 0010, 1000, 1000, 0120, 3, 0400, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3000, 'OddMonster', 0001, 0105, 18205, 00, 2466, 2166, 0712, 0001, 0018, 2000, 0500, 0118, 3, 0100, 0000, 100, 721100, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3024, 'EvilMonkey', 0001, 0156, 4545, 00, 869, 0254, 0302, 0002, 0015, 2000, 0250, 0078, 3, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3023, 'Jumpy', 0001, 0200, 6813, 00, 1324, 0449, 0341, 0002, 0015, 2000, 0250, 0088, 3, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3021, 'CecropsBoss', 0001, 0204, 14335, 00, 2651, 1082, 0597, 0001, 0018, 2000, 0500, 0108, 3, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3020, 'AberrantEvil', 0001, 0197, 18205, 00, 3093, 1275, 0712, 0001, 0018, 2000, 0500, 0118, 3, 0100, 0000, 100, 721108, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3019, 'TyrantBandit', 0001, 0201, 18205, 00, 3093, 1275, 0712, 0001, 0015, 1000, 0500, 0135, 3, 0500, 0000, 100, 721103, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3009, 'BloodGhost', 0001, 0200, 18205, 00, 2000, 1000, 0700, 0001, 0015, 1000, 0500, 0135, 3, 0500, 0000, 100, 721101, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (6000, 'ToughHorn', 0001, 0200, 38500, 00, 1205, 0900, 0600, 0001, 0008, 1000, 0500, 0001, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (6001, 'ToughHorn', 0001, 0200, 38500, 00, 1505, 1100, 0750, 0001, 0008, 1000, 0500, 0001, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (6002, 'ToughHorn', 0001, 0200, 38500, 00, 1205, 0900, 0600, 0001, 0008, 1000, 0500, 0001, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (6003, 'ToughHorn', 0001, 0200, 38500, 00, 1205, 0900, 0600, 0001, 0008, 1000, 0500, 0001, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (6004, 'ToughHorn', 0001, 0200, 38500, 00, 1205, 0900, 0600, 0001, 0008, 1000, 0500, 0001, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (6005, 'ToughHorn', 0001, 0200, 38500, 00, 1205, 0900, 0600, 0001, 0008, 1000, 0500, 0001, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (6006, 'ToughHorn', 0001, 0200, 38500, 00, 2505, 2000, 0600, 0001, 0008, 1000, 0500, 0100, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2997, 'PlainsThief', 0001, 0601, 15864, 00, 120, 0109, 0098, 0001, 0010, 1000, 1000, 0089, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2391, 'ForestThief', 0001, 0401, 10697, 00, 120, 0109, 0098, 0001, 0010, 1000, 1000, 0082, 3, 0000, 0000, 100, 723903, 50, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2390, 'CaveThief', 0001, 0201, 10432, 00, 120, 0109, 0098, 0001, 0010, 1000, 1000, 0082, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3028, 'ShakeMonster', 0001, 0105, 81, 00, 16, 0015, 0200, 0001, 0008, 1000, 0500, 0007, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3027, 'GlumMonster', 0001, 0105, 1000, 00, 3000, 3000, 0400, 0005, 0010, 1000, 0500, 0001, 3, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3026, 'EvilHawk', 0001, 0205, 1000, 00, 8000, 8000, 30000, 0010, 0010, 8000, 0500, 0001, 3, 0100, 3000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3025, 'Pan', 0001, 0201, 1000, 00, 8000, 8000, 0000, 0005, 0010, 2000, 0500, 0001, 67, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3032, 'PoisonousRat', 0001, 0202, 999, 00, 114, 0103, 0000, 0001, 0008, 1000, 0500, 0037, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3029, 'IronEagle', 0001, 0205, 1000, 00, 8000, 8000, 30000, 0010, 0010, 8000, 0500, 0001, 67, 0100, 3000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3050, 'HeresySnakeman', 0001, 0404, 4771, 00, 350, 0315, 0000, 0001, 0008, 1000, 0500, 0062, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (6011, 'Ghost', 0001, 0200, 20673, 00, 691, 0624, 0300, 0001, 0012, 1000, 0500, 0082, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (6010, 'lonelyTyrant', 0001, 0401, 2100, 00, 155, 0125, 0000, 0001, 0008, 1000, 0500, 0045, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (6017, 'ApeMonster', 0001, 0103, 10200, 00, 450, 0410, 0500, 0001, 0003, 1000, 0500, 0060, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2020, '2020', 0001, 0207, 3000, 00, 182, 0122, 1300, 0001, 0008, 1000, 0500, 0055, 35, 0200, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0110, 'WindSnake', 0001, 0203, 400, 00, 73, 0065, 0000, 0001, 0008, 1000, 0500, 0027, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0111, 'AlienBandit', 0001, 0201, 600, 00, 89, 0081, 0000, 0001, 0008, 1000, 0500, 0032, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0112, 'FireRat', 0001, 0202, 800, 00, 114, 0103, 0000, 0001, 0008, 1000, 0500, 0037, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0113, 'FireSpirit', 0001, 0202, 1500, 00, 133, 0119, 0000, 0001, 0008, 1000, 0500, 0042, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3033, 'StoneBandit', 0001, 0105, 8459, 00, 550, 0500, 0500, 0001, 0008, 1000, 0500, 0077, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0120, 'MinMacaque', 0001, 0156, 1759, 00, 173, 0156, 0000, 0001, 0008, 1000, 0500, 0047, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0121, 'ElfApe', 0001, 0156, 2114, 00, 222, 0200, 0000, 0001, 0008, 1000, 0500, 0052, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0122, 'SlowApe', 0001, 0103, 2547, 00, 281, 0253, 0000, 0001, 0008, 1000, 0500, 0057, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0123, 'SnakeMonster', 0001, 0204, 3817, 00, 350, 0315, 0000, 0001, 0008, 1000, 0500, 0062, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0124, 'SandElf', 0001, 0131, 4608, 00, 439, 0397, 0000, 0001, 0008, 1000, 0500, 0067, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0125, 'lapidatingMob', 0001, 0105, 5886, 00, 496, 0448, 0000, 0001, 0008, 1000, 0500, 0072, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0126, 'ToughMonster', 0001, 0105, 6767, 00, 589, 0532, 0000, 0001, 0008, 1000, 0500, 0077, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0127, 'EvilBlade', 0001, 0200, 9338, 00, 691, 0624, 0000, 0001, 0012, 1000, 0500, 0082, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3100, 'FireSnakeSpirit', 0001, 0403, 500, 00, 2000, 1000, 0300, 0005, 0005, 1000, 0250, 0001, 67, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3101, 'ToughSnake', 0001, 0403, 500, 00, 1237, 1237, 0000, 0005, 0010, 2000, 0500, 0001, 3, 0100, 3000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3103, 'WaterSnake', 0001, 0203, 47853, 00, 1704, 1390, 1000, 0015, 0015, 2000, 0250, 0080, 67, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3102, 'SnakeKing', 0001, 0404, 65535, 00, 8000, 8000, 65535, 0020, 0020, 60000, 0500, 0120, 3, 0100, 3000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8100, 'HumanMessenger', 0001, 0108, 1100, 00, 120, 0120, 0025, 0009, 0010, 1300, 0500, 0022, 3, 0500, 3000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8300, 'GhostKing', 0001, 0108, 30000, 00, 160, 0150, 0540, 0001, 0009, 1000, 0500, 0022, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8200, 'HumanAide', 0001, 0108, 1100, 00, 150, 0100, 0086, 0001, 0009, 1000, 0500, 0022, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8101, 'WingedSnakeMsgr', 0001, 0243, 1200, 00, 150, 0150, 0033, 0009, 0010, 1300, 0500, 0027, 3, 0500, 3001, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8301, 'WingedSnakeKing', 0001, 0443, 40000, 00, 200, 0180, 0600, 0001, 0009, 1000, 0500, 0027, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8201, 'WingedSnakeAide', 0001, 0243, 1200, 00, 160, 0150, 0110, 0001, 0009, 1000, 0500, 0027, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8102, 'BanditMessenger', 0001, 0241, 1600, 00, 200, 0200, 0037, 0009, 0010, 1300, 0500, 0032, 3, 0500, 3002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8202, 'BanditAide', 0001, 0241, 1600, 00, 220, 0190, 0136, 0001, 0009, 1000, 0500, 0032, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8302, '8302', 0001, 0241, 45000, 00, 240, 0200, 0640, 0001, 0009, 1000, 0500, 0032, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8203, 'RatAide', 0001, 0242, 2000, 00, 250, 0250, 0175, 0001, 0009, 1000, 0500, 0037, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8103, 'RatMessenger', 0001, 0242, 2000, 00, 260, 0240, 0085, 0009, 0010, 1300, 0500, 0037, 3, 0500, 3003, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8303, 'RatKing', 0001, 0442, 45000, 00, 300, 0270, 0800, 0001, 0009, 1000, 0500, 0037, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8104, 'ElfMessenger', 0001, 0242, 3600, 00, 280, 0280, 0100, 0009, 0010, 1300, 0500, 0042, 3, 0500, 3004, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8304, 'ElfBoss', 0001, 0442, 45000, 00, 330, 0300, 0815, 0001, 0009, 1000, 0500, 0042, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8204, 'ElfAide', 0001, 0242, 3600, 00, 280, 0250, 0205, 0001, 0009, 1000, 0500, 0042, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8305, 'MonkeyKing', 0001, 0196, 45000, 00, 370, 0350, 0860, 0001, 0009, 1000, 0500, 0047, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8105, 'MonkeyMessenger', 0001, 0196, 4400, 00, 300, 0300, 0105, 0009, 0010, 1300, 0500, 0047, 3, 0500, 3005, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8205, 'MonkeyAide', 0001, 0196, 4400, 00, 320, 0280, 0243, 0001, 0009, 1000, 0500, 0047, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8306, 'GiantApeKing', 0001, 0196, 45000, 00, 420, 0400, 1050, 0001, 0009, 1000, 0500, 0052, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8206, 'GiantApeAide', 0001, 0196, 5300, 00, 350, 0310, 0283, 0001, 0009, 1000, 0500, 0052, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8106, 'GiantApeMsgr', 0001, 0196, 5300, 00, 330, 0330, 0140, 0009, 0010, 1300, 0500, 0052, 3, 0500, 3006, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8307, 'ThunderApeKing', 0001, 0103, 45000, 00, 480, 0460, 1140, 0001, 0009, 1000, 0500, 0057, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8107, 'ThunderApeMsgr', 0001, 0103, 6300, 00, 380, 0380, 0270, 0009, 0010, 1300, 0500, 0057, 3, 0500, 3007, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8207, 'ThunderApeAide', 0001, 0103, 6300, 00, 400, 0360, 0378, 0001, 0009, 1000, 0500, 0057, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8108, 'SnakemanMsgr', 0001, 0244, 9500, 00, 450, 0450, 0320, 0009, 0010, 1300, 0500, 0062, 3, 0500, 3008, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8208, 'SnakemanAide', 0001, 0244, 9500, 00, 470, 0430, 0434, 0001, 0009, 1000, 0500, 0062, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8308, 'SnakemanKing', 0001, 0244, 46000, 00, 550, 0530, 1205, 0001, 0009, 1000, 0500, 0062, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8109, 'SandMonsterMsgr', 0001, 0171, 11000, 00, 539, 0539, 0400, 0009, 0010, 1300, 0500, 0067, 3, 0500, 3009, 100, 729090, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8209, 'SandMonsterAide', 0001, 0171, 11000, 00, 550, 0520, 0534, 0001, 0009, 1000, 0500, 0067, 3, 0500, 0000, 100, 729090, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8309, 'SandMonsterKing', 0001, 0171, 47000, 00, 630, 0610, 1668, 0001, 0009, 1000, 0500, 0067, 3, 0500, 0000, 100, 729090, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8310, 'HillMonsterKing', 0001, 0145, 47000, 00, 690, 0670, 1700, 0001, 0009, 1000, 0500, 0072, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8110, 'HillMonsterMsgr', 0001, 0145, 14000, 00, 590, 0590, 0405, 0009, 0010, 1300, 0500, 0072, 3, 0500, 3010, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8210, 'HillMonsterAide', 0001, 0145, 14000, 00, 610, 0570, 0593, 0001, 0009, 1000, 0500, 0072, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8111, 'RockMonsterMsgr', 0001, 0145, 17000, 00, 680, 0680, 0410, 0009, 0010, 1300, 0500, 0077, 3, 0500, 3011, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8311, 'RockMonsterKing', 0001, 0145, 48000, 00, 780, 0760, 1940, 0001, 0009, 1000, 0500, 0077, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8211, 'RockMonsterAide', 0001, 0145, 17000, 00, 700, 0660, 0686, 0001, 0009, 1000, 0500, 0077, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8112, 'BladeGhostMsgr', 0001, 0240, 22000, 00, 760, 0760, 0415, 0009, 0010, 1300, 0500, 0082, 3, 0500, 3012, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8312, 'BladeGhostKing', 0001, 0240, 50000, 00, 860, 0840, 1968, 0001, 0009, 1000, 0500, 0082, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8212, 'BladeGhostAide', 0001, 0240, 22000, 00, 780, 0740, 0769, 0001, 0009, 1000, 0500, 0082, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8314, 'HawkBoss', 0001, 0245, 65535, 00, 990, 0990, 1730, 0001, 0009, 1000, 0500, 0092, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8114, 'HawkMessenger', 0001, 0245, 30000, 00, 890, 0890, 0425, 0009, 0010, 1000, 0500, 0092, 3, 0500, 3014, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8214, 'HawkAide', 0001, 0245, 30000, 00, 910, 0870, 1059, 0001, 0009, 1000, 0500, 0092, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8115, 'RobberMessenger', 0001, 0241, 40000, 00, 1050, 1050, 0430, 0009, 0010, 1300, 0500, 0097, 3, 0500, 3015, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8315, 'RobberKing', 0001, 0241, 65535, 00, 1150, 1150, 1850, 0001, 0009, 1000, 0500, 0097, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8215, 'RobberAide', 0001, 0241, 40000, 00, 1100, 1020, 1252, 0001, 0009, 1000, 0500, 0097, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8116, 'BatMessenger', 0001, 0246, 45000, 00, 1200, 1200, 0435, 0009, 0010, 1300, 0500, 0102, 3, 0500, 3016, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8216, 'BatAide', 0001, 0246, 45000, 00, 1250, 1150, 1375, 0001, 0009, 1000, 0500, 0102, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8316, 'BatKing', 0001, 0246, 65535, 00, 1350, 1350, 30000, 0001, 0009, 1000, 0500, 0102, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8217, 'BloodyBatAide', 0001, 0188, 50000, 00, 1650, 1550, 1630, 0001, 0009, 1000, 0500, 0107, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8317, 'BloodyWing', 0001, 0188, 65535, 00, 1750, 1750, 30000, 0001, 0009, 1000, 0500, 0107, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8117, 'BloodyBatMsgr', 0001, 0188, 50000, 00, 1600, 1600, 0440, 0009, 0010, 1300, 0500, 0107, 3, 0500, 3017, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8318, 'BullKing', 0001, 0117, 65535, 00, 2000, 2000, 30000, 0001, 0009, 1000, 0500, 0112, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8118, 'BullMessenger', 0001, 0117, 55000, 00, 1800, 1800, 0445, 0009, 0010, 1300, 0500, 0112, 3, 0500, 3018, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8218, 'BullAide', 0001, 0117, 55000, 00, 1900, 1700, 1979, 0001, 0009, 1000, 0500, 0112, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8219, 'DevilAide', 0001, 0197, 60460, 00, 2250, 2050, 4000, 0001, 0009, 0700, 0500, 0117, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8119, 'DevilMessenger', 0001, 0197, 60460, 00, 2150, 2150, 2000, 0009, 0010, 1000, 0500, 0117, 3, 0500, 3019, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8319, 'Evillord', 0001, 0197, 65535, 00, 2400, 2250, 40000, 0001, 0009, 0600, 0500, 0117, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0003, 'Robin', 0001, 0132, 183, 00, 35, 0031, 0000, 0001, 0008, 1000, 0500, 0012, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0004, 'Apparition', 0001, 0108, 303, 00, 46, 0042, 0000, 0001, 0008, 1000, 0500, 0017, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7005, 'WaterDevil', 0001, 0205, 38550, 00, 91, 0091, 65000, 0008, 0008, 1000, 0500, 0070, 3, 0500, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7004, 'FireDevil', 0001, 0205, 38550, 00, 182, 0182, 65000, 0008, 0008, 1000, 0500, 0070, 3, 0500, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7003, 'ArcherDevil', 0001, 0205, 33964, 00, 470, 0500, 65000, 0001, 0008, 1000, 0500, 0070, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7002, 'TrojanDevil', 0001, 0205, 9436, 00, 396, 0420, 0700, 0001, 0008, 1000, 0500, 0070, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7001, 'WarriorDevil', 0001, 0205, 23588, 00, 650, 0700, 0700, 0001, 0008, 1000, 0500, 0070, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7000, 'DevilGuard', 0001, 0405, 6590, 00, 4800, 5150, 0000, 0008, 0008, 4000, 0500, 0130, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3120, 'GuildBeast', 0001, 0240, 65535, 00, 5000, 5000, 60000, 0010, 0010, 3000, 0500, 0120, 1, 0250, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (1400, 'HugeSnake', 0001, 0203, 1828, 00, 60, 0060, 0020, 0001, 0008, 2000, 0500, 0042, 3, 0500, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (1401, 'Banditleader', 0001, 0201, 2643, 00, 100, 0100, 0020, 0001, 0008, 2000, 0500, 0052, 3, 0500, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (1402, 'HugeSpirit', 0001, 0202, 3184, 00, 160, 0160, 0050, 0001, 0008, 2000, 0500, 0057, 3, 0400, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (1403, 'CateranSoldier', 0001, 0401, 4771, 00, 220, 0220, 0050, 0001, 0008, 2000, 0500, 0062, 3, 0400, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (1404, 'SeniorCateran', 0001, 0401, 5760, 00, 260, 0260, 0080, 0001, 0008, 2000, 0500, 0067, 3, 0400, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (1405, 'Cateranleader', 0001, 0211, 7358, 00, 300, 0300, 0080, 0001, 0008, 2000, 0500, 0072, 3, 0400, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (1406, 'ChiefCateran', 0001, 0211, 8459, 00, 360, 0360, 0100, 0001, 0008, 2000, 0500, 0078, 3, 0400, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (1407, 'HugeApe', 0001, 0156, 11673, 00, 440, 0440, 0100, 0001, 0008, 2000, 0500, 0082, 3, 0400, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (1408, 'SeniorApe', 0001, 0103, 13422, 00, 500, 0500, 0150, 0009, 0008, 2000, 0500, 0088, 3, 0400, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (1409, 'AlienApe', 0001, 0503, 15432, 00, 600, 0600, 0150, 0001, 0010, 2000, 1000, 0092, 3, 0400, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (1410, 'SeniorSnakeman', 0001, 0204, 20697, 00, 620, 0620, 0200, 0001, 0010, 2000, 1000, 0097, 3, 0400, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (1411, 'Serpent', 0001, 0404, 24472, 00, 700, 0700, 0200, 0001, 0010, 2000, 1000, 0102, 3, 0400, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (1412, 'SeniorSerpent', 0001, 0604, 28140, 00, 800, 0800, 0250, 0001, 0010, 2000, 1000, 0107, 3, 0400, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (1413, 'AlienSerpent', 0001, 0244, 35050, 00, 900, 0900, 0250, 0001, 0010, 2000, 1000, 0112, 3, 0400, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (1414, 'Basilisk', 0001, 0244, 100000, 00, 1000, 1000, 0300, 0001, 0010, 2000, 1000, 0145, 3, 0400, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8415, 'MeteorDove', 0001, 0304, 1000, 00, 89, 0081, 1400, 0001, 0008, 1000, 0500, 0032, 3, 0500, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8416, 'MeteorDove', 0001, 0304, 1000, 00, 89, 0081, 0000, 0001, 0008, 1000, 0500, 0032, 3, 0500, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8417, 'EvilGhost', 0001, 0274, 48944, 00, 500, 0500, 1000, 0002, 0018, 1000, 0500, 0103, 3, 0100, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8418, 'NightDevil', 0001, 0277, 60460, 00, 2000, 2000, 3000, 0001, 0010, 1000, 1000, 0120, 3, 0400, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8419, 'WaterDevil', 0001, 0273, 30000, 00, 700, 0700, 1200, 0001, 0010, 1000, 1000, 0120, 3, 0400, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8420, 'AlienWaterDevil', 0001, 0273, 30000, 00, 4000, 3000, 0000, 0001, 0010, 1000, 1000, 0120, 3, 0400, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0200, 'DevilKing', 0001, 0197, 65000, 00, 800, 0800, 0000, 0001, 0010, 2000, 0500, 0120, 3, 0100, 1002, 100, 723030, 4, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0201, 'DevilKingl122', 0001, 0197, 65000, 00, 900, 0900, 0000, 0001, 0010, 2000, 0500, 0122, 3, 0100, 1002, 100, 723030, 4, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (9000, 'IronEscort', 0001, 0920, 10000, 00, 1480, 1480, 4050, 0004, 0004, 1000, 0050, 0070, 39, 0045, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (9001, 'CopperEscort', 0001, 0920, 20000, 00, 2690, 2690, 7070, 0004, 0004, 1000, 0050, 0090, 39, 0055, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (9002, 'SilverEscort', 0001, 0920, 35000, 00, 4760, 4760, 15050, 0004, 0004, 1000, 0050, 0110, 39, 0065, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (9003, 'GoldEscort', 0001, 0920, 60000, 00, 6460, 6460, 16080, 0004, 0004, 1000, 0050, 0120, 39, 0075, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (9005, 'GhostBat', 0001, 0148, 3000, 00, 28, 0028, 0050, 0005, 0015, 2000, 0050, 0015, 39, 0050, 4030, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (9006, 'FastBat', 0001, 0148, 6000, 00, 66, 0066, 0200, 0005, 0015, 2000, 0050, 0040, 39, 0050, 4030, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (9007, 'SwiftBat', 0001, 0148, 15000, 00, 195, 0195, 0800, 0005, 0015, 2000, 0050, 0070, 39, 0050, 4030, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (9008, 'MagicBat', 0001, 0148, 30000, 00, 448, 0448, 1500, 0005, 0015, 2000, 0050, 0100, 39, 0050, 4030, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (9010, 'GhostBatBoss', 0001, 0548, 10000, 00, 28, 0028, 0050, 0005, 0015, 2000, 0050, 0015, 39, 0050, 4030, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (9011, 'FastBatBoss', 0001, 0548, 20000, 00, 66, 0066, 0200, 0005, 0015, 2000, 0050, 0040, 39, 0050, 4030, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (9012, 'SwiftBatBoss', 0001, 0548, 35000, 00, 195, 0195, 0800, 0005, 0015, 2000, 0050, 0070, 39, 0050, 4030, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (9013, 'MagicBatBoss', 0001, 0548, 60000, 00, 448, 0448, 1500, 0005, 0015, 2000, 0050, 0100, 39, 0050, 4030, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (9015, 'EvilBatA', 0001, 0206, 3000, 00, 14, 0014, 0050, 0001, 0015, 2000, 0050, 0015, 39, 0050, 4040, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (9016, 'EvilBatB', 0001, 0206, 6000, 00, 50, 0030, 0200, 0001, 0015, 2000, 0050, 0040, 39, 0050, 4040, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (9017, 'EvilBatC', 0001, 0206, 15000, 00, 90, 0050, 0800, 0001, 0015, 2000, 0050, 0070, 39, 0050, 4040, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (9018, 'EvilBatD', 0001, 0206, 30000, 00, 130, 0070, 1500, 0001, 0015, 2000, 0050, 0100, 39, 0050, 4040, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (9020, 'FireRatA', 0001, 0209, 3000, 00, 16, 0016, 0050, 0008, 0015, 0700, 0050, 0015, 39, 0050, 1150, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (9021, 'FireRatB', 0001, 0209, 6000, 00, 60, 0060, 0300, 0008, 0015, 0700, 0050, 0040, 39, 0050, 1150, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (9022, 'FireRatC', 0001, 0209, 15000, 00, 130, 0130, 1000, 0008, 0015, 0700, 0050, 0070, 39, 0050, 1150, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (9023, 'FireRatD', 0001, 0209, 30000, 00, 400, 0400, 2000, 0008, 0015, 0700, 0050, 0100, 39, 0050, 1150, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (9025, 'SkeletonA', 0001, 0211, 10000, 00, 14, 0014, 0050, 0003, 0015, 1500, 0050, 0015, 39, 0050, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (9026, 'SkeletonB', 0001, 0211, 20000, 00, 50, 0030, 0300, 0003, 0015, 1500, 0050, 0040, 39, 0050, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (9027, 'SkeletonC', 0001, 0211, 35000, 00, 90, 0050, 1500, 0003, 0015, 1500, 0050, 0070, 39, 0050, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (9028, 'SkeletonD', 0001, 0211, 60000, 00, 130, 0070, 2000, 0003, 0015, 1500, 0050, 0100, 39, 0050, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8500, 'Waterlord', 0001, 0273, 60000, 00, 1400, 1400, 2400, 0001, 0010, 1000, 1000, 0120, 3, 0400, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3136, 'Titanl79', 0001, 0153, 50800, 00, 79, 0079, 0908, 0009, 0008, 1000, 0500, 0077, 3, 0500, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3137, 'Titanl81', 0001, 0153, 54312, 00, 231, 0231, 0908, 0002, 0015, 1000, 0500, 0077, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3141, 'Slinger', 0001, 0145, 32156, 00, 1302, 1205, 0100, 0001, 0008, 1000, 0500, 0103, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3135, 'TitanL75', 0001, 0153, 26719, 00, 543, 0488, 0916, 0002, 0015, 1000, 0500, 0077, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3134, 'TitanL77', 0001, 0153, 42549, 00, 439, 0394, 0908, 0002, 0015, 1000, 0500, 0077, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3133, 'GanodermaL76', 0001, 0133, 45672, 00, 231, 0208, 0716, 0002, 0015, 1000, 0500, 0072, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3132, 'GanodermaL74', 0001, 0133, 40000, 00, 76, 0076, 0716, 0009, 0008, 1000, 0500, 0072, 3, 0500, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3131, 'GanodermaL70', 0001, 0133, 26478, 00, 509, 0458, 0724, 0002, 0015, 1000, 0500, 0072, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3130, 'GanodermaL72', 0001, 0133, 39796, 00, 437, 0393, 0716, 0002, 0015, 1000, 0500, 0072, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3155, 'MinotaurL120', 0001, 0117, 60460, 00, 2328, 1600, 0100, 0001, 0008, 1000, 0500, 0120, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3156, 'Howler', 0001, 0130, 65535, 00, 2550, 2500, 3800, 0001, 0008, 1000, 0500, 0125, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3145, 'AgileRat', 0001, 0442, 58110, 00, 1800, 1800, 0545, 0001, 0009, 1000, 0500, 0112, 3, 0500, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3144, 'Bladeling', 0001, 0240, 33140, 00, 1702, 1300, 0100, 0001, 0009, 1000, 0500, 0110, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3143, 'Gibbon', 0001, 0127, 65535, 00, 2350, 2250, 3500, 0001, 0008, 1000, 0500, 0110, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3142, 'GoldGhost', 0001, 0308, 57110, 00, 1523, 1385, 0120, 0001, 0008, 1000, 0500, 0108, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3322, '3322', 0001, 0224, 3000, 00, 182, 0122, 1300, 0001, 0008, 1000, 0500, 0055, 35, 0200, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3320, 'KungFuBunny', 0001, 0222, 6000000, 00, 150000, 15000, 3500, 0001, 0008, 1000, 0500, 0150, 3, 0200, 1115, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (1610, 'Goblin', 0001, 0133, 33, 00, 6, 0005, 0000, 0001, 0008, 1000, 0500, 0001, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3553, 'ManaApe', 0001, 0156, 40000, 00, 780, 0760, 1940, 0001, 0009, 1000, 0500, 0077, 3, 1000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3552, 'DexterityApe', 0001, 0156, 40000, 00, 780, 0760, 1940, 0001, 0009, 1000, 0500, 0077, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3551, 'StrengthApe', 0001, 0156, 35000, 00, 780, 0760, 2000, 0001, 0009, 1000, 0500, 0077, 3, 1000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3550, 'lifeApe', 0001, 0156, 60000, 00, 780, 0760, 1200, 0001, 0009, 1000, 0500, 0077, 3, 1000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3526, 'FireBat', 0001, 0197, 79800, 00, 860, 0840, 1968, 0001, 0009, 1000, 0500, 0122, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7006, 'Guild Beast', 0001, 0279, 65535, 00, 5000, 5000, 60000, 0010, 0010, 3000, 3000, 0120, 0, 3000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3149, 'Talon', 0001, 0225, 65535, 00, 2650, 2550, 3700, 0001, 0018, 1000, 1000, 0120, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3148, 'FiendBat', 0001, 0606, 60460, 00, 2328, 1600, 4000, 0002, 0018, 1000, 0500, 0117, 3, 0100, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3146, 'Nagalord', 0001, 0162, 65535, 00, 2550, 2450, 3600, 0001, 0009, 1000, 0500, 0115, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (5045, 'UndeadSoldier', 0001, 0263, 3451, 00, 2114, 1903, 4311, 0001, 0020, 2000, 0500, 0120, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (5046, 'Phantom', 0001, 0363, 3451, 00, 2114, 1903, 4311, 0001, 0020, 2000, 0500, 0120, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (5049, 'UndeadSpearman', 0001, 0165, 5176, 00, 2114, 1903, 5174, 0003, 0025, 1200, 0500, 0121, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (5050, 'Revenant', 0001, 0265, 5136, 00, 2115, 1903, 5175, 0003, 0025, 1200, 0500, 0121, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (5051, 'Eidolon', 0001, 0266, 4108, 00, 2643, 2379, 4743, 0006, 0025, 1200, 0500, 0123, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (5053, 'Temptress', 0001, 0230, 6794, 00, 2317, 2085, 4314, 0001, 0025, 0600, 0500, 0122, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (5054, 'Centicore', 0001, 0179, 6794, 00, 2317, 2085, 4314, 0001, 0025, 0500, 0500, 0122, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (5055, 'Wraith', 0001, 0327, 3934, 00, 569, 0569, 1724, 0005, 0025, 2500, 0500, 0122, 3, 0500, 4075, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (5056, 'HellTroll', 0001, 0353, 20368, 00, 2316, 2085, 5175, 0002, 0025, 1000, 0500, 0123, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (5057, 'Naga', 0001, 0262, 6794, 00, 2317, 2085, 4314, 0003, 0025, 1000, 0500, 0122, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (5058, 'Syren', 0001, 0152, 20707, 00, 985, 0985, 7473, 0010, 0025, 2000, 0500, 0124, 3, 0500, 4076, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (5059, 'UltimatePluto', 0001, 0168, 65535, 00, 985, 0985, 7927, 0003, 0025, 1000, 0500, 0126, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (5060, 'HellWraith', 0001, 0327, 3934, 00, 569, 0569, 1724, 0005, 0025, 2000, 0500, 0124, 3, 0500, 4075, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3138, 'RareBox!', 0001, 0141, 65535, 00, 5000, 5000, 9000, 0010, 0010, 3000, 3000, 0130, 65, 3000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3584, 'Siren', 0001, 0310, 500, 00, 20, 0019, 0012, 0001, 0008, 1000, 1000, 0010, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3583, 'MagentaCock', 0001, 0314, 500, 00, 20, 0019, 0012, 0001, 0008, 1000, 1000, 0010, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3582, 'BlueCock', 0001, 0313, 500, 00, 20, 0019, 0012, 0001, 0008, 1000, 1000, 0010, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2996, 'ThiefLeader', 0001, 0241, 20459, 00, 120, 0109, 0098, 0003, 0000, 5000, 0000, 0000, 3, 0000, 0000, 100, 723903, 100, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7696, 'HeavenRoc', 0001, 0997, 150000, 00, 2100, 1000, 0098, 0020, 0020, 1000, 1000, 0100, 3, 1000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3561, 'Turkey', 0001, 0304, 3500, 00, 20, 0019, 0012, 0001, 0008, 1000, 1000, 0010, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3564, 'SandMonster', 0001, 0309, 500, 00, 20, 0019, 0012, 0001, 0008, 1000, 1000, 0010, 3, 0500, 0000, 100, 729090, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3563, 'FireSnake', 0001, 0307, 500, 00, 20, 0019, 0012, 0001, 0008, 1000, 1000, 0010, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3560, 'StoneMonster', 0001, 0306, 500, 00, 20, 0019, 0012, 0001, 0008, 1000, 1000, 0010, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3585, 'BrownCock', 0001, 0312, 500, 00, 20, 0019, 0012, 0001, 0008, 1000, 1000, 0010, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3586, 'FishA', 0001, 0311, 500, 00, 20, 0019, 0012, 0001, 0008, 1000, 1000, 0010, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3600, 'ValeDemon', 0001, 0127, 50000, 00, 1728, 1400, 0000, 0001, 0015, 1000, 0500, 0120, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3601, 'MudDemon', 0001, 0227, 50000, 00, 1728, 1400, 0000, 0001, 0015, 1000, 0500, 0120, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3602, 'AbyssDemon', 0001, 0327, 50000, 00, 1728, 1400, 0000, 0003, 0015, 1000, 0500, 0120, 3, 0500, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3603, 'DemonLord', 0001, 0127, 50000, 00, 2328, 1600, 0100, 0003, 0020, 1000, 0500, 0120, 3, 0500, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3604, 'SingingSerpent', 0001, 0155, 50000, 00, 1728, 1400, 0000, 0001, 0015, 1000, 0500, 0120, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3605, 'RoaringSerpent', 0001, 0355, 50000, 00, 1728, 1400, 0000, 0001, 0015, 1000, 0500, 0120, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3606, 'CryingSerpant', 0001, 0255, 1000000, 00, 2328, 1600, 1700, 0003, 0015, 1000, 0500, 0500, 3, 0500, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3607, 'SerpentSpirit', 0001, 0355, 50000, 00, 2328, 1600, 0100, 0003, 0020, 1000, 0500, 0120, 3, 0500, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3608, 'BlueFiend', 0001, 0326, 50000, 00, 1728, 1400, 0000, 0001, 0015, 1000, 0500, 0120, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3609, 'RedFiend', 0001, 0426, 50000, 00, 1728, 1400, 0000, 0001, 0015, 1000, 0500, 0120, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3610, 'WhiteFiend', 0001, 0226, 50000, 00, 1728, 1400, 0000, 0003, 0015, 1000, 0500, 0120, 3, 0500, 5040, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3611, 'FiendLord', 0001, 0126, 50000, 00, 2328, 1600, 0100, 0003, 0020, 1000, 0500, 0120, 3, 0500, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3612, 'WindApe', 0001, 0325, 50000, 00, 1728, 1400, 0000, 0001, 0015, 1000, 0500, 0120, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3613, 'RainApe', 0001, 0425, 50000, 00, 1728, 1400, 0000, 0001, 0015, 1000, 0500, 0120, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3614, 'lightningApe', 0001, 0225, 50000, 00, 1728, 1400, 0000, 0003, 0015, 1000, 0500, 0120, 3, 0500, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3615, 'FuryApe', 0001, 0225, 50000, 00, 2328, 1600, 0100, 0003, 0020, 1000, 0500, 0120, 3, 0500, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3616, 'WoodHades', 0001, 0160, 50000, 00, 1728, 1400, 0000, 0001, 0015, 1000, 0500, 0120, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3617, 'WaterHades', 0001, 0260, 50000, 00, 1728, 1400, 0000, 0003, 0015, 1000, 0500, 0120, 3, 0500, 1250, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3618, 'EarthHades', 0001, 0360, 50000, 00, 1728, 1400, 0000, 0001, 0015, 1000, 0500, 0120, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3619, 'HadesLord', 0001, 0260, 50000, 00, 2328, 1600, 0100, 0003, 0020, 1000, 0500, 0120, 3, 0500, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3620, 'FuryBat', 0001, 0157, 50000, 00, 1728, 1400, 0000, 0001, 0015, 1000, 0500, 0120, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3621, 'CruelBat', 0001, 0257, 50000, 00, 1728, 1400, 0000, 0001, 0015, 1000, 0500, 0120, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3622, 'ViciousBat', 0001, 0357, 50000, 00, 1728, 1400, 0000, 0003, 0015, 1000, 0500, 0120, 3, 0500, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3623, 'DemonBat', 0001, 0357, 50000, 00, 2328, 1600, 0100, 0003, 0020, 1000, 0500, 0120, 3, 0500, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3624, 'BoneSkeleton', 0001, 0163, 50000, 00, 1728, 1400, 0000, 0001, 0015, 1000, 0500, 0120, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3625, 'LameSkeleton', 0001, 0363, 50000, 00, 1728, 1400, 0000, 0003, 0015, 1000, 0500, 0120, 3, 0500, 7109, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3626, 'CarrionSkeleton', 0001, 0263, 50000, 00, 1728, 1400, 0000, 0001, 0015, 1000, 0500, 0120, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3627, 'FurySkeleton', 0001, 0463, 50000, 00, 2328, 1600, 0100, 0003, 0020, 1000, 0500, 0120, 3, 0500, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3628, 'AngryBeast', 0001, 0154, 50000, 00, 1728, 1400, 0000, 0001, 0015, 1000, 0500, 0120, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3629, 'SensitiveBeast', 0001, 0254, 50000, 00, 1728, 1400, 0000, 0001, 0015, 1000, 0500, 0120, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3630, 'CrazyBeast', 0001, 0354, 50000, 00, 1728, 1400, 0000, 0003, 0015, 1000, 0500, 0120, 3, 0500, 7010, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3631, 'FearlessBeast', 0001, 0354, 50000, 00, 2328, 1600, 0100, 0003, 0020, 1000, 0500, 0120, 3, 0500, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3632, 'HillSpirit', 0001, 0153, 50000, 00, 3394, 1066, 0100, 0003, 0025, 1000, 0500, 0120, 3, 0500, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3633, 'SwiftDevil', 0001, 0133, 55000, 00, 4098, 1285, 0120, 0003, 0027, 1000, 0500, 0120, 3, 0500, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3634, 'Banshee', 0001, 0252, 60460, 00, 4566, 1590, 0150, 0003, 0029, 1000, 0500, 0120, 3, 0500, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3635, 'CleansingDevil', 0001, 0168, 65535, 00, 5394, 3098, 0200, 0003, 0030, 1000, 0500, 0120, 3, 0500, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3636, 'Andrew', 0001, 0361, 65535, 00, 4694, 2066, 0350, 0001, 0030, 1000, 0500, 0120, 3, 1000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3637, 'Peter', 0001, 0362, 65535, 00, 4694, 2066, 0350, 0001, 0030, 1000, 0500, 0120, 3, 1000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3638, 'Philip', 0001, 0268, 65535, 00, 4694, 2066, 0350, 0001, 0030, 1000, 0500, 0120, 3, 1000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3639, 'Timothy', 0001, 0151, 65535, 00, 4694, 2066, 0350, 0001, 0030, 1000, 0500, 0120, 3, 1000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3640, 'Daphne', 0001, 0130, 65535, 00, 4694, 2066, 0350, 0001, 0030, 1000, 0500, 0120, 3, 1000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3641, 'Victoria', 0001, 0328, 65535, 00, 4694, 2066, 0350, 0001, 0030, 1000, 0500, 0120, 3, 1000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3642, 'Wayne', 0001, 0129, 65535, 00, 4694, 2066, 0350, 0001, 0030, 1000, 0500, 0120, 3, 1000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3643, 'Theodore', 0001, 0164, 65535, 00, 4694, 2066, 0350, 0001, 0030, 1000, 0500, 0120, 3, 1000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3644, 'Satan', 0001, 0166, 60000, 00, 1200, 0600, 16800, 0002, 0030, 0500, 0500, 0150, 39, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3645, 'BeastSatan', 0001, 0200, 65535, 00, 1200, 0600, 2538, 0003, 0035, 1000, 1000, 0120, 3, 1000, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3647, 'RulzanSatan', 0001, 0104, 65535, 00, 1200, 0600, 3450, 0003, 0035, 1000, 1000, 0150, 3, 1000, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3708, 'MagentaRooster', 0001, 0382, 10000, 00, 20, 0019, 0010, 0001, 0008, 1000, 1000, 0010, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3707, 'BlueRooster', 0001, 0381, 10000, 00, 15, 0014, 0008, 0001, 0008, 1000, 1000, 0010, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3706, 'GreenRooster', 0001, 0380, 10000, 00, 10, 0009, 0006, 0001, 0008, 1000, 1000, 0010, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3579, 'FishB', 0001, 0311, 500, 00, 20, 0019, 0012, 0001, 0008, 1000, 1000, 0010, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3580, 'Mysticlady', 0001, 0310, 500, 00, 20, 0019, 0012, 0001, 0008, 1000, 1000, 0010, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3190, 'DemonDiamonds', 0001, 0748, 1000000, 00, 1766, 1323, 0900, 0001, 0010, 1000, 0500, 0145, 3, 0300, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (4032, 'ElfKing1', 0001, 0247, 65535, 00, 3950, 2850, 7927, 0003, 0025, 1000, 0500, 0126, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (4034, 'HillMonsterKin', 0001, 0249, 65535, 00, 3950, 2850, 7927, 0003, 0025, 1000, 0500, 0126, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (4036, 'WingedSnakeKin', 0001, 0258, 65535, 00, 3950, 2850, 7927, 0003, 0025, 1000, 0500, 0126, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (4058, 'Piglet', 0001, 0215, 50000, 00, 2328, 1600, 0100, 0003, 0020, 1000, 0500, 0120, 3, 0500, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (4060, 'PinkPiglet', 0001, 0215, 50000, 00, 0, 0000, 0000, 0000, 0000, 1000, 0500, 0600, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3035, 'ArenaSentinel', 0001, 0900, 5000, 00, 380, 0330, 0000, 0001, 0010, 1000, 0500, 0070, 35, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2157, 'GreenSnake', 0001, 0404, 65535, 00, 2000, 1500, 0100, 0003, 0020, 1000, 0500, 0110, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2179, 'BlizzardBat', 0001, 0357, 2100, 00, 120, 0119, 0012, 0001, 0008, 1000, 1000, 0047, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2231, 'RedBull', 0001, 0517, 35535, 00, 1974, 1365, 0000, 0001, 0018, 1000, 0500, 0113, 3, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2232, 'BlueBull', 0001, 0317, 65535, 00, 2350, 1625, 1200, 0001, 0010, 1000, 1000, 0115, 3, 0400, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3648, 'Epic Beast', 0001, 0168, 365535, 00, 6938, 5938, 2538, 0003, 0035, 1000, 1000, 0120, 3, 1000, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (4153, 'TeratoDragonF5', 0001, 0950, 50000000, 00, 30000, 30000, 4000, 0014, 0015, 1500, 0500, 0138, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (4171, 'SnowBanshee', 0001, 0951, 60000000, 00, 20000, 20000, 20000, 0014, 0015, 1000, 4000000000, 0138, 3, 4000000000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2416, 'IcyBladeDevil', 0001, 0329, 60460, 00, 2328, 1600, 0100, 0001, 0008, 1000, 0500, 0130, 3, 0500, 0000, 100, 729102, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2417, 'BladeDevillord', 0001, 0325, 60460, 00, 2328, 1600, 0100, 0001, 0008, 1000, 0500, 0130, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2418, 'BladeDevilKing', 0001, 0155, 60460, 00, 2328, 1600, 0100, 0001, 0008, 1000, 0500, 0130, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2419, 'FrostBladeDevil', 0001, 0154, 60460, 00, 2328, 1600, 0100, 0001, 0008, 1000, 0500, 0130, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (4172, 'ThrillingSpook', 0001, 0952, 30000000, 00, 6000, 5000, 0650, 0001, 0010, 1500, 0500, 0020, 3, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (4170, 'SwordMaster', 0001, 0953, 1500000, 00, 60000, 50000, 0650, 0001, 0010, 1500, 0500, 0200, 3, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0001, 'Pheasant', 0001, 0104, 33, 00, 6, 0005, 0000, 0001, 0006, 1000, 0500, 0001, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2420, 'Demon', 0001, 0209, 33, 00, 6, 0005, 0000, 0000, 0006, 1000, 0100, 0001, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2421, 'AncientDemon', 0001, 0756, 33, 00, 6, 0005, 0000, 0000, 0006, 1000, 0100, 0001, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2422, 'FloodDemon', 0001, 0377, 33, 00, 6, 0005, 0000, 0000, 0006, 1000, 0100, 0001, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2423, 'HeavenDemon', 0001, 0163, 33, 00, 6, 0005, 0000, 0000, 0006, 1000, 0100, 0001, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2424, 'ChaosDemon', 0001, 0327, 33, 00, 6, 0005, 0000, 0000, 0006, 1000, 0100, 0001, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2425, 'ScaredDemon', 0001, 0265, 33, 00, 6, 0005, 0000, 0000, 0006, 1000, 0100, 0001, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (9878, 'Dragon King', 0001, 0950, 500000000, 00, 43541, 35415, 12484, 0010, 0010, 1000, 0100, 0000, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (9877, 'Violet Demon', 0001, 0597, 1642640, 00, 18354, 16845, 4821, 0002, 0010, 1000, 0100, 0000, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (9876, 'Red Demon', 0001, 0197, 1624020, 00, 15483, 14528, 2460, 0002, 0010, 1000, 0100, 0000, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (9879, 'Reviver', 0001, 0900, 01, 01, 2, 0001, 0000, 0012, 0012, 5000, 5000, 0255, 3, 5000, 1110, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (159871, 'DragonSon1', 0001, 0950, 5000000, 00, 60000, 50000, 19999, 0001, 0006, 1000, 0500, 0150, 3, 0500, 10361, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (159872, 'DragonSon2', 0001, 0950, 5000000, 00, 60000, 50000, 19999, 0001, 0006, 1000, 0500, 0150, 3, 0500, 10361, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (159873, 'DragonSon3', 0001, 0950, 5000000, 00, 60000, 50000, 19999, 0001, 0006, 1000, 0500, 0150, 3, 0500, 10361, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (159874, 'DragonSon4', 0001, 0950, 5000000, 00, 60000, 50000, 19999, 0001, 0006, 1000, 0500, 0150, 3, 0500, 10361, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (159875, 'CopraDragon', 0001, 0950, 5000000, 00, 60000, 50000, 19999, 0001, 0006, 1000, 0500, 0150, 3, 0500, 10361, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7323, 'SnowDemon', 0001, 0979, 50000000, 00, 40000, 40000, 65000, 0001, 0010, 1500, 0500, 0200, 3, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7484, 'EvilMonkMisery', 0001, 0981, 50000000, 00, 60000, 50000, 65000, 0001, 0010, 1500, 0500, 0200, 3, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7485, 'FlameDevastator', 0001, 0983, 15000000, 00, 60000, 50000, 65000, 0001, 0010, 1500, 0500, 0200, 3, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7487, 'FuriousDevastato', 0001, 0978, 15000000, 00, 60000, 50000, 65000, 0001, 0010, 1500, 0500, 0200, 3, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7482, 'FuriousDevil', 0001, 0976, 15000000, 00, 60000, 50000, 65000, 0001, 0010, 1500, 0500, 0200, 3, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7559, 'VoltaicWarg', 0001, 0850, 9999, 00, 0, 0000, 0000, 0000, 0000, 0000, 0000, 0001, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7554, 'LoveFox', 0001, 0259, 9999, 00, 0, 0000, 0000, 0000, 0000, 0000, 0000, 0001, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7558, 'PandaBattlemaste', 0001, 0848, 9999, 00, 0, 0000, 0000, 0000, 0000, 0000, 0000, 0001, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7555, 'CranePrincess', 0001, 0847, 9999, 00, 0, 0000, 0000, 0000, 0000, 0000, 0000, 0001, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7557, 'WarPanda', 0001, 0849, 9999, 00, 0, 0000, 0000, 0000, 0000, 0000, 0000, 0001, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (4220, 'NemesisTyrant', 0001, 0984, 90000000, 00, 20000, 20000, 100000, 0014, 0015, 0500, 0500, 0140, 3, 1000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7486, 'AwakeDevastator', 0001, 0977, 15000000, 00, 30000, 30000, 65000, 0001, 0010, 1500, 0500, 0200, 3, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7551, 'TeratoDragonF5', 0001, 0950, 50000000, 00, 30000, 30000, 40000, 0005, 0010, 5000, 0500, 0200, 3, 0500, 7013, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (4152, 'TeratoDragon', 0001, 0950, 50000000, 00, 30000, 30000, 40000, 0014, 0015, 1500, 0500, 0138, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7452, 'ApeCityDragon', 0001, 0950, 3000000, 00, 30000, 30000, 30000, 0005, 0010, 5000, 0500, 0200, 3, 0500, 7014, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (4250, 'CaribbeanCps[VIP', 0001, 0227, 50000000, 00, 60000, 50000, 2000, 0001, 0006, 1000, 0500, 0100, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2717, 'TerrorDragon', 0001, 0950, 50000000, 00, 60000, 50000, 40000, 0014, 0015, 1500, 0500, 0138, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2751, 'ChaosGuard', 0001, 0275, 60000000, 00, 80000, 40000, 30000, 0014, 0015, 0800, 0500, 0140, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7854, 'WarriorofRage', 0001, 0954, 50000, 00, 120, 0109, 0098, 0001, 0015, 5000, 1000, 0132, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7563, 'NightmareCaptain', 0001, 0516, 150000, 00, 120, 0109, 0098, 0001, 0015, 0800, 0500, 0140, 3, 0000, 0000, 100, 1000040, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7904, 'Lv.2GeneralCat', 0001, 0921, 9999, 00, 50000, 50000, 50000, 0000, 12062, 0000, 0000, 0000, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7903, 'Lv.1GeneralCat', 0001, 0921, 9999, 00, 50000, 50000, 50000, 0000, 12062, 0000, 0000, 0000, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7905, 'Lv.3GeneralCat', 0001, 0921, 9999, 00, 50000, 50000, 50000, 0000, 12062, 0000, 0000, 0000, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7906, 'Lv.4GeneralCat', 0001, 0921, 9999, 00, 50000, 50000, 50000, 0000, 12062, 0000, 0000, 0000, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7907, 'Lv.5GeneralCat', 0001, 0921, 9999, 00, 50000, 50000, 50000, 0000, 12062, 0000, 0000, 0000, 3, 0000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (4215, 'ThrillingSpook', 0001, 0952, 10000000, 00, 3000, 2000, 0300, 0005, 0007, 0500, 0500, 0090, 3, 0200, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (4216, 'ThrillingSpook2', 0001, 0952, 32000000, 00, 5000, 4000, 0600, 0010, 0010, 1000, 0800, 0120, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (4217, 'ThrillingSpook3', 0001, 0852, 55000000, 00, 7000, 6000, 1000, 0003, 0015, 1500, 1000, 0140, 3, 1000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2411, 'IcySerpent', 0001, 0255, 65535, 00, 2328, 1600, 0100, 0001, 0008, 1000, 0500, 0122, 3, 0500, 0000, 100, 729102, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2461, 'DarkElf', 0001, 0328, 65535, 00, 2328, 1600, 0100, 0001, 0008, 1000, 0500, 0127, 3, 0500, 0000, 100, 721347, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2466, 'GrottoLady', 0001, 0311, 65535, 00, 2328, 1600, 0100, 0001, 0008, 1000, 0500, 0127, 3, 0500, 0000, 100, 721347, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2479, 'HeadlessSoldier', 0001, 0251, 65535, 00, 1200, 0600, 0000, 0001, 0008, 1000, 0500, 0127, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7022, 'CoinsStealer', 0001, 0519, 38930, 00, 1200, 0600, 0100, 0001, 0001, 1000, 0500, 0102, 3, 1000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2699, 'SilverOctopus', 0001, 0288, 50, 00, 2, 0001, 0000, 0001, 0001, 1000, 1000, 0010, 3, 1000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2700, 'GoldenOctopus', 0001, 0959, 600, 00, 2, 0001, 0000, 0001, 0001, 1000, 1000, 0140, 3, 1000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0059, '59', 0001, 0536, 1000, 00, 46, 0040, 0000, 0001, 0015, 1000, 0500, 0015, 3, 1000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0077, 'BirdmanL88', 0001, 0605, 26844, 00, 851, 0768, 0000, 0002, 0015, 1000, 0250, 0088, 3, 0100, 0000, 100, 729094, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (0038, 'BirdKing', 0001, 0405, 13422, 00, 1013, 0914, 0600, 0009, 0008, 1000, 0500, 0090, 3, 0400, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3010, 'IronBird', 0001, 0205, 4545, 00, 804, 0304, 0253, 0002, 0015, 2000, 0250, 0078, 3, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3011, 'CopperBird', 0001, 0205, 6813, 00, 1137, 0637, 0341, 0002, 0015, 2000, 0250, 0088, 3, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3012, 'SilverBird', 0001, 0205, 9937, 00, 1845, 1345, 0452, 0001, 0018, 2000, 0500, 0098, 3, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3013, 'GoldenBird', 0001, 0205, 14335, 00, 2170, 1670, 0597, 0001, 0018, 2000, 0500, 0108, 3, 0100, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3014, 'FastBird', 0001, 0205, 18205, 00, 2566, 2066, 0712, 0001, 0018, 2000, 0500, 0118, 3, 0100, 0000, 100, 721102, 10, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8213, 'BirdmanAide', 0001, 0245, 26000, 00, 800, 0770, 0934, 0001, 0009, 1000, 0500, 0087, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8313, 'BirdmanKing', 0001, 0245, 60460, 00, 880, 0880, 1770, 0001, 0009, 1000, 0500, 0087, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8113, 'BirdmanMsgr', 0001, 0245, 26000, 00, 780, 0780, 0420, 0009, 0010, 1300, 0500, 0087, 3, 0500, 3013, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8421, 'WaterDevilKing', 0001, 0273, 30000, 00, 60000, 60000, 1200, 0010, 0015, 1000, 1000, 0120, 3, 0400, 1002, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (8422, 'AdultWaterDevil', 0001, 0273, 30000, 00, 5785, 4875, 1200, 0005, 0015, 2000, 1000, 0120, 3, 0400, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3147, 'BlueBird', 0001, 0332, 38050, 00, 1980, 1400, 0100, 0001, 0008, 1000, 1000, 0115, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (3646, 'FurySatan', 0001, 0104, 65535, 00, 1200, 0600, 16800, 0001, 0015, 1000, 0500, 0135, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (4212, 'SnowBansheeSoul', 0001, 0951, 60000000, 00, 20000, 20000, 10000, 0015, 0015, 2000, 4000000000, 0138, 3, 4000000000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7859, 'ThunderDragon', 0001, 0291, 90000000, 00, 2100, 1000, 80000, 0001, 0015, 1000, 0500, 0135, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7861, 'SmallCrystal', 0001, 0916, 20, 00, 1, 0001, 10000, 0001, 0015, 1000, 0500, 0135, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7860, 'BigCrystal', 0001, 0916, 80, 00, 1, 0001, 10000, 0001, 0015, 1000, 0500, 0135, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7896, 'RealmSoldier', 0001, 0290, 50000, 00, 2100, 1000, 0090, 0001, 0015, 1000, 0500, 0135, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7897, 'MilitaryOfficer', 0001, 0910, 100000, 00, 2100, 1000, 0090, 0001, 0015, 1000, 0500, 0135, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7898, 'KylinChariot', 0001, 0788, 100000, 00, 2100, 1000, 0090, 0001, 0015, 1000, 4000000000, 0135, 3, 4000000000, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7891, 'VermilionBirdDef', 0001, 0197, 50000, 00, 2100, 0100, 0090, 0001, 0015, 1000, 0500, 0135, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7890, 'WhiteTigerDefend', 0001, 0384, 50000, 00, 2100, 1000, 0090, 0001, 0015, 1000, 0500, 0135, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7889, 'AzureDragonDefen', 0001, 0255, 50000, 00, 2100, 1000, 0090, 0001, 0015, 1000, 0500, 0135, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7892, 'BlackTurtleDefen', 0001, 0168, 50000, 00, 2100, 1000, 0090, 0001, 0015, 1000, 0500, 0135, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7883, 'AlluringWitch', 0001, 0919, 22500000, 00, 2100, 1000, 80000, 0001, 0015, 1000, 0500, 0135, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7884, 'DarkCrystalofSky', 0001, 0916, 5000000, 00, 2100, 1000, 0090, 0001, 0015, 1000, 0500, 0135, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7971, 'DarkCrystalofWin', 0001, 0916, 5000000, 00, 2100, 1000, 0090, 0001, 0015, 1000, 0500, 0135, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7972, 'ThunderCrystal', 0001, 0916, 5000000, 00, 2100, 1000, 0090, 0001, 0015, 1000, 0500, 0135, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (7973, 'DarkCrystalofFir', 0001, 0916, 5000000, 00, 2100, 1000, 0090, 0001, 0015, 1000, 0500, 0135, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (4151, 'LavaBeast', 0001, 0251, 15000000, 00, 20000, 20000, 20000, 0003, 0015, 1000, 1000, 0135, 3, 0500, 0000, 0, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (2760, 'VioletBatKing', 0001, 0210, 109800, 00, 2100, 1000, 0500, 0001, 0008, 1000, 1000, 0122, 3, 0500, 0000, 100, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (417100, 'WhiteTiger', 0001, 1003, 60000000, 65535, 20000, 20000, 20000, 0100, 0019, 0086, 1000, 0300, 3, 0500, 5000, 1, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (111225, 'EgYConquer', 0001, 50850, 60000000, 1000, 1000, 1000, 1000, 0100, 0050, 1000, 1000, 0140, 3, 0500, 0100, 1, 0, 0, 0, 0, 0, 0);
INSERT INTO `monsterinfos` VALUES (111125, 'EgYConquer2', 0001, 50860, 60000000, 65535, 1000, 1000, 20000, 0100, 0050, 1000, 1000, 0140, 3, 0500, 5000, 1, 0, 0, 0, 0, 0, 0);

-- ----------------------------
-- Table structure for monsterspawns
-- ----------------------------
DROP TABLE IF EXISTS `monsterspawns`;
CREATE TABLE `monsterspawns`  (
  `id` int(4) UNSIGNED ZEROFILL NOT NULL AUTO_INCREMENT,
  `mapid` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `bound_x` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `bound_y` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `bound_cx` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `bound_cy` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `maxnpc` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `rest_secs` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `max_per_gen` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `npctype` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `mapid`(`mapid`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 565645457 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of monsterspawns
-- ----------------------------
INSERT INTO `monsterspawns` VALUES (3910, 1015, 0265, 0095, 0080, 0080, 0024, 0008, 0003, 2093);
INSERT INTO `monsterspawns` VALUES (4403, 1011, 0032, 0370, 0015, 0015, 0012, 0001, 0006, 2140);
INSERT INTO `monsterspawns` VALUES (4402, 1011, 0026, 0391, 0015, 0015, 0012, 0001, 0006, 2140);
INSERT INTO `monsterspawns` VALUES (4401, 1011, 0055, 0328, 0015, 0015, 0012, 0001, 0006, 2140);
INSERT INTO `monsterspawns` VALUES (4400, 1011, 0040, 0362, 0015, 0015, 0012, 0001, 0006, 2140);
INSERT INTO `monsterspawns` VALUES (2140000, 1011, 0055, 0332, 0000, 0000, 0000, 0000, 0000, 2140);
INSERT INTO `monsterspawns` VALUES (3590, 1011, 0099, 0430, 0000, 0000, 0000, 0000, 0000, 2141);
INSERT INTO `monsterspawns` VALUES (3591, 1011, 0099, 0430, 0000, 0000, 0000, 0000, 0000, 2142);
INSERT INTO `monsterspawns` VALUES (3599, 1020, 0713, 0678, 0171, 0144, 0030, 0001, 0030, 2143);
INSERT INTO `monsterspawns` VALUES (3595, 1020, 0795, 0471, 0000, 0000, 0000, 0000, 0000, 2147);
INSERT INTO `monsterspawns` VALUES (3596, 1021, 0795, 0471, 0000, 0000, 0000, 0000, 0000, 2148);
INSERT INTO `monsterspawns` VALUES (0001, 1002, 0288, 0360, 0130, 0030, 0040, 0001, 0035, 0001);
INSERT INTO `monsterspawns` VALUES (0002, 1002, 0300, 0359, 0100, 0030, 0030, 0001, 0025, 0001);
INSERT INTO `monsterspawns` VALUES (0003, 1002, 0424, 0389, 0255, 0100, 0045, 0001, 0035, 0002);
INSERT INTO `monsterspawns` VALUES (0004, 1002, 0290, 0582, 0200, 0100, 0045, 0001, 0035, 0003);
INSERT INTO `monsterspawns` VALUES (0005, 1002, 0114, 0458, 0140, 0090, 0045, 0001, 0035, 0004);
INSERT INTO `monsterspawns` VALUES (0006, 1002, 4294967295, 0225, 0090, 0175, 0045, 0001, 0040, 0005);
INSERT INTO `monsterspawns` VALUES (0007, 1011, 0317, 0077, 0150, 0050, 0055, 0001, 0017, 0006);
INSERT INTO `monsterspawns` VALUES (0008, 1011, 0328, 0227, 0200, 0300, 0055, 0001, 0013, 0007);
INSERT INTO `monsterspawns` VALUES (0009, 1011, 0107, 0325, 0180, 0100, 0055, 0001, 0015, 0007);
INSERT INTO `monsterspawns` VALUES (0010, 1011, 0538, 0272, 0110, 0250, 0040, 0001, 0020, 0008);
INSERT INTO `monsterspawns` VALUES (0011, 1011, 0532, 0768, 0100, 0060, 0013, 0001, 0004, 0009);
INSERT INTO `monsterspawns` VALUES (0012, 1011, 0673, 0638, 0100, 0070, 0015, 0001, 0004, 0009);
INSERT INTO `monsterspawns` VALUES (0013, 1020, 0608, 0556, 0171, 0144, 0055, 0001, 0045, 0010);
INSERT INTO `monsterspawns` VALUES (0014, 1020, 0412, 0230, 0198, 0211, 0055, 0001, 0045, 0011);
INSERT INTO `monsterspawns` VALUES (0015, 1020, 0200, 0177, 0150, 0169, 0055, 0001, 0045, 0012);
INSERT INTO `monsterspawns` VALUES (0016, 1020, 0232, 0460, 0224, 0142, 0055, 0001, 0045, 0013);
INSERT INTO `monsterspawns` VALUES (0018, 1000, 0776, 0561, 0110, 0110, 0075, 0001, 0010, 0015);
INSERT INTO `monsterspawns` VALUES (0019, 1000, 0504, 0333, 0170, 0140, 0075, 0001, 0010, 0015);
INSERT INTO `monsterspawns` VALUES (0020, 1000, 0250, 0134, 0145, 0280, 0075, 0001, 0010, 0016);
INSERT INTO `monsterspawns` VALUES (0021, 1000, 0122, 0269, 0108, 0150, 0075, 0001, 0010, 0017);
INSERT INTO `monsterspawns` VALUES (0022, 1015, 0804, 0564, 0080, 0080, 0055, 0001, 0015, 0018);
INSERT INTO `monsterspawns` VALUES (0023, 1015, 0717, 0636, 0100, 0090, 0055, 0001, 0015, 0018);
INSERT INTO `monsterspawns` VALUES (0025, 1015, 0263, 0103, 0096, 0042, 0055, 0001, 0015, 0019);
INSERT INTO `monsterspawns` VALUES (0024, 1015, 0456, 0681, 0090, 0100, 0055, 0001, 0015, 0019);
INSERT INTO `monsterspawns` VALUES (0032, 1015, 0362, 0469, 0192, 0117, 0015, 0001, 0015, 0055);
INSERT INTO `monsterspawns` VALUES (0033, 1015, 0335, 0329, 0096, 0070, 0015, 0001, 0015, 0055);
INSERT INTO `monsterspawns` VALUES (0056, 1011, 0317, 0077, 0150, 0050, 0039, 0001, 0005, 0065);
INSERT INTO `monsterspawns` VALUES (0081, 1015, 0362, 0469, 0192, 0117, 0039, 0001, 0013, 0079);
INSERT INTO `monsterspawns` VALUES (0082, 1015, 0335, 0329, 0096, 0070, 0039, 0001, 0004, 0079);
INSERT INTO `monsterspawns` VALUES (0125, 1002, 0260, 0344, 0100, 0030, 0113, 3600, 0001, 0021);
INSERT INTO `monsterspawns` VALUES (0126, 1002, 0424, 0374, 0255, 0100, 0263, 3600, 0001, 0022);
INSERT INTO `monsterspawns` VALUES (0127, 1002, 0290, 0582, 0200, 0100, 0213, 3600, 0001, 0023);
INSERT INTO `monsterspawns` VALUES (0128, 1002, 0114, 0458, 0140, 0090, 0153, 3600, 0001, 0024);
INSERT INTO `monsterspawns` VALUES (0129, 1002, 4294967295, 0225, 0090, 0175, 0183, 3600, 0001, 0025);
INSERT INTO `monsterspawns` VALUES (0130, 1000, 0250, 0134, 0145, 0280, 0293, 3600, 0001, 0032);
INSERT INTO `monsterspawns` VALUES (0131, 1000, 0122, 0269, 0108, 0150, 0163, 3600, 0001, 0033);
INSERT INTO `monsterspawns` VALUES (0132, 1000, 0320, 0425, 0090, 0064, 0103, 3600, 0001, 0035);
INSERT INTO `monsterspawns` VALUES (0133, 1000, 0432, 0392, 0045, 0072, 0083, 3600, 0001, 0037);
INSERT INTO `monsterspawns` VALUES (0134, 1015, 0501, 0277, 0090, 0065, 0103, 3600, 0001, 0038);
INSERT INTO `monsterspawns` VALUES (0135, 1015, 0433, 0155, 0070, 0054, 0083, 3600, 0001, 0039);
INSERT INTO `monsterspawns` VALUES (0136, 1015, 0153, 0401, 0090, 0063, 0103, 3600, 0001, 0040);
INSERT INTO `monsterspawns` VALUES (0139, 1015, 0114, 0312, 0095, 0045, 0018, 0001, 0003, 0084);
INSERT INTO `monsterspawns` VALUES (0140, 1015, 0293, 0559, 0056, 0037, 0018, 0001, 0002, 0084);
INSERT INTO `monsterspawns` VALUES (0151, 1015, 0114, 0312, 0095, 0045, 0033, 0001, 0003, 0085);
INSERT INTO `monsterspawns` VALUES (0152, 1015, 0293, 0559, 0056, 0037, 0023, 0001, 0002, 0085);
INSERT INTO `monsterspawns` VALUES (0205, 1015, 0362, 0469, 0192, 0117, 0203, 3600, 0001, 0090);
INSERT INTO `monsterspawns` VALUES (0204, 1015, 0298, 0454, 0015, 0015, 0015, 3600, 0001, 0040);
INSERT INTO `monsterspawns` VALUES (0203, 1015, 0451, 0676, 0093, 0117, 0123, 3600, 0001, 0039);
INSERT INTO `monsterspawns` VALUES (0202, 1015, 0662, 0665, 0138, 0079, 0153, 3600, 0001, 0038);
INSERT INTO `monsterspawns` VALUES (0201, 1000, 0122, 0269, 0108, 0150, 0163, 3600, 0001, 0037);
INSERT INTO `monsterspawns` VALUES (0200, 1000, 0250, 0134, 0145, 0280, 0293, 3600, 0001, 0036);
INSERT INTO `monsterspawns` VALUES (0199, 1000, 0504, 0333, 0170, 0140, 0183, 3600, 0001, 0035);
INSERT INTO `monsterspawns` VALUES (0198, 1000, 0565, 0556, 0180, 0210, 0223, 3600, 0001, 0034);
INSERT INTO `monsterspawns` VALUES (0197, 1020, 0232, 0460, 0224, 0142, 0243, 3600, 0001, 0033);
INSERT INTO `monsterspawns` VALUES (0196, 1020, 0200, 0177, 0150, 0169, 0183, 3600, 0001, 0032);
INSERT INTO `monsterspawns` VALUES (0195, 1020, 0412, 0230, 0198, 0211, 0223, 3600, 0001, 0031);
INSERT INTO `monsterspawns` VALUES (0194, 1020, 0608, 0556, 0171, 0144, 0183, 3600, 0001, 0030);
INSERT INTO `monsterspawns` VALUES (0193, 1011, 0673, 0638, 0100, 0070, 0113, 3600, 0001, 0029);
INSERT INTO `monsterspawns` VALUES (0192, 1011, 0538, 0272, 0110, 0250, 0263, 3600, 0001, 0028);
INSERT INTO `monsterspawns` VALUES (0191, 1011, 0328, 0227, 0200, 0300, 0313, 3600, 0001, 0027);
INSERT INTO `monsterspawns` VALUES (0190, 1011, 0317, 0077, 0150, 0050, 0163, 3600, 0001, 0026);
INSERT INTO `monsterspawns` VALUES (0189, 1002, 4294967295, 0225, 0090, 0175, 0183, 3600, 0001, 0025);
INSERT INTO `monsterspawns` VALUES (0188, 1002, 0114, 0458, 0140, 0090, 0153, 3600, 0001, 0024);
INSERT INTO `monsterspawns` VALUES (0187, 1002, 0290, 0582, 0200, 0100, 0213, 3600, 0001, 0023);
INSERT INTO `monsterspawns` VALUES (0186, 1002, 0424, 0374, 0255, 0100, 0263, 3600, 0001, 0022);
INSERT INTO `monsterspawns` VALUES (0185, 1002, 0388, 0211, 0266, 0249, 0273, 3600, 0001, 0021);
INSERT INTO `monsterspawns` VALUES (0702, 1011, 0532, 0768, 0100, 0060, 0023, 0600, 0018, 3032);
INSERT INTO `monsterspawns` VALUES (0701, 1011, 0328, 0227, 0200, 0300, 0010, 0600, 0005, 3031);
INSERT INTO `monsterspawns` VALUES (3907, 1002, 0385, 0295, 0090, 0100, 0012, 0001, 0007, 0001);
INSERT INTO `monsterspawns` VALUES (3908, 1002, 0402, 0331, 0030, 0050, 0015, 0001, 0005, 0001);
INSERT INTO `monsterspawns` VALUES (0343, 1002, 0397, 0200, 0175, 0140, 0012, 0001, 0005, 0001);
INSERT INTO `monsterspawns` VALUES (0344, 1002, 0488, 0445, 0150, 0100, 0007, 0001, 0025, 0002);
INSERT INTO `monsterspawns` VALUES (0345, 1002, 0290, 0582, 0100, 0100, 0007, 0001, 0025, 0003);
INSERT INTO `monsterspawns` VALUES (0346, 1002, 0072, 0440, 0075, 0060, 0007, 0001, 0025, 0004);
INSERT INTO `monsterspawns` VALUES (0347, 1002, 4294967295, 0210, 0110, 0090, 0007, 0001, 0025, 0005);
INSERT INTO `monsterspawns` VALUES (0348, 1002, 4294967295, 0210, 0110, 0090, 0022, 0001, 0015, 0064);
INSERT INTO `monsterspawns` VALUES (0900, 1011, 0328, 0227, 0200, 0300, 0028, 0600, 0005, 3031);
INSERT INTO `monsterspawns` VALUES (0901, 1011, 0532, 0768, 0100, 0060, 0023, 0600, 0005, 3032);
INSERT INTO `monsterspawns` VALUES (0910, 1020, 0232, 0460, 0224, 0142, 0023, 0600, 0005, 3050);
INSERT INTO `monsterspawns` VALUES (6051, 1000, 0122, 0269, 0108, 0150, 0153, 1800, 0001, 6011);
INSERT INTO `monsterspawns` VALUES (6050, 1011, 0353, 0717, 0050, 0050, 0053, 1200, 0001, 6010);
INSERT INTO `monsterspawns` VALUES (0902, 1000, 0250, 0134, 0145, 0280, 0023, 0600, 0005, 3033);
INSERT INTO `monsterspawns` VALUES (8001, 1002, 4294967295, 0225, 0090, 0175, 0053, 0300, 0001, 8100);
INSERT INTO `monsterspawns` VALUES (8002, 1011, 0317, 0077, 0150, 0050, 0053, 0300, 0001, 8101);
INSERT INTO `monsterspawns` VALUES (8003, 1011, 0328, 0227, 0200, 0300, 0053, 0300, 0001, 8102);
INSERT INTO `monsterspawns` VALUES (8004, 1011, 0538, 0272, 0110, 0250, 0053, 0300, 0001, 8103);
INSERT INTO `monsterspawns` VALUES (8005, 1011, 0532, 0768, 0100, 0060, 0053, 0300, 0001, 8104);
INSERT INTO `monsterspawns` VALUES (8006, 1020, 0608, 0556, 0171, 0144, 0053, 0300, 0001, 8105);
INSERT INTO `monsterspawns` VALUES (8007, 1020, 0412, 0230, 0198, 0211, 0053, 0300, 0001, 8106);
INSERT INTO `monsterspawns` VALUES (8008, 1020, 0200, 0177, 0150, 0169, 0053, 0300, 0001, 8107);
INSERT INTO `monsterspawns` VALUES (8009, 1020, 0232, 0460, 0224, 0142, 0053, 0300, 0001, 8108);
INSERT INTO `monsterspawns` VALUES (8010, 1000, 0565, 0556, 0180, 0210, 0053, 0300, 0001, 8109);
INSERT INTO `monsterspawns` VALUES (8011, 1000, 0504, 0333, 0170, 0140, 0053, 0300, 0001, 8110);
INSERT INTO `monsterspawns` VALUES (8012, 1000, 0250, 0134, 0145, 0280, 0053, 0300, 0001, 8111);
INSERT INTO `monsterspawns` VALUES (8013, 1000, 0122, 0269, 0108, 0150, 0053, 0300, 0001, 8112);
INSERT INTO `monsterspawns` VALUES (8014, 1015, 0662, 0665, 0138, 0079, 0063, 0300, 0001, 8113);
INSERT INTO `monsterspawns` VALUES (8015, 1015, 0456, 0681, 0090, 0100, 0063, 0300, 0001, 8114);
INSERT INTO `monsterspawns` VALUES (8017, 1015, 0362, 0469, 0192, 0117, 0063, 0300, 0001, 8115);
INSERT INTO `monsterspawns` VALUES (8026, 1020, 0608, 0556, 0171, 0144, 0053, 0300, 0001, 8205);
INSERT INTO `monsterspawns` VALUES (8027, 1020, 0412, 0230, 0198, 0211, 0053, 0300, 0001, 8206);
INSERT INTO `monsterspawns` VALUES (8028, 1020, 0200, 0177, 0150, 0169, 0053, 0300, 0001, 8207);
INSERT INTO `monsterspawns` VALUES (8029, 1020, 0232, 0460, 0224, 0142, 0053, 0300, 0001, 8208);
INSERT INTO `monsterspawns` VALUES (8030, 1000, 0565, 0556, 0180, 0210, 0053, 0300, 0001, 8209);
INSERT INTO `monsterspawns` VALUES (8031, 1000, 0504, 0333, 0170, 0140, 0053, 0300, 0001, 8210);
INSERT INTO `monsterspawns` VALUES (8032, 1000, 0250, 0134, 0145, 0280, 0053, 0300, 0001, 8211);
INSERT INTO `monsterspawns` VALUES (8033, 1000, 0122, 0269, 0108, 0150, 0053, 0300, 0001, 8212);
INSERT INTO `monsterspawns` VALUES (8034, 1015, 0662, 0665, 0138, 0079, 0063, 0300, 0001, 8213);
INSERT INTO `monsterspawns` VALUES (8035, 1015, 0456, 0681, 0090, 0100, 0063, 0300, 0001, 8214);
INSERT INTO `monsterspawns` VALUES (8037, 1015, 0362, 0469, 0192, 0117, 0063, 0300, 0001, 8215);
INSERT INTO `monsterspawns` VALUES (8041, 1002, 4294967295, 0225, 0090, 0175, 0068, 0300, 0001, 8300);
INSERT INTO `monsterspawns` VALUES (8046, 1020, 0608, 0556, 0171, 0144, 0068, 0300, 0001, 8305);
INSERT INTO `monsterspawns` VALUES (8047, 1020, 0412, 0230, 0198, 0211, 0068, 0300, 0001, 8306);
INSERT INTO `monsterspawns` VALUES (8048, 1020, 0200, 0177, 0150, 0169, 0068, 0300, 0001, 8307);
INSERT INTO `monsterspawns` VALUES (8049, 1020, 0232, 0460, 0224, 0142, 0068, 0300, 0001, 8308);
INSERT INTO `monsterspawns` VALUES (8050, 1000, 0565, 0556, 0180, 0210, 0068, 0300, 0001, 8309);
INSERT INTO `monsterspawns` VALUES (8051, 1000, 0504, 0333, 0170, 0140, 0068, 0300, 0001, 8310);
INSERT INTO `monsterspawns` VALUES (8052, 1000, 0250, 0134, 0145, 0280, 0068, 0300, 0001, 8311);
INSERT INTO `monsterspawns` VALUES (8053, 1000, 0122, 0269, 0108, 0150, 0068, 0300, 0001, 8312);
INSERT INTO `monsterspawns` VALUES (8054, 1015, 0662, 0665, 0138, 0079, 0073, 0300, 0001, 8313);
INSERT INTO `monsterspawns` VALUES (8055, 1015, 0456, 0681, 0090, 0100, 0073, 0300, 0001, 8314);
INSERT INTO `monsterspawns` VALUES (8057, 1015, 0362, 0469, 0192, 0117, 0073, 0300, 0001, 8315);
INSERT INTO `monsterspawns` VALUES (7581, 1000, 0702, 0723, 0145, 0280, 0075, 0001, 0010, 0014);
INSERT INTO `monsterspawns` VALUES (7582, 1000, 0666, 0694, 0145, 0280, 0075, 0001, 0010, 0014);
INSERT INTO `monsterspawns` VALUES (7583, 1000, 0637, 0706, 0145, 0280, 0075, 0001, 0010, 0014);
INSERT INTO `monsterspawns` VALUES (7584, 1000, 0616, 0689, 0145, 0280, 0075, 0001, 0010, 0014);
INSERT INTO `monsterspawns` VALUES (7585, 1000, 0569, 0661, 0145, 0280, 0075, 0001, 0010, 0014);
INSERT INTO `monsterspawns` VALUES (7586, 1000, 0539, 0730, 0145, 0280, 0075, 0001, 0010, 0014);
INSERT INTO `monsterspawns` VALUES (7587, 1000, 0545, 0791, 0145, 0280, 0075, 0001, 0010, 0014);
INSERT INTO `monsterspawns` VALUES (7588, 1000, 0605, 0633, 0145, 0280, 0075, 0001, 0010, 0014);
INSERT INTO `monsterspawns` VALUES (7589, 1000, 0662, 0639, 0145, 0280, 0075, 0001, 0010, 0014);
INSERT INTO `monsterspawns` VALUES (7590, 1000, 0720, 0666, 0145, 0280, 0075, 0001, 0010, 0014);
INSERT INTO `monsterspawns` VALUES (0058, 1011, 0107, 0286, 0180, 0100, 0039, 0001, 0010, 0066);
INSERT INTO `monsterspawns` VALUES (0059, 1011, 0538, 0272, 0110, 0250, 0039, 0001, 0017, 0067);
INSERT INTO `monsterspawns` VALUES (0060, 1011, 0532, 0768, 0100, 0060, 0033, 0001, 0030, 0068);
INSERT INTO `monsterspawns` VALUES (0061, 1011, 0673, 0638, 0100, 0070, 0039, 0001, 0030, 0068);
INSERT INTO `monsterspawns` VALUES (0062, 1020, 0608, 0556, 0171, 0144, 0039, 0001, 0015, 0069);
INSERT INTO `monsterspawns` VALUES (0063, 1020, 0412, 0230, 0198, 0211, 0039, 0001, 0025, 0070);
INSERT INTO `monsterspawns` VALUES (0064, 1020, 0200, 0177, 0150, 0169, 0039, 0001, 0015, 0071);
INSERT INTO `monsterspawns` VALUES (0065, 1020, 0232, 0460, 0224, 0142, 0039, 0001, 0019, 0072);
INSERT INTO `monsterspawns` VALUES (0066, 1000, 0565, 0556, 0180, 0210, 0039, 0001, 0015, 0073);
INSERT INTO `monsterspawns` VALUES (0067, 1000, 0776, 0561, 0110, 0110, 0039, 0001, 0007, 0074);
INSERT INTO `monsterspawns` VALUES (0068, 1000, 0504, 0333, 0170, 0140, 0039, 0001, 0015, 0074);
INSERT INTO `monsterspawns` VALUES (0069, 1000, 0250, 0134, 0145, 0280, 0039, 0001, 0025, 0075);
INSERT INTO `monsterspawns` VALUES (0070, 1000, 0122, 0269, 0108, 0150, 0039, 0001, 0010, 0076);
INSERT INTO `monsterspawns` VALUES (0071, 1015, 0812, 0548, 0044, 0174, 0039, 0001, 0005, 0077);
INSERT INTO `monsterspawns` VALUES (0072, 1015, 0662, 0665, 0138, 0079, 0039, 0001, 0007, 0077);
INSERT INTO `monsterspawns` VALUES (0073, 1015, 0451, 0676, 0093, 0117, 0018, 0001, 0007, 0078);
INSERT INTO `monsterspawns` VALUES (0074, 1015, 0263, 0103, 0096, 0042, 0018, 0001, 0003, 0078);
INSERT INTO `monsterspawns` VALUES (3598, 1020, 0879, 0562, 0000, 0000, 0000, 0000, 0000, 2149);
INSERT INTO `monsterspawns` VALUES (3610, 1020, 0866, 0552, 0100, 0100, 0030, 0010, 0030, 2143);
INSERT INTO `monsterspawns` VALUES (2314, 1300, 0085, 0279, 0070, 0040, 0015, 0001, 0005, 0200);
INSERT INTO `monsterspawns` VALUES (2315, 1300, 0101, 0353, 0040, 0063, 0015, 0001, 0004, 0200);
INSERT INTO `monsterspawns` VALUES (2316, 1300, 0153, 0401, 0090, 0063, 0015, 0001, 0006, 0200);
INSERT INTO `monsterspawns` VALUES (2317, 1300, 0232, 0175, 0037, 0024, 0015, 0001, 0002, 0200);
INSERT INTO `monsterspawns` VALUES (2318, 1300, 0333, 0253, 0090, 0063, 0015, 0001, 0006, 0200);
INSERT INTO `monsterspawns` VALUES (2319, 1300, 0342, 0171, 0046, 0060, 0015, 0001, 0004, 0200);
INSERT INTO `monsterspawns` VALUES (2320, 1300, 0265, 0131, 0067, 0041, 0015, 0001, 0004, 0200);
INSERT INTO `monsterspawns` VALUES (2321, 1300, 0276, 0196, 0045, 0072, 0015, 0001, 0005, 0201);
INSERT INTO `monsterspawns` VALUES (2322, 1300, 0309, 0597, 0044, 0051, 0039, 0001, 0003, 0201);
INSERT INTO `monsterspawns` VALUES (2324, 1300, 0207, 0518, 0040, 0030, 0039, 0001, 0002, 0201);
INSERT INTO `monsterspawns` VALUES (2325, 1300, 0317, 0514, 0025, 0026, 0039, 0001, 0001, 0201);
INSERT INTO `monsterspawns` VALUES (2326, 1300, 0241, 0473, 0062, 0055, 0039, 0001, 0003, 0201);
INSERT INTO `monsterspawns` VALUES (2327, 1300, 0362, 0605, 0036, 0030, 0039, 0001, 0002, 0201);
INSERT INTO `monsterspawns` VALUES (2328, 1300, 0388, 0371, 0040, 0024, 0039, 0001, 0002, 0201);
INSERT INTO `monsterspawns` VALUES (2329, 1300, 0220, 0347, 0038, 0024, 0039, 0001, 0002, 0201);
INSERT INTO `monsterspawns` VALUES (2330, 1300, 0347, 0378, 0030, 0026, 0039, 0001, 0002, 0201);
INSERT INTO `monsterspawns` VALUES (2331, 1300, 0253, 0304, 0070, 0053, 0039, 0001, 0005, 0201);
INSERT INTO `monsterspawns` VALUES (2332, 1300, 0329, 0343, 0026, 0025, 0039, 0001, 0001, 0201);
INSERT INTO `monsterspawns` VALUES (2333, 1300, 0320, 0425, 0090, 0064, 0039, 0001, 0006, 0201);
INSERT INTO `monsterspawns` VALUES (2334, 1300, 0162, 0319, 0065, 0060, 0039, 0001, 0005, 0201);
INSERT INTO `monsterspawns` VALUES (2335, 1300, 0052, 0323, 0035, 0025, 0039, 0001, 0002, 0201);
INSERT INTO `monsterspawns` VALUES (2336, 1300, 0085, 0279, 0070, 0040, 0039, 0001, 0005, 0201);
INSERT INTO `monsterspawns` VALUES (2337, 1300, 0101, 0353, 0040, 0063, 0039, 0001, 0004, 0201);
INSERT INTO `monsterspawns` VALUES (2338, 1300, 0153, 0401, 0090, 0063, 0039, 0001, 0006, 0201);
INSERT INTO `monsterspawns` VALUES (2339, 1300, 0342, 0171, 0046, 0060, 0039, 0001, 0004, 0201);
INSERT INTO `monsterspawns` VALUES (2340, 1300, 0265, 0131, 0067, 0041, 0039, 0001, 0004, 0201);
INSERT INTO `monsterspawns` VALUES (2341, 1300, 0276, 0196, 0045, 0072, 0039, 0001, 0005, 0201);
INSERT INTO `monsterspawns` VALUES (2342, 1300, 0232, 0175, 0037, 0024, 0039, 0001, 0002, 0201);
INSERT INTO `monsterspawns` VALUES (2343, 1300, 0333, 0253, 0090, 0063, 0039, 0001, 0006, 0201);
INSERT INTO `monsterspawns` VALUES (2344, 1300, 0264, 0368, 0045, 0072, 0018, 0001, 0005, 0201);
INSERT INTO `monsterspawns` VALUES (2345, 1300, 0421, 0327, 0068, 0041, 0018, 0001, 0003, 0201);
INSERT INTO `monsterspawns` VALUES (2346, 1300, 0433, 0155, 0070, 0054, 0018, 0001, 0005, 0201);
INSERT INTO `monsterspawns` VALUES (2347, 1300, 0510, 0196, 0045, 0056, 0018, 0001, 0003, 0201);
INSERT INTO `monsterspawns` VALUES (2348, 1300, 0489, 0449, 0090, 0060, 0018, 0001, 0006, 0201);
INSERT INTO `monsterspawns` VALUES (2349, 1300, 0498, 0367, 0046, 0060, 0018, 0001, 0003, 0201);
INSERT INTO `monsterspawns` VALUES (2350, 1300, 0501, 0277, 0090, 0065, 0018, 0001, 0006, 0201);
INSERT INTO `monsterspawns` VALUES (2351, 1300, 0432, 0392, 0045, 0072, 0018, 0001, 0004, 0201);
INSERT INTO `monsterspawns` VALUES (2352, 1300, 0400, 0199, 0037, 0024, 0018, 0001, 0002, 0201);
INSERT INTO `monsterspawns` VALUES (2353, 1300, 0444, 0220, 0055, 0072, 0018, 0001, 0005, 0201);
INSERT INTO `monsterspawns` VALUES (2354, 1300, 0264, 0368, 0045, 0072, 0033, 0001, 0005, 0201);
INSERT INTO `monsterspawns` VALUES (2355, 1300, 0421, 0327, 0068, 0041, 0033, 0001, 0003, 0201);
INSERT INTO `monsterspawns` VALUES (2356, 1300, 0433, 0155, 0070, 0054, 0033, 0001, 0005, 0201);
INSERT INTO `monsterspawns` VALUES (2357, 1300, 0510, 0196, 0045, 0056, 0033, 0001, 0003, 0201);
INSERT INTO `monsterspawns` VALUES (2358, 1300, 0489, 0449, 0090, 0060, 0043, 0001, 0006, 0201);
INSERT INTO `monsterspawns` VALUES (2359, 1300, 0498, 0367, 0046, 0060, 0033, 0001, 0003, 0201);
INSERT INTO `monsterspawns` VALUES (2360, 1300, 0501, 0277, 0090, 0065, 0043, 0001, 0006, 0201);
INSERT INTO `monsterspawns` VALUES (2361, 1300, 0432, 0392, 0045, 0072, 0033, 0001, 0004, 0201);
INSERT INTO `monsterspawns` VALUES (2362, 1300, 0400, 0199, 0037, 0024, 0018, 0001, 0002, 0201);
INSERT INTO `monsterspawns` VALUES (2363, 1300, 0444, 0220, 0055, 0072, 0033, 0001, 0005, 0201);
INSERT INTO `monsterspawns` VALUES (2391, 1016, 0055, 0055, 0089, 0089, 0089, 0010, 0010, 2390);
INSERT INTO `monsterspawns` VALUES (2390, 1016, 0029, 0029, 0065, 0065, 0030, 0010, 0030, 2390);
INSERT INTO `monsterspawns` VALUES (3597, 1778, 0029, 0026, 0060, 0070, 0015, 0008, 0015, 2146);
INSERT INTO `monsterspawns` VALUES (30564, 1040, 0176, 0396, 0003, 0003, 0004, 0005, 0004, 3020);
INSERT INTO `monsterspawns` VALUES (30563, 1040, 0272, 0492, 0003, 0003, 0004, 0005, 0004, 3014);
INSERT INTO `monsterspawns` VALUES (30562, 1040, 0320, 0540, 0003, 0003, 0004, 0005, 0004, 3009);
INSERT INTO `monsterspawns` VALUES (30561, 1040, 0368, 0588, 0003, 0003, 0004, 0005, 0004, 3000);
INSERT INTO `monsterspawns` VALUES (30560, 1040, 0224, 0444, 0003, 0003, 0004, 0005, 0004, 3019);
INSERT INTO `monsterspawns` VALUES (4686, 1700, 0336, 0321, 0030, 0030, 0010, 0090, 0003, 3609);
INSERT INTO `monsterspawns` VALUES (4685, 1700, 0303, 0356, 0030, 0030, 0010, 0090, 0003, 3613);
INSERT INTO `monsterspawns` VALUES (4684, 1700, 0272, 0419, 0030, 0030, 0010, 0090, 0003, 3629);
INSERT INTO `monsterspawns` VALUES (4683, 1700, 0233, 0384, 0030, 0030, 0010, 0090, 0003, 3625);
INSERT INTO `monsterspawns` VALUES (4682, 1700, 0407, 0286, 0030, 0030, 0010, 0090, 0003, 3621);
INSERT INTO `monsterspawns` VALUES (4681, 1700, 0369, 0246, 0030, 0030, 0010, 0090, 0003, 3617);
INSERT INTO `monsterspawns` VALUES (4680, 1700, 0723, 0751, 0040, 0040, 0010, 0090, 0003, 3629);
INSERT INTO `monsterspawns` VALUES (4679, 1700, 0713, 0645, 0070, 0070, 0010, 0090, 0003, 3625);
INSERT INTO `monsterspawns` VALUES (4678, 1700, 0633, 0707, 0070, 0070, 0010, 0090, 0003, 3625);
INSERT INTO `monsterspawns` VALUES (4677, 1700, 0899, 0469, 0050, 0050, 0010, 0090, 0003, 3621);
INSERT INTO `monsterspawns` VALUES (4676, 1700, 0835, 0416, 0050, 0050, 0010, 0090, 0003, 3617);
INSERT INTO `monsterspawns` VALUES (4675, 1700, 0465, 0902, 0050, 0050, 0010, 0090, 0003, 3613);
INSERT INTO `monsterspawns` VALUES (4674, 1700, 0383, 0843, 0050, 0050, 0010, 0090, 0003, 3609);
INSERT INTO `monsterspawns` VALUES (4673, 1700, 0914, 0874, 0030, 0030, 0010, 0090, 0003, 3605);
INSERT INTO `monsterspawns` VALUES (4672, 1700, 0866, 0916, 0030, 0030, 0010, 0090, 0003, 3601);
INSERT INTO `monsterspawns` VALUES (4671, 1700, 0568, 0147, 0000, 0000, 0001, 0900, 0001, 3631);
INSERT INTO `monsterspawns` VALUES (4670, 1700, 0498, 0117, 0005, 0050, 0005, 0300, 0002, 3630);
INSERT INTO `monsterspawns` VALUES (4669, 1700, 0523, 0177, 0070, 0007, 0007, 0090, 0003, 3629);
INSERT INTO `monsterspawns` VALUES (4668, 1700, 0523, 0147, 0070, 0007, 0007, 0090, 0003, 3629);
INSERT INTO `monsterspawns` VALUES (4667, 1700, 0515, 0154, 0050, 0020, 0010, 0060, 0005, 3628);
INSERT INTO `monsterspawns` VALUES (4666, 1700, 0583, 0147, 0020, 0050, 0010, 0060, 0005, 3628);
INSERT INTO `monsterspawns` VALUES (4665, 1700, 0530, 0096, 0020, 0050, 0010, 0060, 0005, 3628);
INSERT INTO `monsterspawns` VALUES (4664, 1700, 0500, 0096, 0020, 0050, 0010, 0060, 0005, 3628);
INSERT INTO `monsterspawns` VALUES (4663, 1700, 0493, 0096, 0020, 0050, 0010, 0060, 0005, 3628);
INSERT INTO `monsterspawns` VALUES (4662, 1700, 0608, 0297, 0000, 0000, 0001, 0900, 0001, 3627);
INSERT INTO `monsterspawns` VALUES (4661, 1700, 0583, 0287, 0050, 0005, 0005, 0300, 0002, 3626);
INSERT INTO `monsterspawns` VALUES (4660, 1700, 0603, 0299, 0070, 0007, 0007, 0090, 0003, 3625);
INSERT INTO `monsterspawns` VALUES (4659, 1700, 0520, 0299, 0070, 0007, 0007, 0090, 0003, 3625);
INSERT INTO `monsterspawns` VALUES (4658, 1700, 0583, 0237, 0050, 0020, 0010, 0060, 0005, 3624);
INSERT INTO `monsterspawns` VALUES (4657, 1700, 0533, 0237, 0050, 0020, 0010, 0060, 0005, 3624);
INSERT INTO `monsterspawns` VALUES (4656, 1700, 0520, 0327, 0050, 0020, 0010, 0060, 0005, 3624);
INSERT INTO `monsterspawns` VALUES (4655, 1700, 0580, 0327, 0050, 0020, 0010, 0060, 0005, 3624);
INSERT INTO `monsterspawns` VALUES (4654, 1700, 0640, 0327, 0050, 0020, 0010, 0060, 0005, 3624);
INSERT INTO `monsterspawns` VALUES (4653, 1700, 0569, 0492, 0000, 0000, 0001, 0900, 0001, 3623);
INSERT INTO `monsterspawns` VALUES (4652, 1700, 0586, 0472, 0005, 0050, 0005, 0300, 0002, 3622);
INSERT INTO `monsterspawns` VALUES (4651, 1700, 0641, 0462, 0007, 0070, 0007, 0090, 0003, 3621);
INSERT INTO `monsterspawns` VALUES (4650, 1700, 0529, 0462, 0007, 0070, 0007, 0090, 0003, 3621);
INSERT INTO `monsterspawns` VALUES (4649, 1700, 0489, 0462, 0020, 0050, 0010, 0060, 0005, 3620);
INSERT INTO `monsterspawns` VALUES (4648, 1700, 0551, 0472, 0020, 0050, 0010, 0060, 0005, 3620);
INSERT INTO `monsterspawns` VALUES (4647, 1700, 0606, 0462, 0020, 0050, 0010, 0060, 0005, 3620);
INSERT INTO `monsterspawns` VALUES (4646, 1700, 0663, 0507, 0050, 0020, 0010, 0060, 0005, 3620);
INSERT INTO `monsterspawns` VALUES (4645, 1700, 0663, 0467, 0050, 0020, 0010, 0060, 0005, 3620);
INSERT INTO `monsterspawns` VALUES (4644, 1700, 1071, 0709, 0000, 0000, 0001, 0900, 0001, 3619);
INSERT INTO `monsterspawns` VALUES (4643, 1700, 1064, 0699, 0050, 0005, 0005, 0300, 0002, 3618);
INSERT INTO `monsterspawns` VALUES (4642, 1700, 1049, 0643, 0007, 0070, 0007, 0090, 0003, 3617);
INSERT INTO `monsterspawns` VALUES (4641, 1700, 1010, 0670, 0007, 0070, 0007, 0090, 0003, 3617);
INSERT INTO `monsterspawns` VALUES (4640, 1700, 1010, 0612, 0020, 0050, 0010, 0060, 0005, 3616);
INSERT INTO `monsterspawns` VALUES (4639, 1700, 0954, 0734, 0050, 0020, 0010, 0060, 0005, 3616);
INSERT INTO `monsterspawns` VALUES (4638, 1700, 0954, 0710, 0050, 0020, 0010, 0060, 0005, 3616);
INSERT INTO `monsterspawns` VALUES (4637, 1700, 0955, 0641, 0050, 0020, 0010, 0060, 0005, 3616);
INSERT INTO `monsterspawns` VALUES (4636, 1700, 0955, 0612, 0050, 0020, 0010, 0060, 0005, 3616);
INSERT INTO `monsterspawns` VALUES (4635, 1700, 0749, 1039, 0000, 0000, 0001, 0900, 0001, 3615);
INSERT INTO `monsterspawns` VALUES (4634, 1700, 0746, 1028, 0050, 0005, 0005, 0300, 0002, 3614);
INSERT INTO `monsterspawns` VALUES (4633, 1700, 0721, 1012, 0007, 0070, 0007, 0090, 0003, 3613);
INSERT INTO `monsterspawns` VALUES (4632, 1700, 0689, 1008, 0007, 0070, 0007, 0090, 0003, 3613);
INSERT INTO `monsterspawns` VALUES (4631, 1700, 0688, 0950, 0020, 0050, 0010, 0060, 0005, 3612);
INSERT INTO `monsterspawns` VALUES (4630, 1700, 0615, 1087, 0050, 0020, 0010, 0060, 0005, 3612);
INSERT INTO `monsterspawns` VALUES (4629, 1700, 0614, 1061, 0050, 0020, 0010, 0060, 0005, 3612);
INSERT INTO `monsterspawns` VALUES (4628, 1700, 0612, 0984, 0050, 0020, 0010, 0060, 0005, 3612);
INSERT INTO `monsterspawns` VALUES (4627, 1700, 0612, 0955, 0050, 0020, 0010, 0060, 0005, 3612);
INSERT INTO `monsterspawns` VALUES (4626, 1700, 0465, 0596, 0000, 0000, 0001, 0900, 0001, 3611);
INSERT INTO `monsterspawns` VALUES (4625, 1700, 0445, 0613, 0050, 0005, 0005, 0300, 0002, 3610);
INSERT INTO `monsterspawns` VALUES (4624, 1700, 0435, 0668, 0070, 0007, 0007, 0090, 0003, 3609);
INSERT INTO `monsterspawns` VALUES (4623, 1700, 0435, 0556, 0070, 0007, 0007, 0090, 0003, 3609);
INSERT INTO `monsterspawns` VALUES (4622, 1700, 0435, 0516, 0050, 0020, 0010, 0060, 0005, 3608);
INSERT INTO `monsterspawns` VALUES (4621, 1700, 0445, 0578, 0050, 0020, 0010, 0060, 0005, 3608);
INSERT INTO `monsterspawns` VALUES (4620, 1700, 0445, 0633, 0050, 0020, 0010, 0060, 0005, 3608);
INSERT INTO `monsterspawns` VALUES (4619, 1700, 0480, 0690, 0020, 0050, 0010, 0060, 0005, 3608);
INSERT INTO `monsterspawns` VALUES (4618, 1700, 0440, 0690, 0020, 0050, 0010, 0060, 0005, 3608);
INSERT INTO `monsterspawns` VALUES (4617, 1700, 0270, 0635, 0000, 0000, 0001, 0900, 0001, 3607);
INSERT INTO `monsterspawns` VALUES (4616, 1700, 0260, 0610, 0005, 0050, 0005, 0300, 0002, 3606);
INSERT INTO `monsterspawns` VALUES (4615, 1700, 0272, 0630, 0007, 0070, 0007, 0090, 0003, 3605);
INSERT INTO `monsterspawns` VALUES (4614, 1700, 0272, 0547, 0007, 0070, 0007, 0090, 0003, 3605);
INSERT INTO `monsterspawns` VALUES (4613, 1700, 0210, 0610, 0020, 0050, 0010, 0060, 0005, 3604);
INSERT INTO `monsterspawns` VALUES (4606, 1700, 0150, 0550, 0007, 0070, 0007, 0090, 0003, 3601);
INSERT INTO `monsterspawns` VALUES (4605, 1700, 0120, 0550, 0007, 0070, 0007, 0090, 0003, 3601);
INSERT INTO `monsterspawns` VALUES (4604, 1700, 0127, 0542, 0020, 0050, 0010, 0060, 0005, 3600);
INSERT INTO `monsterspawns` VALUES (4602, 1700, 0069, 0565, 0050, 0020, 0010, 0060, 0005, 3600);
INSERT INTO `monsterspawns` VALUES (4600, 1700, 0069, 0505, 0050, 0020, 0010, 0060, 0005, 3600);
INSERT INTO `monsterspawns` VALUES (2008, 1360, 0336, 0363, 0100, 0120, 0040, 0001, 0050, 3166);
INSERT INTO `monsterspawns` VALUES (4603, 1700, 0120, 0615, 0050, 0020, 0010, 0060, 0005, 3600);
INSERT INTO `monsterspawns` VALUES (4601, 1700, 0069, 0535, 0050, 0020, 0010, 0060, 0005, 3600);
INSERT INTO `monsterspawns` VALUES (4607, 1700, 0090, 0505, 0050, 0005, 0005, 0300, 0002, 3602);
INSERT INTO `monsterspawns` VALUES (4608, 1700, 0120, 0595, 0000, 0000, 0001, 0900, 0001, 3603);
INSERT INTO `monsterspawns` VALUES (4609, 1700, 0300, 0667, 0020, 0050, 0010, 0060, 0005, 3604);
INSERT INTO `monsterspawns` VALUES (4612, 1700, 0210, 0560, 0020, 0050, 0010, 0060, 0005, 3604);
INSERT INTO `monsterspawns` VALUES (4611, 1700, 0300, 0547, 0020, 0050, 0010, 0060, 0005, 3604);
INSERT INTO `monsterspawns` VALUES (4610, 1700, 0300, 0607, 0020, 0050, 0010, 0060, 0005, 3604);
INSERT INTO `monsterspawns` VALUES (900055, 1700, 0289, 0440, 0050, 0050, 0001, 0001, 0001, 9031);
INSERT INTO `monsterspawns` VALUES (900056, 1700, 0435, 0888, 0000, 0000, 0001, 1800, 0001, 9033);
INSERT INTO `monsterspawns` VALUES (900057, 1700, 0663, 1004, 0000, 0000, 0001, 1800, 0001, 9034);
INSERT INTO `monsterspawns` VALUES (900058, 1700, 0623, 0234, 0000, 0000, 0001, 1800, 0001, 9035);
INSERT INTO `monsterspawns` VALUES (900059, 1700, 0841, 0452, 0000, 0000, 0001, 1800, 0001, 9036);
INSERT INTO `monsterspawns` VALUES (900060, 1700, 0688, 0509, 0000, 0000, 0001, 1800, 0001, 9037);
INSERT INTO `monsterspawns` VALUES (900061, 1700, 0983, 0665, 0000, 0000, 0001, 1800, 0001, 9038);
INSERT INTO `monsterspawns` VALUES (900053, 1700, 0882, 0928, 0000, 0000, 0001, 1800, 0001, 9032);
INSERT INTO `monsterspawns` VALUES (0017, 2207, 0212, 0227, 0212, 0227, 0015, 0015, 0025, 5046);
INSERT INTO `monsterspawns` VALUES (0057, 7007, 0126, 0293, 0126, 0293, 0065, 0001, 0055, 2418);
INSERT INTO `monsterspawns` VALUES (56456466, 3856, 0150, 0001, 0229, 0379, 0100, 0001, 0100, 7563);
INSERT INTO `monsterspawns` VALUES (5645646, 3856, 0001, 0160, 0391, 0238, 0100, 0001, 0100, 7563);
INSERT INTO `monsterspawns` VALUES (0137, 1021, 0026, 0030, 0030, 0030, 0018, 0001, 0002, 0051);
INSERT INTO `monsterspawns` VALUES (0138, 1003, 0026, 0030, 0030, 0030, 0018, 0001, 0002, 0059);
INSERT INTO `monsterspawns` VALUES (6000, 1043, 0022, 0056, 0081, 0044, 0020, 0008, 0003, 6000);
INSERT INTO `monsterspawns` VALUES (6001, 1043, 0050, 0016, 0027, 0034, 0020, 0010, 0003, 6000);
INSERT INTO `monsterspawns` VALUES (6002, 1043, 0106, 0057, 0036, 0037, 0020, 0010, 0003, 6000);
INSERT INTO `monsterspawns` VALUES (6003, 1043, 0106, 0100, 0020, 0063, 0020, 0010, 0003, 6000);
INSERT INTO `monsterspawns` VALUES (6004, 1043, 0129, 0104, 0057, 0090, 0020, 0008, 0003, 6000);
INSERT INTO `monsterspawns` VALUES (6005, 1043, 0061, 0104, 0039, 0033, 0020, 0010, 0003, 6000);
INSERT INTO `monsterspawns` VALUES (6006, 1044, 0022, 0056, 0081, 0044, 0020, 0008, 0003, 6001);
INSERT INTO `monsterspawns` VALUES (6007, 1044, 0050, 0016, 0027, 0034, 0020, 0010, 0003, 6001);
INSERT INTO `monsterspawns` VALUES (6008, 1044, 0106, 0057, 0036, 0037, 0020, 0010, 0003, 6001);
INSERT INTO `monsterspawns` VALUES (6009, 1044, 0106, 0100, 0020, 0063, 0020, 0010, 0003, 6001);
INSERT INTO `monsterspawns` VALUES (6010, 1044, 0129, 0104, 0057, 0090, 0020, 0008, 0003, 6001);
INSERT INTO `monsterspawns` VALUES (6011, 1044, 0061, 0104, 0039, 0033, 0020, 0010, 0003, 6001);
INSERT INTO `monsterspawns` VALUES (6012, 1045, 0022, 0056, 0081, 0044, 0020, 0008, 0003, 6002);
INSERT INTO `monsterspawns` VALUES (6013, 1045, 0050, 0016, 0027, 0034, 0020, 0010, 0003, 6002);
INSERT INTO `monsterspawns` VALUES (6014, 1045, 0106, 0057, 0036, 0037, 0020, 0010, 0003, 6002);
INSERT INTO `monsterspawns` VALUES (6015, 1045, 0106, 0100, 0020, 0063, 0020, 0010, 0003, 6002);
INSERT INTO `monsterspawns` VALUES (6016, 1045, 0129, 0104, 0057, 0090, 0020, 0008, 0003, 6002);
INSERT INTO `monsterspawns` VALUES (6017, 1045, 0061, 0104, 0039, 0033, 0020, 0010, 0003, 6002);
INSERT INTO `monsterspawns` VALUES (6018, 1046, 0022, 0056, 0081, 0044, 0020, 0008, 0003, 6003);
INSERT INTO `monsterspawns` VALUES (6019, 1046, 0050, 0016, 0027, 0034, 0020, 0010, 0003, 6003);
INSERT INTO `monsterspawns` VALUES (6020, 1046, 0106, 0057, 0036, 0037, 0020, 0010, 0003, 6003);
INSERT INTO `monsterspawns` VALUES (6021, 1046, 0106, 0100, 0020, 0063, 0020, 0010, 0003, 6003);
INSERT INTO `monsterspawns` VALUES (6022, 1046, 0129, 0104, 0057, 0090, 0020, 0008, 0003, 6003);
INSERT INTO `monsterspawns` VALUES (6023, 1046, 0061, 0104, 0039, 0033, 0020, 0010, 0003, 6003);
INSERT INTO `monsterspawns` VALUES (6024, 1047, 0022, 0056, 0081, 0044, 0020, 0008, 0003, 6004);
INSERT INTO `monsterspawns` VALUES (6025, 1047, 0050, 0016, 0027, 0034, 0020, 0010, 0003, 6004);
INSERT INTO `monsterspawns` VALUES (6026, 1047, 0106, 0057, 0036, 0037, 0020, 0010, 0003, 6004);
INSERT INTO `monsterspawns` VALUES (6027, 1047, 0106, 0100, 0020, 0063, 0020, 0010, 0003, 6004);
INSERT INTO `monsterspawns` VALUES (6028, 1047, 0129, 0104, 0057, 0090, 0020, 0008, 0003, 6004);
INSERT INTO `monsterspawns` VALUES (6029, 1047, 0061, 0104, 0039, 0033, 0020, 0010, 0003, 6004);
INSERT INTO `monsterspawns` VALUES (6030, 1048, 0022, 0056, 0081, 0044, 0020, 0008, 0003, 6005);
INSERT INTO `monsterspawns` VALUES (6031, 1048, 0050, 0016, 0027, 0034, 0020, 0010, 0003, 6005);
INSERT INTO `monsterspawns` VALUES (6032, 1048, 0106, 0057, 0036, 0037, 0020, 0010, 0003, 6005);
INSERT INTO `monsterspawns` VALUES (6033, 1048, 0106, 0100, 0020, 0063, 0020, 0010, 0003, 6005);
INSERT INTO `monsterspawns` VALUES (6034, 1048, 0129, 0104, 0057, 0090, 0020, 0008, 0003, 6005);
INSERT INTO `monsterspawns` VALUES (6035, 1048, 0061, 0104, 0039, 0033, 0020, 0010, 0003, 6005);
INSERT INTO `monsterspawns` VALUES (6036, 1049, 0022, 0056, 0081, 0044, 0020, 0008, 0003, 6006);
INSERT INTO `monsterspawns` VALUES (6037, 1049, 0050, 0016, 0027, 0034, 0020, 0010, 0003, 6006);
INSERT INTO `monsterspawns` VALUES (6038, 1049, 0106, 0057, 0036, 0037, 0020, 0010, 0003, 6006);
INSERT INTO `monsterspawns` VALUES (6039, 1049, 0106, 0100, 0020, 0063, 0020, 0010, 0003, 6006);
INSERT INTO `monsterspawns` VALUES (6040, 1049, 0129, 0104, 0057, 0090, 0020, 0008, 0003, 6006);
INSERT INTO `monsterspawns` VALUES (6041, 1049, 0061, 0104, 0039, 0033, 0020, 0010, 0003, 6006);
INSERT INTO `monsterspawns` VALUES (3580, 1770, 0018, 0018, 0040, 0200, 0040, 0010, 0005, 2133);
INSERT INTO `monsterspawns` VALUES (3581, 1771, 0018, 0018, 0040, 0200, 0040, 0010, 0005, 2134);
INSERT INTO `monsterspawns` VALUES (3582, 1772, 0018, 0018, 0040, 0200, 0040, 0010, 0005, 2135);
INSERT INTO `monsterspawns` VALUES (1150, 1730, 0018, 0018, 0080, 0080, 0040, 0040, 0040, 3035);
INSERT INTO `monsterspawns` VALUES (1151, 1731, 0018, 0018, 0080, 0080, 0040, 0040, 0040, 3035);
INSERT INTO `monsterspawns` VALUES (1152, 1732, 0018, 0018, 0080, 0080, 0040, 0040, 0040, 3035);
INSERT INTO `monsterspawns` VALUES (1153, 1733, 0018, 0018, 0080, 0080, 0040, 0040, 0040, 3035);
INSERT INTO `monsterspawns` VALUES (3583, 1773, 0018, 0018, 0040, 0200, 0040, 0010, 0005, 2136);
INSERT INTO `monsterspawns` VALUES (3584, 1774, 0018, 0018, 0040, 0200, 0040, 0010, 0005, 2137);
INSERT INTO `monsterspawns` VALUES (3585, 1775, 0018, 0018, 0040, 0200, 0040, 0010, 0005, 2138);
INSERT INTO `monsterspawns` VALUES (3586, 1777, 0018, 0018, 0040, 0200, 0040, 0010, 0005, 2139);
INSERT INTO `monsterspawns` VALUES (1154, 1734, 0018, 0018, 0080, 0080, 0040, 0040, 0040, 3035);
INSERT INTO `monsterspawns` VALUES (1155, 1735, 0018, 0018, 0080, 0080, 0040, 0040, 0040, 3035);
INSERT INTO `monsterspawns` VALUES (0927, 1063, 0326, 0331, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0926, 1063, 0326, 0344, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0925, 1063, 0326, 0374, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0924, 1063, 0336, 0383, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0923, 1063, 0362, 0372, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0922, 1063, 0376, 0375, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0921, 1063, 0402, 0364, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0920, 1063, 0416, 0367, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (1100, 1076, 0051, 0319, 0200, 0100, 0013, 0010, 0005, 0007);
INSERT INTO `monsterspawns` VALUES (0928, 1063, 0334, 0304, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0929, 1063, 0333, 0291, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0930, 1063, 0342, 0264, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0931, 1063, 0334, 0256, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0932, 1063, 0304, 0255, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0933, 1063, 0290, 0252, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0934, 1063, 0264, 0263, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0935, 1063, 0250, 0260, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0936, 1063, 0224, 0271, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0937, 1063, 0214, 0262, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0938, 1063, 0214, 0232, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0939, 1063, 0213, 0219, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0940, 1063, 0222, 0192, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0941, 1063, 0222, 0179, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0942, 1063, 0230, 0152, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0943, 1063, 0222, 0144, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0944, 1063, 0192, 0143, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0945, 1063, 0178, 0140, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0946, 1063, 0152, 0151, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0947, 1063, 0138, 0149, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0948, 1063, 0112, 0159, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0949, 1063, 0102, 0150, 0000, 0000, 0002, 0005, 0001, 3100);
INSERT INTO `monsterspawns` VALUES (0953, 1063, 0405, 0361, 0004, 0004, 0002, 0008, 0002, 3101);
INSERT INTO `monsterspawns` VALUES (0954, 1063, 0365, 0369, 0004, 0004, 0002, 0008, 0002, 3101);
INSERT INTO `monsterspawns` VALUES (0955, 1063, 0325, 0377, 0004, 0004, 0002, 0008, 0002, 3101);
INSERT INTO `monsterspawns` VALUES (0956, 1063, 0321, 0333, 0004, 0004, 0002, 0008, 0002, 3101);
INSERT INTO `monsterspawns` VALUES (0957, 1063, 0329, 0293, 0004, 0004, 0002, 0008, 0002, 3101);
INSERT INTO `monsterspawns` VALUES (0958, 1063, 0337, 0253, 0004, 0004, 0002, 0008, 0002, 3101);
INSERT INTO `monsterspawns` VALUES (0959, 1063, 0293, 0249, 0004, 0004, 0002, 0008, 0002, 3101);
INSERT INTO `monsterspawns` VALUES (0960, 1063, 0253, 0257, 0004, 0004, 0002, 0008, 0002, 3101);
INSERT INTO `monsterspawns` VALUES (0961, 1063, 0213, 0265, 0004, 0004, 0002, 0008, 0002, 3101);
INSERT INTO `monsterspawns` VALUES (0962, 1063, 0210, 0221, 0004, 0004, 0002, 0008, 0002, 3101);
INSERT INTO `monsterspawns` VALUES (0963, 1063, 0218, 0182, 0004, 0004, 0002, 0008, 0002, 3101);
INSERT INTO `monsterspawns` VALUES (0964, 1063, 0226, 0142, 0004, 0004, 0002, 0008, 0002, 3101);
INSERT INTO `monsterspawns` VALUES (0965, 1063, 0181, 0137, 0004, 0004, 0002, 0008, 0002, 3101);
INSERT INTO `monsterspawns` VALUES (0966, 1063, 0142, 0146, 0004, 0004, 0002, 0008, 0002, 3101);
INSERT INTO `monsterspawns` VALUES (0967, 1063, 0102, 0154, 0004, 0004, 0002, 0008, 0002, 3101);
INSERT INTO `monsterspawns` VALUES (1001, 1063, 0068, 0065, 0000, 0000, 0002, 0180, 0001, 3103);
INSERT INTO `monsterspawns` VALUES (1004, 1063, 0077, 0075, 0000, 0000, 0002, 0180, 0001, 3103);
INSERT INTO `monsterspawns` VALUES (1005, 1063, 0084, 0081, 0000, 0000, 0002, 0180, 0001, 3103);
INSERT INTO `monsterspawns` VALUES (1007, 1063, 0101, 0081, 0000, 0000, 0002, 0180, 0001, 3103);
INSERT INTO `monsterspawns` VALUES (1012, 1063, 0088, 0048, 0000, 0000, 0002, 0180, 0001, 3103);
INSERT INTO `monsterspawns` VALUES (1013, 1063, 0092, 0055, 0000, 0000, 0002, 0180, 0001, 3103);
INSERT INTO `monsterspawns` VALUES (1015, 1063, 0104, 0064, 0000, 0000, 0002, 0180, 0001, 3103);
INSERT INTO `monsterspawns` VALUES (1017, 1063, 0068, 0048, 0000, 0000, 0002, 0180, 0001, 3103);
INSERT INTO `monsterspawns` VALUES (1000, 1063, 0084, 0064, 0000, 0000, 0002, 1800, 0001, 3102);
INSERT INTO `monsterspawns` VALUES (1101, 1076, 0251, 0407, 0200, 0170, 0013, 0010, 0005, 0007);
INSERT INTO `monsterspawns` VALUES (1102, 1076, 0448, 0320, 0290, 0175, 0013, 0010, 0005, 0008);
INSERT INTO `monsterspawns` VALUES (1103, 1076, 0286, 0087, 0160, 0120, 0013, 0010, 0005, 0006);
INSERT INTO `monsterspawns` VALUES (1104, 1076, 0800, 0617, 0087, 0130, 0013, 0010, 0005, 0009);
INSERT INTO `monsterspawns` VALUES (1105, 1076, 0673, 0729, 0130, 0100, 0013, 0010, 0005, 0009);
INSERT INTO `monsterspawns` VALUES (2997, 1016, 0055, 0055, 0089, 0089, 0003, 0060, 0003, 2997);
INSERT INTO `monsterspawns` VALUES (2996, 1016, 0029, 0029, 0065, 0065, 0001, 0360, 0001, 2996);
INSERT INTO `monsterspawns` VALUES (2392, 1013, 0026, 0026, 0065, 0065, 0030, 0001, 0030, 2392);
INSERT INTO `monsterspawns` VALUES (2393, 1013, 0050, 0043, 0079, 0084, 0020, 0001, 0020, 2392);
INSERT INTO `monsterspawns` VALUES (2394, 1013, 0050, 0043, 0079, 0084, 0002, 0060, 0002, 2997);
INSERT INTO `monsterspawns` VALUES (2395, 1013, 0026, 0026, 0065, 0065, 0001, 0060, 0001, 2997);
INSERT INTO `monsterspawns` VALUES (44446, 1013, 0050, 0043, 0079, 0084, 0001, 0360, 0001, 2996);
INSERT INTO `monsterspawns` VALUES (4454, 1014, 0055, 0055, 0164, 0154, 0070, 0001, 0070, 2391);
INSERT INTO `monsterspawns` VALUES (4453, 1014, 0055, 0055, 0164, 0154, 0005, 0060, 0005, 2997);
INSERT INTO `monsterspawns` VALUES (4452, 1014, 0055, 0055, 0164, 0154, 0002, 0360, 0002, 2996);
INSERT INTO `monsterspawns` VALUES (2411, 1762, 0106, 0052, 0300, 0372, 1000, 0001, 1000, 2411);
INSERT INTO `monsterspawns` VALUES (2412, 1762, 0374, 0347, 0585, 0578, 1000, 0001, 1000, 2411);
INSERT INTO `monsterspawns` VALUES (2416, 1927, 0150, 0168, 0625, 0652, 1500, 0001, 1500, 2416);
INSERT INTO `monsterspawns` VALUES (2461, 1999, 0036, 0036, 0400, 0380, 0700, 0001, 0700, 2461);
INSERT INTO `monsterspawns` VALUES (2462, 1999, 0400, 0380, 0713, 0738, 0700, 0001, 0700, 2461);
INSERT INTO `monsterspawns` VALUES (2466, 2054, 0227, 0180, 0630, 0603, 1500, 0001, 1500, 2466);
INSERT INTO `monsterspawns` VALUES (4151, 2055, 0000, 0000, 1000, 1000, 0017, 3600, 0017, 4151);
INSERT INTO `monsterspawns` VALUES (2479, 2056, 0185, 0175, 0654, 0679, 1000, 0001, 1000, 2479);
INSERT INTO `monsterspawns` VALUES (7022, 3071, 0001, 0001, 0390, 0390, 1000, 0001, 1000, 7022);
INSERT INTO `monsterspawns` VALUES (7023, 3071, 0001, 0001, 0390, 0390, 0030, 0001, 0030, 2699);
INSERT INTO `monsterspawns` VALUES (7024, 3071, 0001, 0001, 0390, 0390, 0007, 0001, 0007, 2700);
INSERT INTO `monsterspawns` VALUES (0051, 1077, 0700, 0650, 0100, 0120, 0018, 0001, 0008, 0015);
INSERT INTO `monsterspawns` VALUES (1507, 1523, 0130, 0132, 0010, 0010, 0010, 0001, 0000, 0022);
INSERT INTO `monsterspawns` VALUES (1506, 1523, 0130, 0094, 0010, 0010, 0010, 0001, 0001, 0093);
INSERT INTO `monsterspawns` VALUES (1505, 1523, 0109, 0059, 0010, 0010, 0010, 0001, 0001, 0068);
INSERT INTO `monsterspawns` VALUES (1504, 1523, 0075, 0073, 0010, 0010, 0010, 0001, 0001, 0010);
INSERT INTO `monsterspawns` VALUES (1503, 1523, 0062, 0089, 0010, 0010, 0010, 0001, 0001, 0006);
INSERT INTO `monsterspawns` VALUES (1502, 1523, 0082, 0120, 0010, 0010, 0010, 0001, 0001, 0003);
INSERT INTO `monsterspawns` VALUES (1501, 1523, 0086, 0114, 0010, 0010, 0010, 0001, 0001, 0078);
INSERT INTO `monsterspawns` VALUES (1500, 1523, 0101, 0107, 0010, 0010, 0010, 0001, 0001, 0028);
INSERT INTO `monsterspawns` VALUES (60011, 1043, 0001, 0001, 0300, 0300, 0350, 0001, 0350, 6000);
INSERT INTO `monsterspawns` VALUES (60002, 1044, 0001, 0001, 0300, 0300, 0350, 0001, 0350, 6000);
INSERT INTO `monsterspawns` VALUES (60003, 1045, 0001, 0001, 0300, 0300, 0350, 0001, 0350, 6000);
INSERT INTO `monsterspawns` VALUES (60005, 1046, 0001, 0001, 0300, 0300, 0350, 0001, 0350, 6000);
INSERT INTO `monsterspawns` VALUES (60007, 1047, 0001, 0001, 0300, 0300, 0350, 0001, 0350, 6000);
INSERT INTO `monsterspawns` VALUES (60008, 1048, 0001, 0001, 0300, 0300, 0350, 0001, 0350, 6000);
INSERT INTO `monsterspawns` VALUES (112553, 1351, 0001, 0001, 0500, 1000, 1000, 0001, 0500, 3141);
INSERT INTO `monsterspawns` VALUES (112213, 1351, 0001, 0001, 0500, 1000, 1000, 0001, 0500, 3142);
INSERT INTO `monsterspawns` VALUES (113223, 1352, 0001, 0001, 1000, 1000, 1000, 0001, 1000, 3144);
INSERT INTO `monsterspawns` VALUES (115923, 1353, 0001, 0001, 1000, 1000, 1000, 0001, 1000, 3148);
INSERT INTO `monsterspawns` VALUES (1151123, 1353, 0001, 0001, 1000, 1000, 1000, 0001, 1000, 3147);
INSERT INTO `monsterspawns` VALUES (113123, 1354, 0001, 0001, 1000, 1000, 1000, 0001, 1000, 3155);
INSERT INTO `monsterspawns` VALUES (22223, 1020, 0544, 0620, 0060, 0250, 0200, 0001, 0200, 0010);
INSERT INTO `monsterspawns` VALUES (35656, 1002, 0308, 0305, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (256213, 1002, 0312, 0233, 0001, 0001, 0001, 0001, 0001, 0910);
INSERT INTO `monsterspawns` VALUES (256216, 1002, 0278, 0292, 0001, 0001, 0001, 0001, 0001, 0910);
INSERT INTO `monsterspawns` VALUES (26716, 1011, 0204, 0272, 0001, 0001, 0001, 0001, 0001, 0910);
INSERT INTO `monsterspawns` VALUES (35651, 1002, 0318, 0305, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (35652, 1002, 0333, 0299, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (35653, 1002, 0333, 0286, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (35753, 1002, 0333, 0273, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (35752, 1002, 0335, 0232, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (35162, 1002, 0296, 0237, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (35122, 1002, 0297, 0253, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (35123, 1002, 0285, 0280, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (35113, 1002, 0382, 0259, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (45113, 1002, 0308, 0336, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (55113, 1002, 0324, 0338, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (52113, 1002, 0308, 0367, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (82113, 1002, 0308, 0376, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (88113, 1002, 0293, 0377, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (28113, 1002, 0264, 0376, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (281113, 1002, 0264, 0367, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (281213, 1002, 0066, 0345, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (282213, 1002, 0069, 0356, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (582213, 1000, 0973, 0664, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (582212, 1000, 0501, 0647, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (587212, 1000, 0476, 0642, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (583212, 1000, 0487, 0625, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (589212, 1000, 0489, 0587, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (58235, 1000, 0482, 0587, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (584351, 1000, 0081, 0316, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (582351, 1000, 0081, 0325, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (253351, 1001, 0317, 0643, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (4253351, 1001, 0311, 0645, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (853351, 1002, 0812, 0529, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (823351, 1002, 0811, 0518, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (823621, 1002, 0517, 0810, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (823821, 1002, 0527, 0809, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (823921, 1002, 0094, 0354, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (824921, 1002, 0095, 0367, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (123121, 1011, 0009, 0848, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (123221, 1011, 0010, 0383, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (1232221, 1011, 0190, 0261, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (132221, 1011, 0235, 0246, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (32221, 1011, 0241, 0248, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (322221, 1011, 0197, 0230, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (32222, 1011, 0378, 0048, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (322225, 1011, 0387, 0031, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (35689, 1020, 0381, 0010, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (356891, 1020, 0376, 0014, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (126891, 1020, 0548, 0548, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (326891, 1020, 0570, 0542, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (126861, 1020, 0561, 0561, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (235689, 1015, 1010, 0706, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (239689, 1015, 1013, 0704, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (239610, 1015, 0756, 0566, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (239611, 1015, 0761, 0544, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (1039612, 1015, 0715, 0573, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (103961, 1015, 0713, 0543, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (1039652, 1015, 0733, 0561, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (55558, 3055, 0056, 0086, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (65646, 3055, 0021, 0090, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (4554, 3856, 0375, 0266, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (54456, 3856, 0384, 0266, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (0026, 1001, 0309, 0597, 0044, 0051, 0011, 0001, 0005, 0020);
INSERT INTO `monsterspawns` VALUES (0027, 1001, 0257, 0549, 0040, 0063, 0011, 0001, 0005, 0020);
INSERT INTO `monsterspawns` VALUES (0028, 1001, 0207, 0518, 0040, 0030, 0011, 0001, 0005, 0020);
INSERT INTO `monsterspawns` VALUES (0029, 1001, 0317, 0514, 0025, 0026, 0011, 0001, 0005, 0020);
INSERT INTO `monsterspawns` VALUES (0030, 1001, 0241, 0473, 0062, 0055, 0011, 0001, 0005, 0020);
INSERT INTO `monsterspawns` VALUES (0031, 1001, 0362, 0605, 0036, 0030, 0011, 0001, 0005, 0020);
INSERT INTO `monsterspawns` VALUES (0075, 1001, 0309, 0597, 0044, 0051, 0039, 0001, 0003, 0080);
INSERT INTO `monsterspawns` VALUES (0076, 1001, 0257, 0549, 0040, 0063, 0039, 0001, 0003, 0080);
INSERT INTO `monsterspawns` VALUES (0077, 1001, 0207, 0518, 0040, 0030, 0039, 0001, 0002, 0080);
INSERT INTO `monsterspawns` VALUES (0078, 1001, 0317, 0514, 0025, 0026, 0039, 0001, 0001, 0080);
INSERT INTO `monsterspawns` VALUES (0079, 1001, 0241, 0473, 0062, 0055, 0039, 0001, 0003, 0080);
INSERT INTO `monsterspawns` VALUES (0080, 1001, 0362, 0605, 0036, 0030, 0039, 0001, 0002, 0080);
INSERT INTO `monsterspawns` VALUES (0083, 1001, 0388, 0371, 0040, 0024, 0039, 0001, 0002, 0081);
INSERT INTO `monsterspawns` VALUES (0084, 1001, 0220, 0347, 0038, 0024, 0039, 0001, 0002, 0081);
INSERT INTO `monsterspawns` VALUES (0085, 1001, 0347, 0378, 0030, 0026, 0039, 0001, 0002, 0081);
INSERT INTO `monsterspawns` VALUES (0086, 1001, 0253, 0304, 0070, 0053, 0039, 0001, 0005, 0081);
INSERT INTO `monsterspawns` VALUES (0087, 1001, 0329, 0343, 0026, 0025, 0039, 0001, 0001, 0081);
INSERT INTO `monsterspawns` VALUES (0088, 1001, 0320, 0425, 0090, 0064, 0039, 0001, 0006, 0081);
INSERT INTO `monsterspawns` VALUES (0089, 1001, 0162, 0319, 0065, 0060, 0039, 0001, 0005, 0082);
INSERT INTO `monsterspawns` VALUES (0090, 1001, 0052, 0323, 0035, 0025, 0039, 0001, 0002, 0082);
INSERT INTO `monsterspawns` VALUES (0091, 1001, 0085, 0279, 0070, 0040, 0039, 0001, 0005, 0082);
INSERT INTO `monsterspawns` VALUES (0092, 1001, 0101, 0353, 0040, 0063, 0039, 0001, 0004, 0082);
INSERT INTO `monsterspawns` VALUES (0093, 1001, 0153, 0401, 0090, 0063, 0039, 0001, 0006, 0082);
INSERT INTO `monsterspawns` VALUES (0094, 1001, 0342, 0171, 0046, 0060, 0039, 0001, 0004, 0083);
INSERT INTO `monsterspawns` VALUES (0095, 1001, 0265, 0131, 0067, 0041, 0039, 0001, 0004, 0083);
INSERT INTO `monsterspawns` VALUES (0096, 1001, 0276, 0196, 0045, 0072, 0039, 0001, 0005, 0083);
INSERT INTO `monsterspawns` VALUES (0097, 1001, 0232, 0175, 0037, 0024, 0039, 0001, 0002, 0083);
INSERT INTO `monsterspawns` VALUES (0098, 1001, 0333, 0253, 0090, 0063, 0039, 0001, 0006, 0083);
INSERT INTO `monsterspawns` VALUES (0141, 1001, 0264, 0368, 0045, 0072, 0011, 0001, 0005, 0086);
INSERT INTO `monsterspawns` VALUES (0142, 1001, 0421, 0327, 0068, 0041, 0011, 0001, 0005, 0086);
INSERT INTO `monsterspawns` VALUES (0143, 1001, 0433, 0155, 0070, 0054, 0011, 0001, 0005, 0086);
INSERT INTO `monsterspawns` VALUES (0144, 1001, 0510, 0196, 0045, 0056, 0011, 0001, 0005, 0086);
INSERT INTO `monsterspawns` VALUES (0145, 1001, 0489, 0449, 0090, 0060, 0011, 0001, 0005, 0086);
INSERT INTO `monsterspawns` VALUES (0146, 1001, 0498, 0367, 0046, 0060, 0011, 0001, 0005, 0088);
INSERT INTO `monsterspawns` VALUES (0147, 1001, 0501, 0277, 0090, 0065, 0011, 0001, 0005, 0088);
INSERT INTO `monsterspawns` VALUES (0148, 1001, 0432, 0392, 0045, 0072, 0011, 0001, 0005, 0088);
INSERT INTO `monsterspawns` VALUES (0149, 1001, 0400, 0199, 0037, 0024, 0011, 0001, 0005, 0088);
INSERT INTO `monsterspawns` VALUES (0150, 1001, 0444, 0220, 0055, 0072, 0011, 0001, 0005, 0088);
INSERT INTO `monsterspawns` VALUES (0153, 1001, 0264, 0368, 0045, 0072, 0033, 0001, 0005, 0087);
INSERT INTO `monsterspawns` VALUES (0154, 1001, 0421, 0327, 0068, 0041, 0033, 0001, 0003, 0087);
INSERT INTO `monsterspawns` VALUES (0155, 1001, 0433, 0155, 0070, 0054, 0033, 0001, 0005, 0087);
INSERT INTO `monsterspawns` VALUES (0156, 1001, 0510, 0196, 0045, 0056, 0033, 0001, 0003, 0087);
INSERT INTO `monsterspawns` VALUES (0157, 1001, 0489, 0449, 0090, 0060, 0043, 0001, 0006, 0087);
INSERT INTO `monsterspawns` VALUES (0158, 1001, 0498, 0367, 0046, 0060, 0033, 0001, 0003, 0089);
INSERT INTO `monsterspawns` VALUES (0159, 1001, 0501, 0277, 0090, 0065, 0043, 0001, 0006, 0089);
INSERT INTO `monsterspawns` VALUES (0160, 1001, 0432, 0392, 0045, 0072, 0033, 0001, 0004, 0089);
INSERT INTO `monsterspawns` VALUES (0161, 1001, 0400, 0199, 0037, 0024, 0018, 0001, 0002, 0089);
INSERT INTO `monsterspawns` VALUES (0162, 1001, 0444, 0220, 0055, 0072, 0033, 0001, 0005, 0089);
INSERT INTO `monsterspawns` VALUES (0208, 1001, 0333, 0253, 0090, 0063, 0103, 3600, 0001, 0093);
INSERT INTO `monsterspawns` VALUES (0207, 1001, 0153, 0401, 0090, 0063, 0103, 3600, 0001, 0092);
INSERT INTO `monsterspawns` VALUES (0206, 1001, 0320, 0425, 0090, 0064, 0103, 3600, 0001, 0091);
INSERT INTO `monsterspawns` VALUES (0305, 1001, 0317, 0643, 0001, 0001, 0001, 0010, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (4408, 1001, 0728, 0676, 0001, 0001, 0001, 0060, 0001, 2189);
INSERT INTO `monsterspawns` VALUES (4506, 1001, 0089, 0280, 0020, 0020, 0010, 0001, 0003, 2231);
INSERT INTO `monsterspawns` VALUES (4507, 1001, 0097, 0343, 0020, 0020, 0010, 0001, 0003, 2231);
INSERT INTO `monsterspawns` VALUES (4508, 1001, 0184, 0384, 0020, 0020, 0010, 0001, 0003, 2231);
INSERT INTO `monsterspawns` VALUES (4509, 1001, 0186, 0359, 0001, 0001, 0001, 0010, 0001, 2232);
INSERT INTO `monsterspawns` VALUES (8016, 1001, 0241, 0473, 0062, 0055, 0048, 0300, 0001, 8116);
INSERT INTO `monsterspawns` VALUES (8018, 1001, 0388, 0371, 0040, 0024, 0053, 0300, 0001, 8117);
INSERT INTO `monsterspawns` VALUES (8019, 1001, 0162, 0319, 0065, 0060, 0053, 0300, 0001, 8118);
INSERT INTO `monsterspawns` VALUES (8020, 1001, 0333, 0253, 0090, 0063, 0053, 0300, 0001, 8119);
INSERT INTO `monsterspawns` VALUES (8036, 1001, 0241, 0473, 0062, 0055, 0053, 0300, 0001, 8216);
INSERT INTO `monsterspawns` VALUES (8038, 1001, 0388, 0371, 0040, 0024, 0053, 0300, 0001, 8217);
INSERT INTO `monsterspawns` VALUES (8039, 1001, 0162, 0319, 0065, 0060, 0053, 0300, 0001, 8218);
INSERT INTO `monsterspawns` VALUES (8040, 1001, 0333, 0253, 0090, 0063, 0053, 0300, 0001, 8219);
INSERT INTO `monsterspawns` VALUES (8056, 1001, 0241, 0473, 0062, 0055, 0058, 0300, 0001, 8316);
INSERT INTO `monsterspawns` VALUES (8058, 1001, 0388, 0371, 0040, 0024, 0068, 0300, 0001, 8317);
INSERT INTO `monsterspawns` VALUES (8059, 1001, 0162, 0319, 0065, 0060, 0068, 0300, 0001, 8318);
INSERT INTO `monsterspawns` VALUES (8060, 1001, 0333, 0253, 0090, 0063, 0068, 0300, 0001, 8319);
INSERT INTO `monsterspawns` VALUES (2323, 1300, 0257, 0549, 0040, 0063, 0039, 0001, 0003, 0201);
INSERT INTO `monsterspawns` VALUES (2300, 1300, 0309, 0597, 0044, 0051, 0015, 0001, 0003, 0200);
INSERT INTO `monsterspawns` VALUES (2301, 1300, 0257, 0549, 0040, 0063, 0015, 0001, 0003, 0200);
INSERT INTO `monsterspawns` VALUES (2302, 1300, 0207, 0518, 0040, 0030, 0015, 0001, 0002, 0200);
INSERT INTO `monsterspawns` VALUES (2303, 1300, 0317, 0514, 0025, 0026, 0015, 0001, 0001, 0200);
INSERT INTO `monsterspawns` VALUES (2304, 1300, 0241, 0473, 0062, 0055, 0015, 0001, 0003, 0200);
INSERT INTO `monsterspawns` VALUES (2305, 1300, 0362, 0605, 0036, 0030, 0015, 0001, 0002, 0200);
INSERT INTO `monsterspawns` VALUES (2306, 1300, 0388, 0371, 0040, 0024, 0015, 0001, 0002, 0200);
INSERT INTO `monsterspawns` VALUES (2307, 1300, 0220, 0347, 0038, 0024, 0015, 0001, 0002, 0200);
INSERT INTO `monsterspawns` VALUES (2308, 1300, 0347, 0378, 0030, 0026, 0015, 0001, 0002, 0200);
INSERT INTO `monsterspawns` VALUES (2309, 1300, 0253, 0304, 0070, 0053, 0015, 0001, 0005, 0200);
INSERT INTO `monsterspawns` VALUES (2310, 1300, 0329, 0343, 0026, 0025, 0015, 0001, 0001, 0200);
INSERT INTO `monsterspawns` VALUES (2311, 1300, 0320, 0425, 0090, 0064, 0015, 0001, 0006, 0200);
INSERT INTO `monsterspawns` VALUES (2312, 1300, 0162, 0319, 0065, 0060, 0015, 0001, 0005, 0200);
INSERT INTO `monsterspawns` VALUES (2313, 1300, 0052, 0323, 0050, 0050, 0015, 0001, 0002, 0200);
INSERT INTO `monsterspawns` VALUES (0034, 1001, 0388, 0371, 0040, 0024, 0015, 0001, 0020, 0056);
INSERT INTO `monsterspawns` VALUES (0035, 1001, 0220, 0347, 0038, 0024, 0015, 0001, 0020, 0056);
INSERT INTO `monsterspawns` VALUES (0036, 1001, 0347, 0378, 0030, 0026, 0015, 0001, 0020, 0056);
INSERT INTO `monsterspawns` VALUES (0037, 1001, 0253, 0304, 0070, 0053, 0015, 0001, 0020, 0056);
INSERT INTO `monsterspawns` VALUES (0038, 1001, 0329, 0343, 0026, 0025, 0015, 0001, 0020, 0056);
INSERT INTO `monsterspawns` VALUES (0039, 1001, 0320, 0425, 0090, 0064, 0015, 0001, 0020, 0056);
INSERT INTO `monsterspawns` VALUES (0040, 1001, 0162, 0319, 0065, 0060, 0015, 0001, 0020, 0057);
INSERT INTO `monsterspawns` VALUES (0041, 1001, 0052, 0323, 0035, 0025, 0015, 0001, 0020, 0057);
INSERT INTO `monsterspawns` VALUES (0042, 1001, 0085, 0279, 0070, 0040, 0015, 0001, 0020, 0057);
INSERT INTO `monsterspawns` VALUES (0043, 1001, 0101, 0353, 0040, 0063, 0015, 0001, 0020, 0057);
INSERT INTO `monsterspawns` VALUES (0044, 1001, 0153, 0401, 0090, 0063, 0015, 0001, 0020, 0057);
INSERT INTO `monsterspawns` VALUES (0045, 1001, 0232, 0175, 0037, 0024, 0015, 0001, 0020, 0058);
INSERT INTO `monsterspawns` VALUES (0046, 1001, 0333, 0253, 0090, 0063, 0015, 0001, 0020, 0058);
INSERT INTO `monsterspawns` VALUES (0047, 1001, 0342, 0171, 0046, 0060, 0015, 0001, 0020, 0058);
INSERT INTO `monsterspawns` VALUES (0048, 1001, 0265, 0131, 0067, 0041, 0015, 0001, 0020, 0058);
INSERT INTO `monsterspawns` VALUES (0049, 1001, 0276, 0196, 0045, 0072, 0015, 0001, 0020, 0058);
INSERT INTO `monsterspawns` VALUES (4446, 3955, 0001, 0001, 0600, 0600, 0150, 0001, 0150, 5045);
INSERT INTO `monsterspawns` VALUES (4564, 3955, 0001, 0001, 0600, 0600, 0300, 0001, 0300, 5046);
INSERT INTO `monsterspawns` VALUES (458545, 3957, 0504, 0217, 0010, 0010, 0010, 0001, 0010, 5060);
INSERT INTO `monsterspawns` VALUES (515515, 3955, 0001, 0001, 0600, 0600, 0300, 0001, 0300, 5050);
INSERT INTO `monsterspawns` VALUES (646456, 3955, 0001, 0001, 0600, 0600, 0300, 0001, 0300, 5051);
INSERT INTO `monsterspawns` VALUES (45644, 3956, 0001, 0001, 0400, 0400, 0300, 0001, 0300, 5053);
INSERT INTO `monsterspawns` VALUES (51564, 3956, 0001, 0001, 0400, 0400, 0300, 0001, 0300, 5054);
INSERT INTO `monsterspawns` VALUES (55556, 3956, 0001, 0001, 0400, 0400, 0300, 0001, 0300, 5056);
INSERT INTO `monsterspawns` VALUES (454664, 3956, 0001, 0001, 0400, 0400, 0300, 0001, 0300, 5057);
INSERT INTO `monsterspawns` VALUES (8484, 3957, 0337, 0117, 0010, 0010, 0010, 0001, 0010, 5060);
INSERT INTO `monsterspawns` VALUES (415514, 3957, 0236, 0218, 0010, 0010, 0010, 0001, 0010, 5060);
INSERT INTO `monsterspawns` VALUES (56161, 3957, 0127, 0318, 0010, 0010, 0010, 0001, 0010, 5060);
INSERT INTO `monsterspawns` VALUES (556466, 3957, 0169, 0470, 0010, 0010, 0010, 0001, 0010, 5060);
INSERT INTO `monsterspawns` VALUES (48441, 3957, 0263, 0413, 0010, 0010, 0010, 0001, 0010, 5060);
INSERT INTO `monsterspawns` VALUES (44584, 3957, 0218, 0287, 0010, 0010, 0010, 0001, 0010, 5060);
INSERT INTO `monsterspawns` VALUES (5464, 3957, 0362, 0278, 0010, 0010, 0010, 0001, 0010, 5060);
INSERT INTO `monsterspawns` VALUES (5446, 3957, 0416, 0368, 0010, 0010, 0010, 0001, 0010, 5060);
INSERT INTO `monsterspawns` VALUES (45446, 3958, 0001, 0001, 0300, 0300, 0300, 0001, 0300, 5058);
INSERT INTO `monsterspawns` VALUES (11332, 1737, 0018, 0018, 0080, 0080, 0040, 0040, 0040, 3035);
INSERT INTO `monsterspawns` VALUES (945446, 3935, 0001, 0001, 0700, 0700, 0600, 0001, 0600, 7896);
INSERT INTO `monsterspawns` VALUES (5466, 3935, 0001, 0001, 0700, 0700, 0100, 0001, 0100, 7897);
INSERT INTO `monsterspawns` VALUES (45646, 3935, 0397, 0256, 0006, 0001, 0001, 0030, 0001, 7898);
INSERT INTO `monsterspawns` VALUES (456464, 3935, 0400, 0296, 0001, 0001, 0001, 0030, 0001, 7898);
INSERT INTO `monsterspawns` VALUES (456445, 3935, 0348, 0263, 0001, 0001, 0001, 0030, 0001, 7898);
INSERT INTO `monsterspawns` VALUES (45646456, 3935, 0349, 0275, 0001, 0001, 0001, 0030, 0001, 7898);
INSERT INTO `monsterspawns` VALUES (56464, 3935, 0382, 0293, 0015, 0015, 0025, 0001, 0025, 7891);
INSERT INTO `monsterspawns` VALUES (56616, 3935, 0367, 0458, 0015, 0015, 0025, 0001, 0025, 7890);
INSERT INTO `monsterspawns` VALUES (5464564, 3935, 0471, 0370, 0015, 0015, 0025, 0001, 0025, 7889);
INSERT INTO `monsterspawns` VALUES (21615, 3935, 0296, 0340, 0015, 0015, 0025, 0001, 0025, 7892);
INSERT INTO `monsterspawns` VALUES (54564, 3935, 0307, 0277, 0001, 0001, 0001, 0030, 0001, 7898);
INSERT INTO `monsterspawns` VALUES (456456, 3935, 0288, 0413, 0001, 0001, 0001, 0030, 0001, 7898);
INSERT INTO `monsterspawns` VALUES (4544949, 3935, 0298, 0434, 0001, 0001, 0001, 0030, 0001, 7898);
INSERT INTO `monsterspawns` VALUES (656463, 3935, 0315, 0461, 0001, 0001, 0001, 0030, 0001, 7898);
INSERT INTO `monsterspawns` VALUES (56446, 3935, 0397, 0504, 0001, 0001, 0001, 0030, 0001, 7898);
INSERT INTO `monsterspawns` VALUES (44494949, 3935, 0410, 0502, 0001, 0001, 0001, 0030, 0001, 7898);
INSERT INTO `monsterspawns` VALUES (456456145, 3935, 0433, 0493, 0001, 0001, 0001, 0030, 0001, 7898);
INSERT INTO `monsterspawns` VALUES (8894984, 3935, 0421, 0491, 0001, 0001, 0001, 0030, 0001, 7898);
INSERT INTO `monsterspawns` VALUES (545464, 3935, 0475, 0497, 0001, 0001, 0001, 0030, 0001, 7898);
INSERT INTO `monsterspawns` VALUES (6161464, 3935, 0441, 0492, 0001, 0001, 0001, 0030, 0001, 7898);
INSERT INTO `monsterspawns` VALUES (6545664, 3935, 0417, 0502, 0001, 0001, 0001, 0030, 0001, 7898);
INSERT INTO `monsterspawns` VALUES (565645456, 3935, 0389, 0507, 0001, 0001, 0001, 0030, 0001, 7898);
INSERT INTO `monsterspawns` VALUES (44944, 3935, 0317, 0474, 0001, 0001, 0001, 0030, 0001, 7898);
INSERT INTO `monsterspawns` VALUES (56546, 1002, 0278, 0255, 0001, 0001, 0001, 0001, 0001, 0900);
INSERT INTO `monsterspawns` VALUES (90085, 3998, 0171, 0360, 0020, 0020, 0050, 0001, 0020, 2758);
INSERT INTO `monsterspawns` VALUES (90086, 3998, 0264, 0422, 0020, 0020, 0050, 0001, 0020, 2759);
INSERT INTO `monsterspawns` VALUES (90087, 3998, 0400, 0392, 0706, 0020, 0050, 0001, 0020, 3526);
INSERT INTO `monsterspawns` VALUES (90088, 3998, 0252, 0297, 0020, 0020, 0050, 0001, 0020, 2761);
INSERT INTO `monsterspawns` VALUES (90089, 3998, 0597, 0441, 0020, 0020, 0050, 0001, 0020, 2762);
INSERT INTO `monsterspawns` VALUES (90090, 3998, 0307, 0098, 0020, 0020, 0050, 0001, 0020, 2763);
INSERT INTO `monsterspawns` VALUES (90091, 3998, 0481, 0220, 0020, 0020, 0050, 0001, 0020, 2764);
INSERT INTO `monsterspawns` VALUES (90092, 3998, 0527, 0349, 0020, 0020, 0050, 0001, 0020, 2765);
INSERT INTO `monsterspawns` VALUES (90093, 3998, 0145, 0338, 0030, 0035, 0050, 0001, 0020, 2758);
INSERT INTO `monsterspawns` VALUES (90094, 3998, 0250, 0406, 0030, 0035, 0050, 0001, 0020, 2759);
INSERT INTO `monsterspawns` VALUES (90095, 3998, 0392, 0706, 0020, 0020, 0005, 0001, 0005, 2760);
INSERT INTO `monsterspawns` VALUES (545451, 1005, 0051, 0051, 0020, 0020, 0005, 6000, 0001, 4152);
INSERT INTO `monsterspawns` VALUES (54545, 1927, 0529, 0424, 0020, 0020, 0000, 3000, 0001, 4171);
INSERT INTO `monsterspawns` VALUES (215151, 1010, 0090, 0056, 0020, 0020, 0000, 6000, 0001, 4170);
INSERT INTO `monsterspawns` VALUES (1233, 3998, 0079, 0350, 0001, 0001, 0001, 1800, 0001, 417100);
INSERT INTO `monsterspawns` VALUES (12354, 39981, 0079, 0350, 0001, 0001, 0001, 1800, 0001, 417101);
INSERT INTO `monsterspawns` VALUES (12365, 39982, 0079, 0350, 0001, 0001, 0001, 1800, 0001, 417102);
INSERT INTO `monsterspawns` VALUES (12552, 39983, 0079, 0350, 0001, 0001, 0001, 1800, 0001, 417103);
INSERT INTO `monsterspawns` VALUES (123399, 39984, 0079, 0350, 0001, 0001, 0001, 1800, 0001, 417104);
INSERT INTO `monsterspawns` VALUES (123321, 39985, 0079, 0350, 0001, 0001, 0001, 1800, 0001, 417105);
INSERT INTO `monsterspawns` VALUES (123543, 39986, 0079, 0350, 0001, 0001, 0001, 1800, 0001, 417106);
INSERT INTO `monsterspawns` VALUES (1236556, 39987, 0079, 0350, 0001, 0001, 0001, 1800, 0001, 417107);
INSERT INTO `monsterspawns` VALUES (1255246, 39988, 0079, 0350, 0001, 0001, 0001, 1800, 0001, 417108);
INSERT INTO `monsterspawns` VALUES (1233993, 39989, 0079, 0350, 0001, 0001, 0001, 1800, 0001, 417109);
INSERT INTO `monsterspawns` VALUES (1233456, 39990, 0079, 0350, 0001, 0001, 0001, 1800, 0001, 417110);
INSERT INTO `monsterspawns` VALUES (1235445, 39991, 0079, 0350, 0001, 0001, 0001, 1800, 0001, 417111);
INSERT INTO `monsterspawns` VALUES (1236599, 39992, 0079, 0350, 0001, 0001, 0001, 1800, 0001, 417112);
INSERT INTO `monsterspawns` VALUES (1255249, 39993, 0079, 0350, 0001, 0001, 0001, 1800, 0001, 417113);
INSERT INTO `monsterspawns` VALUES (12339931, 39994, 0079, 0350, 0001, 0001, 0001, 1800, 0001, 417114);
INSERT INTO `monsterspawns` VALUES (111125, 10002, 0401, 0401, 0001, 0001, 0001, 1800, 0001, 111125);

-- ----------------------------
-- Table structure for nobility
-- ----------------------------
DROP TABLE IF EXISTS `nobility`;
CREATE TABLE `nobility`  (
  `EntityName` varchar(16) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `EntityUID` bigint(255) UNSIGNED NOT NULL DEFAULT 0,
  `Donation` bigint(255) UNSIGNED NULL DEFAULT 0,
  `Gender` tinyint(5) UNSIGNED NOT NULL DEFAULT 0,
  `Mesh` bigint(255) UNSIGNED NOT NULL DEFAULT 0,
  PRIMARY KEY (`EntityUID`) USING BTREE,
  UNIQUE INDEX `myIndex`(`EntityUID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of nobility
-- ----------------------------
INSERT INTO `nobility` VALUES ('MyMido', 1000010, 1410065407, 3, 351003);
INSERT INTO `nobility` VALUES ('EgYCo', 1000001, 2371699370, 3, 331003);
INSERT INTO `nobility` VALUES ('RAVEN', 1000013, 30000000, 3, 1391003);

-- ----------------------------
-- Table structure for npcs
-- ----------------------------
DROP TABLE IF EXISTS `npcs`;
CREATE TABLE `npcs`  (
  `id` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `type` int(4) UNSIGNED ZEROFILL NULL DEFAULT 0000,
  `lookface` int(4) UNSIGNED ZEROFILL NULL DEFAULT 0000,
  `mapid` int(4) UNSIGNED ZEROFILL NULL DEFAULT 0000,
  `cellx` int(4) UNSIGNED ZEROFILL NULL DEFAULT 0000,
  `celly` int(4) UNSIGNED ZEROFILL NULL DEFAULT 0000,
  `task0` int(4) UNSIGNED ZEROFILL NULL DEFAULT 0000,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of npcs
-- ----------------------------
INSERT INTO `npcs` VALUES (19169, 'WhiteHerp', 0002, 40307, 3998, 0277, 0275, 0000);
INSERT INTO `npcs` VALUES (19170, 'WhiteHerp', 0002, 40307, 3998, 0281, 0275, 0000);
INSERT INTO `npcs` VALUES (19171, 'WhiteHerp', 0002, 40307, 3998, 0284, 0274, 0000);
INSERT INTO `npcs` VALUES (19175, 'WhiteHerp', 0002, 40307, 3998, 0291, 0277, 0000);
INSERT INTO `npcs` VALUES (0924, 'LadyLuck', 0002, 9236, 0700, 0040, 0050, 5050060);
INSERT INTO `npcs` VALUES (1503, 'CloudBeast', 0002, 1297, 1036, 0184, 0220, 0000);
INSERT INTO `npcs` VALUES (0432, 'GreatMerchant', 0001, 1390, 1036, 0212, 0179, 0000);
INSERT INTO `npcs` VALUES (100229, 'Sam', 0002, 1270, 1002, 0340, 0133, 7402050);
INSERT INTO `npcs` VALUES (0433, 'ConstumerChen', 0001, 0330, 0000, 0204, 0179, 0000);
INSERT INTO `npcs` VALUES (1002, 'WardenZhang', 0002, 1816, 5000, 0413, 0385, 5002300);
INSERT INTO `npcs` VALUES (1001, 'PrizeOfficer', 0002, 1067, 6000, 0039, 0066, 0000);
INSERT INTO `npcs` VALUES (2004, 'DCCaptain', 0002, 1770, 1000, 0476, 0631, NULL);
INSERT INTO `npcs` VALUES (30060, 'MagicTaoist', 0002, 7121, 1012, 0245, 0235, 403000);
INSERT INTO `npcs` VALUES (0941, 'ProficiencyGod', 0002, 9410, 1036, 0189, 0212, 5050400);
INSERT INTO `npcs` VALUES (3952, 'CollectorWong', 0002, 3236, 0700, 0034, 0042, 0000);
INSERT INTO `npcs` VALUES (3961, 'Agate', 0002, 9446, 1735, 0046, 0040, 5055600);
INSERT INTO `npcs` VALUES (2006, 'MCCaptain', 0002, 1797, 1000, 0080, 0318, NULL);
INSERT INTO `npcs` VALUES (2002, 'PCCaptain', 0002, 1757, 1011, 0227, 0257, NULL);
INSERT INTO `npcs` VALUES (3956, 'Agate', 0002, 9446, 1730, 0044, 0044, 5055100);
INSERT INTO `npcs` VALUES (3957, 'Agate', 0002, 9446, 1731, 0044, 0037, 5055200);
INSERT INTO `npcs` VALUES (4454, '2ndFlameStone', 0002, 6580, 1038, 0317, 0270, 0000);
INSERT INTO `npcs` VALUES (3958, 'Agate', 0002, 9446, 1732, 0051, 0043, 5055300);
INSERT INTO `npcs` VALUES (3959, 'Agate', 0002, 9446, 1733, 0051, 0045, 5055400);
INSERT INTO `npcs` VALUES (3960, 'Agate', 0002, 9446, 1734, 0041, 0034, 5055500);
INSERT INTO `npcs` VALUES (4470, 'PharmacistDong', 0002, 5547, 1002, 0258, 0277, 0000);
INSERT INTO `npcs` VALUES (2065, 'JewelerLau', 0002, 1430, 1036, 0240, 0256, 5043800);
INSERT INTO `npcs` VALUES (2003, 'ACCaptain', 0002, 1766, 1020, 0566, 0620, NULL);
INSERT INTO `npcs` VALUES (0923, 'LadyLuck', 0002, 9230, 1036, 0212, 0188, 5049999);
INSERT INTO `npcs` VALUES (4293, 'Assistant', 0002, 1176, 1002, 0100, 0362, 0000);
INSERT INTO `npcs` VALUES (4353, 'Armorer', 0001, 9900, 1000, 0493, 0612, 0000);
INSERT INTO `npcs` VALUES (4354, 'Armorer', 0001, 9910, 1000, 0494, 0609, 0000);
INSERT INTO `npcs` VALUES (4355, 'Armorer', 0001, 0046, 1015, 0756, 0562, 0000);
INSERT INTO `npcs` VALUES (4356, 'StoreKeeper', 0001, 0016, 1011, 0168, 0238, 0000);
INSERT INTO `npcs` VALUES (4357, 'StoreKeeper', 0001, 0010, 1020, 0531, 0512, 0000);
INSERT INTO `npcs` VALUES (8796, 'TaskmasterChang', 0002, 12097, 1004, 0052, 0050, 0000);
INSERT INTO `npcs` VALUES (3601, 'Arthur', 0002, 2177, 1700, 0607, 0637, 0000);
INSERT INTO `npcs` VALUES (3600, 'Alex', 0002, 2160, 1020, 0436, 0311, 5031000);
INSERT INTO `npcs` VALUES (3619, 'PhilipCurse', 0002, 2290, 1700, 0411, 0596, 5032252);
INSERT INTO `npcs` VALUES (3834, 'Scarerow', 0002, 9337, 0601, 0076, 0052, 0000);
INSERT INTO `npcs` VALUES (3365, 'GeneralWinner', 0002, 0320, 5000, 0100, 0110, 7104430);
INSERT INTO `npcs` VALUES (3216, 'Mr.Free', 0002, 1320, 1011, 0154, 0238, 0000);
INSERT INTO `npcs` VALUES (3215, 'SolarSaint', 0002, 1310, 5000, 0297, 0146, 0000);
INSERT INTO `npcs` VALUES (0423, 'Pedlar', 0001, 2706, 1002, 0284, 0261, 0000);
INSERT INTO `npcs` VALUES (7000, 'GuildGateKeeper', 0002, 1131, 1038, 0354, 0345, 5030001);
INSERT INTO `npcs` VALUES (4509, 'None', 0002, 5680, 1779, 0200, 0229, 4022750);
INSERT INTO `npcs` VALUES (4510, 'None', 0002, 5690, 1779, 0142, 0212, 4022760);
INSERT INTO `npcs` VALUES (4359, 'StoreKeeper', 0001, 9930, 1000, 0493, 0616, 0000);
INSERT INTO `npcs` VALUES (4511, 'None', 0002, 5700, 1779, 0108, 0228, 4022770);
INSERT INTO `npcs` VALUES (4526, 'None', 0002, 5710, 1780, 0145, 0134, 4022350);
INSERT INTO `npcs` VALUES (5040, 'GodlyArtisan', 0002, 5040, 1036, 0178, 0193, 0000);
INSERT INTO `npcs` VALUES (3836, 'OfflineTGAdmin', 0002, 3016, 0601, 0057, 0054, 1214000);
INSERT INTO `npcs` VALUES (3839, 'Scarerow', 0002, 9337, 0601, 0080, 0047, 0000);
INSERT INTO `npcs` VALUES (3838, 'Scarerow', 0002, 9337, 0601, 0080, 0052, 0000);
INSERT INTO `npcs` VALUES (3835, 'Scarerow', 0002, 9337, 0601, 0076, 0047, 0000);
INSERT INTO `npcs` VALUES (3664, 'CrystalRiddler', 0002, 2480, 1011, 0398, 0029, 7563400);
INSERT INTO `npcs` VALUES (3665, 'StoreKeeper', 0001, 2490, 1011, 0350, 0039, 7563210);
INSERT INTO `npcs` VALUES (1260, 'Guide', 0002, 2617, 5000, 0143, 0085, 8501000);
INSERT INTO `npcs` VALUES (3640, 'Luna', 0002, 8040, 5000, 0039, 0089, 3502800);
INSERT INTO `npcs` VALUES (3931, 'Stake0', 0002, 9327, 0601, 0080, 0063, 0000);
INSERT INTO `npcs` VALUES (3930, 'Stake0', 0002, 9327, 0601, 0080, 0069, 0000);
INSERT INTO `npcs` VALUES (3833, 'Stake0', 0002, 9327, 0601, 0080, 0075, 0000);
INSERT INTO `npcs` VALUES (3832, 'Stake0', 0002, 9327, 0601, 0076, 0063, 0000);
INSERT INTO `npcs` VALUES (3831, 'Stake0', 0002, 9327, 0601, 0076, 0069, 0000);
INSERT INTO `npcs` VALUES (3830, 'Stake0', 0002, 9327, 0601, 0076, 0075, 0000);
INSERT INTO `npcs` VALUES (3637, 'TheodoreCurse', 0002, 2346, 1700, 0985, 0593, 5032257);
INSERT INTO `npcs` VALUES (3636, 'TheodoreCurse', 0002, 2346, 1700, 0923, 0724, 5032257);
INSERT INTO `npcs` VALUES (3635, 'TheodoreCurse', 0002, 2346, 1700, 0956, 0789, 5032257);
INSERT INTO `npcs` VALUES (3634, 'WayneCurse', 0002, 2330, 1700, 0729, 0298, 5032256);
INSERT INTO `npcs` VALUES (3633, 'WayneCurse', 0002, 2336, 1700, 0635, 0340, 5032256);
INSERT INTO `npcs` VALUES (3632, 'VictoriaCurse', 0002, 2327, 1700, 0790, 0979, 5032255);
INSERT INTO `npcs` VALUES (3631, 'VictoriaCurse', 0002, 2327, 1700, 0591, 0974, 5032255);
INSERT INTO `npcs` VALUES (3627, 'TimothyCurse', 0002, 2307, 1700, 0695, 0557, 5032253);
INSERT INTO `npcs` VALUES (3626, 'TimothyCurse', 0002, 2307, 1700, 0731, 0478, 5032253);
INSERT INTO `npcs` VALUES (3623, 'Stanley', 0001, 2350, 1700, 0617, 0639, 0000);
INSERT INTO `npcs` VALUES (3622, 'TimothyCurse', 0002, 2307, 1700, 0660, 0441, 5032253);
INSERT INTO `npcs` VALUES (3621, 'PhilipCurse', 0002, 2290, 0000, 0525, 0687, 0000);
INSERT INTO `npcs` VALUES (3620, 'PhilipCurse', 0002, 2290, 1700, 0447, 0757, 5032252);
INSERT INTO `npcs` VALUES (3040, 'ArcherHerald', 0002, 6940, 1012, 0552, 0504, 1058000);
INSERT INTO `npcs` VALUES (3050, 'TaoistHerald', 0002, 6950, 1012, 0491, 0458, 1056000);
INSERT INTO `npcs` VALUES (3630, 'VictoriaCurse', 0002, 2327, 1700, 0680, 0919, 5032255);
INSERT INTO `npcs` VALUES (3628, 'DaphneCurse', 0002, 2310, 1700, 0651, 0341, 5032254);
INSERT INTO `npcs` VALUES (3629, 'DaphneCurse', 0002, 2310, 1700, 0731, 0313, 5032254);
INSERT INTO `npcs` VALUES (3117, '', 0026, 0061, 1511, 0058, 0037, 0000);
INSERT INTO `npcs` VALUES (3030, 'TrojanHerald', 0002, 6930, 1012, 0492, 0502, 1057000);
INSERT INTO `npcs` VALUES (8924, '', 0026, 0837, 1511, 0051, 0051, 0000);
INSERT INTO `npcs` VALUES (8926, '', 0026, 0857, 1511, 0065, 0051, 0000);
INSERT INTO `npcs` VALUES (9469, 'Pharmacist', 0001, 17170, 3055, 0068, 0096, 0000);
INSERT INTO `npcs` VALUES (8925, '', 0026, 0847, 1511, 0058, 0051, 0000);
INSERT INTO `npcs` VALUES (3124, 'ItemBox', 0002, 8200, 1511, 0058, 0058, 0000);
INSERT INTO `npcs` VALUES (3123, '', 0026, 0121, 1511, 0065, 0044, 0000);
INSERT INTO `npcs` VALUES (1156, 'NorthGeneral', 0002, 6560, 1354, 0900, 0677, 1040140);
INSERT INTO `npcs` VALUES (35511, 'ArenaGuard', 0002, 0260, 3200, 0025, 0014, 1011630);
INSERT INTO `npcs` VALUES (1155, 'SouthGeneral', 0002, 6550, 1353, 0788, 0545, 1040130);
INSERT INTO `npcs` VALUES (1154, 'WestGeneral', 0002, 6540, 1352, 0670, 0468, 1040120);
INSERT INTO `npcs` VALUES (1153, 'EastGeneral', 0002, 6530, 1351, 0488, 0372, 1040100);
INSERT INTO `npcs` VALUES (4000, 'GeneralWinner', 0002, 0320, 1451, 0066, 0145, 1200060);
INSERT INTO `npcs` VALUES (120036, 'GuildController', 0002, 1156, 1002, 0222, 0237, 30700);
INSERT INTO `npcs` VALUES (0127, 'Assistant', 0002, 1176, 1000, 0312, 0008, 5060800);
INSERT INTO `npcs` VALUES (0126, 'Assistant', 0002, 1176, 1020, 0523, 0889, 5060800);
INSERT INTO `npcs` VALUES (0125, 'Assistant', 0002, 1176, 1011, 0934, 0562, 5060800);
INSERT INTO `npcs` VALUES (9360, 'MailManager', 0002, 16510, 1002, 0299, 0141, 0000);
INSERT INTO `npcs` VALUES (600001, 'Minner', 0002, 6010, 1015, 0707, 0545, 600020);
INSERT INTO `npcs` VALUES (600000, 'Milly', 0002, 6000, 1011, 0804, 0461, 600005);
INSERT INTO `npcs` VALUES (10082, 'Warden', 0002, 1160, 6001, 0025, 0070, 31000);
INSERT INTO `npcs` VALUES (0141, 'Pharmacist', 0001, 0032, 1036, 0198, 0181, 0000);
INSERT INTO `npcs` VALUES (30001, 'SwiftMonster', 0002, 7000, 1040, 0585, 0381, 0000);
INSERT INTO `npcs` VALUES (30002, 'SwiftMonster', 0002, 7000, 1040, 0587, 0379, 0000);
INSERT INTO `npcs` VALUES (30003, 'SwiftMonster', 0002, 7000, 1040, 0589, 0377, 0000);
INSERT INTO `npcs` VALUES (30004, 'SwiftMonster', 0002, 7000, 1040, 0591, 0375, 0000);
INSERT INTO `npcs` VALUES (30005, 'SwiftMonster', 0002, 7000, 1040, 0593, 0373, 0000);
INSERT INTO `npcs` VALUES (30006, 'StageGuard', 0002, 7011, 1040, 0467, 0484, 0000);
INSERT INTO `npcs` VALUES (30007, 'StageGuard', 0002, 7011, 1040, 0443, 0508, 0000);
INSERT INTO `npcs` VALUES (30008, 'StageGuard', 0002, 7011, 1040, 0419, 0532, 0000);
INSERT INTO `npcs` VALUES (30009, 'StageGuard', 0002, 7011, 1040, 0395, 0556, 0000);
INSERT INTO `npcs` VALUES (30010, 'StageGuard', 0002, 7011, 1040, 0371, 0580, 0000);
INSERT INTO `npcs` VALUES (30011, 'GreenSnake', 0002, 7020, 1040, 0532, 0326, 0000);
INSERT INTO `npcs` VALUES (30012, 'GreenSnake', 0002, 7020, 1040, 0534, 0324, 0000);
INSERT INTO `npcs` VALUES (30013, 'GreenSnake', 0002, 7020, 1040, 0536, 0322, 0000);
INSERT INTO `npcs` VALUES (30014, 'GreenSnake', 0002, 7020, 1040, 0538, 0320, 0000);
INSERT INTO `npcs` VALUES (30015, 'GreenSnake', 0002, 7020, 1040, 0540, 0318, 0000);
INSERT INTO `npcs` VALUES (30016, 'StageGuard', 0002, 7011, 1040, 0419, 0436, 0000);
INSERT INTO `npcs` VALUES (30017, 'StageGuard', 0002, 7011, 1040, 0395, 0460, 0000);
INSERT INTO `npcs` VALUES (30018, 'StageGuard', 0002, 7011, 1040, 0371, 0484, 0000);
INSERT INTO `npcs` VALUES (30021, 'EvilHawk', 0002, 7030, 1040, 0482, 0278, 0000);
INSERT INTO `npcs` VALUES (30020, 'StageGuard', 0002, 7011, 1040, 0323, 0532, 0000);
INSERT INTO `npcs` VALUES (30019, 'StageGuard', 0002, 7011, 1040, 0347, 0508, 0000);
INSERT INTO `npcs` VALUES (30024, 'EvilHawk', 0002, 7030, 1040, 0488, 0272, 0000);
INSERT INTO `npcs` VALUES (30023, 'EvilHawk', 0002, 7030, 1040, 0486, 0274, 0000);
INSERT INTO `npcs` VALUES (30022, 'EvilHawk', 0002, 7030, 1040, 0484, 0276, 0000);
INSERT INTO `npcs` VALUES (30026, 'StageGuard', 0002, 7011, 1040, 0371, 0388, 0000);
INSERT INTO `npcs` VALUES (30025, 'EvilHawk', 0002, 7030, 1040, 0490, 0270, 0000);
INSERT INTO `npcs` VALUES (30029, 'StageGuard', 0002, 7011, 1040, 0299, 0460, 0000);
INSERT INTO `npcs` VALUES (30028, 'StageGuard', 0002, 7011, 1040, 0323, 0436, 0000);
INSERT INTO `npcs` VALUES (30027, 'StageGuard', 0002, 7011, 1040, 0347, 0412, 0000);
INSERT INTO `npcs` VALUES (30030, 'StageGuard', 0002, 7011, 1040, 0275, 0484, 0000);
INSERT INTO `npcs` VALUES (30031, 'MonsterGeneral', 0002, 7040, 1040, 0426, 0222, 0000);
INSERT INTO `npcs` VALUES (30032, 'MonsterGeneral', 0002, 7040, 1040, 0428, 0220, 0000);
INSERT INTO `npcs` VALUES (30033, 'MonsterGeneral', 0002, 7040, 1040, 0430, 0218, 0000);
INSERT INTO `npcs` VALUES (30034, 'MonsterGeneral', 0002, 7040, 1040, 0432, 0216, 0000);
INSERT INTO `npcs` VALUES (30035, 'MonsterGeneral', 0002, 7040, 1040, 0434, 0214, 0000);
INSERT INTO `npcs` VALUES (30036, 'StageGuard', 0002, 7011, 1040, 0323, 0340, 0000);
INSERT INTO `npcs` VALUES (30037, 'StageGuard', 0002, 7011, 1040, 0299, 0364, 0000);
INSERT INTO `npcs` VALUES (30039, 'StageGuard', 0002, 7011, 1040, 0251, 0412, 0000);
INSERT INTO `npcs` VALUES (30038, 'StageGuard', 0002, 7011, 1040, 0275, 0388, 0000);
INSERT INTO `npcs` VALUES (30043, 'TerminalGuard', 0002, 7050, 1040, 0383, 0179, 0000);
INSERT INTO `npcs` VALUES (30040, 'StageGuard', 0002, 7011, 1040, 0227, 0436, 0000);
INSERT INTO `npcs` VALUES (30044, 'TerminalGuard', 0002, 7050, 1040, 0385, 0177, 0000);
INSERT INTO `npcs` VALUES (30045, 'TerminalGuard', 0002, 7050, 1040, 0387, 0175, 0000);
INSERT INTO `npcs` VALUES (30051, 'TerminalGuard', 0002, 7050, 1040, 0389, 0173, 0000);
INSERT INTO `npcs` VALUES (30047, 'StageGuard', 0002, 7011, 1040, 0251, 0316, 0000);
INSERT INTO `npcs` VALUES (30046, 'StageGuard', 0002, 7011, 1040, 0275, 0292, 0000);
INSERT INTO `npcs` VALUES (30052, 'TerminalGuard', 0002, 7050, 1040, 0391, 0171, 0000);
INSERT INTO `npcs` VALUES (30049, 'StageGuard', 0002, 7011, 1040, 0203, 0364, 0000);
INSERT INTO `npcs` VALUES (30048, 'StageGuard', 0002, 7011, 1040, 0227, 0340, 0000);
INSERT INTO `npcs` VALUES (30055, 'Daniel', 0002, 7097, 1012, 0155, 0165, 0000);
INSERT INTO `npcs` VALUES (30054, 'GodCloud', 0002, 7100, 1040, 0083, 0184, 0000);
INSERT INTO `npcs` VALUES (30050, 'StageGuard', 0002, 7011, 1040, 0179, 0388, 0000);
INSERT INTO `npcs` VALUES (600003, 'Maggie', 0002, 6100, 1042, 0019, 0019, 600400);
INSERT INTO `npcs` VALUES (10061, 'CaptainFang', 0002, 5006, 5000, 0125, 0125, 17600);
INSERT INTO `npcs` VALUES (600005, 'Ghost', 0002, 6127, 1043, 0006, 0066, 600102);
INSERT INTO `npcs` VALUES (3602, 'Bryan', 0002, 2180, 1700, 0609, 0633, 0000);
INSERT INTO `npcs` VALUES (3604, 'Bruce', 0002, 2200, 1700, 0601, 0641, 0000);
INSERT INTO `npcs` VALUES (3603, 'Angela', 0002, 2190, 1700, 0611, 0638, 0000);
INSERT INTO `npcs` VALUES (15693, 'PropShopkeeper', 0032, 24080, 1700, 0614, 0634, 0000);
INSERT INTO `npcs` VALUES (600007, 'Ghost', 0002, 6127, 1044, 0006, 0066, 600122);
INSERT INTO `npcs` VALUES (600009, 'Ghost', 0002, 6127, 1045, 0006, 0066, 600133);
INSERT INTO `npcs` VALUES (600011, 'Ghost', 0002, 6127, 1046, 0006, 0066, 600152);
INSERT INTO `npcs` VALUES (3605, 'EarthSeal', 0002, 2210, 1700, 0444, 0878, 0000);
INSERT INTO `npcs` VALUES (600013, 'Ghost', 0002, 6127, 1047, 0006, 0066, 600162);
INSERT INTO `npcs` VALUES (3609, 'CleansingStove', 0002, 2250, 1700, 0715, 0744, 0000);
INSERT INTO `npcs` VALUES (600015, 'Ghost', 0002, 6127, 1048, 0006, 0066, 600172);
INSERT INTO `npcs` VALUES (3606, 'WaterSeal', 0002, 2220, 1700, 0883, 0447, 0000);
INSERT INTO `npcs` VALUES (3607, 'FireSeal', 0002, 2230, 1700, 0920, 0929, 0000);
INSERT INTO `npcs` VALUES (600018, 'VagrantGhost', 0002, 6132, 1050, 0184, 0165, 600200);
INSERT INTO `npcs` VALUES (600019, 'VagrantGhost', 0002, 6132, 1050, 0160, 0161, 600200);
INSERT INTO `npcs` VALUES (600021, 'VagrantGhost', 0002, 6133, 1050, 0132, 0124, 600200);
INSERT INTO `npcs` VALUES (600020, 'VagrantGhost', 0002, 6131, 1050, 0183, 0187, 600200);
INSERT INTO `npcs` VALUES (600022, 'VagrantGhost', 0002, 6134, 1050, 0100, 0109, 600200);
INSERT INTO `npcs` VALUES (600023, 'VagrantGhost', 0002, 6132, 1050, 0081, 0103, 600200);
INSERT INTO `npcs` VALUES (600025, 'VagrantGhost', 0002, 6135, 1050, 0094, 0073, 600200);
INSERT INTO `npcs` VALUES (600024, 'VagrantGhost', 0002, 6132, 1050, 0134, 0096, 600200);
INSERT INTO `npcs` VALUES (600026, 'VagrantGhost', 0002, 6133, 1050, 0079, 0130, 600200);
INSERT INTO `npcs` VALUES (600027, 'VagrantGhost', 0002, 6132, 1050, 0121, 0178, 600200);
INSERT INTO `npcs` VALUES (600029, 'VagrantGhost', 0002, 6130, 1050, 0156, 0119, 600200);
INSERT INTO `npcs` VALUES (600028, 'VagrantGhost', 0002, 6136, 1050, 0093, 0041, 600200);
INSERT INTO `npcs` VALUES (600032, 'VagrantGhost', 0002, 6133, 1050, 0054, 0096, 600200);
INSERT INTO `npcs` VALUES (600031, 'VagrantGhost', 0002, 6135, 1050, 0082, 0082, 600200);
INSERT INTO `npcs` VALUES (600030, 'VagrantGhost', 0002, 6132, 1050, 0184, 0134, 600200);
INSERT INTO `npcs` VALUES (600033, 'VagrantGhost', 0002, 6137, 1050, 0175, 0123, 600200);
INSERT INTO `npcs` VALUES (600034, 'VagrantGhost', 0002, 6131, 1050, 0152, 0111, 600200);
INSERT INTO `npcs` VALUES (600036, 'VagrantGhost', 0002, 6133, 1050, 0122, 0139, 600200);
INSERT INTO `npcs` VALUES (600035, 'VagrantGhost', 0002, 6132, 1050, 0125, 0100, 600200);
INSERT INTO `npcs` VALUES (600038, 'VagrantGhost', 0002, 6136, 1050, 0138, 0123, 600200);
INSERT INTO `npcs` VALUES (600037, 'VagrantGhost', 0002, 6135, 1050, 0139, 0156, 600200);
INSERT INTO `npcs` VALUES (600039, 'VagrantGhost', 0002, 6137, 1050, 0050, 0040, 600200);
INSERT INTO `npcs` VALUES (600041, 'VagrantGhost', 0002, 6135, 1050, 0087, 0048, 600200);
INSERT INTO `npcs` VALUES (600040, 'VagrantGhost', 0002, 6133, 1050, 0059, 0028, 600200);
INSERT INTO `npcs` VALUES (4702, 'MasterMoMo', 0002, 4750, 1000, 0520, 0589, 0000);
INSERT INTO `npcs` VALUES (600042, 'VagrantGhost', 0002, 6132, 1050, 0114, 0067, 600200);
INSERT INTO `npcs` VALUES (10850, 'Mr.Wonder', 0002, 36470, 1011, 0211, 0196, 0000);
INSERT INTO `npcs` VALUES (16856, 'SharkSoldier', 0032, 30780, 3856, 0212, 0094, 0000);
INSERT INTO `npcs` VALUES (30073, 'Protectingnd', 0002, 7140, 1062, 0510, 0324, 7001400);
INSERT INTO `npcs` VALUES (30072, 'ProtectingKid', 0002, 7140, 1061, 0615, 0363, 7001350);
INSERT INTO `npcs` VALUES (30071, 'ProtectingKid', 0002, 7140, 1061, 0542, 0291, 7001350);
INSERT INTO `npcs` VALUES (30070, 'ProtectingKid', 0002, 7140, 1061, 0582, 0395, 7001350);
INSERT INTO `npcs` VALUES (30092, 'ProtectingKid', 0002, 7140, 1061, 0512, 0322, 7001350);
INSERT INTO `npcs` VALUES (30069, 'ProtectingKid', 0002, 7140, 1061, 0510, 0324, 7001200);
INSERT INTO `npcs` VALUES (30068, 'ProtectingKid', 0002, 7140, 1060, 0615, 0363, 7001050);
INSERT INTO `npcs` VALUES (30067, 'ProtectingKid', 0002, 7140, 1060, 0542, 0291, 7001050);
INSERT INTO `npcs` VALUES (30066, 'ProtectingKid', 0002, 7140, 1060, 0582, 0395, 7001050);
INSERT INTO `npcs` VALUES (30091, 'ProtectingKid', 0002, 7140, 1060, 0512, 0322, 7001050);
INSERT INTO `npcs` VALUES (30065, 'ProtectingKid', 0002, 7140, 1060, 0510, 0324, 7000900);
INSERT INTO `npcs` VALUES (30064, 'GuardingKid', 0002, 7130, 1041, 0616, 0361, 403006);
INSERT INTO `npcs` VALUES (30063, 'GuardingKid', 0002, 7130, 1041, 0544, 0289, 403006);
INSERT INTO `npcs` VALUES (30062, 'GuardingKid', 0002, 7130, 1041, 0584, 0393, 403006);
INSERT INTO `npcs` VALUES (30090, 'GuardingKid', 0002, 7130, 1041, 0512, 0322, 7001110);
INSERT INTO `npcs` VALUES (30061, 'GuardingKid', 0002, 7130, 1041, 0510, 0324, 7001080);
INSERT INTO `npcs` VALUES (4525, 'None', 0002, 5770, 1780, 0164, 0215, 0000);
INSERT INTO `npcs` VALUES (30085, 'FoodCarrier', 0002, 7200, 1002, 4294967295, 0301, 403064);
INSERT INTO `npcs` VALUES (30102, 'VillageHead', 0002, 7300, 1000, 0479, 0261, 402200);
INSERT INTO `npcs` VALUES (16853, 'SharkSoldier', 0032, 30786, 3856, 0156, 0267, 0000);
INSERT INTO `npcs` VALUES (30093, 'ProtectingKid', 0002, 7140, 1062, 0512, 0322, 7001550);
INSERT INTO `npcs` VALUES (30074, 'ProtectingKid', 0002, 7140, 1062, 0582, 0395, 7001550);
INSERT INTO `npcs` VALUES (30077, 'KindTaoist', 0002, 7110, 1041, 0301, 0151, 403019);
INSERT INTO `npcs` VALUES (30076, 'ProtectingKid', 0002, 7140, 1062, 0615, 0363, 7001550);
INSERT INTO `npcs` VALUES (30075, 'ProtectingKid', 0002, 7140, 1062, 0542, 0291, 7001550);
INSERT INTO `npcs` VALUES (30100, 'MountainKing', 0002, 7280, 1020, 0543, 0544, 405535);
INSERT INTO `npcs` VALUES (30101, 'SnakemanLeader', 0002, 7290, 1020, 0088, 0387, 405530);
INSERT INTO `npcs` VALUES (5658, 'SkullReward', 0002, 9760, 1766, 0072, 0041, 0000);
INSERT INTO `npcs` VALUES (30099, 'GeneralLong', 0002, 7271, 5000, 0443, 0242, 405510);
INSERT INTO `npcs` VALUES (16852, 'SharkSoldier', 0032, 30784, 3856, 0282, 0352, 0000);
INSERT INTO `npcs` VALUES (30083, 'KillerMaple', 0002, 7187, 1011, 0774, 0414, 403054);
INSERT INTO `npcs` VALUES (600062, 'GossiperWang', 0002, 6257, 1015, 0743, 0515, 600726);
INSERT INTO `npcs` VALUES (600066, 'ToughWei', 0002, 6287, 1000, 0066, 0354, 600810);
INSERT INTO `npcs` VALUES (16854, 'SharkSoldier', 0032, 30786, 3856, 0100, 0175, 0000);
INSERT INTO `npcs` VALUES (600060, 'Mr.Pine', 0002, 6232, 1000, 0454, 0242, 600707);
INSERT INTO `npcs` VALUES (600061, 'RichmanZhang', 0002, 6247, 1000, 0520, 0620, 600710);
INSERT INTO `npcs` VALUES (30105, 'HeresyLeader', 0002, 7342, 1020, 0441, 0451, 405570);
INSERT INTO `npcs` VALUES (600075, 'BoxerHuang', 0002, 6440, 1039, 0222, 0220, 0000);
INSERT INTO `npcs` VALUES (30104, 'GuruTodd', 0002, 7310, 1000, 0519, 0615, 402250);
INSERT INTO `npcs` VALUES (30103, 'StoneBandit', 0002, 7320, 1000, 0324, 0456, 402260);
INSERT INTO `npcs` VALUES (10663, 'SwapperStarry', 0002, 31256, 3849, 0027, 0040, 0000);
INSERT INTO `npcs` VALUES (10063, 'ShopBoy', 0002, 5020, 1002, 0296, 0250, 18200);
INSERT INTO `npcs` VALUES (10064, 'Tinter', 0002, 5030, 1008, 0027, 0016, 18202);
INSERT INTO `npcs` VALUES (30114, 'GeneralWu', 0002, 7400, 1011, 0800, 0471, 330161);
INSERT INTO `npcs` VALUES (16855, 'SharkSoldier', 0032, 30780, 3856, 0121, 0096, 0000);
INSERT INTO `npcs` VALUES (0390, 'LoveStone', 0002, 1600, 1036, 0192, 0193, 5042500);
INSERT INTO `npcs` VALUES (30110, 'Mr.Leisure', 0002, 7360, 1002, 0264, 4294967295, 330000);
INSERT INTO `npcs` VALUES (30115, 'GreenSnake', 0002, 7410, 1015, 0468, 0203, 0000);
INSERT INTO `npcs` VALUES (30125, 'GreenSnake', 0002, 7410, 1063, 0087, 0044, 330222);
INSERT INTO `npcs` VALUES (30121, 'Hades', 0002, 7510, 1020, 0381, 0033, 406070);
INSERT INTO `npcs` VALUES (30120, 'DivineArtisan', 0002, 7500, 1012, 0403, 0362, 406000);
INSERT INTO `npcs` VALUES (30123, 'CellGuard', 0002, 7530, 1061, 0547, 0290, 406170);
INSERT INTO `npcs` VALUES (30127, 'CellGuard', 0002, 7530, 1061, 0303, 0142, 406296);
INSERT INTO `npcs` VALUES (30124, 'Lauren', 0002, 7540, 1061, 0266, 0126, 406200);
INSERT INTO `npcs` VALUES (20001, 'SoldierLeader', 0002, 8210, 1020, 0448, 0396, 400880);
INSERT INTO `npcs` VALUES (0400, 'ArcherGod', 0002, 50017, 1004, 0033, 0053, 4024570);
INSERT INTO `npcs` VALUES (9999, 'DiceKing', 0019, 1696, 1036, 0187, 0179, 0000);
INSERT INTO `npcs` VALUES (300015, 'ArcherGuard', 0002, 7740, 1052, 0243, 0219, 511050);
INSERT INTO `npcs` VALUES (0420, 'Warden', 0002, 0380, 6010, 0025, 0068, 21000);
INSERT INTO `npcs` VALUES (20000, 'Shirley', 0002, 8190, 1011, 0783, 0456, 400810);
INSERT INTO `npcs` VALUES (300013, 'FireGuard', 0002, 7720, 1052, 0181, 0176, 511030);
INSERT INTO `npcs` VALUES (300014, 'WaterGuard', 0002, 7730, 1052, 0206, 0179, 511040);
INSERT INTO `npcs` VALUES (300010, 'Exorcist', 0002, 7680, 1015, 0715, 0584, 0000);
INSERT INTO `npcs` VALUES (300011, 'WarriorGuard', 0002, 7690, 1052, 0229, 0233, 511010);
INSERT INTO `npcs` VALUES (300012, 'TrojanGuard', 0002, 7710, 1052, 0191, 0201, 511020);
INSERT INTO `npcs` VALUES (30164, 'Mike', 0002, 8290, 1000, 0486, 0618, 408200);
INSERT INTO `npcs` VALUES (30163, 'Joy', 0002, 8280, 1011, 0324, 0187, 408150);
INSERT INTO `npcs` VALUES (300653, 'Grandpa', 0002, 9120, 1212, 0381, 0032, 300806);
INSERT INTO `npcs` VALUES (300652, 'MineSupervisor', 0002, 9110, 1210, 0717, 1036, 5060800);
INSERT INTO `npcs` VALUES (300651, 'Explorer', 0002, 9100, 1077, 0360, 0008, 300802);
INSERT INTO `npcs` VALUES (600050, 'Fortuneteller', 0002, 6207, 1002, 0412, 0512, 0000);
INSERT INTO `npcs` VALUES (300654, 'BoatMan', 0002, 9130, 1219, 0381, 0032, 300810);
INSERT INTO `npcs` VALUES (35500, 'MightyTao', 0002, 8452, 1004, 0069, 0038, 710000);
INSERT INTO `npcs` VALUES (4101, 'WarehouseMan', 0003, 0086, 1213, 0425, 0269, 8003000);
INSERT INTO `npcs` VALUES (4100, 'Pharmacist', 0001, 0030, 1213, 0468, 0251, 0000);
INSERT INTO `npcs` VALUES (0422, 'OldQuarrier', 0002, 9220, 1020, 0568, 0527, 5001280);
INSERT INTO `npcs` VALUES (4503, 'None', 0002, 5620, 1002, 0233, 0359, 4022000);
INSERT INTO `npcs` VALUES (600100, 'Alcoholist', 0002, 9200, 1001, 0674, 0340, 0000);
INSERT INTO `npcs` VALUES (300500, 'Eternity', 0002, 8364, 1036, 0298, 0217, 300500);
INSERT INTO `npcs` VALUES (2070, 'CollectorZhao', 0002, 3220, 1036, 0212, 0184, 0000);
INSERT INTO `npcs` VALUES (20005, 'Celestine', 0002, 8350, 1002, 0237, 0222, 0000);
INSERT INTO `npcs` VALUES (20090, 'ArenaGuard', 0002, 0260, 5000, 0032, 0078, 407120);
INSERT INTO `npcs` VALUES (20060, 'ArenaGuard', 0002, 0260, 5000, 0030, 0078, 407090);
INSERT INTO `npcs` VALUES (350050, 'CelestialTao', 0002, 8460, 1036, 0291, 0200, 743000);
INSERT INTO `npcs` VALUES (30165, 'EscortChief', 0002, 8400, 5000, 0423, 0325, 426500);
INSERT INTO `npcs` VALUES (20120, 'ArenaGuard', 0002, 0260, 5000, 0034, 0078, 407060);
INSERT INTO `npcs` VALUES (30168, 'Yougo', 0002, 8430, 1077, 0547, 0193, 426750);
INSERT INTO `npcs` VALUES (30167, 'Roy', 0002, 8420, 1011, 0182, 0214, 426650);
INSERT INTO `npcs` VALUES (30166, 'ShopBoy', 0002, 8410, 1000, 0469, 0273, 426600);
INSERT INTO `npcs` VALUES (30129, 'DocterMo', 0002, 7760, 1020, 0705, 0507, 404150);
INSERT INTO `npcs` VALUES (30128, 'CaptainLu', 0002, 7751, 1002, 0524, 0574, 404000);
INSERT INTO `npcs` VALUES (30161, 'FurnitureStore', 0002, 8120, 1002, 0281, 0270, 0000);
INSERT INTO `npcs` VALUES (8917, '', 0026, 0767, 1511, 0037, 0058, 0000);
INSERT INTO `npcs` VALUES (30130, 'CaptainChief', 0002, 7770, 5000, 0416, 0242, 404200);
INSERT INTO `npcs` VALUES (8918, '', 0026, 0777, 1511, 0044, 0058, 0000);
INSERT INTO `npcs` VALUES (8920, '', 0026, 0797, 1511, 0068, 0037, 0000);
INSERT INTO `npcs` VALUES (8919, '', 0026, 0787, 1511, 0037, 0051, 0000);
INSERT INTO `npcs` VALUES (0421, 'Norbert', 0002, 9210, 1011, 0810, 0458, 5001200);
INSERT INTO `npcs` VALUES (1610, 'OldMiner', 0002, 6980, 1011, 0907, 0546, 7100500);
INSERT INTO `npcs` VALUES (8923, '', 0026, 0827, 1511, 0044, 0065, 0000);
INSERT INTO `npcs` VALUES (8922, '', 0026, 0817, 1511, 0065, 0058, 0000);
INSERT INTO `npcs` VALUES (3020, 'WarriorHerald', 0002, 6920, 1012, 0532, 0472, 1055300);
INSERT INTO `npcs` VALUES (3010, 'AntiqueDealer', 0002, 6910, 1015, 0761, 0588, 1050700);
INSERT INTO `npcs` VALUES (3000, 'DoctorHolt', 0002, 6900, 1077, 0474, 0333, 1050500);
INSERT INTO `npcs` VALUES (30000, 'Matchmaker', 0002, 6030, 1000, 0462, 0589, 0000);
INSERT INTO `npcs` VALUES (10060, 'Sunfer', 0002, 1070, 5000, 0081, 0081, 17000);
INSERT INTO `npcs` VALUES (30088, 'OldmanTang', 0002, 7230, 1011, 0779, 0467, 403073);
INSERT INTO `npcs` VALUES (0044, 'WarehouseMan', 0003, 0087, 1036, 0182, 0180, 8003000);
INSERT INTO `npcs` VALUES (0045, 'Mark.Controller', 0029, 0417, 1036, 0215, 0220, 16100);
INSERT INTO `npcs` VALUES (0043, 'CaptainLi', 0002, 0394, 1002, 0385, 0259, 111111);
INSERT INTO `npcs` VALUES (0042, 'Warden', 0002, 0380, 6000, 0035, 0069, 5002050);
INSERT INTO `npcs` VALUES (0040, 'Alchemist', 0002, 0360, 1011, 0178, 0224, 600820);
INSERT INTO `npcs` VALUES (3825, 'UnknownMan', 0002, 3196, 1036, 0212, 0204, 0000);
INSERT INTO `npcs` VALUES (0032, 'GeneralWinner', 0002, 0320, 5000, 0032, 0078, 60050);
INSERT INTO `npcs` VALUES (10056, 'Condustress', 0029, 0280, 1015, 0789, 0566, 10357);
INSERT INTO `npcs` VALUES (10054, 'GeneralPeace', 0002, 0296, 1002, 4294967295, 0363, 10391);
INSERT INTO `npcs` VALUES (10080, 'Conductress', 0029, 0286, 1002, 0307, 0340, 10351);
INSERT INTO `npcs` VALUES (10051, 'Conductress', 0029, 0280, 1000, 0478, 0631, 10361);
INSERT INTO `npcs` VALUES (10052, 'Conductress', 0029, 0287, 1011, 0228, 0255, 10371);
INSERT INTO `npcs` VALUES (10053, 'Conductress', 0029, 0286, 1020, 0566, 0622, 10381);
INSERT INTO `npcs` VALUES (10043, 'SpaceMark', 0002, 0270, 1001, 0472, 0366, 10335);
INSERT INTO `npcs` VALUES (10041, 'SpaceMark', 0002, 0270, 1000, 0793, 0549, 10331);
INSERT INTO `npcs` VALUES (10040, 'SpaceMark', 0002, 0270, 1000, 0225, 0205, 10329);
INSERT INTO `npcs` VALUES (10039, 'SpaceMark', 0002, 0270, 1020, 0106, 0394, 10327);
INSERT INTO `npcs` VALUES (10038, 'SpaceMark', 0002, 0270, 1020, 0491, 0731, 10325);
INSERT INTO `npcs` VALUES (10037, 'SpaceMark', 0002, 0270, 1020, 0824, 0601, 10323);
INSERT INTO `npcs` VALUES (10036, 'SpaceMark', 0002, 0270, 1011, 0067, 0423, 10321);
INSERT INTO `npcs` VALUES (10035, 'SpaceMark', 0002, 0270, 1011, 0734, 0452, 10319);
INSERT INTO `npcs` VALUES (10034, 'SpaceMark', 0002, 0270, 1011, 0538, 0772, 10317);
INSERT INTO `npcs` VALUES (10033, 'SpaceMark', 0002, 0270, 1002, 0667, 0365, 10315);
INSERT INTO `npcs` VALUES (10032, 'SpaceMark', 0002, 0270, 1002, 4294967295, 0223, 10313);
INSERT INTO `npcs` VALUES (10031, 'SpaceMark', 0002, 0270, 1002, 0283, 0604, 10311);
INSERT INTO `npcs` VALUES (10030, 'Pharmacist', 0001, 0030, 1020, 0550, 0542, 0000);
INSERT INTO `npcs` VALUES (10029, 'Blacksmith', 0001, 0050, 1020, 0560, 0508, 0000);
INSERT INTO `npcs` VALUES (10028, 'WarehouseMan', 0003, 0080, 1020, 0576, 0542, 8003000);
INSERT INTO `npcs` VALUES (10027, 'WarehouseMan', 0003, 0080, 1015, 0716, 0541, 8003000);
INSERT INTO `npcs` VALUES (10026, 'Pharmacist', 0001, 0030, 1015, 0765, 0542, 0000);
INSERT INTO `npcs` VALUES (10025, 'Blacksmith', 0001, 0050, 1015, 0751, 0544, 0000);
INSERT INTO `npcs` VALUES (10023, 'StoreKeeper', 0001, 0010, 1015, 0721, 0541, 0000);
INSERT INTO `npcs` VALUES (10015, 'Blacksmith', 0001, 9880, 1000, 0486, 0623, 0000);
INSERT INTO `npcs` VALUES (10021, 'AreaGuard', 0002, 0260, 1002, 0331, 0191, 10280);
INSERT INTO `npcs` VALUES (10020, 'Beautician', 0005, 0250, 1002, 0331, 0231, 0000);
INSERT INTO `npcs` VALUES (4352, 'Armorer', 0001, 0040, 1020, 0557, 0559, 0000);
INSERT INTO `npcs` VALUES (10017, 'Pharmacist', 0001, 0230, 1013, 0046, 0022, 0000);
INSERT INTO `npcs` VALUES (10016, 'Pharmacist', 0001, 0236, 1000, 0519, 0259, 0000);
INSERT INTO `npcs` VALUES (10022, 'TrojanStar', 0002, 50040, 1004, 0044, 0033, 4024550);
INSERT INTO `npcs` VALUES (10014, 'Pharmacist', 0001, 0230, 1011, 0189, 0252, 0000);
INSERT INTO `npcs` VALUES (10013, 'Blacksmith', 0001, 0220, 1011, 0197, 0226, 0000);
INSERT INTO `npcs` VALUES (10012, 'WarehouseMan', 0003, 0210, 1011, 0227, 0246, 8003000);
INSERT INTO `npcs` VALUES (10011, 'WarehouseMan', 0003, 0200, 1000, 0471, 0629, 8003000);
INSERT INTO `npcs` VALUES (10004, 'VillageWiseGuy', 0002, 0170, 5000, 0045, 0050, 10140);
INSERT INTO `npcs` VALUES (10005, 'Blacksmith', 0002, 0056, 5000, 0045, 0045, 10190);
INSERT INTO `npcs` VALUES (600004, 'GuardianGod', 0002, 6111, 1043, 0204, 0150, 0000);
INSERT INTO `npcs` VALUES (10007, 'Armorer', 0002, 0046, 5000, 0076, 0055, 10210);
INSERT INTO `npcs` VALUES (19194, 'Conductress', 0002, 40337, 3998, 0082, 0349, 0000);
INSERT INTO `npcs` VALUES (10009, 'VillageIdiot', 0002, 0180, 5000, 0085, 0041, 10230);
INSERT INTO `npcs` VALUES (46554, 'RefinedPureOven', 0002, 35930, 10332, 0318, 0445, NULL);
INSERT INTO `npcs` VALUES (10000, 'TaoistMoon', 0002, 50050, 1004, 0056, 0033, 4024580);
INSERT INTO `npcs` VALUES (10001, 'WarriorGod', 0002, 50006, 1004, 0033, 0065, 4024560);
INSERT INTO `npcs` VALUES (10003, 'GuildDirector', 0002, 0150, 1002, 0270, 0292, 10030);
INSERT INTO `npcs` VALUES (0011, 'Armorer', 0001, 0116, 1011, 0202, 0242, 0000);
INSERT INTO `npcs` VALUES (0010, 'Blacksmith', 0001, 9890, 1000, 0486, 0621, 0000);
INSERT INTO `npcs` VALUES (10002, 'Barber', 0002, 0026, 1002, 0284, 0276, 8003980);
INSERT INTO `npcs` VALUES (0008, 'WarehouseMan', 0003, 0080, 1002, 0281, 0251, 8003000);
INSERT INTO `npcs` VALUES (0003, 'Pharmacist', 0001, 0030, 1002, 0338, 0227, 0000);
INSERT INTO `npcs` VALUES (0001, 'StoreKeeper', 0001, 0010, 1002, 0287, 0251, 0000);
INSERT INTO `npcs` VALUES (0005, 'Blacksmith', 0001, 0050, 1002, 0324, 0230, 0000);
INSERT INTO `npcs` VALUES (0004, 'Armorer', 0001, 0040, 1002, 0284, 0266, 0000);
INSERT INTO `npcs` VALUES (0009, 'Pharmacist', 0001, 0030, 1000, 0511, 0630, 0000);
INSERT INTO `npcs` VALUES (0121, 'ShopFlag', 0016, 1086, 1036, 0217, 0243, 0000);
INSERT INTO `npcs` VALUES (0120, 'ShopFlag', 0016, 1086, 1036, 0217, 0248, 0000);
INSERT INTO `npcs` VALUES (0119, 'ShopFlag', 0016, 1086, 1036, 0217, 0253, 0000);
INSERT INTO `npcs` VALUES (0118, 'ShopFlag', 0016, 1086, 1036, 0217, 0258, 0000);
INSERT INTO `npcs` VALUES (0117, 'ShopFlag', 0016, 1086, 1036, 0217, 0263, 0000);
INSERT INTO `npcs` VALUES (0116, 'ShopFlag', 0016, 1086, 1036, 0217, 0268, 0000);
INSERT INTO `npcs` VALUES (0115, 'ShopFlag', 0016, 1086, 1036, 0210, 0243, 0000);
INSERT INTO `npcs` VALUES (0114, 'ShopFlag', 0016, 1086, 1036, 0210, 0248, 0000);
INSERT INTO `npcs` VALUES (0113, 'ShopFlag', 0016, 1086, 1036, 0210, 0253, 0000);
INSERT INTO `npcs` VALUES (0112, 'ShopFlag', 0016, 1086, 1036, 0210, 0258, 0000);
INSERT INTO `npcs` VALUES (0111, 'ShopFlag', 0016, 1086, 1036, 0210, 0263, 0000);
INSERT INTO `npcs` VALUES (0110, 'ShopFlag', 0016, 1086, 1036, 0210, 0268, 0000);
INSERT INTO `npcs` VALUES (0109, 'ShopFlag', 0016, 1086, 1036, 0185, 0243, 0000);
INSERT INTO `npcs` VALUES (0108, 'ShopFlag', 0016, 1086, 1036, 0185, 0248, 0000);
INSERT INTO `npcs` VALUES (0107, 'ShopFlag', 0016, 1086, 1036, 0185, 0253, 0000);
INSERT INTO `npcs` VALUES (0106, 'ShopFlag', 0016, 1086, 1036, 0185, 0258, 0000);
INSERT INTO `npcs` VALUES (0105, 'ShopFlag', 0016, 1086, 1036, 0185, 0263, 0000);
INSERT INTO `npcs` VALUES (0104, 'ShopFlag', 0016, 1086, 1036, 0185, 0268, 0000);
INSERT INTO `npcs` VALUES (0103, 'ShopFlag', 0016, 1086, 1036, 0178, 0243, 0000);
INSERT INTO `npcs` VALUES (0102, 'ShopFlag', 0016, 1086, 1036, 0178, 0248, 0000);
INSERT INTO `npcs` VALUES (0101, 'ShopFlag', 0016, 1086, 1036, 0178, 0253, 0000);
INSERT INTO `npcs` VALUES (0100, 'ShopFlag', 0016, 1086, 1036, 0178, 0258, 0000);
INSERT INTO `npcs` VALUES (0099, 'ShopFlag', 0016, 1086, 1036, 0178, 0263, 0000);
INSERT INTO `npcs` VALUES (0098, 'ShopFlag', 0016, 1086, 1036, 0178, 0268, 0000);
INSERT INTO `npcs` VALUES (0097, 'ShopFlag', 0016, 1086, 1036, 0157, 0173, 0000);
INSERT INTO `npcs` VALUES (0096, 'ShopFlag', 0016, 1086, 1036, 0157, 0177, 0000);
INSERT INTO `npcs` VALUES (0095, 'ShopFlag', 0016, 1086, 1036, 0157, 0181, 0000);
INSERT INTO `npcs` VALUES (0094, 'ShopFlag', 0016, 1086, 1036, 0157, 0185, 0000);
INSERT INTO `npcs` VALUES (0093, 'ShopFlag', 0016, 1086, 1036, 0157, 0189, 0000);
INSERT INTO `npcs` VALUES (0092, 'ShopFlag', 0016, 1086, 1036, 0157, 0193, 0000);
INSERT INTO `npcs` VALUES (0091, 'ShopFlag', 0016, 1086, 1036, 0157, 0197, 0000);
INSERT INTO `npcs` VALUES (0090, 'ShopFlag', 0016, 1086, 1036, 0157, 0201, 0000);
INSERT INTO `npcs` VALUES (0089, 'ShopFlag', 0016, 1086, 1036, 0157, 0205, 0000);
INSERT INTO `npcs` VALUES (0088, 'ShopFlag', 0016, 1086, 1036, 0157, 0209, 0000);
INSERT INTO `npcs` VALUES (0087, 'ShopFlag', 0016, 1086, 1036, 0157, 0213, 0000);
INSERT INTO `npcs` VALUES (0086, 'ShopFlag', 0016, 1086, 1036, 0157, 0217, 0000);
INSERT INTO `npcs` VALUES (0085, 'ShopFlag', 0016, 1086, 1036, 0150, 0173, 0000);
INSERT INTO `npcs` VALUES (0084, 'ShopFlag', 0016, 1086, 1036, 0150, 0177, 0000);
INSERT INTO `npcs` VALUES (0083, 'ShopFlag', 0016, 1086, 1036, 0150, 0181, 0000);
INSERT INTO `npcs` VALUES (0082, 'ShopFlag', 0016, 1086, 1036, 0150, 0185, 0000);
INSERT INTO `npcs` VALUES (0081, 'ShopFlag', 0016, 1086, 1036, 0150, 0189, 0000);
INSERT INTO `npcs` VALUES (0080, 'ShopFlag', 0016, 1086, 1036, 0150, 0193, 0000);
INSERT INTO `npcs` VALUES (0079, 'ShopFlag', 0016, 1086, 1036, 0150, 0197, 0000);
INSERT INTO `npcs` VALUES (0078, 'ShopFlag', 0016, 1086, 1036, 0150, 0201, 0000);
INSERT INTO `npcs` VALUES (0077, 'ShopFlag', 0016, 1086, 1036, 0150, 0205, 0000);
INSERT INTO `npcs` VALUES (0076, 'ShopFlag', 0016, 1086, 1036, 0150, 0209, 0000);
INSERT INTO `npcs` VALUES (0075, 'ShopFlag', 0016, 1086, 1036, 0150, 0213, 0000);
INSERT INTO `npcs` VALUES (0074, 'ShopFlag', 0016, 1086, 1036, 0150, 0217, 0000);
INSERT INTO `npcs` VALUES (0073, 'ShopFlag', 0016, 1086, 1036, 0125, 0173, 0000);
INSERT INTO `npcs` VALUES (0072, 'ShopFlag', 0016, 1086, 1036, 0125, 0177, 0000);
INSERT INTO `npcs` VALUES (0071, 'ShopFlag', 0016, 1086, 1036, 0125, 0181, 0000);
INSERT INTO `npcs` VALUES (0070, 'ShopFlag', 0016, 1086, 1036, 0125, 0185, 0000);
INSERT INTO `npcs` VALUES (0069, 'ShopFlag', 0016, 1086, 1036, 0125, 0189, 0000);
INSERT INTO `npcs` VALUES (0068, 'ShopFlag', 0016, 1086, 1036, 0125, 0193, 0000);
INSERT INTO `npcs` VALUES (0067, 'ShopFlag', 0016, 1086, 1036, 0125, 0197, 0000);
INSERT INTO `npcs` VALUES (0066, 'ShopFlag', 0016, 1086, 1036, 0125, 0201, 0000);
INSERT INTO `npcs` VALUES (0065, 'ShopFlag', 0016, 1086, 1036, 0125, 0205, 0000);
INSERT INTO `npcs` VALUES (0064, 'ShopFlag', 0016, 1086, 1036, 0125, 0209, 0000);
INSERT INTO `npcs` VALUES (0063, 'ShopFlag', 0016, 1086, 1036, 0125, 0213, 0000);
INSERT INTO `npcs` VALUES (0062, 'ShopFlag', 0016, 1086, 1036, 0125, 0217, 0000);
INSERT INTO `npcs` VALUES (0061, 'ShopFlag', 0016, 1086, 1036, 0118, 0173, 0000);
INSERT INTO `npcs` VALUES (0060, 'ShopFlag', 0016, 1086, 1036, 0118, 0177, 0000);
INSERT INTO `npcs` VALUES (0059, 'ShopFlag', 0016, 1086, 1036, 0118, 0181, 0000);
INSERT INTO `npcs` VALUES (0058, 'ShopFlag', 0016, 1086, 1036, 0118, 0185, 0000);
INSERT INTO `npcs` VALUES (0057, 'ShopFlag', 0016, 1086, 1036, 0118, 0189, 0000);
INSERT INTO `npcs` VALUES (0056, 'ShopFlag', 0016, 1086, 1036, 0118, 0193, 0000);
INSERT INTO `npcs` VALUES (0055, 'ShopFlag', 0016, 1086, 1036, 0118, 0197, 0000);
INSERT INTO `npcs` VALUES (0054, 'ShopFlag', 0016, 1086, 1036, 0118, 0201, 0000);
INSERT INTO `npcs` VALUES (0053, 'ShopFlag', 0016, 1086, 1036, 0118, 0205, 0000);
INSERT INTO `npcs` VALUES (0052, 'ShopFlag', 0016, 1086, 1036, 0118, 0209, 0000);
INSERT INTO `npcs` VALUES (0051, 'ShopFlag', 0016, 1086, 1036, 0118, 0213, 0000);
INSERT INTO `npcs` VALUES (0050, 'ShopFlag', 0016, 1086, 1036, 0118, 0217, 0000);
INSERT INTO `npcs` VALUES (0122, 'ShopFlag', 0016, 1086, 1036, 0269, 0174, 0000);
INSERT INTO `npcs` VALUES (0197, 'ShopFlag', 0016, 1086, 1036, 0269, 0178, 0000);
INSERT INTO `npcs` VALUES (0196, 'ShopFlag', 0016, 1086, 1036, 0269, 0182, 0000);
INSERT INTO `npcs` VALUES (0194, 'ShopFlag', 0016, 1086, 1036, 0269, 0186, 0000);
INSERT INTO `npcs` VALUES (0193, 'ShopFlag', 0016, 1086, 1036, 0269, 0190, 0000);
INSERT INTO `npcs` VALUES (0192, 'ShopFlag', 0016, 1086, 1036, 0269, 0194, 0000);
INSERT INTO `npcs` VALUES (0191, 'ShopFlag', 0016, 1086, 1036, 0269, 0198, 0000);
INSERT INTO `npcs` VALUES (0190, 'ShopFlag', 0016, 1086, 1036, 0269, 0202, 0000);
INSERT INTO `npcs` VALUES (0189, 'ShopFlag', 0016, 1086, 1036, 0269, 0206, 0000);
INSERT INTO `npcs` VALUES (0188, 'ShopFlag', 0016, 1086, 1036, 0269, 0210, 0000);
INSERT INTO `npcs` VALUES (0187, 'ShopFlag', 0016, 1086, 1036, 0269, 0214, 0000);
INSERT INTO `npcs` VALUES (0186, 'ShopFlag', 0016, 1086, 1036, 0269, 0218, 0000);
INSERT INTO `npcs` VALUES (0221, 'ShopFlag', 0016, 1086, 1036, 0262, 0174, 0000);
INSERT INTO `npcs` VALUES (0220, 'ShopFlag', 0016, 1086, 1036, 0262, 0178, 0000);
INSERT INTO `npcs` VALUES (0219, 'ShopFlag', 0016, 1086, 1036, 0262, 0182, 0000);
INSERT INTO `npcs` VALUES (0218, 'ShopFlag', 0016, 1086, 1036, 0262, 0186, 0000);
INSERT INTO `npcs` VALUES (0217, 'ShopFlag', 0016, 1086, 1036, 0262, 0190, 0000);
INSERT INTO `npcs` VALUES (0216, 'ShopFlag', 0016, 1086, 1036, 0262, 0194, 0000);
INSERT INTO `npcs` VALUES (0215, 'ShopFlag', 0016, 1086, 1036, 0262, 0198, 0000);
INSERT INTO `npcs` VALUES (0214, 'ShopFlag', 0016, 1086, 1036, 0262, 0202, 0000);
INSERT INTO `npcs` VALUES (0213, 'ShopFlag', 0016, 1086, 1036, 0262, 0206, 0000);
INSERT INTO `npcs` VALUES (0212, 'ShopFlag', 0016, 1086, 1036, 0262, 0210, 0000);
INSERT INTO `npcs` VALUES (0211, 'ShopFlag', 0016, 1086, 1036, 0262, 0214, 0000);
INSERT INTO `npcs` VALUES (0210, 'ShopFlag', 0016, 1086, 1036, 0262, 0218, 0000);
INSERT INTO `npcs` VALUES (0209, 'ShopFlag', 0016, 1086, 1036, 0237, 0173, 0000);
INSERT INTO `npcs` VALUES (0208, 'ShopFlag', 0016, 1086, 1036, 0237, 0177, 0000);
INSERT INTO `npcs` VALUES (0207, 'ShopFlag', 0016, 1086, 1036, 0237, 0181, 0000);
INSERT INTO `npcs` VALUES (0206, 'ShopFlag', 0016, 1086, 1036, 0237, 0185, 0000);
INSERT INTO `npcs` VALUES (0205, 'ShopFlag', 0016, 1086, 1036, 0237, 0189, 0000);
INSERT INTO `npcs` VALUES (0204, 'ShopFlag', 0016, 1086, 1036, 0237, 0193, 0000);
INSERT INTO `npcs` VALUES (0203, 'ShopFlag', 0016, 1086, 1036, 0237, 0197, 0000);
INSERT INTO `npcs` VALUES (0202, 'ShopFlag', 0016, 1086, 1036, 0237, 0201, 0000);
INSERT INTO `npcs` VALUES (0201, 'ShopFlag', 0016, 1086, 1036, 0237, 0205, 0000);
INSERT INTO `npcs` VALUES (0200, 'ShopFlag', 0016, 1086, 1036, 0237, 0209, 0000);
INSERT INTO `npcs` VALUES (0199, 'ShopFlag', 0016, 1086, 1036, 0237, 0213, 0000);
INSERT INTO `npcs` VALUES (0198, 'ShopFlag', 0016, 1086, 1036, 0237, 0217, 0000);
INSERT INTO `npcs` VALUES (0185, 'ShopFlag', 0016, 1086, 1036, 0230, 0173, 0000);
INSERT INTO `npcs` VALUES (0184, 'ShopFlag', 0016, 1086, 1036, 0230, 0177, 0000);
INSERT INTO `npcs` VALUES (0183, 'ShopFlag', 0016, 1086, 1036, 0230, 0181, 0000);
INSERT INTO `npcs` VALUES (0182, 'ShopFlag', 0016, 1086, 1036, 0230, 0185, 0000);
INSERT INTO `npcs` VALUES (0181, 'ShopFlag', 0016, 1086, 1036, 0230, 0189, 0000);
INSERT INTO `npcs` VALUES (0180, 'ShopFlag', 0016, 1086, 1036, 0230, 0193, 0000);
INSERT INTO `npcs` VALUES (0179, 'ShopFlag', 0016, 1086, 1036, 0230, 0197, 0000);
INSERT INTO `npcs` VALUES (0178, 'ShopFlag', 0016, 1086, 1036, 0230, 0201, 0000);
INSERT INTO `npcs` VALUES (0177, 'ShopFlag', 0016, 1086, 1036, 0230, 0205, 0000);
INSERT INTO `npcs` VALUES (0176, 'ShopFlag', 0016, 1086, 1036, 0230, 0209, 0000);
INSERT INTO `npcs` VALUES (0175, 'ShopFlag', 0016, 1086, 1036, 0230, 0213, 0000);
INSERT INTO `npcs` VALUES (0174, 'ShopFlag', 0016, 1086, 1036, 0230, 0217, 0000);
INSERT INTO `npcs` VALUES (0173, 'ShopFlag', 0016, 1086, 1036, 0205, 0127, 0000);
INSERT INTO `npcs` VALUES (0172, 'ShopFlag', 0016, 1086, 1036, 0205, 0132, 0000);
INSERT INTO `npcs` VALUES (0171, 'ShopFlag', 0016, 1086, 1036, 0205, 0137, 0000);
INSERT INTO `npcs` VALUES (0170, 'ShopFlag', 0016, 1086, 1036, 0205, 0142, 0000);
INSERT INTO `npcs` VALUES (0169, 'ShopFlag', 0016, 1086, 1036, 0205, 0147, 0000);
INSERT INTO `npcs` VALUES (0168, 'ShopFlag', 0016, 1086, 1036, 0205, 0152, 0000);
INSERT INTO `npcs` VALUES (0167, 'ShopFlag', 0016, 1086, 1036, 0198, 0127, 0000);
INSERT INTO `npcs` VALUES (0166, 'ShopFlag', 0016, 1086, 1036, 0198, 0132, 0000);
INSERT INTO `npcs` VALUES (0165, 'ShopFlag', 0016, 1086, 1036, 0198, 0137, 0000);
INSERT INTO `npcs` VALUES (0164, 'ShopFlag', 0016, 1086, 1036, 0198, 0142, 0000);
INSERT INTO `npcs` VALUES (0163, 'ShopFlag', 0016, 1086, 1036, 0198, 0147, 0000);
INSERT INTO `npcs` VALUES (0162, 'ShopFlag', 0016, 1086, 1036, 0198, 0152, 0000);
INSERT INTO `npcs` VALUES (0161, 'ShopFlag', 0016, 1086, 1036, 0173, 0127, 0000);
INSERT INTO `npcs` VALUES (0160, 'ShopFlag', 0016, 1086, 1036, 0173, 0132, 0000);
INSERT INTO `npcs` VALUES (0159, 'ShopFlag', 0016, 1086, 1036, 0173, 0137, 0000);
INSERT INTO `npcs` VALUES (0158, 'ShopFlag', 0016, 1086, 1036, 0173, 0142, 0000);
INSERT INTO `npcs` VALUES (0157, 'ShopFlag', 0016, 1086, 1036, 0173, 0147, 0000);
INSERT INTO `npcs` VALUES (0156, 'ShopFlag', 0016, 1086, 1036, 0173, 0152, 0000);
INSERT INTO `npcs` VALUES (0155, 'ShopFlag', 0016, 1086, 1036, 0166, 0127, 0000);
INSERT INTO `npcs` VALUES (0150, 'ShopFlag', 0016, 1086, 1036, 0166, 0152, 0000);
INSERT INTO `npcs` VALUES (0151, 'ShopFlag', 0016, 1086, 1036, 0166, 0147, 0000);
INSERT INTO `npcs` VALUES (0154, 'ShopFlag', 0016, 1086, 1036, 0166, 0132, 0000);
INSERT INTO `npcs` VALUES (0153, 'ShopFlag', 0016, 1086, 1036, 0166, 0137, 0000);
INSERT INTO `npcs` VALUES (0152, 'ShopFlag', 0016, 1086, 1036, 0166, 0142, 0000);
INSERT INTO `npcs` VALUES (3381, 'SurgeonMiracle', 0002, 2410, 1036, 0252, 0242, 5029001);
INSERT INTO `npcs` VALUES (5004, 'MillionaireLee', 0002, 4294, 1036, 0182, 0183, 0000);
INSERT INTO `npcs` VALUES (89738, 'MoonGoddess', 0002, 4053, 5000, 0040, 0062, 3350);
INSERT INTO `npcs` VALUES (4720, 'WindSage', 0002, 50026, 1004, 0033, 0041, 0000);
INSERT INTO `npcs` VALUES (4271, 'HeadAbbot', 0002, 50038, 1004, 0033, 0033, 0000);
INSERT INTO `npcs` VALUES (5600, 'Mount', 0002, 6327, 1002, 0260, 0286, 0000);
INSERT INTO `npcs` VALUES (27838, 'GameManager', 0002, 6797, 10360, 0192, 0179, 0000);
INSERT INTO `npcs` VALUES (269857, 'CasinoHostess', 0002, 6806, 0150, 0151, 0086, 0000);
INSERT INTO `npcs` VALUES (369857, 'CasinoHostess', 0002, 6807, 8881, 0086, 0093, 0000);
INSERT INTO `npcs` VALUES (10010, 'WarehouseMan', 0003, 0080, 8880, 0150, 0156, 8003000);
INSERT INTO `npcs` VALUES (5533, 'ClanManager', 0002, 9450, 1002, 0278, 0290, 0000);
INSERT INTO `npcs` VALUES (355910, 'LeadChiMaster', 0002, 8976, 1252, 0033, 0059, 0000);
INSERT INTO `npcs` VALUES (7778, 'LeadMartial', 0002, 8946, 1252, 0033, 0046, 0000);
INSERT INTO `npcs` VALUES (7777, 'LeadApothecary', 0002, 8937, 1252, 0033, 0037, 0000);
INSERT INTO `npcs` VALUES (7776, 'LeadPerformer', 0002, 8920, 1252, 0039, 0033, 0000);
INSERT INTO `npcs` VALUES (7779, 'LeadWarlock', 0002, 8950, 1252, 0047, 0033, 0000);
INSERT INTO `npcs` VALUES (7780, 'LeadSage', 0002, 8961, 1252, 0055, 0033, 0000);
INSERT INTO `npcs` VALUES (8616, 'Wrangler', 0002, 11280, 1252, 0063, 0033, 0000);
INSERT INTO `npcs` VALUES (9521, 'Mr.Sage', 0002, 24540, 1036, 0212, 0192, 0000);
INSERT INTO `npcs` VALUES (6002, 'GarmentShopKeepe', 0032, 4720, 1036, 0200, 0179, 0000);
INSERT INTO `npcs` VALUES (3608, 'SatanSeal', 0002, 2240, 1700, 0331, 0340, 0000);
INSERT INTO `npcs` VALUES (30657, 'WarehouseHouse', 0003, 8200, 1099, 0055, 0055, 0000);
INSERT INTO `npcs` VALUES (2314007, 'spouseaward', 0002, 2920, 1090, 0033, 0045, 0000);
INSERT INTO `npcs` VALUES (445532, 'LevelingMaps', 0002, 29240, 5000, 0309, 0345, 0000);
INSERT INTO `npcs` VALUES (54239, 'LastFighting', 0002, 8500, 5000, 0347, 0252, 0000);
INSERT INTO `npcs` VALUES (4272, 'PirateLord', 0002, 50078, 1004, 0066, 0033, 0000);
INSERT INTO `npcs` VALUES (5014, 'BotJail', 0002, 1160, 6004, 0028, 0068, 0000);
INSERT INTO `npcs` VALUES (7935, 'NameRegistrar', 0032, 9186, 1036, 0201, 0196, 0000);
INSERT INTO `npcs` VALUES (5016, 'ElitePkOut', 0002, 4470, 7777, 0098, 0164, 0000);
INSERT INTO `npcs` VALUES (101888, 'GuildOfficer', 0002, 8160, 1038, 0044, 0060, 0000);
INSERT INTO `npcs` VALUES (9817, 'SilverOne-armed', 0060, 19776, 1036, 0204, 0212, 0000);
INSERT INTO `npcs` VALUES (4451, 'Confiscator', 0002, 8737, 6000, 0028, 0068, 0000);
INSERT INTO `npcs` VALUES (448582, 'ElitePkEnvoy', 0002, 8500, 1002, 0301, 0141, 0000);
INSERT INTO `npcs` VALUES (70754, 'MartialDealer', 0002, 26390, 1036, 0208, 0192, 0000);
INSERT INTO `npcs` VALUES (15805, 'Dr.YinYang', 0002, 24904, 1036, 0198, 0191, 0000);
INSERT INTO `npcs` VALUES (59558, 'RebirthMaster', 0002, 13400, 1036, 0212, 0213, 0000);
INSERT INTO `npcs` VALUES (500010, 'PowerArena', 0002, 9430, 1002, 0336, 0134, 0000);
INSERT INTO `npcs` VALUES (1800, 'Boxer', 0002, 3196, 1002, 0222, 0234, 0000);
INSERT INTO `npcs` VALUES (7231, 'Veer~Points', 0002, 29220, 5000, 0310, 0277, 0000);
INSERT INTO `npcs` VALUES (7012, 'ClassPkEscort', 0002, 5150, 7006, 0085, 0062, 0000);
INSERT INTO `npcs` VALUES (5020, 'Skill[OuT]', 0002, 11220, 4662, 0172, 0165, 0000);
INSERT INTO `npcs` VALUES (4456, '4thFlameStone', 0002, 6600, 1038, 0194, 0168, 0000);
INSERT INTO `npcs` VALUES (4457, '5thFlameStone', 0002, 6610, 1038, 0115, 0053, 0000);
INSERT INTO `npcs` VALUES (4482116, 'ClanWar', 0002, 8500, 5000, 0308, 0145, 0000);
INSERT INTO `npcs` VALUES (448211, 'OuTClan', 0002, 9350, 1509, 0081, 0108, 0000);
INSERT INTO `npcs` VALUES (7010, 'ClassPkEscort', 0002, 5150, 7004, 0085, 0062, 0000);
INSERT INTO `npcs` VALUES (101897, 'ClanTwinCity', 0031, 9340, 1002, 0343, 0248, NULL);
INSERT INTO `npcs` VALUES (7011, 'ClassPkEscort', 0002, 5150, 7005, 0085, 0062, 0000);
INSERT INTO `npcs` VALUES (8911, 'Mascot', 0002, 12170, 1252, 0065, 0033, 0000);
INSERT INTO `npcs` VALUES (8591, 'SubClass', 0002, 11210, 1004, 0050, 0033, 0000);
INSERT INTO `npcs` VALUES (0020, 'KungfuKing', 0002, 50617, 1004, 0075, 0046, 0000);
INSERT INTO `npcs` VALUES (4482115, 'PoleTwinPriz', 0002, 8166, 2072, 0284, 0263, 0000);
INSERT INTO `npcs` VALUES (4455, '3rdFlameStone', 0002, 6590, 1038, 0236, 0291, 0000);
INSERT INTO `npcs` VALUES (6589, 'RealmGuard', 0002, 8630, 1002, 0517, 0742, 0000);
INSERT INTO `npcs` VALUES (1152, 'Simon', 0002, 6520, 1002, 0265, 0135, 0000);
INSERT INTO `npcs` VALUES (10047, 'TaoistYun', 0002, 19980, 1002, 0261, 0240, 0000);
INSERT INTO `npcs` VALUES (35016, 'WuxingOven', 0008, 6680, 1036, 0178, 0204, 0000);
INSERT INTO `npcs` VALUES (131681, 'JiangNPC', 0002, 35621, 5000, 0056, 0053, 0000);
INSERT INTO `npcs` VALUES (65825, 'SoldierRemains', 0002, 8170, 1000, 0593, 0510, 0000);
INSERT INTO `npcs` VALUES (9952, 'Censer', 0002, 19490, 3081, 0045, 0051, 0000);
INSERT INTO `npcs` VALUES (9954, 'Censer', 0002, 19490, 3081, 0064, 0051, 0000);
INSERT INTO `npcs` VALUES (65826, 'SoldierRemains', 0002, 8170, 1000, 0597, 0501, 0000);
INSERT INTO `npcs` VALUES (9953, 'Censer', 0002, 19490, 3081, 0054, 0051, 0000);
INSERT INTO `npcs` VALUES (18716, 'SolarSaint', 0002, 1310, 3955, 0253, 0057, 0000);
INSERT INTO `npcs` VALUES (27837, 'FrozenGrotto', 0002, 4190, 1000, 0472, 0645, 0000);
INSERT INTO `npcs` VALUES (9998, 'DailyQuestEnvoy', 0002, 19936, 1002, 0293, 0301, 0000);
INSERT INTO `npcs` VALUES (9891, 'GoodManLiu', 0002, 19300, 3081, 0054, 0062, 0000);
INSERT INTO `npcs` VALUES (369859, 'CasinoHostess', 0002, 6807, 2807, 0052, 0060, 0000);
INSERT INTO `npcs` VALUES (50404, 'MerchantClerk', 0002, 4276, 1036, 0178, 0187, 0000);
INSERT INTO `npcs` VALUES (600006, 'GuardianGod', 0002, 6111, 1044, 0204, 0150, 0000);
INSERT INTO `npcs` VALUES (9463, 'CoastGeneral', 0002, 17160, 3055, 0061, 0084, 0000);
INSERT INTO `npcs` VALUES (115522, 'EliteGW', 0002, 8500, 5000, 0295, 0145, 0000);
INSERT INTO `npcs` VALUES (0705, 'ClassPkEnvoy', 0002, 1350, 1002, 0308, 0141, 0000);
INSERT INTO `npcs` VALUES (8798, 'Rufurbisher', 0002, 1340, 1012, 0070, 0070, 0000);
INSERT INTO `npcs` VALUES (8308, 'Spook~Shop', 0002, 2507, 1019, 0025, 0030, 0000);
INSERT INTO `npcs` VALUES (0301, 'DesertGuardian', 0002, 8180, 1000, 0536, 0643, 0000);
INSERT INTO `npcs` VALUES (65822, 'SoldierRemains', 0002, 8170, 1000, 0621, 0521, 0000);
INSERT INTO `npcs` VALUES (65823, 'SoldierRemains', 0002, 8170, 1000, 0603, 0529, 0000);
INSERT INTO `npcs` VALUES (65824, 'SoldierRemains', 0002, 8170, 1000, 0598, 0532, 0000);
INSERT INTO `npcs` VALUES (9893, 'HuangDi', 0002, 19440, 3081, 0043, 0044, 0000);
INSERT INTO `npcs` VALUES (9897, 'MagnoliaEnvoy', 0002, 19327, 1806, 0103, 0105, 0000);
INSERT INTO `npcs` VALUES (1650, 'AuctionManager', 0002, 16500, 1036, 0204, 0205, 0000);
INSERT INTO `npcs` VALUES (216341, 'MatchMaker', 0002, 9456, 5000, 0286, 0284, 0000);
INSERT INTO `npcs` VALUES (2315, 'GeneralWinner', 0002, 9450, 1508, 0099, 0126, 0000);
INSERT INTO `npcs` VALUES (9892, 'Nuwa', 0002, 19310, 3081, 0052, 0044, 0000);
INSERT INTO `npcs` VALUES (8426, 'DC-ViceGeneral', 0002, 10010, 1000, 0520, 0641, 0000);
INSERT INTO `npcs` VALUES (9135, 'CashRain', 0002, 14160, 1002, 0285, 0287, 0000);
INSERT INTO `npcs` VALUES (4358, 'StoreKeeper', 0001, 2491, 2057, 0476, 0375, 0000);
INSERT INTO `npcs` VALUES (7005, 'GuildGateKeeper', 0002, 1131, 2057, 0482, 0367, 0000);
INSERT INTO `npcs` VALUES (2071, 'CPAdmin', 0002, 3336, 1036, 0178, 0182, 0000);
INSERT INTO `npcs` VALUES (8427, 'TaoistYu', 0002, 10025, 1000, 0519, 0649, 0000);
INSERT INTO `npcs` VALUES (3962, 'Agate', 0002, 9446, 1737, 0054, 0049, 0000);
INSERT INTO `npcs` VALUES (6001, 'HorseRaceManager', 0002, 5150, 1002, 0289, 0147, 0000);
INSERT INTO `npcs` VALUES (8522, 'SteedRaceOut', 0002, 5150, 1950, 0420, 0420, 0000);
INSERT INTO `npcs` VALUES (0708, 'ClassPkEscort', 0002, 5150, 7002, 0085, 0062, 0000);
INSERT INTO `npcs` VALUES (0709, 'ClassPkEscort', 0002, 5150, 7003, 0085, 0062, 0000);
INSERT INTO `npcs` VALUES (0706, 'ClassPkEscort', 0002, 5150, 7000, 0085, 0062, 0000);
INSERT INTO `npcs` VALUES (0707, 'ClassPkEscort', 0002, 5150, 7001, 0085, 0062, 0000);
INSERT INTO `npcs` VALUES (7013, 'ClassPkEscort', 0002, 5150, 7007, 0085, 0062, 0000);
INSERT INTO `npcs` VALUES (7014, 'ClassPkEscort', 0002, 5150, 7008, 0085, 0062, 0000);
INSERT INTO `npcs` VALUES (9158, 'SkillTeamPk', 0002, 11220, 1002, 0313, 0141, 0000);
INSERT INTO `npcs` VALUES (8158, 'TeamPkManager', 0002, 9730, 1002, 0291, 0142, 0000);
INSERT INTO `npcs` VALUES (565214, 'GeneralBravely', 0002, 8500, 1002, 0327, 0193, 0000);
INSERT INTO `npcs` VALUES (552553, 'Prize', 0002, 8500, 1505, 0165, 0224, 0000);
INSERT INTO `npcs` VALUES (17000, 'KingdomMission', 0002, 33230, 1002, 0284, 0291, 0000);
INSERT INTO `npcs` VALUES (16849, 'P7Explorer', 0002, 30890, 1036, 0197, 0206, 0000);
INSERT INTO `npcs` VALUES (17173, 'Pharmacist', 0001, 0030, 3856, 0377, 0276, 0000);
INSERT INTO `npcs` VALUES (32150, 'SolarSaint', 0002, 1310, 1002, 0297, 0146, 0000);
INSERT INTO `npcs` VALUES (0601, 'Treasury', 0002, 38230, 1002, 0313, 0279, 0000);
INSERT INTO `npcs` VALUES (87464, 'AgateSteed', 0000, 5864, 1006, 0021, 0033, 0000);
INSERT INTO `npcs` VALUES (87465, 'RedSteed', 0000, 7644, 1006, 0025, 0033, 0000);
INSERT INTO `npcs` VALUES (87466, 'TawnySteed', 0000, 7664, 1006, 0029, 0033, 0000);
INSERT INTO `npcs` VALUES (8745, 'NightMare', 0000, 7630, 1006, 0024, 0022, 0000);
INSERT INTO `npcs` VALUES (87462, 'Zebra', 0000, 5850, 1006, 0017, 0022, 0000);
INSERT INTO `npcs` VALUES (9871, 'Blazehoof', 0000, 5844, 1006, 0017, 0033, 0000);
INSERT INTO `npcs` VALUES (0765, 'BlackSteed', 0000, 7650, 1006, 0032, 0022, 0000);
INSERT INTO `npcs` VALUES (5517, 'MountRetailer', 0001, 5836, 1006, 0019, 0029, 0000);
INSERT INTO `npcs` VALUES (5603, 'MountOfficer', 0002, 6224, 1006, 0038, 0034, 0000);
INSERT INTO `npcs` VALUES (9894, 'YanDi', 0002, 19450, 3081, 0061, 0044, 0000);
INSERT INTO `npcs` VALUES (65827, 'SoldierRemains', 0002, 8170, 1000, 0599, 0509, 0000);
INSERT INTO `npcs` VALUES (65828, 'SoldierRemains', 0002, 8170, 1000, 0569, 0515, 0000);
INSERT INTO `npcs` VALUES (65829, 'SoldierRemains', 0002, 8170, 1000, 0564, 0521, 0000);
INSERT INTO `npcs` VALUES (65830, 'SoldierRemains', 0002, 8170, 1000, 0565, 0531, 0000);
INSERT INTO `npcs` VALUES (65831, 'SoldierRemains', 0002, 8170, 1000, 0590, 0505, 0000);
INSERT INTO `npcs` VALUES (65832, 'SoldierRemains', 0002, 8170, 1000, 0594, 0515, 0000);
INSERT INTO `npcs` VALUES (65833, 'SoldierRemains', 0002, 8170, 1000, 0639, 0549, 0000);
INSERT INTO `npcs` VALUES (65834, 'SoldierRemains', 0002, 8170, 1000, 0637, 0560, 0000);
INSERT INTO `npcs` VALUES (65835, 'SoldierRemains', 0002, 8170, 1000, 0633, 0567, 0000);
INSERT INTO `npcs` VALUES (65836, 'SoldierRemains', 0002, 8170, 1000, 0637, 0586, 0000);
INSERT INTO `npcs` VALUES (65837, 'SoldierRemains', 0002, 8170, 1000, 0564, 0453, 0000);
INSERT INTO `npcs` VALUES (65838, 'SoldierRemains', 0002, 8170, 1000, 0569, 0450, 0000);
INSERT INTO `npcs` VALUES (65839, 'SoldierRemains', 0002, 8170, 1000, 0570, 0456, 0000);
INSERT INTO `npcs` VALUES (65840, 'SoldierRemains', 0002, 8170, 1000, 0567, 0439, 0000);
INSERT INTO `npcs` VALUES (16051, 'MysteryChest', 0002, 26586, 9979, 0050, 0061, 0000);
INSERT INTO `npcs` VALUES (16052, 'MysteryChest', 0002, 26596, 9979, 0050, 0057, 0000);
INSERT INTO `npcs` VALUES (16053, 'MysteryChest', 0002, 26606, 9979, 0050, 0053, 0000);
INSERT INTO `npcs` VALUES (16054, 'MysteryChest', 0002, 26616, 9979, 0050, 0049, 0000);
INSERT INTO `npcs` VALUES (16055, 'MysteryChest', 0002, 26626, 9979, 0050, 0045, 0000);
INSERT INTO `npcs` VALUES (16050, 'FairyCloud', 0002, 26576, 9979, 0057, 0053, 0000);
INSERT INTO `npcs` VALUES (16060, 'FairyCloud', 0002, 26576, 9980, 0057, 0053, 0000);
INSERT INTO `npcs` VALUES (16061, 'MysteryChest', 0002, 26846, 9980, 0050, 0047, 0000);
INSERT INTO `npcs` VALUES (16062, 'MysteryChest', 0002, 26856, 9980, 0050, 0051, 0000);
INSERT INTO `npcs` VALUES (16063, 'MysteryChest', 0002, 26866, 9980, 0050, 0055, 0000);
INSERT INTO `npcs` VALUES (16064, 'MysteryChest', 0002, 26876, 9980, 0050, 0059, 0000);
INSERT INTO `npcs` VALUES (16071, 'MysteryChest', 0002, 26886, 9981, 0050, 0058, 0000);
INSERT INTO `npcs` VALUES (16072, 'MysteryChest', 0002, 26896, 9981, 0050, 0053, 0000);
INSERT INTO `npcs` VALUES (16073, 'MysteryChest', 0002, 26906, 9981, 0050, 0048, 0000);
INSERT INTO `npcs` VALUES (16070, 'FairyCloud', 0002, 26576, 9981, 0057, 0053, 0000);
INSERT INTO `npcs` VALUES (16080, 'FairyCloud', 0002, 26576, 9982, 0057, 0053, 0000);
INSERT INTO `npcs` VALUES (16081, 'MysteryChest', 0002, 26886, 9982, 0050, 0056, 0000);
INSERT INTO `npcs` VALUES (16082, 'MysteryChest', 0002, 26896, 9982, 0050, 0050, 0000);
INSERT INTO `npcs` VALUES (7764, 'FrozenGrottoF5', 0002, 9787, 2054, 0387, 0460, 0000);
INSERT INTO `npcs` VALUES (7766, 'FrozenGrotto', 0002, 8145, 2055, 0542, 0576, 0000);
INSERT INTO `npcs` VALUES (98742, 'FlowerBed', 0002, 19390, 1806, 0105, 0098, 0000);
INSERT INTO `npcs` VALUES (98743, 'FlowerBed', 0002, 19400, 1806, 0098, 0111, 0000);
INSERT INTO `npcs` VALUES (30156, 'HouseAdmin', 0002, 8060, 1036, 0196, 0091, 0000);
INSERT INTO `npcs` VALUES (8233, 'HeavenlyMaster', 0002, 7790, 1036, 0244, 0150, 0000);
INSERT INTO `npcs` VALUES (15459, 'SilverOne-armed', 0060, 23136, 1002, 0349, 0289, 0000);
INSERT INTO `npcs` VALUES (15460, 'CPOne-armed', 0060, 23146, 1002, 0349, 0283, 0000);
INSERT INTO `npcs` VALUES (15462, 'CPOne-armed', 0060, 23166, 1002, 0349, 0271, 0000);
INSERT INTO `npcs` VALUES (15461, 'CPOne-armed', 0060, 23156, 1002, 0349, 0277, 0000);
INSERT INTO `npcs` VALUES (9827, 'CPOne-armed', 0060, 19796, 1036, 0232, 0244, 0000);
INSERT INTO `npcs` VALUES (9826, 'CPOne-armed', 0060, 19786, 1036, 0232, 0233, 0000);
INSERT INTO `npcs` VALUES (9828, 'CPOne-armed', 0060, 19806, 1036, 0242, 0233, 0000);
INSERT INTO `npcs` VALUES (0930, 'ChiBox', 0002, 9307, 0700, 0051, 0051, 0000);
INSERT INTO `npcs` VALUES (0927, 'JiangHuBox', 0002, 9267, 0700, 0045, 0045, 0000);
INSERT INTO `npcs` VALUES (0926, 'SteedBox', 0002, 9277, 0700, 0045, 0057, 0000);
INSERT INTO `npcs` VALUES (0928, 'StoneBox', 0002, 9287, 0700, 0057, 0045, 0000);
INSERT INTO `npcs` VALUES (15995, 'ImperialJeweler', 0002, 26490, 1036, 0182, 0207, 0000);
INSERT INTO `npcs` VALUES (9623, 'MammonEnvoy', 0002, 18007, 1068, 0050, 0050, 0000);
INSERT INTO `npcs` VALUES (42251, 'SquidOctopus', 0002, 17987, 1002, 0288, 0207, 0000);
INSERT INTO `npcs` VALUES (9619, 'PCTeleporter', 0002, 17997, 3071, 0346, 0200, NULL);
INSERT INTO `npcs` VALUES (9620, 'PCTeleporter', 0002, 17997, 3071, 0201, 0076, NULL);
INSERT INTO `npcs` VALUES (9622, 'PCTeleporter', 0002, 17997, 3071, 0061, 0128, NULL);
INSERT INTO `npcs` VALUES (9653, 'PCTeleporter', 0002, 17997, 3071, 0146, 0144, NULL);
INSERT INTO `npcs` VALUES (18740, 'KingdomQuiz', 0002, 37456, 1002, 0309, 0348, 0000);
INSERT INTO `npcs` VALUES (3121, '', 0026, 0101, 1511, 0051, 0044, 0000);
INSERT INTO `npcs` VALUES (3120, '', 0026, 0091, 1511, 0044, 0044, 0000);
INSERT INTO `npcs` VALUES (3125, '', 0026, 0041, 1511, 0044, 0051, 0000);
INSERT INTO `npcs` VALUES (3118, '', 0026, 0071, 1511, 0051, 0058, 0000);
INSERT INTO `npcs` VALUES (3114, '', 0026, 0021, 1511, 0037, 0065, 0000);
INSERT INTO `npcs` VALUES (3113, '', 0026, 0010, 1511, 0051, 0065, 0000);
INSERT INTO `npcs` VALUES (8921, '', 0026, 0807, 1511, 0037, 0037, 0000);
INSERT INTO `npcs` VALUES (3119, '', 0026, 0081, 1511, 0037, 0044, 0000);
INSERT INTO `npcs` VALUES (3116, '', 0026, 0051, 1511, 0051, 0037, 0000);
INSERT INTO `npcs` VALUES (3115, '', 0026, 0037, 1511, 0044, 0037, 0000);
INSERT INTO `npcs` VALUES (3122, '', 0026, 0111, 1511, 0058, 0044, 0000);
INSERT INTO `npcs` VALUES (10657, 'YinYangStele', 0002, 31237, 3849, 0038, 0036, 0000);
INSERT INTO `npcs` VALUES (10660, 'DivineShadow', 0002, 50447, 3849, 0028, 0033, 0000);
INSERT INTO `npcs` VALUES (10912, 'GoldenCicada', 0002, 50976, 3983, 0085, 0070, 0000);
INSERT INTO `npcs` VALUES (10913, 'FortuneArhat', 0002, 38426, 3983, 0077, 0083, 0000);
INSERT INTO `npcs` VALUES (10914, 'AltarCleanser', 0002, 38430, 3983, 0062, 0076, 0000);
INSERT INTO `npcs` VALUES (10916, 'WhiteDragon', 0002, 38456, 3983, 0054, 0063, 0000);
INSERT INTO `npcs` VALUES (10915, 'VictoryBuddha', 0002, 38440, 3983, 0060, 0055, 0000);
INSERT INTO `npcs` VALUES (47554, 'ElitePureOven', 0002, 35940, 10332, 0220, 0348, NULL);
INSERT INTO `npcs` VALUES (48554, 'SuperPureOven', 0002, 35950, 10332, 0413, 0444, NULL);
INSERT INTO `npcs` VALUES (15983, 'SunKing', 0002, 50050, 10332, 0322, 0440, 0000);
INSERT INTO `npcs` VALUES (10579, 'Pak\'sGhost', 0002, 29196, 1002, 0267, 0194, 0000);
INSERT INTO `npcs` VALUES (18717, 'SolarSaint', 0002, 1314, 3956, 0215, 0344, 0000);
INSERT INTO `npcs` VALUES (31456, 'HellCrystal', 0002, 37480, 3957, 0502, 0218, 0000);
INSERT INTO `npcs` VALUES (31455, 'HellCrystal', 0002, 37480, 3957, 0335, 0120, 0000);
INSERT INTO `npcs` VALUES (31454, 'HellCrystal', 0002, 37480, 3957, 0235, 0220, 0000);
INSERT INTO `npcs` VALUES (31453, 'HellCrystal', 0002, 37480, 3957, 0125, 0320, 0000);
INSERT INTO `npcs` VALUES (31452, 'HellCrystal', 0002, 37480, 3957, 0165, 0470, 0000);
INSERT INTO `npcs` VALUES (31451, 'HellCrystal', 0002, 37480, 3957, 0260, 0415, 0000);
INSERT INTO `npcs` VALUES (31450, 'HellCrystal', 0002, 37480, 3957, 0278, 0287, 0000);
INSERT INTO `npcs` VALUES (31457, 'HellCrystal', 0002, 37480, 3957, 0360, 0280, 0000);
INSERT INTO `npcs` VALUES (31458, 'HellCrystal', 0002, 37480, 3957, 0417, 0373, 0000);
INSERT INTO `npcs` VALUES (18718, 'SolarSaint', 0002, 1310, 3957, 0343, 0385, 0000);
INSERT INTO `npcs` VALUES (18719, 'SolarSaint', 0002, 1310, 3958, 0149, 0035, 0000);
INSERT INTO `npcs` VALUES (2001, 'TCCaptain', 0002, 1746, 1002, 0307, 0338, NULL);
INSERT INTO `npcs` VALUES (600008, 'GuardianGod', 0002, 6111, 1045, 0204, 0150, 0000);
INSERT INTO `npcs` VALUES (600010, 'GuardianGod', 0002, 6111, 1046, 0204, 0150, 0000);
INSERT INTO `npcs` VALUES (600014, 'GuardianGod', 0002, 6111, 1048, 0204, 0150, 0000);
INSERT INTO `npcs` VALUES (600012, 'GuardianGod', 0002, 6111, 1047, 0204, 0150, 0000);
INSERT INTO `npcs` VALUES (600017, 'Ghost', 0002, 6127, 1049, 0006, 0066, 0000);
INSERT INTO `npcs` VALUES (600016, 'GuardianGod', 0002, 6111, 1049, 0204, 0150, 0000);
INSERT INTO `npcs` VALUES (9071, 'Equipment', 0033, 13390, 1036, 0178, 0199, 0000);
INSERT INTO `npcs` VALUES (10223, 'PrizeOfficer', 0002, 1060, 1002, 0269, 0226, 0000);
INSERT INTO `npcs` VALUES (16833, 'SharkSoldier', 0002, 30770, 3055, 0066, 0087, 0000);
INSERT INTO `npcs` VALUES (9465, 'Armorer', 0001, 0040, 3055, 0041, 0091, 0000);
INSERT INTO `npcs` VALUES (9464, 'Conductress', 0002, 0280, 3055, 0024, 0090, 0000);
INSERT INTO `npcs` VALUES (9467, 'StoreKeeper', 0001, 0017, 3055, 0082, 0102, 0000);
INSERT INTO `npcs` VALUES (9468, 'Blacksmith', 0001, 0055, 3055, 0069, 0110, 0000);
INSERT INTO `npcs` VALUES (16834, 'SharkSoldier', 0032, 30780, 3856, 0380, 0266, 0000);
INSERT INTO `npcs` VALUES (9539, 'GaleShallow', 0002, 17556, 1000, 0467, 0633, 0000);
INSERT INTO `npcs` VALUES (102060, 'SealerProtector', 0002, 16546, 1300, 0430, 0330, 0000);
INSERT INTO `npcs` VALUES (102061, 'SealerProtector', 0002, 16542, 1300, 0444, 0330, 0000);
INSERT INTO `npcs` VALUES (9363, 'DemonSealer', 0002, 16530, 1300, 0437, 0329, 0000);
INSERT INTO `npcs` VALUES (8425, 'DC-ViceCaptain', 0002, 10006, 1000, 0500, 0652, 0000);
INSERT INTO `npcs` VALUES (19313, 'KongfuKing', 0002, 50610, 1002, 0270, 0218, 0000);
INSERT INTO `npcs` VALUES (4488, 'GeneralZhao', 0002, 6716, 1002, 0262, 0247, 0000);
INSERT INTO `npcs` VALUES (4490, 'GuardLi', 0002, 6737, 1002, 0261, 0243, 0000);
INSERT INTO `npcs` VALUES (0047, 'PrizeOfficer', 0002, 1067, 1036, 0185, 0170, 0000);
INSERT INTO `npcs` VALUES (18786, 'CelestialSage', 0002, 37810, 1036, 0178, 0209, 0000);
INSERT INTO `npcs` VALUES (19312, 'DragonballSales', 0002, 41830, 10012, 0293, 0292, 0000);
INSERT INTO `npcs` VALUES (1611, 'GeneralJudd', 0002, 6990, 1002, 0268, 0133, 0000);
INSERT INTO `npcs` VALUES (4704, 'OldBeggar', 0002, 4570, 1002, 0263, 0135, 0000);
INSERT INTO `npcs` VALUES (4432, 'ArsenalGuard', 0002, 5790, 1002, 0277, 0184, 0000);
INSERT INTO `npcs` VALUES (4466, 'ApprenticeLuo', 0002, 5500, 1002, 0319, 0231, 0000);
INSERT INTO `npcs` VALUES (15140, 'Gouda', 0002, 1820, 1002, 0345, 0225, 0000);
INSERT INTO `npcs` VALUES (4436, 'ThiefWong', 0002, 5960, 1002, 0347, 0228, 0000);
INSERT INTO `npcs` VALUES (4504, 'JerkWang', 0002, 5630, 1002, 0347, 0232, 0000);
INSERT INTO `npcs` VALUES (4484, 'BianQing', 0002, 7590, 1002, 0342, 0232, 0000);
INSERT INTO `npcs` VALUES (8615, 'TCWarder', 0002, 14056, 1002, 0342, 0228, 0000);
INSERT INTO `npcs` VALUES (4362, 'GuildChief', 0002, 2710, 5000, 0293, 0304, 0000);
INSERT INTO `npcs` VALUES (4505, 'TaoistShine', 0002, 5643, 1002, 0245, 0258, 0000);
INSERT INTO `npcs` VALUES (4434, 'IronsmithChou', 0002, 5950, 1002, 0240, 0254, 0000);
INSERT INTO `npcs` VALUES (10580, 'SuperMok', 0002, 29206, 1002, 0238, 0216, 0000);
INSERT INTO `npcs` VALUES (5673, 'KungfuBoy', 0002, 5060, 1002, 0270, 0223, 0000);
INSERT INTO `npcs` VALUES (5674, 'KungfuGirl', 0002, 5076, 1002, 0279, 0224, 0000);
INSERT INTO `npcs` VALUES (4486, 'RoyalDoctorLi', 0002, 6886, 1002, 0296, 0243, 0000);
INSERT INTO `npcs` VALUES (4453, '1stFlameStone', 0002, 6570, 1002, 0245, 0216, 0000);
INSERT INTO `npcs` VALUES (4461, '9thFlameStone', 0002, 6650, 1002, 0252, 0216, 0000);
INSERT INTO `npcs` VALUES (4719, 'Carolyn', 0002, 3730, 1000, 0460, 0667, 0000);
INSERT INTO `npcs` VALUES (4718, 'FarmerLynn', 0002, 4070, 1000, 0461, 0661, 0000);
INSERT INTO `npcs` VALUES (4651, 'Louis', 0002, 4056, 1000, 0466, 0658, 0000);
INSERT INTO `npcs` VALUES (4596, 'WineKiddo', 0002, 4130, 1011, 0182, 0280, 0000);
INSERT INTO `npcs` VALUES (4500, 'Minstrel', 0002, 6757, 1011, 0184, 0282, 0000);
INSERT INTO `npcs` VALUES (4464, 'VeteranHong', 0002, 6837, 1011, 0194, 0260, 0000);
INSERT INTO `npcs` VALUES (7995, 'YuLin', 0002, 9660, 1011, 0226, 0258, 0000);
INSERT INTO `npcs` VALUES (8271, 'PhoenixCastlelan', 0002, 4390, 1011, 0230, 0254, 0000);
INSERT INTO `npcs` VALUES (4482, 'ScholarWu', 0002, 7576, 1011, 0214, 0231, 0000);
INSERT INTO `npcs` VALUES (4483, 'PharmacistMuMu', 0002, 7587, 1011, 0182, 0241, 0000);
INSERT INTO `npcs` VALUES (4489, 'PainterFengKang', 0002, 6726, 1020, 0556, 0571, 0000);
INSERT INTO `npcs` VALUES (4485, 'OfficerBao', 0002, 6876, 1020, 0553, 0584, 0000);
INSERT INTO `npcs` VALUES (8590, 'AC-Castellan', 0002, 10986, 1020, 0552, 0594, 0000);
INSERT INTO `npcs` VALUES (4468, 'DuSan', 0002, 5520, 1020, 0552, 0601, 0000);
INSERT INTO `npcs` VALUES (8510, 'BICastellan', 0002, 10316, 1015, 0710, 0572, 0000);
INSERT INTO `npcs` VALUES (8512, 'BI-ViceCaptain', 0002, 10336, 1015, 0715, 0576, 0000);
INSERT INTO `npcs` VALUES (8513, 'ArmorerYu', 0002, 10343, 1015, 0720, 0561, 0000);
INSERT INTO `npcs` VALUES (4700, 'PoorXiao', 0002, 4030, 1015, 0725, 0541, 0000);
INSERT INTO `npcs` VALUES (4566, 'YangYun', 0002, 3950, 1015, 0732, 0516, 0000);
INSERT INTO `npcs` VALUES (8520, 'LittleBen', 0002, 10413, 1015, 0731, 0508, 0000);
INSERT INTO `npcs` VALUES (4705, 'Felix', 0002, 4610, 1015, 0686, 0547, 0000);
INSERT INTO `npcs` VALUES (8523, 'WealthyWanWife', 0002, 10443, 1015, 0684, 0567, 0000);
INSERT INTO `npcs` VALUES (8511, 'XuFan', 0002, 10326, 1015, 0685, 0594, 0000);
INSERT INTO `npcs` VALUES (4565, 'SugarTang', 0002, 3946, 1015, 0685, 0599, 0000);
INSERT INTO `npcs` VALUES (4701, 'DoctorKnowitAll', 0002, 4040, 1015, 0701, 0595, 0000);
INSERT INTO `npcs` VALUES (8519, 'WindTaoist', 0002, 10403, 1015, 0734, 0624, 0000);
INSERT INTO `npcs` VALUES (85211, 'CityGeneral', 0002, 10423, 1015, 0740, 0624, 0000);
INSERT INTO `npcs` VALUES (4633, 'AuntPeach', 0002, 3904, 1015, 0765, 0602, 0000);
INSERT INTO `npcs` VALUES (4458, '6thFlameStone', 0002, 6620, 1038, 0316, 0378, 0000);
INSERT INTO `npcs` VALUES (4459, '7thFlameStone', 0002, 6630, 1038, 0136, 0182, 0000);
INSERT INTO `npcs` VALUES (4460, '8thFlameStone', 0002, 6640, 1038, 0038, 0094, 0000);
INSERT INTO `npcs` VALUES (4462, '10thFlameStone', 0002, 6660, 1038, 0062, 0059, 0000);
INSERT INTO `npcs` VALUES (10948, 'ExplosiveDevice', 0002, 38950, 3935, 0378, 0404, 0000);
INSERT INTO `npcs` VALUES (10951, 'ExplosiveDevice', 0002, 38950, 3935, 0406, 0401, 0000);
INSERT INTO `npcs` VALUES (10950, 'ExplosiveDevice', 0002, 38950, 3935, 0400, 0364, 0000);
INSERT INTO `npcs` VALUES (10949, 'ExplosiveDevice', 0002, 38950, 3935, 0368, 0355, 0000);
INSERT INTO `npcs` VALUES (19121, 'Crystal', 0002, 40066, 3935, 0332, 0302, 0000);
INSERT INTO `npcs` VALUES (19166, 'FieryDragon', 0002, 40290, 4020, 0070, 0071, 0000);
INSERT INTO `npcs` VALUES (19231, 'Conductress', 0002, 0287, 4020, 0088, 0067, 0000);
INSERT INTO `npcs` VALUES (19128, 'CloudSweeper', 0002, 40136, 4020, 0082, 0059, 0000);
INSERT INTO `npcs` VALUES (19139, 'TowerGuardian', 0002, 40430, 4009, 0043, 0030, 0000);
INSERT INTO `npcs` VALUES (19187, 'Pharmacist', 0001, 0230, 4001, 0049, 0030, 0000);
INSERT INTO `npcs` VALUES (19127, 'TowerKeeper', 0002, 40127, 4020, 0079, 0068, 0000);
INSERT INTO `npcs` VALUES (19140, 'TowerGuardian', 0002, 40350, 4001, 0043, 0030, 0000);
INSERT INTO `npcs` VALUES (19141, 'TowerGuardian', 0002, 40360, 4002, 0043, 0030, 0000);
INSERT INTO `npcs` VALUES (19142, 'TowerGuardian', 0002, 40370, 4003, 0043, 0030, 0000);
INSERT INTO `npcs` VALUES (19143, 'TowerGuardian', 0002, 40380, 4004, 0043, 0030, 0000);
INSERT INTO `npcs` VALUES (19144, 'TowerGuardian', 0002, 40390, 4005, 0043, 0030, 0000);
INSERT INTO `npcs` VALUES (19145, 'TowerGuardian', 0002, 40400, 4006, 0043, 0030, 0000);
INSERT INTO `npcs` VALUES (19146, 'TowerGuardian', 0002, 40410, 4007, 0043, 0030, 0000);
INSERT INTO `npcs` VALUES (19147, 'TowerGuardian', 0002, 40420, 4008, 0043, 0030, 0000);
INSERT INTO `npcs` VALUES (19160, 'PakYan', 0002, 40237, 3998, 0085, 0356, 0000);
INSERT INTO `npcs` VALUES (19162, 'ChongYan', 0002, 40257, 3998, 0142, 0320, 0000);
INSERT INTO `npcs` VALUES (19184, 'BrightGuard', 0002, 40457, 3998, 0146, 0326, 0000);
INSERT INTO `npcs` VALUES (19183, 'BrightTribesman', 0002, 40447, 3998, 0147, 0317, 0000);
INSERT INTO `npcs` VALUES (19163, 'BrokenForge', 0002, 40267, 3998, 0311, 0449, 0000);
INSERT INTO `npcs` VALUES (19164, 'ChingYan', 0002, 40277, 3998, 0247, 0273, 0000);
INSERT INTO `npcs` VALUES (19168, 'WhiteHerp', 0002, 40307, 3998, 0269, 0272, 0000);
INSERT INTO `npcs` VALUES (19165, 'FlameAltar', 0002, 40286, 3998, 0573, 0423, 0000);
INSERT INTO `npcs` VALUES (19172, 'LavaFlower', 0002, 40317, 3998, 0553, 0693, 0000);
INSERT INTO `npcs` VALUES (19173, 'LavaFlower', 0002, 40317, 3998, 0555, 0692, 0000);
INSERT INTO `npcs` VALUES (19174, 'LavaFlower', 0002, 40317, 3998, 0554, 0687, 0000);
INSERT INTO `npcs` VALUES (19176, 'LavaFlower', 0002, 40317, 3998, 0555, 0685, 0000);
INSERT INTO `npcs` VALUES (19177, 'LavaFlower', 0002, 40317, 3998, 0558, 0687, 0000);
INSERT INTO `npcs` VALUES (19238, 'LavaFlower', 0002, 40317, 3998, 0564, 0688, 0000);
INSERT INTO `npcs` VALUES (19239, 'LavaFlower', 0002, 40317, 3998, 0569, 0685, 0000);
INSERT INTO `npcs` VALUES (19161, 'RemainofBright', 0002, 40247, 3998, 0162, 0395, 0000);
INSERT INTO `npcs` VALUES (16515, 'PinkHare', 0002, 29450, 1002, 0251, 0255, 0000);
INSERT INTO `npcs` VALUES (16519, 'WhiteHare', 0002, 29460, 1002, 0332, 0342, 0000);
INSERT INTO `npcs` VALUES (16520, 'BlueFox', 0002, 29470, 1002, 0259, 0321, 0000);
INSERT INTO `npcs` VALUES (16508, 'RedFox', 0002, 29440, 1002, 0299, 0347, 0000);
INSERT INTO `npcs` VALUES (16518, 'WhiteHare', 0002, 29460, 1002, 0333, 0371, 0000);
INSERT INTO `npcs` VALUES (16512, 'PinkHare', 0002, 29450, 1002, 0315, 0385, 0000);
INSERT INTO `npcs` VALUES (16517, 'WhiteHare', 0002, 29460, 1002, 0320, 0397, 0000);
INSERT INTO `npcs` VALUES (16521, 'BlueFox', 0002, 29470, 1002, 0336, 0405, 0000);
INSERT INTO `npcs` VALUES (10393, 'Chariot1Model-II', 0002, 26180, 1036, 0183, 0072, 0000);
INSERT INTO `npcs` VALUES (10394, 'Chariot2Model-II', 0002, 26190, 1036, 0175, 0072, 0000);
INSERT INTO `npcs` VALUES (10395, 'Chariot3Model-II', 0002, 26200, 1036, 0167, 0072, 0000);
INSERT INTO `npcs` VALUES (10492, 'Chariot4Model-II', 0002, 26400, 1036, 0181, 0080, 0000);
INSERT INTO `npcs` VALUES (10493, 'Chariot5Model-II', 0002, 26410, 1036, 0173, 0080, 0000);
INSERT INTO `npcs` VALUES (0683, 'Costumer', 0002, 9490, 1036, 0205, 0179, 0000);
INSERT INTO `npcs` VALUES (9945, 'FlowerBed', 0002, 19400, 1806, 0077, 0099, 0000);
INSERT INTO `npcs` VALUES (9944, 'FlowerBed', 0002, 19390, 1806, 0096, 0073, 0000);
INSERT INTO `npcs` VALUES (10306, 'StrangeShadow', 0002, 25030, 1002, 0272, 0188, 0000);
INSERT INTO `npcs` VALUES (751204, 'Beginners Quest', 0002, 65430, 1002, 0284, 0371, 0000);
INSERT INTO `npcs` VALUES (12488, 'Level140', 0002, 16480, 1002, 0280, 0357, 0000);
INSERT INTO `npcs` VALUES (12455, 'Donation', 0002, 34710, 5000, 0313, 0287, 0000);
INSERT INTO `npcs` VALUES (12433, 'PK less', 0002, 29240, 10012, 0325, 0258, 0000);
INSERT INTO `npcs` VALUES (12444, 'Clearinventory', 0002, 41830, 10012, 0307, 0244, 0000);
INSERT INTO `npcs` VALUES (100100, '[Top]ConquerWinn', 0002, 7076, 0010, 0050, 0038, 0000);
INSERT INTO `npcs` VALUES (10100, '[Top] BlackName', 0002, 29440, 10002, 0311, 0289, 0000);
INSERT INTO `npcs` VALUES (9090, '[Top] BlackNameW', 0002, 29440, 0011, 0050, 0038, 0000);
INSERT INTO `npcs` VALUES (8080, '[Top] ChampionRa', 0002, 29440, 10002, 0313, 0289, 0000);
INSERT INTO `npcs` VALUES (10101, '[Top] Conquer', 0002, 7070, 10002, 0309, 0289, 0000);
INSERT INTO `npcs` VALUES (9745679, 'BigMonstor', 0002, 36530, 1002, 0299, 0358, 0000);
INSERT INTO `npcs` VALUES (74114, 'Chi', 0002, 29440, 1002, 0292, 0358, 0000);
INSERT INTO `npcs` VALUES (74113, 'Jiang', 0002, 29440, 1002, 0286, 0358, 0000);
INSERT INTO `npcs` VALUES (18415260, 'OneHit', 0002, 29440, 10002, 0319, 0266, 0000);
INSERT INTO `npcs` VALUES (18415250, 'WinnerOneHit', 0002, 29440, 1811, 0090, 0160, 0000);
INSERT INTO `npcs` VALUES (741230, 'Life~Pk', 0002, 29440, 10002, 0309, 0266, 0000);
INSERT INTO `npcs` VALUES (74130, 'WinnerLifePk', 0002, 29440, 3214, 0050, 0050, 0000);
INSERT INTO `npcs` VALUES (789621, 'BigBoss', 0002, 29440, 1002, 0275, 0357, 0000);
INSERT INTO `npcs` VALUES (789622, 'WinnerBigBoss', 0002, 29440, 1707, 0050, 0050, 0000);
INSERT INTO `npcs` VALUES (333329, 'GentleWar', 0002, 29440, 10002, 0313, 0266, 0000);
INSERT INTO `npcs` VALUES (333328, 'WinnerGentleWar', 0002, 29440, 9391, 0050, 0050, 0000);
INSERT INTO `npcs` VALUES (333377, 'CrazyWar', 0002, 29440, 10002, 0315, 0266, 0000);
INSERT INTO `npcs` VALUES (1841526, 'ButchersWar', 0002, 29440, 10002, 0317, 0266, 0000);
INSERT INTO `npcs` VALUES (1841525, 'WinnerButchersWa', 0002, 29440, 9393, 0050, 0050, 0000);
INSERT INTO `npcs` VALUES (300019, 'out', 0002, 30890, 10002, 0325, 0279, 0000);
INSERT INTO `npcs` VALUES (333378, 'WinnerCrazyWar', 0002, 29440, 9392, 0050, 0050, 0000);
INSERT INTO `npcs` VALUES (4716112, 'Teleport', 0002, 6207, 10012, 0310, 0266, 0000);
INSERT INTO `npcs` VALUES (111184, 'ClassPkEnvoy', 0002, 1360, 20010, 0310, 0142, NULL);
INSERT INTO `npcs` VALUES (111183, 'MountTrainer', 0002, 6327, 20010, 0260, 0286, NULL);
INSERT INTO `npcs` VALUES (111182, '1stFlameStone', 0002, 6650, 20010, 0247, 0212, NULL);
INSERT INTO `npcs` VALUES (111181, '9stFlameStone', 0002, 6570, 20010, 0251, 0212, NULL);
INSERT INTO `npcs` VALUES (111180, 'FlameTaoist', 0002, 6810, 20010, 0249, 0215, NULL);
INSERT INTO `npcs` VALUES (111179, 'Serra Brightspin [Chi]', 0002, 32436, 1002, 0292, 0262, NULL);
INSERT INTO `npcs` VALUES (111178, 'Sam', 0002, 1270, 20010, 0340, 0133, NULL);
INSERT INTO `npcs` VALUES (111177, 'None', 0002, 5510, 20010, 0475, 0572, NULL);
INSERT INTO `npcs` VALUES (111176, 'Assistant', 0002, 1176, 20010, 4294967295, 0295, NULL);
INSERT INTO `npcs` VALUES (111175, 'Pedlar', 0001, 2706, 20010, 0284, 0261, NULL);
INSERT INTO `npcs` VALUES (111174, 'TCCaptain', 0002, 1746, 20010, 0307, 0338, NULL);
INSERT INTO `npcs` VALUES (111173, 'GuildController', 0002, 1156, 20010, 0222, 0237, NULL);
INSERT INTO `npcs` VALUES (111172, 'FortuneTeller', 0002, 6207, 20010, 0209, 0620, NULL);
INSERT INTO `npcs` VALUES (111171, 'FoodCarrier', 0002, 7200, 20010, 4294967295, 0301, NULL);
INSERT INTO `npcs` VALUES (111170, 'John', 0002, 7240, 20010, 0330, 0133, NULL);
INSERT INTO `npcs` VALUES (111169, 'OldGeneral', 0002, 6296, 20010, 0407, 0433, NULL);
INSERT INTO `npcs` VALUES (111168, 'GeneralCai', 0002, 7171, 20010, 0427, 0660, NULL);
INSERT INTO `npcs` VALUES (111167, 'StyleChanger', 0002, 7380, 20010, 0352, 0248, NULL);
INSERT INTO `npcs` VALUES (111166, 'ShopBoy', 0002, 5020, 20010, 0296, 0250, NULL);
INSERT INTO `npcs` VALUES (111165, 'Mr.Leisure', 0002, 7360, 20010, 0264, 4294967295, NULL);
INSERT INTO `npcs` VALUES (111164, 'Wars A', 0002, 35690, 20010, 0313, 0280, NULL);
INSERT INTO `npcs` VALUES (111163, 'None', 0002, 5620, 20010, 0233, 0359, NULL);
INSERT INTO `npcs` VALUES (111162, 'Celestine', 0002, 8350, 20010, 0237, 4294967295, NULL);
INSERT INTO `npcs` VALUES (111161, 'CaptainLu', 0002, 7751, 20010, 0524, 0574, NULL);
INSERT INTO `npcs` VALUES (111160, 'CaptainLi', 0002, 0391, 20010, 0384, 0252, NULL);
INSERT INTO `npcs` VALUES (111159, 'GeneralBravery', 0002, 0310, 20010, 0327, 0192, NULL);
INSERT INTO `npcs` VALUES (111158, 'GeneralPeace', 0002, 0296, 20010, 4294967295, 0363, NULL);
INSERT INTO `npcs` VALUES (111157, 'Conductress', 0029, 0286, 20010, 0307, 0340, NULL);
INSERT INTO `npcs` VALUES (111156, 'SpaceMark', 0002, 0270, 20010, 0667, 0365, NULL);
INSERT INTO `npcs` VALUES (111155, 'SpaceMark', 0002, 0270, 20010, 4294967295, 0223, NULL);
INSERT INTO `npcs` VALUES (111154, 'SpaceMark', 0002, 0270, 20010, 0283, 0604, NULL);
INSERT INTO `npcs` VALUES (111153, 'Pharmacist', 0001, 0030, 1020, 0550, 0542, NULL);
INSERT INTO `npcs` VALUES (111152, 'AreaGuard', 0002, 0260, 20010, 0331, 0191, NULL);
INSERT INTO `npcs` VALUES (111151, 'Beautician', 0005, 0250, 20010, 0331, 0231, NULL);
INSERT INTO `npcs` VALUES (111150, 'GuildDirector', 0002, 0150, 20010, 0270, 0292, NULL);
INSERT INTO `npcs` VALUES (111149, 'Barber', 0002, 0026, 20010, 0284, 0276, NULL);
INSERT INTO `npcs` VALUES (111148, 'WarehouseMan', 0003, 0080, 20010, 0281, 0251, NULL);
INSERT INTO `npcs` VALUES (111147, 'Pharmacist', 0001, 0030, 20010, 0338, 0227, NULL);
INSERT INTO `npcs` VALUES (111146, 'StoreKeeper', 0001, 0010, 20010, 0287, 0251, NULL);
INSERT INTO `npcs` VALUES (111145, 'Blacksmith', 0001, 0050, 20010, 0324, 0230, NULL);
INSERT INTO `npcs` VALUES (111144, 'Armorer', 0001, 0040, 20010, 0284, 0266, NULL);
INSERT INTO `npcs` VALUES (751203, 'Server Guide', 0002, 50046, 1002, 0292, 0279, 0000);
INSERT INTO `npcs` VALUES (111141, 'ClanManager', 0002, 9450, 20010, 0285, 0286, NULL);
INSERT INTO `npcs` VALUES (111140, 'MonthlyPkAward', 0002, 0310, 20010, 0301, 0141, NULL);
INSERT INTO `npcs` VALUES (111139, 'SteedRaceMan', 0002, 5150, 20010, 0294, 0141, NULL);
INSERT INTO `npcs` VALUES (111138, 'PK less', 0002, 0900, 20010, 0285, 0281, NULL);
INSERT INTO `npcs` VALUES (111137, 'TopSpouse', 0002, 6200, 20010, 0296, 0146, NULL);
INSERT INTO `npcs` VALUES (111136, 'spouseaward', 0002, 2920, 1090, 0033, 0045, NULL);
INSERT INTO `npcs` VALUES (111135, 'Huntmaps', 0002, 36520, 20010, 0308, 0344, NULL);
INSERT INTO `npcs` VALUES (111134, 'ClanWar', 0002, 9350, 20010, 0284, 0142, NULL);
INSERT INTO `npcs` VALUES (111133, 'Scent/Fast', 0002, 20880, 20010, 0309, 0246, NULL);
INSERT INTO `npcs` VALUES (111132, 'ClanTwinCity', 0031, 9340, 20010, 0343, 0248, NULL);
INSERT INTO `npcs` VALUES (111131, 'Team Pk', 0002, 9730, 20010, 0312, 0146, NULL);
INSERT INTO `npcs` VALUES (111130, 'Skill Team Pk', 0002, 11220, 20010, 0313, 0141, NULL);
INSERT INTO `npcs` VALUES (111129, 'Zebra', 0002, 6330, 20010, 0260, 0283, NULL);
INSERT INTO `npcs` VALUES (111128, 'CS-CTF-Manager', 0002, 19146, 20010, 0222, 0240, NULL);
INSERT INTO `npcs` VALUES (111127, 'DragonPillQuest', 0002, 8630, 20010, 0517, 0742, 0000);
INSERT INTO `npcs` VALUES (111126, 'Nobility', 0002, 9345, 20010, 0327, 0305, NULL);
INSERT INTO `npcs` VALUES (111125, 'Thalia Emberlace [Jian]', 0002, 32436, 1002, 0292, 0268, NULL);
INSERT INTO `npcs` VALUES (111124, 'SuperGuildWar', 0002, 20880, 20010, 0328, 0246, NULL);
INSERT INTO `npcs` VALUES (111123, 'New Shops', 0002, 50046, 20010, 0292, 0279, NULL);
INSERT INTO `npcs` VALUES (111122, 'New Skills', 0002, 2755, 20010, 0322, 0305, NULL);
INSERT INTO `npcs` VALUES (111121, 'points seller', 0002, 50040, 20010, 0281, 0290, NULL);
INSERT INTO `npcs` VALUES (80800, 'ChampionRace Win', 0002, 7070, 0012, 0050, 0038, 0000);
INSERT INTO `npcs` VALUES (7070, '[Top] RedName', 0002, 29440, 10002, 0315, 0289, 0000);
INSERT INTO `npcs` VALUES (6060, 'Winner', 0002, 29440, 0013, 0050, 0038, 0000);
INSERT INTO `npcs` VALUES (5050, '[Top] DeadWorld', 0002, 29440, 10002, 0317, 0289, 0000);
INSERT INTO `npcs` VALUES (4040, 'Winner', 0002, 29440, 0014, 0050, 0038, 0000);
INSERT INTO `npcs` VALUES (2020, 'Revenger Winner', 0002, 29440, 0015, 0050, 0038, 0000);
INSERT INTO `npcs` VALUES (34430, '[Top] Revenger', 0002, 29440, 10002, 0319, 0289, 0000);
INSERT INTO `npcs` VALUES (300020, 'teleNpc', 0002, 9900, 5000, 0300, 0300, 0000);
INSERT INTO `npcs` VALUES (300021, 'teleNpc2', 0002, 6930, 10012, 0300, 0300, 0000);
INSERT INTO `npcs` VALUES (471612, 'Wardrobe-System', 0002, 35480, 20010, 0300, 0300, 0000);
INSERT INTO `npcs` VALUES (19915, 'Stuff', 0002, 29450, 5000, 0335, 0290, 0000);
INSERT INTO `npcs` VALUES (13700, 'points', 0002, 35480, 5000, 0303, 0303, 0000);
INSERT INTO `npcs` VALUES (5005, 'CelestialSage', 0002, 1314, 1036, 0314, 0240, 0000);
INSERT INTO `npcs` VALUES (9263, 'pointsinner', 0002, 1314, 1036, 0316, 0240, 0000);
INSERT INTO `npcs` VALUES (1191, 'shio', 0001, 1314, 5000, 0296, 0360, 0000);
INSERT INTO `npcs` VALUES (10122, 'innerboxz', 0002, 1314, 0070, 0070, 0306, 0000);
INSERT INTO `npcs` VALUES (75120, 'Jessa Bloomweave [OnlinePoints]', 0002, 35480, 1002, 0292, 0273, 0000);
INSERT INTO `npcs` VALUES (751202, 'Monsters', 0002, 65430, 1002, 0313, 0258, 0000);
INSERT INTO `npcs` VALUES (9687, 'Zara Bloomstitch', 0001, 11286, 1002, 0292, 0288, 0000);
INSERT INTO `npcs` VALUES (9688, 'Maeve Dawnspire', 0001, 8656, 1002, 0292, 0294, 0000);
INSERT INTO `npcs` VALUES (15692, 'Aria Velvetspire', 0001, 35760, 1002, 0322, 0247, 0000);
INSERT INTO `npcs` VALUES (18532, 'Sylvia Clothwing', 0001, 34400, 1002, 0318, 0247, 0000);
INSERT INTO `npcs` VALUES (16922, 'Kael Emberstone', 0001, 34800, 1002, 0326, 0247, 0000);
INSERT INTO `npcs` VALUES (18390, 'Mira Silkwind', 0001, 29200, 1002, 0314, 0247, 0000);

-- ----------------------------
-- Table structure for partners
-- ----------------------------
DROP TABLE IF EXISTS `partners`;
CREATE TABLE `partners`  (
  `EntityID` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `PartnerID` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `PartnerName` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `ProbationStartedOn` bigint(255) NOT NULL DEFAULT 0
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of partners
-- ----------------------------
INSERT INTO `partners` VALUES (1000007, 1000001, 'EgYCo', 636083959028486328);

-- ----------------------------
-- Table structure for pk_explorer
-- ----------------------------
DROP TABLE IF EXISTS `pk_explorer`;
CREATE TABLE `pk_explorer`  (
  `id` int(58) UNSIGNED NOT NULL AUTO_INCREMENT,
  `uid` int(36) UNSIGNED NOT NULL DEFAULT 0,
  `killed_uid` int(36) UNSIGNED NOT NULL DEFAULT 0,
  `killed_name` varchar(100) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL DEFAULT 'None',
  `killed_map` varchar(100) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL DEFAULT 'None',
  `lost_exp` bigint(36) NOT NULL,
  `times` int(36) NOT NULL,
  `battle_power` int(36) NOT NULL,
  `level` int(36) UNSIGNED NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 28781 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pk_explorer
-- ----------------------------
INSERT INTO `pk_explorer` VALUES (28780, 1000010, 1000001, 'EgYCo', 'AFKerJail', 0, 1, 420, 140);

-- ----------------------------
-- Table structure for prizes
-- ----------------------------
DROP TABLE IF EXISTS `prizes`;
CREATE TABLE `prizes`  (
  `UID` int(18) NOT NULL,
  `Name` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Prizes` longblob NULL,
  PRIMARY KEY (`UID`) USING BTREE,
  UNIQUE INDEX `myIndex`(`UID` ASC) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of prizes
-- ----------------------------
INSERT INTO `prizes` VALUES (1000001, 'EgYCo', 0x00);
INSERT INTO `prizes` VALUES (1000009, 'Mid0', 0x00);
INSERT INTO `prizes` VALUES (1000010, 'MyMido', 0x00);
INSERT INTO `prizes` VALUES (1000013, 'RAVEN', 0x00);
INSERT INTO `prizes` VALUES (1000014, 'Alucard', 0x00);

-- ----------------------------
-- Table structure for profs
-- ----------------------------
DROP TABLE IF EXISTS `profs`;
CREATE TABLE `profs`  (
  `EntityID` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  `ID` mediumint(10) UNSIGNED NOT NULL DEFAULT 0,
  `Level` smallint(5) UNSIGNED NOT NULL DEFAULT 0,
  `Experience` bigint(10) UNSIGNED NULL DEFAULT 0,
  `PreviousLevel` smallint(5) UNSIGNED NULL DEFAULT 0,
  `TempLevel` smallint(5) NULL DEFAULT 0
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of profs
-- ----------------------------
INSERT INTO `profs` VALUES (1000010, 611, 20, 0, 0, 0);
INSERT INTO `profs` VALUES (1000010, 612, 20, 0, 0, 0);
INSERT INTO `profs` VALUES (1000010, 613, 20, 0, 0, 0);
INSERT INTO `profs` VALUES (1000010, 614, 20, 0, 0, 0);
INSERT INTO `profs` VALUES (1000010, 615, 20, 0, 0, 0);
INSERT INTO `profs` VALUES (1000010, 616, 20, 0, 0, 0);
INSERT INTO `profs` VALUES (1000010, 617, 20, 0, 0, 0);
INSERT INTO `profs` VALUES (1000010, 618, 20, 0, 0, 0);
INSERT INTO `profs` VALUES (1000010, 619, 20, 0, 0, 0);
INSERT INTO `profs` VALUES (1000010, 620, 20, 0, 0, 0);
INSERT INTO `profs` VALUES (1000010, 621, 20, 0, 0, 0);
INSERT INTO `profs` VALUES (1000010, 622, 20, 0, 0, 0);
INSERT INTO `profs` VALUES (1000010, 623, 20, 0, 0, 0);
INSERT INTO `profs` VALUES (1000010, 624, 20, 1092420192, 0, 0);
INSERT INTO `profs` VALUES (1000013, 0, 2, 11500, 0, 0);
INSERT INTO `profs` VALUES (1000013, 490, 18, 518568175, 0, 0);
INSERT INTO `profs` VALUES (1000014, 601, 2, 45300, 0, 0);
INSERT INTO `profs` VALUES (1000013, 613, 20, 1786512120, 0, 0);

-- ----------------------------
-- Table structure for quests
-- ----------------------------
DROP TABLE IF EXISTS `quests`;
CREATE TABLE `quests`  (
  `UID` int(18) NOT NULL,
  `Name` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `quests` longblob NULL,
  PRIMARY KEY (`UID`) USING BTREE,
  UNIQUE INDEX `myIndex`(`UID` ASC) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of quests
-- ----------------------------
INSERT INTO `quests` VALUES (1000001, 'EgYCo', 0x0200000000000000000000000000000000000000000000000000000000000000000000);
INSERT INTO `quests` VALUES (1000009, 'Mid0', 0x010000000000000000000000000000000000);
INSERT INTO `quests` VALUES (1000010, 'MyMido', 0x0200000000000000000000000000000000000000000000000000000000000000000000);
INSERT INTO `quests` VALUES (1000013, 'RAVEN', 0x070000000000000000000000000000000000D088000001000000000000000100000000D188000001000000000000000100000000D488000001000000000000003300000000DA88000000000000000000000000000000840D0000000000000000000000000000000000000000000000000000000000000000);
INSERT INTO `quests` VALUES (1000014, 'Alucard', 0x0200000000000000000000000000000000000000000000000000000000000000000000);

-- ----------------------------
-- Table structure for rates
-- ----------------------------
DROP TABLE IF EXISTS `rates`;
CREATE TABLE `rates`  (
  `GuildWar` bigint(10) UNSIGNED NULL DEFAULT 0,
  `EliteGw` bigint(10) UNSIGNED NULL DEFAULT 0,
  `TopSpouse` bigint(10) UNSIGNED NULL DEFAULT 0,
  `DailyPk` bigint(10) UNSIGNED NULL DEFAULT 0,
  `LastMan` bigint(10) UNSIGNED NULL DEFAULT 0,
  `Riencration` bigint(10) UNSIGNED NULL DEFAULT 0,
  `kings` bigint(10) UNSIGNED NULL DEFAULT 0,
  `prince` bigint(10) UNSIGNED NULL DEFAULT 0,
  `HousePromete` bigint(10) UNSIGNED NULL DEFAULT 0,
  `HouseUpgrade` bigint(10) UNSIGNED NULL DEFAULT 0,
  `ItemBox` bigint(10) UNSIGNED NULL DEFAULT 0,
  `ServerWebsite` char(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '0',
  `ServerName` char(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '0',
  `LoginSite` char(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '0',
  `MaxCps` bigint(10) UNSIGNED NULL DEFAULT 0,
  `MiniCps` bigint(10) UNSIGNED NULL DEFAULT 0,
  `CpsMethod` char(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT '0',
  `CpsMethodNum` char(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ClassPk` bigint(10) UNSIGNED NULL DEFAULT 0,
  `WeeklyPk` bigint(10) UNSIGNED NULL DEFAULT 0,
  `Coder` char(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '0',
  `CryptKey` char(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '0',
  `ChangeName` int(45) NOT NULL DEFAULT 0,
  `MonthlyPk` int(45) NOT NULL DEFAULT 0,
  `SteedRace` int(45) NOT NULL DEFAULT 0,
  `elitepk` int(45) NOT NULL DEFAULT 0,
  `RemoveBound` int(45) NOT NULL DEFAULT 0,
  `BotJail` int(45) NOT NULL DEFAULT 0,
  `TreasureLow` int(45) NOT NULL DEFAULT 0,
  `TreasureMin` int(45) NOT NULL DEFAULT 0,
  `TreasureMax` int(45) NOT NULL DEFAULT 0,
  `SnowBanshe` int(45) NOT NULL DEFAULT 0,
  `ThrillingSpook` int(45) NOT NULL DEFAULT 0,
  `TeratoDragon` int(45) NOT NULL DEFAULT 0,
  `LevelUp` int(45) NOT NULL DEFAULT 0,
  `DamageGarment` int(45) NOT NULL DEFAULT 0,
  `DamageTails` int(45) NOT NULL DEFAULT 0,
  `SoulP6` int(45) NOT NULL DEFAULT 0,
  `VoteUrl` char(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '0',
  `VotePrize` int(45) NOT NULL DEFAULT 0,
  `Mount` int(45) NOT NULL DEFAULT 0,
  `Steed` int(45) NOT NULL DEFAULT 0,
  `Garment` int(45) NOT NULL DEFAULT 0,
  `Weather` int(45) NOT NULL DEFAULT 0,
  `Night` int(45) NOT NULL DEFAULT 0,
  `SkillTeam` int(45) NOT NULL DEFAULT 0,
  `CaptureFlag` int(45) NOT NULL DEFAULT 0,
  `DemonCave` int(45) NOT NULL DEFAULT 0,
  `PartyDrop` int(45) NOT NULL DEFAULT 0,
  `plus13` int(45) NOT NULL DEFAULT 0,
  `plus14` int(45) NOT NULL DEFAULT 0,
  `plus15` int(45) NOT NULL DEFAULT 0,
  `KoCount` int(45) NOT NULL DEFAULT 0,
  `LastItem` int(45) NOT NULL DEFAULT 0,
  `LastEntity` int(45) NOT NULL DEFAULT 0,
  `Clanwar` int(45) NOT NULL,
  `Treasurebox` int(45) NOT NULL,
  `SSFB` int(45) NOT NULL,
  `NormalQuests` int(45) NOT NULL,
  PRIMARY KEY (`ServerName`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of rates
-- ----------------------------
INSERT INTO `rates` VALUES (15000, 1000, 250, 250, 250, 100, 3, 20, 100, 100, 1000, 'http://tq-91.com/', 'EgYConquer', 'http://tq-91.com/', 5, 1, '-', '20', 500, 250, 'Egy', 'c238xs65pjy7hu9q', 816, 250, 1000, 500, 100, 100, 500, 550, 600, 1000, 1000, 1000, 10000, 10000, 10000, 10000, 'http://www.facebook.com/zyadking.gaming', 500, 675, 1000, 675, 1, 0, 500, 500, 2, 500, 1300000, 7500000, 10000000, 392, 11662, 1000153, 2500, 100, 100, 500);

-- ----------------------------
-- Table structure for refinery
-- ----------------------------
DROP TABLE IF EXISTS `refinery`;
CREATE TABLE `refinery`  (
  `identifier` int(4) NOT NULL DEFAULT 0,
  `refinetype` int(4) NOT NULL,
  `position` int(4) NOT NULL,
  `level` int(2) NOT NULL,
  `percent` int(2) NOT NULL,
  `untradable` int(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`identifier`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of refinery
-- ----------------------------
INSERT INTO `refinery` VALUES (721350, 7, 120, 3, 11, 1);
INSERT INTO `refinery` VALUES (721351, 7, 120, 4, 13, 1);
INSERT INTO `refinery` VALUES (721352, 7, 120, 5, 15, 1);
INSERT INTO `refinery` VALUES (721358, 8, 900, 1, 6, 1);
INSERT INTO `refinery` VALUES (721359, 8, 900, 2, 8, 1);
INSERT INTO `refinery` VALUES (721360, 8, 900, 3, 10, 1);
INSERT INTO `refinery` VALUES (721361, 8, 900, 4, 12, 1);
INSERT INTO `refinery` VALUES (721362, 8, 900, 5, 15, 1);
INSERT INTO `refinery` VALUES (721373, 7, 121, 1, 8, 1);
INSERT INTO `refinery` VALUES (721374, 7, 121, 2, 9, 1);
INSERT INTO `refinery` VALUES (721375, 7, 121, 3, 11, 1);
INSERT INTO `refinery` VALUES (721376, 7, 121, 4, 13, 1);
INSERT INTO `refinery` VALUES (721377, 7, 121, 5, 15, 1);
INSERT INTO `refinery` VALUES (721380, 9, 1, 1, 4, 1);
INSERT INTO `refinery` VALUES (721381, 9, 1, 2, 6, 1);
INSERT INTO `refinery` VALUES (721382, 9, 1, 3, 8, 1);
INSERT INTO `refinery` VALUES (721383, 9, 1, 4, 10, 1);
INSERT INTO `refinery` VALUES (721384, 9, 1, 5, 12, 1);
INSERT INTO `refinery` VALUES (721390, 9, 152, 1, 2, 1);
INSERT INTO `refinery` VALUES (721391, 9, 152, 2, 3, 1);
INSERT INTO `refinery` VALUES (721392, 9, 152, 3, 4, 1);
INSERT INTO `refinery` VALUES (721393, 9, 152, 4, 5, 1);
INSERT INTO `refinery` VALUES (721394, 9, 152, 5, 6, 1);
INSERT INTO `refinery` VALUES (721400, 2, 500, 1, 4, 1);
INSERT INTO `refinery` VALUES (721401, 2, 500, 2, 6, 1);
INSERT INTO `refinery` VALUES (721402, 2, 500, 3, 8, 1);
INSERT INTO `refinery` VALUES (721403, 2, 500, 4, 10, 1);
INSERT INTO `refinery` VALUES (721404, 2, 500, 5, 12, 1);
INSERT INTO `refinery` VALUES (721405, 2, 4, 1, 2, 1);
INSERT INTO `refinery` VALUES (721406, 2, 4, 2, 3, 1);
INSERT INTO `refinery` VALUES (721407, 2, 4, 3, 4, 1);
INSERT INTO `refinery` VALUES (721408, 2, 4, 4, 5, 1);
INSERT INTO `refinery` VALUES (721409, 2, 4, 5, 6, 1);
INSERT INTO `refinery` VALUES (721410, 2, 5, 1, 4, 1);
INSERT INTO `refinery` VALUES (721411, 2, 5, 2, 6, 1);
INSERT INTO `refinery` VALUES (721412, 2, 5, 3, 8, 1);
INSERT INTO `refinery` VALUES (721413, 2, 5, 4, 10, 1);
INSERT INTO `refinery` VALUES (721414, 2, 5, 5, 12, 1);
INSERT INTO `refinery` VALUES (721420, 3, 421, 1, 4, 1);
INSERT INTO `refinery` VALUES (721421, 3, 421, 2, 6, 1);
INSERT INTO `refinery` VALUES (721422, 3, 421, 3, 8, 1);
INSERT INTO `refinery` VALUES (721423, 3, 421, 4, 10, 1);
INSERT INTO `refinery` VALUES (721424, 3, 421, 5, 12, 1);
INSERT INTO `refinery` VALUES (721445, 5, 4, 1, 5, 1);
INSERT INTO `refinery` VALUES (721450, 5, 4, 2, 6, 1);
INSERT INTO `refinery` VALUES (721451, 5, 4, 3, 7, 1);
INSERT INTO `refinery` VALUES (721452, 5, 4, 4, 8, 1);
INSERT INTO `refinery` VALUES (721453, 5, 4, 5, 9, 1);
INSERT INTO `refinery` VALUES (721454, 5, 5, 1, 10, 1);
INSERT INTO `refinery` VALUES (721455, 5, 5, 2, 12, 1);
INSERT INTO `refinery` VALUES (721456, 5, 5, 3, 14, 1);
INSERT INTO `refinery` VALUES (721457, 5, 5, 4, 16, 1);
INSERT INTO `refinery` VALUES (721458, 5, 5, 5, 18, 1);
INSERT INTO `refinery` VALUES (721459, 5, 500, 1, 10, 1);
INSERT INTO `refinery` VALUES (721460, 5, 500, 2, 12, 1);
INSERT INTO `refinery` VALUES (721461, 5, 500, 3, 14, 1);
INSERT INTO `refinery` VALUES (721462, 5, 500, 4, 16, 1);
INSERT INTO `refinery` VALUES (721463, 5, 500, 5, 18, 1);
INSERT INTO `refinery` VALUES (721483, 7, 1, 1, 8, 1);
INSERT INTO `refinery` VALUES (721484, 7, 1, 2, 9, 1);
INSERT INTO `refinery` VALUES (721485, 7, 1, 3, 11, 1);
INSERT INTO `refinery` VALUES (721486, 7, 1, 4, 13, 1);
INSERT INTO `refinery` VALUES (721487, 7, 1, 5, 15, 1);
INSERT INTO `refinery` VALUES (721488, 7, 3, 1, 8, 1);
INSERT INTO `refinery` VALUES (721489, 7, 3, 2, 9, 1);
INSERT INTO `refinery` VALUES (721490, 7, 3, 3, 11, 1);
INSERT INTO `refinery` VALUES (721491, 7, 3, 4, 13, 1);
INSERT INTO `refinery` VALUES (721492, 7, 3, 5, 15, 1);
INSERT INTO `refinery` VALUES (721493, 7, 8, 1, 8, 1);
INSERT INTO `refinery` VALUES (721494, 7, 8, 2, 9, 1);
INSERT INTO `refinery` VALUES (721495, 7, 8, 3, 11, 1);
INSERT INTO `refinery` VALUES (721496, 7, 8, 4, 13, 1);
INSERT INTO `refinery` VALUES (721497, 7, 8, 5, 15, 1);
INSERT INTO `refinery` VALUES (721498, 7, 120, 1, 8, 1);
INSERT INTO `refinery` VALUES (721499, 7, 120, 2, 9, 1);
INSERT INTO `refinery` VALUES (724350, 7, 120, 3, 11, 0);
INSERT INTO `refinery` VALUES (724351, 7, 120, 4, 13, 0);
INSERT INTO `refinery` VALUES (724352, 7, 120, 5, 15, 0);
INSERT INTO `refinery` VALUES (724353, 8, 1, 1, 5, 0);
INSERT INTO `refinery` VALUES (724354, 8, 1, 2, 6, 0);
INSERT INTO `refinery` VALUES (724355, 8, 1, 3, 7, 0);
INSERT INTO `refinery` VALUES (724356, 8, 1, 4, 8, 0);
INSERT INTO `refinery` VALUES (724357, 8, 1, 5, 10, 0);
INSERT INTO `refinery` VALUES (724358, 8, 900, 1, 6, 0);
INSERT INTO `refinery` VALUES (724359, 8, 900, 2, 8, 0);
INSERT INTO `refinery` VALUES (724360, 8, 900, 3, 10, 0);
INSERT INTO `refinery` VALUES (724361, 8, 900, 4, 12, 0);
INSERT INTO `refinery` VALUES (724362, 8, 900, 5, 15, 0);
INSERT INTO `refinery` VALUES (724363, 5, 152, 1, 4, 0);
INSERT INTO `refinery` VALUES (724364, 5, 152, 2, 6, 0);
INSERT INTO `refinery` VALUES (724365, 5, 152, 3, 8, 0);
INSERT INTO `refinery` VALUES (724366, 5, 152, 4, 10, 0);
INSERT INTO `refinery` VALUES (724367, 5, 152, 5, 12, 0);
INSERT INTO `refinery` VALUES (724368, 6, 121, 1, 4, 0);
INSERT INTO `refinery` VALUES (724369, 6, 121, 2, 6, 0);
INSERT INTO `refinery` VALUES (724370, 6, 121, 3, 8, 0);
INSERT INTO `refinery` VALUES (724371, 6, 121, 4, 10, 0);
INSERT INTO `refinery` VALUES (724372, 6, 121, 5, 12, 0);
INSERT INTO `refinery` VALUES (724373, 7, 121, 1, 8, 0);
INSERT INTO `refinery` VALUES (724374, 7, 121, 2, 9, 0);
INSERT INTO `refinery` VALUES (724375, 7, 121, 3, 11, 0);
INSERT INTO `refinery` VALUES (724376, 7, 121, 4, 13, 0);
INSERT INTO `refinery` VALUES (724377, 7, 121, 5, 15, 0);
INSERT INTO `refinery` VALUES (724380, 9, 1, 1, 4, 0);
INSERT INTO `refinery` VALUES (724381, 9, 1, 2, 6, 0);
INSERT INTO `refinery` VALUES (724382, 9, 1, 3, 8, 0);
INSERT INTO `refinery` VALUES (724383, 9, 1, 4, 10, 0);
INSERT INTO `refinery` VALUES (724384, 9, 1, 5, 12, 0);
INSERT INTO `refinery` VALUES (724385, 9, 121, 1, 2, 0);
INSERT INTO `refinery` VALUES (724386, 9, 121, 2, 3, 0);
INSERT INTO `refinery` VALUES (724387, 9, 121, 3, 4, 0);
INSERT INTO `refinery` VALUES (724388, 9, 121, 4, 5, 0);
INSERT INTO `refinery` VALUES (724389, 9, 121, 5, 6, 0);
INSERT INTO `refinery` VALUES (724390, 9, 152, 1, 2, 0);
INSERT INTO `refinery` VALUES (724391, 9, 152, 2, 3, 0);
INSERT INTO `refinery` VALUES (724392, 9, 152, 3, 4, 0);
INSERT INTO `refinery` VALUES (724393, 9, 152, 4, 5, 0);
INSERT INTO `refinery` VALUES (724394, 9, 152, 5, 6, 0);
INSERT INTO `refinery` VALUES (724400, 2, 500, 1, 4, 0);
INSERT INTO `refinery` VALUES (724401, 2, 500, 2, 6, 0);
INSERT INTO `refinery` VALUES (724402, 2, 500, 3, 8, 0);
INSERT INTO `refinery` VALUES (724403, 2, 500, 4, 10, 0);
INSERT INTO `refinery` VALUES (724404, 2, 500, 5, 12, 0);
INSERT INTO `refinery` VALUES (724405, 2, 4, 1, 2, 0);
INSERT INTO `refinery` VALUES (724406, 2, 4, 2, 3, 0);
INSERT INTO `refinery` VALUES (724407, 2, 4, 3, 4, 0);
INSERT INTO `refinery` VALUES (724408, 2, 4, 4, 5, 0);
INSERT INTO `refinery` VALUES (724409, 2, 4, 5, 6, 0);
INSERT INTO `refinery` VALUES (724410, 2, 5, 1, 4, 0);
INSERT INTO `refinery` VALUES (724411, 2, 5, 2, 6, 0);
INSERT INTO `refinery` VALUES (724412, 2, 5, 3, 8, 0);
INSERT INTO `refinery` VALUES (724413, 2, 5, 4, 10, 0);
INSERT INTO `refinery` VALUES (724414, 2, 5, 5, 12, 0);
INSERT INTO `refinery` VALUES (724415, 2, 150, 1, 2, 0);
INSERT INTO `refinery` VALUES (724416, 2, 150, 2, 3, 0);
INSERT INTO `refinery` VALUES (724417, 2, 150, 3, 4, 0);
INSERT INTO `refinery` VALUES (724418, 2, 150, 4, 5, 0);
INSERT INTO `refinery` VALUES (724419, 2, 150, 5, 6, 0);
INSERT INTO `refinery` VALUES (724420, 3, 421, 1, 4, 0);
INSERT INTO `refinery` VALUES (724421, 3, 421, 2, 6, 0);
INSERT INTO `refinery` VALUES (724422, 3, 421, 3, 8, 0);
INSERT INTO `refinery` VALUES (724423, 3, 421, 4, 10, 0);
INSERT INTO `refinery` VALUES (724424, 3, 421, 5, 12, 0);
INSERT INTO `refinery` VALUES (724425, 3, 152, 1, 2, 0);
INSERT INTO `refinery` VALUES (724426, 3, 152, 2, 3, 0);
INSERT INTO `refinery` VALUES (724427, 3, 152, 3, 4, 0);
INSERT INTO `refinery` VALUES (724428, 3, 152, 4, 5, 0);
INSERT INTO `refinery` VALUES (724429, 3, 152, 5, 6, 0);
INSERT INTO `refinery` VALUES (724430, 4, 3, 1, 3, 0);
INSERT INTO `refinery` VALUES (724431, 4, 3, 2, 5, 0);
INSERT INTO `refinery` VALUES (724432, 4, 3, 3, 7, 0);
INSERT INTO `refinery` VALUES (724433, 4, 3, 4, 9, 0);
INSERT INTO `refinery` VALUES (724434, 4, 3, 5, 10, 0);
INSERT INTO `refinery` VALUES (724435, 4, 8, 1, 1, 0);
INSERT INTO `refinery` VALUES (724436, 4, 8, 2, 2, 0);
INSERT INTO `refinery` VALUES (724437, 4, 8, 3, 3, 0);
INSERT INTO `refinery` VALUES (724438, 4, 8, 4, 4, 0);
INSERT INTO `refinery` VALUES (724439, 4, 8, 5, 5, 0);
INSERT INTO `refinery` VALUES (724440, 10, 1, 1, 200, 0);
INSERT INTO `refinery` VALUES (724441, 10, 1, 2, 500, 0);
INSERT INTO `refinery` VALUES (724442, 10, 1, 3, 1000, 0);
INSERT INTO `refinery` VALUES (724443, 10, 1, 4, 1600, 0);
INSERT INTO `refinery` VALUES (724444, 11, 1, 5, 2500, 0);
INSERT INTO `refinery` VALUES (724445, 5, 4, 1, 5, 0);
INSERT INTO `refinery` VALUES (724450, 5, 4, 2, 6, 0);
INSERT INTO `refinery` VALUES (724451, 5, 4, 3, 7, 0);
INSERT INTO `refinery` VALUES (724452, 5, 4, 4, 8, 0);
INSERT INTO `refinery` VALUES (724453, 5, 4, 5, 9, 0);
INSERT INTO `refinery` VALUES (724454, 5, 5, 1, 10, 0);
INSERT INTO `refinery` VALUES (724455, 5, 5, 2, 12, 0);
INSERT INTO `refinery` VALUES (724456, 5, 5, 3, 14, 0);
INSERT INTO `refinery` VALUES (724457, 5, 5, 4, 16, 0);
INSERT INTO `refinery` VALUES (724458, 5, 5, 5, 18, 0);
INSERT INTO `refinery` VALUES (724459, 5, 500, 1, 10, 0);
INSERT INTO `refinery` VALUES (724460, 5, 500, 2, 12, 0);
INSERT INTO `refinery` VALUES (724461, 5, 500, 3, 14, 0);
INSERT INTO `refinery` VALUES (724462, 5, 500, 4, 16, 0);
INSERT INTO `refinery` VALUES (724463, 5, 500, 5, 18, 0);
INSERT INTO `refinery` VALUES (724464, 5, 150, 1, 4, 0);
INSERT INTO `refinery` VALUES (724465, 5, 150, 2, 6, 0);
INSERT INTO `refinery` VALUES (724470, 5, 150, 3, 8, 0);
INSERT INTO `refinery` VALUES (724471, 5, 150, 4, 10, 0);
INSERT INTO `refinery` VALUES (724472, 5, 150, 5, 12, 0);
INSERT INTO `refinery` VALUES (724473, 6, 3, 1, 10, 0);
INSERT INTO `refinery` VALUES (724474, 6, 3, 2, 12, 0);
INSERT INTO `refinery` VALUES (724475, 6, 3, 3, 14, 0);
INSERT INTO `refinery` VALUES (724476, 6, 3, 4, 16, 0);
INSERT INTO `refinery` VALUES (724477, 6, 3, 5, 18, 0);
INSERT INTO `refinery` VALUES (724478, 6, 120, 1, 4, 0);
INSERT INTO `refinery` VALUES (724479, 6, 120, 2, 6, 0);
INSERT INTO `refinery` VALUES (724480, 6, 120, 3, 8, 0);
INSERT INTO `refinery` VALUES (724481, 6, 120, 4, 10, 0);
INSERT INTO `refinery` VALUES (724482, 6, 120, 5, 12, 0);
INSERT INTO `refinery` VALUES (724483, 7, 1, 1, 8, 0);
INSERT INTO `refinery` VALUES (724484, 7, 1, 2, 9, 0);
INSERT INTO `refinery` VALUES (724485, 7, 1, 3, 11, 0);
INSERT INTO `refinery` VALUES (724486, 7, 1, 4, 13, 0);
INSERT INTO `refinery` VALUES (724487, 7, 1, 5, 15, 0);
INSERT INTO `refinery` VALUES (724488, 7, 3, 1, 8, 0);
INSERT INTO `refinery` VALUES (724489, 7, 3, 2, 9, 0);
INSERT INTO `refinery` VALUES (724490, 7, 3, 3, 11, 0);
INSERT INTO `refinery` VALUES (724491, 7, 3, 4, 13, 0);
INSERT INTO `refinery` VALUES (724492, 7, 3, 5, 15, 0);
INSERT INTO `refinery` VALUES (724493, 7, 8, 1, 8, 0);
INSERT INTO `refinery` VALUES (724494, 7, 8, 2, 9, 0);
INSERT INTO `refinery` VALUES (724495, 7, 8, 3, 11, 0);
INSERT INTO `refinery` VALUES (724496, 7, 8, 4, 13, 0);
INSERT INTO `refinery` VALUES (724497, 7, 8, 5, 15, 0);
INSERT INTO `refinery` VALUES (724498, 7, 120, 1, 8, 0);
INSERT INTO `refinery` VALUES (724499, 7, 120, 2, 9, 0);
INSERT INTO `refinery` VALUES (725172, 1, 120, 1, 4, 1);
INSERT INTO `refinery` VALUES (725173, 1, 120, 2, 6, 1);
INSERT INTO `refinery` VALUES (725174, 1, 120, 3, 8, 1);
INSERT INTO `refinery` VALUES (725175, 1, 120, 4, 10, 1);
INSERT INTO `refinery` VALUES (725176, 1, 120, 5, 12, 1);
INSERT INTO `refinery` VALUES (725177, 1, 121, 1, 4, 1);
INSERT INTO `refinery` VALUES (725178, 1, 121, 2, 6, 1);
INSERT INTO `refinery` VALUES (725179, 1, 121, 3, 8, 1);
INSERT INTO `refinery` VALUES (725180, 1, 121, 4, 10, 1);
INSERT INTO `refinery` VALUES (725181, 1, 121, 5, 12, 1);
INSERT INTO `refinery` VALUES (725182, 1, 152, 1, 4, 1);
INSERT INTO `refinery` VALUES (725183, 1, 152, 2, 6, 1);
INSERT INTO `refinery` VALUES (725184, 1, 152, 3, 8, 1);
INSERT INTO `refinery` VALUES (725185, 1, 152, 4, 10, 1);
INSERT INTO `refinery` VALUES (725186, 1, 152, 5, 12, 1);
INSERT INTO `refinery` VALUES (725187, 1, 150, 1, 4, 1);
INSERT INTO `refinery` VALUES (725188, 1, 150, 2, 6, 1);
INSERT INTO `refinery` VALUES (725189, 1, 150, 3, 8, 1);
INSERT INTO `refinery` VALUES (725190, 1, 150, 4, 10, 1);
INSERT INTO `refinery` VALUES (725191, 1, 150, 5, 12, 1);
INSERT INTO `refinery` VALUES (725192, 1, 120, 1, 4, 0);
INSERT INTO `refinery` VALUES (725193, 1, 120, 2, 6, 0);
INSERT INTO `refinery` VALUES (725194, 1, 120, 3, 8, 0);
INSERT INTO `refinery` VALUES (725195, 1, 120, 4, 10, 0);
INSERT INTO `refinery` VALUES (725196, 1, 120, 5, 12, 0);
INSERT INTO `refinery` VALUES (725197, 1, 121, 1, 4, 0);
INSERT INTO `refinery` VALUES (725198, 1, 121, 2, 6, 0);
INSERT INTO `refinery` VALUES (725199, 1, 121, 3, 8, 0);
INSERT INTO `refinery` VALUES (725200, 1, 121, 4, 10, 0);
INSERT INTO `refinery` VALUES (725201, 1, 121, 5, 12, 0);
INSERT INTO `refinery` VALUES (725202, 1, 152, 1, 4, 0);
INSERT INTO `refinery` VALUES (725203, 1, 152, 2, 6, 0);
INSERT INTO `refinery` VALUES (725204, 1, 152, 3, 8, 0);
INSERT INTO `refinery` VALUES (725205, 1, 152, 4, 10, 0);
INSERT INTO `refinery` VALUES (725206, 1, 152, 5, 12, 0);
INSERT INTO `refinery` VALUES (725207, 1, 150, 1, 4, 0);
INSERT INTO `refinery` VALUES (725208, 1, 150, 2, 6, 0);
INSERT INTO `refinery` VALUES (725209, 1, 150, 3, 8, 0);
INSERT INTO `refinery` VALUES (725210, 1, 150, 4, 10, 0);
INSERT INTO `refinery` VALUES (725211, 1, 150, 5, 12, 0);
INSERT INTO `refinery` VALUES (3004136, 1, 120, 6, 12, 1);
INSERT INTO `refinery` VALUES (3004137, 2, 4, 6, 7, 0);
INSERT INTO `refinery` VALUES (3004139, 4, 3, 6, 12, 1);
INSERT INTO `refinery` VALUES (3004140, 11, 1, 6, 3200, 0);
INSERT INTO `refinery` VALUES (3004141, 5, 4, 6, 10, 0);
INSERT INTO `refinery` VALUES (3004142, 6, 3, 6, 20, 1);
INSERT INTO `refinery` VALUES (3004144, 8, 1, 6, 11, 1);
INSERT INTO `refinery` VALUES (3004146, 1, 121, 6, 12, 1);
INSERT INTO `refinery` VALUES (3004149, 4, 8, 6, 6, 1);
INSERT INTO `refinery` VALUES (3004154, 9, 121, 6, 6, 1);
INSERT INTO `refinery` VALUES (3004155, 1, 150, 6, 12, 1);
INSERT INTO `refinery` VALUES (3004157, 1, 152, 6, 12, 1);
INSERT INTO `refinery` VALUES (3004158, 2, 150, 6, 7, 1);
INSERT INTO `refinery` VALUES (3004160, 6, 120, 6, 14, 1);
INSERT INTO `refinery` VALUES (3004163, 5, 150, 6, 16, 1);
INSERT INTO `refinery` VALUES (3004164, 5, 152, 6, 14, 1);
INSERT INTO `refinery` VALUES (3004281, 2, 500, 6, 14, 1);
INSERT INTO `refinery` VALUES (3004282, 2, 4, 6, 10, 1);
INSERT INTO `refinery` VALUES (3004283, 2, 5, 6, 14, 1);
INSERT INTO `refinery` VALUES (3004284, 5, 4, 6, 10, 1);
INSERT INTO `refinery` VALUES (3004285, 5, 5, 6, 20, 1);
INSERT INTO `refinery` VALUES (3004286, 5, 500, 6, 20, 1);
INSERT INTO `refinery` VALUES (3004287, 8, 900, 6, 16, 1);
INSERT INTO `refinery` VALUES (3004288, 7, 1, 6, 17, 1);
INSERT INTO `refinery` VALUES (3004289, 7, 3, 6, 17, 1);
INSERT INTO `refinery` VALUES (3004290, 7, 8, 6, 17, 1);
INSERT INTO `refinery` VALUES (3004291, 7, 120, 6, 17, 1);
INSERT INTO `refinery` VALUES (3004292, 9, 1, 6, 12, 1);
INSERT INTO `refinery` VALUES (3004293, 7, 121, 6, 17, 1);
INSERT INTO `refinery` VALUES (3004294, 9, 152, 6, 6, 1);
INSERT INTO `refinery` VALUES (3004295, 3, 421, 6, 14, 1);
INSERT INTO `refinery` VALUES (3006159, 1, 5, 1, 10, 0);
INSERT INTO `refinery` VALUES (3006160, 2, 5, 2, 11, 0);
INSERT INTO `refinery` VALUES (3006167, 3, 5, 3, 13, 0);
INSERT INTO `refinery` VALUES (3006168, 4, 5, 4, 16, 0);
INSERT INTO `refinery` VALUES (3006169, 5, 5, 5, 18, 0);
INSERT INTO `refinery` VALUES (3006170, 6, 5, 6, 20, 0);

-- ----------------------------
-- Table structure for refineryboxes
-- ----------------------------
DROP TABLE IF EXISTS `refineryboxes`;
CREATE TABLE `refineryboxes`  (
  `Identifier` int(4) NOT NULL DEFAULT 0,
  `Refinetype` int(1) NOT NULL DEFAULT 0,
  `Position` int(2) NOT NULL DEFAULT 0,
  `Untradable` int(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Identifier`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of refineryboxes
-- ----------------------------
INSERT INTO `refineryboxes` VALUES (723130, 9, 152, 1);
INSERT INTO `refineryboxes` VALUES (723131, 9, 152, 1);
INSERT INTO `refineryboxes` VALUES (723132, 9, 152, 1);
INSERT INTO `refineryboxes` VALUES (723133, 3, 421, 1);
INSERT INTO `refineryboxes` VALUES (723134, 3, 421, 1);
INSERT INTO `refineryboxes` VALUES (723135, 3, 421, 1);
INSERT INTO `refineryboxes` VALUES (723651, 2, 500, 1);
INSERT INTO `refineryboxes` VALUES (723652, 2, 500, 1);
INSERT INTO `refineryboxes` VALUES (723653, 2, 500, 1);
INSERT INTO `refineryboxes` VALUES (723654, 2, 4, 1);
INSERT INTO `refineryboxes` VALUES (723655, 2, 4, 1);
INSERT INTO `refineryboxes` VALUES (723656, 2, 4, 1);
INSERT INTO `refineryboxes` VALUES (723657, 2, 5, 1);
INSERT INTO `refineryboxes` VALUES (723658, 2, 5, 1);
INSERT INTO `refineryboxes` VALUES (723659, 2, 5, 1);
INSERT INTO `refineryboxes` VALUES (723660, 5, 4, 1);
INSERT INTO `refineryboxes` VALUES (723661, 5, 4, 1);
INSERT INTO `refineryboxes` VALUES (723662, 5, 4, 1);
INSERT INTO `refineryboxes` VALUES (723663, 5, 5, 1);
INSERT INTO `refineryboxes` VALUES (723664, 5, 5, 1);
INSERT INTO `refineryboxes` VALUES (723665, 5, 5, 1);
INSERT INTO `refineryboxes` VALUES (723666, 5, 500, 1);
INSERT INTO `refineryboxes` VALUES (723667, 5, 500, 1);
INSERT INTO `refineryboxes` VALUES (723668, 5, 500, 1);
INSERT INTO `refineryboxes` VALUES (723669, 8, 900, 1);
INSERT INTO `refineryboxes` VALUES (723670, 8, 900, 1);
INSERT INTO `refineryboxes` VALUES (723671, 8, 900, 1);
INSERT INTO `refineryboxes` VALUES (723672, 7, 1, 1);
INSERT INTO `refineryboxes` VALUES (723673, 7, 1, 1);
INSERT INTO `refineryboxes` VALUES (723674, 7, 1, 1);
INSERT INTO `refineryboxes` VALUES (723675, 7, 3, 1);
INSERT INTO `refineryboxes` VALUES (723676, 7, 3, 1);
INSERT INTO `refineryboxes` VALUES (723677, 7, 3, 1);
INSERT INTO `refineryboxes` VALUES (723678, 7, 8, 1);
INSERT INTO `refineryboxes` VALUES (723679, 7, 8, 1);
INSERT INTO `refineryboxes` VALUES (723680, 7, 8, 1);
INSERT INTO `refineryboxes` VALUES (723681, 7, 120, 1);
INSERT INTO `refineryboxes` VALUES (723682, 7, 120, 1);
INSERT INTO `refineryboxes` VALUES (723683, 7, 120, 1);
INSERT INTO `refineryboxes` VALUES (723684, 9, 1, 1);
INSERT INTO `refineryboxes` VALUES (723685, 9, 1, 1);
INSERT INTO `refineryboxes` VALUES (723686, 9, 1, 1);
INSERT INTO `refineryboxes` VALUES (723690, 7, 121, 1);
INSERT INTO `refineryboxes` VALUES (723691, 7, 121, 1);
INSERT INTO `refineryboxes` VALUES (723692, 7, 121, 1);
INSERT INTO `refineryboxes` VALUES (724130, 9, 152, 1);
INSERT INTO `refineryboxes` VALUES (724131, 9, 152, 1);
INSERT INTO `refineryboxes` VALUES (724132, 9, 152, 1);
INSERT INTO `refineryboxes` VALUES (724133, 3, 421, 1);
INSERT INTO `refineryboxes` VALUES (724134, 3, 421, 1);
INSERT INTO `refineryboxes` VALUES (724135, 3, 421, 1);
INSERT INTO `refineryboxes` VALUES (724136, 4, 3, 1);
INSERT INTO `refineryboxes` VALUES (724137, 4, 3, 1);
INSERT INTO `refineryboxes` VALUES (724138, 4, 3, 1);
INSERT INTO `refineryboxes` VALUES (724139, 10, 1, 1);
INSERT INTO `refineryboxes` VALUES (724140, 10, 1, 1);
INSERT INTO `refineryboxes` VALUES (724141, 10, 1, 1);
INSERT INTO `refineryboxes` VALUES (724142, 6, 3, 1);
INSERT INTO `refineryboxes` VALUES (724143, 6, 3, 1);
INSERT INTO `refineryboxes` VALUES (724144, 6, 3, 1);
INSERT INTO `refineryboxes` VALUES (724145, 8, 1, 1);
INSERT INTO `refineryboxes` VALUES (724146, 8, 1, 1);
INSERT INTO `refineryboxes` VALUES (724147, 8, 1, 1);
INSERT INTO `refineryboxes` VALUES (724148, 9, 121, 1);
INSERT INTO `refineryboxes` VALUES (724149, 9, 121, 1);
INSERT INTO `refineryboxes` VALUES (724150, 9, 121, 1);
INSERT INTO `refineryboxes` VALUES (724151, 2, 500, 1);
INSERT INTO `refineryboxes` VALUES (724152, 2, 500, 1);
INSERT INTO `refineryboxes` VALUES (724153, 2, 500, 1);
INSERT INTO `refineryboxes` VALUES (724154, 2, 4, 1);
INSERT INTO `refineryboxes` VALUES (724155, 2, 4, 1);
INSERT INTO `refineryboxes` VALUES (724156, 2, 4, 1);
INSERT INTO `refineryboxes` VALUES (724157, 2, 5, 1);
INSERT INTO `refineryboxes` VALUES (724158, 2, 5, 1);
INSERT INTO `refineryboxes` VALUES (724159, 2, 5, 1);
INSERT INTO `refineryboxes` VALUES (724160, 5, 4, 1);
INSERT INTO `refineryboxes` VALUES (724161, 5, 4, 1);
INSERT INTO `refineryboxes` VALUES (724162, 5, 4, 1);
INSERT INTO `refineryboxes` VALUES (724163, 5, 5, 1);
INSERT INTO `refineryboxes` VALUES (724164, 5, 5, 1);
INSERT INTO `refineryboxes` VALUES (724165, 5, 5, 1);
INSERT INTO `refineryboxes` VALUES (724166, 5, 500, 1);
INSERT INTO `refineryboxes` VALUES (724167, 5, 500, 1);
INSERT INTO `refineryboxes` VALUES (724168, 5, 500, 1);
INSERT INTO `refineryboxes` VALUES (724169, 8, 900, 1);
INSERT INTO `refineryboxes` VALUES (724170, 8, 900, 1);
INSERT INTO `refineryboxes` VALUES (724171, 8, 900, 1);
INSERT INTO `refineryboxes` VALUES (724172, 7, 1, 1);
INSERT INTO `refineryboxes` VALUES (724173, 7, 1, 1);
INSERT INTO `refineryboxes` VALUES (724174, 7, 1, 1);
INSERT INTO `refineryboxes` VALUES (724175, 7, 3, 1);
INSERT INTO `refineryboxes` VALUES (724176, 7, 3, 1);
INSERT INTO `refineryboxes` VALUES (724177, 7, 3, 1);
INSERT INTO `refineryboxes` VALUES (724178, 7, 8, 1);
INSERT INTO `refineryboxes` VALUES (724179, 7, 8, 1);
INSERT INTO `refineryboxes` VALUES (724180, 7, 8, 1);
INSERT INTO `refineryboxes` VALUES (724181, 7, 120, 1);
INSERT INTO `refineryboxes` VALUES (724182, 7, 120, 1);
INSERT INTO `refineryboxes` VALUES (724183, 7, 120, 1);
INSERT INTO `refineryboxes` VALUES (724184, 9, 1, 1);
INSERT INTO `refineryboxes` VALUES (724185, 9, 1, 1);
INSERT INTO `refineryboxes` VALUES (724186, 9, 1, 1);
INSERT INTO `refineryboxes` VALUES (724190, 7, 121, 1);
INSERT INTO `refineryboxes` VALUES (724191, 7, 121, 1);
INSERT INTO `refineryboxes` VALUES (724192, 7, 121, 1);
INSERT INTO `refineryboxes` VALUES (724193, 2, 150, 1);
INSERT INTO `refineryboxes` VALUES (724194, 2, 150, 1);
INSERT INTO `refineryboxes` VALUES (724195, 3, 152, 1);
INSERT INTO `refineryboxes` VALUES (724196, 3, 152, 1);
INSERT INTO `refineryboxes` VALUES (724197, 3, 152, 1);
INSERT INTO `refineryboxes` VALUES (724198, 5, 150, 1);
INSERT INTO `refineryboxes` VALUES (724199, 5, 150, 1);
INSERT INTO `refineryboxes` VALUES (724200, 5, 150, 1);
INSERT INTO `refineryboxes` VALUES (724201, 5, 152, 1);
INSERT INTO `refineryboxes` VALUES (724202, 5, 152, 1);
INSERT INTO `refineryboxes` VALUES (724203, 5, 152, 1);
INSERT INTO `refineryboxes` VALUES (724204, 6, 120, 1);
INSERT INTO `refineryboxes` VALUES (724205, 6, 120, 1);
INSERT INTO `refineryboxes` VALUES (724207, 6, 121, 1);
INSERT INTO `refineryboxes` VALUES (724208, 6, 121, 1);
INSERT INTO `refineryboxes` VALUES (724210, 4, 8, 1);
INSERT INTO `refineryboxes` VALUES (724211, 4, 8, 1);
INSERT INTO `refineryboxes` VALUES (724212, 4, 8, 1);
INSERT INTO `refineryboxes` VALUES (724215, 2, 150, 1);
INSERT INTO `refineryboxes` VALUES (725055, 2, 150, 1);
INSERT INTO `refineryboxes` VALUES (725056, 3, 152, 1);
INSERT INTO `refineryboxes` VALUES (725057, 5, 150, 1);
INSERT INTO `refineryboxes` VALUES (725058, 5, 152, 1);
INSERT INTO `refineryboxes` VALUES (725160, 1, 120, 1);
INSERT INTO `refineryboxes` VALUES (725161, 1, 120, 1);
INSERT INTO `refineryboxes` VALUES (725162, 1, 120, 1);
INSERT INTO `refineryboxes` VALUES (725163, 1, 121, 1);
INSERT INTO `refineryboxes` VALUES (725164, 1, 121, 1);
INSERT INTO `refineryboxes` VALUES (725165, 1, 121, 1);
INSERT INTO `refineryboxes` VALUES (725166, 1, 152, 1);
INSERT INTO `refineryboxes` VALUES (725167, 1, 152, 1);
INSERT INTO `refineryboxes` VALUES (725168, 1, 152, 1);
INSERT INTO `refineryboxes` VALUES (725169, 1, 150, 0);
INSERT INTO `refineryboxes` VALUES (725170, 1, 150, 1);
INSERT INTO `refineryboxes` VALUES (725171, 1, 150, 1);
INSERT INTO `refineryboxes` VALUES (3006316, 1, 5, 1);
INSERT INTO `refineryboxes` VALUES (3006317, 2, 5, 1);
INSERT INTO `refineryboxes` VALUES (3006318, 3, 5, 1);
INSERT INTO `refineryboxes` VALUES (3006319, 4, 5, 1);
INSERT INTO `refineryboxes` VALUES (3006320, 5, 5, 1);
INSERT INTO `refineryboxes` VALUES (3006321, 6, 5, 1);

-- ----------------------------
-- Table structure for reincarnation
-- ----------------------------
DROP TABLE IF EXISTS `reincarnation`;
CREATE TABLE `reincarnation`  (
  `id` int(36) UNSIGNED NOT NULL AUTO_INCREMENT,
  `uid` int(36) UNSIGNED NOT NULL DEFAULT 0,
  `level` int(36) UNSIGNED NOT NULL DEFAULT 0,
  `experience` int(36) UNSIGNED NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 30131 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of reincarnation
-- ----------------------------
INSERT INTO `reincarnation` VALUES (30128, 1000001, 255, 0);
INSERT INTO `reincarnation` VALUES (30130, 1000013, 140, 0);

-- ----------------------------
-- Table structure for servergift
-- ----------------------------
DROP TABLE IF EXISTS `servergift`;
CREATE TABLE `servergift`  (
  `UID` varchar(255) CHARACTER SET utf16 COLLATE utf16_general_ci NOT NULL,
  `Username` varchar(255) CHARACTER SET utf16 COLLATE utf16_general_ci NOT NULL,
  `Claimed` int(11) NULL DEFAULT NULL,
  `MacAddress` varchar(255) CHARACTER SET utf16 COLLATE utf16_general_ci NOT NULL,
  `RegistrationTime` datetime NULL DEFAULT NULL,
  PRIMARY KEY (`UID`, `Username`, `MacAddress`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf16 COLLATE = utf16_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of servergift
-- ----------------------------
INSERT INTO `servergift` VALUES ('PCI\\VEN:8086&DEV:9BC4&SUBSYS:50F31558&REV:05\\3&11583659&0&10', 'RAVEN', 0, '', '2025-03-04 02:48:59');

-- ----------------------------
-- Table structure for skills
-- ----------------------------
DROP TABLE IF EXISTS `skills`;
CREATE TABLE `skills`  (
  `EntityID` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  `ID` mediumint(10) UNSIGNED NOT NULL DEFAULT 0,
  `Level` smallint(5) UNSIGNED NOT NULL DEFAULT 0,
  `Experience` bigint(10) UNSIGNED NULL DEFAULT 0,
  `PreviousLevel` smallint(5) UNSIGNED NULL DEFAULT 0,
  `TempLevel` smallint(5) NULL DEFAULT 0,
  `LevelHu` smallint(5) NOT NULL DEFAULT 0,
  PRIMARY KEY (`EntityID`, `ID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of skills
-- ----------------------------
INSERT INTO `skills` VALUES (1000007, 10490, 4, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000007, 1045, 2, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000007, 10423, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000008, 1270, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000007, 7001, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 1040, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 1025, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 1015, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 1051, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 1020, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000002, 1380, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000008, 1115, 4, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000002, 6011, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 9876, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000008, 1110, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 12091, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000008, 1190, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000007, 10424, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 6001, 4, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 12089, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000002, 12090, 4, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000002, 6003, 0, 0, 2, 0, 0);
INSERT INTO `skills` VALUES (1000002, 7001, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 7001, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 7002, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 7003, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000002, 3321, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000002, 11170, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000002, 11180, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000002, 12080, 1, 616500, 0, 0, 0);
INSERT INTO `skills` VALUES (1000002, 11230, 3, 0, 1, 0, 0);
INSERT INTO `skills` VALUES (1000002, 6001, 4, 0, 1, 0, 0);
INSERT INTO `skills` VALUES (1000002, 6010, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000002, 6004, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000002, 9876, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000002, 6002, 2, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000004, 12240, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000004, 12220, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000004, 12210, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000004, 12290, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000005, 12240, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000005, 12220, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000005, 12210, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000005, 12290, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 3321, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 12370, 6, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 12400, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 12087, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 12086, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 12085, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 12095, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 12100, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 12101, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 12102, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 12103, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 12081, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 12082, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 12083, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 12084, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 12088, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000007, 9876, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000008, 3321, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000007, 10415, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 6010, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 12080, 4, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 11230, 4, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000007, 1000, 4, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000007, 10420, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000008, 1015, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000008, 3320, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 6011, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 11180, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 6004, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 12090, 5, 50000, 0, 0, 0);
INSERT INTO `skills` VALUES (1000007, 10405, 4, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 11170, 2, 319500, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 12380, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000007, 10430, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000007, 12370, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000007, 10425, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000007, 10421, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 12390, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000007, 10422, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000007, 10381, 4, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000007, 10400, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000007, 10410, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000007, 10390, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000007, 10395, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000009, 12240, 9, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000009, 12220, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000009, 12210, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000009, 12290, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000009, 12320, 2, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000009, 12330, 1, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000009, 12340, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000009, 12270, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000009, 12120, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000009, 12130, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000009, 12140, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000001, 6002, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000009, 12200, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000009, 12280, 2, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000009, 12160, 0, 10000, 0, 0, 0);
INSERT INTO `skills` VALUES (1000009, 12350, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000009, 12170, 0, 10000, 0, 0, 0);
INSERT INTO `skills` VALUES (1000009, 1380, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 10311, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 1045, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 1046, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12670, 9, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12700, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12690, 6, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12660, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 11200, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 10470, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 11160, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12671, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12672, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12673, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12674, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12675, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12676, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12677, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12678, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12679, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12680, 6, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12681, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12682, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12683, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12684, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12688, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12685, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12686, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12687, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12689, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12691, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12692, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12693, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12694, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12695, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12696, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12697, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12770, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12772, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12771, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12773, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12774, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12775, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12776, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12777, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 12778, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 4000, 1, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 4070, 1, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000010, 7001, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000013, 3321, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000013, 8003, 1, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000013, 9000, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000013, 8002, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000013, 4000, 3, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000013, 8030, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000013, 1040, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000014, 6011, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000013, 11610, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000013, 11600, 0, 300, 0, 0, 0);
INSERT INTO `skills` VALUES (1000013, 7001, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000013, 11670, 1, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000013, 3060, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000013, 11620, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000013, 11650, 4, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000013, 11660, 4, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000013, 8000, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000013, 8001, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000013, 9876, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000013, 1290, 9, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000013, 11590, 4, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000013, 4060, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000013, 4050, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000013, 4070, 1, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000013, 4010, 0, 0, 0, 0, 0);
INSERT INTO `skills` VALUES (1000013, 4020, 0, 0, 0, 0, 0);

-- ----------------------------
-- Table structure for skillteampk
-- ----------------------------
DROP TABLE IF EXISTS `skillteampk`;
CREATE TABLE `skillteampk`  (
  `UID` int(32) NOT NULL DEFAULT 0,
  `Avatar` int(32) NOT NULL DEFAULT 0,
  `Name` char(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `Mesh` int(32) NOT NULL DEFAULT 0,
  `Points` int(32) NOT NULL DEFAULT 0,
  `Postion` int(32) NOT NULL DEFAULT 0,
  `MyTitle` bigint(32) NOT NULL DEFAULT 0,
  PRIMARY KEY (`UID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of skillteampk
-- ----------------------------

-- ----------------------------
-- Table structure for sobnpcs
-- ----------------------------
DROP TABLE IF EXISTS `sobnpcs`;
CREATE TABLE `sobnpcs`  (
  `id` int(4) UNSIGNED ZEROFILL NOT NULL AUTO_INCREMENT,
  `name` varchar(16) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `type` smallint(2) UNSIGNED ZEROFILL NULL DEFAULT 00,
  `lookface` int(4) UNSIGNED ZEROFILL NULL DEFAULT 0000,
  `mapid` int(4) UNSIGNED ZEROFILL NULL DEFAULT 0000,
  `cellx` smallint(2) UNSIGNED ZEROFILL NULL DEFAULT 00,
  `celly` smallint(2) UNSIGNED ZEROFILL NULL DEFAULT 00,
  `life` int(4) UNSIGNED ZEROFILL NULL DEFAULT 0000,
  `maxlife` int(4) UNSIGNED ZEROFILL NULL DEFAULT 0000,
  `base` int(4) UNSIGNED ZEROFILL NULL DEFAULT 0000,
  `sort` smallint(2) UNSIGNED ZEROFILL NULL DEFAULT 00,
  `showname` smallint(2) UNSIGNED NULL DEFAULT 0,
  `prize` smallint(1) UNSIGNED NOT NULL DEFAULT 0,
  `effect` varchar(16) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 5160741 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sobnpcs
-- ----------------------------
INSERT INTO `sobnpcs` VALUES (0810, 'Prodgy', 10, 1137, 1038, 84, 99, 0001, 0001, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (516074, 'ii', 26, 0251, 1038, 163, 210, 20000000, 20000000, 0024, 21, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (516075, 'ii', 26, 0277, 1038, 223, 177, 20000000, 20000000, 0027, 21, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (0182, 'BoxerQui', 02, 6447, 1011, 207, 256, 0000, 0000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (0183, 'BoxerChang', 02, 6447, 1020, 553, 559, 0000, 0000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (0184, 'BoxerWu', 02, 6440, 1000, 480, 631, 0000, 0000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (0185, 'BoxerWong', 02, 6447, 1015, 715, 562, 0000, 0000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (1080, 'ii', 10, 8910, 1080, 130, 131, 40000000, 40000000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (0812, 'ii', 21, 8680, 1509, 102, 109, 38200017, 40000000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (101991, 'FlagBase', 21, 8686, 2057, 326, 447, 20000000, 20000000, 0000, 21, 1, 0, '');
INSERT INTO `sobnpcs` VALUES (101992, 'FlagBase', 21, 8686, 2057, 214, 269, 20000000, 20000000, 0000, 21, 1, 0, '');
INSERT INTO `sobnpcs` VALUES (101993, 'FlagBase', 21, 8686, 2057, 207, 98, 20000000, 20000000, 0000, 21, 1, 0, '');
INSERT INTO `sobnpcs` VALUES (0854, 'ii', 10, 1137, 2578, 125, 124, 40000000, 40000000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (0813, 'ii', 10, 1137, 2072, 310, 277, 30000000, 30000000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (516076, 'ii', 26, 0251, 2072, 309, 326, 10000000, 10000000, 0024, 21, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (516077, 'ii', 26, 0277, 2072, 375, 254, 10000000, 10000000, 0027, 21, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (0814, 'ii', 10, 1137, 2073, 210, 261, 30000000, 30000000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (516078, 'ii', 26, 0251, 2073, 214, 307, 10000000, 10000000, 0024, 21, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (516079, 'ii', 26, 0277, 2073, 254, 264, 10000000, 10000000, 0027, 21, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (516080, 'ii', 26, 0251, 2074, 566, 612, 10000000, 10000000, 0024, 21, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (516081, 'ii', 26, 0277, 2074, 587, 584, 10000000, 10000000, 0027, 21, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (0815, 'ii', 10, 1137, 2074, 567, 564, 30000000, 30000000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (516082, 'ii', 26, 0251, 2075, 735, 617, 10000000, 10000000, 0024, 21, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (516083, 'ii', 26, 0277, 2075, 781, 569, 10000000, 10000000, 0027, 21, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (0816, 'ii', 10, 1137, 2075, 724, 573, 30000000, 30000000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (0811, 'ii', 10, 1137, 2071, 125, 124, 30000000, 30000000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5461, 'Lv125Scarecrow', 22, 1520, 1039, 479, 271, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5460, 'Lv125Scarecrow', 22, 1520, 1039, 475, 275, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5459, 'Lv125Scarecrow', 22, 1520, 1039, 471, 279, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5458, 'Lv125Scarecrow', 22, 1520, 1039, 467, 283, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5457, 'Lv125Scarecrow', 22, 1520, 1039, 463, 287, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5456, 'Lv125Scarecrow', 22, 1520, 1039, 459, 291, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5455, 'Lv125Scarecrow', 22, 1520, 1039, 455, 295, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5454, 'Lv125Scarecrow', 22, 1520, 1039, 447, 303, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5453, 'Lv125Scarecrow', 22, 1520, 1039, 443, 307, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5452, 'Lv125Scarecrow', 22, 1520, 1039, 439, 311, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5451, 'Lv125Scarecrow', 22, 1520, 1039, 435, 315, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5450, 'Lv125Scarecrow', 22, 1520, 1039, 431, 319, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5449, 'Lv125Scarecrow', 22, 1520, 1039, 427, 323, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5448, 'Lv125Scarecrow', 22, 1520, 1039, 423, 327, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5447, 'Lv125Scarecrow', 22, 1520, 1039, 415, 335, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5445, 'Lv125Scarecrow', 22, 1520, 1039, 407, 343, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5446, 'Lv125Scarecrow', 22, 1520, 1039, 411, 339, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5444, 'Lv125Scarecrow', 22, 1520, 1039, 403, 347, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5442, 'Lv125Scarecrow', 22, 1520, 1039, 395, 355, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5443, 'Lv125Scarecrow', 22, 1520, 1039, 399, 351, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5441, 'Lv125Scarecrow', 22, 1520, 1039, 391, 359, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5439, 'Lv120Scarecrow', 22, 1030, 1039, 359, 391, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5440, 'Lv120Scarecrow', 22, 1030, 1039, 363, 387, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5438, 'Lv120Scarecrow', 22, 1030, 1039, 355, 395, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5436, 'Lv120Scarecrow', 22, 1030, 1039, 347, 403, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5437, 'Lv120Scarecrow', 22, 1030, 1039, 351, 399, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5435, 'Lv120Scarecrow', 22, 1030, 1039, 343, 407, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5433, 'Lv120Scarecrow', 22, 1030, 1039, 331, 419, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5434, 'Lv120Scarecrow', 22, 1030, 1039, 339, 411, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5002, 'Lv20Scarecrow', 22, 0430, 1039, 18, 202, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5003, 'Lv20Scarecrow', 22, 0430, 1039, 22, 198, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5004, 'Lv20Scarecrow', 22, 0430, 1039, 26, 194, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5005, 'Lv20Scarecrow', 22, 0430, 1039, 30, 190, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5006, 'Lv20Scarecrow', 22, 0430, 1039, 34, 186, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5007, 'Lv20Scarecrow', 22, 0430, 1039, 42, 178, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5008, 'Lv20Scarecrow', 22, 0430, 1039, 46, 174, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5432, 'Lv120Scarecrow', 22, 1030, 1039, 327, 423, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5431, 'Lv120Scarecrow', 22, 1030, 1039, 323, 427, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5430, 'Lv120Scarecrow', 22, 1030, 1039, 319, 431, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5429, 'Lv120Scarecrow', 22, 1030, 1039, 315, 435, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5428, 'Lv120Scarecrow', 22, 1030, 1039, 311, 439, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5427, 'Lv120Scarecrow', 22, 1030, 1039, 307, 443, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5426, 'Lv120Scarecrow', 22, 1030, 1039, 299, 451, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5425, 'Lv120Scarecrow', 22, 1030, 1039, 295, 455, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5424, 'Lv120Scarecrow', 22, 1030, 1039, 291, 459, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5423, 'Lv120Scarecrow', 22, 1030, 1039, 287, 463, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5422, 'Lv120Scarecrow', 22, 1030, 1039, 283, 467, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5421, 'Lv120Scarecrow', 22, 1030, 1039, 279, 471, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5420, 'Lv120Scarecrow', 22, 1030, 1039, 275, 475, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5419, 'Lv115Scarecrow', 22, 1000, 1039, 454, 246, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5418, 'Lv115Scarecrow', 22, 1000, 1039, 450, 250, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5417, 'Lv115Scarecrow', 22, 1000, 1039, 446, 254, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5416, 'Lv115Scarecrow', 22, 1000, 1039, 442, 258, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5415, 'Lv115Scarecrow', 22, 1000, 1039, 438, 262, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5414, 'Lv115Scarecrow', 22, 1000, 1039, 434, 266, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5413, 'Lv115Scarecrow', 22, 1000, 1039, 430, 270, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5412, 'Lv115Scarecrow', 22, 1000, 1039, 422, 278, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5411, 'Lv115Scarecrow', 22, 1000, 1039, 418, 282, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5410, 'Lv115Scarecrow', 22, 1000, 1039, 414, 286, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5409, 'Lv115Scarecrow', 22, 1000, 1039, 410, 290, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5408, 'Lv115Scarecrow', 22, 1000, 1039, 406, 294, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5407, 'Lv115Scarecrow', 22, 1000, 1039, 402, 298, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5406, 'Lv115Scarecrow', 22, 1000, 1039, 398, 302, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5405, 'Lv115Scarecrow', 22, 1000, 1039, 390, 310, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5404, 'Lv115Scarecrow', 22, 1000, 1039, 386, 314, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5403, 'Lv115Scarecrow', 22, 1000, 1039, 382, 318, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5402, 'Lv115Scarecrow', 22, 1000, 1039, 378, 322, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5401, 'Lv115Scarecrow', 22, 1000, 1039, 374, 326, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5400, 'Lv115Scarecrow', 22, 1000, 1039, 370, 330, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5399, 'Lv115Scarecrow', 22, 1000, 1039, 366, 334, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5398, 'Lv110Scarecrow', 22, 0970, 1039, 338, 362, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5397, 'Lv110Scarecrow', 22, 0970, 1039, 334, 366, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5396, 'Lv110Scarecrow', 22, 0970, 1039, 330, 370, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5395, 'Lv110Scarecrow', 22, 0970, 1039, 326, 374, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5394, 'Lv110Scarecrow', 22, 0970, 1039, 322, 378, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5393, 'Lv110Scarecrow', 22, 0970, 1039, 318, 382, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5392, 'Lv110Scarecrow', 22, 0970, 1039, 314, 386, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5391, 'Lv110Scarecrow', 22, 0970, 1039, 306, 394, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5390, 'Lv110Scarecrow', 22, 0970, 1039, 302, 398, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5389, 'Lv110Scarecrow', 22, 0970, 1039, 298, 402, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5388, 'Lv110Scarecrow', 22, 0970, 1039, 294, 406, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5387, 'Lv110Scarecrow', 22, 0970, 1039, 290, 410, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5386, 'Lv110Scarecrow', 22, 0970, 1039, 286, 414, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5385, 'Lv110Scarecrow', 22, 0970, 1039, 282, 418, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5384, 'Lv110Scarecrow', 22, 0970, 1039, 274, 426, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5383, 'Lv110Scarecrow', 22, 0970, 1039, 270, 430, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5382, 'Lv110Scarecrow', 22, 0970, 1039, 266, 434, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5381, 'Lv110Scarecrow', 22, 0970, 1039, 262, 438, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5380, 'Lv110Scarecrow', 22, 0970, 1039, 258, 442, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5379, 'Lv110Scarecrow', 22, 0970, 1039, 254, 446, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5378, 'Lv110Scarecrow', 22, 0970, 1039, 250, 450, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5377, 'Lv105Scarecrow', 22, 0940, 1039, 429, 221, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5376, 'Lv105Scarecrow', 22, 0940, 1039, 425, 225, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5375, 'Lv105Scarecrow', 22, 0940, 1039, 421, 229, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5374, 'Lv105Scarecrow', 22, 0940, 1039, 417, 233, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5373, 'Lv105Scarecrow', 22, 0940, 1039, 413, 237, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5372, 'Lv105Scarecrow', 22, 0940, 1039, 409, 241, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5371, 'Lv105Scarecrow', 22, 0940, 1039, 405, 245, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5370, 'Lv105Scarecrow', 22, 0940, 1039, 397, 253, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5369, 'Lv105Scarecrow', 22, 0940, 1039, 393, 257, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5368, 'Lv105Scarecrow', 22, 0940, 1039, 389, 261, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5367, 'Lv105Scarecrow', 22, 0940, 1039, 385, 265, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5366, 'Lv105Scarecrow', 22, 0940, 1039, 381, 269, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5365, 'Lv105Scarecrow', 22, 0940, 1039, 377, 273, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5364, 'Lv105Scarecrow', 22, 0940, 1039, 373, 277, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5363, 'Lv105Scarecrow', 22, 0940, 1039, 365, 285, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5362, 'Lv105Scarecrow', 22, 0940, 1039, 361, 289, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5361, 'Lv105Scarecrow', 22, 0940, 1039, 357, 293, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5360, 'Lv105Scarecrow', 22, 0940, 1039, 353, 297, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5359, 'Lv105Scarecrow', 22, 0940, 1039, 349, 301, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5358, 'Lv105Scarecrow', 22, 0940, 1039, 345, 305, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5357, 'Lv105Scarecrow', 22, 0940, 1039, 341, 309, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5356, 'Lv100Scarecrow', 22, 0910, 1039, 313, 337, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5355, 'Lv100Scarecrow', 22, 0910, 1039, 309, 341, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5354, 'Lv100Scarecrow', 22, 0910, 1039, 305, 345, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5353, 'Lv100Scarecrow', 22, 0910, 1039, 301, 349, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5352, 'Lv100Scarecrow', 22, 0910, 1039, 297, 353, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5351, 'Lv100Scarecrow', 22, 0910, 1039, 293, 357, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5350, 'Lv100Scarecrow', 22, 0910, 1039, 289, 361, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5349, 'Lv100Scarecrow', 22, 0910, 1039, 281, 369, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5348, 'Lv100Scarecrow', 22, 0910, 1039, 277, 373, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5347, 'Lv100Scarecrow', 22, 0910, 1039, 273, 377, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5346, 'Lv100Scarecrow', 22, 0910, 1039, 269, 381, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5345, 'Lv100Scarecrow', 22, 0910, 1039, 265, 385, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5344, 'Lv100Scarecrow', 22, 0910, 1039, 261, 389, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5343, 'Lv100Scarecrow', 22, 0910, 1039, 257, 393, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5342, 'Lv100Scarecrow', 22, 0910, 1039, 249, 401, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5341, 'Lv100Scarecrow', 22, 0910, 1039, 245, 405, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5340, 'Lv100Scarecrow', 22, 0910, 1039, 241, 409, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5339, 'Lv100Scarecrow', 22, 0910, 1039, 237, 413, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5338, 'Lv100Scarecrow', 22, 0910, 1039, 233, 417, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5337, 'Lv100Scarecrow', 22, 0910, 1039, 229, 421, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5336, 'Lv100Scarecrow', 22, 0910, 1039, 225, 425, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5335, 'Lv95Scarecrow', 22, 0880, 1039, 404, 196, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5334, 'Lv95Scarecrow', 22, 0880, 1039, 400, 200, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5333, 'Lv95Scarecrow', 22, 0880, 1039, 396, 204, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5332, 'Lv95Scarecrow', 22, 0880, 1039, 392, 208, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5331, 'Lv95Scarecrow', 22, 0880, 1039, 388, 212, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5330, 'Lv95Scarecrow', 22, 0880, 1039, 384, 216, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5329, 'Lv95Scarecrow', 22, 0880, 1039, 380, 220, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5328, 'Lv95Scarecrow', 22, 0880, 1039, 372, 228, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5327, 'Lv95Scarecrow', 22, 0880, 1039, 368, 232, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5326, 'Lv95Scarecrow', 22, 0880, 1039, 364, 236, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5325, 'Lv95Scarecrow', 22, 0880, 1039, 360, 240, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5324, 'Lv95Scarecrow', 22, 0880, 1039, 356, 244, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5323, 'Lv95Scarecrow', 22, 0880, 1039, 352, 248, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5322, 'Lv95Scarecrow', 22, 0880, 1039, 348, 252, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5321, 'Lv95Scarecrow', 22, 0880, 1039, 340, 260, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5320, 'Lv95Scarecrow', 22, 0880, 1039, 336, 264, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5319, 'Lv95Scarecrow', 22, 0880, 1039, 332, 268, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5318, 'Lv95Scarecrow', 22, 0880, 1039, 328, 272, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5317, 'Lv95Scarecrow', 22, 0880, 1039, 324, 276, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5316, 'Lv95Scarecrow', 22, 0880, 1039, 320, 280, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5315, 'Lv95Scarecrow', 22, 0880, 1039, 316, 284, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5314, 'Lv90Scarecrow', 22, 0850, 1039, 288, 312, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5313, 'Lv90Scarecrow', 22, 0850, 1039, 284, 316, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5312, 'Lv90Scarecrow', 22, 0850, 1039, 280, 320, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5311, 'Lv90Scarecrow', 22, 0850, 1039, 276, 324, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5310, 'Lv90Scarecrow', 22, 0850, 1039, 272, 328, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5309, 'Lv90Scarecrow', 22, 0850, 1039, 268, 332, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5308, 'Lv90Scarecrow', 22, 0850, 1039, 264, 336, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5307, 'Lv90Scarecrow', 22, 0850, 1039, 256, 344, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5306, 'Lv90Scarecrow', 22, 0850, 1039, 252, 348, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5305, 'Lv90Scarecrow', 22, 0850, 1039, 248, 352, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5304, 'Lv90Scarecrow', 22, 0850, 1039, 244, 356, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5303, 'Lv90Scarecrow', 22, 0850, 1039, 240, 360, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5302, 'Lv90Scarecrow', 22, 0850, 1039, 236, 364, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5301, 'Lv90Scarecrow', 22, 0850, 1039, 232, 368, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5300, 'Lv90Scarecrow', 22, 0850, 1039, 224, 376, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5299, 'Lv90Scarecrow', 22, 0850, 1039, 220, 380, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5298, 'Lv90Scarecrow', 22, 0850, 1039, 216, 384, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5297, 'Lv90Scarecrow', 22, 0850, 1039, 212, 388, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5296, 'Lv90Scarecrow', 22, 0850, 1039, 208, 392, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5295, 'Lv90Scarecrow', 22, 0850, 1039, 204, 396, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5294, 'Lv90Scarecrow', 22, 0850, 1039, 200, 400, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5293, 'Lv85Scarecrow', 22, 0820, 1039, 379, 171, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5292, 'Lv85Scarecrow', 22, 0820, 1039, 375, 175, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5291, 'Lv85Scarecrow', 22, 0820, 1039, 371, 179, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5290, 'Lv85Scarecrow', 22, 0820, 1039, 367, 183, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5289, 'Lv85Scarecrow', 22, 0820, 1039, 363, 187, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5288, 'Lv85Scarecrow', 22, 0820, 1039, 359, 191, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5287, 'Lv85Scarecrow', 22, 0820, 1039, 355, 195, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5286, 'Lv85Scarecrow', 22, 0820, 1039, 347, 203, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5285, 'Lv85Scarecrow', 22, 0820, 1039, 343, 207, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5284, 'Lv85Scarecrow', 22, 0820, 1039, 339, 211, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5283, 'Lv85Scarecrow', 22, 0820, 1039, 335, 215, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5282, 'Lv85Scarecrow', 22, 0820, 1039, 331, 219, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5281, 'Lv85Scarecrow', 22, 0820, 1039, 327, 223, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5280, 'Lv85Scarecrow', 22, 0820, 1039, 323, 227, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5279, 'Lv85Scarecrow', 22, 0820, 1039, 315, 235, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5278, 'Lv85Scarecrow', 22, 0820, 1039, 311, 239, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5277, 'Lv85Scarecrow', 22, 0820, 1039, 307, 243, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5276, 'Lv85Scarecrow', 22, 0820, 1039, 303, 247, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5275, 'Lv85Scarecrow', 22, 0820, 1039, 299, 251, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5274, 'Lv85Scarecrow', 22, 0820, 1039, 295, 255, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5273, 'Lv85Scarecrow', 22, 0820, 1039, 291, 259, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5272, 'Lv80Scarecrow', 22, 0790, 1039, 263, 287, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5271, 'Lv80Scarecrow', 22, 0790, 1039, 259, 291, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5270, 'Lv80Scarecrow', 22, 0790, 1039, 255, 295, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5269, 'Lv80Scarecrow', 22, 0790, 1039, 251, 299, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5268, 'Lv80Scarecrow', 22, 0790, 1039, 247, 303, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5267, 'Lv80Scarecrow', 22, 0790, 1039, 243, 307, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5266, 'Lv80Scarecrow', 22, 0790, 1039, 239, 311, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5265, 'Lv80Scarecrow', 22, 0790, 1039, 231, 319, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5264, 'Lv80Scarecrow', 22, 0790, 1039, 227, 323, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5263, 'Lv80Scarecrow', 22, 0790, 1039, 223, 327, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5262, 'Lv80Scarecrow', 22, 0790, 1039, 219, 331, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5261, 'Lv80Scarecrow', 22, 0790, 1039, 215, 335, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5260, 'Lv80Scarecrow', 22, 0790, 1039, 211, 339, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5259, 'Lv80Scarecrow', 22, 0790, 1039, 207, 343, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5258, 'Lv80Scarecrow', 22, 0790, 1039, 199, 351, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5257, 'Lv80Scarecrow', 22, 0790, 1039, 195, 355, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5256, 'Lv80Scarecrow', 22, 0790, 1039, 191, 359, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5255, 'Lv80Scarecrow', 22, 0790, 1039, 187, 363, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5254, 'Lv80Scarecrow', 22, 0790, 1039, 183, 367, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5253, 'Lv80Scarecrow', 22, 0790, 1039, 179, 371, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5252, 'Lv80Scarecrow', 22, 0790, 1039, 175, 375, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5251, 'Lv75Scarecrow', 22, 0760, 1039, 339, 131, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5250, 'Lv75Scarecrow', 22, 0760, 1039, 335, 135, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5249, 'Lv75Scarecrow', 22, 0760, 1039, 331, 139, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5248, 'Lv75Scarecrow', 22, 0760, 1039, 327, 143, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5247, 'Lv75Scarecrow', 22, 0760, 1039, 323, 147, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5246, 'Lv75Scarecrow', 22, 0760, 1039, 319, 151, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5245, 'Lv75Scarecrow', 22, 0760, 1039, 315, 155, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5244, 'Lv75Scarecrow', 22, 0760, 1039, 307, 163, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5243, 'Lv75Scarecrow', 22, 0760, 1039, 303, 167, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5242, 'Lv75Scarecrow', 22, 0760, 1039, 299, 171, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5241, 'Lv75Scarecrow', 22, 0760, 1039, 295, 175, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5240, 'Lv75Scarecrow', 22, 0760, 1039, 291, 179, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5239, 'Lv75Scarecrow', 22, 0760, 1039, 287, 183, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5238, 'Lv75Scarecrow', 22, 0760, 1039, 283, 187, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5237, 'Lv75Scarecrow', 22, 0760, 1039, 275, 195, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5236, 'Lv75Scarecrow', 22, 0760, 1039, 271, 199, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5235, 'Lv75Scarecrow', 22, 0760, 1039, 267, 203, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5234, 'Lv75Scarecrow', 22, 0760, 1039, 263, 207, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5233, 'Lv75Scarecrow', 22, 0760, 1039, 259, 211, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5232, 'Lv75Scarecrow', 22, 0760, 1039, 255, 215, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5231, 'Lv75Scarecrow', 22, 0760, 1039, 251, 219, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5230, 'Lv70Scarecrow', 22, 0730, 1039, 223, 247, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5229, 'Lv70Scarecrow', 22, 0730, 1039, 219, 251, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5228, 'Lv70Scarecrow', 22, 0730, 1039, 215, 255, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5227, 'Lv70Scarecrow', 22, 0730, 1039, 211, 259, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5226, 'Lv70Scarecrow', 22, 0730, 1039, 207, 263, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5225, 'Lv70Scarecrow', 22, 0730, 1039, 203, 267, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5224, 'Lv70Scarecrow', 22, 0730, 1039, 199, 271, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5223, 'Lv70Scarecrow', 22, 0730, 1039, 191, 279, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5222, 'Lv70Scarecrow', 22, 0730, 1039, 187, 283, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5221, 'Lv70Scarecrow', 22, 0730, 1039, 183, 287, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5220, 'Lv70Scarecrow', 22, 0730, 1039, 179, 291, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5219, 'Lv70Scarecrow', 22, 0730, 1039, 175, 295, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5218, 'Lv70Scarecrow', 22, 0730, 1039, 171, 299, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5217, 'Lv70Scarecrow', 22, 0730, 1039, 167, 303, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5216, 'Lv70Scarecrow', 22, 0730, 1039, 159, 311, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5215, 'Lv70Scarecrow', 22, 0730, 1039, 155, 315, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5214, 'Lv70Scarecrow', 22, 0730, 1039, 151, 319, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5213, 'Lv70Scarecrow', 22, 0730, 1039, 147, 323, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5212, 'Lv70Scarecrow', 22, 0730, 1039, 143, 327, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5211, 'Lv70Scarecrow', 22, 0730, 1039, 139, 331, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5210, 'Lv70Scarecrow', 22, 0730, 1039, 135, 335, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5209, 'Lv65Scarecrow', 22, 0700, 1039, 314, 106, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5208, 'Lv65Scarecrow', 22, 0700, 1039, 310, 110, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5207, 'Lv65Scarecrow', 22, 0700, 1039, 306, 114, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5206, 'Lv65Scarecrow', 22, 0700, 1039, 302, 118, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5205, 'Lv65Scarecrow', 22, 0700, 1039, 298, 122, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5204, 'Lv65Scarecrow', 22, 0700, 1039, 294, 126, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5203, 'Lv65Scarecrow', 22, 0700, 1039, 290, 130, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5202, 'Lv65Scarecrow', 22, 0700, 1039, 282, 138, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5201, 'Lv65Scarecrow', 22, 0700, 1039, 278, 142, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5200, 'Lv65Scarecrow', 22, 0700, 1039, 274, 146, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5199, 'Lv65Scarecrow', 22, 0700, 1039, 270, 150, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5198, 'Lv65Scarecrow', 22, 0700, 1039, 266, 154, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5197, 'Lv65Scarecrow', 22, 0700, 1039, 262, 158, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5196, 'Lv65Scarecrow', 22, 0700, 1039, 258, 162, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5195, 'Lv65Scarecrow', 22, 0700, 1039, 250, 170, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5194, 'Lv65Scarecrow', 22, 0700, 1039, 246, 174, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5193, 'Lv65Scarecrow', 22, 0700, 1039, 242, 178, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5192, 'Lv65Scarecrow', 22, 0700, 1039, 238, 182, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5191, 'Lv65Scarecrow', 22, 0700, 1039, 234, 186, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5190, 'Lv65Scarecrow', 22, 0700, 1039, 230, 190, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5189, 'Lv65Scarecrow', 22, 0700, 1039, 226, 194, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5188, 'Lv60Scarecrow', 22, 0670, 1039, 198, 222, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5187, 'Lv60Scarecrow', 22, 0670, 1039, 194, 226, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5186, 'Lv60Scarecrow', 22, 0670, 1039, 190, 230, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5185, 'Lv60Scarecrow', 22, 0670, 1039, 186, 234, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5184, 'Lv60Scarecrow', 22, 0670, 1039, 182, 238, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5183, 'Lv60Scarecrow', 22, 0670, 1039, 178, 242, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5182, 'Lv60Scarecrow', 22, 0670, 1039, 174, 246, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5181, 'Lv60Scarecrow', 22, 0670, 1039, 166, 254, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5180, 'Lv60Scarecrow', 22, 0670, 1039, 162, 258, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5179, 'Lv60Scarecrow', 22, 0670, 1039, 158, 262, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5178, 'Lv60Scarecrow', 22, 0670, 1039, 154, 266, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5177, 'Lv60Scarecrow', 22, 0670, 1039, 150, 270, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5176, 'Lv60Scarecrow', 22, 0670, 1039, 146, 274, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5175, 'Lv60Scarecrow', 22, 0670, 1039, 142, 278, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5174, 'Lv60Scarecrow', 22, 0670, 1039, 134, 286, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5173, 'Lv60Scarecrow', 22, 0670, 1039, 130, 290, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5172, 'Lv60Scarecrow', 22, 0670, 1039, 126, 294, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5171, 'Lv60Scarecrow', 22, 0670, 1039, 122, 298, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5170, 'Lv60Scarecrow', 22, 0670, 1039, 118, 302, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5169, 'Lv60Scarecrow', 22, 0670, 1039, 114, 306, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5168, 'Lv60Scarecrow', 22, 0670, 1039, 110, 310, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5167, 'Lv55Scarecrow', 22, 0640, 1039, 289, 81, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5166, 'Lv55Scarecrow', 22, 0640, 1039, 285, 85, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5165, 'Lv55Scarecrow', 22, 0640, 1039, 281, 89, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5164, 'Lv55Scarecrow', 22, 0640, 1039, 277, 93, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5163, 'Lv55Scarecrow', 22, 0640, 1039, 273, 97, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5162, 'Lv55Scarecrow', 22, 0640, 1039, 269, 101, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5161, 'Lv55Scarecrow', 22, 0640, 1039, 265, 105, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5160, 'Lv55Scarecrow', 22, 0640, 1039, 257, 113, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5159, 'Lv55Scarecrow', 22, 0640, 1039, 253, 117, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5158, 'Lv55Scarecrow', 22, 0640, 1039, 249, 121, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5157, 'Lv55Scarecrow', 22, 0640, 1039, 245, 125, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5156, 'Lv55Scarecrow', 22, 0640, 1039, 241, 129, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5155, 'Lv55Scarecrow', 22, 0640, 1039, 237, 133, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5154, 'Lv55Scarecrow', 22, 0640, 1039, 233, 137, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5153, 'Lv55Scarecrow', 22, 0640, 1039, 225, 145, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5152, 'Lv55Scarecrow', 22, 0640, 1039, 221, 149, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5151, 'Lv55Scarecrow', 22, 0640, 1039, 217, 153, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5150, 'Lv55Scarecrow', 22, 0640, 1039, 213, 157, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5149, 'Lv55Scarecrow', 22, 0640, 1039, 209, 161, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5148, 'Lv55Scarecrow', 22, 0640, 1039, 205, 165, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5147, 'Lv55Scarecrow', 22, 0640, 1039, 201, 169, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5146, 'Lv50Scarecrow', 22, 0610, 1039, 173, 197, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5145, 'Lv50Scarecrow', 22, 0610, 1039, 169, 201, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5144, 'Lv50Scarecrow', 22, 0610, 1039, 165, 205, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5143, 'Lv50Scarecrow', 22, 0610, 1039, 161, 209, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5142, 'Lv50Scarecrow', 22, 0610, 1039, 157, 213, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5141, 'Lv50Scarecrow', 22, 0610, 1039, 153, 217, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5140, 'Lv50Scarecrow', 22, 0610, 1039, 149, 221, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5139, 'Lv50Scarecrow', 22, 0610, 1039, 141, 229, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5138, 'Lv50Scarecrow', 22, 0610, 1039, 137, 233, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5137, 'Lv50Scarecrow', 22, 0610, 1039, 133, 237, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5136, 'Lv50Scarecrow', 22, 0610, 1039, 129, 241, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5135, 'Lv50Scarecrow', 22, 0610, 1039, 125, 245, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5134, 'Lv50Scarecrow', 22, 0610, 1039, 121, 249, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5133, 'Lv50Scarecrow', 22, 0610, 1039, 117, 253, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5132, 'Lv50Scarecrow', 22, 0610, 1039, 109, 261, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5131, 'Lv50Scarecrow', 22, 0610, 1039, 105, 265, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5130, 'Lv50Scarecrow', 22, 0610, 1039, 101, 269, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5129, 'Lv50Scarecrow', 22, 0610, 1039, 97, 273, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5128, 'Lv50Scarecrow', 22, 0610, 1039, 93, 277, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5127, 'Lv50Scarecrow', 22, 0610, 1039, 89, 281, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5126, 'Lv50Scarecrow', 22, 0610, 1039, 85, 285, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5125, 'Lv45Scarecrow', 22, 0580, 1039, 264, 56, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5124, 'Lv45Scarecrow', 22, 0580, 1039, 260, 60, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5123, 'Lv45Scarecrow', 22, 0580, 1039, 256, 64, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5122, 'Lv45Scarecrow', 22, 0580, 1039, 252, 68, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5121, 'Lv45Scarecrow', 22, 0580, 1039, 248, 72, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5120, 'Lv45Scarecrow', 22, 0580, 1039, 244, 76, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5119, 'Lv45Scarecrow', 22, 0580, 1039, 240, 80, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5118, 'Lv45Scarecrow', 22, 0580, 1039, 232, 88, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5117, 'Lv45Scarecrow', 22, 0580, 1039, 228, 92, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5116, 'Lv45Scarecrow', 22, 0580, 1039, 224, 96, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5115, 'Lv45Scarecrow', 22, 0580, 1039, 220, 100, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5114, 'Lv45Scarecrow', 22, 0580, 1039, 216, 104, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5113, 'Lv45Scarecrow', 22, 0580, 1039, 212, 108, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5112, 'Lv45Scarecrow', 22, 0580, 1039, 208, 112, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5111, 'Lv45Scarecrow', 22, 0580, 1039, 200, 120, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5110, 'Lv45Scarecrow', 22, 0580, 1039, 196, 124, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5109, 'Lv45Scarecrow', 22, 0580, 1039, 192, 128, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5108, 'Lv45Scarecrow', 22, 0580, 1039, 188, 132, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5107, 'Lv45Scarecrow', 22, 0580, 1039, 184, 136, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5106, 'Lv45Scarecrow', 22, 0580, 1039, 180, 140, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5105, 'Lv45Scarecrow', 22, 0580, 1039, 176, 144, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5104, 'Lv40Scarecrow', 22, 0550, 1039, 148, 172, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5103, 'Lv40Scarecrow', 22, 0550, 1039, 144, 176, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5102, 'Lv40Scarecrow', 22, 0550, 1039, 140, 180, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5101, 'Lv40Scarecrow', 22, 0550, 1039, 136, 184, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5100, 'Lv40Scarecrow', 22, 0550, 1039, 132, 188, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5099, 'Lv40Scarecrow', 22, 0550, 1039, 128, 192, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5098, 'Lv40Scarecrow', 22, 0550, 1039, 124, 196, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5097, 'Lv40Scarecrow', 22, 0550, 1039, 116, 204, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5096, 'Lv40Scarecrow', 22, 0550, 1039, 112, 208, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5095, 'Lv40Scarecrow', 22, 0550, 1039, 108, 212, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5094, 'Lv40Scarecrow', 22, 0550, 1039, 104, 216, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5093, 'Lv40Scarecrow', 22, 0550, 1039, 100, 220, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5092, 'Lv40Scarecrow', 22, 0550, 1039, 96, 224, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5091, 'Lv40Scarecrow', 22, 0550, 1039, 92, 228, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5090, 'Lv40Scarecrow', 22, 0550, 1039, 84, 236, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5089, 'Lv40Scarecrow', 22, 0550, 1039, 80, 240, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5088, 'Lv40Scarecrow', 22, 0550, 1039, 76, 244, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5087, 'Lv40Scarecrow', 22, 0550, 1039, 72, 248, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5086, 'Lv40Scarecrow', 22, 0550, 1039, 68, 252, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5085, 'Lv40Scarecrow', 22, 0550, 1039, 64, 256, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5084, 'Lv40Scarecrow', 22, 0550, 1039, 60, 260, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5083, 'Lv35Scarecrow', 22, 0520, 1039, 239, 31, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5082, 'Lv35Scarecrow', 22, 0520, 1039, 235, 35, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5081, 'Lv35Scarecrow', 22, 0520, 1039, 231, 39, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5080, 'Lv35Scarecrow', 22, 0520, 1039, 227, 43, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5079, 'Lv35Scarecrow', 22, 0520, 1039, 223, 47, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5078, 'Lv35Scarecrow', 22, 0520, 1039, 219, 51, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5077, 'Lv35Scarecrow', 22, 0520, 1039, 215, 55, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5076, 'Lv35Scarecrow', 22, 0520, 1039, 207, 63, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5075, 'Lv35Scarecrow', 22, 0520, 1039, 203, 67, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5074, 'Lv35Scarecrow', 22, 0520, 1039, 199, 71, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5073, 'Lv35Scarecrow', 22, 0520, 1039, 195, 75, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5072, 'Lv35Scarecrow', 22, 0520, 1039, 191, 79, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5071, 'Lv35Scarecrow', 22, 0520, 1039, 187, 83, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5070, 'Lv35Scarecrow', 22, 0520, 1039, 183, 87, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5069, 'Lv35Scarecrow', 22, 0520, 1039, 175, 95, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5068, 'Lv35Scarecrow', 22, 0520, 1039, 171, 99, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5067, 'Lv35Scarecrow', 22, 0520, 1039, 167, 103, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5066, 'Lv35Scarecrow', 22, 0520, 1039, 163, 107, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5065, 'Lv35Scarecrow', 22, 0520, 1039, 159, 111, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5064, 'Lv35Scarecrow', 22, 0520, 1039, 155, 115, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5063, 'Lv35Scarecrow', 22, 0520, 1039, 151, 119, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5062, 'Lv30Scarecrow', 22, 0490, 1039, 123, 147, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5061, 'Lv30Scarecrow', 22, 0490, 1039, 119, 151, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5060, 'Lv30Scarecrow', 22, 0490, 1039, 115, 155, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5059, 'Lv30Scarecrow', 22, 0490, 1039, 111, 159, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5058, 'Lv30Scarecrow', 22, 0490, 1039, 107, 163, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5057, 'Lv30Scarecrow', 22, 0490, 1039, 103, 167, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5056, 'Lv30Scarecrow', 22, 0490, 1039, 99, 171, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5055, 'Lv30Scarecrow', 22, 0490, 1039, 91, 179, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5054, 'Lv30Scarecrow', 22, 0490, 1039, 87, 183, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5053, 'Lv30Scarecrow', 22, 0490, 1039, 83, 187, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5052, 'Lv30Scarecrow', 22, 0490, 1039, 79, 191, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5051, 'Lv30Scarecrow', 22, 0490, 1039, 75, 195, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5050, 'Lv30Scarecrow', 22, 0490, 1039, 71, 199, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5049, 'Lv30Scarecrow', 22, 0490, 1039, 67, 203, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5048, 'Lv30Scarecrow', 22, 0490, 1039, 59, 211, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5047, 'Lv30Scarecrow', 22, 0490, 1039, 55, 215, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5046, 'Lv30Scarecrow', 22, 0490, 1039, 51, 219, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5045, 'Lv30Scarecrow', 22, 0490, 1039, 47, 223, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5044, 'Lv30Scarecrow', 22, 0490, 1039, 43, 227, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5043, 'Lv30Scarecrow', 22, 0490, 1039, 39, 231, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5042, 'Lv30Scarecrow', 22, 0490, 1039, 35, 235, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5041, 'Lv25Scarecrow', 22, 0460, 1039, 214, 06, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5040, 'Lv25Scarecrow', 22, 0460, 1039, 210, 10, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5039, 'Lv25Scarecrow', 22, 0460, 1039, 206, 14, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5038, 'Lv25Scarecrow', 22, 0460, 1039, 202, 18, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5037, 'Lv25Scarecrow', 22, 0460, 1039, 198, 22, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5036, 'Lv25Scarecrow', 22, 0460, 1039, 194, 26, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5035, 'Lv25Scarecrow', 22, 0460, 1039, 190, 30, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5034, 'Lv25Scarecrow', 22, 0460, 1039, 182, 38, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5033, 'Lv25Scarecrow', 22, 0460, 1039, 178, 42, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5032, 'Lv25Scarecrow', 22, 0460, 1039, 174, 46, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5031, 'Lv25Scarecrow', 22, 0460, 1039, 170, 50, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5030, 'Lv25Scarecrow', 22, 0460, 1039, 166, 54, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5029, 'Lv25Scarecrow', 22, 0460, 1039, 162, 58, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5028, 'Lv25Scarecrow', 22, 0460, 1039, 158, 62, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5027, 'Lv25Scarecrow', 22, 0460, 1039, 150, 70, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5026, 'Lv25Scarecrow', 22, 0460, 1039, 146, 74, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5025, 'Lv25Scarecrow', 22, 0460, 1039, 142, 78, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5024, 'Lv25Scarecrow', 22, 0460, 1039, 138, 82, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5023, 'Lv25Scarecrow', 22, 0460, 1039, 134, 86, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5022, 'Lv25Scarecrow', 22, 0460, 1039, 130, 90, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5021, 'Lv25Scarecrow', 22, 0460, 1039, 126, 94, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5020, 'Lv20Scarecrow', 22, 0430, 1039, 98, 122, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5019, 'Lv20Scarecrow', 22, 0430, 1039, 94, 126, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5018, 'Lv20Scarecrow', 22, 0430, 1039, 90, 130, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5017, 'Lv20Scarecrow', 22, 0430, 1039, 86, 134, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5016, 'Lv20Scarecrow', 22, 0430, 1039, 82, 138, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5015, 'Lv20Scarecrow', 22, 0430, 1039, 78, 142, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5014, 'Lv20Scarecrow', 22, 0430, 1039, 74, 146, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5013, 'Lv20Scarecrow', 22, 0430, 1039, 66, 154, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5012, 'Lv20Scarecrow', 22, 0430, 1039, 62, 158, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5011, 'Lv20Scarecrow', 22, 0430, 1039, 58, 162, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5010, 'Lv20Scarecrow', 22, 0430, 1039, 54, 166, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5001, 'Lv20Scarecrow', 22, 0430, 1039, 14, 206, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (5000, 'Lv20Scarecrow', 22, 0430, 1039, 10, 210, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6004, 'Lv20Stake', 21, 0420, 1039, 36, 204, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6005, 'Lv20Stake', 21, 0420, 1039, 40, 200, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6006, 'Lv20Stake', 21, 0420, 1039, 44, 196, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6007, 'Lv20Stake', 21, 0420, 1039, 52, 188, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6008, 'Lv20Stake', 21, 0420, 1039, 56, 184, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6009, 'Lv20Stake', 21, 0420, 1039, 60, 180, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6010, 'Lv20Stake', 21, 0420, 1039, 64, 176, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6011, 'Lv20Stake', 21, 0420, 1039, 68, 172, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6012, 'Lv20Stake', 21, 0420, 1039, 72, 168, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6013, 'Lv20Stake', 21, 0420, 1039, 76, 164, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6014, 'Lv20Stake', 21, 0420, 1039, 84, 156, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6015, 'Lv20Stake', 21, 0420, 1039, 88, 152, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6016, 'Lv20Stake', 21, 0420, 1039, 92, 148, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6017, 'Lv20Stake', 21, 0420, 1039, 96, 144, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6018, 'Lv20Stake', 21, 0420, 1039, 100, 140, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6019, 'Lv20Stake', 21, 0420, 1039, 104, 136, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6020, 'Lv20Stake', 21, 0420, 1039, 108, 132, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6021, 'Lv25Stake', 21, 0450, 1039, 136, 104, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6022, 'Lv25Stake', 21, 0450, 1039, 140, 100, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6023, 'Lv25Stake', 21, 0450, 1039, 144, 96, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6024, 'Lv25Stake', 21, 0450, 1039, 148, 92, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6025, 'Lv25Stake', 21, 0450, 1039, 152, 88, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6026, 'Lv25Stake', 21, 0450, 1039, 156, 84, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6027, 'Lv25Stake', 21, 0450, 1039, 160, 80, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6028, 'Lv25Stake', 21, 0450, 1039, 168, 72, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6029, 'Lv25Stake', 21, 0450, 1039, 172, 68, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6030, 'Lv25Stake', 21, 0450, 1039, 176, 64, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6031, 'Lv25Stake', 21, 0450, 1039, 180, 60, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6032, 'Lv25Stake', 21, 0450, 1039, 184, 56, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6033, 'Lv25Stake', 21, 0450, 1039, 188, 52, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6034, 'Lv25Stake', 21, 0450, 1039, 192, 48, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6035, 'Lv25Stake', 21, 0450, 1039, 200, 40, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6036, 'Lv25Stake', 21, 0450, 1039, 204, 36, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6037, 'Lv25Stake', 21, 0450, 1039, 208, 32, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6038, 'Lv25Stake', 21, 0450, 1039, 212, 28, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6039, 'Lv25Stake', 21, 0450, 1039, 216, 24, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6040, 'Lv25Stake', 21, 0450, 1039, 220, 20, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6041, 'Lv25Stake', 21, 0450, 1039, 224, 16, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6042, 'Lv30Stake', 21, 0480, 1039, 45, 245, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6043, 'Lv30Stake', 21, 0480, 1039, 49, 241, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6044, 'Lv30Stake', 21, 0480, 1039, 53, 237, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6045, 'Lv30Stake', 21, 0480, 1039, 57, 233, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6046, 'Lv30Stake', 21, 0480, 1039, 61, 229, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6047, 'Lv30Stake', 21, 0480, 1039, 65, 225, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6048, 'Lv30Stake', 21, 0480, 1039, 69, 221, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6049, 'Lv30Stake', 21, 0480, 1039, 77, 213, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6050, 'Lv30Stake', 21, 0480, 1039, 81, 209, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6051, 'Lv30Stake', 21, 0480, 1039, 85, 205, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6052, 'Lv30Stake', 21, 0480, 1039, 89, 201, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6053, 'Lv30Stake', 21, 0480, 1039, 93, 197, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6054, 'Lv30Stake', 21, 0480, 1039, 97, 193, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6055, 'Lv30Stake', 21, 0480, 1039, 101, 189, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6056, 'Lv30Stake', 21, 0480, 1039, 109, 181, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6057, 'Lv30Stake', 21, 0480, 1039, 113, 177, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6058, 'Lv30Stake', 21, 0480, 1039, 117, 173, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6059, 'Lv30Stake', 21, 0480, 1039, 121, 169, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6060, 'Lv30Stake', 21, 0480, 1039, 125, 165, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6061, 'Lv30Stake', 21, 0480, 1039, 129, 161, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6062, 'Lv30Stake', 21, 0480, 1039, 133, 157, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6063, 'Lv35Stake', 21, 0510, 1039, 161, 129, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6064, 'Lv35Stake', 21, 0510, 1039, 165, 125, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6065, 'Lv35Stake', 21, 0510, 1039, 169, 121, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6066, 'Lv35Stake', 21, 0510, 1039, 173, 117, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6067, 'Lv35Stake', 21, 0510, 1039, 177, 113, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6068, 'Lv35Stake', 21, 0510, 1039, 181, 109, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6069, 'Lv35Stake', 21, 0510, 1039, 185, 105, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6070, 'Lv35Stake', 21, 0510, 1039, 193, 97, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6071, 'Lv35Stake', 21, 0510, 1039, 197, 93, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6072, 'Lv35Stake', 21, 0510, 1039, 201, 89, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6073, 'Lv35Stake', 21, 0510, 1039, 205, 85, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6074, 'Lv35Stake', 21, 0510, 1039, 209, 81, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6075, 'Lv35Stake', 21, 0510, 1039, 213, 77, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6076, 'Lv35Stake', 21, 0510, 1039, 217, 73, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6077, 'Lv35Stake', 21, 0510, 1039, 225, 65, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6078, 'Lv35Stake', 21, 0510, 1039, 229, 61, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6079, 'Lv35Stake', 21, 0510, 1039, 233, 57, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6080, 'Lv35Stake', 21, 0510, 1039, 237, 53, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6081, 'Lv35Stake', 21, 0510, 1039, 241, 49, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6082, 'Lv35Stake', 21, 0510, 1039, 245, 45, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6083, 'Lv35Stake', 21, 0510, 1039, 249, 41, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6084, 'Lv40Stake', 21, 0540, 1039, 70, 270, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6085, 'Lv40Stake', 21, 0540, 1039, 74, 266, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6086, 'Lv40Stake', 21, 0540, 1039, 78, 262, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6087, 'Lv40Stake', 21, 0540, 1039, 82, 258, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6088, 'Lv40Stake', 21, 0540, 1039, 86, 254, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6089, 'Lv40Stake', 21, 0540, 1039, 90, 250, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6090, 'Lv40Stake', 21, 0540, 1039, 94, 246, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6091, 'Lv40Stake', 21, 0540, 1039, 102, 238, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6092, 'Lv40Stake', 21, 0540, 1039, 106, 234, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6093, 'Lv40Stake', 21, 0540, 1039, 110, 230, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6094, 'Lv40Stake', 21, 0540, 1039, 114, 226, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6095, 'Lv40Stake', 21, 0540, 1039, 118, 222, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6096, 'Lv40Stake', 21, 0540, 1039, 122, 218, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6097, 'Lv40Stake', 21, 0540, 1039, 126, 214, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6098, 'Lv40Stake', 21, 0540, 1039, 134, 206, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6099, 'Lv40Stake', 21, 0540, 1039, 138, 202, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6100, 'Lv40Stake', 21, 0540, 1039, 142, 198, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6101, 'Lv40Stake', 21, 0540, 1039, 146, 194, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6102, 'Lv40Stake', 21, 0540, 1039, 150, 190, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6103, 'Lv40Stake', 21, 0540, 1039, 154, 186, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6104, 'Lv40Stake', 21, 0540, 1039, 158, 182, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6105, 'Lv45Stake', 21, 0570, 1039, 186, 154, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6106, 'Lv45Stake', 21, 0570, 1039, 190, 150, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6107, 'Lv45Stake', 21, 0570, 1039, 194, 146, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6108, 'Lv45Stake', 21, 0570, 1039, 198, 142, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6109, 'Lv45Stake', 21, 0570, 1039, 202, 138, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6110, 'Lv45Stake', 21, 0570, 1039, 206, 134, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6111, 'Lv45Stake', 21, 0570, 1039, 210, 130, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6112, 'Lv45Stake', 21, 0570, 1039, 218, 122, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6113, 'Lv45Stake', 21, 0570, 1039, 222, 118, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6114, 'Lv45Stake', 21, 0570, 1039, 226, 114, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6115, 'Lv45Stake', 21, 0570, 1039, 230, 110, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6116, 'Lv45Stake', 21, 0570, 1039, 234, 106, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6117, 'Lv45Stake', 21, 0570, 1039, 238, 102, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6118, 'Lv45Stake', 21, 0570, 1039, 242, 98, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6119, 'Lv45Stake', 21, 0570, 1039, 250, 90, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6120, 'Lv45Stake', 21, 0570, 1039, 254, 86, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6121, 'Lv45Stake', 21, 0570, 1039, 258, 82, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6122, 'Lv45Stake', 21, 0570, 1039, 262, 78, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6123, 'Lv45Stake', 21, 0570, 1039, 266, 74, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6124, 'Lv45Stake', 21, 0570, 1039, 270, 70, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6125, 'Lv45Stake', 21, 0570, 1039, 274, 66, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6126, 'Lv50Stake', 21, 0600, 1039, 95, 295, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6127, 'Lv50Stake', 21, 0600, 1039, 99, 291, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6128, 'Lv50Stake', 21, 0600, 1039, 103, 287, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6129, 'Lv50Stake', 21, 0600, 1039, 107, 283, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6130, 'Lv50Stake', 21, 0600, 1039, 111, 279, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6131, 'Lv50Stake', 21, 0600, 1039, 115, 275, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6132, 'Lv50Stake', 21, 0600, 1039, 119, 271, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6133, 'Lv50Stake', 21, 0600, 1039, 127, 263, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6134, 'Lv50Stake', 21, 0600, 1039, 131, 259, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6135, 'Lv50Stake', 21, 0600, 1039, 135, 255, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6136, 'Lv50Stake', 21, 0600, 1039, 139, 251, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6137, 'Lv50Stake', 21, 0600, 1039, 143, 247, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6138, 'Lv50Stake', 21, 0600, 1039, 147, 243, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6139, 'Lv50Stake', 21, 0600, 1039, 151, 239, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6140, 'Lv50Stake', 21, 0600, 1039, 159, 231, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6141, 'Lv50Stake', 21, 0600, 1039, 163, 227, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6142, 'Lv50Stake', 21, 0600, 1039, 167, 223, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6143, 'Lv50Stake', 21, 0600, 1039, 171, 219, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6144, 'Lv50Stake', 21, 0600, 1039, 175, 215, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6145, 'Lv50Stake', 21, 0600, 1039, 179, 211, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6146, 'Lv50Stake', 21, 0600, 1039, 183, 207, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6147, 'Lv55Stake', 21, 0630, 1039, 211, 179, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6148, 'Lv55Stake', 21, 0630, 1039, 215, 175, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6149, 'Lv55Stake', 21, 0630, 1039, 219, 171, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6150, 'Lv55Stake', 21, 0630, 1039, 223, 167, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6151, 'Lv55Stake', 21, 0630, 1039, 227, 163, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6152, 'Lv55Stake', 21, 0630, 1039, 231, 159, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6153, 'Lv55Stake', 21, 0630, 1039, 235, 155, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6154, 'Lv55Stake', 21, 0630, 1039, 243, 147, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6155, 'Lv55Stake', 21, 0630, 1039, 247, 143, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6156, 'Lv55Stake', 21, 0630, 1039, 251, 139, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6157, 'Lv55Stake', 21, 0630, 1039, 255, 135, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6158, 'Lv55Stake', 21, 0630, 1039, 259, 131, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6159, 'Lv55Stake', 21, 0630, 1039, 263, 127, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6160, 'Lv55Stake', 21, 0630, 1039, 267, 123, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6161, 'Lv55Stake', 21, 0630, 1039, 275, 115, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6162, 'Lv55Stake', 21, 0630, 1039, 279, 111, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6163, 'Lv55Stake', 21, 0630, 1039, 283, 107, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6164, 'Lv55Stake', 21, 0630, 1039, 287, 103, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6165, 'Lv55Stake', 21, 0630, 1039, 291, 99, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6166, 'Lv55Stake', 21, 0630, 1039, 295, 95, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6167, 'Lv55Stake', 21, 0630, 1039, 299, 91, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6168, 'Lv60Stake', 21, 0660, 1039, 120, 320, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6169, 'Lv60Stake', 21, 0660, 1039, 124, 316, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6170, 'Lv60Stake', 21, 0660, 1039, 128, 312, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6171, 'Lv60Stake', 21, 0660, 1039, 132, 308, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6172, 'Lv60Stake', 21, 0660, 1039, 136, 304, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6173, 'Lv60Stake', 21, 0660, 1039, 140, 300, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6174, 'Lv60Stake', 21, 0660, 1039, 144, 296, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6175, 'Lv60Stake', 21, 0660, 1039, 152, 288, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6176, 'Lv60Stake', 21, 0660, 1039, 156, 284, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6177, 'Lv60Stake', 21, 0660, 1039, 160, 280, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6178, 'Lv60Stake', 21, 0660, 1039, 164, 276, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6179, 'Lv60Stake', 21, 0660, 1039, 168, 272, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6180, 'Lv60Stake', 21, 0660, 1039, 172, 268, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6181, 'Lv60Stake', 21, 0660, 1039, 176, 264, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6182, 'Lv60Stake', 21, 0660, 1039, 184, 256, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6183, 'Lv60Stake', 21, 0660, 1039, 188, 252, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6184, 'Lv60Stake', 21, 0660, 1039, 192, 248, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6185, 'Lv60Stake', 21, 0660, 1039, 196, 244, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6186, 'Lv60Stake', 21, 0660, 1039, 200, 240, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6187, 'Lv60Stake', 21, 0660, 1039, 204, 236, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6188, 'Lv60Stake', 21, 0660, 1039, 208, 232, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6189, 'Lv65Stake', 21, 0690, 1039, 236, 204, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6190, 'Lv65Stake', 21, 0690, 1039, 240, 200, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6191, 'Lv65Stake', 21, 0690, 1039, 244, 196, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6192, 'Lv65Stake', 21, 0690, 1039, 248, 192, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6193, 'Lv65Stake', 21, 0690, 1039, 252, 188, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6194, 'Lv65Stake', 21, 0690, 1039, 256, 184, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6195, 'Lv65Stake', 21, 0690, 1039, 260, 180, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6196, 'Lv65Stake', 21, 0690, 1039, 268, 172, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6197, 'Lv65Stake', 21, 0690, 1039, 272, 168, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6198, 'Lv65Stake', 21, 0690, 1039, 276, 164, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6199, 'Lv65Stake', 21, 0690, 1039, 280, 160, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6200, 'Lv65Stake', 21, 0690, 1039, 284, 156, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6201, 'Lv65Stake', 21, 0690, 1039, 288, 152, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6202, 'Lv65Stake', 21, 0690, 1039, 292, 148, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6203, 'Lv65Stake', 21, 0690, 1039, 300, 140, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6204, 'Lv65Stake', 21, 0690, 1039, 304, 136, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6205, 'Lv65Stake', 21, 0690, 1039, 308, 132, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6206, 'Lv65Stake', 21, 0690, 1039, 312, 128, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6207, 'Lv65Stake', 21, 0690, 1039, 316, 124, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6208, 'Lv65Stake', 21, 0690, 1039, 320, 120, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6209, 'Lv65Stake', 21, 0690, 1039, 324, 116, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6210, 'Lv70Stake', 21, 0720, 1039, 145, 345, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6211, 'Lv70Stake', 21, 0720, 1039, 149, 341, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6212, 'Lv70Stake', 21, 0720, 1039, 153, 337, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6213, 'Lv70Stake', 21, 0720, 1039, 157, 333, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6214, 'Lv70Stake', 21, 0720, 1039, 161, 329, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6215, 'Lv70Stake', 21, 0720, 1039, 165, 325, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6216, 'Lv70Stake', 21, 0720, 1039, 169, 321, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6217, 'Lv70Stake', 21, 0720, 1039, 177, 313, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6218, 'Lv70Stake', 21, 0720, 1039, 181, 309, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6219, 'Lv70Stake', 21, 0720, 1039, 185, 305, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6220, 'Lv70Stake', 21, 0720, 1039, 189, 301, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6221, 'Lv70Stake', 21, 0720, 1039, 193, 297, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6222, 'Lv70Stake', 21, 0720, 1039, 197, 293, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6223, 'Lv70Stake', 21, 0720, 1039, 201, 289, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6224, 'Lv70Stake', 21, 0720, 1039, 209, 281, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6225, 'Lv70Stake', 21, 0720, 1039, 213, 277, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6226, 'Lv70Stake', 21, 0720, 1039, 217, 273, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6227, 'Lv70Stake', 21, 0720, 1039, 221, 269, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6228, 'Lv70Stake', 21, 0720, 1039, 225, 265, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6229, 'Lv70Stake', 21, 0720, 1039, 229, 261, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6230, 'Lv70Stake', 21, 0720, 1039, 233, 257, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6231, 'Lv75Stake', 21, 0750, 1039, 261, 229, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6232, 'Lv75Stake', 21, 0750, 1039, 265, 225, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6233, 'Lv75Stake', 21, 0750, 1039, 269, 221, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6234, 'Lv75Stake', 21, 0750, 1039, 273, 217, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6235, 'Lv75Stake', 21, 0750, 1039, 277, 213, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6236, 'Lv75Stake', 21, 0750, 1039, 281, 209, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6237, 'Lv75Stake', 21, 0750, 1039, 285, 205, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6238, 'Lv75Stake', 21, 0750, 1039, 293, 197, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6239, 'Lv75Stake', 21, 0750, 1039, 297, 193, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6240, 'Lv75Stake', 21, 0750, 1039, 301, 189, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6241, 'Lv75Stake', 21, 0750, 1039, 305, 185, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6242, 'Lv75Stake', 21, 0750, 1039, 309, 181, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6243, 'Lv75Stake', 21, 0750, 1039, 313, 177, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6244, 'Lv75Stake', 21, 0750, 1039, 317, 173, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6245, 'Lv75Stake', 21, 0750, 1039, 325, 165, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6246, 'Lv75Stake', 21, 0750, 1039, 329, 161, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6247, 'Lv75Stake', 21, 0750, 1039, 333, 157, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6248, 'Lv75Stake', 21, 0750, 1039, 337, 153, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6249, 'Lv75Stake', 21, 0750, 1039, 341, 149, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6250, 'Lv75Stake', 21, 0750, 1039, 345, 145, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6251, 'Lv75Stake', 21, 0750, 1039, 349, 141, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6252, 'Lv80Stake', 21, 0780, 1039, 185, 385, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6253, 'Lv80Stake', 21, 0780, 1039, 189, 381, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6254, 'Lv80Stake', 21, 0780, 1039, 193, 377, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6255, 'Lv80Stake', 21, 0780, 1039, 197, 373, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6256, 'Lv80Stake', 21, 0780, 1039, 201, 369, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6257, 'Lv80Stake', 21, 0780, 1039, 205, 365, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6258, 'Lv80Stake', 21, 0780, 1039, 209, 361, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6259, 'Lv80Stake', 21, 0780, 1039, 217, 353, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6260, 'Lv80Stake', 21, 0780, 1039, 221, 349, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6261, 'Lv80Stake', 21, 0780, 1039, 225, 345, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6262, 'Lv80Stake', 21, 0780, 1039, 229, 341, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6263, 'Lv80Stake', 21, 0780, 1039, 233, 337, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6264, 'Lv80Stake', 21, 0780, 1039, 237, 333, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6265, 'Lv80Stake', 21, 0780, 1039, 241, 329, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6266, 'Lv80Stake', 21, 0780, 1039, 249, 321, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6267, 'Lv80Stake', 21, 0780, 1039, 253, 317, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6268, 'Lv80Stake', 21, 0780, 1039, 257, 313, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6269, 'Lv80Stake', 21, 0780, 1039, 261, 309, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6270, 'Lv80Stake', 21, 0780, 1039, 265, 305, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6271, 'Lv80Stake', 21, 0780, 1039, 269, 301, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6272, 'Lv80Stake', 21, 0780, 1039, 273, 297, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6273, 'Lv85Stake', 21, 0810, 1039, 301, 269, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6274, 'Lv85Stake', 21, 0810, 1039, 305, 265, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6275, 'Lv85Stake', 21, 0810, 1039, 309, 261, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6276, 'Lv85Stake', 21, 0810, 1039, 313, 257, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6277, 'Lv85Stake', 21, 0810, 1039, 317, 253, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6278, 'Lv85Stake', 21, 0810, 1039, 321, 249, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6279, 'Lv85Stake', 21, 0810, 1039, 325, 245, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6280, 'Lv85Stake', 21, 0810, 1039, 333, 237, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6281, 'Lv85Stake', 21, 0810, 1039, 337, 233, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6282, 'Lv85Stake', 21, 0810, 1039, 341, 229, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6283, 'Lv85Stake', 21, 0810, 1039, 345, 225, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6284, 'Lv85Stake', 21, 0810, 1039, 349, 221, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6285, 'Lv85Stake', 21, 0810, 1039, 353, 217, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6286, 'Lv85Stake', 21, 0810, 1039, 357, 213, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6287, 'Lv85Stake', 21, 0810, 1039, 365, 205, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6288, 'Lv85Stake', 21, 0810, 1039, 369, 201, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6289, 'Lv85Stake', 21, 0810, 1039, 373, 197, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6290, 'Lv85Stake', 21, 0810, 1039, 377, 193, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6291, 'Lv85Stake', 21, 0810, 1039, 381, 189, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6292, 'Lv85Stake', 21, 0810, 1039, 385, 185, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6293, 'Lv85Stake', 21, 0810, 1039, 389, 181, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6294, 'Lv90Stake', 21, 0840, 1039, 210, 410, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6295, 'Lv90Stake', 21, 0840, 1039, 214, 406, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6296, 'Lv90Stake', 21, 0840, 1039, 218, 402, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6297, 'Lv90Stake', 21, 0840, 1039, 222, 398, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6298, 'Lv90Stake', 21, 0840, 1039, 226, 394, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6299, 'Lv90Stake', 21, 0840, 1039, 230, 390, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6300, 'Lv90Stake', 21, 0840, 1039, 234, 386, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6301, 'Lv90Stake', 21, 0840, 1039, 242, 378, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6302, 'Lv90Stake', 21, 0840, 1039, 246, 374, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6303, 'Lv90Stake', 21, 0840, 1039, 250, 370, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6304, 'Lv90Stake', 21, 0840, 1039, 254, 366, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6305, 'Lv90Stake', 21, 0840, 1039, 258, 362, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6306, 'Lv90Stake', 21, 0840, 1039, 262, 358, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6307, 'Lv90Stake', 21, 0840, 1039, 266, 354, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6308, 'Lv90Stake', 21, 0840, 1039, 274, 346, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6309, 'Lv90Stake', 21, 0840, 1039, 278, 342, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6310, 'Lv90Stake', 21, 0840, 1039, 282, 338, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6311, 'Lv90Stake', 21, 0840, 1039, 286, 334, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6312, 'Lv90Stake', 21, 0840, 1039, 290, 330, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6313, 'Lv90Stake', 21, 0840, 1039, 294, 326, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6314, 'Lv90Stake', 21, 0840, 1039, 298, 322, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6315, 'Lv95Stake', 21, 0870, 1039, 326, 294, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6316, 'Lv95Stake', 21, 0870, 1039, 330, 290, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6317, 'Lv95Stake', 21, 0870, 1039, 334, 286, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6318, 'Lv95Stake', 21, 0870, 1039, 338, 282, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6319, 'Lv95Stake', 21, 0870, 1039, 342, 278, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6320, 'Lv95Stake', 21, 0870, 1039, 346, 274, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6321, 'Lv95Stake', 21, 0870, 1039, 350, 270, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6322, 'Lv95Stake', 21, 0870, 1039, 358, 262, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6323, 'Lv95Stake', 21, 0870, 1039, 362, 258, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6324, 'Lv95Stake', 21, 0870, 1039, 366, 254, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6325, 'Lv95Stake', 21, 0870, 1039, 370, 250, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6326, 'Lv95Stake', 21, 0870, 1039, 374, 246, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6327, 'Lv95Stake', 21, 0870, 1039, 378, 242, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6328, 'Lv95Stake', 21, 0870, 1039, 382, 238, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6329, 'Lv95Stake', 21, 0870, 1039, 390, 230, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6330, 'Lv95Stake', 21, 0870, 1039, 394, 226, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6331, 'Lv95Stake', 21, 0870, 1039, 398, 222, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6332, 'Lv95Stake', 21, 0870, 1039, 402, 218, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6333, 'Lv95Stake', 21, 0870, 1039, 406, 214, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6334, 'Lv95Stake', 21, 0870, 1039, 410, 210, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6335, 'Lv95Stake', 21, 0870, 1039, 414, 206, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6336, 'Lv100Stake', 21, 0900, 1039, 235, 435, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6337, 'Lv100Stake', 21, 0900, 1039, 239, 431, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6338, 'Lv100Stake', 21, 0900, 1039, 243, 427, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6339, 'Lv100Stake', 21, 0900, 1039, 247, 423, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6340, 'Lv100Stake', 21, 0900, 1039, 251, 419, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6341, 'Lv100Stake', 21, 0900, 1039, 255, 415, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6342, 'Lv100Stake', 21, 0900, 1039, 259, 411, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6343, 'Lv100Stake', 21, 0900, 1039, 267, 403, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6344, 'Lv100Stake', 21, 0900, 1039, 271, 399, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6345, 'Lv100Stake', 21, 0900, 1039, 275, 395, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6346, 'Lv100Stake', 21, 0900, 1039, 279, 391, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6347, 'Lv100Stake', 21, 0900, 1039, 283, 387, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6348, 'Lv100Stake', 21, 0900, 1039, 287, 383, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6349, 'Lv100Stake', 21, 0900, 1039, 291, 379, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6350, 'Lv100Stake', 21, 0900, 1039, 299, 371, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6351, 'Lv100Stake', 21, 0900, 1039, 303, 367, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6352, 'Lv100Stake', 21, 0900, 1039, 307, 363, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6353, 'Lv100Stake', 21, 0900, 1039, 311, 359, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6354, 'Lv100Stake', 21, 0900, 1039, 315, 355, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6355, 'Lv100Stake', 21, 0900, 1039, 319, 351, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6356, 'Lv100Stake', 21, 0900, 1039, 323, 347, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6357, 'Lv105Stake', 21, 0930, 1039, 351, 319, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6358, 'Lv105Stake', 21, 0930, 1039, 355, 315, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6359, 'Lv105Stake', 21, 0930, 1039, 359, 311, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6360, 'Lv105Stake', 21, 0930, 1039, 363, 307, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6361, 'Lv105Stake', 21, 0930, 1039, 367, 303, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6362, 'Lv105Stake', 21, 0930, 1039, 371, 299, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6363, 'Lv105Stake', 21, 0930, 1039, 375, 295, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6364, 'Lv105Stake', 21, 0930, 1039, 383, 287, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6365, 'Lv105Stake', 21, 0930, 1039, 387, 283, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6366, 'Lv105Stake', 21, 0930, 1039, 391, 279, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6367, 'Lv105Stake', 21, 0930, 1039, 395, 275, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6368, 'Lv105Stake', 21, 0930, 1039, 399, 271, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6369, 'Lv105Stake', 21, 0930, 1039, 403, 267, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6370, 'Lv105Stake', 21, 0930, 1039, 407, 263, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6371, 'Lv105Stake', 21, 0930, 1039, 415, 255, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6372, 'Lv105Stake', 21, 0930, 1039, 419, 251, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6373, 'Lv105Stake', 21, 0930, 1039, 423, 247, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6374, 'Lv105Stake', 21, 0930, 1039, 427, 243, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6375, 'Lv105Stake', 21, 0930, 1039, 431, 239, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6376, 'Lv105Stake', 21, 0930, 1039, 435, 235, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6377, 'Lv105Stake', 21, 0930, 1039, 439, 231, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6378, 'Lv110Stake', 21, 0960, 1039, 260, 460, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6379, 'Lv110Stake', 21, 0960, 1039, 264, 456, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6380, 'Lv110Stake', 21, 0960, 1039, 268, 452, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6381, 'Lv110Stake', 21, 0960, 1039, 272, 448, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6382, 'Lv110Stake', 21, 0960, 1039, 276, 444, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6383, 'Lv110Stake', 21, 0960, 1039, 280, 440, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6384, 'Lv110Stake', 21, 0960, 1039, 284, 436, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6385, 'Lv110Stake', 21, 0960, 1039, 292, 428, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6386, 'Lv110Stake', 21, 0960, 1039, 296, 424, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6387, 'Lv110Stake', 21, 0960, 1039, 300, 420, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6388, 'Lv110Stake', 21, 0960, 1039, 304, 416, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6389, 'Lv110Stake', 21, 0960, 1039, 308, 412, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6390, 'Lv110Stake', 21, 0960, 1039, 312, 408, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6391, 'Lv110Stake', 21, 0960, 1039, 316, 404, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6392, 'Lv110Stake', 21, 0960, 1039, 324, 396, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6393, 'Lv110Stake', 21, 0960, 1039, 328, 392, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6394, 'Lv110Stake', 21, 0960, 1039, 332, 388, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6395, 'Lv110Stake', 21, 0960, 1039, 336, 384, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6396, 'Lv110Stake', 21, 0960, 1039, 340, 380, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6397, 'Lv110Stake', 21, 0960, 1039, 344, 376, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6398, 'Lv110Stake', 21, 0960, 1039, 348, 372, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6399, 'Lv115Stake', 21, 0990, 1039, 376, 344, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6400, 'Lv115Stake', 21, 0990, 1039, 380, 340, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6401, 'Lv115Stake', 21, 0990, 1039, 384, 336, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6402, 'Lv115Stake', 21, 0990, 1039, 388, 332, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6403, 'Lv115Stake', 21, 0990, 1039, 392, 328, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6404, 'Lv115Stake', 21, 0990, 1039, 396, 324, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6405, 'Lv115Stake', 21, 0990, 1039, 400, 320, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6406, 'Lv115Stake', 21, 0990, 1039, 408, 312, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6407, 'Lv115Stake', 21, 0990, 1039, 412, 308, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6408, 'Lv115Stake', 21, 0990, 1039, 416, 304, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6409, 'Lv115Stake', 21, 0990, 1039, 420, 300, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6410, 'Lv115Stake', 21, 0990, 1039, 424, 296, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6411, 'Lv115Stake', 21, 0990, 1039, 428, 292, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6412, 'Lv115Stake', 21, 0990, 1039, 432, 288, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6413, 'Lv115Stake', 21, 0990, 1039, 440, 280, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6414, 'Lv115Stake', 21, 0990, 1039, 444, 276, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6415, 'Lv115Stake', 21, 0990, 1039, 448, 272, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6416, 'Lv115Stake', 21, 0990, 1039, 452, 268, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6417, 'Lv115Stake', 21, 0990, 1039, 456, 264, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6418, 'Lv115Stake', 21, 0990, 1039, 460, 260, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6419, 'Lv115Stake', 21, 0990, 1039, 464, 256, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6420, 'Lv120Stake', 21, 1020, 1039, 285, 485, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6421, 'Lv120Stake', 21, 1020, 1039, 289, 481, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6422, 'Lv120Stake', 21, 1020, 1039, 293, 477, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6423, 'Lv120Stake', 21, 1020, 1039, 297, 473, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6424, 'Lv120Stake', 21, 1020, 1039, 301, 469, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6425, 'Lv120Stake', 21, 1020, 1039, 305, 465, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6426, 'Lv120Stake', 21, 1020, 1039, 309, 461, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6427, 'Lv120Stake', 21, 1020, 1039, 317, 453, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6428, 'Lv120Stake', 21, 1020, 1039, 321, 449, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6429, 'Lv120Stake', 21, 1020, 1039, 325, 445, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6430, 'Lv120Stake', 21, 1020, 1039, 329, 441, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6431, 'Lv120Stake', 21, 1020, 1039, 333, 437, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6432, 'Lv120Stake', 21, 1020, 1039, 337, 433, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6433, 'Lv120Stake', 21, 1020, 1039, 341, 429, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6434, 'Lv120Stake', 21, 1020, 1039, 349, 421, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6435, 'Lv120Stake', 21, 1020, 1039, 353, 417, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6436, 'Lv120Stake', 21, 1020, 1039, 357, 413, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6437, 'Lv120Stake', 21, 1020, 1039, 361, 409, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6438, 'Lv120Stake', 21, 1020, 1039, 365, 405, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6439, 'Lv120Stake', 21, 1020, 1039, 369, 401, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6440, 'Lv120Stake', 21, 1020, 1039, 373, 397, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6441, 'Lv125Stake', 21, 1500, 1039, 401, 369, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6442, 'Lv125Stake', 21, 1500, 1039, 405, 365, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6443, 'Lv125Stake', 21, 1500, 1039, 409, 361, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6444, 'Lv125Stake', 21, 1500, 1039, 413, 357, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6445, 'Lv125Stake', 21, 1500, 1039, 417, 353, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6446, 'Lv125Stake', 21, 1500, 1039, 421, 349, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6447, 'Lv125Stake', 21, 1500, 1039, 425, 345, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6448, 'Lv125Stake', 21, 1500, 1039, 433, 337, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6449, 'Lv125Stake', 21, 1500, 1039, 437, 333, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6450, 'Lv125Stake', 21, 1500, 1039, 441, 329, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6451, 'Lv125Stake', 21, 1500, 1039, 445, 325, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6452, 'Lv125Stake', 21, 1500, 1039, 449, 321, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6453, 'Lv125Stake', 21, 1500, 1039, 453, 317, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6454, 'Lv125Stake', 21, 1500, 1039, 457, 313, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6455, 'Lv125Stake', 21, 1500, 1039, 465, 305, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6456, 'Lv125Stake', 21, 1500, 1039, 469, 301, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6457, 'Lv125Stake', 21, 1500, 1039, 473, 297, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6458, 'Lv125Stake', 21, 1500, 1039, 477, 293, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6459, 'Lv125Stake', 21, 1500, 1039, 481, 289, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6460, 'Lv125Stake', 21, 1500, 1039, 485, 285, 200000, 200000, 0000, 17, 0, 0, '');
INSERT INTO `sobnpcs` VALUES (6461, 'Lv125Stake', 21, 1500, 1039, 489, 281, 200000, 200000, 0000, 17, 0, 0, '');

-- ----------------------------
-- Table structure for spells
-- ----------------------------
DROP TABLE IF EXISTS `spells`;
CREATE TABLE `spells`  (
  `id` int(4) UNSIGNED ZEROFILL NOT NULL AUTO_INCREMENT,
  `type` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `sort` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `name` varchar(30) CHARACTER SET latin1 COLLATE latin1_bin NOT NULL DEFAULT '',
  `crime` tinyint(1) UNSIGNED ZEROFILL NOT NULL DEFAULT 0,
  `ground` tinyint(1) UNSIGNED ZEROFILL NOT NULL DEFAULT 0,
  `multi` tinyint(1) UNSIGNED ZEROFILL NOT NULL DEFAULT 0,
  `target` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `level` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `use_mp` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `power` int(4) NOT NULL DEFAULT 0,
  `intone_speed` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `percent` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0100,
  `step_secs` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `range` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `distance` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `status` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `need_prof` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `need_exp` int(4) NOT NULL DEFAULT 0,
  `need_level` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `use_xp` tinyint(1) UNSIGNED ZEROFILL NOT NULL DEFAULT 0,
  `weapon_subtype` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `active_times` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `auto_active` tinyint(1) UNSIGNED ZEROFILL NOT NULL DEFAULT 0,
  `floor_attr` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `auto_learn` tinyint(1) UNSIGNED ZEROFILL NOT NULL DEFAULT 0,
  `learn_level` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `drop_weapon` tinyint(1) UNSIGNED ZEROFILL NOT NULL DEFAULT 0,
  `use_ep` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `weapon_hit` tinyint(1) UNSIGNED ZEROFILL NOT NULL DEFAULT 0,
  `use_item` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `next_magic` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `delay_ms` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0000,
  `use_item_num` int(4) UNSIGNED ZEROFILL NOT NULL DEFAULT 0001,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 12777 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of spells
-- ----------------------------
INSERT INTO `spells` VALUES (1000, 1000, 0001, 'Thunder', 1, 0, 0, 0000, 0000, 0001, 7, 0000, 0100, 0000, 0000, 0009, 0001, 0000, 2000, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 1000, 0000, 0001);
INSERT INTO `spells` VALUES (1001, 1000, 0001, 'Thunder', 1, 0, 0, 0000, 0001, 0006, 16, 0000, 0100, 0000, 0000, 0009, 0001, 0000, 113060, 0010, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 1000, 0000, 0001);
INSERT INTO `spells` VALUES (1002, 1000, 0001, 'Thunder', 1, 0, 0, 0000, 0002, 0010, 32, 0000, 0100, 0000, 0000, 0010, 0001, 0000, 326107, 0020, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 1000, 0000, 0001);
INSERT INTO `spells` VALUES (1003, 1000, 0001, 'Thunder', 1, 0, 0, 0000, 0003, 0011, 57, 0000, 0100, 0000, 0000, 0010, 0001, 0000, 777950, 0030, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 1000, 0000, 0001);
INSERT INTO `spells` VALUES (1004, 1000, 0001, 'Thunder', 1, 0, 0, 0000, 0004, 0017, 86, 0000, 0100, 0000, 0000, 0011, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 1000, 0000, 0001);
INSERT INTO `spells` VALUES (1005, 1001, 0001, 'Fire', 1, 0, 0, 0000, 0000, 0021, 130, 0000, 0100, 0000, 0000, 0012, 0001, 0000, 4859520, 0040, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 1001, 0000, 0001);
INSERT INTO `spells` VALUES (1006, 1001, 0001, 'Fire', 1, 0, 0, 0000, 0001, 0021, 189, 0000, 0100, 0000, 0000, 0013, 0001, 0000, 11231517, 0060, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 1001, 0000, 0001);
INSERT INTO `spells` VALUES (1007, 1001, 0001, 'Fire', 1, 0, 0, 0000, 0002, 0028, 275, 0000, 0100, 0000, 0000, 0014, 0001, 0000, 26497813, 0070, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 1001, 0000, 0001);
INSERT INTO `spells` VALUES (1008, 1001, 0001, 'Fire', 1, 0, 0, 0000, 0003, 0032, 380, 0000, 0100, 0000, 0000, 0014, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 1001, 0000, 0001);
INSERT INTO `spells` VALUES (1020, 1005, 0002, 'Cure', 0, 0, 0, 0001, 0000, 0010, 20, 0000, 0100, 0000, 0000, 0010, 0001, 0000, 2000, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1021, 1005, 0002, 'Cure', 0, 0, 0, 0001, 0001, 0030, 70, 0000, 0100, 0000, 0000, 0010, 0001, 0000, 12000, 0010, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1022, 1005, 0002, 'Cure', 0, 0, 0, 0001, 0002, 0060, 150, 0000, 0100, 0000, 0000, 0012, 0001, 0000, 30000, 0020, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1023, 1005, 0002, 'Cure', 0, 0, 0, 0001, 0003, 0100, 280, 0000, 0100, 0000, 0000, 0012, 0001, 0000, 64000, 0030, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1024, 1005, 0002, 'Cure', 0, 0, 0, 0001, 0004, 0130, 400, 0000, 0100, 0000, 0000, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1025, 1010, 0005, 'Lightning', 1, 0, 1, 0000, 0000, 0000, 50, 0000, 0100, 0000, 0009, 0009, 0001, 0000, 0, 0003, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1026, 1015, 0006, 'Accuracy', 0, 0, 0, 0002, 0000, 0000, 30300, 0000, 0100, 0120, 0000, 0000, 0005, 0000, 0, 0015, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1027, 1020, 0006, 'Shield', 0, 0, 0, 0002, 0000, 0000, 30300, 0000, 0100, 0120, 0000, 0000, 0044, 0000, 0, 0015, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1028, 1025, 0006, 'Superman', 0, 0, 0, 0002, 0000, 0000, 31000, 0000, 0100, 0020, 0000, 0000, 0013, 0000, 0, 0003, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1029, 1002, 0001, 'Tornado', 1, 0, 0, 0000, 0000, 0032, 505, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 118246825, 0090, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 1002, 0000, 0001);
INSERT INTO `spells` VALUES (1030, 1002, 0001, 'Tornado', 1, 0, 0, 0000, 0001, 0036, 666, 0000, 0100, 0000, 0000, 0017, 0001, 0000, 277035437, 0100, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 1002, 0000, 0001);
INSERT INTO `spells` VALUES (1031, 1002, 0001, 'Tornado', 1, 0, 0, 0000, 0002, 0050, 882, 0000, 0100, 0000, 0000, 0019, 0001, 0000, 920692259, 0110, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 1002, 0000, 0001);
INSERT INTO `spells` VALUES (1032, 1002, 0001, 'Tornado', 1, 0, 0, 0000, 0003, 0064, 1166, 0000, 0100, 0000, 0000, 0021, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 1002, 0000, 0001);
INSERT INTO `spells` VALUES (1036, 1040, 0011, 'Roar', 0, 0, 0, 0002, 0000, 0000, 0, 0000, 0100, 0000, 0000, 0000, 0001, 0000, 0, 0040, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1037, 1045, 0014, 'FastBlade', 1, 1, 0, 0004, 0000, 0000, 30110, 0000, 0100, 0000, 0004, 0010, 0001, 0000, 100000, 0040, 2, 614410, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1038, 1046, 0014, 'ScentSword', 1, 1, 0, 0004, 0000, 0000, 30110, 0000, 0100, 0000, 0004, 0010, 0001, 0000, 100000, 0040, 2, 614420620, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1040, 1125, 0005, 'Volcano', 1, 0, 1, 0000, 0000, 0000, 300, 0000, 0100, 0000, 0012, 0012, 0001, 0000, 0, 0040, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1050, 1050, 0007, 'Revive', 0, 0, 0, 0000, 0000, 0000, 0, 0000, 0100, 0000, 0000, 0012, 0000, 0000, 0, 0000, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1051, 1051, 0012, 'Dash', 1, 1, 0, 0000, 0000, 0000, 30200, 0000, 0100, 0000, 0000, 0001, 0001, 0000, 0, 0000, 2, 0900, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1055, 1055, 0002, 'HealingRain', 0, 0, 1, 0001, 0000, 0150, 100, 0000, 0100, 0000, 0000, 0010, 0001, 0000, 27300, 0040, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1056, 1055, 0002, 'HealingRain', 0, 0, 1, 0001, 0001, 0270, 200, 0000, 0100, 0000, 0000, 0010, 0001, 0000, 121300, 0050, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1057, 1055, 0002, 'HealingRain', 0, 0, 1, 0001, 0002, 0375, 300, 0000, 0100, 0000, 0000, 0010, 0001, 0000, 283900, 0060, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1058, 1055, 0002, 'HealingRain', 0, 0, 1, 0001, 0003, 0440, 400, 0000, 0100, 0000, 0000, 0010, 0001, 0000, 595600, 0070, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1059, 1055, 0002, 'HealingRain', 0, 0, 1, 0001, 0004, 0500, 500, 0000, 0100, 0000, 0000, 0010, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1075, 1075, 0006, 'Invisibility', 0, 0, 0, 0001, 0000, 0200, 1, 0000, 0100, 0020, 0000, 0014, 0017, 0000, 720, 0060, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1076, 1075, 0006, 'Invisibility', 0, 0, 0, 0001, 0001, 0250, 1, 0000, 0100, 0030, 0000, 0014, 0017, 0000, 860, 0070, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1077, 1075, 0006, 'Invisibility', 0, 0, 0, 0001, 0002, 0300, 1, 0000, 0100, 0040, 0000, 0014, 0017, 0000, 960, 0080, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1078, 1075, 0006, 'Invisibility', 0, 0, 0, 0001, 0003, 0330, 1, 0000, 0100, 0050, 0000, 0014, 0017, 0000, 1010, 0090, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1079, 1075, 0006, 'Invisibility', 0, 0, 0, 0001, 0004, 0360, 1, 0000, 0100, 0060, 0000, 0014, 0017, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1085, 1085, 0006, 'StarofAccuracy', 0, 0, 0, 0001, 0000, 0200, 30110, 0000, 0100, 0060, 0000, 0010, 0005, 0000, 430, 0040, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1086, 1085, 0006, 'StarofAccuracy', 0, 0, 0, 0001, 0001, 0250, 30115, 0000, 0100, 0065, 0000, 0011, 0005, 0000, 520, 0040, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1087, 1085, 0006, 'StarofAccuracy', 0, 0, 0, 0001, 0002, 0300, 30120, 0000, 0100, 0070, 0000, 0012, 0005, 0000, 570, 0050, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1088, 1085, 0006, 'StarofAccuracy', 0, 0, 0, 0001, 0003, 0350, 30125, 0000, 0100, 0075, 0000, 0013, 0005, 0000, 620, 0060, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1089, 1085, 0006, 'StarofAccuracy', 0, 0, 0, 0001, 0004, 0400, 30130, 0000, 0100, 0080, 0000, 0014, 0005, 0000, 0, 0070, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1090, 1090, 0006, 'MagicShield', 0, 0, 0, 0001, 0000, 0200, 30110, 0000, 0100, 0060, 0000, 0010, 0006, 0000, 430, 0040, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1091, 1090, 0006, 'MagicShield', 0, 0, 0, 0001, 0001, 0250, 30115, 0000, 0100, 0065, 0000, 0011, 0006, 0000, 520, 0040, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1092, 1090, 0006, 'MagicShield', 0, 0, 0, 0001, 0002, 0300, 30120, 0000, 0100, 0070, 0000, 0012, 0006, 0000, 570, 0050, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1093, 1090, 0006, 'MagicShield', 0, 0, 0, 0001, 0003, 0350, 30125, 0000, 0100, 0075, 0000, 0013, 0006, 0000, 620, 0060, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1094, 1090, 0006, 'MagicShield', 0, 0, 0, 0001, 0004, 0400, 30130, 0000, 0100, 0080, 0000, 0014, 0006, 0000, 0, 0070, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1095, 1095, 0006, 'Stigma', 0, 0, 0, 0001, 0000, 0200, 30110, 0000, 0100, 0060, 0000, 0010, 0007, 0000, 430, 0040, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1096, 1095, 0006, 'Stigma', 0, 0, 0, 0001, 0001, 0250, 30115, 0000, 0100, 0065, 0000, 0011, 0007, 0000, 520, 0040, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1097, 1095, 0006, 'Stigma', 0, 0, 0, 0001, 0002, 0300, 30120, 0000, 0100, 0070, 0000, 0012, 0007, 0000, 570, 0050, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1098, 1095, 0006, 'Stigma', 0, 0, 0, 0001, 0003, 0350, 30125, 0000, 0100, 0075, 0000, 0013, 0007, 0000, 620, 0060, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1099, 1095, 0006, 'Stigma', 0, 0, 0, 0001, 0004, 0400, 30130, 0000, 0100, 0080, 0000, 0014, 0007, 0000, 0, 0070, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1100, 1100, 0007, 'Pray', 0, 0, 0, 0000, 0000, 1000, 0, 0000, 0100, 0000, 0000, 0012, 0000, 0000, 0, 0070, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1105, 1105, 0002, 'Restore', 0, 0, 0, 0002, 0000, 0000, 32768, 0000, 0100, 0000, 0000, 0012, 0001, 0000, 0, 0000, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1110, 1110, 0006, 'Cyclone', 0, 0, 0, 0002, 0000, 0000, 30020, 0000, 0100, 0020, 0000, 0000, 0018, 0000, 0, 0003, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1115, 1045, 0014, 'FastBlade', 1, 1, 0, 0004, 0001, 0000, 30115, 0000, 0100, 0000, 0005, 0010, 0001, 0000, 300000, 0050, 2, 614410, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1116, 1045, 0014, 'FastBlade', 1, 1, 0, 0004, 0002, 0000, 30120, 0000, 0100, 0000, 0006, 0010, 0001, 0000, 741000, 0060, 2, 614410, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1117, 1045, 0014, 'FastBlade', 1, 1, 0, 0004, 0003, 0000, 30125, 0000, 0100, 0000, 0007, 0010, 0001, 0000, 1440000, 0070, 2, 614410, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1118, 1045, 0014, 'FastBlade', 1, 1, 0, 0004, 0004, 0000, 30130, 0000, 0100, 0000, 0008, 0010, 0001, 0000, 0, 0000, 2, 614410, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1120, 1046, 0014, 'ScentSword', 1, 1, 0, 0004, 0001, 0000, 30115, 0000, 0100, 0000, 0005, 0010, 0001, 0000, 300000, 0050, 2, 614420620, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1121, 1046, 0014, 'ScentSword', 1, 1, 0, 0004, 0002, 0000, 30120, 0000, 0100, 0000, 0006, 0010, 0001, 0000, 741000, 0060, 2, 614420620, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1122, 1046, 0014, 'ScentSword', 1, 1, 0, 0004, 0003, 0000, 30125, 0000, 0100, 0000, 0007, 0010, 0001, 0000, 1440000, 0070, 2, 614420620, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1123, 1046, 0014, 'ScentSword', 1, 1, 0, 0004, 0004, 0000, 30130, 0000, 0100, 0000, 0008, 0010, 0001, 0000, 0, 0000, 2, 614420620, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1130, 1115, 0005, 'Hercules', 1, 1, 0, 0000, 0000, 0000, 30085, 0000, 0100, 0000, 0003, 0010, 0001, 0000, 167600, 0040, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1131, 1115, 0005, 'Hercules', 1, 1, 0, 0000, 0001, 0000, 30085, 0000, 0100, 0000, 0004, 0010, 0001, 0000, 590000, 0050, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1132, 1115, 0005, 'Hercules', 1, 1, 0, 0000, 0002, 0000, 30085, 0000, 0100, 0000, 0005, 0010, 0001, 0000, 1216800, 0060, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1133, 1115, 0005, 'Hercules', 1, 1, 0, 0000, 0003, 0000, 30090, 0000, 0100, 0000, 0005, 0010, 0001, 0000, 2948000, 0070, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1134, 1115, 0005, 'Hercules', 1, 1, 0, 0000, 0004, 0000, 30090, 0000, 0100, 0000, 0006, 0010, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1135, 1120, 0005, 'FireCircle', 1, 1, 0, 0004, 0000, 0150, 540, 0000, 0100, 0000, 0008, 0007, 0001, 0000, 53104696, 0065, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1136, 1120, 0005, 'FireCircle', 1, 1, 0, 0004, 0001, 0170, 650, 0000, 0100, 0000, 0009, 0008, 0001, 0000, 98875022, 0075, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1137, 1120, 0005, 'FireCircle', 1, 1, 0, 0004, 0002, 0190, 720, 0000, 0100, 0000, 0010, 0009, 0001, 0000, 180034734, 0085, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1138, 1120, 0005, 'FireCircle', 1, 1, 0, 0004, 0003, 0210, 770, 0000, 0100, 0000, 0012, 0010, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1150, 1150, 0001, 'FireRing', 1, 0, 0, 0000, 0000, 0032, 378, 0000, 0080, 0000, 0000, 0009, 0001, 0000, 195000, 0055, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1151, 1150, 0001, 'FireRing', 1, 0, 0, 0000, 0001, 0042, 550, 0000, 0080, 0000, 0000, 0009, 0001, 0000, 339600, 0065, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1152, 1150, 0001, 'FireRing', 1, 0, 0, 0000, 0002, 0048, 760, 0000, 0080, 0000, 0000, 0010, 0001, 0000, 576800, 0075, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1153, 1150, 0001, 'FireRing', 1, 0, 0, 0000, 0003, 0048, 1010, 0000, 0080, 0000, 0000, 0010, 0001, 0000, 912000, 0085, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1154, 1150, 0001, 'FireBall', 1, 0, 0, 0000, 0004, 0054, 1332, 0000, 0080, 0000, 0000, 0011, 0001, 0000, 1362600, 0095, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1155, 1150, 0001, 'FireBall', 1, 0, 0, 0000, 0005, 0075, 1764, 0000, 0080, 0000, 0000, 0012, 0001, 0000, 1998000, 0105, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1156, 1150, 0001, 'FireBall', 1, 0, 0, 0000, 0006, 0096, 2332, 0000, 0080, 0000, 0000, 0013, 0001, 0000, 3307500, 0115, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1157, 1150, 0001, 'FireBall', 1, 0, 0, 0000, 0007, 0120, 2800, 0000, 0080, 0000, 0000, 0014, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1160, 1160, 0001, 'Bomb', 1, 0, 0, 0000, 0000, 0053, 855, 0000, 0080, 0000, 0000, 0011, 0001, 0000, 1282500, 0082, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1161, 1160, 0001, 'Bomb', 1, 0, 0, 0000, 0001, 0060, 1498, 0000, 0080, 0000, 0000, 0012, 0001, 0000, 2696400, 0102, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1162, 1160, 0001, 'Bomb', 1, 0, 0, 0000, 0002, 0082, 1985, 0000, 0080, 0000, 0000, 0013, 0001, 0000, 3970000, 0112, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1163, 1160, 0001, 'Bomb', 1, 0, 0, 0000, 0003, 0105, 2623, 0000, 0080, 0000, 0000, 0014, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1165, 1165, 0005, 'FireofHell', 1, 0, 1, 0000, 0000, 0120, 180, 0000, 0100, 0000, 0003, 0006, 0001, 0000, 1282500, 0084, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1166, 1165, 0005, 'FireofHell', 1, 0, 1, 0000, 0001, 0150, 240, 0000, 0100, 0000, 0004, 0007, 0001, 0000, 2696400, 0106, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1167, 1165, 0005, 'FireofHell', 1, 0, 1, 0000, 0002, 0180, 310, 0000, 0100, 0000, 0005, 0008, 0001, 0000, 3970000, 0113, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1168, 1165, 0005, 'FireofHell', 1, 0, 1, 0000, 0003, 0210, 400, 0000, 0100, 0000, 0006, 0009, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1170, 1170, 0002, 'Nectar', 0, 0, 1, 0001, 0000, 0600, 600, 0000, 0100, 0000, 0000, 0010, 0001, 0000, 163600, 0094, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1171, 1170, 0002, 'Nectar', 0, 0, 1, 0001, 0001, 0660, 700, 0000, 0100, 0000, 0000, 0010, 0001, 0000, 303700, 0104, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1172, 1170, 0002, 'Nectar', 0, 0, 1, 0001, 0002, 0720, 800, 0000, 0100, 0000, 0000, 0010, 0001, 0000, 727200, 0114, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1173, 1170, 0002, 'Nectar', 0, 0, 1, 0001, 0003, 0770, 900, 0000, 0100, 0000, 0000, 0010, 0001, 0000, 1075800, 0118, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1174, 1170, 0002, 'Nectar', 0, 0, 1, 0001, 0004, 0820, 1000, 0000, 0100, 0000, 0000, 0010, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1175, 1175, 0002, 'AdvancedCure', 0, 0, 0, 0001, 0000, 0160, 500, 0000, 0100, 0000, 0000, 0010, 0001, 0000, 500000, 0081, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1176, 1175, 0002, 'AdvancedCure', 0, 0, 0, 0001, 0001, 0190, 600, 0000, 0100, 0000, 0000, 0010, 0001, 0000, 720000, 0091, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1177, 1175, 0002, 'AdvancedCure', 0, 0, 0, 0001, 0002, 0215, 700, 0000, 0100, 0000, 0000, 0012, 0001, 0000, 980000, 0101, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1178, 1175, 0002, 'AdvancedCure', 0, 0, 0, 0001, 0003, 0235, 800, 0000, 0100, 0000, 0000, 0012, 0001, 0000, 1280000, 0111, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1179, 1175, 0002, 'AdvancedCure', 0, 0, 0, 0001, 0004, 0255, 900, 0000, 0100, 0000, 0000, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1192, 1180, 0001, 'FireMeteor', 1, 0, 0, 0000, 0000, 0062, 760, 0000, 0040, 0000, 0000, 0010, 0001, 0000, 8631000, 0052, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1193, 1180, 0001, 'FireMeteor', 1, 0, 0, 0000, 0001, 0074, 1040, 0000, 0043, 0000, 0000, 0011, 0001, 0000, 13981500, 0062, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1194, 1180, 0001, 'FireMeteor', 1, 0, 0, 0000, 0002, 0115, 1250, 0000, 0050, 0000, 0000, 0012, 0001, 0000, 24242000, 0072, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1195, 1180, 0001, 'FireMeteor', 1, 0, 0, 0000, 0003, 0130, 1480, 0000, 0055, 0000, 0000, 0013, 0001, 0000, 38661200, 0082, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1196, 1180, 0001, 'FireMeteor', 1, 0, 0, 0000, 0004, 0150, 1810, 0000, 0060, 0000, 0000, 0014, 0001, 0000, 67900000, 0092, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1197, 1180, 0001, 'FireMeteor', 1, 0, 0, 0000, 0005, 0215, 2210, 0000, 0065, 0000, 0000, 0015, 0001, 0000, 269625000, 0105, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1198, 1180, 0001, 'FireMeteor', 1, 0, 0, 0000, 0006, 0285, 2700, 0000, 0070, 0000, 0000, 0015, 0001, 0000, 815850000, 0115, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1199, 1180, 0001, 'FireMeteor', 1, 0, 0, 0000, 0007, 0300, 3250, 0000, 0075, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1200, 1190, 0002, 'SpiritHealing', 0, 0, 0, 0002, 0000, 0000, 500, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 560000, 0040, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1201, 1190, 0002, 'SpiritHealing', 0, 0, 0, 0002, 0001, 0000, 800, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 4000000, 0070, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1202, 1190, 0002, 'SpiritHealing', 0, 0, 0, 0002, 0002, 0000, 1300, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1205, 1195, 0020, 'Meditation', 0, 0, 0, 0002, 0000, 0000, 310, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 537140, 0044, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1206, 1195, 0020, 'Meditation', 0, 0, 0, 0002, 0001, 0000, 600, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 918542, 0074, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1207, 1195, 0020, 'Meditation', 0, 0, 0, 0002, 0002, 0000, 1020, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1250, 1250, 0004, 'WideStrike', 1, 1, 0, 0008, 0000, 0000, 30100, 0000, 0020, 0000, 0003, 0009, 0001, 0000, 20243, 0015, 2, 0510, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1251, 1250, 0004, 'WideStrike', 1, 1, 0, 0008, 0001, 0000, 30105, 0000, 0022, 0000, 0003, 0009, 0001, 0000, 37056, 0030, 2, 0510, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1252, 1250, 0004, 'WideStrike', 1, 1, 0, 0008, 0002, 0000, 30110, 0000, 0024, 0000, 0004, 0009, 0001, 0000, 66011, 0045, 2, 0510, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1253, 1250, 0004, 'WideStrike', 1, 1, 0, 0008, 0003, 0000, 30120, 0000, 0026, 0000, 0004, 0009, 0001, 0000, 116140, 0060, 2, 0510, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1254, 1250, 0004, 'WideStrike', 1, 1, 0, 0008, 0004, 0000, 30130, 0000, 0028, 0000, 0005, 0009, 0001, 0000, 192800, 0070, 2, 0510, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1255, 1250, 0004, 'WideStrike', 1, 1, 0, 0008, 0005, 0000, 30140, 0000, 0030, 0000, 0005, 0009, 0001, 0000, 418030, 0080, 2, 0510, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1256, 1250, 0004, 'WideStrike', 1, 1, 0, 0008, 0006, 0000, 30150, 0000, 0032, 0000, 0006, 0009, 0001, 0000, 454350, 0090, 2, 0510, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1257, 1250, 0004, 'WideStrike', 1, 1, 0, 0008, 0007, 0000, 30160, 0000, 0034, 0000, 0006, 0009, 0001, 0000, 491200, 0100, 2, 0510, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1258, 1250, 0004, 'WideStrike', 1, 1, 0, 0008, 0008, 0000, 30170, 0000, 0036, 0000, 0006, 0009, 0001, 0000, 520030, 0110, 2, 0510, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1259, 1250, 0004, 'WideStrike', 1, 1, 0, 0008, 0009, 0000, 30180, 0000, 0038, 0000, 0006, 0009, 0001, 0000, 0, 0000, 2, 0510, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1260, 1260, 0014, 'SpeedGun', 1, 1, 0, 0008, 0000, 0000, 30100, 0000, 0020, 0000, 0003, 0009, 0001, 0000, 20243, 0015, 2, 0560, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1261, 1260, 0014, 'SpeedGun', 1, 1, 0, 0008, 0001, 0000, 30115, 0000, 0023, 0000, 0003, 0009, 0001, 0000, 37056, 0030, 2, 0560, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1262, 1260, 0014, 'SpeedGun', 1, 1, 0, 0008, 0002, 0000, 30125, 0000, 0026, 0000, 0004, 0009, 0001, 0000, 66011, 0045, 2, 0560, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1263, 1260, 0014, 'SpeedGun', 1, 1, 0, 0008, 0003, 0000, 30140, 0000, 0029, 0000, 0004, 0009, 0001, 0000, 116140, 0060, 2, 0560, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1264, 1260, 0014, 'SpeedGun', 1, 1, 0, 0008, 0004, 0000, 30150, 0000, 0032, 0000, 0005, 0009, 0001, 0000, 192800, 0070, 2, 0560, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1265, 1260, 0014, 'SpeedGun', 1, 1, 0, 0008, 0005, 0000, 30165, 0000, 0035, 0000, 0005, 0009, 0001, 0000, 418030, 0080, 2, 0560, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1266, 1260, 0014, 'SpeedGun', 1, 1, 0, 0008, 0006, 0000, 30175, 0000, 0038, 0000, 0006, 0009, 0001, 0000, 454350, 0090, 2, 0560, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1267, 1260, 0014, 'SpeedGun', 1, 1, 0, 0008, 0007, 0000, 30190, 0000, 0041, 0000, 0006, 0009, 0001, 0000, 491200, 0100, 2, 0560, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1268, 1260, 0014, 'SpeedGun', 1, 1, 0, 0008, 0008, 0000, 30200, 0000, 0044, 0000, 0007, 0009, 0001, 0000, 520030, 0110, 2, 0560, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1269, 1260, 0014, 'SpeedGun', 1, 1, 0, 0008, 0009, 0000, 30210, 0000, 0047, 0000, 0007, 0009, 0001, 0000, 0, 0000, 2, 0560, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1270, 1270, 0019, 'Golem', 0, 0, 0, 0002, 0000, 0000, 2000, 0000, 0100, 0035, 0000, 0005, 0001, 0000, 60, 0041, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1271, 1270, 0019, 'Golem', 0, 0, 0, 0002, 0001, 0000, 2001, 0000, 0100, 0040, 0000, 0005, 0001, 0000, 100, 0046, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1272, 1270, 0019, 'Golem', 0, 0, 0, 0002, 0002, 0000, 2002, 0000, 0100, 0045, 0000, 0005, 0001, 0000, 140, 0051, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1273, 1270, 0019, 'Golem', 0, 0, 0, 0002, 0003, 0000, 2003, 0000, 0100, 0050, 0000, 0005, 0001, 0000, 240, 0061, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1274, 1270, 0019, 'Golem', 0, 0, 0, 0002, 0004, 0000, 2010, 0000, 0100, 0055, 0000, 0005, 0001, 0000, 370, 0071, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1275, 1270, 0019, 'Golem', 0, 0, 0, 0002, 0005, 0000, 2011, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 600, 0081, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1276, 1270, 0019, 'Golem', 0, 0, 0, 0002, 0006, 0000, 2012, 0000, 0100, 0065, 0000, 0005, 0001, 0000, 1050, 0091, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1277, 1270, 0019, 'Golem', 0, 0, 0, 0002, 0007, 0000, 2013, 0000, 0100, 0070, 0000, 0005, 0001, 0000, 0, 0000, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1280, 1280, 0019, 'WaterElf', 0, 0, 0, 0002, 0000, 0000, 2005, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 80, 0055, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1281, 1280, 0019, 'WaterElf', 0, 0, 0, 0002, 0001, 0000, 2006, 0000, 0100, 0035, 0000, 0005, 0001, 0000, 100, 0060, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1282, 1280, 0019, 'WaterElf', 0, 0, 0, 0002, 0002, 0000, 2007, 0000, 0100, 0040, 0000, 0005, 0001, 0000, 120, 0065, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1283, 1280, 0019, 'WaterElf', 0, 0, 0, 0002, 0003, 0000, 2008, 0000, 0100, 0045, 0000, 0005, 0001, 0000, 150, 0070, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1284, 1280, 0019, 'WaterElf', 0, 0, 0, 0002, 0004, 0000, 2009, 0000, 0100, 0050, 0000, 0005, 0001, 0000, 840, 0080, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1285, 1280, 0019, 'WaterElf', 0, 0, 0, 0002, 0005, 0000, 2040, 0000, 0100, 0055, 0000, 0005, 0001, 0000, 1340, 0090, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1286, 1280, 0019, 'WaterElf', 0, 0, 0, 0002, 0006, 0000, 2041, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 2080, 0100, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1287, 1280, 0019, 'WaterElf', 0, 0, 0, 0002, 0007, 0000, 2042, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 3490, 0110, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1288, 1280, 0019, 'WaterElf', 0, 0, 0, 0002, 0008, 0000, 2043, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1290, 1290, 0016, 'Penetration', 1, 1, 0, 0008, 0000, 0000, 30195, 0000, 0026, 0000, 0000, 0009, 0007, 0000, 20243, 0015, 2, 0490, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1291, 1290, 0016, 'Penetration', 1, 1, 0, 0008, 0001, 0000, 30200, 0000, 0027, 0000, 0000, 0009, 0007, 0000, 37056, 0030, 2, 0490, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1292, 1290, 0016, 'Penetration', 1, 1, 0, 0008, 0002, 0000, 30205, 0000, 0028, 0000, 0000, 0009, 0007, 0000, 66011, 0045, 2, 0490, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1293, 1290, 0016, 'Penetration', 1, 1, 0, 0008, 0003, 0000, 30210, 0000, 0029, 0000, 0000, 0009, 0007, 0000, 116140, 0060, 2, 0490, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1294, 1290, 0016, 'Penetration', 1, 1, 0, 0008, 0004, 0000, 30215, 0000, 0030, 0000, 0000, 0009, 0007, 0000, 192800, 0070, 2, 0490, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1295, 1290, 0016, 'Penetration', 1, 1, 0, 0008, 0005, 0000, 30220, 0000, 0031, 0000, 0000, 0009, 0007, 0000, 418030, 0080, 2, 0490, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1296, 1290, 0016, 'Penetration', 1, 1, 0, 0008, 0006, 0000, 30225, 0000, 0032, 0000, 0000, 0009, 0007, 0000, 454350, 0090, 2, 0490, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1297, 1290, 0016, 'Penetration', 1, 1, 0, 0008, 0007, 0000, 30230, 0000, 0033, 0000, 0000, 0009, 0007, 0000, 491200, 0100, 2, 0490, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1298, 1290, 0016, 'Penetration', 1, 1, 0, 0008, 0008, 0000, 30235, 0000, 0034, 0000, 0000, 0009, 0007, 0000, 520030, 0110, 2, 0490, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1299, 1290, 0016, 'Penetration', 1, 1, 0, 0008, 0009, 0000, 30240, 0000, 0035, 0000, 0000, 0009, 0007, 0000, 0, 0000, 2, 0490, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1300, 1300, 0004, 'Halt', 1, 1, 0, 0008, 0000, 0000, 30100, 0000, 0020, 0000, 0005, 0009, 0001, 0000, 20243, 0015, 2, 0540, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1301, 1300, 0004, 'Halt', 1, 1, 0, 0008, 0001, 0000, 30105, 0000, 0022, 0000, 0005, 0009, 0001, 0000, 37056, 0030, 2, 0540, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1302, 1300, 0004, 'Halt', 1, 1, 0, 0008, 0002, 0000, 30110, 0000, 0024, 0000, 0006, 0009, 0001, 0000, 66011, 0045, 2, 0540, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1303, 1300, 0004, 'Halt', 1, 1, 0, 0008, 0003, 0000, 30120, 0000, 0026, 0000, 0006, 0009, 0001, 0000, 116140, 0060, 2, 0540, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1304, 1300, 0004, 'Halt', 1, 1, 0, 0008, 0004, 0000, 30130, 0000, 0028, 0000, 0007, 0009, 0001, 0000, 192800, 0070, 2, 0540, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1305, 1300, 0004, 'Halt', 1, 1, 0, 0008, 0005, 0000, 30140, 0000, 0030, 0000, 0007, 0009, 0001, 0000, 418030, 0080, 2, 0540, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1306, 1300, 0004, 'Halt', 1, 1, 0, 0008, 0006, 0000, 30150, 0000, 0032, 0000, 0008, 0009, 0001, 0000, 454350, 0090, 2, 0540, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1307, 1300, 0004, 'Halt', 1, 1, 0, 0008, 0007, 0000, 30160, 0000, 0034, 0000, 0008, 0009, 0001, 0000, 491200, 0100, 2, 0540, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1308, 1300, 0004, 'Halt', 1, 1, 0, 0008, 0008, 0000, 30170, 0000, 0036, 0000, 0009, 0009, 0001, 0000, 520030, 0110, 2, 0540, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1309, 1300, 0004, 'Halt', 1, 1, 0, 0008, 0009, 0000, 30180, 0000, 0038, 0000, 0009, 0009, 0001, 0000, 0, 0000, 2, 0540, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1320, 1320, 0001, 'FlyingMoon', 1, 0, 0, 0000, 0000, 0000, 458, 0000, 0100, 0000, 0000, 0013, 0001, 0000, 500, 0070, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1321, 1320, 0001, 'FlyingMoon', 1, 0, 0, 0000, 0001, 0000, 1950, 0000, 0100, 0000, 0000, 0014, 0001, 0000, 1000, 0100, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1322, 1320, 0001, 'FlyingMoon', 1, 0, 0, 0000, 0002, 0000, 5890, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1350, 1350, 0019, 'DivineHare', 0, 0, 0, 0002, 0000, 0000, 2020, 0000, 0100, 0040, 0000, 0005, 0001, 0000, 1800, 0055, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1351, 1350, 0019, 'DivineHare', 0, 0, 0, 0002, 0001, 0000, 2021, 0000, 0100, 0050, 0000, 0005, 0001, 0000, 4000, 0065, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1352, 1350, 0019, 'DivineHare', 0, 0, 0, 0002, 0002, 0000, 2022, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 5500, 0075, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1353, 1350, 0019, 'DivineHare', 0, 0, 0, 0002, 0003, 0000, 2023, 0000, 0100, 0070, 0000, 0005, 0001, 0000, 9000, 0085, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1354, 1350, 0019, 'DivineHare', 0, 0, 0, 0002, 0004, 0000, 2024, 0000, 0100, 0080, 0000, 0005, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1360, 1360, 0019, 'NightDevil', 0, 0, 0, 0002, 0000, 0000, 2030, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 40, 0080, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1361, 1360, 0019, 'NightDevil', 0, 0, 0, 0002, 0001, 0000, 2031, 0000, 0100, 0080, 0000, 0005, 0001, 0000, 250, 0090, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1362, 1360, 0019, 'NightDevil', 0, 0, 0, 0002, 0002, 0000, 2032, 0000, 0100, 0090, 0000, 0005, 0001, 0000, 450, 0100, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1363, 1360, 0019, 'NightDevil', 0, 0, 0, 0002, 0003, 0000, 2033, 0000, 0100, 0100, 0000, 0005, 0001, 0000, 700, 0110, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1364, 1360, 0019, 'NightDevil', 0, 0, 0, 0002, 0004, 0000, 2034, 0000, 0100, 0110, 0000, 0005, 0001, 0000, 0, 0000, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1380, 1380, 0022, 'Dance2', 0, 0, 0, 0002, 0000, 0000, 0, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0010, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1385, 1385, 0022, 'Dance3', 0, 0, 0, 0002, 0000, 0000, 0, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0010, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1390, 1390, 0022, 'Dance4', 0, 0, 0, 0002, 0000, 0000, 0, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0010, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1395, 1395, 0022, 'Dance5', 0, 0, 0, 0002, 0000, 0000, 0, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0010, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1400, 1400, 0022, 'Dance6', 0, 0, 0, 0002, 0000, 0000, 0, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0010, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1405, 1405, 0022, 'Dance7', 0, 0, 0, 0002, 0000, 0000, 0, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0010, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1410, 1410, 0022, 'Dance8', 0, 0, 0, 0002, 0000, 0000, 0, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0010, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1415, 1415, 0019, 'BattleDance', 0, 0, 0, 0002, 0000, 0000, 0, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0010, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1416, 1416, 0019, 'Triumph', 0, 0, 0, 0002, 0000, 0000, 0, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0010, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1417, 1417, 0019, 'StepStomp', 0, 0, 0, 0002, 0000, 0000, 0, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0010, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1418, 1418, 0019, 'Moonlight', 0, 0, 0, 0002, 0000, 0000, 0, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0010, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1419, 1419, 0019, 'SnowWind', 0, 0, 0, 0002, 0000, 0000, 0, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0010, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1420, 1420, 0019, 'Phoenix', 0, 0, 0, 0002, 0000, 0000, 0, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0010, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1421, 1421, 0019, 'Melody', 0, 0, 0, 0002, 0000, 0000, 0, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0010, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1422, 1422, 0019, 'Tipsy', 0, 0, 0, 0002, 0000, 0000, 0, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0010, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (1423, 1423, 0019, 'Felicity', 0, 0, 0, 0002, 0000, 0000, 0, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0010, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3000, 3000, 0001, 'HumanMessenger', 1, 0, 0, 0000, 0000, 0000, 0, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3001, 3001, 0001, 'WingedSnakeMsgr', 1, 0, 0, 0000, 0000, 0000, 0, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3002, 3002, 0001, 'BanditMessenger', 1, 0, 0, 0000, 0000, 0000, 0, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3003, 3003, 0001, 'RatMessenger', 1, 0, 0, 0000, 0000, 0000, 0, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3004, 3004, 0001, 'ElfMessenger', 1, 0, 0, 0000, 0000, 0000, 0, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3005, 3005, 0001, 'MonkeyMessenger', 1, 0, 0, 0000, 0000, 0000, 0, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3006, 3006, 0001, 'GiantApeMsgr', 1, 0, 0, 0000, 0000, 0000, 0, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3007, 3007, 0001, 'ThunderApeMsgr', 1, 0, 0, 0000, 0000, 0000, 0, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3008, 3008, 0001, 'SnakemanMsgr', 1, 0, 0, 0000, 0000, 0000, 0, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3009, 3009, 0001, 'SandMonsterMsgr', 1, 0, 0, 0000, 0000, 0000, 0, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3010, 3010, 0001, 'HillMonsterMsgr', 1, 0, 0, 0000, 0000, 0000, 0, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3011, 3011, 0001, 'RockMonsterMsgr', 1, 0, 0, 0000, 0000, 0000, 0, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3012, 3012, 0001, 'BladeGhostMsgr', 1, 0, 0, 0000, 0000, 0000, 0, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3013, 3013, 0001, 'BirdmanMsgr', 1, 0, 0, 0000, 0000, 0000, 0, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3014, 3014, 0001, 'HawkMsgr', 1, 0, 0, 0000, 0000, 0000, 0, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3015, 3015, 0001, 'BanditMessenger', 1, 0, 0, 0000, 0000, 0000, 0, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3016, 3016, 0001, 'BatMessenger', 1, 0, 0, 0000, 0000, 0000, 0, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3017, 3017, 0001, 'BloodyBatMsgr', 1, 0, 0, 0000, 0000, 0000, 0, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3018, 3018, 0001, 'BullMessenger', 1, 0, 0, 0000, 0000, 0000, 0, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3019, 3019, 0001, 'DevilMessenger', 1, 0, 0, 0000, 0000, 0000, 0, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3050, 3050, 0026, 'CruelShade', 1, 0, 0, 0000, 0000, 0000, 30010, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 500, 0015, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3051, 3050, 0026, 'CruelShade', 1, 0, 0, 0000, 0001, 0000, 30014, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 800, 0040, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3052, 3050, 0026, 'CruelShade', 1, 0, 0, 0000, 0002, 0000, 30018, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 1000, 0070, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3053, 3050, 0026, 'CruelShade', 1, 0, 0, 0000, 0003, 0000, 30020, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3080, 3080, 0006, 'Dodge', 0, 0, 0, 0001, 0000, 0200, 30110, 0000, 0100, 0060, 0000, 0010, 0021, 0000, 500, 0070, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3081, 3080, 0006, 'Dodge', 0, 0, 0, 0001, 0001, 0300, 30120, 0000, 0100, 0060, 0000, 0010, 0021, 0000, 1000, 0100, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3082, 3080, 0006, 'Dodge', 0, 0, 0, 0001, 0002, 0400, 30130, 0000, 0100, 0060, 0000, 0010, 0021, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3090, 3090, 0005, 'Pervade', 1, 1, 0, 0004, 0002, 0000, 540, 0000, 0100, 0000, 0008, 0007, 0001, 0000, 1296000, 0065, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 3091, 0000, 0001);
INSERT INTO `spells` VALUES (3091, 3091, 0005, 'Pervade21', 1, 1, 0, 0004, 0000, 0000, 550, 0000, 0100, 0000, 0008, 0007, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 3092, 0000, 0001);
INSERT INTO `spells` VALUES (3092, 3092, 0005, 'Pervade22', 1, 1, 0, 0004, 0000, 0000, 560, 0000, 0100, 0000, 0008, 0007, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 3093, 0000, 0001);
INSERT INTO `spells` VALUES (3093, 3093, 0005, 'Pervade23', 1, 1, 0, 0004, 0000, 0000, 570, 0000, 0100, 0000, 0008, 0007, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 3106, 0000, 0001);
INSERT INTO `spells` VALUES (3094, 3090, 0005, 'Pervade', 1, 1, 0, 0004, 0003, 0000, 650, 0000, 0100, 0000, 0009, 0007, 0001, 0000, 1950000, 0075, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 3095, 0000, 0001);
INSERT INTO `spells` VALUES (3095, 3095, 0005, 'Pervade31', 1, 1, 0, 0004, 0000, 0000, 660, 0000, 0100, 0000, 0009, 0007, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 3096, 0000, 0001);
INSERT INTO `spells` VALUES (3096, 3096, 0005, 'Pervade32', 1, 1, 0, 0004, 0000, 0000, 670, 0000, 0100, 0000, 0009, 0007, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 3097, 0000, 0001);
INSERT INTO `spells` VALUES (3097, 3097, 0005, 'Pervade33', 1, 1, 0, 0004, 0000, 0000, 680, 0000, 0100, 0000, 0009, 0007, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 3107, 0000, 0001);
INSERT INTO `spells` VALUES (3098, 3090, 0005, 'Pervade', 1, 1, 0, 0004, 0004, 0000, 720, 0000, 0100, 0000, 0010, 0007, 0001, 0000, 2376000, 0085, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 3099, 0000, 0001);
INSERT INTO `spells` VALUES (3099, 3099, 0005, 'Pervade41', 1, 1, 0, 0004, 0000, 0000, 730, 0000, 0100, 0000, 0010, 0007, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 3100, 0000, 0001);
INSERT INTO `spells` VALUES (3100, 3100, 0005, 'Pervade42', 1, 1, 0, 0004, 0000, 0000, 740, 0000, 0100, 0000, 0010, 0007, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 3101, 0000, 0001);
INSERT INTO `spells` VALUES (3101, 3101, 0005, 'Pervade43', 1, 1, 0, 0004, 0000, 0000, 750, 0000, 0100, 0000, 0010, 0007, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 3108, 0000, 0001);
INSERT INTO `spells` VALUES (3102, 3090, 0005, 'Pervade', 1, 1, 0, 0004, 0005, 0000, 770, 0000, 0100, 0000, 0012, 0007, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 3103, 0000, 0001);
INSERT INTO `spells` VALUES (3103, 3103, 0005, 'Pervade51', 1, 1, 0, 0004, 0000, 0000, 780, 0000, 0100, 0000, 0012, 0007, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 3104, 0000, 0001);
INSERT INTO `spells` VALUES (3104, 3104, 0005, 'Pervade52', 1, 1, 0, 0004, 0000, 0000, 790, 0000, 0100, 0000, 0012, 0007, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 3105, 0000, 0001);
INSERT INTO `spells` VALUES (3105, 3105, 0005, 'Pervade53', 1, 1, 0, 0004, 0000, 0000, 800, 0000, 0100, 0000, 0012, 0007, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 3109, 0000, 0001);
INSERT INTO `spells` VALUES (3106, 3106, 0005, 'Pervade24', 1, 1, 0, 0004, 0000, 0000, 580, 0000, 0100, 0000, 0008, 0007, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3107, 3107, 0005, 'Pervade34', 1, 1, 0, 0004, 0000, 0000, 690, 0000, 0100, 0000, 0009, 0007, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3108, 3108, 0005, 'Pervade44', 1, 1, 0, 0004, 0000, 0000, 760, 0000, 0100, 0000, 0010, 0007, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3109, 3109, 0005, 'Pervade54', 1, 1, 0, 0004, 0000, 0000, 810, 0000, 0100, 0000, 0012, 0007, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3190, 3090, 0005, 'Pervade', 1, 1, 0, 0004, 0000, 0000, 130, 0000, 0100, 0000, 0007, 0007, 0001, 0000, 195000, 0045, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 3191, 0000, 0001);
INSERT INTO `spells` VALUES (3191, 3191, 0005, 'Pervade1', 1, 1, 0, 0004, 0000, 0000, 140, 0000, 0100, 0000, 0007, 0007, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 3192, 0000, 0001);
INSERT INTO `spells` VALUES (3192, 3192, 0005, 'Pervade2', 1, 1, 0, 0004, 0000, 0000, 150, 0000, 0100, 0000, 0007, 0007, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 3193, 0000, 0001);
INSERT INTO `spells` VALUES (3193, 3193, 0005, 'Pervade3', 1, 1, 0, 0004, 0000, 0000, 160, 0000, 0100, 0000, 0007, 0007, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 3206, 0000, 0001);
INSERT INTO `spells` VALUES (3206, 3206, 0005, 'Pervade4', 1, 1, 0, 0004, 0000, 0000, 170, 0000, 0100, 0000, 0007, 0007, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3290, 3090, 0005, 'Pervade', 1, 1, 0, 0004, 0001, 0000, 200, 0000, 0100, 0000, 0007, 0007, 0001, 0000, 300000, 0055, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 3291, 0000, 0001);
INSERT INTO `spells` VALUES (3291, 3291, 0005, 'Pervade11', 1, 1, 0, 0004, 0000, 0000, 210, 0000, 0100, 0000, 0007, 0007, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 3292, 0000, 0001);
INSERT INTO `spells` VALUES (3292, 3292, 0005, 'Pervade12', 1, 1, 0, 0004, 0000, 0000, 220, 0000, 0100, 0000, 0007, 0007, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 3293, 0000, 0001);
INSERT INTO `spells` VALUES (3293, 3293, 0005, 'Pervade13', 1, 1, 0, 0004, 0000, 0000, 230, 0000, 0100, 0000, 0008, 0007, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 3306, 0000, 0001);
INSERT INTO `spells` VALUES (3306, 3306, 0005, 'Pervade14', 1, 1, 0, 0004, 0000, 0000, 240, 0000, 0100, 0000, 0008, 0007, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3310, 3310, 0019, 'CuteBunny', 0, 0, 0, 0002, 0000, 0000, 3310, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0055, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3311, 3311, 0019, 'PlayfulBunny', 0, 0, 0, 0002, 0000, 0000, 3311, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0055, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3312, 3312, 0019, 'CuteBunny', 0, 0, 0, 0002, 0000, 0000, 3312, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0055, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3313, 3313, 0019, 'FowlEmpress', 0, 0, 0, 0002, 0000, 0000, 3037, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3320, 3320, 0019, 'tuzi', 0, 0, 0, 0002, 0000, 0000, 3320, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3321, 3321, 0019, 'Piglet', 0, 0, 0, 0002, 0000, 0000, 3321, 0000, 0100, 65535, 0000, 0005, 0001, 0000, 0, 0055, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (3322, 3322, 0019, 'EmceeRabbit', 0, 0, 0, 0002, 0000, 0000, 3322, 0000, 0100, 1200, 0000, 0005, 0001, 0000, 0, 0055, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (4000, 4000, 0023, 'SummonGuard', 0, 0, 0, 0002, 0000, 0000, 9000, 0000, 0100, 0000, 0005, 0010, 0001, 0000, 100, 0040, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (4001, 4000, 0023, 'SummonGuard', 0, 0, 0, 0002, 0001, 0000, 9001, 0000, 0100, 0000, 0000, 0010, 0001, 0000, 300, 0080, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (4002, 4000, 0023, 'SummonGuard', 0, 0, 0, 0002, 0002, 0000, 9002, 0000, 0100, 0000, 0000, 0010, 0001, 0000, 500, 0105, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (4003, 4000, 0023, 'SummonGuard', 0, 0, 0, 0002, 0003, 0000, 9003, 0000, 0100, 0000, 0000, 0010, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (4010, 4010, 0023, 'SummonBat', 0, 0, 0, 0002, 0000, 0000, 9005, 0000, 0100, 0060, 0000, 0010, 0001, 0000, 30, 0040, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (4011, 4010, 0023, 'SummonBat', 0, 0, 0, 0002, 0001, 0000, 9006, 0000, 0100, 0080, 0000, 0010, 0001, 0000, 50, 0070, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (4012, 4010, 0023, 'SummonBat', 0, 0, 0, 0002, 0002, 0000, 9007, 0000, 0100, 0100, 0000, 0010, 0001, 0000, 100, 0100, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (4013, 4010, 0023, 'SummonBat', 0, 0, 0, 0002, 0003, 0000, 9008, 0000, 0100, 0120, 0000, 0010, 0001, 0000, 0, 0000, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (4020, 4020, 0023, 'SummonBatBoss', 0, 0, 0, 0002, 0000, 0000, 9010, 0000, 0100, 0060, 0000, 0010, 0001, 0000, 30, 0040, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (4021, 4020, 0023, 'SummonBatBoss', 0, 0, 0, 0002, 0001, 0000, 9011, 0000, 0100, 0080, 0000, 0010, 0001, 0000, 50, 0070, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (4022, 4020, 0023, 'SummonBatBoss', 0, 0, 0, 0002, 0002, 0000, 9012, 0000, 0100, 0100, 0000, 0010, 0001, 0000, 100, 0100, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (4023, 4020, 0023, 'SummonBatBoss', 0, 0, 0, 0002, 0003, 0000, 9013, 0000, 0100, 0120, 0000, 0010, 0001, 0000, 0, 0000, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (4030, 4030, 0025, 'StandinBat', 1, 0, 0, 0000, 0000, 0000, 30020, 0000, 0100, 0000, 0000, 0010, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (4040, 4040, 0024, 'Vampire', 1, 0, 0, 0000, 0000, 0000, 30200, 0000, 0100, 0000, 0000, 0019, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (4050, 4050, 0023, 'BloodyBat', 0, 0, 0, 0002, 0000, 0000, 9015, 0000, 0100, 0060, 0000, 0010, 0001, 0000, 200, 0040, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (4051, 4050, 0023, 'BloodyBat', 0, 0, 0, 0002, 0001, 0000, 9016, 0000, 0100, 0080, 0000, 0010, 0001, 0000, 500, 0070, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (4052, 4050, 0023, 'BloodyBat', 0, 0, 0, 0002, 0002, 0000, 9017, 0000, 0100, 0100, 0000, 0010, 0001, 0000, 1000, 0100, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (4053, 4050, 0023, 'BloodyBat', 0, 0, 0, 0002, 0003, 0000, 9018, 0000, 0100, 0120, 0000, 0010, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (4060, 4060, 0023, 'FireEvil', 0, 0, 0, 0002, 0000, 0000, 9020, 0000, 0100, 0060, 0000, 0010, 0001, 0000, 200, 0040, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (4061, 4060, 0023, 'FireEvil', 0, 0, 0, 0002, 0001, 0000, 9021, 0000, 0100, 0080, 0000, 0010, 0001, 0000, 500, 0070, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (4062, 4060, 0023, 'FireEvil', 0, 0, 0, 0002, 0002, 0000, 9022, 0000, 0100, 0100, 0000, 0010, 0001, 0000, 1000, 0100, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (4063, 4060, 0023, 'FireEvil', 0, 0, 0, 0002, 0003, 0000, 9023, 0000, 0100, 0120, 0000, 0010, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (4070, 4070, 0023, 'Skeleton', 0, 0, 0, 0002, 0000, 0000, 9025, 0000, 0100, 0060, 0000, 0010, 0001, 0000, 200, 0040, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (4071, 4070, 0023, 'Skeleton', 0, 0, 0, 0002, 0001, 0000, 9026, 0000, 0100, 0080, 0000, 0010, 0001, 0000, 500, 0070, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (4072, 4070, 0023, 'Skeleton', 0, 0, 0, 0002, 0002, 0000, 9027, 0000, 0100, 0100, 0000, 0010, 0001, 0000, 1000, 0100, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (4073, 4070, 0023, 'Skeleton', 0, 0, 0, 0002, 0003, 0000, 9028, 0000, 0100, 0120, 0000, 0010, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (4075, 4075, 0001, 'Wraith', 1, 0, 0, 0000, 0000, 0000, 0, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (4076, 4076, 0001, 'Syren', 1, 0, 0, 0000, 0000, 0000, 0, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5000, 5000, 0016, 'FreezingArrow', 1, 1, 0, 0008, 0000, 0000, 8000, 0000, 0005, 0000, 0000, 0009, 0016, 0000, 0, 0000, 2, 0500, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (5001, 5001, 0005, 'SpeedLightning', 1, 0, 1, 0000, 0000, 0000, 450, 0000, 0100, 0000, 0012, 0012, 0001, 0000, 0, 0080, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5002, 5002, 0016, 'PoisonousArrows', 1, 0, 0, 0008, 0000, 0000, 30002, 0000, 0010, 0002, 0000, 0015, 0002, 0000, 0, 0000, 2, 0500, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (5010, 5010, 0005, 'Snow', 1, 1, 0, 0008, 0000, 0000, 30110, 0000, 0020, 0000, 0002, 0009, 0001, 0000, 20243, 0015, 2, 0561, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5011, 5010, 0005, 'Snow', 1, 1, 0, 0008, 0001, 0000, 30110, 0000, 0023, 0000, 0002, 0009, 0001, 0000, 37056, 0030, 2, 0561, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5012, 5010, 0005, 'Snow', 1, 1, 0, 0008, 0002, 0000, 30110, 0000, 0026, 0000, 0002, 0009, 0001, 0000, 66011, 0045, 2, 0561, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5013, 5010, 0005, 'Snow', 1, 1, 0, 0008, 0003, 0000, 30110, 0000, 0029, 0000, 0002, 0009, 0001, 0000, 116140, 0060, 2, 0561, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5014, 5010, 0005, 'Snow', 1, 1, 0, 0008, 0004, 0000, 30140, 0000, 0031, 0000, 0003, 0009, 0001, 0000, 192800, 0070, 2, 0561, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5015, 5010, 0005, 'Snow', 1, 1, 0, 0008, 0005, 0000, 30140, 0000, 0034, 0000, 0003, 0009, 0001, 0000, 418030, 0080, 2, 0561, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5016, 5010, 0005, 'Snow', 1, 1, 0, 0008, 0006, 0000, 30140, 0000, 0037, 0000, 0003, 0009, 0001, 0000, 454350, 0090, 2, 0561, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5017, 5010, 0005, 'Snow', 1, 1, 0, 0008, 0007, 0000, 30140, 0000, 0040, 0000, 0003, 0009, 0001, 0000, 491200, 0100, 2, 0561, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5018, 5010, 0005, 'Snow', 1, 1, 0, 0008, 0008, 0000, 30140, 0000, 0043, 0000, 0003, 0009, 0001, 0000, 520030, 0110, 2, 0561, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5019, 5010, 0005, 'Snow', 1, 1, 0, 0008, 0009, 0000, 30145, 0000, 0045, 0000, 0003, 0009, 0001, 0000, 0, 0000, 2, 0561, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5020, 5020, 0004, 'StrandedMonster', 1, 1, 0, 0008, 0000, 0000, 30130, 0000, 0020, 0000, 0003, 0009, 0001, 0000, 20243, 0015, 2, 0580, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5021, 5020, 0004, 'StrandedMonster', 1, 1, 0, 0008, 0001, 0000, 30130, 0000, 0023, 0000, 0003, 0009, 0001, 0000, 37056, 0030, 2, 0580, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5022, 5020, 0004, 'StrandedMonster', 1, 1, 0, 0008, 0002, 0000, 30130, 0000, 0026, 0000, 0003, 0009, 0001, 0000, 66011, 0045, 2, 0580, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5023, 5020, 0004, 'StrandedMonster', 1, 1, 0, 0008, 0003, 0000, 30130, 0000, 0029, 0000, 0004, 0009, 0001, 0000, 116140, 0060, 2, 0580, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5024, 5020, 0004, 'StrandedMonster', 1, 1, 0, 0008, 0004, 0000, 30130, 0000, 0031, 0000, 0004, 0009, 0001, 0000, 192800, 0070, 2, 0580, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5025, 5020, 0004, 'StrandedMonster', 1, 1, 0, 0008, 0005, 0000, 30130, 0000, 0034, 0000, 0004, 0009, 0001, 0000, 418030, 0080, 2, 0580, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5026, 5020, 0004, 'StrandedMonster', 1, 1, 0, 0008, 0006, 0000, 30130, 0000, 0037, 0000, 0005, 0009, 0001, 0000, 454350, 0090, 2, 0580, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5027, 5020, 0004, 'StrandedMonster', 1, 1, 0, 0008, 0007, 0000, 30130, 0000, 0040, 0000, 0005, 0009, 0001, 0000, 491200, 0100, 2, 0580, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5028, 5020, 0004, 'StrandedMonster', 1, 1, 0, 0008, 0008, 0000, 30130, 0000, 0043, 0000, 0006, 0009, 0001, 0000, 520030, 0110, 2, 0580, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5029, 5020, 0004, 'StrandedMonster', 1, 1, 0, 0008, 0009, 0000, 30135, 0000, 0045, 0000, 0006, 0009, 0001, 0000, 0, 0000, 2, 0580, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5030, 5030, 0016, 'Phoenix', 1, 1, 0, 0008, 0000, 0000, 30125, 0000, 0040, 0000, 0000, 0009, 0007, 0000, 20243, 0015, 2, 420620, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5031, 5030, 0016, 'Phoenix', 1, 1, 0, 0008, 0001, 0000, 30128, 0000, 0045, 0000, 0000, 0009, 0007, 0000, 37056, 0030, 2, 420620, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5032, 5030, 0016, 'Phoenix', 1, 1, 0, 0008, 0002, 0000, 30130, 0000, 0050, 0000, 0000, 0009, 0007, 0000, 66011, 0045, 2, 420620, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5033, 5030, 0016, 'Phoenix', 1, 1, 0, 0008, 0003, 0000, 30133, 0000, 0055, 0000, 0000, 0009, 0007, 0000, 116140, 0060, 2, 420620, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5034, 5030, 0016, 'Phoenix', 1, 1, 0, 0008, 0004, 0000, 30136, 0000, 0060, 0000, 0000, 0009, 0007, 0000, 192800, 0070, 2, 420620, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5035, 5030, 0016, 'Phoenix', 1, 1, 0, 0008, 0005, 0000, 30139, 0000, 0065, 0000, 0000, 0009, 0007, 0000, 418030, 0080, 2, 420620, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5036, 5030, 0016, 'Phoenix', 1, 1, 0, 0008, 0006, 0000, 30141, 0000, 0070, 0000, 0000, 0009, 0007, 0000, 454350, 0090, 2, 420620, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5037, 5030, 0016, 'Phoenix', 1, 1, 0, 0008, 0007, 0000, 30144, 0000, 0075, 0000, 0000, 0009, 0007, 0000, 491200, 0100, 2, 420620, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5038, 5030, 0016, 'Phoenix', 1, 1, 0, 0008, 0008, 0000, 30147, 0000, 0080, 0000, 0000, 0009, 0007, 0000, 520030, 0110, 2, 420620, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5039, 5030, 0016, 'Phoenix', 1, 1, 0, 0008, 0009, 0000, 30150, 0000, 0085, 0000, 0000, 0009, 0007, 0000, 0, 0000, 2, 420620, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5040, 5040, 0016, 'Boom', 1, 1, 0, 0008, 0000, 0000, 6000, 0000, 0010, 0000, 0000, 0009, 0016, 0000, 200, 0015, 2, 0460, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5041, 5040, 0016, 'Boom', 1, 1, 0, 0008, 0001, 0000, 6000, 0000, 0012, 0000, 0000, 0009, 0016, 0000, 300, 0030, 2, 0460, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5042, 5040, 0016, 'Boom', 1, 1, 0, 0008, 0002, 0000, 7000, 0000, 0014, 0000, 0000, 0009, 0016, 0000, 400, 0045, 2, 0460, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5043, 5040, 0016, 'Boom', 1, 1, 0, 0008, 0003, 0000, 7000, 0000, 0016, 0000, 0000, 0009, 0016, 0000, 500, 0060, 2, 0460, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5044, 5040, 0016, 'Boom', 1, 1, 0, 0008, 0004, 0000, 8000, 0000, 0018, 0000, 0000, 0009, 0016, 0000, 600, 0070, 2, 0460, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5045, 5040, 0016, 'Boom', 1, 1, 0, 0008, 0005, 0000, 8000, 0000, 0020, 0000, 0000, 0009, 0016, 0000, 700, 0080, 2, 0460, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5046, 5040, 0016, 'Boom', 1, 1, 0, 0008, 0006, 0000, 9000, 0000, 0022, 0000, 0000, 0009, 0016, 0000, 800, 0090, 2, 0460, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5047, 5040, 0016, 'Boom', 1, 1, 0, 0008, 0007, 0000, 9000, 0000, 0024, 0000, 0000, 0009, 0016, 0000, 900, 0100, 2, 0460, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5048, 5040, 0016, 'Boom', 1, 1, 0, 0008, 0008, 0000, 10000, 0000, 0026, 0000, 0000, 0009, 0016, 0000, 1000, 0110, 2, 0460, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5049, 5040, 0016, 'Boom', 1, 1, 0, 0008, 0009, 0000, 10000, 0000, 0028, 0000, 0000, 0009, 0016, 0000, 0, 0000, 2, 0460, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5050, 5050, 0004, 'Boreas', 1, 1, 0, 0008, 0000, 0000, 30100, 0000, 0020, 0000, 0003, 0009, 0001, 0000, 20243, 0015, 2, 0530, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5051, 5050, 0004, 'Boreas', 1, 1, 0, 0008, 0001, 0000, 30105, 0000, 0022, 0000, 0003, 0009, 0001, 0000, 37056, 0030, 2, 0530, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5052, 5050, 0004, 'Boreas', 1, 1, 0, 0008, 0002, 0000, 30110, 0000, 0024, 0000, 0004, 0009, 0001, 0000, 66011, 0045, 2, 0530, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5053, 5050, 0004, 'Boreas', 1, 1, 0, 0008, 0003, 0000, 30120, 0000, 0026, 0000, 0004, 0009, 0001, 0000, 116140, 0060, 2, 0530, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5054, 5050, 0004, 'Boreas', 1, 1, 0, 0008, 0004, 0000, 30130, 0000, 0028, 0000, 0005, 0009, 0001, 0000, 192800, 0070, 2, 0530, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5055, 5050, 0004, 'Boreas', 1, 1, 0, 0008, 0005, 0000, 30140, 0000, 0030, 0000, 0005, 0009, 0001, 0000, 418030, 0080, 2, 0530, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5056, 5050, 0004, 'Boreas', 1, 1, 0, 0008, 0006, 0000, 30150, 0000, 0032, 0000, 0006, 0009, 0001, 0000, 454350, 0090, 2, 0530, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5057, 5050, 0004, 'Boreas', 1, 1, 0, 0008, 0007, 0000, 30160, 0000, 0034, 0000, 0006, 0009, 0001, 0000, 491200, 0100, 2, 0530, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5058, 5050, 0004, 'Boreas', 1, 1, 0, 0008, 0008, 0000, 30170, 0000, 0036, 0000, 0006, 0009, 0001, 0000, 520030, 0110, 2, 0530, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5059, 5050, 0004, 'Boreas', 1, 1, 0, 0008, 0009, 0000, 30180, 0000, 0038, 0000, 0006, 0009, 0001, 0000, 0, 0000, 2, 0530, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (5100, 5100, 0006, 'IronShirt', 0, 0, 0, 0002, 0000, 0000, 30400, 0000, 0100, 0020, 0000, 0000, 0116, 0000, 0, 0015, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 5101, 0000, 0001);
INSERT INTO `spells` VALUES (5101, 5101, 0006, 'IronShirt1', 0, 0, 0, 0000, 0000, 0000, 30400, 0000, 0100, 0010, 0000, 0000, 0116, 0000, 0, 0015, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 5102, 0000, 0001);
INSERT INTO `spells` VALUES (5102, 5102, 0006, 'IronShirt2', 0, 0, 0, 0000, 0000, 0000, 30400, 0000, 0100, 0010, 0000, 0000, 0006, 0000, 0, 0015, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 5103, 0000, 0001);
INSERT INTO `spells` VALUES (5103, 5103, 0006, 'IronShirt3', 0, 0, 0, 0000, 0000, 0000, 30400, 0000, 0100, 0010, 0000, 0000, 0006, 0000, 0, 0015, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 5104, 0000, 0001);
INSERT INTO `spells` VALUES (5104, 5104, 0006, 'IronShirt4', 0, 0, 0, 0000, 0000, 0000, 30400, 0000, 0100, 0010, 0000, 0000, 0006, 0000, 0, 0015, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 5105, 0000, 0001);
INSERT INTO `spells` VALUES (5105, 5105, 0006, 'IronShirt5', 0, 0, 0, 0000, 0000, 0000, 30400, 0000, 0100, 0010, 0000, 0000, 0006, 0000, 0, 0015, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (6000, 6000, 0001, 'TwofoldBlades', 1, 0, 0, 0000, 0000, 0000, 30010, 0000, 0100, 0000, 0003, 0003, 0001, 0000, 300000, 0040, 2, 601616, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (6001, 6000, 0001, 'TwofoldBlades', 1, 0, 0, 0000, 0001, 0000, 30015, 0000, 0100, 0000, 0003, 0003, 0001, 0000, 900000, 0070, 2, 601616, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (6002, 6000, 0001, 'TwofoldBlades', 1, 0, 0, 0000, 0002, 0000, 30020, 0000, 0100, 0000, 0003, 0003, 0001, 0000, 2223000, 0100, 2, 601616, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (6003, 6000, 0001, 'TwofoldBlades', 1, 0, 0, 0000, 0003, 0000, 30025, 0000, 0100, 0000, 0003, 0003, 0001, 0000, 4320000, 0120, 2, 601616, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (6004, 6000, 0001, 'TwofoldBlades', 1, 0, 0, 0000, 0004, 0000, 30030, 0000, 0100, 0000, 0003, 0003, 0001, 0000, 0, 0000, 2, 601616, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (6005, 6001, 0027, 'ToxicFog', 1, 0, 1, 0004, 0000, 0000, 30010, 0000, 0080, 0003, 0004, 0010, 0043, 0000, 5400, 0070, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0035, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (6006, 6001, 0027, 'ToxicFog', 1, 0, 1, 0004, 0001, 0000, 30020, 0000, 0080, 0003, 0004, 0010, 0043, 0000, 12000, 0080, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0035, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (6007, 6001, 0027, 'ToxicFog', 1, 0, 1, 0004, 0002, 0000, 30030, 0000, 0080, 0003, 0004, 0010, 0043, 0000, 16500, 0090, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0035, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (6008, 6001, 0027, 'ToxicFog', 1, 0, 1, 0004, 0003, 0000, 30040, 0000, 0080, 0003, 0004, 0010, 0043, 0000, 27000, 0100, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0035, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (6009, 6001, 0027, 'ToxicFog', 1, 0, 1, 0004, 0004, 0000, 30050, 0000, 0080, 0003, 0004, 0010, 0043, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0035, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (6010, 6010, 0028, 'ShurikenVortex', 1, 0, 0, 0002, 0000, 0000, 30030, 0000, 0100, 0002, 0012, 0015, 0039, 0000, 0, 0000, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (6011, 6011, 0006, 'FatalStrike', 0, 0, 0, 0002, 0000, 0000, 30500, 0000, 0100, 0060, 0000, 0018, 0040, 0000, 0, 0003, 1, 601511616, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (6012, 6003, 0029, 'CounterKill', 0, 0, 0, 0016, 0000, 0000, 30020, 0000, 0030, 0000, 0000, 0020, 0001, 0000, 5400, 0070, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (6013, 6003, 0029, 'CounterKill', 0, 0, 0, 0016, 0001, 0000, 30028, 0000, 0030, 0000, 0000, 0020, 0001, 0000, 12000, 0090, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (6014, 6003, 0029, 'CounterKill', 0, 0, 0, 0016, 0002, 0000, 30036, 0000, 0030, 0000, 0000, 0020, 0001, 0000, 16500, 0110, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (6015, 6003, 0029, 'CounterKill', 0, 0, 0, 0016, 0003, 0000, 30042, 0000, 0030, 0000, 0000, 0020, 0001, 0000, 27000, 0120, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (6016, 6003, 0029, 'CounterKill', 0, 0, 0, 0016, 0004, 0000, 30050, 0000, 0030, 0000, 0000, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (6017, 6002, 0006, 'PoisonStar', 1, 0, 0, 0000, 0000, 0000, 1, 0000, 0080, 0005, 0005, 0010, 0042, 0000, 5400, 0070, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0035, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (6018, 6002, 0006, 'PoisonStar', 1, 0, 0, 0000, 0001, 0000, 1, 0000, 0080, 0010, 0005, 0010, 0042, 0000, 12000, 0090, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0035, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (6019, 6002, 0006, 'PoisonStar', 1, 0, 0, 0000, 0002, 0000, 1, 0000, 0080, 0015, 0005, 0010, 0042, 0000, 16500, 0100, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0035, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (6020, 6002, 0006, 'PoisonStar', 1, 0, 0, 0000, 0003, 0000, 1, 0000, 0080, 0020, 0005, 0010, 0042, 0000, 27000, 0120, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0035, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (6021, 6002, 0006, 'PoisonStar', 1, 0, 0, 0000, 0004, 0000, 1, 0000, 0080, 0025, 0005, 0010, 0042, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0035, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (6022, 6004, 0007, 'ArcherBane', 1, 0, 0, 0032, 0000, 0000, 30010, 0000, 0080, 0000, 0000, 0015, 0022, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0035, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (7000, 7000, 0016, 'Seizer', 1, 1, 0, 0008, 0000, 0000, 30130, 0000, 0045, 0000, 0000, 0009, 0007, 0000, 5000, 0015, 2, 0430, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 7100, 0000, 0001);
INSERT INTO `spells` VALUES (7001, 7000, 0016, 'Seizer', 1, 1, 0, 0008, 0001, 0000, 30135, 0000, 0045, 0000, 0000, 0009, 0007, 0000, 9000, 0030, 2, 0430, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 7101, 0000, 0001);
INSERT INTO `spells` VALUES (7002, 7000, 0016, 'Seizer', 1, 1, 0, 0008, 0002, 0000, 30140, 0000, 0045, 0000, 0000, 0009, 0007, 0000, 16000, 0045, 2, 0430, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 7102, 0000, 0001);
INSERT INTO `spells` VALUES (7003, 7000, 0016, 'Seizer', 1, 1, 0, 0008, 0003, 0000, 30145, 0000, 0045, 0000, 0000, 0009, 0007, 0000, 29000, 0060, 2, 0430, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 7103, 0000, 0001);
INSERT INTO `spells` VALUES (7004, 7000, 0016, 'Seizer', 1, 1, 0, 0008, 0004, 0000, 30150, 0000, 0045, 0000, 0000, 0009, 0007, 0000, 48000, 0070, 2, 0430, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 7104, 0000, 0001);
INSERT INTO `spells` VALUES (7005, 7000, 0016, 'Seizer', 1, 1, 0, 0008, 0005, 0000, 30155, 0000, 0045, 0000, 0000, 0009, 0007, 0000, 104000, 0080, 2, 0430, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 7105, 0000, 0001);
INSERT INTO `spells` VALUES (7006, 7000, 0016, 'Seizer', 1, 1, 0, 0008, 0006, 0000, 30160, 0000, 0045, 0000, 0000, 0009, 0007, 0000, 113000, 0090, 2, 0430, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 7106, 0000, 0001);
INSERT INTO `spells` VALUES (7007, 7000, 0016, 'Seizer', 1, 1, 0, 0008, 0007, 0000, 30165, 0000, 0045, 0000, 0000, 0009, 0007, 0000, 122000, 0100, 2, 0430, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 7107, 0000, 0001);
INSERT INTO `spells` VALUES (7008, 7000, 0016, 'Seizer', 1, 1, 0, 0008, 0008, 0000, 30170, 0000, 0045, 0000, 0000, 0009, 0007, 0000, 130000, 0110, 2, 0430, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 7108, 0000, 0001);
INSERT INTO `spells` VALUES (7009, 7000, 0016, 'Seizer', 1, 1, 0, 0008, 0009, 0000, 30175, 0000, 0045, 0000, 0000, 0009, 0007, 0000, 0, 0000, 2, 0430, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 7109, 0000, 0001);
INSERT INTO `spells` VALUES (7010, 7010, 0016, 'Earthquake', 1, 1, 0, 0008, 0000, 0000, 6000, 0000, 0026, 0000, 0000, 0009, 0016, 0000, 200, 0015, 2, 0450, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7011, 7010, 0016, 'Earthquake', 1, 1, 0, 0008, 0001, 0000, 6000, 0000, 0027, 0000, 0000, 0009, 0016, 0000, 300, 0030, 2, 0450, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7012, 7010, 0016, 'Earthquake', 1, 1, 0, 0008, 0002, 0000, 7000, 0000, 0028, 0000, 0000, 0009, 0016, 0000, 400, 0045, 2, 0450, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7013, 7010, 0016, 'Earthquake', 1, 1, 0, 0008, 0003, 0000, 7000, 0000, 0029, 0000, 0000, 0009, 0016, 0000, 500, 0060, 2, 0450, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7014, 7010, 0016, 'Earthquake', 1, 1, 0, 0008, 0004, 0000, 8000, 0000, 0030, 0000, 0000, 0009, 0016, 0000, 600, 0070, 2, 0450, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7015, 7010, 0016, 'Earthquake', 1, 1, 0, 0008, 0005, 0000, 8000, 0000, 0031, 0000, 0000, 0009, 0016, 0000, 700, 0080, 2, 0450, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7016, 7010, 0016, 'Earthquake', 1, 1, 0, 0008, 0006, 0000, 9000, 0000, 0032, 0000, 0000, 0009, 0016, 0000, 800, 0090, 2, 0450, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7017, 7010, 0016, 'Earthquake', 1, 1, 0, 0008, 0007, 0000, 9000, 0000, 0033, 0000, 0000, 0009, 0016, 0000, 900, 0100, 2, 0450, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7018, 7010, 0016, 'Earthquake', 1, 1, 0, 0008, 0008, 0000, 10000, 0000, 0034, 0000, 0000, 0009, 0016, 0000, 1000, 0110, 2, 0450, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7019, 7010, 0016, 'Earthquake', 1, 1, 0, 0008, 0009, 0000, 10000, 0000, 0035, 0000, 0000, 0009, 0016, 0000, 0, 0000, 2, 0450, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7020, 7020, 0005, 'Rage', 1, 1, 0, 0008, 0000, 0000, 30130, 0000, 0045, 0000, 0002, 0009, 0001, 0000, 20243, 0015, 2, 0480, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7021, 7020, 0005, 'Rage', 1, 1, 0, 0008, 0001, 0000, 30135, 0000, 0045, 0000, 0002, 0009, 0001, 0000, 37056, 0030, 2, 0480, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7022, 7020, 0005, 'Rage', 1, 1, 0, 0008, 0002, 0000, 30140, 0000, 0045, 0000, 0002, 0009, 0001, 0000, 66011, 0045, 2, 0480, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7023, 7020, 0005, 'Rage', 1, 1, 0, 0008, 0003, 0000, 30145, 0000, 0045, 0000, 0002, 0009, 0001, 0000, 116140, 0060, 2, 0480, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7024, 7020, 0005, 'Rage', 1, 1, 0, 0008, 0004, 0000, 30150, 0000, 0045, 0000, 0002, 0009, 0001, 0000, 192800, 0070, 2, 0480, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7025, 7020, 0005, 'Rage', 1, 1, 0, 0008, 0005, 0000, 30155, 0000, 0045, 0000, 0002, 0009, 0001, 0000, 418030, 0080, 2, 0480, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7026, 7020, 0005, 'Rage', 1, 1, 0, 0008, 0006, 0000, 30160, 0000, 0045, 0000, 0002, 0009, 0001, 0000, 454350, 0090, 2, 0480, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7027, 7020, 0005, 'Rage', 1, 1, 0, 0008, 0007, 0000, 30165, 0000, 0045, 0000, 0002, 0009, 0001, 0000, 491200, 0100, 2, 0480, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7028, 7020, 0005, 'Rage', 1, 1, 0, 0008, 0008, 0000, 30170, 0000, 0045, 0000, 0002, 0009, 0001, 0000, 520030, 0110, 2, 0480, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7029, 7020, 0005, 'Rage', 1, 1, 0, 0008, 0009, 0000, 30175, 0000, 0045, 0000, 0002, 0009, 0001, 0000, 0, 0000, 2, 0480, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7030, 7030, 0016, 'Celestial', 1, 1, 0, 0008, 0000, 0000, 30125, 0000, 0045, 0000, 0000, 0009, 0007, 0000, 5000, 0015, 2, 0481, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 7130, 0000, 0001);
INSERT INTO `spells` VALUES (7031, 7030, 0016, 'Celestial', 1, 1, 0, 0008, 0001, 0000, 30128, 0000, 0045, 0000, 0000, 0009, 0007, 0000, 9000, 0030, 2, 0481, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 7131, 0000, 0001);
INSERT INTO `spells` VALUES (7032, 7030, 0016, 'Celestial', 1, 1, 0, 0008, 0002, 0000, 30130, 0000, 0045, 0000, 0000, 0009, 0007, 0000, 16000, 0045, 2, 0481, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 7132, 0000, 0001);
INSERT INTO `spells` VALUES (7033, 7030, 0016, 'Celestial', 1, 1, 0, 0008, 0003, 0000, 30133, 0000, 0045, 0000, 0000, 0009, 0007, 0000, 29000, 0060, 2, 0481, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 7133, 0000, 0001);
INSERT INTO `spells` VALUES (7034, 7030, 0016, 'Celestial', 1, 1, 0, 0008, 0004, 0000, 30136, 0000, 0045, 0000, 0000, 0009, 0007, 0000, 48000, 0070, 2, 0481, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 7134, 0000, 0001);
INSERT INTO `spells` VALUES (7035, 7030, 0016, 'Celestial', 1, 1, 0, 0008, 0005, 0000, 30139, 0000, 0045, 0000, 0000, 0009, 0007, 0000, 104000, 0080, 2, 0481, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 7135, 0000, 0001);
INSERT INTO `spells` VALUES (7036, 7030, 0016, 'Celestial', 1, 1, 0, 0008, 0006, 0000, 30141, 0000, 0045, 0000, 0000, 0009, 0007, 0000, 113000, 0090, 2, 0481, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 7136, 0000, 0001);
INSERT INTO `spells` VALUES (7037, 7030, 0016, 'Celestial', 1, 1, 0, 0008, 0007, 0000, 30144, 0000, 0045, 0000, 0000, 0009, 0007, 0000, 122000, 0100, 2, 0481, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 7137, 0000, 0001);
INSERT INTO `spells` VALUES (7038, 7030, 0016, 'Celestial', 1, 1, 0, 0008, 0008, 0000, 30147, 0000, 0045, 0000, 0000, 0009, 0007, 0000, 130000, 0110, 2, 0481, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 7138, 0000, 0001);
INSERT INTO `spells` VALUES (7039, 7030, 0016, 'Celestial', 1, 1, 0, 0008, 0009, 0000, 30150, 0000, 0045, 0000, 0000, 0009, 0007, 0000, 0, 0000, 2, 0481, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 7139, 0000, 0001);
INSERT INTO `spells` VALUES (7040, 7040, 0004, 'Roamer', 1, 1, 0, 0008, 0000, 0000, 30130, 0000, 0045, 0000, 0004, 0009, 0001, 0000, 20243, 0015, 2, 0440, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7041, 7040, 0004, 'Roamer', 1, 1, 0, 0008, 0001, 0000, 30135, 0000, 0045, 0000, 0004, 0009, 0001, 0000, 37056, 0030, 2, 0440, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7042, 7040, 0004, 'Roamer', 1, 1, 0, 0008, 0002, 0000, 30140, 0000, 0045, 0000, 0004, 0009, 0001, 0000, 66011, 0045, 2, 0440, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7043, 7040, 0004, 'Roamer', 1, 1, 0, 0008, 0003, 0000, 30145, 0000, 0045, 0000, 0004, 0009, 0001, 0000, 116140, 0060, 2, 0440, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7044, 7040, 0004, 'Roamer', 1, 1, 0, 0008, 0004, 0000, 30150, 0000, 0045, 0000, 0004, 0009, 0001, 0000, 192800, 0070, 2, 0440, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7045, 7040, 0004, 'Roamer', 1, 1, 0, 0008, 0005, 0000, 30155, 0000, 0045, 0000, 0004, 0009, 0001, 0000, 418030, 0080, 2, 0440, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7046, 7040, 0004, 'Roamer', 1, 1, 0, 0008, 0006, 0000, 30160, 0000, 0045, 0000, 0005, 0009, 0001, 0000, 454350, 0090, 2, 0440, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7047, 7040, 0004, 'Roamer', 1, 1, 0, 0008, 0007, 0000, 30165, 0000, 0045, 0000, 0005, 0009, 0001, 0000, 491200, 0100, 2, 0440, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7048, 7040, 0004, 'Roamer', 1, 1, 0, 0008, 0008, 0000, 30170, 0000, 0045, 0000, 0005, 0009, 0001, 0000, 520030, 0110, 2, 0440, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7049, 7040, 0004, 'Roamer', 1, 1, 0, 0008, 0009, 0000, 30175, 0000, 0045, 0000, 0005, 0009, 0001, 0000, 0, 0000, 2, 0440, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7100, 7100, 0016, 'Seizer0', 1, 1, 0, 0008, 0000, 0000, 30110, 0000, 0100, 0000, 0000, 0015, 0007, 0000, 0, 0000, 2, 0430, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7101, 7101, 0016, 'Seizer1', 1, 1, 0, 0008, 0000, 0000, 30115, 0000, 0100, 0000, 0000, 0015, 0007, 0000, 0, 0000, 2, 0430, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7102, 7102, 0016, 'Seizer2', 1, 1, 0, 0008, 0000, 0000, 30119, 0000, 0100, 0000, 0000, 0015, 0007, 0000, 0, 0000, 2, 0430, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7103, 7103, 0016, 'Seizer3', 1, 1, 0, 0008, 0000, 0000, 30122, 0000, 0100, 0000, 0000, 0015, 0007, 0000, 0, 0000, 2, 0430, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7104, 7104, 0016, 'Seizer4', 1, 1, 0, 0008, 0000, 0000, 30125, 0000, 0100, 0000, 0000, 0015, 0007, 0000, 0, 0000, 2, 0430, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7105, 7105, 0016, 'Seizer5', 1, 1, 0, 0008, 0000, 0000, 30126, 0000, 0100, 0000, 0000, 0015, 0007, 0000, 0, 0000, 2, 0430, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7106, 7106, 0016, 'Seizer6', 1, 1, 0, 0008, 0000, 0000, 30127, 0000, 0100, 0000, 0000, 0015, 0007, 0000, 0, 0000, 2, 0430, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7107, 7107, 0016, 'Seizer7', 1, 1, 0, 0008, 0000, 0000, 30128, 0000, 0100, 0000, 0000, 0015, 0007, 0000, 0, 0000, 2, 0430, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7108, 7108, 0016, 'Seizer8', 1, 1, 0, 0008, 0000, 0000, 30129, 0000, 0100, 0000, 0000, 0015, 0007, 0000, 0, 0000, 2, 0430, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7109, 7109, 0016, 'Seizer9', 1, 1, 0, 0008, 0000, 0000, 30130, 0000, 0100, 0000, 0000, 0015, 0007, 0000, 0, 0000, 2, 0430, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (7130, 7130, 0016, 'Poison10', 1, 0, 0, 0008, 0000, 0000, 30001, 0000, 0100, 0002, 0000, 0015, 0002, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (7131, 7131, 0016, 'Poison11', 1, 0, 0, 0008, 0000, 0000, 30001, 0000, 0100, 0002, 0000, 0015, 0002, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (7132, 7132, 0016, 'Poison12', 1, 0, 0, 0008, 0000, 0000, 30001, 0000, 0100, 0002, 0000, 0015, 0002, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (7133, 7133, 0016, 'Poison13', 1, 0, 0, 0008, 0000, 0000, 30001, 0000, 0100, 0002, 0000, 0015, 0002, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (7134, 7134, 0016, 'Poison14', 1, 0, 0, 0008, 0000, 0000, 30001, 0000, 0100, 0002, 0000, 0015, 0002, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (7135, 7135, 0016, 'Poison15', 1, 0, 0, 0008, 0000, 0000, 30001, 0000, 0100, 0002, 0000, 0015, 0002, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (7136, 7136, 0016, 'Poison16', 1, 0, 0, 0008, 0000, 0000, 30002, 0000, 0100, 0002, 0000, 0015, 0002, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (7137, 7137, 0016, 'Poison17', 1, 0, 0, 0008, 0000, 0000, 30002, 0000, 0100, 0002, 0000, 0015, 0002, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (7138, 7138, 0016, 'Poison18', 1, 0, 0, 0008, 0000, 0000, 30002, 0000, 0100, 0002, 0000, 0015, 0002, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (7139, 7139, 0016, 'Poison19', 1, 0, 0, 0008, 0000, 0000, 30002, 0000, 0100, 0002, 0000, 0015, 0002, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (8000, 8000, 0001, 'RapidFire', 1, 0, 0, 0000, 0000, 0000, 30150, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 100000, 0056, 2, 0500, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (8001, 8000, 0001, 'RapidFire', 1, 0, 0, 0000, 0001, 0000, 30180, 0000, 0100, 0000, 0000, 0016, 0001, 0000, 150000, 0068, 2, 0500, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (8002, 8000, 0001, 'RapidFire', 1, 0, 0, 0000, 0002, 0000, 30210, 0000, 0100, 0000, 0000, 0017, 0001, 0000, 200000, 0078, 2, 0500, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (8003, 8000, 0001, 'RapidFire', 1, 0, 0, 0000, 0003, 0000, 30240, 0000, 0100, 0000, 0000, 0018, 0001, 0000, 250000, 0088, 2, 0500, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (8004, 8000, 0001, 'RapidFire', 1, 0, 0, 0000, 0004, 0000, 30270, 0000, 0100, 0000, 0000, 0019, 0001, 0000, 300000, 0098, 2, 0500, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (8005, 8000, 0001, 'RapidFire', 1, 0, 0, 0000, 0005, 0000, 30300, 0000, 0100, 0000, 0000, 0020, 0001, 0000, 0, 0000, 2, 0500, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (8010, 8001, 0004, 'ScatterFire', 1, 1, 0, 0004, 0000, 0000, 30050, 0000, 0100, 0000, 0008, 0015, 0001, 0000, 100000, 0032, 2, 0500, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (8011, 8001, 0004, 'ScatterFire', 1, 1, 0, 0004, 0001, 0000, 30055, 0000, 0100, 0000, 0009, 0016, 0001, 0000, 150000, 0048, 2, 0500, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (8012, 8001, 0004, 'ScatterFire', 1, 1, 0, 0004, 0002, 0000, 30060, 0000, 0100, 0000, 0010, 0017, 0001, 0000, 200000, 0054, 2, 0500, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (8013, 8001, 0004, 'ScatterFire', 1, 1, 0, 0004, 0003, 0000, 30065, 0000, 0100, 0000, 0011, 0018, 0001, 0000, 250000, 0066, 2, 0500, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (8014, 8001, 0004, 'ScatterFire', 1, 1, 0, 0004, 0004, 0000, 30068, 0000, 0100, 0000, 0012, 0019, 0001, 0000, 300000, 0072, 2, 0500, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (8015, 8001, 0004, 'ScatterFire', 1, 1, 0, 0004, 0005, 0000, 30072, 0000, 0100, 0000, 0013, 0020, 0001, 0000, 0, 0000, 2, 0500, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (8020, 8002, 0006, 'Fly', 0, 0, 0, 0002, 0000, 0000, 1, 0000, 0100, 0040, 0000, 0009, 0022, 0000, 0, 0000, 1, 0500, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (8021, 8003, 0006, 'Fly', 0, 0, 0, 0002, 0000, 0000, 1, 0000, 0100, 0040, 0000, 0009, 0022, 0000, 0, 0000, 0, 0500, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (8022, 8003, 0006, 'Fly', 0, 0, 0, 0002, 0001, 0000, 1, 0000, 0100, 0060, 0000, 0009, 0022, 0000, 0, 0000, 0, 0500, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (8030, 8030, 0005, 'ArrowRain', 1, 0, 1, 0000, 0000, 0000, 30100, 0000, 0100, 0000, 0009, 0009, 0001, 0000, 0, 0000, 1, 0500, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (8031, 8031, 0004, 'Scatter2', 1, 1, 0, 0004, 0000, 0000, 30030, 0000, 0100, 0000, 0008, 0016, 0001, 0000, 0, 0000, 0, 0500, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 8032, 0000, 0000);
INSERT INTO `spells` VALUES (8032, 8032, 0004, 'Scatter3', 1, 1, 0, 0004, 0000, 0000, 30030, 0000, 0100, 0000, 0009, 0017, 0001, 0000, 0, 0000, 0, 0500, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 8033, 0000, 0000);
INSERT INTO `spells` VALUES (8033, 8033, 0004, 'Scatter4', 1, 1, 0, 0004, 0000, 0000, 30030, 0000, 0100, 0000, 0009, 0018, 0001, 0000, 0, 0000, 0, 0500, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 8034, 0000, 0000);
INSERT INTO `spells` VALUES (8034, 8034, 0004, 'Scatter5', 1, 1, 0, 0004, 0000, 0000, 30030, 0000, 0100, 0000, 0010, 0019, 0001, 0000, 0, 0000, 0, 0500, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 8035, 0000, 0000);
INSERT INTO `spells` VALUES (8035, 8035, 0004, 'Scatter6', 1, 1, 0, 0004, 0000, 0000, 30030, 0000, 0100, 0000, 0010, 0020, 0001, 0000, 0, 0000, 0, 0500, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (8036, 8036, 0001, 'Guard`sSpell', 1, 0, 0, 0000, 0000, 0000, 30035, 0000, 0100, 0000, 0000, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (8040, 8040, 0019, 'Turkey', 0, 0, 0, 0002, 0000, 0000, 0, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8041, 8041, 0019, 'StoneMonster', 0, 0, 0, 0002, 0000, 0000, 0, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8042, 8042, 0019, 'FireSnake', 0, 0, 0, 0002, 0000, 0000, 0, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8043, 8043, 0019, 'RoaringApe', 0, 0, 0, 0002, 0000, 0000, 0, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8044, 8044, 0019, 'SandMonster', 0, 0, 0, 0002, 0000, 0000, 0, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8046, 8046, 0019, 'Temptress', 0, 0, 0, 0002, 0000, 0000, 5053, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8047, 8047, 0019, 'Siren', 0, 0, 0, 0002, 0000, 0000, 3584, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8056, 8056, 0019, 'EasterRooster', 0, 0, 0, 0002, 0000, 0000, 3706, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0055, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8057, 8057, 0019, 'EasterRooster', 0, 0, 0, 0002, 0000, 0000, 3707, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0055, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8058, 8058, 0019, 'EasterRooster', 0, 0, 0, 0002, 0000, 0000, 3708, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0055, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8059, 8059, 0019, 'EasterRooster', 0, 0, 0, 0002, 0000, 0000, 3709, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0055, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8060, 8060, 0019, 'RatKing', 0, 0, 0, 0002, 0000, 0000, 3042, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8061, 8061, 0019, 'FireRat', 0, 0, 0, 0002, 0000, 0000, 3043, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8062, 8062, 0019, 'ToxicRat', 0, 0, 0, 0002, 0000, 0000, 3044, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8068, 8068, 0019, 'FishLady', 0, 0, 0, 0002, 0000, 0000, 3580, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8080, 8080, 0019, 'Eidolon', 0, 0, 0, 0002, 0000, 0000, 5051, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8081, 8081, 0019, 'Eidolon', 0, 0, 0, 0002, 0000, 0000, 5051, 0000, 0100, 1200, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8082, 8082, 0019, 'UltimatePluto', 0, 0, 0, 0002, 0000, 0000, 4030, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8083, 8083, 0019, 'UltimatePluto', 0, 0, 0, 0002, 0000, 0000, 4030, 0000, 0100, 1200, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8084, 8084, 0019, 'Syren', 0, 0, 0, 0002, 0000, 0000, 3584, 0000, 0100, 1200, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8085, 8085, 0019, 'Temptress', 0, 0, 0, 0002, 0000, 0000, 5053, 0000, 0100, 1200, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8086, 8086, 0019, 'FireRatA', 0, 0, 0, 0002, 0000, 0000, 9020, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8087, 8087, 0019, 'FireRatA', 0, 0, 0, 0002, 0000, 0000, 9020, 0000, 0100, 1200, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8088, 8088, 0019, 'Cateran', 0, 0, 0, 0002, 0000, 0000, 3031, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8089, 8089, 0019, 'Cateran', 0, 0, 0, 0002, 0000, 0000, 3031, 0000, 0100, 1200, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8090, 8090, 0019, 'FurySkeleton', 0, 0, 0, 0002, 0000, 0000, 4031, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8091, 8091, 0019, 'FurySkeleton', 0, 0, 0, 0002, 0000, 0000, 4031, 0000, 0100, 1200, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8096, 8096, 0019, 'ElfKing1', 0, 0, 0, 0002, 0000, 0000, 4032, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8097, 8097, 0019, 'BloodyBat1', 0, 0, 0, 0002, 0000, 0000, 4033, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8098, 8098, 0019, 'HillMonsterKin', 0, 0, 0, 0002, 0000, 0000, 4034, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8099, 8099, 0019, 'BirdKing1', 0, 0, 0, 0002, 0000, 0000, 4035, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8100, 8100, 0019, 'WingedSnakeKin', 0, 0, 0, 0002, 0000, 0000, 4036, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8101, 8101, 0019, 'BlueCooer', 0, 0, 0, 0002, 0000, 0000, 22, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8102, 8102, 0019, 'Wraith', 0, 0, 0, 0002, 0000, 0000, 5055, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8103, 8103, 0019, 'Eidolon', 0, 0, 0, 0002, 0000, 0000, 5051, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8110, 8110, 0019, 'Temptress', 0, 0, 0, 0002, 0000, 0000, 5053, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8111, 8111, 0019, 'Siren', 0, 0, 0, 0002, 0000, 0000, 3584, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8112, 8112, 0019, 'BlueCock', 0, 0, 0, 0002, 0000, 0000, 3582, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8113, 8113, 0019, 'MagentaCock', 0, 0, 0, 0002, 0000, 0000, 3583, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8114, 8114, 0019, 'FishA', 0, 0, 0, 0002, 0000, 0000, 3586, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8115, 8115, 0019, 'BrownCock', 0, 0, 0, 0002, 0000, 0000, 3585, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8116, 8116, 0019, 'Wraith', 0, 0, 0, 0002, 0000, 0000, 5055, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8117, 8117, 0019, 'PurpleRooster', 0, 0, 0, 0002, 0000, 0000, 3706, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8118, 8118, 0019, 'SandMonster', 0, 0, 0, 0002, 0000, 0000, 3564, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8119, 8119, 0019, 'RoaringApe', 0, 0, 0, 0002, 0000, 0000, 31, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8120, 8120, 0019, 'Cateran', 0, 0, 0, 0002, 0000, 0000, 3031, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8121, 8121, 0019, 'FireSnake', 0, 0, 0, 0002, 0000, 0000, 3563, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8122, 8122, 0019, 'StoneMonster', 0, 0, 0, 0002, 0000, 0000, 3560, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8123, 8123, 0019, 'Turkey', 0, 0, 0, 0002, 0000, 0000, 3561, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8124, 8124, 0019, 'Snakeman', 0, 0, 0, 0002, 0000, 0000, 13, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8125, 8125, 0019, 'BullDevil', 0, 0, 0, 0002, 0000, 0000, 92, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8126, 8126, 0019, 'FishB', 0, 0, 0, 0002, 0000, 0000, 3579, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8127, 8127, 0019, 'Eidolon', 0, 0, 0, 0002, 0000, 0000, 5051, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8129, 8129, 0019, 'KungFuBunny', 0, 0, 0, 0002, 0000, 0000, 3320, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8130, 8130, 0019, 'Pheasant', 0, 0, 0, 0002, 0000, 0000, 2100, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8131, 8131, 0019, 'TurtleDove', 0, 0, 0, 0002, 0000, 0000, 2, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8132, 8132, 0019, 'Robin', 0, 0, 0, 0002, 0000, 0000, 2101, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8133, 8133, 0019, 'Apparition', 0, 0, 0, 0002, 0000, 0000, 2102, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8134, 8134, 0019, 'WingedSnake', 0, 0, 0, 0002, 0000, 0000, 6, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8135, 8135, 0019, 'Bandit', 0, 0, 0, 0002, 0000, 0000, 7, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8136, 8136, 0019, 'Ratling', 0, 0, 0, 0002, 0000, 0000, 8, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8137, 8137, 0019, 'Vampire', 0, 0, 0, 0002, 0000, 0000, 4082, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8138, 8138, 0019, 'Vampire', 0, 0, 0, 0002, 0000, 0000, 4083, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8139, 8139, 0019, 'Vampire', 0, 0, 0, 0002, 0000, 0000, 4084, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8140, 8140, 0019, 'Vampire', 0, 0, 0, 0002, 0000, 0000, 4085, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8141, 8141, 0019, 'Vampire', 0, 0, 0, 0002, 0000, 0000, 4086, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8142, 8142, 0019, 'Vampire', 0, 0, 0, 0002, 0000, 0000, 4087, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8143, 8143, 0019, 'Vampire', 0, 0, 0, 0002, 0000, 0000, 4088, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8144, 8144, 0019, 'Vampire', 0, 0, 0, 0002, 0000, 0000, 4088, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8204, 8204, 0019, 'PinkPiglet', 0, 0, 0, 0002, 0000, 0000, 4058, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8210, 8210, 0019, 'PinkPiglet', 0, 0, 0, 0002, 0000, 0000, 4060, 0000, 0100, 0120, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8212, 8212, 0019, 'yishe', 0, 0, 0, 0002, 0000, 0000, 6, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8213, 8213, 0019, 'Transform8', 0, 0, 0, 0002, 0000, 0000, 7, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8214, 8214, 0019, 'ChristmasElf', 0, 0, 0, 0002, 0000, 0000, 3039, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8215, 8215, 0019, 'Pig', 0, 0, 0, 0002, 0000, 0000, 4060, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8216, 8216, 0019, 'FireRat', 0, 0, 0, 0002, 0000, 0000, 8, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8218, 8218, 0019, 'Bat', 0, 0, 0, 0002, 0000, 0000, 2131, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8219, 8219, 0019, 'Mink', 0, 0, 0, 0002, 0000, 0000, 2132, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8225, 8225, 0019, 'GrumPig', 0, 0, 0, 0002, 0000, 0000, 4060, 0000, 0100, 7200, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8226, 8226, 0019, 'LavaRat', 0, 0, 0, 0002, 0000, 0000, 8, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8257, 8257, 0019, 'BanditsL95', 0, 0, 0, 0002, 0000, 0000, 7, 0000, 0100, 0090, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8258, 8258, 0019, 'WeirdPumpkin', 0, 0, 0, 0002, 0000, 0000, 2237, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8259, 8259, 0019, 'ElfMessenger', 0, 0, 0, 0002, 0000, 0000, 8104, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8260, 8260, 0019, 'EvilSnakeMsgr', 0, 0, 0, 0002, 0000, 0000, 8101, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8261, 8261, 0019, 'ScriptElf', 0, 0, 0, 0002, 0000, 0000, 3634, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8262, 8262, 0019, 'BloodyBat', 0, 0, 0, 0002, 0000, 0000, 3621, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8270, 8270, 0019, 'Pheasant', 0, 0, 0, 0002, 0000, 0000, 2100, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8271, 8271, 0019, 'Turtledove', 0, 0, 0, 0002, 0000, 0000, 2, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8272, 8272, 0019, 'Robin', 0, 0, 0, 0002, 0000, 0000, 2101, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8273, 8273, 0019, 'Apparition', 0, 0, 0, 0002, 0000, 0000, 2102, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8274, 8274, 0019, 'WingedSnake', 0, 0, 0, 0002, 0000, 0000, 6, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8275, 8275, 0019, 'Bandit', 0, 0, 0, 0002, 0000, 0000, 7, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8276, 8276, 0019, 'Ratling', 0, 0, 0, 0002, 0000, 0000, 8, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8279, 8279, 0019, 'Babirusa', 0, 0, 0, 0002, 0000, 0000, 4060, 0000, 0100, 0180, 0000, 0005, 0000, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8280, 8280, 0019, 'Rabbit', 0, 0, 0, 0002, 0000, 0000, 3322, 0000, 0100, 0180, 0000, 0005, 0000, 0000, 0, 0055, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8282, 8282, 0019, 'BigWhirl', 0, 0, 0, 0002, 0000, 0000, 9020, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8283, 8283, 0019, 'LittleWhirl', 0, 0, 0, 0002, 0000, 0000, 3160, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8284, 8284, 0019, 'BronzeHead', 0, 0, 0, 0002, 0000, 0000, 5054, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8285, 8285, 0019, 'IronHead', 0, 0, 0, 0002, 0000, 0000, 2328, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8286, 8286, 0019, 'BigLark', 0, 0, 0, 0002, 0000, 0000, 1, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8287, 8287, 0019, 'LittleLark', 0, 0, 0, 0002, 0000, 0000, 2, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8288, 8288, 0019, 'BlueDragon', 0, 0, 0, 0002, 0000, 0000, 3606, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8289, 8289, 0019, 'RedDragon', 0, 0, 0, 0002, 0000, 0000, 3605, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8290, 8290, 0019, 'Happy', 0, 0, 0, 0002, 0000, 0000, 2329, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8291, 8291, 0019, 'VeryHappy', 0, 0, 0, 0002, 0000, 0000, 8419, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8292, 8292, 0019, 'Loud', 0, 0, 0, 0002, 0000, 0000, 26, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8293, 8293, 0019, 'VeryLoud', 0, 0, 0, 0002, 0000, 0000, 2327, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8294, 8294, 0019, 'BigJay', 0, 0, 0, 0002, 0000, 0000, 5058, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8295, 8295, 0019, 'LittleJay', 0, 0, 0, 0002, 0000, 0000, 3634, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8296, 8296, 0019, 'BrightWing', 0, 0, 0, 0002, 0000, 0000, 3147, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8297, 8297, 0019, 'DarkWing', 0, 0, 0, 0002, 0000, 0000, 3, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8298, 8298, 0019, 'GoodLuck', 0, 0, 0, 0002, 0000, 0000, 4058, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8299, 8299, 0019, 'BetterLuck', 0, 0, 0, 0002, 0000, 0000, 2325, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8300, 8300, 0019, 'GoodFly', 0, 0, 0, 0002, 0000, 0000, 93, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8301, 8301, 0019, 'BetterFly', 0, 0, 0, 0002, 0000, 0000, 2326, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8302, 8302, 0019, 'YellowOx', 0, 0, 0, 0002, 0000, 0000, 2340, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8303, 8303, 0019, 'HeavenOx', 0, 0, 0, 0002, 0000, 0000, 2351, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8304, 8304, 0019, 'GoldenOx', 0, 0, 0, 0002, 0000, 0000, 2352, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8305, 8305, 0019, 'SilverOx', 0, 0, 0, 0002, 0000, 0000, 2353, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8307, 8307, 0019, 'RedFox1', 0, 0, 0, 0002, 0000, 0000, 2354, 0000, 0100, 7200, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8308, 8308, 0019, 'BlueFox1', 0, 0, 0, 0002, 0000, 0000, 2355, 0000, 0100, 7200, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8309, 8309, 0019, 'RedFox2', 0, 0, 0, 0002, 0000, 0000, 2354, 0000, 0100, 0010, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8310, 8310, 0019, 'BlueFox2', 0, 0, 0, 0002, 0000, 0000, 2355, 0000, 0100, 0010, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8311, 8311, 0019, 'Guard', 0, 0, 0, 0002, 0000, 0000, 2367, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8312, 8312, 0019, 'TurtleDove', 0, 0, 0, 0002, 0000, 0000, 2, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8313, 8313, 0019, 'Ratling', 0, 0, 0, 0002, 0000, 0000, 8, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8314, 8314, 0019, 'WingedSnake', 0, 0, 0, 0002, 0000, 0000, 6, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8318, 8318, 0019, 'Transform1', 0, 0, 0, 0002, 0000, 0000, 2102, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8319, 8319, 0019, 'Transform2', 0, 0, 0, 0002, 0000, 0000, 13, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8320, 8320, 0019, 'Transform3', 0, 0, 0, 0002, 0000, 0000, 31, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8321, 8321, 0019, 'Transform4', 0, 0, 0, 0002, 0000, 0000, 3322, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0055, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8322, 8322, 0019, 'Transform5', 0, 0, 0, 0002, 0000, 0000, 4060, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8323, 8323, 0019, 'Transform6', 0, 0, 0, 0002, 0000, 0000, 3007, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8324, 8324, 0019, 'Transform7', 0, 0, 0, 0002, 0000, 0000, 2351, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8325, 8325, 0019, 'Minotaur', 0, 0, 0, 0002, 0000, 0000, 92, 0000, 0100, 0015, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8326, 8326, 0019, 'MaleGhost1', 0, 0, 0, 0002, 0000, 0000, 2430, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8327, 8317, 0019, 'FemaleGhost1', 0, 0, 0, 0002, 0000, 0000, 2431, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8329, 8329, 0019, 'Piggy', 0, 0, 0, 0002, 0000, 0000, 4060, 0000, 0100, 0600, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8330, 8330, 0019, 'Bandit1', 0, 0, 0, 0002, 0000, 0000, 7, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8332, 8332, 0019, 'Bandit2', 0, 0, 0, 0002, 0000, 0000, 7, 0000, 0100, 1800, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8333, 8333, 0019, 'OccultBeast2', 0, 0, 0, 0002, 0000, 0000, 2328, 0000, 0100, 1800, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8334, 8334, 0019, 'WaterElf2', 0, 0, 0, 0002, 0000, 0000, 2043, 0000, 0100, 1800, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8335, 8335, 0019, 'OccultBeast1', 0, 0, 0, 0002, 0000, 0000, 2328, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8336, 8336, 0019, 'WaterElf1', 0, 0, 0, 0002, 0000, 0000, 2043, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8337, 8337, 0019, 'Pumpkin1', 0, 0, 0, 0002, 0000, 0000, 3058, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8338, 8338, 0019, 'Pumpkin2', 0, 0, 0, 0002, 0000, 0000, 3059, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8339, 8339, 0019, 'Pumpkin3', 0, 0, 0, 0002, 0000, 0000, 3055, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8341, 8341, 0019, 'PinkPiglet', 0, 0, 0, 0002, 0000, 0000, 4060, 0000, 0100, 1800, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8342, 8342, 0019, 'KungFuBunny', 0, 0, 0, 0002, 0000, 0000, 3320, 0000, 0100, 1800, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8343, 8343, 0019, 'NightDevil', 0, 0, 0, 0002, 0000, 0000, 2030, 0000, 0100, 1800, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8344, 8344, 0019, 'IronHead', 0, 0, 0, 0002, 0000, 0000, 2328, 0000, 0100, 1800, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8345, 8345, 0019, 'FireRatA', 0, 0, 0, 0002, 0000, 0000, 9020, 0000, 0100, 1800, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8350, 8350, 0019, 'HeavenTiger', 0, 0, 0, 0002, 0000, 0000, 4144, 0000, 0100, 0060, 0000, 0005, 0000, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8351, 8351, 0019, 'GoldenTiger', 0, 0, 0, 0002, 0000, 0000, 4145, 0000, 0100, 0060, 0000, 0005, 0000, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8352, 8352, 0019, 'SilverTiger', 0, 0, 0, 0002, 0000, 0000, 4146, 0000, 0100, 0060, 0000, 0005, 0000, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (8353, 8353, 0019, 'TigerRoarer', 0, 0, 0, 0002, 0000, 0000, 4147, 0000, 0100, 0300, 0000, 0005, 0000, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (9000, 9000, 0006, 'Intensify', 0, 0, 0, 0002, 0000, 0000, 30200, 0000, 0100, 0060, 0000, 0015, 0023, 0000, 3000, 0083, 2, 0500, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9001, 9000, 0006, 'Intensify', 0, 0, 0, 0002, 0001, 0000, 30250, 0000, 0100, 0080, 0000, 0015, 0023, 0000, 4000, 0093, 2, 0500, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9002, 9000, 0006, 'Intensify', 0, 0, 0, 0002, 0002, 0000, 30300, 0000, 0100, 0100, 0000, 0015, 0023, 0000, 5000, 0104, 2, 0500, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9003, 9000, 0006, 'Intensify', 0, 0, 0, 0002, 0003, 0000, 30350, 0000, 0100, 0120, 0000, 0015, 0023, 0000, 0, 0000, 2, 0500, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9950, 7011, 0033, 'TailSweep', 0, 0, 0, 0004, 0000, 0000, 1200, 0000, 0100, 0000, 0008, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (9951, 7012, 0001, 'Destroy', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0005, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (9952, 7013, 0035, 'FlameShower', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0005, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (9953, 7014, 0004, 'DragonBreath', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (9954, 7015, 0034, 'FearRoar', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0010, 0015, 0020, 0051, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (9955, 7016, 0002, 'BloodRestore', 0, 0, 0, 0001, 0000, 0000, 250, 0000, 0100, 0000, 0000, 0000, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (9956, 7017, 0036, 'MistyFlame', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (9965, 9965, 0001, 'MistyQuake', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9966, 9966, 0001, 'RoarBlade', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9967, 9967, 0001, 'Scorch', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9968, 9968, 0001, 'ArchChop', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9969, 9969, 0001, 'AsuraChop', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9970, 9970, 0001, 'DespotKill', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9971, 9971, 0001, 'SandyShatter', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9972, 9972, 0001, 'DestinyFire', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9973, 9973, 0001, 'NineFlames', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9974, 9974, 0001, 'DestinyThunder', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9975, 9975, 0001, 'HeavenShatter', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9976, 9976, 0001, 'DeathChop', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9977, 9977, 0001, 'InfernoSting', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9978, 9978, 0004, 'RedLocust', 0, 0, 0, 0004, 0000, 0000, 21, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9979, 9979, 0004, 'BlueLocust', 0, 0, 0, 0004, 0000, 0000, 35, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9980, 9980, 0004, 'WhiteLocust', 0, 0, 0, 0004, 0000, 0000, 50, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9981, 9981, 0004, 'ScarletFog', 0, 0, 0, 0004, 0000, 0000, 55, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9982, 9982, 0004, 'WhiteShuttle', 0, 0, 0, 0004, 0000, 0000, 60, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9983, 9983, 0004, 'FireShower', 0, 0, 0, 0004, 0000, 0000, 70, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9984, 9984, 0004, 'FeralAttack', 0, 0, 0, 0004, 0000, 0000, 80, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9985, 9985, 0004, 'BlueShadow', 0, 0, 0, 0004, 0000, 0000, 85, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9986, 9986, 0004, 'PurpleShadow', 0, 0, 0, 0004, 0000, 0000, 95, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9987, 9987, 0004, 'FanaticBite', 0, 0, 0, 0004, 0000, 0000, 100, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9988, 9988, 0004, 'FearRoar', 0, 0, 0, 0004, 0000, 0000, 110, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9989, 9989, 0004, 'IcyCurse', 0, 0, 0, 0004, 0000, 0000, 115, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9990, 9990, 0004, 'WhirlCurse', 0, 0, 0, 0004, 0000, 0000, 125, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9991, 9991, 0004, 'SandBite', 0, 0, 0, 0004, 0000, 0000, 130, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9992, 9992, 0004, 'FlyingFlame', 0, 0, 0, 0004, 0000, 0000, 140, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9993, 9993, 0004, 'FireImplosion', 0, 0, 0, 0004, 0000, 0000, 148, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9994, 9994, 0004, 'DireScorch', 0, 0, 0, 0004, 0000, 0000, 155, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9995, 9995, 0004, 'FirePenetration', 0, 0, 0, 0004, 0000, 0000, 160, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9996, 9996, 0004, 'SwiftBlaze', 0, 0, 0, 0004, 0000, 0000, 170, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9997, 9997, 0004, 'DarkShadow', 0, 0, 0, 0004, 0000, 0000, 180, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9998, 9998, 0004, 'PoisonousAwn', 0, 0, 0, 0004, 0000, 0000, 185, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (9999, 9999, 0004, 'FeralBoil', 0, 0, 0, 0004, 0000, 0000, 200, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (10000, 10000, 0001, 'Blade', 0, 0, 0, 0004, 0000, 0000, 500, 0000, 0100, 0000, 0008, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (10001, 10001, 0004, 'Smash', 0, 0, 0, 0004, 0000, 0000, 600, 0000, 0100, 0000, 0008, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (10002, 10002, 0034, 'Roar', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (10003, 10003, 0033, 'Sweep', 0, 0, 0, 0004, 0000, 0000, 700, 0000, 0100, 0000, 0005, 0010, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (10004, 10004, 0004, 'UnholyImplosion', 0, 0, 0, 0004, 0000, 0000, 205, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (10005, 10005, 0004, 'AcidicBreath', 0, 0, 0, 0004, 0000, 0000, 210, 0000, 0100, 0000, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (10010, 10010, 0016, 'Poison0', 1, 0, 0, 0008, 0000, 0000, 30002, 0000, 0100, 0002, 0000, 0015, 0002, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (10011, 10011, 0016, 'Poison1', 1, 0, 0, 0008, 0000, 0000, 30002, 0000, 0100, 0002, 0000, 0015, 0002, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (10012, 10012, 0016, 'Poison2', 1, 0, 0, 0008, 0000, 0000, 30002, 0000, 0100, 0002, 0000, 0015, 0002, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (10013, 10013, 0016, 'Poison3', 1, 0, 0, 0008, 0000, 0000, 30002, 0000, 0100, 0002, 0000, 0015, 0002, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (10015, 10015, 0016, 'IcyArrow0', 1, 0, 0, 0000, 0000, 0000, 10000, 0000, 0100, 0000, 0000, 0009, 0016, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (10016, 10016, 0016, 'IcyArrow1', 1, 0, 0, 0000, 0000, 0000, 15000, 0000, 0100, 0000, 0000, 0009, 0016, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (10017, 10017, 0016, 'IcyArrow2', 1, 0, 0, 0000, 0000, 0000, 20000, 0000, 0100, 0000, 0000, 0009, 0016, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (10020, 10020, 0016, 'Bolt0', 1, 0, 0, 0008, 0000, 0000, 30100, 0000, 0100, 0000, 0000, 0015, 0007, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10030, 0000, 0000);
INSERT INTO `spells` VALUES (10021, 10021, 0016, 'Bolt1', 1, 0, 0, 0008, 0000, 0000, 30100, 0000, 0100, 0000, 0000, 0015, 0007, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10031, 0000, 0000);
INSERT INTO `spells` VALUES (10022, 10022, 0016, 'Bolt2', 1, 0, 0, 0008, 0000, 0000, 30100, 0000, 0100, 0000, 0000, 0015, 0007, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10032, 0000, 0000);
INSERT INTO `spells` VALUES (10023, 10023, 0016, 'Bolt3', 1, 0, 0, 0008, 0000, 0000, 30100, 0000, 0100, 0000, 0000, 0015, 0007, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10033, 0000, 0000);
INSERT INTO `spells` VALUES (10030, 10030, 0001, 'Bolt10', 1, 0, 0, 0000, 0000, 0000, 30100, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10040, 0000, 0000);
INSERT INTO `spells` VALUES (10031, 10031, 0001, 'Bolt11', 1, 0, 0, 0000, 0000, 0000, 30100, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10041, 0000, 0000);
INSERT INTO `spells` VALUES (10032, 10032, 0001, 'Bolt12', 1, 0, 0, 0000, 0000, 0000, 30100, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10042, 0000, 0000);
INSERT INTO `spells` VALUES (10033, 10033, 0001, 'Bolt13', 1, 0, 0, 0000, 0000, 0000, 30100, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10043, 0000, 0000);
INSERT INTO `spells` VALUES (10040, 10040, 0001, 'Bolt20', 1, 0, 0, 0000, 0000, 0000, 30100, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10050, 0000, 0000);
INSERT INTO `spells` VALUES (10041, 10041, 0001, 'Bolt21', 1, 0, 0, 0000, 0000, 0000, 30100, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10051, 0000, 0000);
INSERT INTO `spells` VALUES (10042, 10042, 0001, 'Bolt22', 1, 0, 0, 0000, 0000, 0000, 30100, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10052, 0000, 0000);
INSERT INTO `spells` VALUES (10043, 10043, 0001, 'Bolt23', 1, 0, 0, 0000, 0000, 0000, 30100, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10053, 0000, 0000);
INSERT INTO `spells` VALUES (10050, 10050, 0001, 'ArrowStorm0', 1, 0, 0, 0000, 0000, 0000, 30120, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10060, 0000, 0000);
INSERT INTO `spells` VALUES (10051, 10051, 0001, 'ArrowStorm1', 1, 0, 0, 0000, 0000, 0000, 30140, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10061, 0000, 0000);
INSERT INTO `spells` VALUES (10052, 10052, 0001, 'ArrowStorm2', 1, 0, 0, 0000, 0000, 0000, 30160, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10062, 0000, 0000);
INSERT INTO `spells` VALUES (10053, 10053, 0001, 'ArrowStorm3', 1, 0, 0, 0000, 0000, 0000, 30180, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10063, 0000, 0000);
INSERT INTO `spells` VALUES (10060, 10060, 0001, 'ArrowStorm10', 1, 0, 0, 0000, 0000, 0000, 30120, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10070, 0000, 0000);
INSERT INTO `spells` VALUES (10061, 10061, 0001, 'ArrowStorm11', 1, 0, 0, 0000, 0000, 0000, 30140, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10071, 0000, 0000);
INSERT INTO `spells` VALUES (10062, 10062, 0001, 'ArrowStorm12', 1, 0, 0, 0000, 0000, 0000, 30160, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10072, 0000, 0000);
INSERT INTO `spells` VALUES (10063, 10063, 0001, 'ArrowStorm13', 1, 0, 0, 0000, 0000, 0000, 30180, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10073, 0000, 0000);
INSERT INTO `spells` VALUES (10070, 10070, 0001, 'ArrowStorm20', 1, 0, 0, 0000, 0000, 0000, 30120, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10080, 0000, 0000);
INSERT INTO `spells` VALUES (10071, 10071, 0001, 'ArrowStorm21', 1, 0, 0, 0000, 0000, 0000, 30140, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10080, 0000, 0000);
INSERT INTO `spells` VALUES (10072, 10072, 0001, 'ArrowStorm22', 1, 0, 0, 0000, 0000, 0000, 30160, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10080, 0000, 0000);
INSERT INTO `spells` VALUES (10073, 10073, 0001, 'ArrowStorm23', 1, 0, 0, 0000, 0000, 0000, 30180, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10080, 0000, 0000);
INSERT INTO `spells` VALUES (10080, 10080, 0001, 'ArrowStorm30', 1, 0, 0, 0000, 0000, 0000, 30140, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10081, 0000, 0000);
INSERT INTO `spells` VALUES (10081, 10081, 0001, 'ArrowStorm31', 1, 0, 0, 0000, 0000, 0000, 30160, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10082, 0000, 0000);
INSERT INTO `spells` VALUES (10082, 10082, 0001, 'ArrowStorm32', 1, 0, 0, 0000, 0000, 0000, 30170, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10083, 0000, 0000);
INSERT INTO `spells` VALUES (10083, 10083, 0001, 'ArrowStorm33', 1, 0, 0, 0000, 0000, 0000, 30200, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (10100, 10100, 0016, 'Rocket0', 1, 0, 0, 0008, 0000, 0000, 30100, 0000, 0100, 0000, 0000, 0015, 0007, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10110, 0000, 0000);
INSERT INTO `spells` VALUES (10101, 10101, 0016, 'Rocket1', 1, 0, 0, 0008, 0000, 0000, 30100, 0000, 0100, 0000, 0000, 0015, 0007, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10111, 0000, 0000);
INSERT INTO `spells` VALUES (10102, 10102, 0016, 'Rocket2', 1, 0, 0, 0008, 0000, 0000, 30100, 0000, 0100, 0000, 0000, 0015, 0007, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10112, 0000, 0000);
INSERT INTO `spells` VALUES (10103, 10103, 0016, 'Rocket3', 1, 0, 0, 0008, 0000, 0000, 30100, 0000, 0100, 0000, 0000, 0015, 0007, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10113, 0000, 0000);
INSERT INTO `spells` VALUES (10110, 10110, 0001, 'Rocket10', 1, 0, 0, 0000, 0000, 0000, 30100, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10120, 0000, 0000);
INSERT INTO `spells` VALUES (10111, 10111, 0001, 'Rocket11', 1, 0, 0, 0000, 0000, 0000, 30100, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10121, 0000, 0000);
INSERT INTO `spells` VALUES (10112, 10112, 0001, 'Rocket12', 1, 0, 0, 0000, 0000, 0000, 30100, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10122, 0000, 0000);
INSERT INTO `spells` VALUES (10113, 10113, 0001, 'Rocket13', 1, 0, 0, 0000, 0000, 0000, 30100, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10123, 0000, 0000);
INSERT INTO `spells` VALUES (10120, 10120, 0001, 'Rocket20', 1, 0, 0, 0000, 0000, 0000, 30100, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10130, 0000, 0000);
INSERT INTO `spells` VALUES (10121, 10121, 0001, 'Rocket21', 1, 0, 0, 0000, 0000, 0000, 30100, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10131, 0000, 0000);
INSERT INTO `spells` VALUES (10122, 10122, 0001, 'Rocket22', 1, 0, 0, 0000, 0000, 0000, 30100, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10132, 0000, 0000);
INSERT INTO `spells` VALUES (10123, 10123, 0001, 'Rocket23', 1, 0, 0, 0000, 0000, 0000, 30100, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10133, 0000, 0000);
INSERT INTO `spells` VALUES (10130, 10130, 0005, 'Rocket30', 1, 1, 0, 0004, 0000, 0000, 30030, 0000, 0100, 0000, 0010, 0010, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10140, 0000, 0000);
INSERT INTO `spells` VALUES (10131, 10131, 0005, 'Rocket31', 1, 1, 0, 0004, 0000, 0000, 30040, 0000, 0100, 0000, 0012, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10141, 0000, 0000);
INSERT INTO `spells` VALUES (10132, 10132, 0005, 'Rocket32', 1, 1, 0, 0004, 0000, 0000, 30050, 0000, 0100, 0000, 0014, 0014, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10142, 0000, 0000);
INSERT INTO `spells` VALUES (10133, 10133, 0005, 'Rocket33', 1, 1, 0, 0004, 0000, 0000, 30060, 0000, 0100, 0000, 0016, 0016, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10143, 0000, 0000);
INSERT INTO `spells` VALUES (10140, 10140, 0005, 'Rocket40', 1, 1, 0, 0004, 0000, 0000, 30040, 0000, 0100, 0000, 0010, 0010, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10150, 0000, 0000);
INSERT INTO `spells` VALUES (10141, 10141, 0005, 'Rocket41', 1, 1, 0, 0004, 0000, 0000, 30050, 0000, 0100, 0000, 0012, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10151, 0000, 0000);
INSERT INTO `spells` VALUES (10142, 10142, 0005, 'Rocket42', 1, 1, 0, 0004, 0000, 0000, 30060, 0000, 0100, 0000, 0014, 0014, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10152, 0000, 0000);
INSERT INTO `spells` VALUES (10143, 10143, 0005, 'Rocket43', 1, 1, 0, 0004, 0000, 0000, 30070, 0000, 0100, 0000, 0016, 0016, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10153, 0000, 0000);
INSERT INTO `spells` VALUES (10150, 10150, 0005, 'Rocket50', 1, 1, 0, 0004, 0000, 0000, 30030, 0000, 0100, 0000, 0010, 0010, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10160, 0000, 0000);
INSERT INTO `spells` VALUES (10151, 10151, 0005, 'Rocket51', 1, 1, 0, 0004, 0000, 0000, 30040, 0000, 0100, 0000, 0012, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10161, 0000, 0000);
INSERT INTO `spells` VALUES (10152, 10152, 0005, 'Rocket52', 1, 1, 0, 0004, 0000, 0000, 30050, 0000, 0100, 0000, 0014, 0014, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10162, 0000, 0000);
INSERT INTO `spells` VALUES (10153, 10153, 0005, 'Rocket53', 1, 1, 0, 0004, 0000, 0000, 30060, 0000, 0100, 0000, 0016, 0016, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10163, 0000, 0000);
INSERT INTO `spells` VALUES (10160, 10160, 0005, 'Rocket60', 1, 1, 0, 0004, 0000, 0000, 30040, 0000, 0100, 0000, 0010, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10170, 0000, 0000);
INSERT INTO `spells` VALUES (10161, 10161, 0005, 'Rocket61', 1, 1, 0, 0004, 0000, 0000, 30050, 0000, 0100, 0000, 0012, 0014, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10170, 0000, 0000);
INSERT INTO `spells` VALUES (10162, 10162, 0005, 'Rocket62', 1, 1, 0, 0004, 0000, 0000, 30060, 0000, 0100, 0000, 0014, 0016, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10180, 0000, 0000);
INSERT INTO `spells` VALUES (10163, 10163, 0005, 'Rocket63', 1, 1, 0, 0004, 0000, 0000, 30070, 0000, 0100, 0000, 0016, 0018, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10180, 0000, 0000);
INSERT INTO `spells` VALUES (10170, 10170, 0005, 'Rocket70', 1, 1, 0, 0004, 0000, 0000, 30030, 0000, 0100, 0000, 0010, 0014, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10171, 0000, 0000);
INSERT INTO `spells` VALUES (10171, 10171, 0005, 'Rocket71', 1, 1, 0, 0004, 0000, 0000, 30040, 0000, 0100, 0000, 0012, 0016, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10172, 0000, 0000);
INSERT INTO `spells` VALUES (10172, 10172, 0005, 'Rocket72', 1, 1, 0, 0004, 0000, 0000, 30050, 0000, 0100, 0000, 0014, 0018, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10173, 0000, 0000);
INSERT INTO `spells` VALUES (10173, 10173, 0005, 'Rocket73', 1, 1, 0, 0004, 0000, 0000, 30060, 0000, 0100, 0000, 0016, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (10180, 10180, 0005, 'Rocket80', 1, 1, 0, 0004, 0000, 0000, 30050, 0000, 0100, 0000, 0020, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10181, 0000, 0000);
INSERT INTO `spells` VALUES (10181, 10181, 0005, 'Rocket81', 1, 1, 0, 0004, 0000, 0000, 30060, 0000, 0100, 0000, 0020, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10182, 0000, 0000);
INSERT INTO `spells` VALUES (10182, 10182, 0005, 'Rocket82', 1, 1, 0, 0004, 0000, 0000, 30070, 0000, 0100, 0000, 0020, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 10183, 0000, 0000);
INSERT INTO `spells` VALUES (10183, 10183, 0005, 'Rocket83', 1, 1, 0, 0004, 0000, 0000, 30080, 0000, 0100, 0000, 0020, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (10200, 10200, 0016, 'Poisoning', 1, 0, 0, 0008, 0000, 0000, 30002, 0000, 0005, 0002, 0000, 0012, 0002, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10201, 10201, 0002, 'Cure', 0, 0, 0, 0008, 0000, 0000, 300, 0000, 0020, 0000, 0000, 0010, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10202, 10202, 0020, 'Meditation', 0, 0, 0, 0008, 0000, 0000, 310, 0000, 0020, 0000, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10203, 10203, 0006, 'MagicShield', 0, 0, 0, 0008, 0000, 0000, 30130, 0000, 0001, 0030, 0000, 0010, 0006, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10300, 9876, 0006, 'Bless', 0, 0, 0, 0002, 0000, 0000, 1, 0000, 0100, 1000000, 0000, 0000, 0025, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0100, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10301, 7001, 0032, 'Riding', 0, 0, 0, 0002, 0000, 0000, 1, 0000, 0100, 1000000, 0000, 0000, 0047, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10302, 7002, 0030, 'Spook', 0, 0, 0, 0000, 0000, 0000, 0, 0000, 0100, 0000, 0010, 0010, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10303, 7003, 0031, 'WarCry', 0, 0, 0, 0000, 0000, 0000, 0, 0000, 0100, 0000, 0010, 0010, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10304, 10304, 0034, 'SoulEnticer', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0003, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10305, 10305, 0034, 'AtrociousSeal', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0003, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10306, 10306, 0034, 'AppalingWind', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0003, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10307, 10307, 0034, 'IcySting', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0003, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10308, 10308, 0034, 'Thwart', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0003, 0006, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10309, 10309, 0037, 'ChainBolt', 1, 0, 1, 0000, 0000, 0000, 275, 0000, 0100, 0060, 0004, 0010, 0088, 0000, 628464, 0040, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10310, 10310, 0001, 'HeavenBlade', 1, 0, 0, 0000, 0000, 0100, 2500, 0000, 0070, 0000, 0000, 0010, 0001, 0000, 28028389, 0040, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10311, 10311, 0041, 'Perseverance', 1, 0, 0, 0008, 0000, 0000, 0, 0000, 0000, 0000, 0000, 0000, 0001, 0000, 0, 0000, 3, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10312, 30000, 0006, 'AzureShield', 0, 0, 0, 0001, 0000, 0080, 0, 0000, 0100, 0000, 0000, 0020, 0089, 0000, 950, 0040, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10313, 10313, 0038, 'StarArrow', 1, 0, 0, 0000, 0000, 0000, 30100, 0000, 0090, 0000, 0011, 0015, 0001, 0000, 250000, 0040, 0, 0500, 0000, 0, 0000, 0, 0000, 0, 0050, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (10314, 10310, 0001, 'HeavenBlade', 1, 0, 0, 0000, 0001, 0100, 3500, 0000, 0070, 0000, 0000, 0012, 0001, 0000, 56976183, 0050, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10315, 10315, 0040, 'DragonWhirl', 1, 0, 1, 0004, 0000, 0000, 30070, 0000, 0100, 0000, 0003, 0020, 0001, 0000, 191500, 0040, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0003);
INSERT INTO `spells` VALUES (10316, 10315, 0040, 'DragonWhirl', 1, 0, 1, 0004, 0001, 0000, 30080, 0000, 0100, 0000, 0003, 0020, 0001, 0000, 758600, 0050, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0003);
INSERT INTO `spells` VALUES (10317, 10315, 0040, 'DragonWhirl', 1, 0, 1, 0004, 0002, 0000, 30080, 0000, 0100, 0000, 0004, 0020, 0001, 0000, 1564500, 0070, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0003);
INSERT INTO `spells` VALUES (10318, 10315, 0040, 'DragonWhirl', 1, 0, 1, 0004, 0003, 0000, 30090, 0000, 0100, 0000, 0004, 0020, 0001, 0000, 3685000, 0090, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0003);
INSERT INTO `spells` VALUES (10319, 10315, 0040, 'DragonWhirl', 1, 0, 1, 0004, 0004, 0000, 30090, 0000, 0100, 0000, 0005, 0020, 0001, 0000, 0, 0110, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0003);
INSERT INTO `spells` VALUES (10320, 10310, 0001, 'HeavenBlade', 1, 0, 0, 0000, 0002, 0100, 5000, 0000, 0070, 0000, 0000, 0014, 0001, 0000, 136801964, 0070, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10321, 10310, 0001, 'HeavenBlade', 1, 0, 0, 0000, 0003, 0100, 7000, 0000, 0070, 0000, 0000, 0015, 0001, 0000, 298009073, 0090, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10322, 10310, 0001, 'HeavenBlade', 1, 0, 0, 0000, 0004, 0100, 10000, 0000, 0070, 0000, 0000, 0015, 0001, 0000, 0, 0110, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10327, 10313, 0038, 'StarArrow', 1, 0, 0, 0000, 0001, 0000, 30104, 0000, 0090, 0000, 0012, 0016, 0001, 0000, 450000, 0050, 0, 0500, 0000, 0, 0000, 0, 0000, 0, 0050, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (10328, 10313, 0038, 'StarArrow', 1, 0, 0, 0000, 0002, 0000, 30108, 0000, 0090, 0000, 0013, 0017, 0001, 0000, 650000, 0070, 0, 0500, 0000, 0, 0000, 0, 0000, 0, 0050, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (10329, 10313, 0038, 'StarArrow', 1, 0, 0, 0000, 0003, 0000, 30113, 0000, 0090, 0000, 0014, 0018, 0001, 0000, 850000, 0090, 0, 0500, 0000, 0, 0000, 0, 0000, 0, 0050, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (10330, 10313, 0038, 'StarArrow', 1, 0, 0, 0000, 0004, 0000, 30120, 0000, 0090, 0000, 0015, 0020, 0001, 0000, 0, 0110, 0, 0500, 0000, 0, 0000, 0, 0000, 0, 0050, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (10335, 30000, 0006, 'AzureShield', 0, 0, 0, 0001, 0001, 0123, 0, 0000, 0100, 0000, 0000, 0021, 0089, 0000, 1190, 0050, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10336, 30000, 0006, 'AzureShield', 0, 0, 0, 0001, 0002, 0169, 0, 0000, 0100, 0000, 0000, 0022, 0089, 0000, 1442, 0070, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10337, 30000, 0006, 'AzureShield', 0, 0, 0, 0001, 0003, 0280, 0, 0000, 0100, 0000, 0000, 0023, 0089, 0000, 1690, 0090, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10338, 30000, 0006, 'AzureShield', 0, 0, 0, 0001, 0004, 0405, 0, 0000, 0100, 0000, 0000, 0025, 0089, 0000, 0, 0110, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10343, 10309, 0037, 'ChainBolt', 1, 0, 1, 0000, 0001, 0000, 520, 0000, 0100, 0060, 0004, 0012, 0088, 0000, 1071730, 0050, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10344, 10309, 0037, 'ChainBolt', 1, 0, 1, 0000, 0002, 0000, 740, 0000, 0100, 0060, 0004, 0014, 0088, 0000, 2823723, 0070, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10345, 10309, 0037, 'ChainBolt', 1, 0, 1, 0000, 0003, 0000, 1105, 0000, 0100, 0060, 0004, 0015, 0088, 0000, 6715536, 0090, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10346, 10309, 0037, 'ChainBolt', 1, 0, 1, 0000, 0004, 0000, 1625, 0000, 0100, 0060, 0004, 0015, 0088, 0000, 0, 0110, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10351, 10500, 0035, 'Immolation', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0002, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10352, 10505, 0041, 'RevengePunch', 0, 0, 0, 0004, 0000, 0000, 300, 0000, 0100, 0000, 0005, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10353, 10502, 0036, 'BladeStorm', 0, 0, 0, 0004, 0000, 0000, 500, 0000, 0100, 0000, 0000, 0003, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10354, 10506, 0036, 'BladeVortex', 0, 0, 0, 0004, 0000, 0000, 100, 0000, 0100, 0000, 0012, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10355, 10504, 0036, 'SwordBeam', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10356, 30013, 0044, 'ArrowHail', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0012, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10357, 30012, 0035, 'Avalanche', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0003, 0008, 0015, 0056, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10358, 30011, 0034, 'IceShock', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0003, 0006, 0006, 0056, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10359, 30010, 0036, 'IceBolt', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10360, 10360, 0042, 'SurgingShake', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0008, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10361, 10361, 0035, 'BloodyBattle', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0005, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10362, 10362, 0043, 'SoaringLeap', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0005, 0015, 0020, 0055, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10363, 10363, 0036, 'LightningCrash', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10364, 10364, 0001, 'PiercingStrike', 0, 0, 0, 0004, 0000, 0000, 250, 0000, 0100, 0000, 0005, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10365, 30014, 0001, 'FrostClaw', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0005, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10366, 10503, 0036, 'BladeStorm', 0, 0, 0, 0004, 0000, 0000, 500, 0000, 0100, 0000, 0000, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10369, 10369, 0001, 'Slash', 0, 0, 0, 0004, 0000, 0000, 100, 0000, 0100, 0000, 0005, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10370, 10370, 0002, 'Recovery', 0, 0, 0, 0001, 0000, 0000, 100, 0000, 0100, 0000, 0000, 0000, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10371, 30015, 0045, 'ArrowHail', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0012, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10372, 10372, 0002, 'Metaplasia', 0, 0, 0, 0001, 0000, 0000, 250, 0000, 0100, 0000, 0000, 0000, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10373, 10373, 0001, 'IceBurst', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0005, 0015, 0020, 0056, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10374, 10374, 0002, 'Heal', 0, 0, 0, 0001, 0000, 0000, 50, 0000, 0100, 0000, 0000, 0000, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10381, 10381, 0048, 'RadiantPalm', 1, 0, 1, 0000, 0000, 0000, 30100, 0000, 0100, 0000, 0005, 0004, 0055, 0000, 300000, 0040, 0, 610622, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10382, 10381, 0048, 'RadiantPalm', 1, 0, 1, 0000, 0001, 0000, 30105, 0000, 0100, 0000, 0005, 0004, 0055, 0000, 900000, 0050, 0, 610622, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10383, 10381, 0048, 'RadiantPalm', 1, 0, 1, 0000, 0002, 0000, 30110, 0000, 0100, 0000, 0005, 0004, 0055, 0000, 2223000, 0070, 0, 610622, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10384, 10381, 0048, 'RadiantPalm', 1, 0, 1, 0000, 0003, 0000, 30115, 0000, 0100, 0000, 0005, 0004, 0055, 0000, 4320000, 0090, 0, 610622, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10385, 10381, 0048, 'RadiantPalm', 1, 0, 1, 0000, 0004, 0000, 30120, 0000, 0100, 0000, 0005, 0004, 0055, 0000, 0, 0110, 0, 610622, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10390, 10390, 0053, 'Oblivion', 0, 0, 0, 0000, 0000, 0000, 33, 0000, 0100, 0030, 0000, 0000, 0002, 0000, 0, 0015, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10395, 10395, 0051, 'TyrantAura', 0, 0, 1, 0000, 0000, 0050, 2, 0000, 0100, 1000000, 0030, 0000, 0092, 0000, 5400, 0020, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10396, 10395, 0051, 'TyrantAura', 0, 0, 1, 0000, 0001, 0100, 4, 0000, 0100, 1000000, 0030, 0000, 0092, 0000, 12000, 0040, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10397, 10395, 0051, 'TyrantAura', 0, 0, 1, 0000, 0002, 0150, 6, 0000, 0100, 1000000, 0030, 0000, 0092, 0000, 16500, 0060, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10398, 10395, 0051, 'TyrantAura', 0, 0, 1, 0000, 0003, 0200, 8, 0000, 0100, 1000000, 0030, 0000, 0092, 0000, 27000, 0080, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10399, 10395, 0051, 'TyrantAura', 0, 0, 1, 0000, 0004, 0250, 10, 0000, 0100, 1000000, 0030, 0000, 0092, 0000, 0, 0100, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10400, 10400, 0046, 'Serenity', 0, 0, 0, 0000, 0000, 0050, 0, 0000, 0100, 0010, 0000, 0000, 0000, 0000, 0, 0040, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0050, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10405, 10405, 0006, 'SoulShackle', 1, 0, 0, 0064, 0000, 0000, 0, 0000, 0100, 0030, 0000, 0015, 0106, 0000, 720, 0090, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10406, 10405, 0006, 'SoulShackle', 1, 0, 0, 0064, 0001, 0000, 0, 0000, 0100, 0045, 0000, 0015, 0106, 0000, 860, 0100, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10407, 10405, 0006, 'SoulShackle', 1, 0, 0, 0064, 0002, 0000, 0, 0000, 0100, 0060, 0000, 0015, 0106, 0000, 960, 0110, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10408, 10405, 0006, 'SoulShackle', 1, 0, 0, 0064, 0003, 0000, 0, 0000, 0100, 0075, 0000, 0015, 0106, 0000, 1010, 0115, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10409, 10405, 0006, 'SoulShackle', 1, 0, 0, 0064, 0004, 0000, 0, 0000, 0100, 0090, 0000, 0015, 0106, 0000, 0, 0120, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10410, 10410, 0051, 'FendAura', 0, 0, 1, 0000, 0000, 0050, 2, 0000, 0100, 1000000, 0030, 0000, 0094, 0000, 5400, 0020, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10411, 10410, 0051, 'FendAura', 0, 0, 1, 0000, 0001, 0100, 4, 0000, 0100, 1000000, 0030, 0000, 0094, 0000, 12000, 0040, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10412, 10410, 0051, 'FendAura', 0, 0, 1, 0000, 0002, 0150, 6, 0000, 0100, 1000000, 0030, 0000, 0094, 0000, 16500, 0060, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10413, 10410, 0051, 'FendAura', 0, 0, 1, 0000, 0003, 0200, 8, 0000, 0100, 1000000, 0030, 0000, 0094, 0000, 27000, 0080, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10414, 10410, 0051, 'FendAura', 0, 0, 1, 0000, 0004, 0250, 10, 0000, 0100, 1000000, 0030, 0000, 0094, 0000, 0, 0100, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10415, 10415, 0054, 'WhirlwindKick', 1, 0, 1, 0000, 0000, 0000, 30065, 0000, 0100, 0000, 0003, 0000, 0000, 0000, 167600, 0015, 0, 610622, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10416, 10415, 0054, 'WhirlwindKick', 1, 0, 1, 0000, 0001, 0000, 30070, 0000, 0100, 0000, 0003, 0000, 0000, 0000, 590000, 0030, 0, 610622, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10417, 10415, 0054, 'WhirlwindKick', 1, 0, 1, 0000, 0002, 0000, 30075, 0000, 0100, 0000, 0004, 0000, 0000, 0000, 1216800, 0050, 0, 610622, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10418, 10415, 0054, 'WhirlwindKick', 1, 0, 1, 0000, 0003, 0000, 30080, 0000, 0100, 0000, 0004, 0000, 0000, 0000, 2948000, 0070, 0, 610622, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10419, 10415, 0054, 'WhirlwindKick', 1, 0, 1, 0000, 0004, 0000, 30085, 0000, 0100, 0000, 0005, 0000, 0000, 0000, 0, 0090, 0, 610622, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10425, 10425, 0047, 'Tranquility', 0, 0, 0, 0000, 0000, 0100, 0, 0000, 0100, 0002, 0000, 0020, 0000, 0000, 0, 0070, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0025, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10430, 10430, 0050, 'Compassion', 0, 0, 1, 0002, 0000, 0250, 0, 0000, 0100, 0000, 0020, 0020, 0000, 0000, 0, 0100, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0075, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10440, 10420, 0051, 'ElementalAuraMetal', 0, 0, 1, 0000, 0000, 0050, 30, 0000, 0100, 1000000, 0030, 0000, 0096, 0000, 5400, 0100, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10441, 10420, 0051, 'ElementalAuraMetal', 0, 0, 1, 0000, 0001, 0100, 35, 0000, 0100, 1000000, 0030, 0000, 0096, 0000, 12000, 0105, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10442, 10420, 0051, 'ElementalAuraMetal', 0, 0, 1, 0000, 0002, 0150, 40, 0000, 0100, 1000000, 0030, 0000, 0096, 0000, 16500, 0110, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10443, 10420, 0051, 'ElementalAuraMetal', 0, 0, 1, 0000, 0003, 0200, 45, 0000, 0100, 1000000, 0030, 0000, 0096, 0000, 27000, 0115, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10444, 10420, 0051, 'ElementalAuraMetal', 0, 0, 1, 0000, 0004, 0250, 50, 0000, 0100, 1000000, 0030, 0000, 0096, 0000, 0, 0120, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10445, 10421, 0051, 'ElementalAuraWood', 0, 0, 1, 0000, 0000, 0050, 30, 0000, 0100, 1000000, 0030, 0000, 0098, 0000, 5400, 0100, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10446, 10421, 0051, 'ElementalAuraWood', 0, 0, 1, 0000, 0001, 0100, 35, 0000, 0100, 1000000, 0030, 0000, 0098, 0000, 12000, 0105, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10447, 10421, 0051, 'ElementalAuraWood', 0, 0, 1, 0000, 0002, 0150, 40, 0000, 0100, 1000000, 0030, 0000, 0098, 0000, 16500, 0110, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10448, 10421, 0051, 'ElementalAuraWood', 0, 0, 1, 0000, 0003, 0200, 45, 0000, 0100, 1000000, 0030, 0000, 0098, 0000, 27000, 0115, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10449, 10421, 0051, 'ElementalAuraWood', 0, 0, 1, 0000, 0004, 0250, 50, 0000, 0100, 1000000, 0030, 0000, 0098, 0000, 0, 0120, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10450, 10422, 0051, 'ElementalAuraWater', 0, 0, 1, 0000, 0000, 0050, 30, 0000, 0100, 1000000, 0030, 0000, 0100, 0000, 5400, 0100, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10451, 10422, 0051, 'ElementalAuraWater', 0, 0, 1, 0000, 0001, 0100, 35, 0000, 0100, 1000000, 0030, 0000, 0100, 0000, 12000, 0105, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10452, 10422, 0051, 'ElementalAuraWater', 0, 0, 1, 0000, 0002, 0150, 40, 0000, 0100, 1000000, 0030, 0000, 0100, 0000, 16500, 0110, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10453, 10422, 0051, 'ElementalAuraWater', 0, 0, 1, 0000, 0003, 0200, 45, 0000, 0100, 1000000, 0030, 0000, 0100, 0000, 27000, 0115, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10454, 10422, 0051, 'ElementalAuraWater', 0, 0, 1, 0000, 0004, 0250, 50, 0000, 0100, 1000000, 0030, 0000, 0100, 0000, 0, 0120, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10455, 10423, 0051, 'ElementalAuraFire', 0, 0, 1, 0000, 0000, 0050, 30, 0000, 0100, 1000000, 0030, 0000, 0102, 0000, 5400, 0100, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10456, 10423, 0051, 'ElementalAuraFire', 0, 0, 1, 0000, 0001, 0100, 35, 0000, 0100, 1000000, 0030, 0000, 0102, 0000, 12000, 0105, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10457, 10423, 0051, 'ElementalAuraFire', 0, 0, 1, 0000, 0002, 0150, 40, 0000, 0100, 1000000, 0030, 0000, 0102, 0000, 16500, 0110, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10458, 10423, 0051, 'ElementalAuraFire', 0, 0, 1, 0000, 0003, 0200, 45, 0000, 0100, 1000000, 0030, 0000, 0102, 0000, 27000, 0115, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10459, 10423, 0051, 'ElementalAuraFire', 0, 0, 1, 0000, 0004, 0250, 50, 0000, 0100, 1000000, 0030, 0000, 0102, 0000, 0, 0120, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10460, 10424, 0051, 'ElementalAuraEarth', 0, 0, 1, 0000, 0000, 0050, 30, 0000, 0100, 1000000, 0030, 0000, 0104, 0000, 5400, 0100, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10461, 10424, 0051, 'ElementalAuraEarth', 0, 0, 1, 0000, 0001, 0100, 35, 0000, 0100, 1000000, 0030, 0000, 0104, 0000, 12000, 0105, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10462, 10424, 0051, 'ElementalAuraEarth', 0, 0, 1, 0000, 0002, 0150, 40, 0000, 0100, 1000000, 0030, 0000, 0104, 0000, 16500, 0110, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10463, 10424, 0051, 'ElementalAuraEarth', 0, 0, 1, 0000, 0003, 0200, 45, 0000, 0100, 1000000, 0030, 0000, 0104, 0000, 27000, 0115, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10464, 10424, 0051, 'ElementalAuraEarth', 0, 0, 1, 0000, 0004, 0250, 50, 0000, 0100, 1000000, 0030, 0000, 0104, 0000, 0, 0120, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10470, 10470, 0052, 'ShieldBlock', 0, 0, 0, 0000, 0000, 0000, 10, 0000, 0100, 0030, 0000, 0000, 0055, 0000, 5400, 0040, 0, 0900, 0000, 0, 0000, 0, 0000, 0, 0050, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10471, 10470, 0052, 'ShieldBlock', 0, 0, 0, 0000, 0001, 0000, 15, 0000, 0100, 0030, 0000, 0000, 0055, 0000, 12000, 0050, 0, 0900, 0000, 0, 0000, 0, 0000, 0, 0050, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10472, 10470, 0052, 'ShieldBlock', 0, 0, 0, 0000, 0002, 0000, 20, 0000, 0100, 0040, 0000, 0000, 0055, 0000, 16500, 0070, 0, 0900, 0000, 0, 0000, 0, 0000, 0, 0050, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10473, 10470, 0052, 'ShieldBlock', 0, 0, 0, 0000, 0003, 0000, 25, 0000, 0100, 0050, 0000, 0000, 0055, 0000, 27000, 0090, 0, 0900, 0000, 0, 0000, 0, 0000, 0, 0050, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10474, 10470, 0052, 'ShieldBlock', 0, 0, 0, 0000, 0004, 0000, 30, 0000, 0100, 0060, 0000, 0000, 0055, 0000, 0, 0110, 0, 0900, 0000, 0, 0000, 0, 0000, 0, 0050, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10484, 10484, 0001, 'DeathBlow', 0, 0, 0, 0004, 0000, 0000, 500, 0000, 0100, 0002, 0005, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10485, 10485, 0019, 'Snakeman', 0, 0, 0, 0002, 0000, 0000, 13, 0000, 0100, 0600, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10486, 10486, 0019, 'HeresySnakeman', 0, 0, 0, 0002, 0000, 0000, 4185, 0000, 0100, 0600, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10487, 10487, 0019, 'Macaque', 0, 0, 0, 0002, 0000, 0000, 30, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10488, 10488, 0019, 'Macaque', 0, 0, 0, 0002, 0000, 0000, 30, 0000, 0100, 0010, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10490, 10490, 0055, 'TripleAttack', 0, 0, 1, 0008, 0000, 0000, 0, 0000, 0040, 1000000, 0030, 0000, 0000, 0000, 5400, 0005, 0, 610622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10491, 10490, 0055, 'TripleAttack', 0, 0, 1, 0008, 0001, 0000, 0, 0000, 0044, 1000000, 0030, 0000, 0000, 0000, 12000, 0040, 0, 610622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10492, 10490, 0055, 'TripleAttack', 0, 0, 1, 0008, 0002, 0000, 0, 0000, 0048, 1000000, 0030, 0000, 0000, 0000, 16500, 0050, 0, 610622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10493, 10490, 0055, 'TripleAttack', 0, 0, 1, 0008, 0003, 0000, 0, 0000, 0052, 1000000, 0030, 0000, 0000, 0000, 27000, 0060, 0, 610622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10494, 10490, 0055, 'TripleAttack', 0, 0, 1, 0008, 0004, 0000, 0, 0000, 0055, 1000000, 0030, 0000, 0000, 0000, 0, 0070, 0, 610622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10501, 10501, 0001, 'BladeGhostMessenger', 1, 0, 0, 0000, 0000, 0000, 0, 0000, 0100, 0150, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10508, 10508, 0041, 'FatalCrash', 0, 0, 0, 0004, 0000, 0000, 150, 0000, 0100, 0000, 0005, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10509, 10509, 0036, 'IcyTouch', 0, 0, 0, 0004, 0000, 0000, 50, 0000, 0100, 0000, 0012, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10510, 10510, 0042, 'Fumigate', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0005, 0015, 0020, 0055, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10511, 10511, 0001, 'RosySpell', 0, 0, 0, 0004, 0000, 0000, 125, 0000, 0100, 0000, 0005, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10512, 10512, 0043, 'RomanticStrike', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0005, 0015, 0020, 0056, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10513, 10513, 0002, 'LoverHug', 0, 0, 0, 0001, 0000, 0000, 50, 0000, 0100, 0000, 0000, 0000, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10514, 10514, 0019, 'Pheasant', 0, 0, 0, 0002, 0000, 0000, 2100, 0000, 0100, 0010, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10522, 10522, 0019, 'DivineHare', 0, 0, 0, 0002, 0000, 0000, 3081, 0000, 0100, 0060, 0000, 0005, 0000, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10523, 10523, 0019, 'GoldenHare', 0, 0, 0, 0002, 0000, 0000, 3082, 0000, 0100, 0060, 0000, 0005, 0000, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10524, 10524, 0019, 'SilverHare', 0, 0, 0, 0002, 0000, 0000, 3083, 0000, 0100, 0060, 0000, 0005, 0000, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10525, 10525, 0019, 'HareGuard', 0, 0, 0, 0002, 0000, 0000, 3084, 0000, 0100, 0300, 0000, 0005, 0000, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10526, 10526, 0019, 'GoldRabbit', 0, 0, 0, 0002, 0000, 0000, 3081, 0000, 0100, 0030, 0000, 0005, 0000, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10527, 10527, 0019, 'SilverRabbit', 0, 0, 0, 0002, 0000, 0000, 3082, 0000, 0100, 0300, 0000, 0005, 0000, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10528, 10528, 0019, 'Pheasant', 0, 0, 0, 0002, 0000, 0000, 2100, 0000, 0100, 0300, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10529, 10529, 0034, 'Freeze', 0, 0, 0, 0001, 0000, 0000, 0, 0000, 0100, 0003, 0006, 0012, 0056, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10530, 10530, 0034, 'Frenzy', 0, 0, 0, 0001, 0000, 0000, 0, 0000, 0100, 0003, 0006, 0012, 0051, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (10531, 10531, 0034, 'Stunt', 0, 0, 0, 0001, 0000, 0000, 0, 0000, 0100, 0003, 0006, 0012, 0055, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11000, 11000, 0014, 'DragonTail', 1, 1, 0, 0004, 0000, 0000, 30095, 0000, 0100, 0000, 0004, 0005, 0001, 0000, 100000, 0040, 2, 0561, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11001, 11000, 0014, 'DragonTail', 1, 1, 0, 0004, 0001, 0000, 30100, 0000, 0100, 0000, 0005, 0006, 0001, 0000, 300000, 0050, 2, 0561, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11002, 11000, 0014, 'DragonTail', 1, 1, 0, 0004, 0002, 0000, 30105, 0000, 0100, 0000, 0006, 0007, 0001, 0000, 741000, 0060, 2, 0561, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11003, 11000, 0014, 'DragonTail', 1, 1, 0, 0004, 0003, 0000, 30110, 0000, 0100, 0000, 0007, 0008, 0001, 0000, 1440000, 0070, 2, 0561, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11004, 11000, 0014, 'DragonTail', 1, 1, 0, 0004, 0004, 0000, 30115, 0000, 0100, 0000, 0008, 0009, 0001, 0000, 0, 0000, 2, 0561, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11005, 11005, 0014, 'ViperFang', 1, 1, 0, 0004, 0000, 0000, 30095, 0000, 0100, 0000, 0004, 0005, 0001, 0000, 100000, 0040, 2, 0560, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11006, 11005, 0014, 'ViperFang', 1, 1, 0, 0004, 0001, 0000, 30100, 0000, 0100, 0000, 0005, 0006, 0001, 0000, 300000, 0050, 2, 0560, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11007, 11005, 0014, 'ViperFang', 1, 1, 0, 0004, 0002, 0000, 30105, 0000, 0100, 0000, 0006, 0007, 0001, 0000, 741000, 0060, 2, 0560, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11008, 11005, 0014, 'ViperFang', 1, 1, 0, 0004, 0003, 0000, 30110, 0000, 0100, 0000, 0007, 0008, 0001, 0000, 1440000, 0070, 2, 0560, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11009, 11005, 0014, 'ViperFang', 1, 1, 0, 0004, 0004, 0000, 30115, 0000, 0100, 0000, 0008, 0009, 0001, 0000, 0, 0000, 2, 0560, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11010, 11010, 0019, 'WingedSnake1', 0, 0, 0, 0002, 0000, 0000, 6, 0000, 0100, 0900, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11011, 11011, 0019, 'Bandit1', 0, 0, 0, 0002, 0000, 0000, 7, 0000, 0100, 0900, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11012, 11012, 0019, 'Ratling1', 0, 0, 0, 0002, 0000, 0000, 8, 0000, 0100, 0900, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11013, 11013, 0019, 'Macaque1', 0, 0, 0, 0002, 0000, 0000, 2611, 0000, 0100, 0900, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11014, 11014, 0019, 'GiantApe1', 0, 0, 0, 0002, 0000, 0000, 2612, 0000, 0100, 0900, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11015, 11015, 0019, 'Snakeman1', 0, 0, 0, 0002, 0000, 0000, 13, 0000, 0100, 0900, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11016, 11016, 0019, 'SandMonster1', 0, 0, 0, 0002, 0000, 0000, 2613, 0000, 0100, 0900, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11017, 11017, 0019, 'Rockmonster1', 0, 0, 0, 0002, 0000, 0000, 2614, 0000, 0100, 0900, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11018, 11018, 0019, 'BladeGhost1', 0, 0, 0, 0002, 0000, 0000, 17, 0000, 0100, 0900, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11019, 11019, 0019, 'HareGuard', 0, 0, 0, 0002, 0000, 0000, 3084, 0000, 0100, 0180, 0000, 0005, 0000, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11020, 11020, 0019, 'PumpkinSpell', 0, 0, 0, 0000, 0000, 0000, 3058, 0000, 0000, 1800, 0000, 0000, 0000, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11021, 11021, 0019, 'PirateOctopus', 0, 0, 0, 0002, 0000, 0000, 2659, 0000, 0100, 1800, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11022, 11022, 0001, 'DoomSlash', 0, 0, 0, 0004, 0000, 0000, 400, 0000, 0100, 0000, 0005, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11024, 11024, 0036, 'HeavenSlash', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0006, 0006, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11025, 11025, 0041, 'EarthSlash', 0, 0, 0, 0004, 0000, 0000, 1000, 0000, 0100, 0000, 0005, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11026, 11026, 0034, 'FrostBurst', 0, 0, 0, 0004, 0000, 0000, 300, 0000, 0100, 0000, 0006, 0006, 0056, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11027, 11027, 0002, 'BreathRecovery', 0, 0, 0, 0001, 0000, 0000, 100, 0000, 0100, 0000, 0000, 0000, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11028, 11028, 0035, 'MagicStones', 0, 0, 0, 0004, 0000, 0000, 700, 0000, 0100, 0000, 0008, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11030, 11030, 0001, 'EagleEye', 1, 0, 0, 0000, 0000, 0000, 30028, 0000, 0100, 0000, 0000, 0015, 0000, 0000, 300000, 0040, 0, 0612, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11031, 11030, 0001, 'EagleEye', 1, 0, 0, 0000, 0001, 0000, 30030, 0000, 0100, 0000, 0000, 0015, 0000, 0000, 900000, 0050, 0, 0612, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11032, 11030, 0001, 'EagleEye', 1, 0, 0, 0000, 0002, 0000, 30032, 0000, 0100, 0000, 0000, 0015, 0000, 0000, 4446000, 0070, 0, 0612, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11033, 11030, 0001, 'EagleEye', 1, 0, 0, 0000, 0003, 0000, 30034, 0000, 0100, 0000, 0000, 0015, 0000, 0000, 12960000, 0090, 0, 0612, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11034, 11030, 0001, 'EagleEye', 1, 0, 0, 0000, 0004, 0000, 30036, 0000, 0100, 0000, 0000, 0015, 0000, 0000, 0, 0110, 0, 0612, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11040, 11040, 0056, 'ScurvyBomb', 1, 1, 1, 0004, 0000, 0000, 30010, 0000, 0100, 0010, 0004, 0010, 0118, 0000, 5400, 0040, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11041, 11040, 0056, 'ScurvyBomb', 1, 1, 1, 0004, 0001, 0000, 30012, 0000, 0100, 0010, 0004, 0010, 0118, 0000, 12000, 0050, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11042, 11040, 0056, 'ScurvyBomb', 1, 1, 1, 0004, 0002, 0000, 30014, 0000, 0100, 0012, 0004, 0010, 0118, 0000, 16500, 0070, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11043, 11040, 0056, 'ScurvyBomb', 1, 1, 1, 0004, 0003, 0000, 30017, 0000, 0100, 0012, 0004, 0010, 0118, 0000, 27000, 0090, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11044, 11040, 0056, 'ScurvyBomb', 1, 1, 1, 0004, 0004, 0000, 30020, 0000, 0100, 0015, 0004, 0010, 0118, 0000, 0, 0110, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11050, 11050, 0057, 'CannonBarrage', 1, 1, 1, 0004, 0000, 0000, 30500, 0000, 0100, 0060, 0005, 0010, 0119, 0000, 0, 0015, 1, 0611, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11060, 11060, 0063, 'Blackbeard`sRage', 1, 0, 1, 0000, 0000, 0000, 30025, 0000, 0100, 0060, 0005, 0012, 0120, 0000, 628464, 0040, 1, 0612, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11061, 11060, 0063, 'Blackbeard`sRage', 1, 0, 1, 0000, 0001, 0000, 30030, 0000, 0100, 0060, 0005, 0012, 0120, 0000, 1071730, 0050, 1, 0612, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11062, 11060, 0063, 'Blackbeard`sRage', 1, 0, 1, 0000, 0002, 0000, 30030, 0000, 0100, 0060, 0005, 0012, 0120, 0000, 2823723, 0070, 1, 0612, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11063, 11060, 0063, 'Blackbeard`sRage', 1, 0, 1, 0000, 0003, 0000, 30035, 0000, 0100, 0060, 0005, 0012, 0120, 0000, 6715536, 0090, 1, 0612, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11064, 11060, 0063, 'Blackbeard`sRage', 1, 0, 1, 0000, 0004, 0000, 30035, 0000, 0100, 0060, 0005, 0012, 0120, 0000, 0, 0110, 1, 0612, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11070, 11070, 0060, 'GaleBomb', 1, 1, 1, 0004, 0000, 0000, 30020, 0000, 0100, 0000, 0005, 0010, 0000, 0000, 5400, 0020, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11071, 11070, 0060, 'GaleBomb', 1, 1, 1, 0004, 0001, 0000, 30022, 0000, 0100, 0000, 0005, 0010, 0000, 0000, 12000, 0040, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11072, 11070, 0060, 'GaleBomb', 1, 1, 1, 0004, 0002, 0000, 30024, 0000, 0100, 0000, 0005, 0010, 0000, 0000, 16500, 0060, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11073, 11070, 0060, 'GaleBomb', 1, 1, 1, 0004, 0003, 0000, 30027, 0000, 0100, 0000, 0005, 0010, 0000, 0000, 27000, 0080, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11074, 11070, 0060, 'GaleBomb', 1, 1, 1, 0004, 0004, 0000, 30030, 0000, 0100, 0000, 0005, 0010, 0000, 0000, 0, 0100, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11083, 11083, 0036, 'SoulIceSpell', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0020, 0056, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11084, 11084, 0001, 'SoulIcyClaw', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0005, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11085, 11085, 0042, 'SoulIcyStun', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0005, 0015, 0020, 0051, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11086, 11086, 0034, 'SoulIcyPunch', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0003, 0008, 0008, 0056, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11087, 30015, 0045, 'SoulIcyArrow', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0005, 0012, 0020, 0050, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11088, 11088, 0002, 'SoulTransform', 0, 0, 0, 0001, 0000, 0000, 250, 0000, 0100, 0000, 0000, 0000, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11089, 11089, 0035, 'SoulFreezingCall', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0003, 0008, 0015, 0056, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11100, 11100, 0062, 'Kraken`sRevenge', 1, 0, 1, 0001, 0000, 0000, 0, 0000, 0100, 0008, 0005, 0015, 0001, 0000, 5400, 0070, 0, 0612, 0000, 0, 0000, 0, 0000, 0, 0035, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11101, 11100, 0062, 'Kraken`sRevenge', 1, 0, 1, 0001, 0001, 0000, 0, 0000, 0100, 0010, 0005, 0015, 0001, 0000, 12000, 0080, 0, 0612, 0000, 0, 0000, 0, 0000, 0, 0035, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11102, 11100, 0062, 'Kraken`sRevenge', 1, 0, 1, 0001, 0002, 0000, 0, 0000, 0100, 0010, 0005, 0015, 0001, 0000, 16500, 0090, 0, 0612, 0000, 0, 0000, 0, 0000, 0, 0035, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11103, 11100, 0062, 'Kraken`sRevenge', 1, 0, 1, 0001, 0003, 0000, 0, 0000, 0100, 0012, 0005, 0015, 0001, 0000, 27000, 0095, 0, 0612, 0000, 0, 0000, 0, 0000, 0, 0035, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11104, 11100, 0062, 'Kraken`sRevenge', 1, 0, 1, 0001, 0004, 0000, 0, 0000, 0100, 0012, 0005, 0015, 0001, 0000, 0, 0100, 0, 0612, 0000, 0, 0000, 0, 0000, 0, 0035, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11110, 11110, 0061, 'BladeTempest', 1, 1, 1, 0004, 0000, 0000, 30090, 0000, 0100, 0008, 0001, 0020, 0001, 0000, 167600, 0015, 0, 0611, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11111, 11110, 0061, 'BladeTempest', 1, 1, 1, 0004, 0001, 0000, 30095, 0000, 0100, 0010, 0001, 0020, 0001, 0000, 590000, 0030, 0, 0611, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11112, 11110, 0061, 'BladeTempest', 1, 1, 1, 0004, 0002, 0000, 30100, 0000, 0100, 0010, 0001, 0020, 0001, 0000, 1216800, 0050, 0, 0611, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11113, 11110, 0061, 'BladeTempest', 1, 1, 1, 0004, 0003, 0000, 30105, 0000, 0100, 0012, 0001, 0020, 0001, 0000, 2948000, 0070, 0, 0611, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11114, 11110, 0061, 'BladeTempest', 1, 1, 1, 0004, 0004, 0000, 30110, 0000, 0100, 0012, 0001, 0020, 0001, 0000, 0, 0090, 0, 0611, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11120, 11120, 0058, 'BlackSpot', 0, 0, 0, 0000, 0000, 0000, 0, 0000, 0025, 0008, 0000, 0000, 0000, 0000, 5400, 0040, 0, 0612, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11121, 11120, 0058, 'BlackSpot', 0, 0, 0, 0000, 0001, 0000, 0, 0000, 0027, 0010, 0000, 0000, 0000, 0000, 12000, 0060, 0, 0612, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11122, 11120, 0058, 'BlackSpot', 0, 0, 0, 0000, 0002, 0000, 0, 0000, 0029, 0010, 0000, 0000, 0000, 0000, 16500, 0080, 0, 0612, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11123, 11120, 0058, 'BlackSpot', 0, 0, 0, 0000, 0003, 0000, 0, 0000, 0032, 0012, 0000, 0000, 0000, 0000, 27000, 0100, 0, 0612, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11124, 11120, 0058, 'BlackSpot', 0, 0, 0, 0000, 0004, 0000, 0, 0000, 0035, 0012, 0000, 0000, 0000, 0000, 0, 0110, 0, 0612, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11130, 11130, 0059, 'AdrenalineRush', 0, 0, 0, 0000, 0000, 0000, 0, 0000, 0025, 0000, 0000, 0000, 0000, 0000, 5400, 0040, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11131, 11130, 0059, 'AdrenalineRush', 0, 0, 0, 0000, 0001, 0000, 0, 0000, 0027, 0000, 0000, 0000, 0000, 0000, 12000, 0050, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11132, 11130, 0059, 'AdrenalineRush', 0, 0, 0, 0000, 0002, 0000, 0, 0000, 0029, 0000, 0000, 0000, 0000, 0000, 16500, 0070, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11133, 11130, 0059, 'AdrenalineRush', 0, 0, 0, 0000, 0003, 0000, 0, 0000, 0032, 0000, 0000, 0000, 0000, 0000, 27000, 0090, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11134, 11130, 0059, 'AdrenalineRush', 0, 0, 0, 0000, 0004, 0000, 0, 0000, 0035, 0000, 0000, 0000, 0000, 0000, 0, 0110, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11140, 11140, 0055, 'Windstorm', 0, 0, 1, 0008, 0000, 0000, 0, 0000, 0045, 1000000, 0030, 0000, 0000, 0000, 5400, 0005, 0, 0611, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11141, 11140, 0055, 'Windstorm', 0, 0, 1, 0008, 0001, 0000, 0, 0000, 0048, 1000000, 0030, 0000, 0000, 0000, 12000, 0040, 0, 0611, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11142, 11140, 0055, 'Windstorm', 0, 0, 1, 0008, 0002, 0000, 0, 0000, 0052, 1000000, 0030, 0000, 0000, 0000, 16500, 0050, 0, 0611, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11143, 11140, 0055, 'Windstorm', 0, 0, 1, 0008, 0003, 0000, 0, 0000, 0057, 1000000, 0030, 0000, 0000, 0000, 27000, 0060, 0, 0611, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11144, 11140, 0055, 'Windstorm', 0, 0, 1, 0008, 0004, 0000, 0, 0000, 0060, 1000000, 0030, 0000, 0000, 0000, 0, 0070, 0, 0611, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11154, 11154, 0019, 'Deer', 0, 0, 0, 0002, 0000, 0000, 2680, 0000, 0100, 0180, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11155, 11155, 0019, 'UndeadSoldier', 0, 0, 0, 0002, 0000, 0000, 5045, 0000, 0100, 0060, 0000, 0005, 0000, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11156, 11156, 0019, 'Phantom', 0, 0, 0, 0002, 0000, 0000, 5046, 0000, 0100, 0060, 0000, 0005, 0000, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11157, 11157, 0019, 'Revenant', 0, 0, 0, 0002, 0000, 0000, 5050, 0000, 0100, 0060, 0000, 0005, 0000, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11158, 11210, 0035, 'Immolation', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0002, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11159, 11211, 0036, 'BladeStorm', 0, 0, 0, 0004, 0000, 0000, 250, 0000, 0100, 0000, 0000, 0003, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11160, 11160, 0006, 'DefensiveStance', 0, 0, 0, 0002, 0000, 0000, 30200, 0000, 0100, 0120, 0000, 0000, 0125, 0000, 27000, 0070, 0, 0900, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11161, 11160, 0006, 'DefensiveStance', 0, 0, 0, 0002, 0001, 0000, 30300, 0000, 0100, 0240, 0000, 0000, 0125, 0000, 0, 0100, 0, 0900, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11170, 11170, 0005, 'BloodyScythe', 1, 1, 0, 0000, 0000, 0000, 30080, 0000, 0100, 0000, 0004, 0010, 0001, 0000, 167600, 0040, 0, 511616, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11171, 11170, 0005, 'BloodyScythe', 1, 1, 0, 0000, 0001, 0000, 30085, 0000, 0100, 0000, 0004, 0010, 0001, 0000, 590000, 0050, 0, 511616, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11172, 11170, 0005, 'BloodyScythe', 1, 1, 0, 0000, 0002, 0000, 30090, 0000, 0100, 0000, 0005, 0010, 0001, 0000, 1216800, 0060, 0, 511616, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11173, 11170, 0005, 'BloodyScythe', 1, 1, 0, 0000, 0003, 0000, 30095, 0000, 0100, 0000, 0005, 0010, 0001, 0000, 2948000, 0070, 0, 511616, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11174, 11170, 0005, 'BloodyScythe', 1, 1, 0, 0000, 0004, 0000, 30105, 0000, 0100, 0000, 0006, 0010, 0001, 0000, 0, 0100, 0, 511616, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11180, 11180, 0065, 'MortalDrag', 1, 0, 0, 0000, 0000, 0000, 30012, 0000, 0100, 0000, 0000, 0006, 0001, 0000, 5400, 0020, 0, 511616, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11181, 11180, 0065, 'MortalDrag', 1, 0, 0, 0000, 0001, 0000, 30015, 0000, 0100, 0000, 0000, 0006, 0001, 0000, 12000, 0040, 0, 511616, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11182, 11180, 0065, 'MortalDrag', 1, 0, 0, 0000, 0002, 0000, 30018, 0000, 0100, 0000, 0000, 0006, 0001, 0000, 16500, 0060, 0, 511616, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11183, 11180, 0065, 'MortalDrag', 1, 0, 0, 0000, 0003, 0000, 30022, 0000, 0100, 0000, 0000, 0006, 0001, 0000, 27000, 0080, 0, 511616, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11184, 11180, 0065, 'MortalDrag', 1, 0, 0, 0000, 0004, 0000, 30025, 0000, 0100, 0000, 0000, 0006, 0001, 0000, 0, 0100, 0, 511616, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11190, 11190, 0064, 'ChargingVortex', 1, 0, 1, 0004, 0000, 0000, 30070, 0000, 0100, 0000, 0003, 0020, 0001, 0000, 191500, 0030, 0, 50000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11191, 11190, 0064, 'ChargingVortex', 1, 0, 1, 0004, 0001, 0000, 30075, 0000, 0100, 0000, 0003, 0020, 0001, 0000, 758600, 0050, 0, 50000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11192, 11190, 0064, 'ChargingVortex', 1, 0, 1, 0004, 0002, 0000, 30080, 0000, 0100, 0000, 0003, 0020, 0001, 0000, 1564500, 0070, 0, 50000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11193, 11190, 0064, 'ChargingVortex', 1, 0, 1, 0004, 0003, 0000, 30085, 0000, 0100, 0000, 0003, 0020, 0001, 0000, 3685000, 0090, 0, 50000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11194, 11190, 0064, 'ChargingVortex', 1, 0, 1, 0004, 0004, 0000, 30090, 0000, 0100, 0000, 0003, 0020, 0001, 0000, 0, 0110, 0, 50000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11200, 11200, 0051, 'MagicDefender', 0, 0, 1, 0000, 0000, 0000, 0, 0000, 0100, 0022, 0004, 0000, 0126, 0000, 950, 0040, 0, 0900, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11201, 11200, 0051, 'MagicDefender', 0, 0, 1, 0000, 0001, 0000, 0, 0000, 0100, 0024, 0004, 0000, 0126, 0000, 1190, 0050, 0, 0900, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11202, 11200, 0051, 'MagicDefender', 0, 0, 1, 0000, 0002, 0000, 0, 0000, 0100, 0026, 0004, 0000, 0126, 0000, 1442, 0070, 0, 0900, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11203, 11200, 0051, 'MagicDefender', 0, 0, 1, 0000, 0003, 0000, 0, 0000, 0100, 0028, 0004, 0000, 0126, 0000, 1690, 0090, 0, 0900, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11204, 11200, 0051, 'MagicDefender', 0, 0, 1, 0000, 0004, 0000, 0, 0000, 0100, 0030, 0004, 0000, 0126, 0000, 0, 0110, 0, 0900, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11212, 11212, 0036, 'BladeStorm', 0, 0, 0, 0004, 0000, 0000, 250, 0000, 0100, 0000, 0000, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11213, 11213, 0036, 'SwordBeam', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11214, 11214, 0041, 'RevengePunch', 0, 0, 0, 0004, 0000, 0000, 150, 0000, 0100, 0000, 0005, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11215, 11215, 0036, 'BladeVortex', 0, 0, 0, 0004, 0000, 0000, 50, 0000, 0100, 0000, 0012, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11216, 11216, 0001, 'Slash', 0, 0, 0, 0004, 0000, 0000, 50, 0000, 0100, 0000, 0005, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11217, 11217, 0002, 'Heal', 0, 0, 0, 0001, 0000, 0000, 25, 0000, 0100, 0000, 0000, 0000, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11230, 11230, 0026, 'GapingWounds', 0, 0, 0, 0008, 0000, 0000, 30020, 0000, 0030, 0000, 0000, 0000, 0000, 0000, 5400, 0001, 0, 601511616, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11231, 11230, 0026, 'GapingWounds', 0, 0, 0, 0008, 0001, 0000, 30025, 0000, 0030, 0000, 0000, 0000, 0000, 0000, 12000, 0050, 0, 601511616, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11232, 11230, 0026, 'GapingWounds', 0, 0, 0, 0008, 0002, 0000, 30030, 0000, 0030, 0000, 0000, 0000, 0000, 0000, 16500, 0070, 0, 601511616, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11233, 11230, 0026, 'GapingWounds', 0, 0, 0, 0008, 0003, 0000, 30035, 0000, 0030, 0000, 0000, 0000, 0000, 0000, 27000, 0090, 0, 601511616, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11234, 11230, 0026, 'GapingWounds', 0, 0, 0, 0008, 0004, 0000, 30040, 0000, 0030, 0000, 0000, 0000, 0000, 0000, 0, 0110, 0, 601511616, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11304, 11304, 0019, 'Pheasant', 0, 0, 0, 0002, 0000, 0000, 2100, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11305, 11305, 0019, 'Apparition', 0, 0, 0, 0002, 0000, 0000, 2102, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11306, 11306, 0019, 'Ratling', 0, 0, 0, 0002, 0000, 0000, 8, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11307, 11307, 0019, 'WingedSnake', 0, 0, 0, 0002, 0000, 0000, 6, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11308, 11308, 0019, 'Minotaur', 0, 0, 0, 0002, 0000, 0000, 92, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11309, 11309, 0042, 'SurgingShake', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0008, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11310, 11310, 0035, 'BloodyBattle', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0005, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11311, 11311, 0036, 'LightningCrash', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11312, 11312, 0036, 'LightningCrash', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11313, 11313, 0001, 'PiercingStrike', 0, 0, 0, 0004, 0000, 0000, 500, 0000, 0100, 0000, 0005, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11314, 11314, 0002, 'Recovery', 0, 0, 0, 0001, 0000, 0000, 150, 0000, 0100, 0000, 0000, 0000, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11315, 11315, 0042, 'SurgingShake', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0008, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11316, 11316, 0035, 'BloodyBattle', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0005, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11317, 11317, 0036, 'LightningCrash', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11318, 11318, 0001, 'PiercingStrike', 0, 0, 0, 0004, 0000, 0000, 1200, 0000, 0100, 0000, 0005, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11319, 11319, 0002, 'Recovery', 0, 0, 0, 0001, 0000, 0000, 800, 0000, 0100, 0000, 0000, 0000, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11320, 11320, 0002, 'Recovery', 0, 0, 0, 0001, 0000, 0000, 50, 0000, 0100, 0000, 0000, 0000, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11321, 11321, 0043, 'SoaringLeap', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0005, 0015, 0020, 0055, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11322, 11322, 0042, 'SurgingShake', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0008, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11324, 11324, 0019, 'FruitMonster', 0, 0, 0, 0002, 0000, 0000, 32, 0000, 0100, 0100, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11520, 11520, 0019, 'Fox', 0, 0, 0, 0002, 0000, 0000, 7026, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11590, 11590, 0067, 'KineticSpark', 0, 0, 0, 0002, 0000, 0000, 0, 0000, 0022, 1000000, 0005, 0012, 0147, 0000, 37056, 0050, 0, 0613, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11591, 11590, 0067, 'KineticSpark', 0, 0, 0, 0002, 0001, 0000, 0, 0000, 0024, 1000000, 0005, 0012, 0147, 0000, 66011, 0060, 0, 0613, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11592, 11590, 0067, 'KineticSpark', 0, 0, 0, 0002, 0002, 0000, 0, 0000, 0026, 1000000, 0005, 0012, 0147, 0000, 116140, 0070, 0, 0613, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11593, 11590, 0067, 'KineticSpark', 0, 0, 0, 0002, 0003, 0000, 0, 0000, 0028, 1000000, 0005, 0012, 0147, 0000, 192800, 0080, 0, 0613, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11594, 11590, 0067, 'KineticSpark', 0, 0, 0, 0002, 0004, 0000, 0, 0000, 0030, 1000000, 0005, 0012, 0147, 0000, 0, 0000, 0, 0613, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11600, 11600, 0021, 'DaggerStorm', 1, 1, 0, 0000, 0000, 0000, 30140, 0000, 0100, 0000, 0004, 0010, 0000, 0000, 900000, 0100, 0, 0613, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11601, 11600, 0021, 'DaggerStorm', 1, 1, 0, 0000, 0001, 0000, 30150, 0000, 0100, 0000, 0004, 0010, 0000, 0000, 3500000, 0105, 0, 0613, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11602, 11600, 0021, 'DaggerStorm', 1, 1, 0, 0000, 0002, 0000, 30160, 0000, 0100, 0000, 0004, 0010, 0000, 0000, 8000000, 0110, 0, 0613, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11603, 11600, 0021, 'DaggerStorm', 1, 1, 0, 0000, 0003, 0000, 30170, 0000, 0100, 0000, 0004, 0010, 0000, 0000, 12000000, 0115, 0, 0613, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11604, 11600, 0021, 'DaggerStorm', 1, 1, 0, 0000, 0004, 0000, 30180, 0000, 0100, 0000, 0004, 0010, 0000, 0000, 0, 0000, 0, 0613, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11610, 11610, 0068, 'BladeFlurry', 1, 1, 1, 0000, 0000, 0000, 30300, 0000, 0100, 0045, 0012, 0020, 0146, 0000, 0, 0040, 1, 0613, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11620, 11620, 0006, 'PathOfShadow', 0, 0, 0, 0002, 0000, 0000, 0, 0000, 0100, 0000, 0000, 0000, 0145, 0000, 0, 0040, 0, 0613, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11630, 11630, 0019, 'BanditZero', 0, 0, 0, 0002, 0000, 0000, 7, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11631, 11631, 0019, 'Snakeman', 0, 0, 0, 0002, 0000, 0000, 13, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11650, 11650, 0069, 'BlisteringWave', 1, 1, 0, 0004, 0000, 0000, 30280, 0000, 0100, 0000, 0012, 0015, 0001, 0000, 900000, 0070, 2, 0613, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11651, 11650, 0069, 'BlisteringWave', 1, 1, 0, 0004, 0001, 0000, 30300, 0000, 0100, 0000, 0012, 0015, 0001, 0000, 3500000, 0080, 2, 0613, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11652, 11650, 0069, 'BlisteringWave', 1, 1, 0, 0004, 0002, 0000, 30310, 0000, 0100, 0000, 0012, 0015, 0001, 0000, 8000000, 0090, 2, 0613, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11653, 11650, 0069, 'BlisteringWave', 1, 1, 0, 0004, 0003, 0000, 30330, 0000, 0100, 0000, 0012, 0015, 0001, 0000, 12000000, 0100, 2, 0613, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11654, 11650, 0069, 'BlisteringWave', 1, 1, 0, 0004, 0004, 0000, 30350, 0000, 0100, 0000, 0012, 0015, 0001, 0000, 0, 0000, 2, 0613, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11660, 11660, 0001, 'MortalWound', 1, 0, 0, 0000, 0000, 0000, 30400, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 900000, 0040, 2, 0613, 0000, 0, 0000, 0, 0000, 0, 0015, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11661, 11660, 0001, 'MortalWound', 1, 0, 0, 0000, 0001, 0000, 30430, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 3500000, 0060, 2, 0613, 0000, 0, 0000, 0, 0000, 0, 0015, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11662, 11660, 0001, 'MortalWound', 1, 0, 0, 0000, 0002, 0000, 30450, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 8000000, 0080, 2, 0613, 0000, 0, 0000, 0, 0000, 0, 0015, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11663, 11660, 0001, 'MortalWound', 1, 0, 0, 0000, 0003, 0000, 30480, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 12000000, 0100, 2, 0613, 0000, 0, 0000, 0, 0000, 0, 0015, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11664, 11660, 0001, 'MortalWound', 1, 0, 0, 0000, 0004, 0000, 30500, 0000, 0100, 0000, 0000, 0015, 0001, 0000, 0, 0000, 2, 0613, 0000, 0, 0000, 0, 0000, 0, 0015, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11670, 11670, 0006, 'SpiritFocus', 0, 0, 0, 0002, 0000, 0000, 30250, 0000, 0100, 0060, 0000, 0015, 0023, 0000, 37056, 0090, 2, 0613, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11671, 11670, 0006, 'SpiritFocus', 0, 0, 0, 0002, 0001, 0000, 30260, 0000, 0100, 0080, 0000, 0015, 0023, 0000, 66011, 0095, 2, 0613, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11672, 11670, 0006, 'SpiritFocus', 0, 0, 0, 0002, 0002, 0000, 30280, 0000, 0100, 0100, 0000, 0015, 0023, 0000, 116140, 0100, 2, 0613, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11673, 11670, 0006, 'SpiritFocus', 0, 0, 0, 0002, 0003, 0000, 30300, 0000, 0100, 0120, 0000, 0015, 0023, 0000, 192800, 0110, 2, 0613, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11674, 11670, 0006, 'SpiritFocus', 0, 0, 0, 0002, 0004, 0000, 30320, 0000, 0100, 0150, 0000, 0015, 0023, 0000, 0, 0000, 2, 0613, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (11682, 11682, 0019, 'LittleBird', 0, 0, 0, 0002, 0000, 0000, 3147, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11683, 11683, 0019, 'LittleRabbit', 0, 0, 0, 0002, 0000, 0000, 3082, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11684, 11684, 0019, 'LittleMonkey', 0, 0, 0, 0002, 0000, 0000, 7224, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11685, 11685, 0001, 'BirdmanMsgr', 1, 0, 0, 0000, 0000, 0000, 0, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11686, 11686, 0019, 'Snakeman', 0, 0, 0, 0002, 0000, 0000, 13, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11687, 11687, 0019, 'BullDevil', 0, 0, 0, 0002, 0000, 0000, 92, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11688, 11688, 0019, 'RoaringApe', 0, 0, 0, 0002, 0000, 0000, 31, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11689, 11689, 0019, 'PinkPiglet30s', 0, 0, 0, 0002, 0000, 0000, 4058, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11690, 11690, 0019, 'KungFuBunny30s', 0, 0, 0, 0002, 0000, 0000, 3320, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11691, 11691, 0019, 'BloodyDevil30s', 0, 0, 0, 0002, 0000, 0000, 88, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11692, 11692, 0019, 'Eidolon30s', 0, 0, 0, 0002, 0000, 0000, 5051, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11693, 11693, 0019, 'UltimatePluto30s', 0, 0, 0, 0002, 0000, 0000, 4030, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11694, 11694, 0019, 'FireRatA30s', 0, 0, 0, 0002, 0000, 0000, 9020, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11695, 11695, 0019, 'Siren30s', 0, 0, 0, 0002, 0000, 0000, 3584, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11696, 11696, 0019, 'Temptress30s', 0, 0, 0, 0002, 0000, 0000, 5053, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11697, 11697, 0019, 'Cateran30s', 0, 0, 0, 0002, 0000, 0000, 3031, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11698, 11698, 0019, 'FurySkeleton30s', 0, 0, 0, 0002, 0000, 0000, 4031, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11699, 11699, 0019, 'MaleGhost130s', 0, 0, 0, 0002, 0000, 0000, 2430, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11700, 11700, 0019, 'FemaleGhost130s', 0, 0, 0, 0002, 0000, 0000, 2431, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11701, 11701, 0019, 'BirdKing130s', 0, 0, 0, 0002, 0000, 0000, 4035, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11704, 11704, 0019, 'Pheasant', 0, 0, 0, 0002, 0000, 0000, 2100, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11705, 11705, 0019, 'Robin', 0, 0, 0, 0002, 0000, 0000, 2101, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11706, 11706, 0019, 'Ratling', 0, 0, 0, 0002, 0000, 0000, 8, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11707, 11707, 0019, 'SandMonster', 0, 0, 0, 0002, 0000, 0000, 7288, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11708, 11708, 0019, 'BladeGhost', 0, 0, 0, 0002, 0000, 0000, 17, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11709, 11709, 0019, 'Birdman', 0, 0, 0, 0002, 0000, 0000, 18, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11710, 11710, 0019, 'BloodyDevil', 0, 0, 0, 0002, 0000, 0000, 7289, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11716, 11716, 0019, 'Pheasant', 0, 0, 0, 0002, 0000, 0000, 2100, 0000, 0100, 0120, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11717, 11717, 0019, 'HawkDefender', 0, 0, 0, 0002, 0000, 0000, 7316, 0000, 0100, 0120, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11718, 11718, 0019, 'NightDevil', 0, 0, 0, 0002, 0000, 0000, 2030, 0000, 0100, 0120, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11930, 11930, 0019, 'RoaringApe', 0, 0, 0, 0002, 0000, 0000, 31, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11940, 11940, 0019, 'RedFox', 0, 0, 0, 0002, 0000, 0000, 7026, 0000, 0100, 0010, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11960, 11960, 0070, 'BreathFocus', 0, 0, 0, 0008, 0000, 0000, 5, 0000, 0100, 0000, 0000, 0000, 0000, 0000, 20243, 0015, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11961, 11960, 0070, 'BreathFocus', 0, 0, 0, 0008, 0001, 0000, 6, 0000, 0100, 0000, 0000, 0000, 0000, 0000, 60000, 0030, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11962, 11960, 0070, 'BreathFocus', 0, 0, 0, 0008, 0002, 0000, 7, 0000, 0100, 0000, 0000, 0000, 0000, 0000, 100000, 0045, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11963, 11960, 0070, 'BreathFocus', 0, 0, 0, 0008, 0003, 0000, 8, 0000, 0100, 0000, 0000, 0000, 0000, 0000, 150000, 0060, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11964, 11960, 0070, 'BreathFocus', 0, 0, 0, 0008, 0004, 0000, 9, 0000, 0100, 0000, 0000, 0000, 0000, 0000, 200000, 0070, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11965, 11960, 0070, 'BreathFocus', 0, 0, 0, 0008, 0005, 0000, 10, 0000, 0100, 0000, 0000, 0000, 0000, 0000, 300000, 0080, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11966, 11960, 0070, 'BreathFocus', 0, 0, 0, 0008, 0006, 0000, 11, 0000, 0100, 0000, 0000, 0000, 0000, 0000, 500000, 0090, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11967, 11960, 0070, 'BreathFocus', 0, 0, 0, 0008, 0007, 0000, 12, 0000, 0100, 0000, 0000, 0000, 0000, 0000, 750000, 0100, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11968, 11960, 0070, 'BreathFocus', 0, 0, 0, 0008, 0008, 0000, 14, 0000, 0100, 0000, 0000, 0000, 0000, 0000, 1000000, 0110, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11969, 11960, 0070, 'BreathFocus', 0, 0, 0, 0008, 0009, 0000, 20, 0000, 0100, 0000, 0000, 0000, 0000, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11970, 11970, 0006, 'SuperCyclone', 0, 0, 0, 0002, 0000, 0000, 30020, 0000, 0100, 0040, 0000, 0000, 0150, 0000, 0, 0000, 1, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11980, 11980, 0071, 'FatalCross', 1, 1, 1, 0004, 0000, 0000, 30085, 0000, 0100, 0000, 0002, 0026, 0000, 0000, 300000, 0040, 0, 61614614, 0000, 0, 0000, 0, 0000, 0, 0025, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11981, 11980, 0071, 'FatalCross', 1, 1, 1, 0004, 0001, 0000, 30090, 0000, 0100, 0000, 0002, 0026, 0000, 0000, 1000000, 0070, 0, 61614614, 0000, 0, 0000, 0, 0000, 0, 0025, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11982, 11980, 0071, 'FatalCross', 1, 1, 1, 0004, 0002, 0000, 30095, 0000, 0100, 0000, 0002, 0026, 0000, 0000, 5000000, 0080, 0, 61614614, 0000, 0, 0000, 0, 0000, 0, 0025, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11983, 11980, 0071, 'FatalCross', 1, 1, 1, 0004, 0003, 0000, 30100, 0000, 0100, 0000, 0002, 0026, 0000, 0000, 10000000, 0090, 0, 61614614, 0000, 0, 0000, 0, 0000, 0, 0025, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11984, 11980, 0071, 'FatalCross', 1, 1, 1, 0004, 0004, 0000, 30105, 0000, 0100, 0000, 0002, 0026, 0000, 0000, 50000000, 0100, 0, 61614614, 0000, 0, 0000, 0, 0000, 0, 0025, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11985, 11980, 0071, 'FatalCross', 1, 1, 1, 0004, 0005, 0000, 30110, 0000, 0100, 0000, 0002, 0026, 0000, 0000, 100000000, 0110, 0, 61614614, 0000, 0, 0000, 0, 0000, 0, 0025, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11986, 11980, 0071, 'FatalCross', 1, 1, 1, 0004, 0006, 0000, 30115, 0000, 0100, 0000, 0002, 0026, 0000, 0000, 0, 0000, 0, 61614614, 0000, 0, 0000, 0, 0000, 0, 0025, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11990, 11990, 0069, 'MortalStrike', 1, 1, 0, 0008, 0000, 0000, 30195, 0000, 0030, 0000, 0005, 0000, 0000, 0000, 20243, 0015, 0, 61614614, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11991, 11990, 0069, 'MortalStrike', 1, 1, 0, 0008, 0001, 0000, 30200, 0000, 0031, 0000, 0005, 0000, 0000, 0000, 60000, 0030, 0, 61614614, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11992, 11990, 0069, 'MortalStrike', 1, 1, 0, 0008, 0002, 0000, 30205, 0000, 0032, 0000, 0005, 0000, 0000, 0000, 100000, 0045, 0, 61614614, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11993, 11990, 0069, 'MortalStrike', 1, 1, 0, 0008, 0003, 0000, 30210, 0000, 0033, 0000, 0005, 0000, 0000, 0000, 150000, 0060, 0, 61614614, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11994, 11990, 0069, 'MortalStrike', 1, 1, 0, 0008, 0004, 0000, 30215, 0000, 0034, 0000, 0005, 0000, 0000, 0000, 200000, 0070, 0, 61614614, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11995, 11990, 0069, 'MortalStrike', 1, 1, 0, 0008, 0005, 0000, 30220, 0000, 0035, 0000, 0005, 0000, 0000, 0000, 300000, 0080, 0, 61614614, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11996, 11990, 0069, 'MortalStrike', 1, 1, 0, 0008, 0006, 0000, 30225, 0000, 0036, 0000, 0005, 0000, 0000, 0000, 500000, 0090, 0, 61614614, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11997, 11990, 0069, 'MortalStrike', 1, 1, 0, 0008, 0007, 0000, 30230, 0000, 0037, 0000, 0005, 0000, 0000, 0000, 750000, 0100, 0, 61614614, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11998, 11990, 0069, 'MortalStrike', 1, 1, 0, 0008, 0008, 0000, 30235, 0000, 0038, 0000, 0005, 0000, 0000, 0000, 1000000, 0110, 0, 61614614, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (11999, 11990, 0069, 'MortalStrike', 1, 1, 0, 0008, 0009, 0000, 30240, 0000, 0042, 0000, 0005, 0000, 0000, 0000, 0, 0000, 0, 61614614, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12000, 12000, 0019, 'EasterRabbit', 0, 0, 0, 0002, 0000, 0000, 3322, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12001, 12001, 0019, 'EasterChicken', 0, 0, 0, 0002, 0000, 0000, 2, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12002, 12002, 0019, 'FlowerFairy', 0, 0, 0, 0002, 0000, 0000, 5058, 0000, 0100, 0060, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12020, 12020, 0072, 'SummonFox', 0, 0, 0, 0002, 0000, 0000, 7554, 0000, 0100, 0000, 0000, 0010, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12030, 12030, 0072, 'SummonCrane', 0, 0, 0, 0002, 0000, 0000, 7555, 0000, 0100, 0000, 0000, 0010, 0001, 0000, 3000, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12031, 12030, 0072, 'SummonCrane', 0, 0, 0, 0002, 0001, 0000, 7556, 0000, 0100, 0000, 0000, 0010, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12040, 12040, 0072, 'SummonPanda', 0, 0, 0, 0002, 0000, 0000, 7557, 0000, 0100, 0000, 0000, 0010, 0001, 0000, 700, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12041, 12040, 0072, 'SummonPanda', 0, 0, 0, 0002, 0001, 0000, 7558, 0000, 0100, 0000, 0000, 0010, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12050, 12050, 0072, 'SummonVoltaicWarg', 0, 0, 0, 0002, 0000, 0000, 7559, 0000, 0100, 0000, 0000, 0010, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12060, 12060, 0001, 'WheelOfDevastation', 0, 0, 0, 0004, 0000, 0000, 250, 0000, 0100, 0000, 0006, 0006, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12061, 12061, 0035, 'SwordVerdict', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0002, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12062, 12062, 0004, 'RagingFlame', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12063, 12063, 0035, 'MeteorFlame', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0015, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12064, 12064, 0043, 'DoomLeap', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0003, 0015, 0020, 0055, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12065, 12065, 0002, 'TideTurner', 0, 0, 0, 0001, 0000, 0000, 800, 0000, 0100, 0000, 0000, 0000, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12066, 12066, 0034, 'IceRaid', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0004, 0006, 0006, 0056, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12070, 12070, 0021, 'TwilightDance', 1, 1, 1, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0003, 0020, 0000, 0000, 900000, 0040, 0, 61616616, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12071, 12070, 0021, 'TwilightDance', 1, 1, 1, 0004, 0001, 0000, 0, 0000, 0100, 0000, 0003, 0020, 0000, 0000, 3000000, 0070, 0, 61616616, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12072, 12070, 0021, 'TwilightDance', 1, 1, 1, 0004, 0002, 0000, 0, 0000, 0100, 0000, 0003, 0020, 0000, 0000, 10000000, 0080, 0, 61616616, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12073, 12070, 0021, 'TwilightDance', 1, 1, 1, 0004, 0003, 0000, 0, 0000, 0100, 0000, 0003, 0020, 0000, 0000, 50000000, 0090, 0, 61616616, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12074, 12070, 0021, 'TwilightDance', 1, 1, 1, 0004, 0004, 0000, 0, 0000, 0100, 0000, 0003, 0020, 0000, 0000, 100000000, 0100, 0, 61616616, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12075, 12070, 0021, 'TwilightDance', 1, 1, 1, 0004, 0005, 0000, 0, 0000, 0100, 0000, 0003, 0020, 0000, 0000, 500000000, 0110, 0, 61616616, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12076, 12070, 0021, 'TwilightDance', 1, 1, 1, 0004, 0006, 0000, 0, 0000, 0100, 0000, 0003, 0020, 0000, 0000, 0, 0000, 0, 61616616, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12080, 12080, 0001, 'SuperTwofoldBlade', 1, 0, 0, 0000, 0000, 0000, 30085, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 300000, 0040, 2, 60601616, 0000, 0, 0000, 0, 0000, 0, 0015, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12081, 12080, 0001, 'SuperTwofoldBlade', 1, 0, 0, 0000, 0001, 0000, 30088, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 900000, 0070, 2, 60601616, 0000, 0, 0000, 0, 0000, 0, 0015, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12082, 12080, 0001, 'SuperTwofoldBlade', 1, 0, 0, 0000, 0002, 0000, 30090, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 2223000, 0100, 2, 60601616, 0000, 0, 0000, 0, 0000, 0, 0015, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12083, 12080, 0001, 'SuperTwofoldBlade', 1, 0, 0, 0000, 0003, 0000, 30092, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 4320000, 0120, 2, 60601616, 0000, 0, 0000, 0, 0000, 0, 0015, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12084, 12080, 0001, 'SuperTwofoldBlade', 1, 0, 0, 0000, 0004, 0000, 30102, 0000, 0100, 0000, 0000, 0005, 0001, 0000, 0, 0000, 2, 60601616, 0000, 0, 0000, 0, 0000, 0, 0015, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12090, 12090, 0023, 'ShadowClone', 0, 0, 0, 0002, 0000, 0000, 7570, 0000, 0100, 9999999, 0000, 0015, 0001, 0000, 5000, 0050, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0040, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12091, 12090, 0023, 'ShadowClone', 0, 0, 0, 0002, 0001, 0000, 7570, 0000, 0100, 9999999, 0000, 0015, 0001, 0000, 10000, 0060, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0040, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12092, 12090, 0023, 'ShadowClone', 0, 0, 0, 0002, 0002, 0000, 7570, 0000, 0100, 9999999, 0000, 0015, 0001, 0000, 20000, 0070, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0040, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12093, 12090, 0023, 'ShadowClone', 0, 0, 0, 0002, 0003, 0000, 7570, 0000, 0100, 9999999, 0000, 0015, 0001, 0000, 30000, 0080, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0040, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12094, 12090, 0023, 'ShadowClone', 0, 0, 0, 0002, 0004, 0000, 7570, 0000, 0100, 9999999, 0000, 0015, 0001, 0000, 50000, 0090, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0040, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12095, 12090, 0023, 'ShadowClone', 0, 0, 0, 0002, 0005, 0000, 7570, 0000, 0100, 9999999, 0000, 0015, 0001, 0000, 70000, 0100, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0040, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12096, 12090, 0023, 'ShadowClone', 0, 0, 0, 0002, 0006, 0000, 7570, 0000, 0100, 9999999, 0000, 0015, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0040, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12101, 12101, 0036, 'Frozen', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12102, 12102, 0002, 'Metaplasia', 0, 0, 0, 0001, 0000, 0000, 200, 0000, 0100, 0000, 0000, 0000, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12103, 12103, 0001, 'IceClaw', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0005, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12104, 12104, 0001, 'Icebound', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0005, 0015, 0020, 0056, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12105, 12105, 0034, 'BitterCold', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0003, 0006, 0006, 0056, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12106, 12106, 0035, 'IceRain', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0003, 0008, 0015, 0056, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12107, 12107, 0035, 'SwordVerdict', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0002, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12110, 12110, 0073, 'FatalSpin', 1, 0, 0, 0004, 0000, 0000, 30190, 0000, 0030, 0000, 0001, 0007, 0001, 0000, 20243, 0015, 2, 61616616, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12111, 12110, 0073, 'FatalSpin', 1, 0, 0, 0004, 0001, 0000, 30195, 0000, 0031, 0000, 0001, 0007, 0001, 0000, 60000, 0030, 2, 61616616, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12112, 12110, 0073, 'FatalSpin', 1, 0, 0, 0004, 0002, 0000, 30200, 0000, 0032, 0000, 0001, 0007, 0001, 0000, 100000, 0045, 2, 61616616, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12113, 12110, 0073, 'FatalSpin', 1, 0, 0, 0004, 0003, 0000, 30205, 0000, 0033, 0000, 0001, 0007, 0001, 0000, 150000, 0060, 2, 61616616, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12114, 12110, 0073, 'FatalSpin', 1, 0, 0, 0004, 0004, 0000, 30210, 0000, 0034, 0000, 0001, 0007, 0001, 0000, 200000, 0070, 2, 61616616, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12115, 12110, 0073, 'FatalSpin', 1, 0, 0, 0004, 0005, 0000, 30215, 0000, 0035, 0000, 0001, 0007, 0001, 0000, 300000, 0080, 2, 61616616, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12116, 12110, 0073, 'FatalSpin', 1, 0, 0, 0004, 0006, 0000, 30220, 0000, 0036, 0000, 0001, 0007, 0001, 0000, 500000, 0090, 2, 61616616, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12117, 12110, 0073, 'FatalSpin', 1, 0, 0, 0004, 0007, 0000, 30225, 0000, 0037, 0000, 0001, 0007, 0001, 0000, 750000, 0100, 2, 61616616, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12118, 12110, 0073, 'FatalSpin', 1, 0, 0, 0004, 0008, 0000, 30230, 0000, 0038, 0000, 0001, 0007, 0001, 0000, 1000000, 0110, 2, 61616616, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12119, 12110, 0073, 'FatalSpin', 1, 0, 0, 0004, 0009, 0000, 30240, 0000, 0042, 0000, 0001, 0007, 0001, 0000, 0, 0000, 2, 61616616, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12120, 12120, 0001, 'SpeedKick', 1, 0, 0, 0000, 0000, 0000, 30030, 0000, 0100, 0000, 0000, 0015, 0000, 0000, 500000, 0040, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0010, 0, 0000, 12130, 0000, 0000);
INSERT INTO `spells` VALUES (12121, 12120, 0001, 'SpeedKick', 1, 0, 0, 0000, 0001, 0000, 30032, 0000, 0100, 0000, 0000, 0015, 0000, 0000, 10000000, 0050, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0010, 0, 0000, 12130, 0000, 0000);
INSERT INTO `spells` VALUES (12122, 12120, 0001, 'SpeedKick', 1, 0, 0, 0000, 0002, 0000, 30034, 0000, 0100, 0000, 0000, 0015, 0000, 0000, 0, 0000, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0010, 0, 0000, 12130, 0000, 0000);
INSERT INTO `spells` VALUES (12130, 12130, 0001, 'ViolentKick', 1, 0, 0, 0000, 0000, 0000, 30034, 0000, 0100, 0000, 0000, 0015, 0000, 0000, 5000000, 0070, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0005, 0, 0000, 12140, 0000, 0000);
INSERT INTO `spells` VALUES (12131, 12130, 0001, 'ViolentKick', 1, 0, 0, 0000, 0001, 0000, 30035, 0000, 0100, 0000, 0000, 0015, 0000, 0000, 0, 0000, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0005, 0, 0000, 12140, 0000, 0000);
INSERT INTO `spells` VALUES (12140, 12140, 0001, 'StormKick', 1, 0, 0, 0000, 0000, 0000, 30036, 0000, 0100, 0000, 0000, 0015, 0000, 0000, 0, 0000, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12160, 12160, 0069, 'CrackingSwipe', 1, 0, 1, 0000, 0000, 0000, 30090, 0000, 0100, 0000, 0008, 0020, 0000, 0000, 300000, 0015, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 12170, 0000, 0000);
INSERT INTO `spells` VALUES (12161, 12160, 0069, 'CrackingSwipe', 1, 0, 1, 0000, 0001, 0000, 30095, 0000, 0100, 0000, 0008, 0020, 0000, 0000, 3000000, 0025, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 12170, 0000, 0000);
INSERT INTO `spells` VALUES (12162, 12160, 0069, 'CrackingSwipe', 1, 0, 1, 0000, 0002, 0000, 30100, 0000, 0100, 0000, 0008, 0020, 0000, 0000, 10000000, 0035, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 12170, 0000, 0000);
INSERT INTO `spells` VALUES (12163, 12160, 0069, 'CrackingSwipe', 1, 0, 1, 0000, 0003, 0000, 30105, 0000, 0100, 0000, 0008, 0020, 0000, 0000, 30000000, 0040, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 12170, 0000, 0000);
INSERT INTO `spells` VALUES (12164, 12160, 0069, 'CrackingSwipe', 1, 0, 1, 0000, 0004, 0000, 30110, 0000, 0100, 0000, 0008, 0020, 0000, 0000, 0, 0000, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0020, 0, 0000, 12170, 0000, 0000);
INSERT INTO `spells` VALUES (12170, 12170, 0069, 'SplittingSwipe', 1, 0, 1, 0000, 0000, 0000, 30093, 0000, 0100, 0000, 0008, 0020, 0000, 0000, 300000, 0040, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0010, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12171, 12170, 0069, 'SplittingSwipe', 1, 0, 1, 0000, 0001, 0000, 30098, 0000, 0100, 0000, 0008, 0020, 0000, 0000, 3000000, 0050, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0010, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12172, 12170, 0069, 'SplittingSwipe', 1, 0, 1, 0000, 0002, 0000, 30103, 0000, 0100, 0000, 0008, 0020, 0000, 0000, 10000000, 0060, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0010, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12173, 12170, 0069, 'SplittingSwipe', 1, 0, 1, 0000, 0003, 0000, 30108, 0000, 0100, 0000, 0008, 0020, 0000, 0000, 30000000, 0070, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0010, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12174, 12170, 0069, 'SplittingSwipe', 1, 0, 1, 0000, 0004, 0000, 30118, 0000, 0100, 0000, 0008, 0020, 0000, 0000, 0, 0000, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0010, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12200, 12200, 0006, 'DragonSwing', 0, 0, 0, 0001, 0000, 0000, 1100, 0000, 0100, 1000000, 0000, 0000, 0160, 0000, 5000, 0070, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12201, 12200, 0006, 'DragonSwing', 0, 0, 0, 0001, 0001, 0000, 1200, 0000, 0100, 1000000, 0000, 0000, 0160, 0000, 10000, 0080, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12202, 12200, 0006, 'DragonSwing', 0, 0, 0, 0001, 0002, 0000, 1300, 0000, 0100, 1000000, 0000, 0000, 0160, 0000, 50000, 0090, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12203, 12200, 0006, 'DragonSwing', 0, 0, 0, 0001, 0003, 0000, 1500, 0000, 0100, 1000000, 0000, 0000, 0160, 0000, 100000, 0100, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12204, 12200, 0006, 'DragonSwing', 0, 0, 0, 0001, 0004, 0000, 2000, 0000, 0100, 1000000, 0000, 0000, 0160, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0030, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12210, 12210, 0078, 'AirStrike', 0, 0, 1, 0008, 0000, 0000, 0, 0000, 0100, 0000, 0006, 0000, 0000, 0000, 0, 0000, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12220, 12220, 0078, 'EarthSweep', 0, 0, 1, 0008, 0000, 0000, 0, 0000, 0100, 0000, 0007, 0000, 0000, 0000, 0, 0000, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12230, 12230, 0076, 'Kick', 0, 0, 1, 0008, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0000, 0000, 0000, 0, 0000, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12240, 12240, 0073, 'DragonPunch', 0, 0, 0, 0004, 0000, 0000, 30180, 0000, 0030, 0000, 0001, 0007, 0000, 0000, 10000, 0015, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12241, 12240, 0073, 'DragonPunch', 0, 0, 0, 0004, 0001, 0000, 30185, 0000, 0031, 0000, 0001, 0007, 0000, 0000, 50000, 0020, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12242, 12240, 0073, 'DragonPunch', 0, 0, 0, 0004, 0002, 0000, 30190, 0000, 0032, 0000, 0001, 0007, 0000, 0000, 100000, 0025, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12243, 12240, 0073, 'DragonPunch', 0, 0, 0, 0004, 0003, 0000, 30195, 0000, 0033, 0000, 0001, 0007, 0000, 0000, 150000, 0030, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12244, 12240, 0073, 'DragonPunch', 0, 0, 0, 0004, 0004, 0000, 30200, 0000, 0034, 0000, 0001, 0007, 0000, 0000, 200000, 0040, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12245, 12240, 0073, 'DragonPunch', 0, 0, 0, 0004, 0005, 0000, 30205, 0000, 0035, 0000, 0001, 0007, 0000, 0000, 300000, 0050, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12246, 12240, 0073, 'DragonPunch', 0, 0, 0, 0004, 0006, 0000, 30210, 0000, 0036, 0000, 0001, 0007, 0000, 0000, 500000, 0060, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12247, 12240, 0073, 'DragonPunch', 0, 0, 0, 0004, 0007, 0000, 30220, 0000, 0037, 0000, 0001, 0007, 0000, 0000, 750000, 0070, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12248, 12240, 0073, 'DragonPunch', 0, 0, 0, 0004, 0008, 0000, 30230, 0000, 0038, 0000, 0001, 0007, 0000, 0000, 1000000, 0080, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12249, 12240, 0073, 'DragonPunch', 0, 0, 0, 0004, 0009, 0000, 30245, 0000, 0042, 0000, 0001, 0007, 0000, 0000, 0, 0000, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12270, 12270, 0006, 'DragonFlow', 0, 0, 0, 0002, 0000, 0000, 5, 0000, 0100, 0008, 0000, 0000, 0148, 0000, 5000, 0015, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12271, 12270, 0006, 'DragonFlow', 0, 0, 0, 0002, 0001, 0000, 10, 0000, 0100, 0008, 0000, 0000, 0148, 0000, 20000, 0025, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12272, 12270, 0006, 'DragonFlow', 0, 0, 0, 0002, 0002, 0000, 15, 0000, 0100, 0008, 0000, 0000, 0148, 0000, 100000, 0035, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12273, 12270, 0006, 'DragonFlow', 0, 0, 0, 0002, 0003, 0000, 20, 0000, 0100, 0008, 0000, 0000, 0148, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12280, 12280, 0011, 'DragonRoar', 0, 0, 0, 0002, 0000, 0000, 20, 0000, 0100, 0000, 0012, 0000, 0001, 0000, 5000, 0040, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12281, 12280, 0011, 'DragonRoar', 0, 0, 0, 0002, 0001, 0000, 25, 0000, 0100, 0000, 0012, 0000, 0001, 0000, 10000, 0050, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12282, 12280, 0011, 'DragonRoar', 0, 0, 0, 0002, 0002, 0000, 30, 0000, 0100, 0000, 0012, 0000, 0001, 0000, 50000, 0060, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12283, 12280, 0011, 'DragonRoar', 0, 0, 0, 0002, 0003, 0000, 35, 0000, 0100, 0000, 0012, 0000, 0001, 0000, 100000, 0070, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12284, 12280, 0011, 'DragonRoar', 0, 0, 0, 0002, 0004, 0000, 50, 0000, 0100, 0000, 0012, 0000, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12290, 12290, 0075, 'DragonCyclone', 1, 1, 0, 0004, 0000, 0000, 30500, 0000, 0100, 0045, 0004, 0020, 0159, 0000, 0, 0000, 1, 0617, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12300, 12300, 0001, 'DragonFury', 1, 0, 0, 0000, 0000, 0000, 30010, 0000, 0100, 0004, 0000, 0012, 0158, 0000, 100000, 0070, 0, 0617, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12301, 12300, 0001, 'DragonFury', 1, 0, 0, 0000, 0001, 0000, 30015, 0000, 0100, 0004, 0000, 0012, 0158, 0000, 300000, 0080, 0, 0617, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12302, 12300, 0001, 'DragonFury', 1, 0, 0, 0000, 0002, 0000, 30020, 0000, 0100, 0004, 0000, 0012, 0158, 0000, 741000, 0090, 0, 0617, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12303, 12300, 0001, 'DragonFury', 1, 0, 0, 0000, 0003, 0000, 30025, 0000, 0100, 0004, 0000, 0012, 0158, 0000, 1440000, 0100, 0, 0617, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12304, 12300, 0001, 'DragonFury', 1, 0, 0, 0000, 0004, 0000, 30030, 0000, 0100, 0004, 0000, 0012, 0158, 0000, 0, 0000, 0, 0617, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12320, 12320, 0079, 'AirKick', 1, 1, 1, 0004, 0000, 0000, 30020, 0000, 0100, 0000, 0004, 0020, 0001, 0000, 5000000, 0015, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0015, 0, 0000, 12330, 0000, 0000);
INSERT INTO `spells` VALUES (12321, 12320, 0079, 'AirKick', 1, 1, 1, 0004, 0001, 0000, 30022, 0000, 0100, 0000, 0004, 0020, 0001, 0000, 100000000, 0015, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0015, 0, 0000, 12330, 0000, 0000);
INSERT INTO `spells` VALUES (12322, 12320, 0079, 'AirKick', 1, 1, 1, 0004, 0002, 0000, 30025, 0000, 0100, 0000, 0004, 0020, 0001, 0000, 0, 0000, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0015, 0, 0000, 12330, 0000, 0000);
INSERT INTO `spells` VALUES (12330, 12330, 0054, 'AirSweep', 1, 1, 1, 0004, 0000, 0000, 30030, 0000, 0100, 0000, 0004, 0020, 0000, 0000, 300000000, 0015, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0005, 0, 0000, 12340, 0000, 0000);
INSERT INTO `spells` VALUES (12331, 12330, 0054, 'AirSweep', 1, 1, 1, 0004, 0001, 0000, 30035, 0000, 0100, 0000, 0004, 0020, 0000, 0000, 0, 0000, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0005, 0, 0000, 12340, 0000, 0000);
INSERT INTO `spells` VALUES (12340, 12340, 0054, 'AirRaid', 1, 1, 1, 0004, 0000, 0000, 30050, 0000, 0100, 0000, 0004, 0020, 0000, 0000, 0, 0000, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12350, 12350, 0014, 'DragonSlash', 1, 1, 0, 0004, 0000, 0000, 30110, 0000, 0100, 0000, 0011, 0020, 0001, 0000, 100000, 0040, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0015, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12351, 12350, 0014, 'DragonSlash', 1, 1, 0, 0004, 0001, 0000, 30115, 0000, 0100, 0000, 0011, 0020, 0001, 0000, 300000, 0050, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0015, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12352, 12350, 0014, 'DragonSlash', 1, 1, 0, 0004, 0002, 0000, 30120, 0000, 0100, 0000, 0011, 0020, 0001, 0000, 741000, 0060, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0015, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12353, 12350, 0014, 'DragonSlash', 1, 1, 0, 0004, 0003, 0000, 30125, 0000, 0100, 0000, 0011, 0020, 0001, 0000, 1440000, 0070, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0015, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12354, 12350, 0014, 'DragonSlash', 1, 1, 0, 0004, 0004, 0000, 30135, 0000, 0100, 0000, 0011, 0020, 0001, 0000, 0, 0000, 0, 61617617, 0000, 0, 0000, 0, 0000, 0, 0015, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12361, 12361, 0019, 'SunnyFox', 0, 0, 0, 0002, 0000, 0000, 7026, 0000, 0100, 0030, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12370, 12370, 0021, 'AuroraLotus', 0, 1, 1, 0004, 0000, 0000, 200005, 0000, 0100, 0000, 0004, 0015, 0000, 0000, 5000, 0050, 0, 0620, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12371, 12370, 0021, 'AuroraLotus', 0, 1, 1, 0004, 0001, 0000, 200005, 0000, 0100, 0000, 0004, 0015, 0000, 0000, 10000, 0060, 0, 0620, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12372, 12370, 0021, 'AuroraLotus', 0, 1, 1, 0004, 0002, 0000, 200005, 0000, 0100, 0000, 0004, 0015, 0000, 0000, 20000, 0070, 0, 0620, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12373, 12370, 0021, 'AuroraLotus', 0, 1, 1, 0004, 0003, 0000, 200005, 0000, 0100, 0000, 0004, 0015, 0000, 0000, 50000, 0080, 0, 0620, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12374, 12370, 0021, 'AuroraLotus', 0, 1, 1, 0004, 0004, 0000, 200005, 0000, 0100, 0000, 0004, 0015, 0000, 0000, 100000, 0090, 0, 0620, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12375, 12370, 0021, 'AuroraLotus', 0, 1, 1, 0004, 0005, 0000, 200005, 0000, 0100, 0000, 0004, 0015, 0000, 0000, 150000, 0100, 0, 0620, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12376, 12370, 0021, 'AuroraLotus', 0, 1, 1, 0004, 0006, 0000, 200005, 0000, 0100, 0000, 0004, 0015, 0000, 0000, 0, 0000, 0, 0620, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12380, 12380, 0021, 'FlameLotus', 1, 1, 1, 0004, 0000, 0000, 300005, 0000, 0100, 0000, 0004, 0015, 0000, 0000, 5000, 0050, 0, 0620, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12381, 12380, 0021, 'FlameLotus', 1, 1, 1, 0004, 0001, 0000, 300005, 0000, 0100, 0000, 0004, 0015, 0000, 0000, 10000, 0060, 0, 0620, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12382, 12380, 0021, 'FlameLotus', 1, 1, 1, 0004, 0002, 0000, 300005, 0000, 0100, 0000, 0004, 0015, 0000, 0000, 20000, 0070, 0, 0620, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12383, 12380, 0021, 'FlameLotus', 1, 1, 1, 0004, 0003, 0000, 300005, 0000, 0100, 0000, 0004, 0015, 0000, 0000, 50000, 0080, 0, 0620, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12384, 12380, 0021, 'FlameLotus', 1, 1, 1, 0004, 0004, 0000, 300005, 0000, 0100, 0000, 0004, 0015, 0000, 0000, 100000, 0090, 0, 0620, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12385, 12380, 0021, 'FlameLotus', 1, 1, 1, 0004, 0005, 0000, 300005, 0000, 0100, 0000, 0004, 0015, 0000, 0000, 150000, 0100, 0, 0620, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12386, 12380, 0021, 'FlameLotus', 1, 1, 1, 0004, 0006, 0000, 300005, 0000, 0100, 0000, 0004, 0015, 0000, 0000, 0, 0000, 0, 0620, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12390, 12390, 0000, 'BlessingTouch', 0, 0, 0, 0000, 0000, 0000, 0, 0000, 0010, 0000, 0000, 0000, 0000, 0000, 10000, 0050, 0, 0619, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12391, 12390, 0000, 'BlessingTouch', 0, 0, 0, 0000, 0001, 0000, 0, 0000, 0013, 0000, 0000, 0000, 0000, 0000, 50000, 0060, 0, 0619, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12392, 12390, 0000, 'BlessingTouch', 0, 0, 0, 0000, 0002, 0000, 0, 0000, 0016, 0000, 0000, 0000, 0000, 0000, 100000, 0070, 0, 0619, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12393, 12390, 0000, 'BlessingTouch', 0, 0, 0, 0000, 0003, 0000, 0, 0000, 0019, 0000, 0000, 0000, 0000, 0000, 200000, 0080, 0, 0619, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12394, 12390, 0000, 'BlessingTouch', 0, 0, 0, 0000, 0004, 0000, 0, 0000, 0022, 0000, 0000, 0000, 0000, 0000, 500000, 0090, 0, 0619, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12395, 12390, 0000, 'BlessingTouch', 0, 0, 0, 0000, 0005, 0000, 0, 0000, 0025, 0000, 0000, 0000, 0000, 0000, 1000000, 0100, 0, 0619, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12396, 12390, 0000, 'BlessingTouch', 0, 0, 0, 0000, 0006, 0000, 0, 0000, 0030, 0000, 0000, 0000, 0000, 0000, 0, 0000, 0, 0619, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12400, 12400, 0005, 'SearingTouch', 1, 1, 0, 0000, 0000, 0000, 0, 0000, 0100, 0000, 0006, 0000, 0000, 0000, 10000, 0050, 0, 0619, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12401, 12400, 0005, 'SearingTouch', 1, 1, 0, 0000, 0001, 0000, 0, 0000, 0100, 0000, 0006, 0000, 0000, 0000, 50000, 0060, 0, 0619, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12402, 12400, 0005, 'SearingTouch', 1, 1, 0, 0000, 0002, 0000, 0, 0000, 0100, 0000, 0006, 0000, 0000, 0000, 100000, 0070, 0, 0619, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12403, 12400, 0005, 'SearingTouch', 1, 1, 0, 0000, 0003, 0000, 0, 0000, 0100, 0000, 0006, 0000, 0000, 0000, 200000, 0080, 0, 0619, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12404, 12400, 0005, 'SearingTouch', 1, 1, 0, 0000, 0004, 0000, 0, 0000, 0100, 0000, 0006, 0000, 0000, 0000, 500000, 0090, 0, 0619, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12405, 12400, 0005, 'SearingTouch', 1, 1, 0, 0000, 0005, 0000, 0, 0000, 0100, 0000, 0006, 0000, 0000, 0000, 1000000, 0100, 0, 0619, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12406, 12400, 0005, 'SearingTouch', 1, 1, 0, 0000, 0006, 0000, 0, 0000, 0100, 0000, 0006, 0000, 0000, 0000, 0, 0000, 0, 0619, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12550, 12550, 0021, 'InfernalEcho', 1, 1, 0, 0004, 0000, 0000, 30070, 0000, 0100, 0000, 0003, 0015, 0000, 0000, 900000, 10000, 0, 0622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 3000);
INSERT INTO `spells` VALUES (12551, 12550, 0021, 'InfernalEcho', 1, 1, 0, 0004, 0001, 0000, 30074, 0000, 0100, 0000, 0003, 0015, 0000, 0000, 3000000, 30000, 0, 0622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 5000);
INSERT INTO `spells` VALUES (12552, 12550, 0021, 'InfernalEcho', 1, 1, 0, 0004, 0002, 0000, 30074, 0000, 0100, 0000, 0003, 0015, 0000, 0000, 3000000, 30000, 0, 0622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 5000);
INSERT INTO `spells` VALUES (12553, 12550, 0021, 'InfernalEcho', 1, 1, 0, 0004, 0003, 0000, 30074, 0000, 0100, 0000, 0003, 0015, 0000, 0000, 3000000, 30000, 0, 0622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 5000);
INSERT INTO `spells` VALUES (12554, 12550, 0021, 'InfernalEcho', 1, 1, 0, 0004, 0004, 0000, 30074, 0000, 0100, 0000, 0003, 0015, 0000, 0000, 3000000, 30000, 0, 0622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 5000);
INSERT INTO `spells` VALUES (12555, 12550, 0021, 'InfernalEcho', 1, 1, 0, 0004, 0005, 0000, 30074, 0000, 0100, 0000, 0003, 0015, 0000, 0000, 3000000, 30000, 0, 0622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 5000);
INSERT INTO `spells` VALUES (12556, 12550, 0021, 'InfernalEcho', 1, 1, 0, 0004, 0006, 0000, 30074, 0000, 0100, 0000, 0003, 0015, 0000, 0000, 3000000, 30000, 0, 0622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 5000);
INSERT INTO `spells` VALUES (12560, 12560, 0000, 'GraceofHeaven', 0, 0, 0, 0008, 0000, 0000, 10, 0000, 0000, 0000, 0000, 0000, 0000, 0000, 5000, 0050, 0, 61622622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12561, 12560, 0000, 'GraceofHeaven', 0, 0, 0, 0008, 0001, 0000, 15, 0000, 0000, 0000, 0000, 0000, 0000, 0000, 10000, 0060, 0, 61622622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12562, 12560, 0000, 'GraceofHeaven', 0, 0, 0, 0008, 0002, 0000, 20, 0000, 0000, 0000, 0000, 0000, 0000, 0000, 20000, 0070, 0, 61622622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12563, 12560, 0000, 'GraceofHeaven', 0, 0, 0, 0008, 0003, 0000, 25, 0000, 0000, 0000, 0000, 0000, 0000, 0000, 50000, 0080, 0, 61622622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12564, 12560, 0000, 'GraceofHeaven', 0, 0, 0, 0008, 0004, 0000, 30, 0000, 0000, 0000, 0000, 0000, 0000, 0000, 100000, 0090, 0, 61622622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12565, 12560, 0000, 'GraceofHeaven', 0, 0, 0, 0008, 0005, 0000, 35, 0000, 0000, 0000, 0000, 0000, 0000, 0000, 150000, 0100, 0, 61622622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12566, 12560, 0000, 'GraceofHeaven', 0, 0, 0, 0008, 0006, 0000, 50, 0000, 0000, 0000, 0000, 0000, 0000, 0000, 0, 0000, 0, 61622622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12570, 12570, 0021, 'WrathoftheEmperor', 1, 0, 0, 0008, 0000, 0000, 30190, 0000, 0100, 0000, 0002, 0015, 0000, 0000, 5000, 0015, 0, 0622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12571, 12570, 0021, 'WrathoftheEmperor', 1, 0, 0, 0008, 0001, 0000, 30195, 0000, 0100, 0000, 0002, 0015, 0000, 0000, 10000, 0030, 0, 0622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12572, 12570, 0021, 'WrathoftheEmperor', 1, 0, 0, 0008, 0002, 0000, 30200, 0000, 0100, 0000, 0002, 0015, 0000, 0000, 20000, 0045, 0, 0622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12573, 12570, 0021, 'WrathoftheEmperor', 1, 0, 0, 0008, 0003, 0000, 30205, 0000, 0100, 0000, 0002, 0015, 0000, 0000, 50000, 0060, 0, 0622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12574, 12570, 0021, 'WrathoftheEmperor', 1, 0, 0, 0008, 0004, 0000, 30210, 0000, 0100, 0000, 0002, 0015, 0000, 0000, 80000, 0070, 0, 0622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12575, 12570, 0021, 'WrathoftheEmperor', 1, 0, 0, 0008, 0005, 0000, 30215, 0000, 0100, 0000, 0002, 0015, 0000, 0000, 100000, 0080, 0, 0622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12576, 12570, 0021, 'WrathoftheEmperor', 1, 0, 0, 0008, 0006, 0000, 30220, 0000, 0100, 0000, 0002, 0015, 0000, 0000, 200000, 0090, 0, 0622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12577, 12570, 0021, 'WrathoftheEmperor', 1, 0, 0, 0008, 0007, 0000, 30225, 0000, 0100, 0000, 0002, 0015, 0000, 0000, 500000, 0100, 0, 0622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12578, 12570, 0021, 'WrathoftheEmperor', 1, 0, 0, 0008, 0008, 0000, 30230, 0000, 0100, 0000, 0002, 0015, 0000, 0000, 1000000, 0110, 0, 0622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12579, 12570, 0021, 'WrathoftheEmperor', 1, 0, 0, 0008, 0009, 0000, 30245, 0000, 0100, 0000, 0002, 0015, 0000, 0000, 0, 0000, 0, 0622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12580, 12580, 0078, 'UpSweep', 0, 0, 1, 0008, 0000, 0000, 0, 0000, 0100, 0000, 0005, 0000, 0000, 0000, 0, 0000, 0, 61622622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12590, 12590, 0078, 'DownSweep', 0, 0, 1, 0008, 0000, 0000, 0, 0000, 0100, 0000, 0006, 0000, 0000, 0000, 0, 0000, 0, 61622622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12600, 12600, 0081, 'Strike', 0, 0, 1, 0008, 0000, 0000, 0, 0000, 0100, 0000, 0003, 0000, 0000, 0000, 0, 0000, 0, 61622622, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12615, 12615, 0019, 'EvolutionToken', 0, 0, 0, 0002, 0000, 0000, 7901, 0000, 0100, 0600, 0000, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12631, 12631, 0066, 'WrathofDragon', 1, 1, 0, 0004, 0000, 0000, 1000, 0000, 0100, 0000, 0006, 0010, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12632, 12632, 0066, 'WrathofDragon', 1, 1, 0, 0004, 0000, 0000, 30100, 0000, 0100, 0000, 0006, 0010, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12633, 12633, 0005, 'FistofTiger', 1, 0, 1, 0000, 0000, 0000, 30100, 0000, 0100, 0000, 0006, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12634, 12634, 0005, 'RiseofFlame', 1, 0, 1, 0000, 0000, 0000, 10000, 0000, 0100, 0000, 0006, 0010, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12635, 12635, 0005, 'TerrorTurtle', 1, 1, 0, 0000, 0000, 0000, 30095, 0000, 0100, 0000, 0006, 0010, 0001, 0000, 0, 0000, 2, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12637, 12637, 0004, 'DragonBreath', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12638, 12638, 0042, 'SurgingShake', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0008, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12639, 12639, 0035, 'BloodyBattle', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0005, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12640, 12640, 0036, 'LightningCrash', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12641, 12641, 0036, 'MistyFlame', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12642, 12642, 0035, 'Avalanche', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0003, 0008, 0015, 0056, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12643, 12643, 0004, 'RagingFlame', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12644, 12644, 0001, 'WheelOfDevastation', 0, 0, 0, 0004, 0000, 0000, 500, 0000, 0100, 0000, 0006, 0006, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12645, 12645, 0042, 'SurgingShake', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0008, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12646, 12646, 0036, 'LightningCrash', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12647, 12647, 0001, 'PiercingStrike', 0, 0, 0, 0004, 0000, 0000, 800, 0000, 0100, 0000, 0005, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12648, 12648, 0035, 'Immolation', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0002, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12649, 12649, 0041, 'RevengePunch', 0, 0, 0, 0004, 0000, 0000, 2000, 0000, 0100, 0000, 0005, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12650, 12650, 0036, 'BladeStorm', 0, 0, 0, 0004, 0000, 0000, 1000, 0000, 0100, 0000, 0000, 0003, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12651, 12651, 0036, 'BladeVortex', 0, 0, 0, 0004, 0000, 0000, 100, 0000, 0100, 0000, 0012, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12652, 12652, 0036, 'SwordBeam', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12653, 12653, 0036, 'BladeStorm', 0, 0, 0, 0004, 0000, 0000, 500, 0000, 0100, 0000, 0000, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12654, 12654, 0001, 'Slash', 0, 0, 0, 0004, 0000, 0000, 1000, 0000, 0100, 0000, 0005, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12655, 12655, 0036, 'SwordBeam', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12660, 12660, 0006, 'TwistofWar', 0, 0, 0, 0002, 0000, 0000, 0, 0000, 0100, 0000, 0000, 0000, 0182, 0000, 0, 0000, 0, 61624624, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12670, 12670, 0082, 'ScarofEarth', 1, 1, 1, 0008, 0000, 0000, 30195, 0000, 0030, 0003, 0005, 0000, 0180, 0000, 20243, 0015, 0, 61624624, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12671, 12670, 0082, 'ScarofEarth', 1, 1, 1, 0008, 0001, 0000, 30200, 0000, 0031, 0003, 0005, 0000, 0180, 0000, 60000, 0030, 0, 61624624, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12672, 12670, 0082, 'ScarofEarth', 1, 1, 1, 0008, 0002, 0000, 30205, 0000, 0032, 0003, 0005, 0000, 0180, 0000, 100000, 0045, 0, 61624624, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12673, 12670, 0082, 'ScarofEarth', 1, 1, 1, 0008, 0003, 0000, 30210, 0000, 0033, 0003, 0005, 0000, 0180, 0000, 150000, 0060, 0, 61624624, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12674, 12670, 0082, 'ScarofEarth', 1, 1, 1, 0008, 0004, 0000, 30215, 0000, 0034, 0003, 0005, 0000, 0180, 0000, 200000, 0070, 0, 61624624, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12675, 12670, 0082, 'ScarofEarth', 1, 1, 1, 0008, 0005, 0000, 30220, 0000, 0035, 0003, 0005, 0000, 0180, 0000, 300000, 0080, 0, 61624624, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12676, 12670, 0082, 'ScarofEarth', 1, 1, 1, 0008, 0006, 0000, 30225, 0000, 0036, 0003, 0005, 0000, 0180, 0000, 500000, 0090, 0, 61624624, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12677, 12670, 0082, 'ScarofEarth', 1, 1, 1, 0008, 0007, 0000, 30230, 0000, 0037, 0003, 0005, 0000, 0180, 0000, 750000, 0100, 0, 61624624, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12678, 12670, 0082, 'ScarofEarth', 1, 1, 1, 0008, 0008, 0000, 30240, 0000, 0038, 0003, 0005, 0000, 0180, 0000, 1000000, 0110, 0, 61624624, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12679, 12670, 0082, 'ScarofEarth', 1, 1, 1, 0008, 0009, 0000, 30245, 0000, 0042, 0003, 0005, 0000, 0180, 0000, 0, 0000, 0, 61624624, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12680, 12680, 0006, 'Backfire', 0, 0, 0, 0002, 0000, 0000, 55, 0000, 0100, 0010, 0000, 0000, 0179, 0000, 500, 0060, 0, 61624624, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12681, 12680, 0006, 'Backfire', 0, 0, 0, 0002, 0001, 0000, 60, 0000, 0100, 0010, 0000, 0000, 0179, 0000, 1000, 0070, 0, 61624624, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12682, 12680, 0006, 'Backfire', 0, 0, 0, 0002, 0002, 0000, 65, 0000, 0100, 0010, 0000, 0000, 0179, 0000, 10000, 0080, 0, 61624624, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12683, 12680, 0006, 'Backfire', 0, 0, 0, 0002, 0003, 0000, 70, 0000, 0100, 0010, 0000, 0000, 0179, 0000, 50000, 0090, 0, 61624624, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12684, 12680, 0006, 'Backfire', 0, 0, 0, 0002, 0004, 0000, 75, 0000, 0100, 0010, 0000, 0000, 0179, 0000, 100000, 0100, 0, 61624624, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12685, 12680, 0006, 'Backfire', 0, 0, 0, 0002, 0005, 0000, 80, 0000, 0100, 0010, 0000, 0000, 0179, 0000, 500000, 0110, 0, 61624624, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12686, 12680, 0006, 'Backfire', 0, 0, 0, 0002, 0006, 0000, 90, 0000, 0100, 0010, 0000, 0000, 0179, 0000, 0, 0000, 0, 61624624, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12690, 12690, 0069, 'WaveofBlood', 1, 1, 1, 0004, 0000, 0000, 30095, 0000, 0100, 0008, 0008, 0020, 0178, 0000, 500, 3000, 15, 61624624, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12691, 12690, 0069, 'WaveofBlood', 1, 1, 1, 0004, 0001, 0000, 30100, 0000, 0100, 0008, 0008, 0020, 0178, 0000, 1000, 6000, 30, 61624624, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12692, 12690, 0069, 'WaveofBlood', 1, 1, 1, 0004, 0002, 0000, 30105, 0000, 0100, 0008, 0008, 0020, 0178, 0000, 10000, 10000, 50, 61624624, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12693, 12690, 0069, 'WaveofBlood', 1, 1, 1, 0004, 0003, 0000, 30108, 0000, 0100, 0008, 0008, 0020, 0178, 0000, 50000, 50000, 70, 61624624, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12694, 12690, 0069, 'WaveofBlood', 1, 1, 1, 0004, 0004, 0000, 30110, 0000, 0100, 0008, 0008, 0020, 0178, 0000, 100000, 100000, 90, 61624624, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12695, 12690, 0069, 'WaveofBlood', 1, 1, 1, 0004, 0005, 0000, 30115, 0000, 0100, 0008, 0008, 0020, 0178, 0000, 500000, 150000, 110, 61624624, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12696, 12690, 0069, 'WaveofBlood', 1, 1, 1, 0004, 0006, 0000, 30118, 0000, 0100, 0008, 0008, 0020, 0178, 0000, 0, 0000, 0, 61624624, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12700, 12700, 0083, 'ManiacDance', 1, 0, 0, 0002, 0000, 0000, 30100, 0000, 0100, 0001, 0005, 0000, 0181, 0000, 0, 0000, 1, 61624624, 0015, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12722, 12722, 0001, 'Blade', 0, 0, 0, 0004, 0000, 0000, 1000, 0000, 0100, 0000, 0008, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12723, 12723, 0004, 'Smash', 0, 0, 0, 0004, 0000, 0000, 4000, 0000, 0100, 0000, 0008, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12724, 12724, 0033, 'Sweep', 0, 0, 0, 0004, 0000, 0000, 7000, 0000, 0100, 0000, 0005, 0010, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12725, 12725, 0001, 'PiercingStrike', 0, 0, 0, 0004, 0000, 0000, 1000, 0000, 0100, 0000, 0005, 0005, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12726, 12726, 0033, 'TailSweep', 0, 0, 0, 0004, 0000, 0000, 10000, 0000, 0100, 0000, 0008, 0012, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12727, 12727, 0035, 'FlameShower', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0005, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12728, 12728, 0004, 'DragonBreath', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0015, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12729, 12729, 0036, 'MistyFlame', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12730, 12730, 0035, 'Immolation', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0002, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12731, 12731, 0035, 'Avalanche', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0003, 0008, 0015, 0056, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12732, 12732, 0034, 'IceShock', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0003, 0006, 0006, 0056, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12733, 12733, 0036, 'IceBolt', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12734, 12734, 0045, 'ArrowHail', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0000, 0012, 0020, 0001, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12735, 12735, 0034, 'IceShock', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0003, 0006, 0006, 0056, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12736, 12736, 0035, 'Avalanche', 0, 0, 0, 0004, 0000, 0000, 0, 0000, 0100, 0003, 0008, 0015, 0056, 0000, 0, 0000, 0, 0000, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12740, 12740, 0078, 'LeftHook', 0, 0, 1, 0008, 0000, 0000, 0, 0000, 0100, 0000, 0006, 0000, 0000, 0000, 0, 0000, 0, 61624624, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12750, 12750, 0078, 'RightHook', 0, 0, 1, 0008, 0000, 0000, 0, 0000, 0100, 0000, 0006, 0000, 0000, 0000, 0, 0000, 0, 61624624, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12760, 12760, 0076, 'StraightFist', 0, 0, 1, 0008, 0000, 0000, 0, 0000, 0100, 0000, 0008, 0000, 0000, 0000, 0, 0000, 0, 61624624, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12770, 12770, 0084, 'Pounce', 1, 1, 0, 0004, 0000, 0000, 30030, 0000, 0100, 0000, 0005, 0020, 0000, 0000, 500, 3000, 60, 0900, 0000, 0, 0000, 0, 0000, 0, 0000, 0, 0000, 0000, 0000, 0001);
INSERT INTO `spells` VALUES (12771, 12770, 0084, 'Pounce', 1, 1, 0, 0004, 0001, 0000, 30035, 0000, 0100, 0000, 0005, 0020, 0000, 0000, 1000, 0070, 0, 0900, 0000, 0, 0000, 0, 0000, 0, 0050, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12773, 12770, 0084, 'Pounce', 1, 1, 0, 0004, 0003, 0000, 30045, 0000, 0100, 0000, 0005, 0020, 0000, 0000, 50000, 0090, 0, 0900, 0000, 0, 0000, 0, 0000, 0, 0050, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12774, 12770, 0084, 'Pounce', 1, 1, 0, 0004, 0004, 0000, 30050, 0000, 0100, 0000, 0005, 0020, 0000, 0000, 100000, 0100, 0, 0900, 0000, 0, 0000, 0, 0000, 0, 0050, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12775, 12770, 0084, 'Pounce', 1, 1, 0, 0004, 0005, 0000, 30055, 0000, 0100, 0000, 0005, 0020, 0000, 0000, 500000, 0110, 0, 0900, 0000, 0, 0000, 0, 0000, 0, 0050, 0, 0000, 0000, 0000, 0000);
INSERT INTO `spells` VALUES (12776, 12770, 0084, 'Pounce', 1, 1, 0, 0004, 0006, 0000, 30060, 0000, 0100, 0000, 0005, 0020, 0000, 0000, 0, 0000, 0, 0900, 0000, 0, 0000, 0, 0000, 0, 0050, 0, 0000, 0000, 0000, 0000);

-- ----------------------------
-- Table structure for status
-- ----------------------------
DROP TABLE IF EXISTS `status`;
CREATE TABLE `status`  (
  `id` int(4) UNSIGNED NOT NULL AUTO_INCREMENT,
  `EntityID` int(4) NOT NULL DEFAULT 0,
  `status` bigint(255) UNSIGNED NOT NULL DEFAULT 0,
  `time` bigint(16) NULL DEFAULT 0,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 8900 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of status
-- ----------------------------
INSERT INTO `status` VALUES (8897, 1000007, 10, 20160909061312);
INSERT INTO `status` VALUES (8899, 1000010, 9, 20160919083035);

-- ----------------------------
-- Table structure for subclasses
-- ----------------------------
DROP TABLE IF EXISTS `subclasses`;
CREATE TABLE `subclasses`  (
  `unique` int(36) UNSIGNED NOT NULL AUTO_INCREMENT,
  `id` int(36) UNSIGNED NOT NULL DEFAULT 0,
  `uid` int(36) UNSIGNED NOT NULL DEFAULT 1,
  `phase` int(36) UNSIGNED NOT NULL DEFAULT 1,
  `level` int(36) UNSIGNED NOT NULL DEFAULT 1,
  PRIMARY KEY (`unique`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 31323 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of subclasses
-- ----------------------------
INSERT INTO `subclasses` VALUES (31318, 1000001, 9, 3, 3);
INSERT INTO `subclasses` VALUES (31319, 1000002, 9, 1, 1);
INSERT INTO `subclasses` VALUES (31320, 1000007, 9, 1, 1);
INSERT INTO `subclasses` VALUES (31321, 1000010, 9, 1, 1);
INSERT INTO `subclasses` VALUES (31322, 1000013, 9, 9, 9);

-- ----------------------------
-- Table structure for teamarena
-- ----------------------------
DROP TABLE IF EXISTS `teamarena`;
CREATE TABLE `teamarena`  (
  `EntityID` int(10) UNSIGNED NOT NULL,
  `EntityName` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `ArenaPoints` int(10) UNSIGNED NULL DEFAULT 0,
  `ActivityPoints` int(10) UNSIGNED NULL DEFAULT 0,
  `TodayWin` int(10) UNSIGNED NULL DEFAULT 0,
  `TodayBattles` int(10) UNSIGNED NULL DEFAULT 0,
  `TotalWin` int(10) UNSIGNED NULL DEFAULT 0,
  `TotalLose` int(10) UNSIGNED NULL DEFAULT 0,
  `CurrentHonor` int(10) UNSIGNED NULL DEFAULT 0,
  `HistoryHonor` int(10) UNSIGNED NULL DEFAULT 0,
  `LastSeasonRank` int(10) UNSIGNED NULL DEFAULT 0,
  `Level` int(10) UNSIGNED NULL DEFAULT 0,
  `Class` int(10) UNSIGNED NULL DEFAULT 0,
  `ArenaPointFill` bigint(255) UNSIGNED NULL DEFAULT 0,
  `Model` bigint(255) UNSIGNED NULL DEFAULT 0,
  `LastSeasonArenaPoints` bigint(255) UNSIGNED NULL DEFAULT 0,
  `LastSeasonWin` int(10) UNSIGNED NULL DEFAULT 0,
  `LastSeasonLose` int(10) UNSIGNED NULL DEFAULT 0,
  PRIMARY KEY (`EntityID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of teamarena
-- ----------------------------
INSERT INTO `teamarena` VALUES (1000001, 'EgYCo', 4000, 0, 0, 0, 0, 0, 0, 0, 0, 140, 55, 0, 331003, 0, 0, 0);
INSERT INTO `teamarena` VALUES (1000010, 'MyMido', 4003, 0, 0, 0, 0, 0, 0, 0, 0, 140, 25, 0, 351003, 0, 0, 0);
INSERT INTO `teamarena` VALUES (1000009, 'Mid0', 4000, 0, 0, 0, 0, 0, 0, 0, 0, 140, 80, 0, 381003, 0, 0, 0);
INSERT INTO `teamarena` VALUES (1000013, '', 4000, 0, 0, 0, 0, 0, 52000, 0, 0, 140, 45, 0, 1391003, 0, 0, 0);
INSERT INTO `teamarena` VALUES (1000014, 'Alucard', 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 50, 0, 401003, 0, 0, 0);

-- ----------------------------
-- Table structure for teampk
-- ----------------------------
DROP TABLE IF EXISTS `teampk`;
CREATE TABLE `teampk`  (
  `UID` int(32) NOT NULL DEFAULT 0,
  `Avatar` int(32) NOT NULL DEFAULT 0,
  `Name` char(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `Mesh` int(32) NOT NULL DEFAULT 0,
  `Points` int(32) NOT NULL DEFAULT 0,
  `Postion` int(32) NOT NULL DEFAULT 0,
  `MyTitle` bigint(32) NOT NULL DEFAULT 0,
  PRIMARY KEY (`UID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of teampk
-- ----------------------------

-- ----------------------------
-- Table structure for titles
-- ----------------------------
DROP TABLE IF EXISTS `titles`;
CREATE TABLE `titles`  (
  `Id` bigint(20) NOT NULL DEFAULT 0,
  `Points` int(11) NOT NULL DEFAULT 500,
  `Data` longblob NULL,
  `Time` bigint(20) NULL DEFAULT 0,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of titles
-- ----------------------------
INSERT INTO `titles` VALUES (1000001, 0, 0x0B73171600017317160001731716000173171600017317160001731716000173171600017317160001731716000173171600017317160000, 0);
INSERT INTO `titles` VALUES (1000002, 0, 0xA3EC07EC0701A10FE8030171171400007217150000731716000074171700000100E80300E207010000D107020000D207030000D307040000D407050000D507060000D607070000E407E40700E507E50700E607E60700E707E70700E807E80700E907E907007517751700EC07EC0700ED07ED0700EE07EE0700EF07EF0700F007F00701F107F10701791779170077177717007817781700EA07EA0700EB07EB07007B177B1700F207F20700EC07EC07007417170000D6070700007317160000D5070600007317160000D5070600007417170000D6070700007317160000D5070600007317160000D507060000A10FE8030171171400007217150000731716000074171700000100E80300E207010000D107020000D207030000D307040000D407050000D507060000D607070000E407E40700E507E50700E607E60700E707E70700E807E80700E907E907007517751700EC07EC0700ED07ED0700EE07EE0700EF07EF0700F007F00700F107F10701791779170077177717007817781700EA07EA0700EB07EB07007B177B1700F207F20700EC07EC07007317160000D5070600007417170000D6070700007317160000D5070600007317160000D5070600007417170000D6070700007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007417170000D6070700007417170000D6070700007417170000D6070700007417170000D6070700007417170000D6070700007417170000D6070700007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007417170000D6070700007417170000D6070700007417170000D6070700007417170000D6070700007417170000D6070700007417170000D6070700007417170000D6070700007417170000D6070700007417170000D6070700007417170000D6070700007417170000D6070700007417170000D607070000, 0);
INSERT INTO `titles` VALUES (1000003, 0, 0x50EC07EC0700A10FE8030071171400017217150000731716000074171700000100E80300E207010001D107020001D207030000D307040000D407050000D507060000D607070000E407E40700E507E50700E607E60700E707E70700E807E80700E907E907007517751700EC07EC0700ED07ED0700EE07EE0700EF07EF0700F007F00700F107F10700791779170077177717007817781700EA07EA0700EB07EB07007B177B1700F207F20700EC07EC0700731716000073171600007417170000D6070700007417170000D6070700007417170000D6070700007417170000D6070700007417170000D6070700007417170000D6070700007417170000D6070700007417170000D6070700007417170000D6070700007417170000D6070700007417170000D6070700007417170000D6070700007417170000D6070700007417170000D60707000073171600007417170000D6070700007417170000D607070000731716000073171600007417170000D607070000731716000073171600007417170000D60707000073171600007317160000, 0);
INSERT INTO `titles` VALUES (1000004, 0, 0x01EC07EC0700, 0);
INSERT INTO `titles` VALUES (1000005, 0, 0x0AEC07EC0700D407050000D407050000D407050000D407050000D407050000D407050000D407050000D407050000D407050000, 0);
INSERT INTO `titles` VALUES (1000008, 0, 0x01EC07EC0700, 0);
INSERT INTO `titles` VALUES (1000006, 0, 0x23EC07EC0700A10FE8030071171400017217150000731716000074171700000100E80300E207010000D107020000D207030000D307040000D407050000D507060000D607070000E407E40700E507E50700E607E60700E707E70700E807E80700E907E907007517751700EC07EC0700ED07ED0700EE07EE0700EF07EF0700F007F00700F107F10700791779170077177717007817781700EA07EA0700EB07EB07007B177B1700F207F20700EC07EC0700, 0);
INSERT INTO `titles` VALUES (1000007, 0, 0x01EC07EC0700, 0);
INSERT INTO `titles` VALUES (1000013, 0, 0xE1EC07EC0700741717000074171700007417170000741717000073171600007317160000731716000073171600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D507060000741717000074171700007417170000741717000074171700007417170000741717000074171700007417170000741717000074171700007417170000A10FE8030071171400007217150000731716000074171700000100E80300E207010000D107020000D207030000D307040000D407050000D507060000D607070000E407E40700E507E50700E607E60700E707E70700E807E80700E907E907007517751700EC07EC0700ED07ED0700EE07EE0700EF07EF0700F007F00700F107F10700791779170077177717007817781700EA07EA0700EB07EB07007B177B1701F207F20700EC07EC07007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D50706000074171700007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007417170000741717000074171700007417170000741717000074171700007417170000741717000074171700007417170000741717000074171700007417170000741717000074171700007417170000741717000074171700007417170000741717000074171700007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D507060000D4070500007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D5070600007317160000D50706000074171700007417170000D6070700007417170000D6070700007417170000D6070700007317160000D5070600007317160000D5070600007317160000D5070600007417170000D607070000, 0);
INSERT INTO `titles` VALUES (1000014, 0, 0x01EC07EC0700, 0);

-- ----------------------------
-- Table structure for unions
-- ----------------------------
DROP TABLE IF EXISTS `unions`;
CREATE TABLE `unions`  (
  `ID` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  `GoldBricks` bigint(255) UNSIGNED NOT NULL DEFAULT 0,
  `UnionFund` bigint(255) UNSIGNED NOT NULL DEFAULT 0,
  `Name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `Title` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `LeaderName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `Bulletin` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `Declaration` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `LeaderUID` bigint(18) UNSIGNED NOT NULL DEFAULT 0,
  `CoreOfficals` varchar(999) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '0#0#0#0#0#0#0#0',
  `Harem` varchar(999) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '0#0#0#0#0#0#0#0#0#0',
  `Guards` varchar(999) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0',
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of unions
-- ----------------------------
INSERT INTO `unions` VALUES (2003, 0, 160, 'Sll', '', '1stTest', '', '', 1000001, '0#0#0#0#0#0#0#0', '0#0#0#0#0#0#0#0#0#0', '0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#0');

-- ----------------------------
-- Table structure for variablevault
-- ----------------------------
DROP TABLE IF EXISTS `variablevault`;
CREATE TABLE `variablevault`  (
  `entityid` int(18) NOT NULL,
  `data` longblob NULL,
  PRIMARY KEY (`entityid`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of variablevault
-- ----------------------------
INSERT INTO `variablevault` VALUES (0, 0x020000009562DD40EEB05E7E0100AAB4745F15ECAFCD02EA03000000000000);
INSERT INTO `variablevault` VALUES (1000001, 0x090000006E14A21CAAA34714040E496E66696E6974652074696D652EEF95279C097655AE02FFFFFFFFFFFFFFFFBE0C8ED6B94D08E805C466C50243D9D308E35B7C7349539D0E0083AB99C152F0995900A8AF75E6BC73503F020000000000000000C91766807C5E4A7C020000000000000000C3D7A5C4164CAB080200000000000000006B542A1ABD43DB69057E9056FFDBE14309);
INSERT INTO `variablevault` VALUES (1000002, 0x090000006E14A21CAAA34714040E496E66696E6974652074696D652EEF95279C097655AE02FFFFFFFFFFFFFFFFBE0C8ED6B94D08E805F8494ED4B4D5D308E35B7C7349539D0E0083AB99C152F0995900A8AF75E6BC73503F020000000000000000C91766807C5E4A7C020000000000000000C3D7A5C4164CAB080200000000000000006B542A1ABD43DB6905D94CF99345E94309);
INSERT INTO `variablevault` VALUES (1000003, 0x080000006E14A21CAAA34714040E496E66696E6974652074696D652EEF95279C097655AE02FFFFFFFFFFFFFFFFE35B7C7349539D0E0083AB99C152F0995900A8AF75E6BC73503F020000000000000000C3D7A5C4164CAB08020100000000000000C91766807C5E4A7C0200000000000000006B542A1ABD43DB6905137D1C89B4E94309);
INSERT INTO `variablevault` VALUES (1000004, 0x080000006E14A21CAAA34714040E496E66696E6974652074696D652EEF95279C097655AE02FFFFFFFFFFFFFFFFE35B7C7349539D0E0083AB99C152F0995900A8AF75E6BC73503F020000000000000000C91766807C5E4A7C020000000000000000C3D7A5C4164CAB080200000000000000006B542A1ABD43DB69051149AECEBEE94309);
INSERT INTO `variablevault` VALUES (1000005, 0x080000006E14A21CAAA34714040E496E66696E6974652074696D652EEF95279C097655AE02FFFFFFFFFFFFFFFFE35B7C7349539D0E0083AB99C152F0995900A8AF75E6BC73503F020000000000000000C3D7A5C4164CAB08020400000000000000C91766807C5E4A7C0200000000000000006B542A1ABD43DB69058ED893F65EEA4309);
INSERT INTO `variablevault` VALUES (1000006, 0x090000006E14A21CAAA34714040E496E66696E6974652074696D652EA8AF75E6BC73503F0200000000000000006B542A1ABD43DB6905C5E9D3D770EA4309C91766807C5E4A7C020000000000000000EF95279C097655AE02FFFFFFFFFFFFFFFFC3D7A5C4164CAB0800BE0C8ED6B94D08E805C5070C8043D9D308E35B7C7349539D0E0083AB99C152F0995900);
INSERT INTO `variablevault` VALUES (1000007, 0x080000006E14A21CAAA34714040E496E66696E6974652074696D652EEF95279C097655AE02FFFFFFFFFFFFFFFFE35B7C7349539D0E0083AB99C152F0995900A8AF75E6BC73503F020000000000000000C3D7A5C4164CAB0800C91766807C5E4A7C0200000000000000006B542A1ABD43DB6905C9ABE04262EE4309);
INSERT INTO `variablevault` VALUES (1000008, 0x080000006E14A21CAAA34714040E496E66696E6974652074696D652EEF95279C097655AE02FFFFFFFFFFFFFFFFE35B7C7349539D0E0083AB99C152F0995900A8AF75E6BC73503F020000000000000000C3D7A5C4164CAB08020000000000000000C91766807C5E4A7C0200000000000000006B542A1ABD43DB69059083D99467EE4309);
INSERT INTO `variablevault` VALUES (1000012, 0x00000000);
INSERT INTO `variablevault` VALUES (1000013, 0x09000000EF95279C097655AE02FFFFFFFFFFFFFFFF816AD7E51E2661EB006E14A21CAAA34714040E496E66696E6974652074696D652E83AB99C152F0995900A8AF75E6BC73503F020000000000000000E35B7C7349539D0E006B542A1ABD43DB6905F00B2DCFB26E4D09C3D7A5C4164CAB08020100000000000000C91766807C5E4A7C020000000000000000);
INSERT INTO `variablevault` VALUES (1000014, 0x08000000C3D7A5C4164CAB0800EF95279C097655AE02FFFFFFFFFFFFFFFF6E14A21CAAA34714040E496E66696E6974652074696D652EC91766807C5E4A7C020000000000000000E35B7C7349539D0E0083AB99C152F0995900A8AF75E6BC73503F0200000000000000006B542A1ABD43DB690555A90DDDD56F4D09);
INSERT INTO `variablevault` VALUES (1000015, 0x00000000);

-- ----------------------------
-- Table structure for way2heroes
-- ----------------------------
DROP TABLE IF EXISTS `way2heroes`;
CREATE TABLE `way2heroes`  (
  `UID` int(18) NOT NULL,
  `Name` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Way2Heroes` longblob NULL,
  PRIMARY KEY (`UID`) USING BTREE,
  UNIQUE INDEX `myIndex`(`UID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of way2heroes
-- ----------------------------
INSERT INTO `way2heroes` VALUES (1000014, 'Alucard', NULL);
INSERT INTO `way2heroes` VALUES (1000013, 'RAVEN', NULL);

SET FOREIGN_KEY_CHECKS = 1;
