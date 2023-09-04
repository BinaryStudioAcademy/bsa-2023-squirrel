import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'formatDate',
})
export class FormatDatePipe implements PipeTransform {
    transform(value: any, format: string = 'dd-MM-yyyy'): string {
        let dateValue = value;

        if (!(dateValue instanceof Date)) {
            dateValue = new Date(dateValue);
        }

        if (Number.isNaN(dateValue.getTime())) {
            return '';
        }

        const day = dateValue.getDate().toString().padStart(2, '0');
        const month = (dateValue.getMonth() + 1).toString().padStart(2, '0');
        const year = dateValue.getFullYear();

        return format.replace('dd', day).replace('MM', month).replace('yyyy', year.toString());
    }
}
