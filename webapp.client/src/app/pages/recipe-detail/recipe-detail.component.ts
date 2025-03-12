import {Component} from '@angular/core';
import {ActivatedRoute, ParamMap} from '@angular/router';
import {HttpClient} from "@angular/common/http";
import {Recipe} from "../../models/Recipe";
import {User} from "../../models/User";
import {Router} from "@angular/router"

@Component({
  selector: 'app-recipe-detail',
  templateUrl: './recipe-detail.component.html',
  styleUrl: './recipe-detail.component.scss'
})
export class RecipeDetailComponent {
  protected id?: number;
  protected recipe?: Recipe;
  protected imageUrl?: string;

  constructor(private route: ActivatedRoute, protected http: HttpClient, private router: Router) {
  }

  delete(): void {
    this.http.delete(`api/recipe/${this.recipe?.id}`,).subscribe(x => {
        this.router.navigate(['/recipe'])
      },
      (error) => {
        console.log(error);
      })
  }

  protected ngOnInit(): void {
    this.route.paramMap.subscribe((params: ParamMap) => {
      if (params.get('id')) {
        this.id = Number.parseInt(params.get("id") ?? "0");
      }
    });
    this.http.get<Recipe>(`api/recipe/${this.id}`).subscribe((recipe: Recipe) => {
        this.recipe = recipe;
        console.log(recipe)
        this.http.get(this.recipe.headerPhoto.url, {responseType: 'blob'}).subscribe(imageFile => {
          console.log(imageFile);
          this.imageUrl = URL.createObjectURL(imageFile);
        });
      },
      error => {
        console.log(error);
      });


  }
}

