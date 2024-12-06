import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EmployeeService, Employee } from '../../services/employee.service';
import { DepartmentService, Department } from '../../services/department.service';

@Component({
  selector: 'app-employee-details',
  templateUrl: './employee-details.component.html',
  styleUrls: ['./employee-details.component.css'],
})
export class EmployeeDetailsComponent implements OnInit {
  employee: Employee | null = null;
  departments: Department[] = [];
  selectedDepartmentId: string = "";

  constructor(
    private route: ActivatedRoute,
    private employeeService: EmployeeService,
    private departmentService: DepartmentService
  ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.fetchEmployee(id);
      this.fetchDepartments();
    }
  }

  fetchEmployee(id: string): void {
    this.employeeService.getEmployeeById(id).subscribe({
      next: (data) => (this.employee = data, this.selectedDepartmentId = data.departmentId),
      error: (err) => console.error('Error fetching employee details:', err),
    });
  }

  fetchDepartments(): void {
    this.departmentService.getDepartments().subscribe({
      next: (data) => (this.departments = data),
      error: (err) => console.error('Error fetching departments:', err),
    });
  }

  onDepartmentChange(): void {
    console.log('Selected Department ID:', this.employee);
  }

  updateDepartment(): void {
    if (this.employee?.department?.id) {
      this.employeeService.updateEmployeeDepartment(this.employee)
        .subscribe(
          updatedEmployee => {
            console.log('Employee department updated successfully:', updatedEmployee);
          },
          error => {
            console.error('Error updating employee department:', error);
          }
        );
    } else {
      console.error('Employee or Department is not defined.');
    }
  }
}
