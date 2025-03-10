import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {LoginComponent} from "./pages/login/login.component";
import {RegistrationComponent} from "./pages/registration/registration.component";
import {RecipeComponent} from "./pages/recipe/recipe.component";
import {RecipeAddComponent} from "./pages/recipe-add/recipe-add.component";
import {RecipeDetailComponent} from "./pages/recipe-detail/recipe-detail.component";

const routes: Routes = [
  {path: '', redirectTo:'recipe', pathMatch:'full'},
  {path: "login", component: LoginComponent},
  {path: "register", component: RegistrationComponent},
  {path: "recipe", component: RecipeComponent},
  {path: "recipe/add", component: RecipeAddComponent},
  {path: "recipe/:id", component: RecipeDetailComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
