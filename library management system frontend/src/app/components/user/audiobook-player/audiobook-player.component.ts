import { Component } from '@angular/core';

@Component({
  selector: 'app-audiobook-player',
  templateUrl: './audiobook-player.component.html',
  styleUrl: './audiobook-player.component.css'
})
export class AudiobookPlayerComponent {
  audio = new Audio('https://localhost:7261/Audiobooks/bc916c4e-02cc-465d-8a0c-fa189eec3a50.mp3');

  isPlaying = false;
  duration = 0;
  currentTime = 0;
  currentProgress = 0;
  volume = 50;
  isFavorite = false;
  vuLevel = 0;

  constructor() {
    this.audio.addEventListener('loadedmetadata', () => {
      this.duration = this.audio.duration;
    });

    this.audio.addEventListener('timeupdate', () => {
      this.currentTime = this.audio.currentTime;
      this.currentProgress = (this.audio.currentTime / this.duration) * 100;
      this.updateVuMeter();
    });
  }

  togglePlay() {
    this.isPlaying ? this.audio.pause() : this.audio.play();
    this.isPlaying = !this.isPlaying;
  }

  seek(seconds: number) {
    this.audio.currentTime = Math.max(
      0,
      Math.min(this.audio.currentTime + seconds, this.duration)
    );
  }

  onSeek(event: Event) {
    const input = event.target as HTMLInputElement;
    const seekTime = (parseFloat(input.value) / 100) * this.duration;
    this.audio.currentTime = seekTime;
  }

  setVolume(event: Event) {
    const input = event.target as HTMLInputElement;
    this.volume = parseFloat(input.value);
    this.audio.volume = this.volume / 100;
  }

  toggleFavorite() {
    this.isFavorite = !this.isFavorite;
  }

  like() {
    alert('Liked!');
  }

  dislike() {
    alert('Disliked!');
  }

  updateVuMeter() {
    this.vuLevel = Math.random() * 100; // Simulate VU meter
  }
}
