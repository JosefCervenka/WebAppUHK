import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {User} from "./models/User";
import {UserService} from "./services/user.service";

interface UserLogin{
  email :string;
  password: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  public loginDto? : UserLogin;

  constructor(private http: HttpClient, private userService: UserService) {

  }

  ngOnInit() {
    this.getUser();
  }

  getUser() {
    // try if user is alredy logged
    this.http.get<User>('/api/authorization/user').subscribe(
      (user) => {
        this.userService.setUser(user)
      },
    );
  }

  title = 'RedMole';
}
