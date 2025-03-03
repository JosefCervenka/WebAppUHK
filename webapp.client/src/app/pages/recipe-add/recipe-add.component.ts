import {Component} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {FormControl, FormGroup} from "@angular/forms";
import {Router} from "@angular/router";
import {Recipe} from "../../models/Recipe";
import {IdObject} from "../../models/IdObject";

@Component({
  selector: 'app-recipe-add',
  templateUrl: './recipe-add.component.html',
  styleUrl: './recipe-add.component.scss'
})
export class RecipeAddComponent {
  protected selectedFile: File | null = null;

  protected titleForm: FormControl = new FormControl("");
  protected textForm: FormControl = new FormControl("");
  protected stepForms: FormControl[] = [];


  constructor(protected http: HttpClient, private router: Router) {
  }

  addStep()
  {
    this.stepForms.push(new FormControl(""));
  }

  removeStep(){
    this.stepForms.pop();
  }
  onPost() {
    let formData: FormData = new FormData();
    formData.append("picture", this.selectedFile as Blob)
    formData.append("title", this.titleForm.value)
    formData.append("text", this.textForm.value)

    for (let stepForm in this.stepForms) {
      formData.append("steps", this.stepForms[stepForm].value);
    }

    this.http.post<IdObject>("api/recipe", formData).subscribe(response => {
      console.log(response);

        this.router.navigate(['/recipe', response.id]);
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

  protected readonly FormControl = FormControl;
}
