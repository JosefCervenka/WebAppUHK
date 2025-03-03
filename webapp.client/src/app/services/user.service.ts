import {Injectable} from '@angular/core';
import {User} from "../models/User";
import {BehaviorSubject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private userSubject = new BehaviorSubject<User | null>(null);

  user$ = this.userSubject.asObservable();

  setUser(user: User) {
    this.userSubject.next(user);
  }

  logout() {
    this.userSubject.next(null);
  }

  getUser() {
    return this.userSubject.value;
  }

  getRoles() {
    return this.userSubject.value?.userRoles;
  }

  containRole(role: string) {
    console.log(role);
    console.log(this.userSubject.value);
    console.log(this.userSubject.value?.userRoles);
    return this.userSubject.value?.userRoles
      .some(x => x === role);
  }

  constructor() {
  }
}
