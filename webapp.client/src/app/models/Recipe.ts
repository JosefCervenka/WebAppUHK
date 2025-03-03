import {Photo} from "./Photo";
import {Step} from "./Step";

export interface Recipe {
  id: number;
  authorId: number;
  title: string;
  text: string;
  headerPhoto: Photo;
  steps: Step[];
}
