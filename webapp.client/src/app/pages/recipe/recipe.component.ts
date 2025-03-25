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
import {FavoriteService} from "../../services/favorite.service";
import {PageEvent, MatPaginatorModule} from '@angular/material/paginator';
import {JsonPipe} from '@angular/common';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import {MatInputModule} from '@angular/material/input';
import {Count} from '../../models/Count';

@Component({
  selector: 'app-recepie',
  templateUrl: './recipe.component.html',
  styleUrl: './recipe.component.scss'
})
export class RecipeComponent {
  protected searchByIngredient: boolean = false;

  protected recipes?: Recipe[];

  readonly templateKeywords: WritableSignal<string[]> = signal([]);

  protected favoriteOnly: boolean = false;

  protected query: string = '';

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

  constructor(private httpClient: HttpClient, private favoriteService: FavoriteService) {

  }

  searchByIngredients() {
    let ingredientsQuery: string = this.templateKeywords().map(ingredient => `ingredient=${ingredient}`).join('&');
    let favoriteQuery = this.favoriteService.getFavorites().map(favorite => `favorite=${favorite}`).join('&');

    if (this.favoriteOnly) {
      this.query = `${ingredientsQuery}&${favoriteQuery}`;
    } else {
      this.query = `${ingredientsQuery}`;
    }

    this.httpClient.get<Recipe[]>(`/api/recipe?${this.query}`).subscribe(
      (recipes) => {
        this.recipes = recipes;
      });
    this.httpClient.get<Count>(`/api/recipe/count?${this.query}`).subscribe(
      (count) => {
        this.length = count.count;
      },
    );

    this.pageIndex = 0
  }

  search(search: string) {
    let favoriteQuery = this.favoriteService.getFavorites().map(favorite => `favorite=${favorite}`).join('&');
    let searchQuery = `search=${search}`;

    if (this.favoriteOnly) {
      this.query = `${searchQuery}&${favoriteQuery}`;
    } else {
      this.query = `${searchQuery}`;
    }

    this.httpClient.get<Recipe[]>(`/api/recipe?${this.query}`).subscribe(
      (recipes) => {
        this.recipes = recipes;
      });

    this.httpClient.get<Count>(`/api/recipe/count?${this.query}`).subscribe(
      (count) => {
        this.length = count.count;
      },
    );

    this.pageIndex = 0;
  }

  ngOnInit() {
    this.httpClient.get<Recipe[]>("/api/recipe").subscribe(
      (recipes) => {
        this.recipes = recipes;
      },
    );

    this.httpClient.get<Count>("/api/recipe/count").subscribe(
      (count) => {
        this.length = count.count;
      },
    );


    this.pageIndex = 0
  }

  length = 0;
  pageSize = 5;
  pageIndex = 0;
  hidePageSize = true;
  showFirstLastButtons = true;
  disabled = false;

  protected pageEvent: PageEvent = new PageEvent();

  handlePageEvent(e: PageEvent) {
    this.pageEvent = e;
    this.pageIndex = e.pageIndex;

    let path = `/api/recipe?pageIndex=${this.pageIndex}`;
    let pathCount = `/api/recipe/count`;

    if (this.query) {
      path = `${path}&${this.query}`;
      pathCount += `${pathCount}?${this.query}`;
    }

    this.httpClient.get<Count>(pathCount).subscribe(
      (count) => {
        this.length = count.count;
      },
    );

    this.httpClient.get<Recipe[]>(path).subscribe(
      (recipes) => {
        this.recipes = recipes;
      }
    );
  }
}
