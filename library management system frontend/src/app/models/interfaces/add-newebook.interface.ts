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
    // Define the fields of EbookMetadataDto based on its structure
    // For example:
    Field1: string;
    Field2: number;
    // Add other fields as required
  }
  