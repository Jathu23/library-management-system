import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-audiobook-details',
  templateUrl: './audiobook-details.component.html',
  styleUrl: './audiobook-details.component.css'
})
export class AudiobookDetailsComponent implements OnInit {
  audiobook: any;
  audio: HTMLAudioElement;

  constructor(private route: ActivatedRoute) {
    this.audio = new Audio();
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.fetchAudiobookDetails(parseInt(id));
    }
  }

  fetchAudiobookDetails(id: number) {
    // Sample data (replace with actual API call)
    const audiobooks = [
      {
        id: 7,
        title: "Audiobook Title 7",
        author: "Author 7",
        genre: "Romance",
        filePath: "https://localhost:7261/Audiobooks/bc916c4e-02cc-465d-8a0c-fa189eec3a50.mp3",
        coverImagePath: "https://images.unsplash.com/photo-1551300329-b91a61fa5ebe?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTZ8fGJvb2slMjBjb3ZlcnxlbnwwfHwwfHx8MA%3D%3D",
        publishYear: 2018,
        language: "German",
        narrator: "Narrator 7",
        publisher: "Publisher 7"
      },
      {
        id: 8,
        title: "Audiobook Title 8",
        author: "Author 8",
        genre: "History",
        filePath: "Audiobooks/a (8).mp3",
        coverImagePath: "AudiobookCovers/a (8).jpg",
        publishYear: 2020,
        language: "English",
        narrator: "Narrator 8",
        publisher: "Publisher 8"
      }
    ];

    this.audiobook = audiobooks.find(book => book.id === id);
    this.audio.src = this.audiobook.filePath; // Set audio source
  }

  playAudio() {
    this.audio.play();
  }

  pauseAudio() {
    this.audio.pause();
  }

  stopAudio() {
    this.audio.pause();
    this.audio.currentTime = 0;
  }
}
