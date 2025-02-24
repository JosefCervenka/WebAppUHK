import {Component, signal} from '@angular/core';
import {UserRegister} from "../../models/UserRegister";
import {HttpClient} from "@angular/common/http";
import {FormControl} from "@angular/forms";
import {User} from "../../models/User";
import {UserService} from "../../services/user.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})
export class RegistrationComponent {
  constructor(private http: HttpClient, private userService: UserService, private router: Router) {

  }

  protected nameFormControl: FormControl = new FormControl('');
  protected emailFormControl: FormControl = new FormControl('');
  protected passwordFormControl: FormControl = new FormControl('');
  protected passwordFormConfirmControl: FormControl = new FormControl('');


  hide = signal(true);

  clickEvent(event: MouseEvent) {
    this.hide.set(!this.hide());
    event.stopPropagation();
  }

  errorMessage?: string;

  onSubmit() {
    var registerData: UserRegister = {
      password: this.passwordFormControl.value,
      passwordAgain: this.passwordFormConfirmControl.value,
      email: this.emailFormControl.value,
      name: this.nameFormControl.value,
    }

    var login = {
      email: this.emailFormControl.value,
      password: this.passwordFormControl.value,
    }

    this.http.post<string>("api/authorization/register", registerData).subscribe(
      (x) => {
        this.http.post("api/authorization/login", login)
          .subscribe((x) => {
            this.http.get<User>('/api/authorization/user').subscribe(
              (user) => {
                this.userService.setUser(user);
                this.router.navigate(['/']);
              })
          }, (x) => {
            console.log(x)
          })
      },
      (error) => {
        console.log(error)
        this.errorMessage = error.error.message;
      }
    );
  }
}
