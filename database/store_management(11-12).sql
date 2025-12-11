-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               8.4.4 - MySQL Community Server - GPL
-- Server OS:                    Win64
-- HeidiSQL Version:             12.10.0.7000
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dumping database structure for store_management
CREATE DATABASE IF NOT EXISTS `store_management` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `store_management`;

-- Dumping structure for table store_management.categories
CREATE TABLE IF NOT EXISTS `categories` (
  `category_id` int NOT NULL AUTO_INCREMENT,
  `category_name` varchar(100) NOT NULL,
  PRIMARY KEY (`category_id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table store_management.categories: ~5 rows (approximately)
INSERT INTO `categories` (`category_id`, `category_name`) VALUES
	(1, 'Đồ uống'),
	(2, 'Bánh kẹo'),
	(3, 'Gia vị'),
	(4, 'Đồ gia dụng'),
	(5, 'Mỹ phẩm');

-- Dumping structure for table store_management.customers
CREATE TABLE IF NOT EXISTS `customers` (
  `customer_id` int NOT NULL AUTO_INCREMENT,
  `account_id` int DEFAULT NULL,
  `name` varchar(100) NOT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `address` text,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`customer_id`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table store_management.customers: ~20 rows (approximately)
INSERT INTO `customers` (`customer_id`, `account_id`, `name`, `phone`, `email`, `address`, `created_at`) VALUES
	(1, NULL, 'Khách hàng 1', '0909000001', 'kh1@mail.com', 'Địa chỉ 1', '2025-10-15 06:32:15'),
	(2, NULL, 'Khách hàng 2', '0909000002', 'kh2@mail.com', 'Địa chỉ 2', '2025-10-15 06:32:15'),
	(3, NULL, 'Khách hàng 3', '0909000003', 'kh3@mail.com', 'Địa chỉ 3', '2025-10-15 06:32:15'),
	(4, NULL, 'Khách hàng 4', '0909000004', 'kh4@mail.com', 'Địa chỉ 4', '2025-10-15 06:32:15'),
	(5, NULL, 'Khách hàng 5', '0909000005', 'kh5@mail.com', 'Địa chỉ 5', '2025-10-15 06:32:15'),
	(6, NULL, 'Khách hàng 6', '0909000006', 'kh6@mail.com', 'Địa chỉ 6', '2025-10-15 06:32:15'),
	(7, NULL, 'Khách hàng 7', '0909000007', 'kh7@mail.com', 'Địa chỉ 7', '2025-10-15 06:32:15'),
	(8, NULL, 'Khách hàng 8', '0909000008', 'kh8@mail.com', 'Địa chỉ 8', '2025-10-15 06:32:15'),
	(9, NULL, 'Khách hàng 9', '0909000009', 'kh9@mail.com', 'Địa chỉ 9', '2025-10-15 06:32:15'),
	(10, NULL, 'Khách hàng 10', '0909000010', 'kh10@mail.com', 'Địa chỉ 10', '2025-10-15 06:32:15'),
	(11, NULL, 'Khách hàng 11', '0909000011', 'kh11@mail.com', 'Địa chỉ 11', '2025-10-15 06:32:15'),
	(12, NULL, 'Khách hàng 12', '0909000012', 'kh12@mail.com', 'Địa chỉ 12', '2025-10-15 06:32:15'),
	(13, NULL, 'Khách hàng 13', '0909000013', 'kh13@mail.com', 'Địa chỉ 13', '2025-10-15 06:32:15'),
	(14, NULL, 'Khách hàng 14', '0909000014', 'kh14@mail.com', 'Địa chỉ 14', '2025-10-15 06:32:15'),
	(15, NULL, 'Khách hàng 15', '0909000015', 'kh15@mail.com', 'Địa chỉ 15', '2025-10-15 06:32:15'),
	(16, NULL, 'Khách hàng 16', '0909000016', 'kh16@mail.com', 'Địa chỉ 16', '2025-10-15 06:32:15'),
	(17, NULL, 'Khách hàng 17', '0909000017', 'kh17@mail.com', 'Địa chỉ 17', '2025-10-15 06:32:15'),
	(18, NULL, 'Khách hàng 18', '0909000018', 'kh18@mail.com', 'Địa chỉ 18', '2025-10-15 06:32:15'),
	(19, NULL, 'Khách hàng 19', '0909000019', 'kh19@mail.com', 'Địa chỉ 19', '2025-10-15 06:32:15'),
	(20, NULL, 'Khách hàng 20', '0909000020', 'kh20@mail.com', 'Địa chỉ 20', '2025-10-15 06:32:15');

-- Dumping structure for table store_management.inventory
CREATE TABLE IF NOT EXISTS `inventory` (
  `inventory_id` int NOT NULL AUTO_INCREMENT,
  `product_id` int NOT NULL,
  `quantity` int DEFAULT '0',
  `updated_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`inventory_id`)
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table store_management.inventory: ~50 rows (approximately)
INSERT INTO `inventory` (`inventory_id`, `product_id`, `quantity`, `updated_at`) VALUES
	(1, 1, 25, '2025-10-15 06:32:55'),
	(2, 2, 169, '2025-10-15 06:32:55'),
	(3, 3, 77, '2025-10-15 06:32:55'),
	(4, 4, 169, '2025-10-15 06:32:55'),
	(5, 5, 90, '2025-10-15 06:32:55'),
	(6, 6, 105, '2025-10-15 06:32:55'),
	(7, 7, 125, '2025-10-15 06:32:55'),
	(8, 8, 37, '2025-10-15 06:32:55'),
	(9, 9, 74, '2025-10-15 06:32:55'),
	(10, 10, 149, '2025-10-15 06:32:55'),
	(11, 11, 69, '2025-10-15 06:32:55'),
	(12, 12, 23, '2025-10-15 06:32:55'),
	(13, 13, 46, '2025-10-15 06:32:55'),
	(14, 14, 144, '2025-10-15 06:32:55'),
	(15, 15, 134, '2025-10-15 06:32:55'),
	(16, 16, 182, '2025-10-15 06:32:55'),
	(17, 17, 99, '2025-10-15 06:32:55'),
	(18, 18, 72, '2025-10-15 06:32:55'),
	(19, 19, 128, '2025-10-15 06:32:55'),
	(20, 20, 123, '2025-10-15 06:32:55'),
	(21, 21, 155, '2025-10-15 06:32:55'),
	(22, 22, 78, '2025-10-15 06:32:55'),
	(23, 23, 166, '2025-10-15 06:32:55'),
	(24, 24, 117, '2025-10-15 06:32:55'),
	(25, 25, 168, '2025-10-15 06:32:55'),
	(26, 26, 197, '2025-10-15 06:32:55'),
	(27, 27, 36, '2025-10-15 06:32:55'),
	(28, 28, 145, '2025-10-15 06:32:55'),
	(29, 29, 61, '2025-10-15 06:32:55'),
	(30, 30, 139, '2025-10-15 06:32:55'),
	(31, 31, 47, '2025-10-15 06:32:55'),
	(32, 32, 154, '2025-10-15 06:32:55'),
	(33, 33, 194, '2025-10-15 06:32:55'),
	(34, 34, 41, '2025-10-15 06:32:55'),
	(35, 35, 154, '2025-10-15 06:32:55'),
	(36, 36, 71, '2025-10-15 06:32:55'),
	(37, 37, 49, '2025-10-15 06:32:55'),
	(38, 38, 165, '2025-10-15 06:32:55'),
	(39, 39, 73, '2025-10-15 06:32:55'),
	(40, 40, 176, '2025-10-15 06:32:55'),
	(41, 41, 41, '2025-10-15 06:32:55'),
	(42, 42, 34, '2025-10-15 06:32:55'),
	(43, 43, 175, '2025-10-15 06:32:55'),
	(44, 44, 59, '2025-10-15 06:32:55'),
	(45, 45, 198, '2025-10-15 06:32:55'),
	(46, 46, 106, '2025-10-15 06:32:55'),
	(47, 47, 99, '2025-10-15 06:32:55'),
	(48, 48, 55, '2025-10-15 06:32:55'),
	(49, 49, 62, '2025-10-15 06:32:55'),
	(50, 50, 33, '2025-10-15 06:32:55');

-- Dumping structure for table store_management.orders
CREATE TABLE IF NOT EXISTS `orders` (
  `order_id` int NOT NULL AUTO_INCREMENT,
  `customer_id` int DEFAULT NULL,
  `user_id` int DEFAULT NULL,
  `promo_id` int DEFAULT NULL,
  `order_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `status` enum('pending','paid','canceled') DEFAULT 'pending',
  `total_amount` decimal(10,2) DEFAULT NULL,
  `discount_amount` decimal(10,2) DEFAULT '0.00',
  PRIMARY KEY (`order_id`)
) ENGINE=InnoDB AUTO_INCREMENT=46 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table store_management.orders: ~30 rows (approximately)
INSERT INTO `orders` (`order_id`, `customer_id`, `user_id`, `promo_id`, `order_date`, `status`, `total_amount`, `discount_amount`) VALUES
	(1, 5, 3, 5, '2025-10-15 06:32:55', 'paid', 1292330.00, 100000.00),
	(2, 17, 3, NULL, '2025-10-15 06:32:55', 'paid', 1731608.00, 0.00),
	(3, 8, 3, NULL, '2025-10-15 06:32:55', 'paid', 720782.00, 0.00),
	(4, 20, 3, 5, '2025-10-15 06:32:55', 'paid', 21686.00, 21686.00),
	(5, 1, 2, NULL, '2025-10-15 06:32:55', 'paid', 94180.00, 0.00),
	(6, 5, 3, 2, '2025-10-15 06:32:55', 'paid', 3888671.00, 100000.00),
	(7, 9, 3, 4, '2025-10-15 06:32:55', 'paid', 512594.00, 102518.80),
	(8, 11, 3, 3, '2025-10-15 06:32:55', 'paid', 1715029.00, 171502.90),
	(9, 11, 3, NULL, '2025-10-15 06:32:55', 'paid', 2484051.00, 0.00),
	(10, 11, 3, 2, '2025-10-15 06:32:55', 'paid', 1070239.00, 100000.00),
	(11, 20, 3, NULL, '2025-10-15 06:32:55', 'paid', 1532741.00, 0.00),
	(12, 10, 2, NULL, '2025-10-15 06:32:55', 'paid', 1785354.00, 0.00),
	(13, 10, 3, 2, '2025-10-15 06:32:55', 'paid', 1588276.00, 100000.00),
	(14, 6, 2, 2, '2025-10-15 06:32:55', 'paid', 2896096.00, 50000.00),
	(15, 10, 2, 3, '2025-10-15 06:32:55', 'paid', 186000.00, 27900.00),
	(16, 10, 2, 5, '2025-10-15 06:32:55', 'paid', 1024090.00, 50000.00),
	(17, 19, 3, NULL, '2025-10-15 06:32:55', 'paid', 467148.00, 0.00),
	(18, 10, 2, NULL, '2025-10-15 06:32:55', 'paid', 394342.00, 0.00),
	(19, 8, 3, 4, '2025-10-15 06:32:55', 'paid', 1965637.00, 294845.55),
	(20, 3, 3, NULL, '2025-10-15 06:32:55', 'paid', 2889813.00, 0.00),
	(21, 9, 2, NULL, '2025-10-15 06:32:55', 'paid', 2288406.00, 0.00),
	(22, 17, 3, NULL, '2025-10-15 06:32:55', 'paid', 331008.00, 0.00),
	(23, 6, 3, 1, '2025-10-15 06:32:55', 'paid', 2154851.00, 323227.65),
	(24, 1, 3, 1, '2025-10-15 06:32:55', 'paid', 1138686.00, 170802.90),
	(25, 2, 2, 5, '2025-10-15 06:32:55', 'paid', 393847.00, 100000.00),
	(26, 15, 3, 1, '2025-10-15 06:32:55', 'paid', 260658.00, 52131.60),
	(27, 4, 2, NULL, '2025-10-15 06:32:55', 'paid', 933199.00, 0.00),
	(28, 16, 2, NULL, '2025-10-15 06:32:55', 'paid', 2609123.00, 0.00),
	(29, 4, 3, 4, '2025-10-15 06:32:55', 'paid', 2406292.00, 481258.40),
	(30, 1, 3, NULL, '2025-10-15 06:32:55', 'paid', 2912134.00, 0.00);

-- Dumping structure for table store_management.order_items
CREATE TABLE IF NOT EXISTS `order_items` (
  `order_item_id` int NOT NULL AUTO_INCREMENT,
  `order_id` int DEFAULT NULL,
  `product_id` int DEFAULT NULL,
  `quantity` int NOT NULL,
  `price` decimal(10,2) NOT NULL,
  `subtotal` decimal(10,2) NOT NULL,
  PRIMARY KEY (`order_item_id`)
) ENGINE=InnoDB AUTO_INCREMENT=107 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table store_management.order_items: ~94 rows (approximately)
INSERT INTO `order_items` (`order_item_id`, `order_id`, `product_id`, `quantity`, `price`, `subtotal`) VALUES
	(1, 1, 23, 2, 31265.00, 62530.00),
	(2, 1, 5, 2, 205683.00, 411366.00),
	(3, 1, 47, 1, 477948.00, 477948.00),
	(4, 1, 25, 2, 170243.00, 340486.00),
	(5, 2, 39, 1, 447059.00, 447059.00),
	(6, 2, 14, 1, 51108.00, 51108.00),
	(7, 2, 46, 3, 411147.00, 1233441.00),
	(8, 3, 18, 3, 202167.00, 606501.00),
	(9, 3, 34, 1, 44219.00, 44219.00),
	(10, 3, 26, 3, 23354.00, 70062.00),
	(11, 4, 24, 2, 10843.00, 21686.00),
	(12, 5, 9, 1, 94180.00, 94180.00),
	(13, 6, 18, 3, 186886.00, 560658.00),
	(14, 6, 22, 2, 199267.00, 398534.00),
	(15, 6, 42, 3, 215726.00, 647178.00),
	(16, 6, 17, 3, 474268.00, 1422804.00),
	(17, 6, 20, 3, 286499.00, 859497.00),
	(18, 7, 8, 2, 256297.00, 512594.00),
	(19, 8, 42, 1, 355116.00, 355116.00),
	(20, 8, 43, 2, 129224.00, 258448.00),
	(21, 8, 31, 3, 367155.00, 1101465.00),
	(22, 9, 17, 2, 48755.00, 97510.00),
	(23, 9, 12, 2, 381904.00, 763808.00),
	(24, 9, 43, 2, 167445.00, 334890.00),
	(25, 9, 19, 3, 429281.00, 1287843.00),
	(26, 10, 25, 1, 232635.00, 232635.00),
	(27, 10, 1, 2, 245362.00, 490724.00),
	(28, 10, 23, 2, 127233.00, 254466.00),
	(29, 10, 49, 2, 46207.00, 92414.00),
	(30, 11, 3, 2, 347879.00, 695758.00),
	(31, 11, 23, 3, 130215.00, 390645.00),
	(32, 11, 4, 1, 64761.00, 64761.00),
	(33, 11, 33, 1, 240159.00, 240159.00),
	(34, 11, 7, 1, 141418.00, 141418.00),
	(35, 12, 40, 2, 455428.00, 910856.00),
	(36, 12, 46, 2, 75412.00, 150824.00),
	(37, 12, 34, 2, 189856.00, 379712.00),
	(38, 12, 25, 3, 114654.00, 343962.00),
	(39, 13, 24, 2, 143251.00, 286502.00),
	(40, 13, 23, 2, 381347.00, 762694.00),
	(41, 13, 18, 2, 179146.00, 358292.00),
	(42, 13, 9, 2, 90394.00, 180788.00),
	(43, 14, 24, 2, 327016.00, 654032.00),
	(44, 14, 2, 1, 403478.00, 403478.00),
	(45, 14, 27, 3, 404474.00, 1213422.00),
	(46, 14, 4, 2, 312582.00, 625164.00),
	(47, 15, 18, 1, 105328.00, 105328.00),
	(48, 15, 27, 2, 17303.00, 34606.00),
	(49, 15, 50, 2, 23033.00, 46066.00),
	(50, 16, 15, 1, 43160.00, 43160.00),
	(51, 16, 16, 2, 18541.00, 37082.00),
	(52, 16, 44, 1, 492698.00, 492698.00),
	(53, 16, 41, 1, 451150.00, 451150.00),
	(54, 17, 42, 1, 467148.00, 467148.00),
	(55, 18, 30, 1, 64334.00, 64334.00),
	(56, 18, 11, 1, 178454.00, 178454.00),
	(57, 18, 20, 3, 50518.00, 151554.00),
	(58, 19, 16, 1, 89280.00, 89280.00),
	(59, 19, 23, 3, 404655.00, 1213965.00),
	(60, 19, 11, 2, 331196.00, 662392.00),
	(61, 20, 49, 1, 367325.00, 367325.00),
	(62, 20, 32, 2, 264392.00, 528784.00),
	(63, 20, 19, 3, 345903.00, 1037709.00),
	(64, 20, 17, 2, 392028.00, 784056.00),
	(65, 20, 19, 1, 171939.00, 171939.00),
	(66, 21, 11, 3, 227666.00, 682998.00),
	(67, 21, 25, 2, 436122.00, 872244.00),
	(68, 21, 48, 1, 340400.00, 340400.00),
	(69, 21, 10, 2, 58482.00, 116964.00),
	(70, 21, 4, 2, 137900.00, 275800.00),
	(71, 22, 40, 2, 165504.00, 331008.00),
	(72, 23, 1, 2, 296698.00, 593396.00),
	(73, 23, 16, 3, 384657.00, 1153971.00),
	(74, 23, 40, 3, 135828.00, 407484.00),
	(75, 24, 3, 3, 379562.00, 1138686.00),
	(76, 25, 9, 1, 22063.00, 22063.00),
	(77, 25, 16, 2, 185892.00, 371784.00),
	(78, 26, 47, 2, 130329.00, 260658.00),
	(79, 27, 37, 1, 448581.00, 448581.00),
	(80, 27, 23, 1, 484618.00, 484618.00),
	(81, 28, 20, 3, 357837.00, 1073511.00),
	(82, 28, 34, 1, 161219.00, 161219.00),
	(83, 28, 1, 3, 458131.00, 1374393.00),
	(84, 29, 28, 1, 485514.00, 485514.00),
	(85, 29, 7, 3, 487044.00, 1461132.00),
	(86, 29, 42, 1, 235885.00, 235885.00),
	(87, 29, 38, 1, 223761.00, 223761.00),
	(88, 30, 25, 1, 426943.00, 426943.00),
	(89, 30, 11, 3, 130209.00, 390627.00),
	(90, 30, 5, 2, 73116.00, 146232.00),
	(91, 30, 46, 2, 272220.00, 544440.00),
	(92, 30, 23, 3, 467964.00, 1403892.00),
	(93, 1, 1, 1, 1.00, 1.00),
	(94, 1, 1, 1, 1.00, 1.00);

-- Dumping structure for table store_management.payments
CREATE TABLE IF NOT EXISTS `payments` (
  `payment_id` int NOT NULL AUTO_INCREMENT,
  `order_id` int NOT NULL,
  `amount` decimal(10,2) NOT NULL,
  `payment_method` enum('cash','card','bank_transfer','e-wallet') DEFAULT 'cash',
  `payment_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `status` enum('pending','success','failed') CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT 'pending',
  `transaction_ref` bigint DEFAULT NULL,
  PRIMARY KEY (`payment_id`)
) ENGINE=InnoDB AUTO_INCREMENT=41 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table store_management.payments: ~30 rows (approximately)
INSERT INTO `payments` (`payment_id`, `order_id`, `amount`, `payment_method`, `payment_date`, `status`, `transaction_ref`) VALUES
	(1, 1, 1192330.00, 'cash', '2025-10-15 06:32:55', NULL, NULL),
	(2, 2, 1731608.00, 'e-wallet', '2025-10-15 06:32:55', NULL, NULL),
	(3, 3, 720782.00, 'e-wallet', '2025-10-15 06:32:55', NULL, NULL),
	(4, 4, 0.00, 'card', '2025-10-15 06:32:55', NULL, NULL),
	(5, 5, 94180.00, 'cash', '2025-10-15 06:32:55', NULL, NULL),
	(6, 6, 3788671.00, 'cash', '2025-10-15 06:32:55', NULL, NULL),
	(7, 7, 410075.20, 'e-wallet', '2025-10-15 06:32:55', NULL, NULL),
	(8, 8, 1543526.10, 'cash', '2025-10-15 06:32:55', NULL, NULL),
	(9, 9, 2484051.00, 'cash', '2025-10-15 06:32:55', NULL, NULL),
	(10, 10, 970239.00, 'card', '2025-10-15 06:32:55', NULL, NULL),
	(11, 11, 1532741.00, 'e-wallet', '2025-10-15 06:32:55', NULL, NULL),
	(12, 12, 1785354.00, 'card', '2025-10-15 06:32:55', NULL, NULL),
	(13, 13, 1488276.00, 'card', '2025-10-15 06:32:55', NULL, NULL),
	(14, 14, 2846096.00, 'cash', '2025-10-15 06:32:55', NULL, NULL),
	(15, 15, 158100.00, 'card', '2025-10-15 06:32:55', NULL, NULL),
	(16, 16, 974090.00, 'cash', '2025-10-15 06:32:55', NULL, NULL),
	(17, 17, 467148.00, 'cash', '2025-10-15 06:32:55', NULL, NULL),
	(18, 18, 394342.00, 'e-wallet', '2025-10-15 06:32:55', NULL, NULL),
	(19, 19, 1670791.45, 'card', '2025-10-15 06:32:55', NULL, NULL),
	(20, 20, 2889813.00, 'card', '2025-10-15 06:32:55', NULL, NULL),
	(21, 21, 2288406.00, 'cash', '2025-10-15 06:32:55', NULL, NULL),
	(22, 22, 331008.00, 'e-wallet', '2025-10-15 06:32:55', NULL, NULL),
	(23, 23, 1831623.35, 'cash', '2025-10-15 06:32:55', NULL, NULL),
	(24, 24, 967883.10, 'e-wallet', '2025-10-15 06:32:55', NULL, NULL),
	(25, 25, 293847.00, 'cash', '2025-10-15 06:32:55', NULL, NULL),
	(26, 26, 208526.40, 'cash', '2025-10-15 06:32:55', NULL, NULL),
	(27, 27, 933199.00, 'cash', '2025-10-15 06:32:55', NULL, NULL),
	(28, 28, 2609123.00, 'card', '2025-10-15 06:32:55', NULL, NULL),
	(29, 29, 1925033.60, 'cash', '2025-10-15 06:32:55', NULL, NULL),
	(30, 30, 2912134.00, 'card', '2025-10-15 06:32:55', NULL, NULL);

-- Dumping structure for table store_management.products
CREATE TABLE IF NOT EXISTS `products` (
  `product_id` int NOT NULL AUTO_INCREMENT,
  `category_id` int DEFAULT NULL,
  `supplier_id` int DEFAULT NULL,
  `product_name` varchar(100) NOT NULL,
  `barcode` varchar(50) DEFAULT NULL,
  `price` decimal(10,2) NOT NULL,
  `unit` varchar(20) DEFAULT 'pcs',
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `product_img` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`product_id`),
  UNIQUE KEY `barcode` (`barcode`)
) ENGINE=InnoDB AUTO_INCREMENT=55 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table store_management.products: ~50 rows (approximately)
INSERT INTO `products` (`product_id`, `category_id`, `supplier_id`, `product_name`, `barcode`, `price`, `unit`, `created_at`, `product_img`) VALUES
	(1, 2, 1, 'Coca Cola lon', '8900000000001', 314838.00, 'hộp', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765284151/ef4a44fb0c806a130d74efca2ee6ce87_kgh3h1.jpg'),
	(2, 1, 3, 'Pepsi lon', '8900000000002', 114807.00, 'cái', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765288978/thung-24-lon-nuoc-ngot-pepsi-cola-320ml-202405140910328596_lmm8uf.jpg'),
	(3, 3, 3, 'Trà Xanh 0 độ', '8900000000003', 415725.00, 'tuýp', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765289014/fgjiol_202410140914265805_dys7ub.jpg'),
	(4, 2, 1, 'Sting dâu', '8900000000004', 351670.00, 'cái', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765289054/nuoc-tang-luc-sting-dau-pet-330ml_202509291516185862_tttvmj.jpg'),
	(5, 3, 2, 'Red Bull', '8900000000005', 402179.00, 'lon', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765289082/cdy78owspj4htoh3uok6.jpg'),
	(6, 2, 2, 'Bánh Oreo', '8900000000006', 209283.00, 'chai', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765289700/banh-quy-nhan-kem-vani-oreo-goi-1196g-202301311524404090_nkmpgb.jpg'),
	(7, 5, 3, 'Bánh Chocopie', '8900000000007', 212528.00, 'lon', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765289788/banh-choco-pie-396g-12-cai-201903151505091563_faakte.jpg'),
	(8, 1, 2, 'Kẹo Alpenliebe', '8900000000008', 34313.00, 'lon', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765291265/keo-sua-caramen-alpenliebe-goi-1155g-202304050953174731_mkkrbv.webp'),
	(9, 5, 1, 'Kẹo bạc hà', '8900000000009', 316289.00, 'cái', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765291360/1658197096_drjrkt.jpg'),
	(10, 1, 2, 'Socola KitKat', '8900000000010', 139959.00, 'chai', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765291402/BANH-XOP-PHU-SOCOLA-KITKAT-GO_CC_81I-102G_gw9sde.jpg'),
	(11, 5, 1, 'Nước mắm Nam Ngư', '8900000000011', 51792.00, 'chai', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765291456/nuoc-mam-nam-ngu-nhan-vang-14-do-dam-chai-650ml-202212051137093617_ikmbyp.png'),
	(12, 2, 2, 'Nước tương Maggi', '8900000000012', 462539.00, 'lon', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765291537/nuoc-tuong-maggi-dam-dac-12x700ml_202509240921311156_pa8sqd.jpg'),
	(13, 5, 3, 'Muối i-ốt', '8900000000013', 173302.00, 'cái', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765292158/DKS07496-scaled_ccwnlj.jpg'),
	(14, 1, 1, 'Bột ngọt Ajinomoto', '8900000000014', 443069.00, 'cái', '2025-10-15 06:44:35', 'https://cdn.tgdd.vn/Products/Images/2806/77080/bhx/bot-ngot-ajinomoto-goi-1kg-201912111050340356.jpg'),
	(15, 2, 2, 'Dầu ăn Tường An', '8900000000015', 281354.00, 'tuýp', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765292257/dau-thuc-vat-tuong-an-cooking-oil-chai-1-lit-202105201322134036_cywuzt.jpg'),
	(16, 2, 1, 'Nồi cơm điện', '8900000000016', 405347.00, 'hộp', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765292388/nag0146-1500x1500_sbjkzr.jpg'),
	(17, 1, 3, 'Ấm siêu tốc', '8900000000017', 113087.00, 'chai', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765292501/thiet-ke-chua-co-ten-61_ric0rb.png'),
	(18, 3, 2, 'Quạt máy', '8900000000018', 69968.00, 'hộp', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765292951/b1610-03_47f799bf849f446e86f1d12e04f3c45d_master_bpqcad.jpg'),
	(19, 4, 1, 'Bếp gas mini', '8900000000019', 416845.00, 'lon', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765293043/218728-min-600x600_i8oyjq.jpg'),
	(20, 3, 3, 'Máy xay sinh tố', '8900000000020', 334564.00, 'hộp', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765293100/ble876302_v7rzrj.jpg'),
	(21, 1, 1, 'Sữa rửa mặt Hazeline', '8900000000021', 188475.00, 'lon', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765337362/0010169_sua-rua-mat-hazeline-yen-mach-va-dau-tam-100g_cbxr99.jpg'),
	(22, 4, 1, 'Kem dưỡng da Pond\'s', '8900000000022', 413840.00, 'hộp', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765337741/MDM4ODE2MzcgKDEp_oibfdd.jpg'),
	(23, 3, 3, 'Dầu gội Sunsilk', '8900000000023', 158950.00, 'tuýp', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765337766/dau-goi-sunsilk-650g-1_pawfri.jpg'),
	(24, 4, 2, 'Sữa tắm Dove', '8900000000024', 336928.00, 'chai', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765337783/google-shopping-sua-tam-duong-the-dove-duong-am-chuyen-sau-900g-1667533561_lsg4lv.jpg'),
	(25, 1, 1, 'Nước hoa Romano', '8900000000025', 352508.00, 'cái', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765337821/picture1_yx9euh.png'),
	(26, 1, 1, 'Cà phê G7', '8900000000026', 201228.00, 'lon', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765337838/hop-18-768x768-1_wmy7gw.jpg'),
	(27, 2, 1, 'Trà Lipton', '8900000000027', 38039.00, 'cái', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765337860/tra-lipton-nhan-vang-25-goi-x-2g_9f5aaafb485945c2bbfbb6c8064d722e_1024x1024_vhzoob.jpg'),
	(28, 2, 3, 'Sữa Vinamilk', '8900000000028', 252845.00, 'chai', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765337914/sua-tuoi-tiet-trung-khong-duong-vinamilk-100-sua-tuoi-hop-1-lit-202403281355125054_z7crkx.jpg'),
	(29, 3, 1, 'Sữa TH True Milk', '8900000000029', 35278.00, 'hộp', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765337941/sua-tuoi-th-true-milk-co-duong-hop-180ml-4-2_kgf5mv.jpg'),
	(30, 3, 2, 'Nước suối Lavie', '8900000000030', 331637.00, 'lon', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765337983/lavie-5l_imb1fh.jpg'),
	(31, 5, 3, 'Khăn giấy Tempo', '8900000000031', 102525.00, 'chai', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765338008/khan-giay-rut-tempo-huong-bac-ha-4-lop-goi-90-to-16cm-x-195cm-202105261324403812_kxg7yo.jpg'),
	(32, 4, 3, 'Giấy vệ sinh Pulppy', '8900000000032', 495429.00, 'chai', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765338041/giay-ve-sinh-pulppy-10-cuon_rvts5z.jpg'),
	(33, 3, 2, 'Bình nước Lock&Lock', '8900000000033', 354771.00, 'gói', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765338073/binh-giu-nhiet-locklock-lhc6180blk_qxb0z4.jpg'),
	(34, 2, 1, 'Hộp nhựa Tupperware', '8900000000034', 297415.00, 'cái', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765338097/bo-hop-dong-gen-ii-tupperware-450ml-22-1000x1000_iowxeu.jpg'),
	(35, 1, 3, 'Dao Inox', '8900000000035', 47523.00, 'hộp', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765338116/dao-inox-cao-cap_cbfqkc.jpg'),
	(36, 3, 1, 'Bàn chải Colgate', '8900000000036', 136417.00, 'chai', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765338134/P11365_1_m5pb5a.jpg'),
	(37, 2, 2, 'Kem đánh răng P/S', '8900000000037', 93713.00, 'hộp', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765338155/1_8on6-lg_v04b4v.jpg'),
	(38, 2, 3, 'Nước súc miệng Listerine', '8900000000038', 223906.00, 'gói', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765338242/total_care_750ml_front-vi-vn_cgbkty.jpg'),
	(39, 1, 2, 'Bông tẩy trang', '8900000000039', 317819.00, 'tuýp', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765338191/ipek-min_f63a80c2eac5423ab7d0d5c6ff386f8c_1024x1024_qhmmdu.png'),
	(40, 4, 1, 'Khẩu trang 3M', '8900000000040', 464252.00, 'gói', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765338284/khau-trang-3m-8210-n95-loc-it-nhat-95-bui-min-virus-vi-khan_5921_anh1_pjbpz7.png'),
	(41, 3, 1, 'Bánh mì sandwich', '8900000000041', 279350.00, 'cái', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765338310/banh-sandwich-staff-huu-nghi-food-goi-275g-202111221632134506_j7fzk2.jpg'),
	(42, 5, 2, 'Mì gói Hảo Hảo', '8900000000042', 9413.00, 'hộp', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765338332/slide-1_202410151354441725_pg6u8m.jpg'),
	(43, 1, 2, 'Mì Omachi', '8900000000043', 26616.00, 'hộp', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765338397/mi_sot_bo_ham_cjki7c.jpg'),
	(44, 5, 2, 'Bún khô', '8900000000044', 350911.00, 'gói', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765338404/bun-kho-400grx20-2-700x467_nxyjgw.jpg'),
	(45, 3, 1, 'Phở ăn liền', '8900000000045', 407779.00, 'tuýp', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765338444/pho-bo-nho-mai-mai-to-73g-202303141550235848_wzgjyj.jpg'),
	(46, 1, 1, 'Nước ngọt Sprite', '8900000000046', 230083.00, 'hộp', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765338452/thung-24-lon-nuoc-ngot-sprite-huong-chanh-320ml-202407121623260163_quotxk.jpg'),
	(47, 1, 3, 'Trà sữa đóng chai', '8900000000047', 15130.00, 'cái', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765338504/tra-sua-khong-do-machiato-chai-268ml-24-chai-2-700x467_teudej.jpg'),
	(48, 3, 3, 'Snack Oishi', '8900000000048', 43415.00, 'cái', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765338544/snack-tom-cay-dac-biet-oishi-goi-39g-202211281008305246_wgnflz.png'),
	(49, 4, 2, 'Snack Lay\'s', '8900000000049', 83536.00, 'tuýp', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765338637/Lays-MAX-3D-PackagingNori10M-front-scaled_h5gli4.png'),
	(50, 1, 2, 'Kẹo dẻo Haribo', '8900000000050', 328680.00, 'cái', '2025-10-15 06:44:35', 'https://res.cloudinary.com/dl5lkru7o/image/upload/v1765338661/keo-deo-haribo-goldbears-huong-trai-cay-80g-201902221047320504_d4mkts.jpg');

-- Dumping structure for table store_management.promotions
CREATE TABLE IF NOT EXISTS `promotions` (
  `promo_id` int NOT NULL AUTO_INCREMENT,
  `promo_code` varchar(50) NOT NULL,
  `description` varchar(255) DEFAULT NULL,
  `discount_type` enum('percent','fixed') NOT NULL,
  `discount_value` decimal(10,2) NOT NULL,
  `start_date` date NOT NULL,
  `end_date` date NOT NULL,
  `min_order_amount` decimal(10,2) DEFAULT '0.00',
  `usage_limit` int DEFAULT '0',
  `used_count` int DEFAULT '0',
  `status` enum('active','inactive') DEFAULT 'active',
  PRIMARY KEY (`promo_id`),
  UNIQUE KEY `promo_code` (`promo_code`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table store_management.promotions: ~5 rows (approximately)
INSERT INTO `promotions` (`promo_id`, `promo_code`, `description`, `discount_type`, `discount_value`, `start_date`, `end_date`, `min_order_amount`, `usage_limit`, `used_count`, `status`) VALUES
	(1, 'SALE10', 'Giảm 10% cho mọi đơn hàng', 'percent', 10.00, '2025-01-01', '2025-12-31', 0.00, 0, 0, 'active'),
	(2, 'FREESHIP50K', 'Giảm 50,000 cho đơn từ 300,000 trở lên', 'fixed', 50000.00, '2025-03-01', '2025-12-31', 300000.00, 500, 0, 'active'),
	(3, 'NEWUSER', 'Giảm 20% cho khách hàng mới', 'percent', 20.00, '2025-01-01', '2025-06-30', 0.00, 1, 0, 'active'),
	(4, 'SUMMER15', 'Giảm 15% mùa hè', 'percent', 15.00, '2025-06-01', '2025-08-31', 50000.00, 1000, 0, 'active'),
	(5, 'VIP100K', 'Giảm 100,000 cho đơn từ 1 triệu', 'fixed', 100000.00, '2025-01-01', '2025-12-31', 1000000.00, 200, 0, 'active');

-- Dumping structure for table store_management.suppliers
CREATE TABLE IF NOT EXISTS `suppliers` (
  `supplier_id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `address` text,
  PRIMARY KEY (`supplier_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table store_management.suppliers: ~3 rows (approximately)
INSERT INTO `suppliers` (`supplier_id`, `name`, `phone`, `email`, `address`) VALUES
	(1, 'Công ty ABC', '0909123456', 'abc@gmail.com', 'Hà Nội'),
	(2, 'Công ty XYZ', '0912123456', 'xyz@gmail.com', 'TP HCM'),
	(3, 'Công ty 123', '0933123456', '123@gmail.com', 'Đà Nẵng');

-- Dumping structure for table store_management.users
CREATE TABLE IF NOT EXISTS `users` (
  `user_id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(50) NOT NULL,
  `password` varchar(255) NOT NULL,
  `full_name` varchar(100) DEFAULT NULL,
  `role` enum('admin','staff','user') CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT 'staff',
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `username` (`username`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table store_management.users: ~4 rows (approximately)
INSERT INTO `users` (`user_id`, `username`, `password`, `full_name`, `role`, `created_at`) VALUES
	(1, 'admin', 'AQAAAAIAAYagAAAAEOWD9MInHhtrc22QmhAzuxP1AAgrZLBadRbvPblt2BOdYfuyVOlzJu2MgZMj1lav7Q==', 'Quản trị viên', 'admin', '2025-10-15 06:32:15'),
	(2, 'staff01', '123456', 'Nguyễn Văn A', 'staff', '2025-10-15 06:32:15'),
	(3, 'staff02', '123456', 'Lê Thị B', 'user', '2025-10-15 06:32:15'),
	(4, 'ducem', 'AQAAAAIAAYagAAAAEOWD9MInHhtrc22QmhAzuxP1AAgrZLBadRbvPblt2BOdYfuyVOlzJu2MgZMj1lav7Q==', NULL, 'user', '2025-12-03 12:43:48');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
