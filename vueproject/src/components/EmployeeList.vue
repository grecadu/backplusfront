<template>
  <div class="employee-list">
    <div class="employee-card"
         v-for="employee in employees"
         :key="employee.id">
      <div class="employee-card-header">
        <h3>{{ employee.firstName }} {{ employee.lastName }}</h3>
        <p>{{ employee.department.description }}</p>
      </div>
      <div class="employee-card-body">
        <p><strong>Phone:</strong> {{ employee.phone }}</p>
        <p><strong>Hire Date:</strong> {{ formatDate(employee.hireDate) }}</p>
      </div>
      <div class="employee-card-footer">
        <router-link :to="{ name: 'EmployeeDetails', params: { id: employee.id } }">
          View Details
        </router-link>
      </div>
    </div>
  </div>
</template>

<script>
  import { ref, onMounted } from 'vue';
  import { fetchEmployees } from '../services/employeeservice';

  export default {
    name: 'EmployeeList',
    setup() {
      const employees = ref([]);

      const loadEmployees = async () => {
        employees.value = await fetchEmployees();
      };

      const formatDate = (dateString) => {
        const date = new Date(dateString);
        return date.toLocaleDateString();
      };

      onMounted(loadEmployees);

      return { employees, formatDate };
    },
  };
</script>

<style scoped>
  .employee-list {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
    gap: 16px;
    padding: 20px;
  }

  .employee-card {
    border: 1px solid #ddd;
    border-radius: 8px;
    padding: 16px;
    background-color: #f9f9f9;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    transition: transform 0.3s ease, box-shadow 0.3s ease;
  }

    .employee-card:hover {
      transform: translateY(-5px);
      box-shadow: 0 6px 12px rgba(0, 0, 0, 0.1);
    }

  .employee-card-header {
    font-size: 1.2em;
    font-weight: bold;
  }

  .employee-card-body {
    margin-top: 10px;
  }

  .employee-card-footer {
    margin-top: 10px;
    text-align: center;
  }

  .employee-card footer a {
    color: #007bff;
    text-decoration: none;
  }

    .employee-card footer a:hover {
      text-decoration: underline;
    }
</style>
