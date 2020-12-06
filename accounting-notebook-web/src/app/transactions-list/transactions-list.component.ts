import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Transaction } from 'src/models/transaction';
import { TransactionsService } from 'src/services/transactions.service';

@Component({
  selector: 'app-transactions-list',
  templateUrl: './transactions-list.component.html',
  styleUrls: ['./transactions-list.component.css']
})
export class TransactionsListComponent implements OnInit {

  constructor(private service: TransactionsService) { }

  public transactions$: Observable<Transaction[]> = new Observable<Transaction[]>();

  ngOnInit(): void {
    this.transactions$ = this.service.getTransactions();
  }

}
