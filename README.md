# PolSource Backend Developer Assignment

### What is required for running the project
- Microsoft SQL Server
- Microsoft SQL Server Management Studio
- Visual Studio 2019
- Netcoreapp 3.1
- Sdk= Microsoft .NET .Sdk .Web
- Microsoft.EntityFrameworkCore Version="3.1.8"
- Microsoft.EntityFrameworkCore.SqlServer Version="3.1.8"
- Microsoft.EntityFrameworkCore.Tools Version="3.1.8"
- IIS Express Server

### Steps how to run scripts that will setup database for the project

Create a database in Microsoft SQL Server using Microsoft SQL Server Management Sttudio. To achieve this, you must connect to the database server using Microsoft SQL Server Management Studio. After connection, create a database by right-clicking on the folder named "databases" and selecting "new database". After creating the database, upload a script named "DatabaseScript.sql" to it. This script will create the necessary tables in the database.

### Steps how to build and run the project

Before building and running the project, add a connection string to your database. It must be assigned to a variable named "_connectionString" in a file named "Startup.cs". Connection string can be obtained by creating a new database connection with Visual Studio in "server explorer". After establishing the connection, select the database and select its properties. The connection string should be available there.
The easiest way to build and run a project is to use the visual studio IDE by pressing the RUN button.
This can also be achieved by using commands in project directory:

```ssh
dotnet build
dotnet run
```

To run test you can use command in project test directory:

```ssh
dotnet test
```

### Example usages
##### GET All Notes

```ssh
curl http://localhost:60469/api/notes
```

##### GET All History

```ssh
curl http://localhost:60469/api/NoteHistories/
```

##### GET {id} Note

```ssh
curl http://localhost:60469/api/notes/17
```

##### GET {id} History

```ssh
curl http://localhost:60469/api/NoteHistories/17
```

##### Delete {id} Note

```ssh
curl -L -X DELETE 'http://localhost:60469/api/notes/1007'
```

##### Post Note

```ssh
curl -d '{\"title\":\"value1\", \"content\":\"value2\"}' -H "Content-Type: application/json" -X POST 'http://localhost:60469/api/notes'
```

##### PUT {id} Note

```ssh
curl -L -X PUT 'http://localhost:60469/api/notes/1009' -H 'Content-Type: application/json' -d '{\"idNote\": 1009,\"title\": \"value1\", \"content\": \"value2\"}'
```