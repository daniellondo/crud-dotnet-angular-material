import { Component, inject } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable, map, shareReplay } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { AddComponent } from 'src/app/crud/components/dialogs/add/add.component';

@Component({
  selector: 'shared-side-menu',
  templateUrl: './side-menu.component.html',
  styleUrls: ['./side-menu.component.css'],
})
export class SideMenuComponent {
  private breakpointObserver = inject(BreakpointObserver);
  showTable : boolean = true;
  constructor(public dialog: MatDialog) {}
  isHandset$: Observable<boolean> = this.breakpointObserver
    .observe(Breakpoints.Handset)
    .pipe(
      map((result) => result.matches),
      shareReplay()
    );

  openAddDialog(): void {
    const dialogRef = this.dialog.open(AddComponent);
    dialogRef.afterClosed().subscribe((result) => {
    });

  }
}
