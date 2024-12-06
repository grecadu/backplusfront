import { createRouter, createWebHistory } from 'vue-router';
import EmployeeList from './components/EmployeeList.vue';
import EmployeeDetails from './components/EmployeeDetails.vue';

const routes = [
  {
    path: '/',
    name: 'EmployeeList',
    component: EmployeeList,
  },
  {
    path: '/employee/:id',
    name: 'EmployeeDetails',
    component: EmployeeDetails,
    props: true,  // Allows passing the ID as a prop to the EmployeeDetails component
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
