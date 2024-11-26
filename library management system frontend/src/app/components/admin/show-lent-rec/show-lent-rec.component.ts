import { Component, OnInit } from '@angular/core';
import { RentService } from '../../../services/lent-service/rent.service';

@Component({
  selector: 'app-show-lent-rec',
  templateUrl: './show-lent-rec.component.html',
  styleUrl: './show-lent-rec.component.css'
})
export class ShowLentRecComponent implements OnInit {
  lentrec: any[] = [];
  isLoading = false;
  userId :number =1;

constructor(private lentservice:RentService){}
  ngOnInit(): void {
   this.getallrentrecods();
  }

lodadrecodes(){
  this.lentservice.getlentrecByuserid(this.userId).subscribe(
(response) =>{
  const result = response.data;
  console.log(result);
  
},
(error) =>{
console.log(error);

}
  );
}

getallrentrecods(){
  this.lentservice.getallrentrecods().subscribe(
(response) =>{
  const result = response.data;
  console.log(result);
  
},
(error) =>{
console.log(error);

}
);
}


}
