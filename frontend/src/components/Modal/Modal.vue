<template>
  <v-dialog v-model="isOpen" width="auto">
    <v-card max-width="400">
      <v-card-title class="text-h5 font-weight-bold">
        {{ title }}
      </v-card-title>
      
      <v-card-text>
        {{ content }}
      </v-card-text>
      
      <v-card-actions>
        <v-btn color="red darken-1" variant="elevated" @click="confirm">
        {{ confirmText }}
        </v-btn>

        <v-btn color="darken-1" @click="close">
          {{ cancelText }}
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  props: {
    modelValue: {
      type: Boolean,
      default: false
    },
    title: {
      type: String,
      default: 'Título Padrão'
    },
    content: {
      type: String,
      default: 'Conteúdo do modal'
    },
    cancelText: {
      type: String,
      default: 'Cancelar'
    },
    confirmText: {
      type: String,
      default: 'Confirmar'
    }
  },
  computed: {
    isOpen: {
      get() {
        return this.modelValue;
      },
      set(value) {
        this.$emit('update:modelValue', value);
      }
    }
  },
  methods: {
    close() {
      this.$emit('update:modelValue', false);
    },
    confirm() {
      this.$emit('confirm');
    }
  }
}
</script>
