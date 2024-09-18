<!-- views/UserManagement.vue -->
<template>
    <div class="container">
        <h2>User Management</h2>
        <div class="mb-2">
            <button @click="blockUsers" class="btn btn-danger">Block</button>
            <button @click="unblockUsers" class="btn btn-secondary">Unblock</button>
            <button @click="deleteUsers" class="btn btn-secondary">Delete</button>
            <button @click="logout" class="btn btn-link">Logout</button>
        </div>
        <table class="table table-bordered">
            <thead>
                <tr>
                    <th><input type="checkbox" @change="selectAll" v-model="allSelected" /></th>
                    <th>ID</th>
                    <th>Email</th>
                    <th>Registration Date</th>
                    <th>Last Login Date</th>
                    <th>Status</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="user in users" :key="user.id">
                    <td><input type="checkbox" v-model="selectedUsers" :value="user.id" /></td>
                    <td>{{ user.id }}</td>
                    <td>{{ user.email }}</td>
                    <td>{{ formatDate(user.registrationDate) }}</td>
                    <td>{{ formatDate(user.lastLoginDate) }}</td>
                    <td>{{ user.isBlocked ? 'Blocked' : 'Active' }}</td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      users: [],
      selectedUsers: [],
      allSelected: false,
    };
  },
  async created() {
    await this.fetchUsers();
  },
  methods: {
    async fetchUsers() {
      try {
        const token = localStorage.getItem('token');
        const response = await axios.get('http://localhost:7042/api/users', {
          headers: { Authorization: `Bearer ${token}` },
        });
        this.users = response.data;
      } catch (error) {
        alert('Session expired or user is blocked.');
        localStorage.removeItem('token');
        this.$router.push('/');
      }
    },
    formatDate(dateString) {
      const date = new Date(dateString);
      return date.toLocaleString();
    },
    selectAll() {
      if (this.allSelected) {
        this.selectedUsers = this.users.map((user) => user.id);
      } else {
        this.selectedUsers = [];
      }
    },
    async blockUsers() {
      await this.updateUserStatus('block');
    },
    async unblockUsers() {
      await this.updateUserStatus('unblock');
    },
    async deleteUsers() {
      await this.updateUserStatus('delete');
    },
    async updateUserStatus(action) {
      try {
        const token = localStorage.getItem('token');
        await axios.post(`http://localhost:7042/api/users/${action}`, this.selectedUsers, {
          headers: { Authorization: `Bearer ${token}` },
        });

        // If current user is affected, log out
        const currentUserId = this.getCurrentUserIdFromToken();
        if (this.selectedUsers.includes(currentUserId)) {
          localStorage.removeItem('token');
          this.$router.push('/');
        } else {
          await this.fetchUsers();
          this.selectedUsers = [];
          this.allSelected = false;
        }
      } catch (error) {
        alert('Action failed.');
      }
    },
    getCurrentUserIdFromToken() {
      const token = localStorage.getItem('token');
      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload.sub;
    },
    logout() {
      localStorage.removeItem('token');
      this.$router.push('/');
    },
  },
};
</script>
