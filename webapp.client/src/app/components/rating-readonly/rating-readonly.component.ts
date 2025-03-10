import { Component, Input } from '@angular/core';


@Component({
  selector: 'app-rating-readonly',
  templateUrl: './rating-readonly.component.html',
  styleUrl: './rating-readonly.component.scss'
})
export class RatingReadonlyComponent {

  @Input() rating: number = 0;
  protected readonly filledArray = Array;

  protected empty = 5;
  protected readonly emptyArray = Array;

  public ngOnInit(): void {
    this.empty = this.empty - this.rating;
  }

}
