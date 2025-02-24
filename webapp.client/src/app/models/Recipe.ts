import {Photo} from "./Photo";

export interface Recipe {
  id: number;
  authorId: number;
  title: string;
  text: string;
  headerPhoto: Photo;
}
