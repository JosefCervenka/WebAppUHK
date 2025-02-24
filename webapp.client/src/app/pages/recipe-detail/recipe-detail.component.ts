import {Component} from '@angular/core';
import {ActivatedRoute, ParamMap} from '@angular/router';

@Component({
  selector: 'app-recipe-detail',
  templateUrl: './recipe-detail.component.html',
  styleUrl: './recipe-detail.component.scss'
})
export class RecipeDetailComponent {
  protected id?: number;
  constructor(private route: ActivatedRoute) {

  }
  ngOnInit(): void {
    this.route.paramMap.subscribe((params: ParamMap) => {
      if (params.get('id')) {
        this.id = Number.parseInt(params.get("id") ?? "0");
      }
    });
  }
}
