export interface AddBookDto {
  ISBN: string;
  Title: string;
  Author: string;
  Genre: string[]; // Use an array to support multiple genres
  PublishYear: number;
  ShelfLocation: string;
  TotalCopies: number;
  CoverImages?: File[]; // Represent IFormFile as File for the client-side
}
