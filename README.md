# strawberry-movies
Um pequeno crud de filmes desenvolvido com .Net 6 e MVC, para estudos.

# Sobre
O sistema conta com autenticação de usuário, onde apenas usuários administradores possuem o privilégio para gerenciar os filmes cadastrados. De início não haverá nenhum.
Para cadastrar um filme, primeiro precisa fazer manualmente o cadastro de generos e plataforma de streamings, pois são necessários para registrar os filmes.
Há campos de seleção, onde é possivel escolher mais de um Gênero e Streaming para os filmes, basta segurar a tecla CTRL
Não é possivel cadastrar usuários administradores, mas há um metodo pronto que faz o registro automatico de um, quando rodar o projeto, basta colocar na url do navegador a seguinte rota:
https://localhost:44350/UserAuthentication/RegisterAdmin,
Login: admin,
Senha Admin@123

# Configuração do projeto
O sistema está configurado para rodar com SQL Server e tem migrations configurado.
Após clonar o repositório e instalar as dependencias, é necessário ajustar a string de conexão com o banco de dados localizada no arquivo "appsettings.json", na raiz do projeto. criar e rodar as migrations.
No Visual Studio, basta ir em Ferramentas > Gerenciador de pacotes NuGet > Console do gerenciador de pacotes.
No terminal que abrir rodar 2 comandos: "add-migrations init" e depois "update-database"
Basta rodar o projeto e ele vai abrir por conta uma nova aba no navegador.
