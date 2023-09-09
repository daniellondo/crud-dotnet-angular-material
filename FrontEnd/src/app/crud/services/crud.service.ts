import { Injectable, OnInit } from '@angular/core';
import { environments } from 'src/environments/environments';
import { Observable, Subject, catchError, throwError } from 'rxjs';
import {
  EndpointBranchResponse,
  EndpointCurrencyResponse,
} from '../interfaces/response.interface';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Branch } from '../interfaces/branch.interface';

@Injectable({
  providedIn: 'root',
})
export class CrudService {
  private readonly API_URL = `${environments.baseUrl}Branch`;
  private readonly API_URL_Currencies = `${environments.baseUrl}Currency`;
  private branches$ = new Subject<Branch[]>();
  private branches: Branch[] = [];

  constructor(private http: HttpClient) {
    this.refresh();
  }

  refresh(){
    this.getAllBranchs().subscribe((data) => {
      this.branches.push(...data.result);
      this.branches$.next(this.branches);
    });
  }

  getBranches$(): Observable<Branch[]> {
    return this.branches$.asObservable();
  }

  getAllBranchs(id?: string): Observable<EndpointBranchResponse> {
    const url = !id ? '' : `?BranchId=${id}`;
    return this.http.get<EndpointBranchResponse>(`${this.API_URL}${url}`);
  }

  getAllCurrencies(): Observable<EndpointCurrencyResponse> {
    return this.http
      .get<EndpointCurrencyResponse>(`${this.API_URL_Currencies}`)
      .pipe(catchError(this.handleError));
  }

  addBranch(branch: Branch): void {
    this.http
      .post<EndpointBranchResponse>(`${this.API_URL}`, branch)
      .pipe(catchError(this.handleError))
      .subscribe(data =>{
        this.branches.push(...data.result);
        this.branches$.next(this.branches);
      });
  }

  updateBranch(branch: Branch): void {
    if (!branch.branchId) throw Error('Id is required');
    this.http
      .put<EndpointBranchResponse>(`${this.API_URL}`, branch)
      .pipe(catchError(this.handleError))
      .subscribe((data) => console.log(data));
    this.branches[this.branches.findIndex((obj) => obj.branchId == branch.branchId)] = branch;
    this.branches$.next(this.branches);
  }

  deleteBranch(id: string):void{
    this.http
      .delete<EndpointBranchResponse>(`${this.API_URL}?BranchId=${id}`)
      .pipe(catchError(this.handleError))
      .subscribe(result => console.log(result));
      this.branches = this.branches.filter(
        (item) => item.branchId != id
      );
      this.branches$.next(this.branches);
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, body was: `,
        error.error
      );
    }
    // Return an observable with a user-facing error message.
    return throwError(
      () => new Error('Something bad happened; please try again later.')
    );
  }
}
