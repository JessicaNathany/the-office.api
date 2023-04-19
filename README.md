# The Office API <br>

<p align="center">
  <img src="https://img.shields.io/badge/Framework-dotnet-blue"/> 
  <img src="https://img.shields.io/badge/Framework%20version-dotnet%206-blue"/>
  <img src="https://img.shields.io/badge/Language-C%23-blue"/> 
  <img src="https://img.shields.io/badge/Status-development-green"/>  
   <img src=" https://img.shields.io/badge/Status-development-green"/>  
</p>



 <h4 align="center"> 
	🚧  Project 🚀 under construction...  🚧
 </h4>

This is a .NET 6 Web API that provides endpoints to access and manage data related to the TV show The Office. <br/>
The API allows users to retrieve information about characters, episodes, and quotes from the show. <br/><br/>

## API Documentation 📝 <br/>
The API endpoints are documented using Swagger

Open a web browser and navigate to http://localhost:5000/swagger.
This will display the Swagger UI, which provides a user-friendly interface for exploring the API 
endpoints. The API provides the following endpoints:<br/>

- GET /characters - retrieves a list of all characters in The Office.
- GET /characters/{id} - retrieves a specific character by ID.
- GET /episodes - retrieves a list of all episodes in The Office.
- GET /episodes/{id} - retrieves a specific episode by ID.
- GET /quotes/characters/{id} - retrieves a list of all better frases for specific character in The Office.<br/><br/>


1. **Presentation Layer**
  - Name: the-office.api<br/>
  - Description: This layer is responsible for exposing the API to the outside world, i.e. it receives HTTP requests, <br/>
    processes them and returns the responses. It is in this layer that most of the API configurations are done. <br/>
    
2. **Application Layer**
  - Name: the-office.application<br/>
  - Description: The application layer is responsible for housing the application's business rules, <br/>
    it uses the services provided by the other layers to perform the required operations. It is in this layer <br/>
    that the application use cases are implemented. <br/>
    
3. **Domain Layer**
  - Name: the-office.domain<br/>
  - Description: The application is responsible for defining the application's business concepts and rules. <br/>
    It is in this layer that the entities and object values that the application uses are defined.<br/>
    
  ![image](https://user-images.githubusercontent.com/11943572/233072285-dd0c4ffd-eb50-4d27-ada9-2789191fcfb4.png)


##  Getting Started 🚀
To get started with the API, clone this repository to your local machine and run the following <br/>
command to start the application: <br/>

To get started with the API, follow these steps: <br/>

## Installation 

1. Install .NET 6 if you haven't already. You can download it from [here](https://dotnet.microsoft.com/download/dotnet/6.0).
2. Clone this repository to your local machine.`https://github.com/JessicaNathany/the-office.api.git`
3. Next, navigate to the project directory and run the following command to restore the dependencies:
`dotnet restore`
4. Finally, run the following command to start the API:
`dotnet run`
5. The application will start listening on http://localhost:5000 <br/><br/>


## Current features: <br/>
- GET /characters - retrieves a list of all characters in The Office.
- GET /characters/{id} - retrieves a specific character by ID.
- GET /episodes - retrieves a list of all episodes in The Office.
- GET /episodes/{id} - retrieves a specific episode by ID.
- GET /quotes/characters/{id} - retrieves a list of all better frases for specific character in The Office

## Next  features: <br/>
- GET /gifs - retrieves a list of all gifs in The Office.
- GET /images - retrieves a list of all iamges in The Office.
- GET /images/character/{id} - retrieves of all iamges in specific character in The Office. <br/>


Project status
This project is currently at version 1.0.0. Below is a list of currently available features and those that will<br/> 
be added in future updates.<br/>

🛠️ Built With
*. NET 6
* Entity Framework Core
* Swagger
<br/><br/>

🤝 Contributing
Contributions are welcome! <br>
To contribute to this project, please fork this repository and submit a pull request.<br/><br/>

📄 License
This project is licensed under the MIT License - see the LICENSE.md file for details.<br/><br/>

📧 Contact
If you have any questions or feedback about this project, please feel free to reach out to us at jessicanathany.web@gmail.com<br/><br/>
update...
