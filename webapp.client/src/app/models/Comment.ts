import {User} from "./User";
export interface Comment {
  id: number,
  text: string,
  author: User,
  authorId: number,
  rating: number,
}
