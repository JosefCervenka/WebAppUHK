import { Injectable } from '@angular/core';
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

  logout(){
    this.userSubject.next(null);
  }

  getUser() {
    return this.userSubject.value;
  }
  constructor() { }
}
