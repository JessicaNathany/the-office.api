# The Office API <br>

![image](https://user-images.githubusercontent.com/11943572/233072545-0fad64bf-c397-4d43-8eb7-84c08304d003.png)
<br/>

<p align="center">
  <img src="https://img.shields.io/badge/Framework-dotnet-blue"/> 
  <img src="https://img.shields.io/badge/Framework%20version-dotnet%206-blue"/>
  <img src="https://img.shields.io/badge/Language-C%23-blue"/> 
  <img src="https://img.shields.io/badge/Status-development-green"/>  
   <img src=" https://img.shields.io/badge/Status-development-green"/>  
</p>



 <h4 align="center"> 
	🚧  Project 🚀 em construção...  🚧
 </h4>

Este é um .NET 6 Web API que fornece pontos finais para acessar e gerenciar dados relacionados ao programa de TV The Office.<br/>
A API permite que os usuários recuperem informações sobre personagens, episódios e citações do programa.<br/><br/>


## Documentação da API 📝 <br/>
Os endpoints da API é documentada usando Swagger

Abra o navegador em http://localhost:5000/swagger.
Isto exibirá a interface de usuário Swagger, que fornece uma interface amigável para explorar a API 
pontos finais. A API fornece os seguintes pontos finais:<br/>

- GET /characters - retrieves a list of all characters in The Office.
- GET /characters/{id} - retrieves a specific character by ID.
- GET /episodes - retrieves a list of all episodes in The Office.
- GET /episodes/{id} - retrieves a specific episode by ID.
- GET /quotes/characters/{id} - retrieves a list of all better frases for specific character in The Office.<br/><br/>


1. **Presentation Layer**
  - Nome: the-office.api<br/>
  - Descrição: Esta camada é responsável por expor os endpoinds da API para o mundo externo  e receber requisições HTTP<br/>
    processar e retornar as respostas. Esta é nesta camada que a maioria das configurações API é feita. <br/>
    
2. **Application Layer**
  - Nome: the-office.application<br/>
  - Descrição: A camada de aplicação é responsável por alojar as regras comerciais da aplicação, <br/>
    utiliza os serviços prestados pelas outras camadas para realizar as operações necessárias. Está nesta camada <br/>
    que os casos de uso da aplicação sejam implementados. <br/>
    
3. **Domain Layer**
  - Nome: the-office.domain<br/>
  - Descrição: A aplicação é responsável pela definição dos conceitos e regras comerciais da aplicação. <br/>
    É nesta camada que são definidas as entidades e os valores dos objetos que a aplicação utiliza.<br/>
    
  ![image](https://user-images.githubusercontent.com/11943572/233072285-dd0c4ffd-eb50-4d27-ada9-2789191fcfb4.png)


##  Como começar 🚀<br/>
Para começar com a API, clone este repositório para sua máquina local e execute o seguinte <br/>
comando para iniciar a aplicação: <br/>

Para começar a utilizar o API, siga estes passos: <br/>

## Instalação :wrench: <br/>

1. Install .NET 7 if you haven't already. You can download it from [here](https://dotnet.microsoft.com/pt-br/download/dotnet/7.0).
2. Clone o  repositório para sua máquina local.`https://github.com/JessicaNathany/the-office.api.git`
3. Próximo, navegue até o diretório do projeto e execute o seguinte comando para restaurar as dependências:
`dotnet restore`
4. Finalmente, execute o seguinte comando para iniciar a API:
`dotnet run`
5. O aplicativo começará a ouvir em http://localhost:5000 <br/><br/>


## Recursos atuais :clipboard: <br/>
- GET /characters - retorna uma lista de personagens do The Office.
- GET /characters/{id} - obtém personagens por ID.
- GET /episodes - retorna uam lista de episódios do The Office.
- GET /episodes/{id} - retorna um episódio específico por ID.
- GET /quotes/characters/{id} - retorna frases específicas por id do ator do The Office

## Próximos recursos :dart: <br/>
- GET /gifs - retornar uma lista de Gifs do Office.
- GET /images - retornar todas as imagens do The Office.
- GET /images/character/{id} - retornar imagens do personagem do The Office. <br/>


Status do projeto
Este projeto está atualmente na versão 1.0.0. Abaixo está uma lista das características atualmente disponíveis<br/> e aquelas que irão
ser adicionado em futuras atualizações.<br/>

 ## Bibliotecas e pacotes 🛠️
- [Moq](https://www.nuget.org/packages/Moq)
- [AutoMoq](https://www.nuget.org/packages/AutoMoq)
- [Newtonsoft](https://www.nuget.org/packages/Newtonsoft.Json)
- [MediatR](https://www.nuget.org/packages/MediatR)
- [FluentValidation](https://www.nuget.org/packages/fluentvalidation/)
<br/><br/>

🤝 Contribuições
Contribuições são bem-vindas! <br>
Para contribuir com este projeto, por favor, bifurque este repositório e apresente um pedido de puxar.<br/><br/>

📄 Licença
Este projeto está licenciado sob a Licença MIT - consulte o arquivo LICENSE.md para maiores detalhes..<br/><br/>

📧 Contact
Se você tiver alguma pergunta ou feedback sobre este projeto, sinta-se à vontade para nos contatar em jessicanathany.web@gmail.com<br/><br/>
atualizando...
