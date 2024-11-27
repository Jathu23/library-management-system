export interface AddEbookDto {
    ISBN: string;
    Title: string;
    Author: string;
    Genre: string;
    PublishYear: number;
    EbookFile: File; // Corresponds to IFormFile
    CoverImages?: File | null; // Corresponds to optional IFormFile
    Metadata: EbookMetadataDto;
  }
  
  export interface EbookMetadataDto {
    Language:string;
    Publisher:string;
    Description:string;
    DigitalRights:string;
  }
  