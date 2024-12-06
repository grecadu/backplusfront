import { Component, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { EmployeeService, Employee } from '../../services/employee.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css'],
})
export class EmployeeListComponent implements OnInit {
  employees: Employee[] = [];
  loading = true;

  constructor(private employeeService: EmployeeService, private router: Router,
) { }

  ngOnInit(): void {
    this.loadEmployees();
  }


  ngOnChanges(changes: SimpleChanges): void {
    if (changes['route']) {
      this.employeeService.getEmployees();
    }
  }

  loadEmployees(): void {
    this.employeeService.getEmployees().subscribe(
      (employees) => {
        this.employees = employees;
      },
      (error) => {
        console.error('Error loading employees:', error);
      }
    );
  }

  deleteEmployee(employeeId: string): void {
    if (confirm('Are you sure you want to delete this employee?')) {
      this.employeeService.deleteEmployee(employeeId).subscribe(
        (response) => {
          console.log('Employee deleted successfully!', response);
          this.loadEmployees();
        },
        (error) => {
          console.error('Error deleting employee:', error);
        }
      );
    }
  }
}
