import {Photo} from "./Photo";
import {Step} from "./Step";
import {Ingredient} from "./Ingredient";

export interface Recipe {
  id: number;
  authorId: number;
  title: string;
  text: string;
  headerPhoto: Photo;
  steps: Step[];
  ingredients: Ingredient[]
}
