-- Database: ComputerService

-- DROP DATABASE IF EXISTS "ComputerService";

CREATE DATABASE "ComputerService"
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Russian_Russia.1251'
    LC_CTYPE = 'Russian_Russia.1251'
    LOCALE_PROVIDER = 'libc'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;





CREATE TABLE users (
    login VARCHAR(50) PRIMARY KEY,
    password_hash VARCHAR(255) NOT NULL,
    email VARCHAR(100) NOT NULL
);

CREATE TABLE user_privileges (
    user_login VARCHAR(50) NOT NULL,
    privilege_name VARCHAR(100) NOT NULL,
    PRIMARY KEY (user_login, privilege_name),
    FOREIGN KEY (user_login) REFERENCES users(login) ON DELETE CASCADE
);

CREATE TABLE pages (
    id SERIAL PRIMARY KEY
);

CREATE TABLE page_translations (
    content TEXT NOT NULL,
    lang_code VARCHAR(10) NOT NULL,
	page_id INTEGER NOT NULL,
	PRIMARY KEY (page_id, lang_code),
	FOREIGN KEY (page_id) REFERENCES pages(id) ON DELETE CASCADE
);

CREATE TABLE news (
    id SERIAL PRIMARY KEY,
    date DATE NOT NULL,
    image VARCHAR(255)
);

CREATE TABLE news_translations (
	title VARCHAR(200) NOT NULL,
    content TEXT,
	lang_code VARCHAR(10) NOT NULL,
	news_id INTEGER NOT NULL,
	PRIMARY KEY (news_id, lang_code),
	FOREIGN KEY (news_id) REFERENCES news(id) ON DELETE CASCADE
);

CREATE TABLE categories (
    id SERIAL PRIMARY KEY
);

CREATE TABLE category_translations (
	name VARCHAR(150) NOT NULL,
	lang_code VARCHAR(10) NOT NULL,
	category_id INTEGER NOT NULL,
	PRIMARY KEY (category_id, lang_code),
	FOREIGN KEY (category_id) REFERENCES categories(id) ON DELETE CASCADE
);

CREATE TABLE products (
    id SERIAL PRIMARY KEY,
    price DECIMAL(10,2) NOT NULL,
    category_id INTEGER NOT NULL,
    FOREIGN KEY (category_id) REFERENCES categories(id),
	visible BOOLEAN DEFAULT true
);

CREATE TABLE product_translations (
	name VARCHAR(200) NOT NULL,
    description TEXT,
	lang_code VARCHAR(10) NOT NULL,
	product_id INTEGER NOT NULL,
	PRIMARY KEY (product_id, lang_code),
	FOREIGN KEY (product_id) REFERENCES products(id) ON DELETE CASCADE
);

CREATE TABLE product_images (
    id SERIAL PRIMARY KEY,
    product_id INTEGER NOT NULL,
    image_url VARCHAR(255) NOT NULL,
    FOREIGN KEY (product_id) REFERENCES products(id) ON DELETE CASCADE
);

CREATE TABLE orders (
    id SERIAL PRIMARY KEY,
    date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    customer_name VARCHAR(150) NOT NULL,
    phone VARCHAR(30) NOT NULL,
    email VARCHAR(100) NOT NULL,
    payment_method VARCHAR(100),
    delivery_method VARCHAR(100),
    delivery_address VARCHAR(255),
    status VARCHAR(20) DEFAULT 'new',
    total DECIMAL(10,2)
);

CREATE TABLE order_items (
    id SERIAL PRIMARY KEY,
    order_id INTEGER NOT NULL,
    product_id INTEGER NOT NULL,
    quantity INTEGER NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (order_id) REFERENCES orders(id) ON DELETE CASCADE,
    FOREIGN KEY (product_id) REFERENCES products(id)
);

CREATE TABLE reviews (
    id SERIAL PRIMARY KEY,
    author_name VARCHAR(100) NOT NULL,
    date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    text TEXT NOT NULL,
    rating INTEGER CHECK (rating BETWEEN 1 AND 5)
);

CREATE TABLE support_messages (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL,
    message TEXT NOT NULL,
    date TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);