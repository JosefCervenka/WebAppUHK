import {Component, Input, ViewChild} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Recipe} from "../../models/Recipe";
import {User} from "../../models/User";
import {Router} from '@angular/router';
import {FavoriteService} from "../../services/favorite.service";

@Component({
  selector: 'app-recepie-item',
  templateUrl: './recipe-item.component.html',
  styleUrl: './recipe-item.component.scss'
})
export class RecipeItemComponent {

  @Input() recipe?: Recipe;

  user?: User;

  imageUrl?: string;

  constructor(private http: HttpClient, private favoriteService: FavoriteService) {
  }

  like(id: number): void {
    console.log('like', id);
    this.favoriteService.addFavorites(id);
  }

  delete(): void {
    this.http.delete(`api/recipe/${this.recipe?.id}`,).subscribe(x => {
        console.log(x);
      },
      (error) => {
        console.log(error);
      })
  }

  ngOnInit(): void {
    if (this.recipe?.headerPhoto?.url) {
      this.http.get(this.recipe.headerPhoto.url, {responseType: 'blob'}).subscribe(imageFile => {
          console.log(imageFile);
          this.imageUrl = URL.createObjectURL(imageFile);
        },
        (error) => {
          console.log(error);
        })
    }
  }
}
