#!/bin/bash
set -e

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL
    CREATE USER test_app;
    CREATE DATABASE mahjong;
    GRANT ALL PRIVILEGES ON DATABASE mahjong TO test_app;
EOSQL
