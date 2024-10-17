<template>
  <v-app>
    <v-navigation-drawer app>
      <v-list dense>
        <v-list-item>
          <v-list-item-title>Módulo Acadêmico</v-list-item-title>
        </v-list-item>
        <v-divider></v-divider>
        <v-list-item>
          <v-list-item-title>Alunos</v-list-item-title>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>

    <!-- Área principal onde o conteúdo será inserido -->
    <v-main>
      <v-container>
        <slot></slot>
      </v-container>
    </v-main>
  </v-app>
</template>

<script>
export default {
  name: 'LayoutBase',
};
</script>

<style scoped>
.v-navigation-drawer {
  width: 200px;
}
</style>