import {Component} from '@angular/core';
import {UserRegister} from "../models/UserRegister";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})
export class RegistrationComponent {
  constructor(private http: HttpClient) {

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
