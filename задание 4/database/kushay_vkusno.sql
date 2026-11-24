SET NAMES utf8mb4;
SET SESSION sql_mode = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION,NO_BACKSLASH_ESCAPES';

CREATE DATABASE IF NOT EXISTS `isp-44_ilchenko_dm_kushay_vkusno` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE `isp-44_ilchenko_dm_kushay_vkusno`;

CREATE TABLE IF NOT EXISTS `roles` (
    `id` INT AUTO_INCREMENT PRIMARY KEY,
    `name` VARCHAR(50) NOT NULL UNIQUE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `users` (
    `id` INT AUTO_INCREMENT PRIMARY KEY,
    `full_name` VARCHAR(150) NOT NULL,
    `role_id` INT NOT NULL,
    `login` VARCHAR(100) NOT NULL UNIQUE,
    `password` VARCHAR(100) NOT NULL,
    CONSTRAINT `fk_users_role` FOREIGN KEY (`role_id`) REFERENCES `roles` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `categories` (
    `id` INT AUTO_INCREMENT PRIMARY KEY,
    `name` VARCHAR(100) NOT NULL UNIQUE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `dishes` (
    `id` INT AUTO_INCREMENT PRIMARY KEY,
    `name` VARCHAR(150) NOT NULL UNIQUE,
    `price` DECIMAL(10, 2) NOT NULL CHECK (`price` >= 0),
    `category_id` INT NOT NULL,
    `description` TEXT NOT NULL,
    `image_path` VARCHAR(255) NULL,
    CONSTRAINT `fk_dishes_category` FOREIGN KEY (`category_id`) REFERENCES `categories` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `order_statuses` (
    `id` INT AUTO_INCREMENT PRIMARY KEY,
    `name` VARCHAR(50) NOT NULL UNIQUE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `orders` (
    `id` INT AUTO_INCREMENT PRIMARY KEY,
    `user_id` INT NOT NULL,
    `order_date` DATETIME NOT NULL,
    `table_number` INT NOT NULL CHECK (`table_number` > 0),
    `status_id` INT NOT NULL,
    `details` TEXT NULL,
    CONSTRAINT `fk_orders_user` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON UPDATE CASCADE,
    CONSTRAINT `fk_orders_status` FOREIGN KEY (`status_id`) REFERENCES `order_statuses` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `order_items` (
    `order_id` INT NOT NULL,
    `dish_id` INT NOT NULL,
    `quantity` INT NOT NULL CHECK (`quantity` > 0),
    `price` DECIMAL(10, 2) NOT NULL CHECK (`price` >= 0),
    PRIMARY KEY (`order_id`, `dish_id`),
    CONSTRAINT `fk_order_items_order` FOREIGN KEY (`order_id`) REFERENCES `orders` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT `fk_order_items_dish` FOREIGN KEY (`dish_id`) REFERENCES `dishes` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

START TRANSACTION;

INSERT INTO `roles` (`id`, `name`) VALUES
(1, 'Официант'),
(2, 'Администратор')
ON DUPLICATE KEY UPDATE `name`=VALUES(`name`);

INSERT INTO `order_statuses` (`id`, `name`) VALUES
(1, 'Новый'),
(2, 'Готовится'),
(3, 'Завершен'),
(4, 'Отказ')
ON DUPLICATE KEY UPDATE `name`=VALUES(`name`);

INSERT INTO `users` (`id`, `full_name`, `role_id`, `login`, `password`) VALUES
(1, 'Данилова Лина Адольфовна', 1, 'm4ic8j5qgstw@gmail.com', '2L6KZG'),
(2, 'Иванов Павел Макарович', 1, 'd43zfg9tlsyv@gmail.com', 'uzWC67'),
(3, 'Поляков Наум Борисович', 1, '8ohgisf6k45w@outlook.com', '8ntwUp'),
(4, 'Фадеев Антон Владимирович', 1, 'hi1brwj46czx@mail.com', 'YOyhfR'),
(5, 'Гаврилов Корнелий Давидович', 2, 'fvkbcamhlj52@gmail.com', 'RSbvHv'),
(6, 'Соловьёва Полина Парфеньевна', 1, '9qxnce8jwruv@gmail.com', 'rwVDh9'),
(7, 'Костина Алина Станиславовна', 1, 'dotiex942p1r@gmail.com', 'LdNyos'),
(8, 'Егоров Марк Тихонович', 1, 'n0bmi2h1xral@tutanota.com', 'gynQMT'),
(9, 'Григорьева Мелиана Георгиевна', 1, 'sfm3t278kdvz@yahoo.com', 'AtnDjr'),
(10, 'Михеева Валерия Филипповна', 2, 'ilb8rdut0v7e@mail.com', 'JlFRCZ')
ON DUPLICATE KEY UPDATE `full_name`=VALUES(`full_name`), `role_id`=VALUES(`role_id`), `password`=VALUES(`password`);

INSERT INTO `categories` (`id`, `name`) VALUES
(1, 'Десерты'),
(2, 'Пицца'),
(3, 'Первые блюда'),
(4, 'Паста'),
(5, 'Вторые блюда')
ON DUPLICATE KEY UPDATE `name`=VALUES(`name`);

INSERT INTO `dishes` (`id`, `name`, `price`, `category_id`, `description`, `image_path`) VALUES
(1, 'Фруктовое ассорти', 395.00, 1, 'Ассорти из апельсина, тропического киви, яблока, винограда, банана.', 'Fruit platter.jpg'),
(2, 'Пицца Цезарь', 440.00, 2, 'Грудка копчёная, томаты черри, салат листовой, соус Цезарь, сыр.', 'Pizza Caesar.jpg'),
(3, 'Ароматная Уха', 250.00, 3, 'Рыбный бульон, филе сёмги, филе судака, картофель, лук, помидор', 'Fish soup.jpg'),
(4, 'Феттучини с тигровыми креветками', 305.00, 4, 'Паста Феттучини, тигровые креветки, сливочный соус, сыр Пармезан', 'Fettuccine.jpg'),
(5, 'Картофельные шарики', 115.00, 4, 'Картофель, яйца, панировочные сухари, растительное масло, соль', NULL),
(6, 'Лосось со спаржей в сливочно-икорном соусе', 620.00, 5, 'Филе сёмги, сливочный соус, спаржа.', 'Salmon.jpg')
ON DUPLICATE KEY UPDATE `price`=VALUES(`price`), `description`=VALUES(`description`), `image_path`=VALUES(`image_path`);

INSERT INTO `orders` (`id`, `user_id`, `order_date`, `table_number`, `status_id`, `details`) VALUES
(1, 2, '2022-11-30 13:42:00', 2, 1, NULL),
(2, 3, '2022-12-04 14:34:00', 1, 1, NULL),
(3, 4, '2022-12-11 16:58:00', 4, 3, NULL),
(4, 1, '2022-12-27 10:27:00', 2, 3, 'Меньше растительного масла'),
(5, 8, '2022-12-27 16:29:00', 3, 4, NULL),
(6, 6, '2023-01-15 17:18:00', 7, 2, NULL),
(7, 7, '2023-01-15 17:22:00', 7, 1, NULL),
(8, 3, '2023-01-21 18:58:00', 8, 2, 'Без цитрусов'),
(9, 8, '2023-01-27 14:01:00', 2, 3, 'Уха без лука'),
(10, 9, '2023-02-05 10:04:00', 6, 3, NULL),
(11, 1, '2023-02-17 09:33:00', 8, 4, NULL)
ON DUPLICATE KEY UPDATE `table_number`=VALUES(`table_number`), `status_id`=VALUES(`status_id`), `details`=VALUES(`details`);

INSERT INTO `order_items` (`order_id`, `dish_id`, `quantity`, `price`) VALUES
(1, 2, 1, 440.00),
(2, 5, 4, 115.00),
(3, 2, 1, 440.00),
(3, 1, 2, 395.00),
(4, 5, 1, 115.00),
(5, 6, 2, 620.00),
(6, 4, 5, 305.00),
(7, 3, 3, 250.00),
(8, 1, 1, 395.00),
(9, 3, 2, 250.00),
(9, 5, 1, 115.00),
(10, 4, 2, 305.00),
(11, 3, 1, 250.00)
ON DUPLICATE KEY UPDATE `quantity`=VALUES(`quantity`), `price`=VALUES(`price`);

COMMIT;
