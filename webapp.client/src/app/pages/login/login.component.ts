import {Component, signal} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from '@angular/router';
import {User} from "../../models/User";
import {UserService} from "../../services/user.service";
import {
  FormControl,
  FormGroupDirective,
  NgForm,
  Validators,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import {ErrorStateMatcher} from '@angular/material/core';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {Value} from "sass";
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})



export class LoginComponent{
  protected errorMessage: string = '';
  protected passwordFormControl: FormControl = new FormControl('');
  protected emailFormControl: FormControl = new FormControl('');

  hide = signal(true);
  clickEvent(event: MouseEvent) {
    this.hide.set(!this.hide());
    event.stopPropagation();
  }
  constructor(private http: HttpClient, private router: Router, private userService: UserService) {

  }
  onSubmit() {
    var loginData = {email: this.emailFormControl.value, password: this.passwordFormControl.value};

    this.http.post<string>("api/authorization/login", loginData).subscribe(
      (status) => {
        this.http.get<User>('/api/authorization/user').subscribe(
          (user) => {
            this.userService.setUser(user);
            this.router.navigate(['/']);
          },
          (error) => {
            console.error(error);

          }
        );
      },
      (error) => {
        this.errorMessage = error.error.message;
      }
    );
  }
}
