import { Component } from '@angular/core';
import {Recipe} from "../../models/Recipe";
import {HttpClient} from '@angular/common/http';
import {User} from "../../models/User";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})


export class HomeComponent {
  constructor(private http: HttpClient) {

  }
}
