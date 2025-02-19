import { Component } from '@angular/core';
import {Recipe} from "../../models/Recipe";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-recepie',
  templateUrl: './recipe.component.html',
  styleUrl: './recipe.component.css'
})
export class RecipeComponent {
  protected recipes?: Recipe[];

  constructor(private httpClient: HttpClient) {
  }
  ngOnInit() {
    this.httpClient.get<Recipe[]>("api/recepie").subscribe(
      (recipes) => {
        this.recipes  = recipes;
      },
    );
  }
}
