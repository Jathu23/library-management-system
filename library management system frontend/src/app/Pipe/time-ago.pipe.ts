import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'timeAgo'
})
export class TimeAgoPipe implements PipeTransform {

  transform(value: string | Date): string {
    if (!value) return '';

    const time = new Date(value instanceof Date ? value : value);
    const now = new Date();
    const seconds = Math.floor((now.getTime() - time.getTime()) / 1000); // Difference in seconds

    // Time units in seconds
    const minute = 60;
    const hour = minute * 60;
    const day = hour * 24;
    const week = day * 7;
    const month = day * 30;
    const year = day * 365;

    let timeAgo = '';

    if (seconds < minute) {
      timeAgo = `${seconds} seconds ago`;
    } else if (seconds < hour) {
      const minutes = Math.floor(seconds / minute);
      timeAgo = `${minutes} minute${minutes > 1 ? 's' : ''} ago`;
    } else if (seconds < day) {
      const hours = Math.floor(seconds / hour);
      timeAgo = `${hours} hour${hours > 1 ? 's' : ''} ago`;
    } else if (seconds < week) {
      const days = Math.floor(seconds / day);
      timeAgo = `${days} day${days > 1 ? 's' : ''} ago`;
    } else if (seconds < month) {
      const weeks = Math.floor(seconds / week);
      timeAgo = `${weeks} week${weeks > 1 ? 's' : ''} ago`;
    } else if (seconds < year) {
      const months = Math.floor(seconds / month);
      timeAgo = `${months} month${months > 1 ? 's' : ''} ago`;
    } else {
      const years = Math.floor(seconds / year);
      timeAgo = `${years} year${years > 1 ? 's' : ''} ago`;
    }

    return timeAgo;
  }
}
