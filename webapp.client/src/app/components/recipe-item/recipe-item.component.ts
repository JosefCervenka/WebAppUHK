import {Component, Input, ViewChild} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Recipe} from "../../models/Recipe";

@Component({
  selector: 'app-recepie-item',
  templateUrl: './recipe-item.component.html',
  styleUrl: './recipe-item.component.scss'
})
export class RecipeItemComponent {

  @Input() recipe?: Recipe;

  imageUrl?: string;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    if (this.recipe?.headerPhoto?.url) { // More robust check
      this.http.get(this.recipe.headerPhoto.url, { responseType: 'blob' }).subscribe(imageFile => {
        console.log(imageFile);
        this.imageUrl = URL.createObjectURL(imageFile);
      });
    }
  }
}
