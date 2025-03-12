import {Component, WritableSignal} from '@angular/core';
import {Recipe} from "../../models/Recipe";
import {HttpClient} from "@angular/common/http";
import {LiveAnnouncer} from '@angular/cdk/a11y';
import {ChangeDetectionStrategy, inject, signal} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {MatButtonModule} from '@angular/material/button';
import {MatChipInputEvent, MatChipsModule} from '@angular/material/chips';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import {toSignal} from "@angular/core/rxjs-interop";

@Component({
  selector: 'app-recepie',
  templateUrl: './recipe.component.html',
  styleUrl: './recipe.component.scss'
})
export class RecipeComponent {
  protected searchByIngredient: boolean = false;

  protected recipes?: Recipe[];

  readonly templateKeywords: WritableSignal<string[]> = signal([]);

  announcer = inject(LiveAnnouncer);

  removeTemplateKeyword(keyword: string) {
    this.templateKeywords.update(keywords => {
      const index = keywords.indexOf(keyword);
      if (index < 0) {
        return keywords;
      }

      keywords.splice(index, 1);
      this.announcer.announce(`removed ${keyword} from template form`);
      return [...keywords];
    });

    this.searchByIngredients();
  }

  addTemplateKeyword(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();

    if (value) {
      this.templateKeywords.update(keywords => [...keywords, value]);
      this.announcer.announce(`added ${value} to template form`);
    }
    event.chipInput!.clear();

    this.searchByIngredients();
  }

  constructor(private httpClient: HttpClient) {
  }

  searchByIngredients() {
    var ingredients: string[] = this.templateKeywords();

    var query: string = ingredients.map(ingredient => `ingredient=${ingredient}`).join('&');

    this.httpClient.get<Recipe[]>(`/api/recipe?${query}`).subscribe(
      (recipes) => {
        this.recipes = recipes;
      });
  }

  search(search: string) {
    this.httpClient.get<Recipe[]>(`/api/recipe?search=${search}`).subscribe(
      (recipes) => {
        this.recipes = recipes;
      },
    );
  }

  ngOnInit() {
    this.httpClient.get<Recipe[]>("/api/recipe").subscribe(
      (recipes) => {
        this.recipes = recipes;
      },
    );
  }
}
