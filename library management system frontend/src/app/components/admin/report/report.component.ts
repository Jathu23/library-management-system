import { Component } from '@angular/core';
@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.css']
})
export class ReportComponent {
  overallreport: OverallReport = {
    "date": "0001-01-01T00:00:00",
    "totalRengings": 4,
    "pending": 3,
    "onTime": 1,
    "later": 0,
    "pendingLent": [
      {
        "id": 2,
        "userId": 1,
        "userName": "Esvaran Jathushan",
        "userEmail": "jathushanj2003@gmail.com",
        "iAdminId": 1,
        "iAdminName": "Esvaran Jathushan",
        "rAdminId": null,
        "rAdminName": null,
        "bookId": 2,
        "bookTitle": "title2",
        "bookISBN": "54",
        "bookAuthor": "author21",
        "bookGenre": "Non-Fiction, Science",
        "bookPublishYear": 5665,
        "bookCopyId": 2,
        "bookCondition": "New",
        "lentDate": "2024-12-11T18:28:05.9408338",
        "dueDate": "2024-12-13T18:28:05.9408338",
        "returnDate": null,
        "status": "1 days 20 hours remaining",
        "statusValue": 2691,
        "maxValue": 2880
      },
      {
        "id": 3,
        "userId": 1,
        "userName": "Esvaran Jathushan",
        "userEmail": "jathushanj2003@gmail.com",
        "iAdminId": 1,
        "iAdminName": "Esvaran Jathushan",
        "rAdminId": null,
        "rAdminName": null,
        "bookId": 1,
        "bookTitle": "title1",
        "bookISBN": "12311",
        "bookAuthor": "author1",
        "bookGenre": "Non-Fiction,Science",
        "bookPublishYear": 1523,
        "bookCopyId": 3,
        "bookCondition": "New",
        "lentDate": "2024-12-11T18:32:20.4132878",
        "dueDate": "2024-12-13T18:32:20.4132878",
        "returnDate": null,
        "status": "1 days 20 hours remaining",
        "statusValue": 2695,
        "maxValue": 2880
      },
      {
        "id": 4,
        "userId": 1,
        "userName": "Esvaran Jathushan",
        "userEmail": "jathushanj2003@gmail.com",
        "iAdminId": 1,
        "iAdminName": "Esvaran Jathushan",
        "rAdminId": null,
        "rAdminName": null,
        "bookId": 1,
        "bookTitle": "title1",
        "bookISBN": "12311",
        "bookAuthor": "author1",
        "bookGenre": "Non-Fiction,Science",
        "bookPublishYear": 1523,
        "bookCopyId": 1,
        "bookCondition": "New",
        "lentDate": "2024-12-11T19:02:10.7712386",
        "dueDate": "2024-12-13T19:02:10.7712386",
        "returnDate": null,
        "status": "1 days 21 hours remaining",
        "statusValue": 2725,
        "maxValue": 2880
      }
    ],
    "onTimeLent": [
      {
        "id": 1,
        "userId": 1,
        "userName": "Esvaran Jathushan",
        "userEmail": "jathushanj2003@gmail.com",
        "iAdminId": 1,
        "iAdminName": "Esvaran Jathushan",
        "rAdminId": 1,
        "rAdminName": "Esvaran Jathushan",
        "bookId": 1,
        "bookTitle": "title1",
        "bookISBN": "12311",
        "bookAuthor": "author1",
        "bookGenre": "Non-Fiction,Science",
        "bookPublishYear": 1523,
        "bookCopyId": 1,
        "bookCondition": "New",
        "lentDate": "2024-12-11T18:26:41.4821258",
        "dueDate": "2024-12-12T18:26:41.4821258",
        "returnDate": "2024-12-11T19:00:23.3491402",
        "status": "On Time",
        "statusValue": 1249,
        "maxValue": 1440
      }
    ],
    "laterLent": []
  }
  lentReportByUserId: Report[] = [];
  allBookBorrowReport: Report[] = [];
  
  selectedReportType: string = 'overall'; // Default report type
  userId: number = 1;  // Default user ID
  bookId: number = 1;  // Default book ID



    // Control bar method to change report type
    onReportTypeChange(): void {
      if (this.selectedReportType === 'overall') {
        
      } else if (this.selectedReportType === 'lentByUser') {
        
      } else if (this.selectedReportType === 'bookBorrow') {
       
      }
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