import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { Transaction } from 'src/models/transaction';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TransactionsService {

  constructor(private httpClient: HttpClient) { }

  public getTransactions(): Observable<Transaction[]> {
    const url = environment.baseApiUrl + 'transactions';

    return this.httpClient.get<Transaction[]>(url);
  }
}
