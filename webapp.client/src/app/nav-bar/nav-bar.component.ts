import {Component, Input} from '@angular/core';
import {UserServiceService} from "../Services/user-service.service";
import {User} from "../models/User";

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css'
})
export class NavBarComponent {
  user?: User;

  constructor(private userService: UserServiceService) {
    this.userService.user$.subscribe(user => {
      if (user) {
        this.user = user;
      }
    });
  }
}
