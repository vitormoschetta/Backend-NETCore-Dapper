# Backend NET Core Dapper



# Arquitetura DDD

Nosso projeto de backend está dividido em três partes principais, mais a camada de testes:

###### dotnet new webai
```
Api
```

###### dotnet new classlib netstandar2.1
```
Domain
Infra
```

### Referências entre as camadas

- **Infra** faz referência ao **Domain**  
- **Api** faz referência ao **Domain** e **Infra**  



<br>
<br>

--- 

# ORM Dapper


### Migrations:

<https://fluentmigrator.github.io/>


<br>
<br>



