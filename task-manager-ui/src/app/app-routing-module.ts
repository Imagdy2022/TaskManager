import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProjectList } from './projects/project-list/project-list';
import { ProjectDetail } from './projects/project-detail/project-detail';

const routes: Routes = [
  { path: 'projects', component: ProjectList },
  { path: 'projects/:id', component: ProjectDetail },
  { path: '', component: ProjectList },
  { path: '**', component: ProjectList },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
