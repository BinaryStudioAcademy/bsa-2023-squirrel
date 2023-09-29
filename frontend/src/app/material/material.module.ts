import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';

@NgModule({
    declarations: [],
    imports: [
        CommonModule,
        MatButtonModule,
        MatToolbarModule,
        MatIconModule,
        MatCardModule,
        MatAutocompleteModule,
        MatChipsModule,
        MatDialogModule,
        MatTableModule,
        MatMenuModule,
        MatFormFieldModule,
        MatInputModule,
        MatSelectModule,
    ],
    exports: [
        CommonModule,
        MatButtonModule,
        MatToolbarModule,
        MatIconModule,
        MatCardModule,
        MatAutocompleteModule,
        MatChipsModule,
        MatDialogModule,
        MatTableModule,
        MatMenuModule,
        MatFormFieldModule,
        MatInputModule,
        MatSelectModule,
    ],
})
export class MaterialModule {}
