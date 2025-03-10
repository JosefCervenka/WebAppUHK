import {Component, EventEmitter, Input, Output} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {FormControl} from "@angular/forms";

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrl: './comment.component.scss'
})
export class CommentComponent {

  protected textForm: FormControl = new FormControl("");

  @Input() recipeId: number | null = null;
  protected rating: number | null = null;
  @Output() commentPosted = new EventEmitter<void>();

  constructor(protected http: HttpClient) {
  }


  protected onRatingChanged(newRating: number) {
    this.rating = newRating;
    console.log("Parent received rating:", newRating);
  }

  onPost() {
    console.log(this.rating);
    if(this.rating == null)
      return;

    if (!this.recipeId)
      return;

    if (!this.textForm.value)
      return;

    let formData: FormData = new FormData();
    formData.append("text", this.textForm.value);
    formData.append("rating", `${this.rating}`);

    this.http.post(`/api/recipe/${this.recipeId}/comment`, formData).subscribe(
      status => {
        this.textForm.reset();
        this.commentPosted.emit();
      },
      error => {
        console.log(error);
      }
    );
  }
}
