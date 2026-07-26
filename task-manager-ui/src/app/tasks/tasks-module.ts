import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { TaskForm } from './task-form/task-form';

@NgModule({
  declarations: [TaskForm],
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  exports: [TaskForm],
})
export class TasksModule {}
