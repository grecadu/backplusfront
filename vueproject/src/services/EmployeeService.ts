import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'https://localhost:7215/api/',
  headers: {
    'Content-Type': 'application/json',
  },
});

export const fetchEmployees = async () => {
  try {
    const response = await apiClient.get('/Employee');
    return response.data;
  } catch (error) {
    console.error('Error fetching employees:', error);
    throw error;
  }
};

export const fetchEmployeeDetails = async (id:string) => {
  try {
    const response = await apiClient.get(`/Employee/${id}`);
    return response.data;
  } catch (error) {
    console.error('Error fetching employee details:', error);
    throw error;
  }

};

export const fetchDepartments = async () => {
  const response = await apiClient.get('/Department');
  return response.data;
};

export const updateEmployeeDepartment = async (employee) => {
  const response = await apiClient.put(`/Employee/${employee.id}`, employee);
  return response.data;
};
