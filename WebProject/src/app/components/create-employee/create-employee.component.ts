import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Employee, EmployeeService } from '../../services/employee.service';
import { DepartmentService } from '../../services/department.service';

@Component({
  selector: 'app-create-employee',
  templateUrl: './create-employee.component.html',
  styleUrls: ['./create-employee.component.css']
})
export class CreateEmployeeComponent implements OnInit {

  employee: any = {
    firstName: '',
    phone: '',
    address: '',
    departmentId: '',
    hireDate: '' 
  };
  departments: any[] = [];

  constructor(
    private employeeService: EmployeeService,
    private departmentService: DepartmentService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.departmentService.getDepartments().subscribe((departments) => {
      this.departments = departments;
    });
  }

  onSubmit(): void {
    if (this.employee) {
      {
        this.employeeService.createEmployee(this.employee).subscribe(
          (response) => {
            console.log('Employee created successfully!', response);
            this.router.navigate(['/employees']);
          },
          (error) => {
            console.error('Error creating employee:', error);
          }
        );
      }
    }
  }
}
