CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE SCHEMA mahjong AUTHORIZATION test_app;

CREATE TABLE mahjong.users (
	id uuid NOT NULL DEFAULT uuid_generate_v1mc(),
	objectid varchar(50) NULL,
	"name" varchar(50) NOT NULL,
	email varchar(250) NULL,
	CONSTRAINT users_pk PRIMARY KEY (id),
	CONSTRAINT users_un UNIQUE (objectid)
);

CREATE TABLE mahjong.leagues (
	id uuid NOT NULL DEFAULT uuid_generate_v1mc(),
	"name" varchar(200) NOT NULL,
	description varchar(500) NULL,
	CONSTRAINT league_pk PRIMARY KEY (id)
);

CREATE TABLE mahjong.seasons (
	id uuid NOT NULL DEFAULT uuid_generate_v1mc(),
	"name" varchar(50) NOT NULL,
	date_from date NOT NULL,
	date_to date NULL,
	league_id uuid NOT NULL,
	CONSTRAINT seasons_pk PRIMARY KEY (id)
);
