<template>
  <div>
    <h1>Employee Management</h1>
    <EmployeeList @viewDetails="viewEmployeeDetails" />
    <EmployeeDetails v-if="selectedEmployee" :employee="selectedEmployee" @close="closeDetails" />
  </div>
</template>

<script>
import { ref } from 'vue';
import EmployeeList from './EmployeeList.vue';
import EmployeeDetails from './EmployeeDetails.vue';

export default {
  components: {
    EmployeeList,
    EmployeeDetails,
  },
  setup() {
    const selectedEmployee = ref(null);

    const viewEmployeeDetails = (employee) => {
      selectedEmployee.value = employee;
    };

    const closeDetails = () => {
      selectedEmployee.value = null;
    };

    return {
      selectedEmployee,
      viewEmployeeDetails,
      closeDetails,
    };
  },
};
</script>
