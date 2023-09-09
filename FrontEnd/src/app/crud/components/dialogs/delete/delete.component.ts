import { Component, Inject } from '@angular/core';
import { CrudService } from '../../../services/crud.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-delete',
  templateUrl: './delete.component.html',
  styleUrls: ['./delete.component.css'],
})
export class DeleteComponent {
  constructor(
    public dialogRef: MatDialogRef<DeleteComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private service: CrudService
  ) {
  }

  confirmDelete(): void {
    this.service.deleteBranch(this.data.branch.branchId);
    this.dialogRef.close(true);
  }
}
