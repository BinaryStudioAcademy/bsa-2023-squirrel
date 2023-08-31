import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';

@NgModule({
    declarations: [],
    imports: [CommonModule, MatButtonModule, MatToolbarModule, MatIconModule, MatCardModule, MatDialogModule],
    exports: [CommonModule, MatButtonModule, MatToolbarModule, MatIconModule, MatCardModule, MatDialogModule],
})
export class MaterialModule {}
