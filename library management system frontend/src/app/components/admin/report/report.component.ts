import { Component } from '@angular/core';
import { RentService } from '../../../services/lent-service/rent.service';
@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.css']
})
export class ReportComponent {
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
  BookBorrowReport: any = [];
  Lendingcount:any=[];

  selectedReportType: string = 'overall';  // Default selected report
  userId: number = 1; 
  bookId: number = 2; 
  date: any = Date.now; 
  reportData: any; 
  isLoading: boolean = false;
  errorMessage: string = '';


  constructor(private rentService: RentService) {}

  fetchReport(): void {
    this.isLoading = true;
    this.errorMessage = '';

    switch (this.selectedReportType) {
      case 'overall':
        this.rentService.getLentReport(null).subscribe(
          (response) => {
            this.overallreport = response;
            console.log(response);
            this.isLoading = false;
          },
          (error) => {
            this.errorMessage = 'Error fetching overall report';
            this.isLoading = false;
          }
        );
        break;

      case 'lentByUser':
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
        break;

      case 'bookBorrow':
        this.rentService.getBookLendingReport(null).subscribe(
          (response) => {
            this.BookBorrowReport = response.reports;
            console.log(response);
            this.isLoading = false;
          },
          (error) => {
            this.errorMessage = 'Error fetching book lending report';
            this.isLoading = false;
          }
        );
        break;

        case 'bookBorrowbyid':
          this.rentService.getBookLendingReport(this.bookId).subscribe(
            (response) => {
              this.BookBorrowReport =response.reports;
              console.log(response);
              this.isLoading = false;
            },
            (error) => {
              this.errorMessage = 'Error fetching book lending report';
              this.isLoading = false;
            }
          );
          break;

      case 'lentcount':
        this.rentService.getLendingCountReport(null).subscribe(
          (response) => {
            this.Lendingcount = response.countReports;
            console.log(response);
            this.isLoading = false;
          },
          (error) => {
            this.errorMessage = 'Error fetching lending count report';
            this.isLoading = false;
          }
        );
        break;

        case 'lentcountbybookid':
          this.rentService.getLendingCountReport(this.bookId).subscribe(
            (response) => {
              this.Lendingcount = response.countReports;
              console.log(response);
              
              this.isLoading = false;
            },
            (error) => {
              this.errorMessage = 'Error fetching lending count report';
              this.isLoading = false;
            }
          );
          break;

      default:
        this.errorMessage = 'Invalid report type selected';
        this.isLoading = false;
        break;
    }
  }

  downloadReport(): void {
    if (this.selectedReportType == 'overall') {
      if (!this.date) {
        console.error('Invalid date.');
        return;
      }
      this.rentService.getLentReportPdfAll(null).subscribe(
        (pdfBlob: Blob) => {
          const fileURL = URL.createObjectURL(pdfBlob);
          const link = document.createElement('a');
          link.href = fileURL;
          link.download = `LendReport_${new Date().toLocaleString().replace(/[\/:]/g, '-')}.pdf`; // Name file based on current date and time

          link.click();
        },
        (error) => {
          console.error('Error downloading the report:', error);
        }
      );
    }
    
    if(this.selectedReportType == 'lentByUser') {
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

    // else{
    //   alert("select corect option");
    // }
  
  }

}
// Define an interface for each lent record
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