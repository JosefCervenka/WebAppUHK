import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';


interface User {
  name: string;
}

interface UserLogin{
  email :string;
  password: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  public user?: User;
  public loginDto? : UserLogin;

  constructor(private http: HttpClient) {

  }

  ngOnInit() {
    this.loginDto = {
      email: "AdminUser@gmail.com",
      password: "Heslo123"
    };

    this.http.post<UserLogin>("api/authorization/login", this.loginDto).subscribe(
      (response) => {
        console.log("Login successful:", response);
      },
      (error) => {
        console.error("Login failed:", error);
      }
    );

    this.getUser();
  }

  getUser() {
    this.http.get<User>('/api/authorization/user').subscribe(
      (result) => {
        this.user = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  title = 'webapp.client';
}
