import {Component} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {FormControl} from "@angular/forms";
import {ParamMap, Router, Route, ActivatedRoute} from "@angular/router";
import {IdObject} from "../../models/IdObject";
import {Unit} from "../../models/Unit";
import {Recipe} from "../../models/Recipe";

@Component({
  selector: 'app-recipe-edit',
  templateUrl: './recipe-edit.component.html',
  styleUrl: './recipe-edit.component.scss'
})
export class RecipeEditComponent {
  protected selectedFile: File | null = null;

  protected titleForm: FormControl = new FormControl("");
  protected textForm: FormControl = new FormControl("");

  protected stepForms: FormControl[] = [];

  protected ingredientsForms: FormControl[] = [];
  protected countForms: FormControl[] = [];
  protected unitForms: FormControl[] = [];
  protected units: Unit[] = []
  protected id: number = 0;

  protected recipe: Recipe | null = null;
  protected imageUrl: string | null = null;

  ngOnInit() {
    this.route.paramMap.subscribe((params: ParamMap) => {
      if (params.get('id')) {
        this.id = Number.parseInt(params.get("id") ?? "0");
      }
    });
    this.http.get<Unit[]>("api/unit").subscribe(
      (units) => {
        this.units = units;
      },
    );
    this.http.get<Recipe>(`api/recipe/${this.id}`).subscribe((recipe: Recipe) => {
        this.recipe = recipe;


        let ingredients = this.recipe?.ingredients ?? [];
        let steps = this.recipe?.steps ?? [];

        this.textForm = new FormControl(recipe.text);
        this.titleForm = new FormControl(recipe.title);

        this.http.get(this.recipe.headerPhoto.url, {responseType: 'blob'}).subscribe(imageFile => {
          console.log(imageFile);
          this.imageUrl = URL.createObjectURL(imageFile);
        });

        for (let ingredient of ingredients) {
          this.ingredientsForms.push(new FormControl(ingredient.name));
          this.unitForms.push(new FormControl(`${ingredient.unit.id}`));
          this.countForms.push(new FormControl(ingredient.count))
        }

        for (let step of steps) {
          this.stepForms.push(new FormControl(step.text));
        }

      },
      error => {
        console.log(error);
      });
  }

  constructor(protected http: HttpClient, protected router: Router, protected route: ActivatedRoute) {
  }

  addIngredients() {
    this.ingredientsForms.push(new FormControl(""));
    this.unitForms.push(new FormControl(""));
    this.countForms.push(new FormControl(""))
  }

  removeIngredients() {
    if (this.ingredientsForms.length == 1)
      return;

    this.ingredientsForms.pop();
    this.unitForms.pop();
    this.countForms.pop();
  }

  addStep() {
    this.stepForms.push(new FormControl(""));
  }

  removeStep() {
    if (this.stepForms.length == 1)
      return;

    this.stepForms.pop();
  }

  protected onPost() {
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


    this.http.put<IdObject>(`api/recipe/${this.id}`, formData).subscribe(response => {
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

