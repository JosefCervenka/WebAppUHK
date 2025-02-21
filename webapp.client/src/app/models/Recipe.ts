import {Photo} from "./Photo";

export interface Recipe {
  userId: number;
  title: string;
  text: string;
  headerPhoto: Photo;
}
