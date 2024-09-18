<!-- views/Register.vue -->
<template>
    <div class="container">
        <h2>Register</h2>
        <form @submit.prevent="register">
            <div class="mb-3">
                <label>Email</label>
                <input v-model="email" type="email" class="form-control" required />
            </div>
            <div class="mb-3">
                <label>Password</label>
                <input v-model="password" type="password" class="form-control" required />
            </div>
            <button type="submit" class="btn btn-primary">Register</button>
            <p>
                Already have an account?
                <router-link to="/">Login here</router-link>
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
    async register() {
      try {
        const response = await axios.post('http://localhost:7042/api/account/register', {
          email: this.email,
          password: this.password,
        });
        localStorage.setItem('token', response.data.token);
        this.$router.push('/users');
      } catch (error) {
        alert('Registration failed.');
      }
    },
  },
};
</script>
