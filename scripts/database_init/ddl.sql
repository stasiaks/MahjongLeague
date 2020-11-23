CREATE SCHEMA mahjong AUTHORIZATION test_app;

CREATE TABLE mahjong.users (
	id uuid NOT NULL,
	objectid varchar(50) NULL,
	"name" varchar(50) NOT NULL,
	email varchar(250) NULL,
	CONSTRAINT users_pk PRIMARY KEY (id),
	CONSTRAINT users_un UNIQUE (objectid)
);

