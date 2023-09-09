import { Component, Inject } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Currency } from 'src/app/crud/interfaces/currency.interface';
import { CrudService } from 'src/app/crud/services/crud.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styles: [
  ]
})
export class EditComponent {
  minDate = new Date();
  public currencies: Currency[] = [];

  public branchform: FormGroup = this.fb.group({
    code: [this.data.branch.code, [Validators.required]],
    description: [this.data.branch.description, [Validators.required, Validators.maxLength(250)]],
    address: [this.data.branch.address, [Validators.required, Validators.maxLength(250)]],
    identification: [this.data.branch.identification, [Validators.required, Validators.maxLength(50)]],
    createDate: [this.data.branch.createDate, [Validators.required]],
    currencyId: ['', [Validators.required]],
  });

  constructor(
    public dialogRef: MatDialogRef<EditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    public dataService: CrudService,
  ) {}

  ngOnInit(): void {
    this.dataService
      .getAllCurrencies()
      .subscribe((data) => (this.currencies = data.result));
  }

  isValidField(field: string): boolean | null {
    return (
      this.branchform.controls[field].errors &&
      this.branchform.controls[field].touched
    );
  }

  getFieldError(field: string): string | null {
    if (!this.branchform.controls[field]) return null;

    const errors = this.branchform.controls[field].errors || {};

    for (const key of Object.keys(errors)) {
      switch (key) {
        case 'required':
          return 'Este campo es requerido';

        case 'minlength':
          return `Mínimo ${errors['minlength'].requiredLength} caracters.`;
      }
    }

    return null;
  }

  editBranch() {
    this.branchform.value.branchId = this.data.branch.branchId;
    this.dataService.updateBranch(this.branchform.value);
    this.dialogRef.close(this.branchform.value);
  }

  onSubmit(): void {
    if (this.branchform.invalid) {
      this.branchform.markAllAsTouched();
      return;
    }
    this.branchform.reset();
  }
}
