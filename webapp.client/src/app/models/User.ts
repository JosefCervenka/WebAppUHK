import {UserRole} from "./UserRole";

export interface User {
  name: string;
  email: string;
  userRoles: UserRole[];
}
