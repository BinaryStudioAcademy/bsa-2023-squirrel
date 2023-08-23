import { Component, EventEmitter, Input, OnChanges, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.sass']
})
export class ButtonComponent implements OnChanges{
  @Input() text = '';

  @Input() width = 'auto';
  
  @Input() height = 'auto';

  @Input() padding = "10px 20px";

  @Input() fontSize = '16px';

  @Input() variant = "outline-primary";

  @Input() isDisabled = false;

  @Output() buttonOnClick: EventEmitter<void> = new EventEmitter<void>();

  btnClass = "";

  ngOnChanges(): void {
    switch (this.variant){
      case "outline-primary":{
        this.btnClass = "outline-primary-button";
        break;
      }
      case "outline-secondary":{
        this.btnClass = "outline-secondary-button";
        break;
      }
      case "filled": {
        this.btnClass = "filled-button";
        break;
      }
    }

    if(this.isDisabled == true){
      this.btnClass="disabled-button";
    }
  }

  handleClick(): void{
    this.buttonOnClick.emit();
  }

}
