# Feedback - Avaliação Geral

## Front End
### Navegação
  * Pontos positivos:
    - Rotas de CRUD encontradas

### Design
    - Será avaliado na entrega final
 
### Funcionalidade
  * Pontos positivos:
    - CRUD Implementado

## Back End
### Arquitetura
  * Pontos positivos:
    - Camadas separadas com responsabilidades claras (API, regras de negócio, infraestrutura).

  * Pontos negativos:
    - Projeto contém 4 camadas (`Api`, `AppMvc`, `Business`, `Infra`) enquanto o desafio especifica apenas 3.
    - Recomenda-se simplificar e "deixar o arsenal técnico para desafios que exigem complexidade".

### Funcionalidade
  * Pontos positivos:
    - API configurada, com uso do Entity Framework Core.
    - Presença de SQLite como banco.
  
  * Pontos negativos:
    - Não há implementação da criação automática da entidade "Vendedor" ao registrar usuário via Identity.

### Modelagem
  * Pontos positivos:
    - Modelos simples, bem definidos, respeitando a proposta de modelagem anêmica.

## Projeto
### Organização
  * Pontos positivos:
    - Presença da pasta `src` na raiz.
    - Camadas bem organizadas por projeto.
    - Solution `.sln` presente corretamente dentro da `src`.

  * Pontos negativos:
    - Não persistir arquivos de banco de dados `.db`, `.shm`, `.wal` presentes na raiz do projeto MVC

### Documentação
  * Pontos positivos:
    - Presença de arquivo `README.md`.

  * Pontos negativos:
    - `README.md` sem instruções de execução ou descrição do projeto.
    - Ausência do arquivo `FEEDBACK.MD`.

### Instalação
  * Pontos positivos:
    - Uso do SQLite identificado em arquivos de configuração e banco existente no repositório.
