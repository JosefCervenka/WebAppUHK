import {Component} from '@angular/core';
import {ActivatedRoute, ParamMap} from '@angular/router';
import {HttpClient} from "@angular/common/http";
import {Recipe} from "../../models/Recipe";
import {User} from "../../models/User";

@Component({
  selector: 'app-recipe-detail',
  templateUrl: './recipe-detail.component.html',
  styleUrl: './recipe-detail.component.scss'
})
export class RecipeDetailComponent {
  protected id?: number;
  protected recipe?: Recipe;
  protected author?: User;
  protected imageUrl?: string;

  constructor(private route: ActivatedRoute, protected http: HttpClient) {
  }

  ngOnInit(): void {
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

        this.http.get<User>(`api/user/${this.recipe.authorId}`).subscribe((user: User) => {
          this.author = user;
        });
      },
      error => {
        console.log(error);
      });


  }
}

