<template>
  <div v-if="employee" class="employee-details">
    <!-- Header with name and photo -->
    <div class="employee-header">
      <div class="employee-photo-container">
        <img v-if="employee.photoUrl" :src="employee.photoUrl" alt="Employee Photo" class="employee-photo" />
      </div>
      <div class="employee-header-info">
        <h2>{{ employee.firstName }} {{ employee.lastName }}</h2>
        <p class="department">{{ employee.department.description }}</p>
      </div>
    </div>

    <!-- Card of details -->
    <div class="employee-card">
      <div class="employee-card-body">
        <div class="detail-item">
          <i class="fas fa-phone"></i>
          <p><strong>Phone:</strong> {{ employee.phone }}</p>
        </div>
        <div class="detail-item">
          <i class="fas fa-map-marker-alt"></i>
          <p><strong>Address:</strong> {{ employee.address }}</p>
        </div>
        <div class="detail-item">
          <i class="fas fa-calendar-day"></i>
          <p><strong>Hire Date:</strong> {{ formatDate(employee.hireDate) }}</p>
        </div>
        <!-- Department Select -->
        <div class="detail-item">
          <i class="fas fa-building"></i>
          <label for="department-select"><strong>Change Department:</strong></label>
          <select v-model="selectedDepartment" id="department-select">
            <option v-for="department in departments" :key="department.id" :value="department.id">
              {{ department.description }}
            </option>
          </select>
        </div>
      </div>
    </div>

    <!-- Save Button -->
    <div class="employee-footer">
      <button @click="updateDepartment" class="btn-update">
        Save Changes
      </button>
      <router-link to="/">Back to Employee List</router-link>
    </div>
  </div>
  <div v-else>
    <p>Loading...</p>
  </div>
</template>

<script>
  import { ref, onMounted } from 'vue';
  import { fetchEmployeeDetails, fetchDepartments, updateEmployeeDepartment } from '../services/EmployeeService';

  export default {
    name: 'EmployeeDetails',
    props: {
      id: {
        type: String,
        required: true,
      },
    },
    setup(props) {
      const employee = ref(null);
      const departments = ref([]);
      const selectedDepartment = ref(null);

      // Fetch employee details and departments
      const loadEmployeeDetails = async () => {
        try {
          employee.value = await fetchEmployeeDetails(props.id);
          // Set the selected department to the employee's current departmentId
          selectedDepartment.value = employee.value.department.id;
        } catch (error) {
          console.error('Error fetching employee details:', error);
        }
      };

      const loadDepartments = async () => {
        try {
          departments.value = await fetchDepartments();
        } catch (error) {
          console.error('Error fetching departments:', error);
        }
      };

      const formatDate = (dateString) => {
        const date = new Date(dateString);
        return date.toLocaleDateString();
      };

      const updateDepartment = async () => {
        if (selectedDepartment.value !== employee.value.department.id) {
          // Update the departmentId in the employee object
          employee.value.departmentId = selectedDepartment.value;
          // Find the selected department description to update the employee's department object
          const selectedDept = departments.value.find(dept => dept.id === selectedDepartment.value);
          employee.value.department.description = selectedDept.description;

          try {
            // Send the entire employee object with the updated departmentId to the API
            await updateEmployeeDepartment(employee.value);
            alert('Department updated successfully!');
          } catch (error) {
            console.error('Error updating department:', error);
            alert('Failed to update department');
          }
        }
      };

      onMounted(() => {
        loadEmployeeDetails();
        loadDepartments();
      });

      return { employee, departments, selectedDepartment, formatDate, updateDepartment };
    },
  };
</script>

<style scoped>
  .employee-details {
    max-width: 800px;
    margin: 0 auto;
    padding: 20px;
  }

  .employee-header {
    display: flex;
    align-items: center;
    margin-bottom: 20px;
  }

  .employee-photo-container {
    margin-right: 20px;
  }

  .employee-photo {
    width: 100px;
    height: 100px;
    border-radius: 50%;
    object-fit: cover;
    border: 2px solid #ddd;
  }

  .employee-header-info {
    flex: 1;
  }

    .employee-header-info h2 {
      margin: 0;
      font-size: 2rem;
    }

  .department {
    font-size: 1.1rem;
    color: #555;
  }

  .employee-card {
    background-color: #fff;
    padding: 20px;
    border-radius: 8px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  }

  .employee-card-body {
    padding: 10px 0;
  }

  .detail-item {
    display: flex;
    align-items: center;
    margin-bottom: 15px;
  }

    .detail-item i {
      font-size: 1.5rem;
      color: #007bff;
      margin-right: 10px;
    }

    .detail-item p {
      margin: 0;
      font-size: 1.1rem;
    }

  select {
    padding: 8px;
    border-radius: 4px;
    border: 1px solid #ccc;
    font-size: 1rem;
  }

  .employee-footer {
    margin-top: 20px;
    text-align: center;
  }

    .employee-footer a {
      color: #007bff;
      text-decoration: none;
    }

      .employee-footer a:hover {
        text-decoration: underline;
      }

  .btn-update {
    background-color: #28a745;
    color: white;
    padding: 10px 15px;
    border-radius: 5px;
    border: none;
    cursor: pointer;
  }

    .btn-update:hover {
      background-color: #218838;
    }
</style>
