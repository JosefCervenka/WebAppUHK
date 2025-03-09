import {User} from "./User";
export interface Comment {
  text: string,
  author: User,
  authorId: number,
}
