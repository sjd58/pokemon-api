IF DB_ID('test_pokemon_db_name') IS NOT NULL
BEGIN
	ALTER DATABASE test_pokemon_db_name SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE test_pokemon_db_name;
END

CREATE DATABASE test_pokemon_db_name;