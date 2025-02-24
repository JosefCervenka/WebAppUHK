import {Component, Input, ViewChild} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Recipe} from "../../models/Recipe";
import {User} from "../../models/User";
import { Router } from '@angular/router';

@Component({
  selector: 'app-recepie-item',
  templateUrl: './recipe-item.component.html',
  styleUrl: './recipe-item.component.scss'
})
export class RecipeItemComponent {

  @Input() recipe?: Recipe;

  user?: User;

  imageUrl?: string;

  constructor(private http: HttpClient) {
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
    if (this.recipe?.authorId) {
      this.http.get<User>(`/api/user/${this.recipe.authorId}`)
        .subscribe((user: User) => {
            this.user = user;
          },
          (error) => {
            console.log(error);
          });
    }
  }
}
