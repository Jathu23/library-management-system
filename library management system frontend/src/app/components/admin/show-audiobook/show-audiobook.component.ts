import { Component } from '@angular/core';

@Component({
  selector: 'app-show-audiobook',
  templateUrl: './show-audiobook.component.html',
  styleUrl: './show-audiobook.component.css'
})
export class ShowAudiobookComponent {
  dataSource: any[] = [
    {
      id: 1,
      title: 'Audiobook Title 1',
      author: 'Author 1',
      genre: 'Fiction',
      filePath: 'Audiobooks/a (1).mp3',
      coverImagePath: 'AudiobookCovers/a (1).jpg',
      publishYear: 2020,
      language: 'English',
      narrator: 'Narrator 1',
      publisher: 'Publisher 1',
    },
    {
      id: 2,
      title: 'Audiobook Title 2',
      author: 'Author 2',
      genre: 'Science',
      filePath: 'Audiobooks/a (2).mp3',
      coverImagePath: 'AudiobookCovers/a (2).jpg',
      publishYear: 2019,
      language: 'English',
      narrator: 'Narrator 2',
      publisher: 'Publisher 2',
    },
    {
      id: 3,
      title: 'Audiobook Title 3',
      author: 'Author 3',
      genre: 'Fantasy',
      filePath: 'Audiobooks/a (3).mp3',
      coverImagePath: 'AudiobookCovers/a (3).jpg',
      publishYear: 2021,
      language: 'Spanish',
      narrator: 'Narrator 3',
      publisher: 'Publisher 3',
    },
  ];

  expandedElementId: number | null = null;
element: any;


  toggleRow(id:any): void {
    this.element =id;
    this.expandedElementId = this.expandedElementId === id ? null : id;
  }
}
