import {Component, Input} from '@angular/core';
import {UserService} from "../../services/user.service";
import {User} from "../../models/User";
import {AuthorizedViewComponent} from "../authorized-view/authorized-view.component";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css'
})
export class NavBarComponent {
  private httpClient: HttpClient

  constructor(protected userService: UserService, httpClient: HttpClient) {

    this.httpClient = httpClient;
  }

  logout() {
    this.httpClient.post("/api/authorization/logout", null).subscribe(message =>
      {
        console.log(message)
      });
    this.userService.logout();

  }
}
