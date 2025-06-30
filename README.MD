# Sistema de Candidaturas - Teste MBM

## Descrição

Este projeto é uma aplicação web ASP.NET Core MVC para gerenciar candidaturas em vagas de emprego. Permite o cadastro, edição, exclusão e listagem de candidaturas associadas a pessoas e vagas disponíveis. 

Possui filtros avançados para facilitar a busca e visualização, incluindo filtro por vaga, texto livre e status de aprovação (Sim, Não ou Todos). Além disso, suporta exportação da lista filtrada para arquivo CSV.

O armazenamento é simulado através de arquivos `.txt` para facilitar o uso sem banco de dados real.

## Funcionalidades

- CRUD completo para candidaturas  
- Associação entre Candidatura, Pessoa e Vaga  
- Filtros por vaga, busca textual e status de aprovação  
- Paginação na listagem  
- Exportação para CSV  
- Validação dos dados  
- Layout responsivo com Bootstrap  

## Tecnologias Utilizadas

- ASP.NET Core MVC (.NET 8)  
- C#  
- Bootstrap 4/5  
- AutoMapper  
- Serviços singleton para simular acesso a dados (arquivos `.txt`)  
- Razor Views  

## Como Executar

1. Clone o repositório:  
   ```bash
   git clone <url-do-repositorio>
