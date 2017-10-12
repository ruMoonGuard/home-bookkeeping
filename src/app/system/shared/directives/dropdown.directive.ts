import { Directive, HostBinding, HostListener } from '@angular/core';

@Directive({
  selector: '[cswDropdown]'
})
export class DropdownDirective {
  @HostBinding('class.open') isOpen = false;

  @HostListener('click') dropdown() {
    this.isOpen = !this.isOpen;
  }
}
