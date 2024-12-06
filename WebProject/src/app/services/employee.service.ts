import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  private apiUrl = 'https://localhost:7215/api/Employee';
  private apiKey = '20871d1806144c4e802fbb9961ef4862';

  constructor(private http: HttpClient) { }

  getEmployeeById(id: string): Observable<Employee> {
    console.log("in the service")
    return this.http.get<Employee>(`${this.apiUrl}/${id}`);
  }


  getEmployees(): Observable<Employee[]> {
    const headers = new HttpHeaders().set('x-api-key', this.apiKey);

    return this.http.get<Employee[]>(this.apiUrl, { headers }).pipe(
      map((employees) =>
        employees.map((employee) => ({
          ...employee,
          duration: this.calculateDuration(new Date(employee.hireDate)),
        }))
      )
    );
  }

  updateEmployeeDepartment(employee: Employee): Observable<Employee> {
    return this.http.put<Employee>(`${this.apiUrl}/${employee.id}`, employee);
  }

  createEmployee(employee: Employee): Observable<Employee> {
    return this.http.post<Employee>(this.apiUrl, employee);
  }

  deleteEmployee(employeeId: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${employeeId}`);
  }


  private calculateDuration(hireDate: Date): string {
    const now = new Date();
    const diffYears = now.getFullYear() - hireDate.getFullYear();
    const diffMonths = now.getMonth() - hireDate.getMonth();
    const diffDays = now.getDate() - hireDate.getDate();

    let years = diffYears;
    let months = diffMonths;
    let days = diffDays;

    if (days < 0) {
      const prevMonth = new Date(now.getFullYear(), now.getMonth(), 0);
      days += prevMonth.getDate();
      months--;
    }
    if (months < 0) {
      months += 12;
      years--;
    }

    return `${years} years, ${months} months, ${days} days`;
  }
}

export interface Employee {
  id: string;
  firstName: string;
  hireDate: Date;
  duration?: string;
  phone: string;
  address: string;
  departmentId: string;
  department: {
    id: string;
    description: string;
  };
}
