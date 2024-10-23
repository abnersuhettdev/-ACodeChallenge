<template>
  <LayoutBase>
    <div class="container">
      <Toolbar title="Consulta de Alunos" class="toolbar">
        <v-text-field
          v-model="search"
          append-inner-icon="mdi-magnify"
          density="compact"
          label="Buscar Aluno"
          variant="solo"
          hide-details
          single-line
        ></v-text-field>
        <v-btn class="action-btn add-btn mr-3" :to="{ path: '/register' }" append-icon="mdi-plus">
          Cadastrar Aluno
        </v-btn>
      </Toolbar>

      <v-data-table :headers="headers" :items="filteredStudents">
        <template v-slot:header="{ column }" >
          <div>
            <div>{{ column.title }}</div >
          </div>
        </template>

        <template v-slot:item.actions="{ item }">
          <v-btn class="action-btn edit-btn" @click="editItem(item)" prepend-icon="mdi-pencil" rounded>
            Editar
          </v-btn>
          <v-btn class="action-btn delete-btn" @click="openDeleteModal(item)" prepend-icon="mdi-delete" rounded>
            Excluir
          </v-btn>
        </template>
      </v-data-table>

      <Snackbar :text="snackbarMessage" v-model="showSnackbar" :color="snackbarColor"/>

      <Modal 
        v-model="showModal" 
        :title="`Confirmar Exclusão`" 
        :content="`Tem certeza que deseja excluir o aluno ${studentToDelete ? studentToDelete.name : ''}?`" 
        @confirm="handleConfirm"
        cancelText="Não"
        confirmText="Sim"
      />
    </div>
  </LayoutBase>
</template>

<script>
import { ref, computed, onMounted } from 'vue';
import { useRouter } from 'vue-router'; 
import LayoutBase from '../components/BaseLayout/BaseLayout.vue';
import Toolbar from '../components/Toolbar/Toolbar.vue';
import Snackbar from '../components/Snackbar/Snackbar.vue';
import { DeleteStudent, GetAllStudents } from '@/services/StudentsService';
import Modal from '../components/Modal/Modal.vue';

export default {
  components: {
    LayoutBase,
    Toolbar,
    Snackbar,
    Modal
  },
  setup() {
    const router = useRouter();
    const search = ref('');
    const students = ref([]);
    const snackbarMessage = ref('');
    const snackbarColor = ref('');
    const showSnackbar = ref(false);
    const showModal = ref(false);
    const studentToDelete = ref(null);

    const headers = [
      { title: 'Registro Acadêmico', key: 'ra', sortable: false },
      { title: 'Nome', key: 'name', sortable: false },
      { title: 'E-mail', key: 'email', sortable: false },
      { title: 'CPF', key: 'cpf', sortable: false },
      { title: 'Ações', key: 'actions', sortable: false },
    ];

    const fetchStudents = () => {
      GetAllStudents()
      .then((data)=> {
        students.value = data;
      })
      .catch((err)=>{
        showSnackbarWithMessage(err.response.data, 'red')
      });
    };

    const filteredStudents = computed(() => {
      return filterStudents(students.value);
    });

    const filterStudents = (studentsList) => {
      return !search.value
        ? studentsList
        : studentsList.filter(student =>
            student.name.toLowerCase().includes(search.value.toLowerCase())
          );
    };

    const editItem = (item) => {
      router.push({ path: '/register', query: { ra: item.ra } }); 
    };

    const openDeleteModal = (item) => {
      studentToDelete.value = item;
      showModal.value = true;
    };

    const handleConfirm = () => {
      deleteItem(studentToDelete.value);
    };

    const deleteItem = async (item) => {
      DeleteStudent(item.ra)
        .then(() =>{
            fetchStudents();
            showSnackbarWithMessage("Aluno excluído com sucesso", 'green');
        })
        .catch((error)=> {
          showSnackbarWithMessage(error.response.data, 'red');
        })
        .finally(() => {
          showModal.value = false;
          studentToDelete.value = null;
        });
    };

    const showSnackbarWithMessage = (message, color) => {
      snackbarMessage.value = message;
      showSnackbar.value = true;
      snackbarColor.value = color;
    };

    onMounted(fetchStudents);

    return {
      search,
      headers,
      filteredStudents,
      editItem,
      openDeleteModal,
      handleConfirm,
      showSnackbar,
      snackbarMessage,
      snackbarColor,
      showModal,
      studentToDelete
    };
  },
};
</script>

<style scoped>
.v-data-table {
  background-color: #fafafa;
  border-radius: 0 0 10px 10px;
}

.action-btn {
  margin: 0 10px;
  min-width: 100px;
  color: white;
}

.edit-btn {
  background-color: #1976d2;
}

.edit-btn:hover {
  background-color: #0d47a1;
}

.delete-btn {
  background-color: #e53935;
}

.delete-btn:hover {
  background-color: #b71c1c;
}

.add-btn {
  background-color: #f5f5f5;
  color: #000;
}
</style>
