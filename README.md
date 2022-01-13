# Backend NET Core



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

###### dotnet new mstest
```
Tests
```


### Referências entre as camadas

- **Infra** faz referência ao **Domain**  
- **Api** faz referência ao **Domain** e **Infra**  
- **Test** faz referência ao **Domain**  




<br>
<br>

--- 

# ORM

Dapper

<br>
<br>



