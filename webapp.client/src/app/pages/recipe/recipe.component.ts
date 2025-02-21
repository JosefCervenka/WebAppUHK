import { Component } from '@angular/core';
import {Recipe} from "../../models/Recipe";
import {HttpClient} from "@angular/common/http";
import {CdkVirtualScrollableWindow} from "@angular/cdk/scrolling";

@Component({
  selector: 'app-recepie',
  templateUrl: './recipe.component.html',
  styleUrl: './recipe.component.scss'
})
export class RecipeComponent {
  protected recipes?: Recipe[];

  constructor(private httpClient: HttpClient) {
  }
  ngOnInit() {
    this.httpClient.get<Recipe[]>("api/recipe").subscribe(
      (recipes) => {
        console.log(recipes)
        this.recipes  = recipes;
      },
    );
  }
}
