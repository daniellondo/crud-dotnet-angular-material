import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Currency } from 'src/app/crud/interfaces/currency.interface';
import { CrudService } from '../../../services/crud.service';
import { TableComponent } from '../../table/table.component';

@Component({
  selector: 'crud-dialogs-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css'],
})
export class AddComponent {
  minDate = new Date();
  public currencies: Currency[] = [];

  public branchform: FormGroup = this.fb.group({
    code: ['', [Validators.required]],
    description: ['', [Validators.required, Validators.maxLength(250)]],
    address: ['', [Validators.required, Validators.maxLength(250)]],
    identification: ['', [Validators.required, Validators.maxLength(50)]],
    createDate: ['', [Validators.required]],
    currencyId: ['', [Validators.required]],
  });

  constructor(private fb: FormBuilder, public dataService: CrudService) {}

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

  addBranch() {
    this.dataService.addBranch(this.branchform.value);
  }

  onSubmit(): void {
    if (this.branchform.invalid) {
      this.branchform.markAllAsTouched();
      return;
    }
    this.branchform.reset();
  }
}
