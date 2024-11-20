
export interface AddAudiobookDto {
    ISBN: string;
    Title: string;
    Author: string;
    Genre: string;
    PublishYear: number;
    AudioFile: File;
    CoverImage: File;
    FileFormat: string;
    Language: string;
    Narrator: string;
    Publisher: string;
    Description: string;
    DigitalRights: string;
  }