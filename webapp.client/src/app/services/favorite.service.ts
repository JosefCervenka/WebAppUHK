import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FavoriteService {

  public isFavorite(id: number): boolean {
    return this.getFavorites().includes(id);
  }

  public removeFavorites(id: number): void {
    if (isNaN(id)) return;

    let favorites = this.getFavorites().filter((x: number) => {
      return x !== id;
    });

    localStorage.setItem("favorites", favorites.join(','));
  }

  public addFavorites(id: number) {
    if (isNaN(id)) return;

    let favorites = this.getFavorites();

    if (favorites.includes(id)) return;

    favorites.push(id);
    localStorage.setItem("favorites", favorites.join(','));
  }

  public getFavorites(): number[] {
    let favoritesIdsString = localStorage.getItem('favorites') || "";

    return favoritesIdsString
      ? favoritesIdsString.split(',')
        .map(id => parseInt(id, 10))
        .filter(id => !isNaN(id))
      : [];
  }

  constructor() {
  }
}
