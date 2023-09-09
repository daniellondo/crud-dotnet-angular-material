import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableComponent } from './components/table/table.component';
import { MaterialModule } from '../material/material.module';
import { AddComponent } from './components/dialogs/add/add.component';
import { DeleteComponent } from './components/dialogs/delete/delete.component';
import { ReactiveFormsModule } from '@angular/forms';
import { EditComponent } from './components/dialogs/edit/edit.component';

@NgModule({
  declarations: [
    TableComponent,
    AddComponent,
    DeleteComponent,
    EditComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    ReactiveFormsModule,
  ],
  exports:[
    TableComponent,
    AddComponent
  ]
})
export class CrudModule { }
