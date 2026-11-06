USE retrogames_individual;
INSERT INTO tb_condicao_jogo VALUES (1,'Novo'),(2,'Usado'),(3,'Recondicionado');
INSERT INTO tb_perfil VALUES (1,'Administrador'),(2,'Cliente');
INSERT INTO tb_plataforma VALUES
(1,'NES','Nintendo',1983),(2,'Super Nintendo','Nintendo',1990),
(3,'Mega Drive','Sega',1988),(4,'Nintendo 64','Nintendo',1996),
(5,'PlayStation','Sony',1994),(6,'Atari 2600','Atari',1977);
INSERT INTO tb_genero VALUES (1,'Plataforma'),(2,'Aventura'),(3,'RPG'),(4,'Luta'),
(5,'Corrida'),(6,'Beat em up'),(7,'Ação'),(8,'Estratégia');
INSERT INTO tb_jogo
(no_jogo,nu_ano_lancamento,co_plataforma,vl_preco,qt_estoque,st_ativo,dt_cadastro,co_condicao_jogo) VALUES
('Super Mario Bros.',1985,1,50.00,0,TRUE,UTC_TIMESTAMP(),2),
('The Legend of Zelda',1986,1,55.00,3,FALSE,UTC_TIMESTAMP(),2),
('Metroid',1986,1,60.00,3,TRUE,UTC_TIMESTAMP(),2),
('Super Mario Bros. 3',1988,1,65.00,3,TRUE,UTC_TIMESTAMP(),2),
('Mega Man 2',1988,1,70.00,3,TRUE,UTC_TIMESTAMP(),2),
('Castlevania',1986,1,75.00,3,TRUE,UTC_TIMESTAMP(),2),
('Super Mario World',1990,2,80.00,3,TRUE,UTC_TIMESTAMP(),2),
('Super Metroid',1994,2,85.00,3,TRUE,UTC_TIMESTAMP(),2),
('Chrono Trigger',1995,2,90.00,3,TRUE,UTC_TIMESTAMP(),2),
('Donkey Kong Country',1994,2,95.00,3,TRUE,UTC_TIMESTAMP(),2),
('Street Fighter II',1992,2,100.00,3,TRUE,UTC_TIMESTAMP(),2),
('F-Zero',1990,2,105.00,3,TRUE,UTC_TIMESTAMP(),2),
('Sonic the Hedgehog',1991,3,110.00,3,TRUE,UTC_TIMESTAMP(),2),
('Streets of Rage 2',1992,3,115.00,3,TRUE,UTC_TIMESTAMP(),2),
('Golden Axe',1989,3,120.00,3,TRUE,UTC_TIMESTAMP(),2),
('Shining Force',1992,3,125.00,3,TRUE,UTC_TIMESTAMP(),2),
('Sonic the Hedgehog 2',1992,3,130.00,3,TRUE,UTC_TIMESTAMP(),2),
('Super Mario 64',1996,4,135.00,3,TRUE,UTC_TIMESTAMP(),2),
('Mario Kart 64',1996,4,140.00,3,TRUE,UTC_TIMESTAMP(),2),
('Final Fantasy VII',1997,5,145.00,3,TRUE,UTC_TIMESTAMP(),2);
INSERT INTO tb_jogo_genero VALUES
(1,1),(1,7),(2,2),(2,7),(3,2),(3,7),(4,1),(4,7),(5,1),(5,7),(6,2),(6,7),(7,1),(7,7),(8,2),(8,7),(9,3),(9,7),(10,1),(10,7),(11,4),(11,7),(12,5),(12,7),(13,1),(13,7),(14,6),(14,7),(15,6),(15,7),(16,3),(16,7),(17,1),(17,7),(18,1),(18,7),(19,5),(19,7),(20,3),(20,7);
