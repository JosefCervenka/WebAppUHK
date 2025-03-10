import {Component} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {FormControl} from "@angular/forms";
import {Router} from "@angular/router";
import {IdObject} from "../../models/IdObject";
import {Unit} from "../../models/Unit";

@Component({
  selector: 'app-recipe-add',
  templateUrl: './recipe-add.component.html',
  styleUrl: './recipe-add.component.scss'
})
export class RecipeAddComponent {
  protected selectedFile: File | null = null;

  protected titleForm: FormControl = new FormControl("");
  protected textForm: FormControl = new FormControl("");

  protected stepForms: FormControl[] = [new FormControl("")];

  protected ingredientsForms: FormControl[] = [new FormControl("")];
  protected countForms: FormControl[] = [new FormControl("")];
  protected unitForms: FormControl[] = [new FormControl("")];

  protected units: Unit[] = []


  ngOnInit() {
    this.http.get<Unit[]>("api/unit").subscribe(
      (units) => {
        console.log(units)
        this.units = units;
      },
    );
  }

  constructor(protected http: HttpClient, private router: Router) {
  }

  addIngredients(){
    this.ingredientsForms.push(new FormControl(""));
    this.unitForms.push(new FormControl(""));
    this.countForms.push(new FormControl(""))
  }

  removeIngredients(){
    if(this.ingredientsForms.length == 1)
      return;

    this.ingredientsForms.pop();
    this.unitForms.pop();
    this.countForms.pop();
  }

  addStep()
  {
    this.stepForms.push(new FormControl(""));
  }

  removeStep(){
    if(this.stepForms.length == 1)
      return;

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
    for (let unitForm in this.unitForms) {
      formData.append("unitIds", this.unitForms[unitForm].value);
    }
    for (let countForm in this.countForms) {
      formData.append("counts", this.countForms[countForm].value);
    }
    for (let ingredientsForm in this.ingredientsForms) {
      formData.append("ingredients", this.ingredientsForms[ingredientsForm].value);
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

