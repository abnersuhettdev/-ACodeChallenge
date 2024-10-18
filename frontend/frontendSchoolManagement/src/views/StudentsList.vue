<template>
  <LayoutBase>
    <!-- Barra de ferramentas -->
    <Toolbar title="Consulta de alunos">
      <v-text-field v-model="search" append-icon="mdi-magnify" label="Digite sua busca" hide-details
        single-line></v-text-field>
      <v-btn class="ml-3" color="primary">Pesquisar</v-btn>
      <v-btn class="ml-3" color="primary" :to="{ path: '/register' }">Cadastrar Aluno</v-btn>
    </Toolbar>

    <v-data-table :headers="headers" :items="filteredStudents">
      <template v-slot:item.actions="{ item }">
        <v-btn small color="primary" @click="editItem(item)">
          Editar
        </v-btn>
        <v-btn small color="red" @click="confirmDelete(item)">
          Excluir
        </v-btn>
      </template>
    </v-data-table>
  </LayoutBase>
</template>

<script>
import LayoutBase from '../components/BaseLayout/BaseLayout.vue';
import Toolbar from '../components/Toolbar/Toolbar.vue';

export default {
  components: {
    LayoutBase,
    Toolbar,
  },
  data() {
    return {
      headers: [
        { title: 'Registro Acadêmico', key: 'ra' },
        { title: 'Nome', key: 'name' },
        { title: 'CPF', key: 'cpf' },
        { title: 'Ações', key: 'actions', sortable: false },
      ],
      search: '',
      // Dados dos alunos
      students: JSON.parse(localStorage.getItem('students')) || [],
    };
  },
  computed: {
    filteredStudents() {
      if (!this.search) return this.students;
      return this.students.filter(student =>
        student.name.toLowerCase().includes(this.search.toLowerCase())
      );
    },
  },
  methods: {
    editItem(item) {
      this.$router.push({ path: '/register', query: { ra: item.ra } });
    },
    confirmDelete(item) {
      const confirmed = confirm(`Você realmente deseja excluir o aluno ${item.name}?`);
      if (confirmed) {
        this.deleteItem(item);
      }
    },
    deleteItem(item) {
      // Filtra o aluno que não deve ser mantido na lista
      this.students = this.students.filter(student => student.ra !== item.ra);
      // Atualiza a lista no localStorage
      localStorage.setItem('students', JSON.stringify(this.students));
    },
  },
};
</script>
