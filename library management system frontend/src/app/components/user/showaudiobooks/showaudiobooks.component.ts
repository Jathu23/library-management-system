import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { GetbooksService } from '../../../services/bookservice/getbooks.service';

@Component({
  selector: 'app-showaudiobooks',
  templateUrl: './showaudiobooks.component.html',
  styleUrls: ['./showaudiobooks.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush, // Improve rendering performance
})
export class ShowaudiobooksComponent implements OnInit {
  audiobooksCache: Map<number, any[]> = new Map(); // Cache for paginated data
  filteredAudiobooks: any[] = [];
  playingAudio: any = null;
  audio = new Audio();
  isPlaying = false;
  progress: number = 0;
  currentTime: string = '0:00';
  duration: string = '0:00';
  searchQuery: string = '';
  currentPage = 1;
  pageSize = 10;
  totalItems = 0;
  isLoading = false;
  isModalOpen = false;
  selectedAudiobook: any = null;
  cachedAudioFiles: Map<string, string> = new Map(); // Cache for audio file paths
  private debounceTimer: any;

  constructor(private getbookservice: GetbooksService) {}

  ngOnInit() {
    // Trigger audiobook loading on page load
    this.loadAudiobooks();

    this.audio.addEventListener('timeupdate', () => {
      this.updateProgressDisplay();
    });

    this.audio.addEventListener('ended', () => {
      this.isPlaying = false;
    });
  }

  loadAudiobooks() {
    // Prevent duplicate requests if loading is already in progress or data is already cached
    if (this.isLoading || this.audiobooksCache.has(this.currentPage)) {
      this.filteredAudiobooks = this.audiobooksCache.get(this.currentPage) || [];
      return; // Use cached data
    }

    this.isLoading = true;
    this.getbookservice.getaudiobooks(this.currentPage, this.pageSize).subscribe(
      (response) => {
        const result = response.data;
        const audiobooks = result.items;

        // Cache data based on the current page
        this.audiobooksCache.set(this.currentPage, audiobooks);

        this.totalItems = result.totalCount;
        this.filteredAudiobooks = audiobooks;
        this.isLoading = false;
      },
      (error) => {
        console.error('Error fetching audiobooks:', error);
        this.isLoading = false;
      }
    );
  }

  filterAudiobooks() {
    // Debounce search input
    clearTimeout(this.debounceTimer);
    this.debounceTimer = setTimeout(() => {
      const query = this.searchQuery.toLowerCase();
      const allAudiobooks = Array.from(this.audiobooksCache.values()).flat();
      this.filteredAudiobooks = allAudiobooks.filter(
        (audiobook) =>
          audiobook.title.toLowerCase().includes(query) ||
          audiobook.author.toLowerCase().includes(query) ||
          audiobook.genre.toLowerCase().includes(query)
      );
    }, 300); // Debounce for 300ms
  }

  openModal(audiobook: any) {
    this.selectedAudiobook = audiobook;
    this.isModalOpen = true;
  }

  closeModal() {
    this.isModalOpen = false;
  }

  playAudio(audiobook: any) {
    if (this.playingAudio?.id !== audiobook.id || !this.isPlaying) {
      this.stopAudio();
      this.playingAudio = audiobook;

      if (this.cachedAudioFiles.has(audiobook.id)) {
        this.audio.src = this.cachedAudioFiles.get(audiobook.id)!;
      } else {
        const audioPath = `https://localhost:7261/${audiobook.filePath}`;
        this.cachedAudioFiles.set(audiobook.id, audioPath);
        this.audio.src = audioPath;
      }

      this.audio.play();
      this.isPlaying = true;
    } else {
      this.audio.pause();
      this.isPlaying = false;
    }
  }

  togglePlay() {
    if (this.isPlaying) {
      this.audio.pause();
    } else {
      this.audio.play();
    }
    this.isPlaying = !this.isPlaying;
  }

  stopAudio() {
    this.audio.pause();
    this.audio.currentTime = 0;
    this.isPlaying = false;
  }

  updateProgress(event: any) {
    this.audio.currentTime = (event.target.value / 100) * this.audio.duration;
  }

  skipTime(seconds: number) {
    const newTime = this.audio.currentTime + seconds;
    if (newTime >= 0 && newTime <= this.audio.duration) {
      this.audio.currentTime = newTime;
    }
  }

  updateProgressDisplay() {
    if (this.audio.duration) {
      this.progress = (this.audio.currentTime / this.audio.duration) * 100;
      this.currentTime = this.formatTime(this.audio.currentTime);
      this.duration = this.formatTime(this.audio.duration);
    }
  }

  formatTime(seconds: number): string {
    const minutes = Math.floor(seconds / 60);
    const secs = Math.floor(seconds % 60);
    return `${minutes}:${secs < 10 ? '0' : ''}${secs}`;
  }

  onPageChange(event: any) {
    // Update pagination parameters
    const { pageIndex, pageSize } = event;
    this.currentPage = pageIndex + 1;
    this.pageSize = pageSize;

    this.loadAudiobooks();
   
  }
}
