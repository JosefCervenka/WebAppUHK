import {Component} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {FormControl, FormGroup} from "@angular/forms";
import {Router} from "@angular/router";

@Component({
  selector: 'app-recipe-add',
  templateUrl: './recipe-add.component.html',
  styleUrl: './recipe-add.component.scss'
})
export class RecipeAddComponent {
  protected selectedFile: File | null = null;

  protected titleForm: FormControl = new FormControl('');
  protected textForm: FormControl = new FormControl('');

  constructor(protected http: HttpClient, private router: Router) {
  }

  onPost() {
    let formData: FormData = new FormData();
    formData.append("picture", this.selectedFile as Blob)
    formData.append("title", this.titleForm.value)
    formData.append("text", this.textForm.value)

    this.http.post("api/recipe", formData).subscribe(response => {

      },
      error => {

      })
  }

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files?.length) {
      this.selectedFile = input.files[0];
    }
  }
}
