import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {LoginComponent} from "./pages/login/login.component";
import {RegistrationComponent} from "./pages/registration/registration.component";
import {RecipeComponent} from "./pages/recipe/recipe.component";
import {IngredientComponent} from "./pages/ingredient/ingredient.component";
import {RecipeAddComponent} from "./pages/recipe-add/recipe-add.component";

const routes: Routes = [
  {path: '', redirectTo:'recipe', pathMatch:'full'},
  {path: "login", component: LoginComponent},
  {path: "register", component: RegistrationComponent},
  {path: "recipe", component: RecipeComponent},
  {path: "ingredient", component: IngredientComponent},
  {path: "recipe/add", component: RecipeAddComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
