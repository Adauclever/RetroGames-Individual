-- Execute os testes individualmente no Workbench. Em erro, execute ROLLBACK.
-- FK de plataforma: Erro 1452 confirmado.
START TRANSACTION;
INSERT INTO tb_jogo(no_jogo,nu_ano_lancamento,vl_preco,qt_estoque,dt_cadastro,co_plataforma,co_condicao_jogo) VALUES('Teste',1990,10,1,UTC_TIMESTAMP(),999999,2);
ROLLBACK;

-- Nome duplicado: Erro 1062 confirmado.
START TRANSACTION;
INSERT INTO tb_plataforma(no_plataforma,no_fabricante,nu_ano_lancamento) VALUES('NES','Nintendo',1983);
ROLLBACK;

-- Exclusão com dependentes: Erro 1451 confirmado.
START TRANSACTION;
DELETE FROM tb_plataforma WHERE co_seq_plataforma=1;
ROLLBACK;

-- Nome obrigatório: Erro 1048 confirmado.
START TRANSACTION;
INSERT INTO tb_jogo(no_jogo,nu_ano_lancamento,vl_preco,qt_estoque,dt_cadastro,co_plataforma,co_condicao_jogo) VALUES(NULL,1990,10,1,UTC_TIMESTAMP(),1,2);
ROLLBACK;

-- Preço zero aceito: aceito; revertido por ROLLBACK.
START TRANSACTION;
INSERT INTO tb_jogo(no_jogo,nu_ano_lancamento,vl_preco,qt_estoque,dt_cadastro,co_plataforma,co_condicao_jogo) VALUES('Teste preço zero',1990,0,1,UTC_TIMESTAMP(),1,2);
ROLLBACK;
