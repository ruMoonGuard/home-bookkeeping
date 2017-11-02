import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'cswFilter'
})
export class FilterPipe implements PipeTransform {
  transform(items: any, value: string, field: string): any {
    if (items.length === 0 || !value) { return items; }

    return items.filter((i) => {
      const obj = Object.assign([], i);

      if (!isNaN(obj[field])) {
        obj[field] = obj[field] + '';
      }

      if (field === 'type') {
        obj[field] = obj[field] === 'income' ? 'доход' : 'расход';
      }

      if (field === 'category') {
        obj[field] = obj.catName;
      }

      return obj[field].toLowerCase().indexOf(value.toLowerCase()) !== -1;
    });
  }
}
