import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'calculateExperience',
})
export class CalculateExperiencePipe implements PipeTransform {
  transform(hireDate: Date): string {
    const hire = new Date(hireDate);
    const now = new Date();

    const years = now.getFullYear() - hire.getFullYear();
    const months = now.getMonth() - hire.getMonth();
    const days = now.getDate() - hire.getDate();

    return `${years} years, ${Math.abs(months)} months, ${Math.abs(days)} days`;
  }
}
