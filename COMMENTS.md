# Decisão da Arquitetura Utilizada

## Front-end

### Estrutura de Pastas e Decisão de Arquitetura
O projeto segue uma arquitetura modular e organizada, facilitando a manutenção e escalabilidade. Abaixo está um resumo das principais pastas e suas funções:

1. **`assets`**
   - Armazena recursos estáticos como imagens, incluindo o logo da aplicação.

2. **`components`**
   - Contém componentes reutilizáveis da interface, como `BaseLayout`, `Modal`, `Snackbar` e `Toolbar`. Essa organização promove a reutilização de código e facilita a manutenção, permitindo que a interface seja composta por elementos modulares.

3. **`router`**
   - Centraliza a lógica de navegação da aplicação. O arquivo `index.js` define as rotas principais, como páginas de registro e listagem de alunos, tornando a estrutura de navegação clara e fácil de expandir.

4. **`services`**
   - Responsável por abstrair a comunicação com APIs externas. Arquivos como `ApiSettings.js` e `StudentsService.js` organizam toda a lógica de integração, separando a camada de negócio da interface.

5. **`views`**
   - Contém as páginas principais da aplicação, como `RegisterStudent.vue` e `StudentsList.vue`. Essas views são responsáveis por renderizar a interface, utilizando componentes e serviços para criar as telas da aplicação.

---

## Back-end

A arquitetura segue o padrão em camadas, separando claramente a lógica de negócios (`BusinessRules`), controle de rotas (`Controllers`), persistência de dados (`Repository` e `Data`) e modelos de entidades (`Models`). Isso melhora a organização, facilita testes unitários e promove a escalabilidade da aplicação.

- **Padrão MVC**: O uso de `Controllers` para receber e responder às requisições mantém a separação de responsabilidades entre as camadas.
- **Repository Pattern**: A aplicação do padrão `Repository` permite a separação da lógica de acesso a dados da lógica de negócios, tornando o código mais modular e testável.
- **Entity Framework**: O uso de migrações facilita a manutenção e evolução do banco de dados.

---

## Lista de Bibliotecas de Terceiros Utilizadas

- **Vue.js**: Framework utilizado para a criação da interface e gestão do fluxo de dados.
- **Vuetify**: Framework de UI para Vue.js.
- **Vue Router**: Gerenciamento de rotas da aplicação.
- **Axios**: Biblioteca para fazer requisições HTTP.
- **MDI Icons**: Inclusão de ícones na aplicação.
- **vue-the-mask** : Inclusão de máscara no campo CPF.
---

## O Que Eu Melhoraria se Tivesse Mais Tempo

1. Incluiria funções de login e autenticação para criar um usuário administrador e aumentar a segurança.
2. Melhoraria o design e a responsividade para dar mais valor ao projeto.
3. Implementaria novas funções e telas para utilizar os dados da aplicação de forma mais completa.

---

## Requisitos Obrigatórios Que Não Foram Entregues

Todos os requisitos obrigatórios foram entregues.
