<template>
  <div class="page-container">
    <md-app md-waterfall md-mode="fixed">
      <md-app-toolbar class="md-transparent">
        <div class="md-toolbar-section-end">
          <div>
            <md-menu md-align-trigger>
              <md-avatar class="md-avatar-icon" md-menu-trigger>
                <md-icon>person</md-icon>
              </md-avatar>
              <md-menu-content>
                <md-menu-item>
                  <strong>{{profile.UserName}}</strong>
                </md-menu-item>
                <md-divider></md-divider>
                <md-menu-item @click="signOut">
                  <span>Sign Out</span>
                </md-menu-item>
              </md-menu-content>
            </md-menu>
          </div>
        </div>
      </md-app-toolbar>

      <md-app-drawer md-permanent="full">
        <md-toolbar class="md-transparent" md-elevation="0">
          <span class="md-headline">Todo App</span>
        </md-toolbar>
        <md-list :md-expand-single="false">
          <md-list-item md-expand :md-expanded="true">
            <span class="md-list-item-text">Tasks</span>
            <md-list slot="md-expand">
              <md-list-item :to="{ name: 'view-all', params: { listId: -1 } }">
                <md-icon>all_inbox</md-icon>
                <span class="md-list-item-text">All Tasks</span>
              </md-list-item>

              <md-list-item class="menu-item" v-for="list in lists" v-bind:key="list.Id" :to="{ name: 'view-list', params: { listId: list.Id } }">
                <md-icon>label</md-icon>
                <span class="md-list-item-text">{{list.Name}}</span>
              </md-list-item>
            </md-list>
          </md-list-item>
        </md-list>
      </md-app-drawer>
      <md-app-content style="padding: 0px; padding-left: -16px; margin: 0px;">
        <router-view></router-view>
      </md-app-content>
    </md-app>
  </div>
</template>

<script>

import axios from 'axios';

export default {
  name: "App",
  components: {
  },
  data: () => ({
    lists: [],
    profile:
    {

    }
  }),
  async created() {
    await Promise.all([this.getProfile(), this.getLists()]);
  },
  methods: {
    async getProfile()
    {
      const response = await axios.get('api/user/profile');

      if (response.data)
        this.profile = response.data;
    },
    async getLists()
    {
      const response = await axios.get('/api/todo/lists');

      if (response.data)
        this.lists = response.data;
    },
    async signOut()
    {
      const response = await axios.delete('/api/user/session');

      window.location.replace(response.headers.location);
    }
  },
};
</script>

<style lang="scss" scoped>
.md-drawer
{
  width: 230px;
  max-width: calc(100vw - 125px);
}

.md-app
{
  min-height: 100vh;
}

.el-menu /deep/ .md-list-item-content
{
  justify-content: normal;
}

</style>