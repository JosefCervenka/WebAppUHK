import {Unit} from "./Unit";

export interface Ingredient {
  id: number;
  name: string;
  count: number;
  unit: Unit;
}
