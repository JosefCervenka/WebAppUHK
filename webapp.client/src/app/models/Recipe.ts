import {Photo} from "./Photo";
import {Step} from "./Step";
import {Ingredient} from "./Ingredient";
import {Comment} from "./Comment";
import {User} from "./User";

export interface Recipe {
  id: number;
  authorId: number;
  author: User;
  title: string;
  text: string;
  headerPhoto: Photo;
  steps: Step[];
  ingredients: Ingredient[];
  comments: Comment[];
  average: number;
}
