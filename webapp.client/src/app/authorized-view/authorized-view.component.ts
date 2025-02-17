import { Component, ContentChild, TemplateRef } from '@angular/core';
import { UserServiceService } from "../Services/user-service.service";
import { AsyncPipe } from '@angular/common';
import {User} from "../models/User";
@Component({
  selector: 'app-authorized-view',
  templateUrl: './authorized-view.component.html',
  styleUrl: './authorized-view.component.css'
})
export class AuthorizedViewComponent {
  user?: User = undefined;

  constructor(private userService: UserServiceService) {
    this.userService.user$.subscribe(user => {
      if (user) {
        this.user = user;
      }
    });
  }

  @ContentChild("authorizedView") authorizedView!: TemplateRef<any>;
  @ContentChild("unauthorizedView") unauthorizedView!: TemplateRef<any>;

}
