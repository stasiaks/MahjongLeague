-- DROP SCHEMA mahjong;

CREATE SCHEMA mahjong AUTHORIZATION test_app;

-- DROP TYPE mahjong."_users";

CREATE TYPE mahjong."_users" (
	INPUT = array_in,
	OUTPUT = array_out,
	RECEIVE = array_recv,
	SEND = array_send,
	ANALYZE = array_typanalyze,
	ALIGNMENT = 8,
	STORAGE = any,
	CATEGORY = A,
	ELEMENT = mahjong.users,
	DELIMITER = ',');

-- DROP TYPE mahjong.users;

CREATE TYPE mahjong.users AS (
	id uuid,
	objectid varchar(50),
	"name" varchar(50),
	email varchar(250));
-- mahjong.users definition

-- Drop table

-- DROP TABLE mahjong.users;

CREATE TABLE mahjong.users (
	id uuid NOT NULL,
	objectid varchar(50) NULL,
	"name" varchar(50) NOT NULL,
	email varchar(250) NULL,
	CONSTRAINT users_pk PRIMARY KEY (id),
	CONSTRAINT users_un UNIQUE (objectid)
);

