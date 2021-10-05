<template>
  <div class="page-container">
    <md-app md-mode="fixed">
      <md-app-content>
        <form novalidate class="md-layout sign-in-form" @submit.prevent="signIn">
          <div
            class="md-layout-item card-list"
            style="max-width: 768px; margin: 0 auto;">
            <md-card class="md-elevation-10">
              <md-card-header>
                <h1 class="md-title">Sign In</h1>
              </md-card-header>
              <md-card-content>
                <md-field>
                  <md-icon>person</md-icon>
                  <label>User Name</label>
                  <md-input v-model="username" name="userName"></md-input>
                </md-field>
                <md-field>
                  <md-icon>password</md-icon>
                  <label>Password</label>
                  <md-input type="password" v-model="password" name="password"></md-input>
                </md-field>
                <div class="error" :style="{visibility: showError ? 'visible' : 'hidden'}">
                  <span>Invalid username and/or password.</span>
                </div>
                <div style="min-height: 5px;">
                  <md-progress-bar md-mode="indeterminate" v-if="sending" />
                </div>
              </md-card-content>
              <md-card-actions>
                <md-button type="submit" class="md-primary" :disabled="!canSignIn()">Sign In</md-button>
              </md-card-actions>
            </md-card>
          </div>
        </form>
      </md-app-content>
    </md-app>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: "App",
  data: () => ({
    showError: false,
    sending: false,
    username: '',
    password: ''
  }),
  methods: {
    canSignIn()
    {
      return !this.sending && this.username.length > 0 && this.password.length > 0;
    },
    async setToken()
    {
      const response = await axios.post('api/user/session/token');

      if (response.status == 200)
        window.localStorage.setItem('request-token', response.data.Token);
    },
    async signIn()
    {
      try
      {
        this.showError = false;
        this.sending = true;
        const response = await axios.post('api/user/session', { userName: this.username, password: this.password });

        if (response.status == 200)
        {
          await this.setToken();
          window.location.replace(response.data.url);
        }
      }

      catch(error)
      {
        if (error.response && error.response.status == 401)
          this.showError = true;
      }

      finally
      {
        this.sending = false;
      }
    }
  },
};
</script>

<style lang="scss" scoped>
.progress {
  margin-left: -16px;
  margin-right: -16px;
  margin-top: -16px;
  margin-bottom: 16px;
}

.error
{
  text-align: right;
  color: red;
}

.md-content {
  min-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
}

.sign-in-form {
  width: 100%;
  max-width: 500px;
}

.md-card {
  border-top-left-radius: 8px;
  border-top-right-radius: 8px;
  border-bottom-left-radius: 8px;
  border-bottom-right-radius: 8px;
}
</style>

<style lang="scss">
.md-radio .md-radio-label {
  height: auto;
}
</style>