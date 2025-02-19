import {Component, Input} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Recipe} from "../../models/Recipe";

@Component({
  selector: 'app-recepie-item',
  templateUrl: './recipe-item.component.html',
  styleUrl: './recipe-item.component.css'
})
export class RecipeItemComponent {

  @Input() recipe?: Recipe;

  constructor(private http: HttpClient) {
  }
}
