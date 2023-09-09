import { AfterViewInit, Component, ViewChild, OnInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Branch } from '../../interfaces/branch.interface';
import { CrudService } from '../../services/crud.service';
import { MatDialog } from '@angular/material/dialog';
import { DeleteComponent } from '../dialogs/delete/delete.component';
import { EditComponent } from '../dialogs/edit/edit.component';
import { Observable } from 'rxjs';

@Component({
  selector: 'crud-table',
  templateUrl: './table.component.html',
  styles: [],
})
export class TableComponent implements AfterViewInit,OnInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  dataSource = new MatTableDataSource<Branch>([]);
  branches$?: Observable<Branch[]>;
  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = [
    'branchId',
    'code',
    'description',
    'address',
    'identification',
    'createDate',
    'currencyId',
    'actions',
  ];

  constructor(private service: CrudService, public dialog: MatDialog) {}
  ngOnInit(): void {
    this.service.getBranches$().subscribe(branches => {
      this.dataSource.data = branches;
    });
  }

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  startEdit(branch: Branch) {
    const dialogRef = this.dialog.open(EditComponent, {
      data: { branch },
    });
    dialogRef.afterClosed().subscribe();
  }

  deleteItem(branch: Branch) {
    const dialogRef = this.dialog.open(DeleteComponent, {
      data: { branch },
    });
    dialogRef.afterClosed().subscribe();
  }
}
