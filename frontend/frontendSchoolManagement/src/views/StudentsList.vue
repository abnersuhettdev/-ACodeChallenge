<template>
  <LayoutBase>
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

    <v-data-table :headers="headers" :items="filteredStudents">
      <template v-slot:header="{ column }">
        <div style="cursor: default;" @click.stop>
          <span style="cursor: default;">{{ column.title }}</span>
          <v-icon small class="ml-2" @click="toggleSort(column.key)" style="cursor: pointer;">
            {{ getIcon(column.key) }}
          </v-icon>
        </div>
      </template>

      <template v-slot:item.actions="{ item }">
        <v-btn small color="primary" @click="editItem(item)">Editar</v-btn>
        <v-btn small color="red" @click="confirmDelete(item)">Excluir</v-btn>
      </template>
    </v-data-table>

    <Snackbar :text="snackbarMessage" v-model="showSnackbar"/>
  </LayoutBase>
</template>

<script>
import LayoutBase from '../components/BaseLayout/BaseLayout.vue';
import Toolbar from '../components/Toolbar/Toolbar.vue';
import Snackbar from '../components/Snackbar/Snackbar.vue';

export default {
  components: {
    LayoutBase,
    Toolbar,
    Snackbar,
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
      students: JSON.parse(localStorage.getItem('students')) || [],
      sortBy: 'name',
      sortDesc: false,
      snackbarMessage: '',
      showSnackbar: false,
    };
  },
  computed: {
    filteredStudents() {
      return this.sortStudents(this.filterStudents(this.students));
    },
  },
  methods: {
    filterStudents(students) {
      if (!this.search) return students;
      return students.filter(student =>
        student.name.toLowerCase().includes(this.search.toLowerCase())
      );
    },
    sortStudents(students) {
      const modifier = this.sortDesc ? -1 : 1;
      return students.sort((a, b) => {
        if (a[this.sortBy] < b[this.sortBy]) return -1 * modifier;
        if (a[this.sortBy] > b[this.sortBy]) return 1 * modifier;
        return 0;
      });
    },
    toggleSort(column) {
      if (this.sortBy === column) {
        this.sortDesc = !this.sortDesc;
      } else {
        this.sortBy = column;
        this.sortDesc = false;
      }
    },
    getIcon(column) {
      return this.sortBy === column ? (this.sortDesc ? 'mdi-chevron-up' : 'mdi-chevron-down') : 'mdi-chevron-up';
    },
    editItem(item) {
      this.$router.push({ path: '/register', query: { ra: item.ra } });
    },
    confirmDelete(item) {
      const confirmed = confirm(`Você realmente deseja excluir o aluno ${item.name}?`);
      if (confirmed) {
        this.deleteItem(item);
        this.showSnackbarWithMessage("Aluno excluído com sucesso");
      }
    },
    deleteItem(item) {
      this.students = this.students.filter(student => student.ra !== item.ra);
      localStorage.setItem('students', JSON.stringify(this.students));
    },
    showSnackbarWithMessage(message) {
      this.snackbarMessage = message;
      this.showSnackbar = true;
    },
  },
};
</script>
