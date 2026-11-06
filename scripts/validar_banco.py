"""Verificações reais do banco individual. Execute com Docker Desktop iniciado."""
from pathlib import Path
import subprocess

raiz = Path(__file__).resolve().parents[1]
def executar(sql, erro_esperado=None):
    resultado = subprocess.run(['docker','compose','exec','-T','database','sh','-c',
        'MYSQL_PWD="$MYSQL_ROOT_PASSWORD" mysql -uroot --default-character-set=utf8mb4 --batch retrogames_individual'],
        input=sql, text=True, encoding='utf-8', capture_output=True, cwd=raiz)
    if erro_esperado:
        assert resultado.returncode != 0 and f'ERROR {erro_esperado}' in resultado.stderr, resultado.stderr
        return f'Erro {erro_esperado} confirmado'
    assert resultado.returncode == 0, resultado.stderr
    return resultado.stdout.strip()

print(executar('SHOW TABLES; SELECT COUNT(*) AS plataformas FROM tb_plataforma; SELECT COUNT(*) AS generos FROM tb_genero; SELECT COUNT(*) AS jogos FROM tb_jogo; SELECT COUNT(*) AS usuarios FROM tb_usuario;'))
assert executar('SELECT COUNT(*) FROM tb_jogo;').splitlines()[-1]=='20'
assert executar('SELECT COUNT(*) FROM tb_usuario;').splitlines()[-1]=='0'
assert executar('SELECT COUNT(*) FROM tb_jogo j LEFT JOIN tb_jogo_genero g ON g.co_jogo=j.co_seq_jogo WHERE g.co_jogo IS NULL;').splitlines()[-1]=='0'
assert executar('SELECT COUNT(*) FROM tb_jogo WHERE st_ativo=FALSE;').splitlines()[-1]=='1'
assert executar('SELECT COUNT(*) FROM tb_jogo WHERE qt_estoque=0;').splitlines()[-1]=='1'
executar((raiz/'scripts/consultas/01_catalogo.sql').read_text(encoding='utf-8'))
testes = [
    ('FK de plataforma', "INSERT INTO tb_jogo(no_jogo,nu_ano_lancamento,vl_preco,qt_estoque,dt_cadastro,co_plataforma,co_condicao_jogo) VALUES('Teste',1990,10,1,UTC_TIMESTAMP(),999999,2)",1452),
    ('Nome duplicado', "INSERT INTO tb_plataforma(no_plataforma,no_fabricante,nu_ano_lancamento) VALUES('NES','Nintendo',1983)",1062),
    ('Exclusão com dependentes', 'DELETE FROM tb_plataforma WHERE co_seq_plataforma=1',1451),
    ('Nome obrigatório', "INSERT INTO tb_jogo(no_jogo,nu_ano_lancamento,vl_preco,qt_estoque,dt_cadastro,co_plataforma,co_condicao_jogo) VALUES(NULL,1990,10,1,UTC_TIMESTAMP(),1,2)",1048),
    ('Preço zero aceito', "INSERT INTO tb_jogo(no_jogo,nu_ano_lancamento,vl_preco,qt_estoque,dt_cadastro,co_plataforma,co_condicao_jogo) VALUES('Teste preço zero',1990,0,1,UTC_TIMESTAMP(),1,2)",None),
]
anotacoes=[]
for nome,sql,erro in testes:
    resultado = executar('START TRANSACTION;\n'+sql+';\nROLLBACK;',erro)
    anotacoes.append(f'-- {nome}: {resultado if erro else "aceito; revertido por ROLLBACK"}.\nSTART TRANSACTION;\n{sql};\nROLLBACK;')
    print(nome, 'OK')
(raiz/'scripts/consultas/02_integridade.sql').write_text('-- Execute os testes individualmente no Workbench. Em erro, execute ROLLBACK.\n'+'\n\n'.join(anotacoes)+'\n',encoding='utf-8')
assert executar('SELECT COUNT(*) FROM tb_jogo;').splitlines()[-1]=='20'
assert executar('SELECT COUNT(*) FROM tb_plataforma;').splitlines()[-1]=='6'
print('Consultas e integridade verificadas; dados de teste revertidos.')
