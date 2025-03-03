import {Component, ContentChild, Input, TemplateRef} from '@angular/core';
import {UserService} from "../../services/user.service";
import {User} from "../../models/User";

@Component({
  selector: 'app-authorized-view',
  templateUrl: './authorized-view.component.html',
})
export class AuthorizedViewComponent {

  @Input() role: string | null;
  constructor(protected userService: UserService) {
    this.role = null;
  }

  @ContentChild("authorizedView") authorizedView!: TemplateRef<any>;
  @ContentChild("unauthorizedView") unauthorizedView!: TemplateRef<any>;

}
