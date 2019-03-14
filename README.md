# ZX-Ventures Challenge Solution

#Introdução

Projeto para atender ao desafio ZX-Ventures Backend

#Informações

Tendo em vista que foi solicitado suporte multiplataforma e sabendo que o ambiente na ZX é totalemnte Linux resolvi adicionar suporte ao Docker com imagens Linux

Para Rodar:

1-) Baixe o projeto no repositorio: https://dev.azure.com/loretorafa/_git/ZX_Challenge

2-) No seu prompt de comando navegue até pasta ZX_Challenge.Api rode os seguintes comandos:

docker build -t zxchallenge -f Dockerfile ..

docker run -d -p 8080:80 --name api zxchallenge

3-) Acesse http://localhost:8080 em seu browser


Para Publicar:

Aqui optei por uma abordagem DevOps com integração e entregas contínuas.

Um commit no branch master está configurado para gerar e publicar a imagem no repositório do Azure, e sob demanda posso replicar esta imagem para "Produção" em: https://pdvapi.azurewebsites.net


#Considerações Finais

Adorei desenvolver este projeto, super desafiador, pois nunca havia trabalhado com os tipos espaciais do SQLServer e sempre imaginei que o MongoDB seria a única opção lógica para estes tipos de dados.

Também apanhei bastante para configurar a parte de DevOps no Azure trabalhando com containeres Docker, Azure Container Registry e Container Instances,
evitando utilizar os recursos de Publish do Visual Studio com os quais estou acostumado, mas valeu muito o aprendizado.

Único pesar foi que perdi muito tempo nesta parte e poderia ter utilizado para desenvolver um controller GraphQL, quem sabe em breve :)

Espero que gostem tanto quanto eu, quaisquer dúvidas estou disponível.



