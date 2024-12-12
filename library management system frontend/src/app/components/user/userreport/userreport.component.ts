import { Component } from '@angular/core';
import { RentService } from '../../../services/lent-service/rent.service';
import { environment } from '../../../../environments/environment.testing';

@Component({
  selector: 'app-userreport',
  templateUrl: './userreport.component.html',
  styleUrl: './userreport.component.css'
})
export class UserreportComponent {
  overallreport: OverallReport = {
    date: '',
    totalRengings: 0,
    pending: 0,
    onTime: 0,
    later: 0,
    pendingLent: [],
    onTimeLent: [],
    laterLent: []
  }
  isLoading: boolean = false;
  errorMessage: string = '';
  userId: number = 1; 
  constructor(private rentService: RentService) {
    const tokendata = environment.getTokenData();
    this.userId= Number(tokendata.ID);
  }

  fetchReport(){
    this.rentService.getLentReportByUserId(this.userId).subscribe(
      (response) => {
        this.overallreport = response;
        console.log(response);
        this.isLoading = false;
      },
      (error) => {
        this.errorMessage = 'Error fetching report by user ID';
        this.isLoading = false;
      }
    );
  }
  downloadReport(): void {
      this.rentService.getLentReportPdf(this.userId).subscribe(
        (pdfBlob: Blob) => {
          const fileURL = URL.createObjectURL(pdfBlob);
          const link = document.createElement('a');
          link.href = fileURL;
          link.download = `LendReportuser.pdf`; // Name the file dynamically based on user ID
          link.click();
        },
        (error) => {
          console.error('Error downloading the report:', error);
        }
      );
  
  }


}
interface LentRecord {
  id: number;
  userId: number;
  userName: string;
  userEmail: string;
  iAdminId: number;
  iAdminName: string;
  rAdminId: number | null;
  rAdminName: string | null;
  bookId: number;
  bookTitle: string;
  bookISBN: string;
  bookAuthor: string;
  bookGenre: string;
  bookPublishYear: number;
  bookCopyId: number;
  bookCondition: string;
  lentDate: string;
  dueDate: string;
  returnDate: string | null;
  status: string;
  statusValue: number;
  maxValue: number;
}

// Define an interface for the overall report
interface OverallReport {
  date: string;
  totalRengings: number;
  pending: number;
  onTime: number;
  later: number;
  pendingLent: LentRecord[];
  onTimeLent: LentRecord[];
  laterLent: LentRecord[];
}