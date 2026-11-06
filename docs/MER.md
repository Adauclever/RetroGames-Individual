# Modelo de dados
Sete tabelas: condição e perfil com identificadores fixos; plataforma, gênero, jogo e usuário com identificadores gerados; jogo-gênero com chave composta.
Plataforma e condição têm relação 1:N com jogo. Jogo e gênero têm relação N:N por jogo-gênero. Perfil tem relação 1:N com usuário.
O script `scripts/database/01_estrutura.sql` é o dicionário físico completo. Somente `ds_jogo` aceita NULL.
Como a especificação não define tamanhos de textos, foram adotados: nomes 100 (jogo 150), descrições de domínio 50, e-mail 254 e hash 500 caracteres; descrição de jogo TEXT. Identificadores e anos são INT. Preço DECIMAL(10,2), datas DATETIME em UTC e situação BOOLEAN.
Sem CHECK, triggers, functions ou ações referenciais especiais. As regras de negócio serão implementadas no Service nas próximas aulas.
