# Feedback - Avaliação Geral

## Front End

### Navegação
  * Pontos positivos:
    - Projeto MVC (`Delta.AppMvc`) com navegação funcional e views completas para categorias, produtos e autenticação.
    - Estrutura de rotas e controllers bem organizada.

  * Pontos negativos:
    - Nenhum.

### Design
  - Interface visual clara e objetiva. Cumpre bem o papel de interface administrativa.

### Funcionalidade
  * Pontos positivos:
    - CRUD completo de produtos e categorias implementado em ambas as camadas (MVC e API).
    - Implementação correta da autenticação com Identity (JWT na API, cookie no MVC).
    - Registro do usuário realiza também a criação do vendedor com o mesmo ID (conforme especificação).
    - SQLite implementado corretamente com seed e migrations automáticas.

  * Pontos negativos:
    - Duas implementações de `DbMigrationsHelpers`, uma em cada aplicação, gerando redundância. Uma abordagem compartilhada evitaria duplicação de código.

## Back End

### Arquitetura
  * Pontos positivos:
    - Separação de camadas em API, MVC, Business e Infra, com injeção de dependência configurada corretamente.
    - Aplicação bem estruturada em projetos distintos.

  * Pontos negativos:
    - A divisão entre `Business` e `Infra` adiciona complexidade desnecessária para o nível de desafio proposto. Uma única camada chamada `Core` atenderia perfeitamente sem perda de organização.

### Funcionalidade
  * Pontos positivos:
    - Todas as funcionalidades previstas no escopo foram implementadas corretamente e estão operacionais.
    - Verificações e operações de domínio estão presentes e funcionais.

  * Pontos negativos:
    - Nenhum.

### Modelagem
  * Pontos positivos:
    - Modelagem de entidades aderente ao domínio proposto.
    - Classes organizadas, coerentes e com separação clara entre entidades e view models.

  * Pontos negativos:
    - Nenhum.

## Projeto

### Organização
  * Pontos positivos:
    - Uso correto de `src`, solution `.sln` na raiz, e estrutura clara de projetos.
    - README e FEEDBACK presentes.
    - Swagger na API implementado.

  * Pontos negativos:
    - Nenhum.

### Documentação
  * Pontos positivos:
    - Documentação clara e bem estruturada.
    - Explicações sobre execução, dependências e projeto.

  * Pontos negativos:
    - Nenhum.

### Instalação
  * Pontos positivos:
    - Migrations automáticas e seed de dados funcionam em ambas as camadas.
    - Uso de SQLite viabiliza a execução sem dependências externas.

  * Pontos negativos:
    - Uso duplicado de helpers de seed/migration entre API e MVC.

---

# 📊 Matriz de Avaliação de Projetos

| **Critério**                   | **Peso** | **Nota** | **Resultado Ponderado**                  |
|-------------------------------|----------|----------|------------------------------------------|
| **Funcionalidade**            | 30%      | 10       | 3,0                                      |
| **Qualidade do Código**       | 20%      | 10       | 2,0                                      |
| **Eficiência e Desempenho**   | 20%      | 8        | 1,6                                      |
| **Inovação e Diferenciais**   | 10%      | 10       | 1,0                                      |
| **Documentação e Organização**| 10%      | 8        | 0,8                                      |
| **Resolução de Feedbacks**    | 10%      | 10       | 1,0                                      |
| **Total**                     | 100%     | -        | **9,4**                                  |

## 🎯 **Nota Final: 9,4 / 10**

