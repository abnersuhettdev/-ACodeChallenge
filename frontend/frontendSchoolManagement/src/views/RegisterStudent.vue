<template>
  <LayoutBase>
    <!-- Barra de ferramentas -->
    <Toolbar title="Cadastro de aluno"></Toolbar>

    <!-- Formulário de cadastro -->
    <v-form ref="form" v-model="valid" lazy-validation>
      <v-row>
        <v-col cols="12">
          <v-text-field v-model="student.name" label="Nome" :rules="[rules.required]"
            placeholder="Informe o nome completo"></v-text-field>
        </v-col>

        <v-col cols="12">
          <v-text-field v-model="student.email" label="E-mail" :rules="[rules.required, rules.email]"
            placeholder="Informe apenas um e-mail"></v-text-field>
        </v-col>

        <v-col cols="12">
          <v-text-field v-model="student.ra" label="RA" :rules="[rules.required]"
            placeholder="Informe o registro acadêmico" :disabled="isEditing"></v-text-field>
        </v-col>

        <v-col cols="12">
          <v-text-field v-model="student.cpf" label="CPF" :rules="[rules.required, rules.cpf]"
            placeholder="Informe o número do documento" :disabled="isEditing"></v-text-field>
        </v-col>
      </v-row>

      <v-row justify="end">
        <v-btn color="grey" class="mr-4" :to="{ path: '/' }">Cancelar</v-btn>
        <v-btn color="primary" @click="saveForm" :disabled="!valid">Salvar</v-btn>
      </v-row>
    </v-form>
  </LayoutBase>
</template>

<script>
import { ref, onMounted } from 'vue'; // Importa ref e onMounted
import { useRoute, useRouter } from 'vue-router'; // Importa useRoute e useRouter
import LayoutBase from '../components/BaseLayout/BaseLayout.vue';
import Toolbar from '../components/Toolbar/Toolbar.vue';

export default {
  components: {
    LayoutBase,
    Toolbar,
  },
  setup() {
    const valid = ref(false);
    const isEditing = ref(false); // Inicializa como false
    const student = ref({
      name: '',
      email: '',
      ra: '',
      cpf: '',
    });

    const rules = {
      required: value => !!value || 'Este campo é obrigatório.',
      email: value => {
        const pattern = /^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$/;
        return pattern.test(value) || 'E-mail inválido.';
      },
      cpf: value => {
        const pattern = /^\d{3}\.\d{3}\.\d{3}-\d{2}$/;
        return pattern.test(value) || 'CPF inválido.';
      },
    };

    const route = useRoute(); // Obtém a instância da rota
    const router = useRouter(); // Obtém a instância do router

    // Componente lifecycle
    onMounted(() => {
      const ra = route.query.ra; // Obtendo o RA da URL
      if (ra) {
        const students = JSON.parse(localStorage.getItem('students')) || [];
        const foundStudent = students.find(s => s.ra === ra);

        if (foundStudent) {
          student.value = { ...foundStudent };
          isEditing.value = true; // Ativa o modo de edição se o aluno for encontrado
        }
      }
    });

    const saveForm = () => {
      if (valid.value) { // Usando valid.value
        let students = JSON.parse(localStorage.getItem('students')) || [];
        const index = students.findIndex(s => s.ra === student.value.ra);

        if (index !== -1) {
          students[index] = student.value; // Atualiza o aluno existente
        } else {
          students.push(student.value); // Adiciona um novo aluno
        }

        localStorage.setItem('students', JSON.stringify(students));
        router.push('/'); // Redireciona para a lista de alunos
      }
    };

    return {
      valid,
      isEditing,
      student,
      rules,
      saveForm,
    };
  },
};
</script>
