import { Component, OnInit } from '@angular/core';
import { RentService } from '../../../services/lent-service/rent.service';

@Component({
  selector: 'app-show-lent-rec',
  templateUrl: './show-lent-rec.component.html',
  styleUrls: ['./show-lent-rec.component.css']
})
export class ShowLentRecComponent implements OnInit {
  lentRecords: any[] = []; // Holds all the records fetched from the API
  isLoading = false;      // Flag to show loading state
  errorMessage: string = ''; // Holds error messages, if any

  constructor(private lentService: RentService) {}

  ngOnInit(): void {
    this.getallrentrecods(); // Fetch all records when the component initializes
  }

<<<<<<< HEAD
  getallrentrecods(): void {
    this.isLoading = true; // Start the loading state
    this.errorMessage = ''; // Clear previous errors

    this.lentService.getallrentrecods().subscribe(
      (response) => {
        if (response && response.data) {
          this.lentRecords = response.data; // Bind the data to the table
          console.log('Fetched records:', this.lentRecords);
        } else {
          console.error('Unexpected response format:', response);
          this.errorMessage = 'Failed to fetch data. Please try again later.';
        }
        this.isLoading = false; // Stop the loading state
      },
      (error) => {
        console.error('Error fetching data:', error);
        this.errorMessage = 'An error occurred while fetching the records.';
        this.isLoading = false; // Stop the loading state
      }
    );
  }
=======
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


>>>>>>> 75fb154a63cbe0d9f0b18c4a59835bd96ce0e28c
}
