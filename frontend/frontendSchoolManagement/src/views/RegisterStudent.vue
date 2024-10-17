<template>
  <LayoutBase>
    <!-- Barra de ferramentas -->
    <Toolbar title="Cadastro de aluno"></Toolbar>

    <!-- Formulário de cadastro -->
    <v-form ref="form" v-model="valid" lazy-validation>
      <v-row>
        <v-col cols="12">
          <v-text-field
            v-model="student.name"
            label="Nome"
            :rules="[rules.required]"
            placeholder="Informe o nome completo"
          ></v-text-field>
        </v-col>

        <v-col cols="12">
          <v-text-field
            v-model="student.email"
            label="E-mail"
            :rules="[rules.required, rules.email]"
            placeholder="Informe apenas um e-mail"
          ></v-text-field>
        </v-col>

        <v-col cols="12">
          <v-text-field
            v-model="student.ra"
            label="RA"
            :rules="[rules.required]"
            placeholder="Informe o registro acadêmico"
          ></v-text-field>
        </v-col>

        <v-col cols="12">
          <v-text-field
            v-model="student.cpf"
            label="CPF"
            :rules="[rules.required, rules.cpf]"
            placeholder="Informe o número do documento"
          ></v-text-field>
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
import LayoutBase from '../components/BaseLayout/BaseLayout.vue'
import Toolbar from '../components/Toolbar/Toolbar.vue'

export default {
  components: {
    LayoutBase,
    Toolbar
  },
  data() {
    return {
      valid: false,
      student: {
        name: '',
        email: '',
        ra: '',
        cpf: '',
      },
      rules: {
        required: value => !!value || 'Este campo é obrigatório.',
        email: value => {
          const pattern = /^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$/;
          return pattern.test(value) || 'E-mail inválido.';
        },
        cpf: value => {
          const pattern = /^\d{3}\.\d{3}\.\d{3}-\d{2}$/;
          return pattern.test(value) || 'CPF inválido.';
        },
      },
    };
  },
  methods: {
    saveForm() {
      if (this.$refs.form.validate()) {
        console.log('Dados salvos:', this.student);
      }
    },
  },
};
</script>