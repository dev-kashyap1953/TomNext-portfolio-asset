import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: 'main-screen', pathMatch: "full" },
  { path: 'main-screen', loadComponent:() => import('../app/components/screen-layout/screen-layout.component').then(c => c.ScreenLayoutComponent) },
  { path: '**', redirectTo: '' }
];
