export interface AdminLoginRequest {
    emailOrNic: string;
    password: string;
  }
  export interface UserRequestModel {
    UserNic?: string;            
    FirstName: string;           
    LastName: string;            
    Email: string;               
    PhoneNumber: string;         
    Address: string;             
    Password: string;           
    ProfileImage?: File;         
    IsActive: boolean;           
    IsSubscribed: boolean;       
  }
  