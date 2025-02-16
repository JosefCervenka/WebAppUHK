import {Component, EventEmitter, Output} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from '@angular/router';
import {concatWith} from "rxjs";
import {User} from "../models/User";
import {UserServiceService} from "../Services/user-service.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  loginData = {email: '', password: ''};
  errorMessage?: string = "";

  constructor(private http: HttpClient, private router: Router, private userService: UserServiceService) {

  }

  onSubmit() {
    this.http.post<string>("api/authorization/login", this.loginData).subscribe(
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
        console.log(error.error)
        this.errorMessage = error.error.message;
      }
    );
  }
}
