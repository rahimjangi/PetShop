import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Product } from './models/product';
import { Pagination } from './models/pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'PetShop';
  products: Product[] = [];
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http
      .get<Pagination<Product[]>>('https://localhost:5001/api/Products')
      .subscribe({
        next: (response) => {
          console.log(response);
          this.products = response.data;
        },
        error: (err) => {
          console.log(err);
        },
      });
  }
}
