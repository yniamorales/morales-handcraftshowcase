-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               10.4.32-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win64
-- HeidiSQL Version:             12.17.0.7270
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

-- Dumping data for table moonlight_db.tbl_artisan: ~0 rows (approximately)
INSERT INTO `tbl_artisan` (`ArtisanId`, `UserId`, `artisanBio`, `contactNum`, `artisanStatus`, `artisanName`) VALUES
	(1, 2, 'Whimsical Handmade Crafts', '09219496060', 'active', 'Nia\'s Whimsical Shop'),
	(2, 7, NULL, NULL, 'active', 'Olivia Rodrigo'),
	(3, 8, 'Pink Pony Crafts is a dreamy little world of rhinestones, ribbon, disco colors, and unapologetic self-expression — inspired by late-night pop anthems, small-town drama, queer joy, and the magic of becoming exactly who you are. Every piece is crafted with camp, chaos, and a touch of heartbreak: from sparkly trinkets and charms to statement décor made to feel like a pop song you can hold in your hands.', '230724589', 'active', 'Pink Pony Crafts');

-- Dumping data for table moonlight_db.tbl_product: ~6 rows (approximately)
INSERT INTO `tbl_product` (`ProductId`, `ArtisanId`, `ProductName`, `ProductDesc`, `ProductImgUrl`, `ProductStatus`) VALUES
	(1, 1, 'Button Headbands', 'button headbands imnida', '/Content/ProductImages/16c0a9de-9afa-4d80-9749-399994e7a979.jpg', 'approved'),
	(4, 2, 'Fuzzy Wire Flower Bouquet (Purple)', 'Fuzzy wire flower bouquet made from colorful chenille stems, handcrafted into soft, fluffy flowers with detailed petals and leaves. Lightweight, long-lasting, and perfect for gifts, room décor, or special occasions. Wrapped neatly for a charming bouquet presentation.\r\n\r\nCONTACT US:\r\ncontact number: +63 900 000 0000\r\ninstagram app: @xxxxxx', '/Content/ProductImages/6ab93586-3100-42f9-be5c-7e87696e9827.jpg', 'approved'),
	(5, 1, 'Fairy Garden Sun Catcher', 'Fairy Garden Sun Catcher is a whimsical handmade décor piece designed to catch sunlight and create sparkling reflections. Made with crystals, beads, flowers, and fairy-inspired accents, it adds magic to windows, gardens, patios, and indoor spaces, perfect for fairycore lovers. It brings a dreamy, calming glow to any setting.\r\n\r\nFB: xxxx | IG: xxxx | TikTok: xxxx | Contact: +639 000 000 0000', '/Content/ProductImages/032a7793-6f67-42b0-acd1-3b5c984988a2.jpg', 'approved'),
	(6, 3, 'Pink Pony Club Acrylic Earrings', 'Handmade with glitter, attitude, and a little pop-star drama, the Pink Pony Club Acrylic Earrings are bold statement pieces inspired by disco dreams and unapologetic self-expression. Lightweight yet eye-catching, these earrings add the perfect sparkle to concerts, parties, or everyday glam. Designed for the dreamers, dancers, and heartbreak queens who were born to stand out. ✨💖🐴\r\n\r\nIG/TikTok: @pinkponycrafts\r\nContact: xxxxxxxxx', '/Content/ProductImages/d09b34ce-feae-4ad2-882b-011ef31c765c.jpg', 'approved'),
	(7, 1, 'Celestial Glow Art Bottles', 'Hand-painted and filled with warm fairy lights, the Celestial Glow Art Bottles turn ordinary glass into luminous works of art. Each bottle is uniquely designed with vibrant colors and intricate details inspired by nature, fantasy, and dreamy landscapes. Perfect as bedroom décor, desk lights, or cozy night accents, these glowing creations bring a soft magical atmosphere to any space. Every piece is handmade with creativity and a touch of wonder. \r\n\r\nIG: @lumilorebottles\r\nContact: xxxxxxx', '/Content/ProductImages/dee47da1-408a-4597-b353-554ea4d96034.jpg', 'pending'),
	(9, 3, 'Felt Photocard Holders', 'Felt Photocard Holders (can customize) Message me for customization: instagram: @veenyuuh contact number: 307438273', '/Content/ProductImages/ed3a2dbd-9b97-472b-9c25-f89ce991b2a0.jpg', 'pending');

-- Dumping data for table moonlight_db.tbl_user: ~9 rows (approximately)
INSERT INTO `tbl_user` (`UserId`, `username`, `password`, `name`, `user_role`, `status`, `created_at`) VALUES
	(1, 'yniamorales', '123@Ynia', 'Ynia Morales', 'artisan', 'active', '0000-00-00 00:00:00'),
	(2, 'veenyuuh', '123456', 'Nia Morales', 'artisan', 'active', '0000-00-00 00:00:00'),
	(3, 'yniamoralesADM', 'Morales@123', 'Ynia Admin', 'admin', 'active', '0000-00-00 00:00:00'),
	(4, 'nanamikento', 'nanamin', 'Kento Nanami', 'user', 'active', '2026-05-16 00:23:29'),
	(6, 'taylorswift', 'taylorswift', 'Taylor Batumbakal Swift', 'user', 'active', '2026-05-16 00:22:49'),
	(7, 'oliviarodrigo', 'oliviarodrigo', 'Olivia Rodrigo', 'artisan', 'active', '0000-00-00 00:00:00'),
	(8, 'chappellroan', 'chappell', 'Chappell Roan', 'artisan', 'active', '0000-00-00 00:00:00'),
	(9, 'madisonbeer', 'madison', 'Madison Beer', 'user', 'active', '0000-00-00 00:00:00'),
	(10, 'ishalicuanan', 'laquisha', 'Laquisha Licuanan', 'user', 'active', '0000-00-00 00:00:00');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
