USE retrogames_individual;
-- Exercícios SQL da Aula 1; os Repositories da Aula 2 não utilizam JOIN.
SELECT j.*,p.no_plataforma,c.ds_condicao_jogo FROM tb_jogo j
JOIN tb_plataforma p ON p.co_seq_plataforma=j.co_plataforma
JOIN tb_condicao_jogo c ON c.co_condicao_jogo=j.co_condicao_jogo
WHERE j.st_ativo=TRUE ORDER BY j.no_jogo;
SET @id_jogo=1; SET @id_genero=1;
SELECT g.* FROM tb_genero g JOIN tb_jogo_genero jg ON jg.co_genero=g.co_seq_genero WHERE jg.co_jogo=@id_jogo;
SELECT j.* FROM tb_jogo j JOIN tb_jogo_genero jg ON jg.co_jogo=j.co_seq_jogo WHERE jg.co_genero=@id_genero;
SELECT p.no_plataforma,COUNT(j.co_seq_jogo) AS quantidade FROM tb_plataforma p
LEFT JOIN tb_jogo j ON j.co_plataforma=p.co_seq_plataforma GROUP BY p.co_seq_plataforma,p.no_plataforma;
SELECT * FROM tb_jogo WHERE no_jogo LIKE '%o%' ORDER BY no_jogo LIMIT 5 OFFSET 5;
SELECT @@hostname AS servidor,@@port AS porta,CURRENT_USER() AS usuario,DATABASE() AS banco;
