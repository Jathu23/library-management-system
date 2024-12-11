import { Component } from '@angular/core';
import { RentService } from '../../../services/lent-service/rent.service';
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
  lentReportByUserId: any= [];
  allBookBorrowReport: any = [
    {
      "bookId": 1,
      "bookTitle": "title1",
      "isbn": "12311",
      "author": "author1",
      "bookRentDetails": [
        {
          "bookCopyId": 1,
          "userName": "Esvaran Jathushan",
          "issuingAdmin": "Esvaran Jathushan",
          "receivingAdmin": "Esvaran Jathushan",
          "lendDate": "2024-12-11T18:26:41.4821258",
          "dueDate": "2024-12-12T18:26:41.4821258",
          "returnDate": "2024-12-11T19:00:23.3491402"
        },
        {
          "bookCopyId": 1,
          "userName": "Esvaran Jathushan",
          "issuingAdmin": "Esvaran Jathushan",
          "receivingAdmin": null,
          "lendDate": "2024-12-11T19:02:10.7712386",
          "dueDate": "2024-12-13T19:02:10.7712386",
          "returnDate": null
        },
        {
          "bookCopyId": 3,
          "userName": "Esvaran Jathushan",
          "issuingAdmin": "Esvaran Jathushan",
          "receivingAdmin": null,
          "lendDate": "2024-12-11T18:32:20.4132878",
          "dueDate": "2024-12-13T18:32:20.4132878",
          "returnDate": null
        }
      ]
    },
    {
      "bookId": 2,
      "bookTitle": "title2",
      "isbn": "54",
      "author": "author21",
      "bookRentDetails": [
        {
          "bookCopyId": 2,
          "userName": "Esvaran Jathushan",
          "issuingAdmin": "Esvaran Jathushan",
          "receivingAdmin": null,
          "lendDate": "2024-12-11T18:28:05.9408338",
          "dueDate": "2024-12-13T18:28:05.9408338",
          "returnDate": null
        }
      ]
    }
  ];
  BookBorrowReportbyId: any =  [
    {
      "bookId": 1,
      "bookTitle": "title1",
      "isbn": "12311",
      "author": "author1",
      "bookRentDetails": [
        {
          "bookCopyId": 1,
          "userName": "Esvaran Jathushan",
          "issuingAdmin": "Esvaran Jathushan",
          "receivingAdmin": "Esvaran Jathushan",
          "lendDate": "2024-12-11T18:26:41.4821258",
          "dueDate": "2024-12-12T18:26:41.4821258",
          "returnDate": "2024-12-11T19:00:23.3491402"
        },
        {
          "bookCopyId": 1,
          "userName": "Esvaran Jathushan",
          "issuingAdmin": "Esvaran Jathushan",
          "receivingAdmin": null,
          "lendDate": "2024-12-11T19:02:10.7712386",
          "dueDate": "2024-12-13T19:02:10.7712386",
          "returnDate": null
        },
        {
          "bookCopyId": 3,
          "userName": "Esvaran Jathushan",
          "issuingAdmin": "Esvaran Jathushan",
          "receivingAdmin": null,
          "lendDate": "2024-12-11T18:32:20.4132878",
          "dueDate": "2024-12-13T18:32:20.4132878",
          "returnDate": null
        }
      ]
    }
  ];
allLendingcount:any=[
  {
    "bookID": 1,
    "totalRentCount": 1,
    "induvalCopyrentcount": [
      {
        "coppyId": 1,
        "rentCount": 1
      },
      {
        "coppyId": 3,
        "rentCount": 0
      }
    ]
  },
  {
    "bookID": 2,
    "totalRentCount": 0,
    "induvalCopyrentcount": [
      {
        "coppyId": 2,
        "rentCount": 0
      }
    ]
  }
];
Lendingcountbybookid:any= [
  {
    "bookID": 1,
    "totalRentCount": 1,
    "induvalCopyrentcount": [
      {
        "coppyId": 1,
        "rentCount": 1
      },
      {
        "coppyId": 3,
        "rentCount": 0
      }
    ]
  }
];
  selectedReportType: string = 'overall';  // Default selected report
  userId: number = 1; 
  bookId: number = 2; 
  date: string = '2024-12-11'; 
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
            this.allBookBorrowReport = response;
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
              this.BookBorrowReportbyId = response;
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
            this.allLendingcount = response;
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
              this.Lendingcountbybookid = response;
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