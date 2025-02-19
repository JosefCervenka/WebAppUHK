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
  title: string = "";
  text: string = "";
  selectedFile: File | null = null;
  test:number[] = [];

  constructor(private http: HttpClient) {}

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
    }
  }

  onSubmit(event: Event) {
    event.preventDefault();

    if (!this.selectedFile || !this.title || !this.text || !this.test) {
      return;
    }

    const formData = new FormData();
    for (const item in this.test) {
      formData.append("test", item);
    }

    formData.append('title', this.title);
    formData.append('description', this.text);
    formData.append('picture', this.selectedFile);

    this.http.post('/api/recipe', formData)
      .subscribe(response => {

      }, error => {
        console.error('Upload failed', error);
        alert('Upload failed!');
      });
  }
}
