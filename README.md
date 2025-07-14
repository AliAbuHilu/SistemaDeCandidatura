# Sistema de Candidatura para Vagas de Emprego

Este projeto é uma aplicação web ASP.NET Core MVC para gerenciar candidaturas em vagas de emprego.  
Permite o cadastro, edição, exclusão e listagem de candidaturas associadas a pessoas e vagas disponíveis.

Possui filtros avançados para facilitar a busca e visualização, incluindo filtro por vaga, texto livre e status de aprovação (Sim, Não ou Todos).  
Além disso, suporta exportação da lista filtrada para arquivo CSV.

O armazenamento é simulado através de arquivos .txt para facilitar o uso sem banco de dados real.

---

## Funcionalidades

- CRUD completo para candidaturas  
- Associação entre Candidatura, Pessoa e Vaga  
- Filtros por vaga, busca textual e status de aprovação  
- Paginação na listagem  
- Exportação para CSV  
- Validação dos dados  
- Layout responsivo com Bootstrap  

---

## Tecnologias Utilizadas

- ASP.NET Core MVC (.NET 8)  
- C#  
- Bootstrap 4/5  
- AutoMapper  
- Serviços singleton para simular acesso a dados (arquivos .txt)  
- Razor Views  

---

## Como rodar o projeto

1. Clone este repositório:  
   ```bash
   git clone https://github.com/AliAbuHilu/SistemaDeCandidatura.git

2. Abra a solução no Visual Studio 2022 (ou superior) e execute a aplicação (pressione F5).

3. Restaure os pacotes NuGet, se necessário: dotnet restore

4. Compile e execute o projeto (F5 no Visual Studio ou no terminal):dotnet run --project WebApp


5. Quando a aplicação iniciar, observe no terminal a mensagem com a URL onde o servidor está rodando, por exemplo:Now listening on: https://localhost:7186

6. Acesse essa URL no navegador para usar o sistema.

