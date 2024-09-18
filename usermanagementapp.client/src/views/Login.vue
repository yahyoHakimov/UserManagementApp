<!-- views/Login.vue -->
<template>
    <div class="container">
        <h2>Login</h2>
        <form @submit.prevent="login">
            <div class="mb-3">
                <label>Email</label>
                <input v-model="email" type="email" class="form-control" required />
            </div>
            <div class="mb-3">
                <label>Password</label>
                <input v-model="password" type="password" class="form-control" required />
            </div>
            <button type="submit" class="btn btn-primary">Login</button>
            <p>
                Don't have an account?
                <router-link to="/register">Register here</router-link>
            </p>
        </form>
    </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      email: '',
      password: '',
    };
  },
  methods: {
    async login() {
      try {
        const response = await axios.post('http://localhost:7042/api/account/login', {
          email: this.email,
          password: this.password,
        });
        localStorage.setItem('token', response.data.token);
        this.$router.push('/users');
      } catch (error) {
        alert('Invalid credentials or user is blocked.');
      }
    },
  },
};
</script>
