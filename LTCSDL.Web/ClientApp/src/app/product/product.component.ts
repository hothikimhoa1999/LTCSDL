import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

//khai báo để sử dụng jquery
declare var $:any;


@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  products:any = {
    data: [],
    totalRecord:0,
    page:0,
    size:5,
    totalPage:0
  }

// khai báo lấy các thông tin từ cột nào từ 2 bảng product và category
product:any = {
  productId : 1,
  productName : "Laptop Asus",
  categoryId : 1,
  supplierId : 1,
  unitPrice : 15000000,
  quantityPerUnit: "cai"
}

isEdit : boolean = true;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { 
  }

  ngOnInit() {
    this.searchProduct(1);
  }

//lấy data từ swagger về local host 4200

  searchProduct(cPage) {
    let x= {
      page:cPage,
      size:5,
      keyword:""
    }
    this.http.post('https://localhost:44369/api/Products/search-product',x).subscribe(result => {
      this.products = result;
      this.products = this.products.data;
      console.log(this.products);
    }, error => console.error(error));
  }

  // viết hàm bấm nextpage
  searchNext() {
    if(this.products.page <this.products.totalPages)
    {
      let nextPage = this.products.page +1;
      let x= {
        page:nextPage,
        size:5,
        keyword:""
      }
      this.http.post('https://localhost:44369/api/Products/search-product',x).subscribe(result => {
        this.products = result;
        this.products = this.products.data;
      }, error => console.error(error));
    } else {
      alert("This is last page");
    }
  }

  //viết hàm bấm previous page
  searchPrevious() {
    if(this.products.page > 1  )
    {
      let previousPage = this.products.page - 1;
      let x= {
        page:previousPage,
        size:5,
        keyword:""
      }
      this.http.post('https://localhost:44369/api/Products/search-product',x).subscribe(result => {
        this.products = result;
        this.products = this.products.data;
      }, error => console.error(error));
    } else {
      alert("this is first page");
    }
  }

  // tạo hàm hiện ra những thông tin khi hàm openModal có trong modal được gọi
  openModal(isNew, index) {
    if(isNew) {
      this.isEdit = false;
      this.product = {
        productId: 0,
        productName : "",
        categoryId : 1,
        supplierId : 1,
        quantityPerUnit: "",
        unitPrice : 0
      }
    }
    else {
      this.isEdit = true;
      this.product = index;
    }
    //mở popup bằng jquery
    $('#exampleModal').modal("show");
  }
// Tajo hàm để làm nút add
  addProduct() {
    var x = this.product;
    this.http.post('https://localhost:44369/api/Products/create-product',x).subscribe(result => {
    var res:any = result;
    if(res.success) {
    this.product = res.data;
    this.isEdit = true;
    this.searchProduct(1);
    alert("New products have been added successfully!");
    }  
    }, error => console.error(error));
  }

  // Tạo hàm làm nút save
  updateProduct() {
    var x = this.product;
    this.http.post('https://localhost:44369/api/Products/update-product',x).subscribe(result => {
    var res:any = result;
    if(res.success) {
    this.product = res.data;
    this.isEdit = true;
    this.searchProduct(1);
    alert("New products have been saved successfully!");
    }  
    }, error => console.error(error));
  }

}


