import { NgModule, provideZoneChangeDetection } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { ProjectsModule } from './projects/projects-module';
import { TasksModule } from './tasks/tasks-module';
import { SharedModule } from './shared/shared-module';

@NgModule({
  declarations: [App],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    SharedModule,
    ProjectsModule,
    TasksModule,
  ],
  providers: [provideZoneChangeDetection({ eventCoalescing: true })],
  bootstrap: [App],
})
export class AppModule {}
