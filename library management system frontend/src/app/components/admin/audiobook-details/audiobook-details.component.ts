import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AudiobookService } from '../../../services/bookservice/audiobook.service';

@Component({
  selector: 'app-audiobook-details',
  templateUrl: './audiobook-details.component.html',
  styleUrl: './audiobook-details.component.css'
})
export class AudiobookDetailsComponent implements OnInit {
  audiobooks: any[] = [];
  selectedAudiobook: any = null;
  audio: HTMLAudioElement = new Audio();
  currentTime: number = 0;
  duration: number = 0;
  timer: any;

  constructor(private audiobookService: AudiobookService) {}

  ngOnInit(): void {
    this.loadAudiobooks();
  }

  loadAudiobooks(): void {
    this.audiobookService.getAudiobooks().subscribe(response => {
      this.audiobooks = response.data.items;
    });
  }

  openPopup(audiobook: any): void {
    this.selectedAudiobook = audiobook;
    this.audio.src = audiobook.filePath;
    this.audio.load();

    this.audio.onloadedmetadata = () => {
      this.duration = this.audio.duration;
    };

    this.startTimer();
  }

  closePopup(): void {
    this.selectedAudiobook = null;
    this.audio.pause();
    this.stopTimer();
  }

  playAudio(): void {
    this.audio.play();
  }

  pauseAudio(): void {
    this.audio.pause();
  }

  skipForward(): void {
    this.audio.currentTime += 5;
  }

  skipBackward(): void {
    this.audio.currentTime = Math.max(this.audio.currentTime - 5, 0);
  }

  startTimer(): void {
    this.stopTimer();
    this.timer = setInterval(() => {
      this.currentTime = this.audio.currentTime;
    }, 1000);
  }

  stopTimer(): void {
    if (this.timer) {
      clearInterval(this.timer);
    }
  }

  formatTime(seconds: number): string {
    const minutes = Math.floor(seconds / 60);
    const secs = Math.floor(seconds % 60);
    return `${minutes}:${secs < 10 ? '0' : ''}${secs}`;
  }
}
