import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.sass']
})
export class ButtonComponent {

  @Input() text = '';

  @Input() width = 'auto';
  
  @Input() height = 'auto';

  @Input() fontSize = '16px';

  @Input() btnClass = "";

  @Output() buttonOnClick: EventEmitter<void> = new EventEmitter<void>();

  handleClick(): void{
    this.buttonOnClick.emit();
  }

}
