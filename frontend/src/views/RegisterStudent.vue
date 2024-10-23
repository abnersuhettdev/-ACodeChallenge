<template>
  <LayoutBase>
    <Toolbar :title="toolbarTitle" class="toolbar" />

    <v-form ref="formRef" v-model="valid" lazy-validation>
      <v-container class="formContainer">
      <v-row>
        <v-col cols="12">
          <v-text-field
            v-model="student.name"
            label="Nome"
            :rules="[rules.required]"
            :error-messages="getErrorMessages('name')"
            placeholder="Informe o nome completo"
            variant="outlined"
          />
        </v-col>

        <v-col cols="12">
          <v-text-field
            v-model="student.email"
            label="E-mail"
            variant="outlined"
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
            variant="outlined"
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
            variant="outlined"
            :rules="[rules.required, rules.cpf]"
            :error-messages="getErrorMessages('cpf')"
            placeholder="Informe o número do documento"
            :disabled="isEditing"
            v-mask="'###.###.###-##'"
            @focus="setTouched('cpf')"
          />
        </v-col>
      </v-row>
      </v-container>
      <v-row justify="end" class="mt-4 mr-3">
        <v-btn color="grey" class="mr-4" :to="{ path: '/' }">Cancelar</v-btn>
        <v-btn color="primary" @click="saveForm">Salvar</v-btn>
      </v-row>
    </v-form>

    <Snackbar :text="snackbarMessage" v-model="showSnackbar" :color="snackbarColor" />
  </LayoutBase>
</template>

<script>
import { ref, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import LayoutBase from '../components/BaseLayout/BaseLayout.vue';
import Toolbar from '../components/Toolbar/Toolbar.vue';
import Snackbar from '../components/Snackbar/Snackbar.vue';
import { CreateStudent, EditStudent, GetStudentByRA } from '@/services/StudentsService';

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
    const snackbarColor = ref('');
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
    const toolbarTitle = ref('Cadastro de aluno'); 

    onMounted(() => {
      const ra = route.query.ra;
      if (ra) {
        toolbarTitle.value = 'Edição de aluno';

        GetStudentByRA(ra)
          .then((data) => {
            student.value = data;
            isEditing.value = true;
          })
          .catch((error) => {
            showSnackbarWithMessage(error.response.data, 'red');
          });
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

      if (valid.value) {
        const studentData = {
          name: student.value.name,
          email: student.value.email,
          ra: student.value.ra,
          cpf: student.value.cpf,
        };

        if (!isEditing.value) {
          CreateStudent(studentData)
            .then((data) => {
              showSnackbarWithMessage('Aluno cadastrado com sucesso!', 'green');
              resetForm();
            })
            .catch((error) => {
              showSnackbarWithMessage(error.response.data, 'red');
            });
        } else {
          EditStudent(studentData.ra, studentData)
            .then((data) => {
              showSnackbarWithMessage('Aluno atualizado com sucesso!', 'green');
            })
            .catch((error) => {
              showSnackbarWithMessage(error.response.data, 'red');
            });
        }
      }
    };

    const showSnackbarWithMessage = (message, color) => {
      snackbarMessage.value = message;
      showSnackbar.value = true;
      snackbarColor.value = color
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
      snackbarColor,
      showSnackbar,
      isFormSubmitted,
      setTouched,
      getErrorMessages,
      toolbarTitle,
    };
  },
};
</script>

<style>
.formContainer{
  background-color: #FFFF;
  border-radius: 0px 0px 10px 10px;
  margin-bottom: 30px;
  padding: 30px;
}


</style>