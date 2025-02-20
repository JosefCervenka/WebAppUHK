import {Component, ContentChild, TemplateRef} from '@angular/core';
import {UserService} from "../../services/user.service";
import {User} from "../../models/User";

@Component({
  selector: 'app-authorized-view',
  templateUrl: './authorized-view.component.html',
  styleUrl: './authorized-view.component.css'
})
export class AuthorizedViewComponent {

  constructor(protected userService: UserService) {

  }

  @ContentChild("authorizedView") authorizedView!: TemplateRef<any>;
  @ContentChild("unauthorizedView") unauthorizedView!: TemplateRef<any>;

}
