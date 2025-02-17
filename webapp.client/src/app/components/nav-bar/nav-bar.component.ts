import {Component, Input} from '@angular/core';
import {UserService} from "../../services/user.service";
import {User} from "../../models/User";
import {AuthorizedViewComponent} from "../authorized-view/authorized-view.component";

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css'
})
export class NavBarComponent {
  user?: User;

  constructor(private userService: UserService) {
    this.userService.user$.subscribe(user => {
      if (user) {
        this.user = user;
      }
    });
  }
}
