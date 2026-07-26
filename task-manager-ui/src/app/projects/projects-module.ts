import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { ProjectList } from './project-list/project-list';
import { ProjectDetail } from './project-detail/project-detail';
import { ProjectForm } from './project-form/project-form';
import { SharedModule } from '../shared/shared-module';
import { TasksModule } from '../tasks/tasks-module';

@NgModule({
  declarations: [ProjectList, ProjectDetail, ProjectForm],
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    FormsModule,
    SharedModule,
    TasksModule,
  ],
})
export class ProjectsModule {}
