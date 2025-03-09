import {Component, ContentChild, Input, TemplateRef} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {FormControl} from "@angular/forms";
import {concatWith} from "rxjs";

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrl: './comment.component.scss'
})
export class CommentComponent {

  protected textForm: FormControl = new FormControl("");

  @Input() recipeId: number | null = null;

  constructor(protected http: HttpClient) {
  }

  onPost() {
    if (!this.recipeId)
      return;

    if(!this.textForm.value)
      return;

    let formData: FormData = new FormData();
    formData.append("text", this.textForm.value);

    this.http.post(`/api/recipe/${this.recipeId}/comment`, formData).subscribe(
      status => {
          console.log(status);
      },
      error => {
          console.log(error);
      }
    );
  }
}
