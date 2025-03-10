import {Component, Output, EventEmitter} from '@angular/core';

@Component({
  selector: 'app-rating',
  templateUrl: './rating.component.html',
  styleUrl: './rating.component.scss'
})


export class RatingComponent {
  protected temporaryRating: number | null = null;
  protected rating: number | null= null;

  @Output() ratingChange = new EventEmitter<number>();

  protected setTemporaryRating(rating: number) {
    this.temporaryRating = rating;
  }

  protected setRating() {
    this.rating = this.temporaryRating!;
    this.ratingChange.emit(this.rating);
  }

  protected resetTemporaryRating() {
    this.temporaryRating = null;
  }
}
