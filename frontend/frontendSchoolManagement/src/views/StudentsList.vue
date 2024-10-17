<template>
  <LayoutBase>
    <!-- Barra de ferramentas -->
    <Toolbar title="Consulta de alunos">
      <v-text-field
        v-model="search"
        append-icon="mdi-magnify"
        label="Digite sua busca"
        hide-details
        single-line
      ></v-text-field>
      <v-btn class="ml-3" color="primary">Pesquisar</v-btn>
      <v-btn class="ml-3" color="primary" :to="{ path: '/register' }">Cadastrar Aluno</v-btn>
    </Toolbar>

    <v-data-table :headers="headers" :items="students">
        <template v-slot:item.actions="{ item }">
          <v-btn small color="primary" @click="editItem(item)">
            Editar
          </v-btn>
          <v-btn small color="red" @click="deleteItem(item)">
            Excluir
          </v-btn>
        </template>
      </v-data-table>
      
  </LayoutBase>
</template>

<script>
import LayoutBase from '../components/BaseLayout/BaseLayout.vue'
import Toolbar from '../components/Toolbar/Toolbar.vue'
export default {
  components: {
    LayoutBase,
    Toolbar
  },
  data() {
    return {
      headers: [
        { title: 'Registro Acadêmico', key: 'id' },
        { title: 'Nome', key: 'name' },
        { title: 'CPF', key: 'cpf' },
        { title: 'Ações', key: 'actions', sortable: false },
      ],
      // Dados dos alunos
      students: [
        { id: '101235', name: 'Paula Souza', cpf: '121.999.999-99' },
        { id: '111687', name: 'João Silva', cpf: '122.999.999-99' },
        { id: '111365', name: 'Marina Miranda', cpf: '123.999.999-99' },
        { id: '101299', name: 'Maurício Souza', cpf: '124.999.999-99' },
      ],
    };
  },
  methods: {
    editItem(item) {
      console.log('Editando:', item);
    },
    deleteItem(item) {
      console.log('Excluindo:', item);
    },
  },
};
</script>