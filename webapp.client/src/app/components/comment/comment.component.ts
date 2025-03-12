import {Component, EventEmitter, Input, Output} from '@angular/core';
import {Comment} from "../../models/Comment";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrl: './comment.component.scss'
})
export class CommentComponent {
  @Input() comment: Comment | null = null;

  @Output() commentDelete = new EventEmitter<void>();

  constructor(private http: HttpClient) {
  }
  delete(){
    this.http.delete(`/api/comment/${this.comment?.id}`).subscribe(
      status => {
        console.log(status)
        this.commentDelete.emit();
      },
      error => {
        console.log(error)
      }
    );
  }
}

