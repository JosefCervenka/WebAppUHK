import {Component, signal} from '@angular/core';
import {UserRegister} from "../../models/UserRegister";
import {HttpClient} from "@angular/common/http";
import {FormControl} from "@angular/forms";

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})
export class RegistrationComponent {
  constructor(private http: HttpClient) {

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

    this.http.post<string>("api/authorization/register", registerData).subscribe(
      (x) => {
        console.log(x);
      },
      (error) => {
        console.log(error)
        this.errorMessage = error.error.message;
      }
    );
  }
}
