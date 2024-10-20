<template>
  <LayoutBase>
    <Toolbar title="Cadastro de aluno" />

    <v-form ref="formRef" v-model="valid" lazy-validation>
      <v-row>
        <v-col cols="12">
          <v-text-field
            v-model="student.name"
            label="Nome"
            :rules="[rules.required]"
            :error-messages="getErrorMessages('name')"
            placeholder="Informe o nome completo"
          />
        </v-col>

        <v-col cols="12">
          <v-text-field
            v-model="student.email"
            label="E-mail"
            :rules="[rules.required, rules.email]"
            :error-messages="getErrorMessages('email')"
            placeholder="Informe apenas um e-mail"
            @focus="setTouched('email')"
          />
        </v-col>

        <v-col cols="12">
          <v-text-field
            v-model="student.ra"
            label="RA"
            :rules="[rules.required, rules.ra]"
            :error-messages="getErrorMessages('ra')"
            placeholder="Informe o registro acadêmico"
            :disabled="isEditing"
            maxlength="6"
            @focus="setTouched('ra')"
          />
        </v-col>

        <v-col cols="12">
          <v-text-field
            v-model="student.cpf"
            label="CPF"
            :rules="[rules.required, rules.cpf]"
            :error-messages="getErrorMessages('cpf')"
            placeholder="Informe o número do documento"
            :disabled="isEditing"
            v-mask="'###.###.###-##'"
            @focus="setTouched('cpf')"
          />
        </v-col>
      </v-row>

      <v-row justify="end">
        <v-btn color="grey" class="mr-4" :to="{ path: '/' }">Cancelar</v-btn>
        <v-btn color="primary" @click="saveForm">Salvar</v-btn>
      </v-row>
    </v-form>

    <Snackbar :text="snackbarMessage" v-model="showSnackbar" />
  </LayoutBase>
</template>

<script>
import { ref, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import LayoutBase from '../components/BaseLayout/BaseLayout.vue';
import Toolbar from '../components/Toolbar/Toolbar.vue';
import Snackbar from '../components/Snackbar/Snackbar.vue';

export default {
  components: {
    LayoutBase,
    Toolbar,
    Snackbar,
  },
  setup() {
    const formRef = ref(null);
    const valid = ref(false);
    const isEditing = ref(false);
    const student = ref({
      name: '',
      email: '',
      ra: '',
      cpf: '',
    });

    const snackbarMessage = ref('');
    const showSnackbar = ref(false);
    const isFormSubmitted = ref(false);
    const touchedFields = ref({
      name: false,
      email: false,
      ra: false,
      cpf: false,
    });

    const rules = {
      required: (value) => !!value || 'Este campo é obrigatório.',
      ra: (value) => /^\d{6}$/.test(value) || 'RA deve conter exatamente 6 dígitos numéricos.',
      email: (value) => /^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$/.test(value) || 'E-mail inválido.',
      cpf: (value) => /^\d{3}\.\d{3}\.\d{3}-\d{2}$/.test(value) || 'CPF inválido.',
    };

    const route = useRoute();

    onMounted(() => {
      const ra = route.query.ra;
      if (ra) {
        const students = JSON.parse(localStorage.getItem('students')) || [];
        const foundStudent = students.find((s) => s.ra === ra);

        if (foundStudent) {
          student.value = { ...foundStudent };
          isEditing.value = true;
        }
      }
    });

    const setTouched = (field) => {
      touchedFields.value[field] = true;
    };

    const getErrorMessages = (field) => {
      const value = student.value[field];
      const isTouched = isFormSubmitted.value || touchedFields.value[field];
      const ruleSet = field === 'name' ? [rules.required] : [rules.required, rules[field]];

      return isTouched && !ruleSet.every((rule) => rule(value) === true)
        ? ruleSet.map((rule) => rule(value)).filter((msg) => msg !== true)
        : [];
    };

    const saveForm = () => {
      isFormSubmitted.value = true;

      if (Object.keys(student.value).every((field) => getErrorMessages(field).length === 0)) {
        let students = JSON.parse(localStorage.getItem('students')) || [];
        const index = students.findIndex((s) => s.ra === student.value.ra);

        if (index !== -1) {
          students[index] = student.value;
          snackbarMessage.value = 'Aluno atualizado com sucesso!';
        } else {
          students.push(student.value);
          snackbarMessage.value = 'Aluno cadastrado com sucesso!';
        }

        localStorage.setItem('students', JSON.stringify(students));
        showSnackbar.value = true;

        resetForm();
      }
    };

    
    const resetForm = () => {
      student.value = { name: '', email: '', ra: '', cpf: '' };

      touchedFields.value = { name: false, email: false, ra: false, cpf: false };

      if (formRef.value) {
        formRef.value.reset(); 
        isFormSubmitted.value = false;
        valid.value = false; 
      }
    };

    return {
      formRef,
      valid,
      isEditing,
      student,
      rules,
      saveForm,
      snackbarMessage,
      showSnackbar,
      isFormSubmitted,
      setTouched,
      getErrorMessages,
    };
  },
};
</script>
