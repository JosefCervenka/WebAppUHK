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

  protected passwordFormControl: FormControl = new FormControl('');
  protected emailFormControl: FormControl = new FormControl('');

  hide = signal(true);
  clickEvent(event: MouseEvent) {
    this.hide.set(!this.hide());
    event.stopPropagation();
  }

  protected registerData: UserRegister = {
    password: "",
    passwordAgain: "",
    email: "",
    name: "",
  }
  errorMessage?: string;

  onSubmit() {
    console.log("hello")
    this.http.post<string>("api/authorization/register", this.registerData).subscribe(
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
