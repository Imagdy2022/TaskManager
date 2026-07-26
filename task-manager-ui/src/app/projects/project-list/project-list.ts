import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ProjectService } from '../../services/project';
import { Project } from '../../models/project.model';

@Component({
  selector: 'app-project-list',
  standalone: false,
  templateUrl: './project-list.html',
  styleUrl: './project-list.scss',
})
export class ProjectList implements OnInit {
  projects: Project[] = [];
  loading = false;
  error = '';

  showForm = false;
  editingProject: Project | null = null;
  showConfirm = false;
  deletingId: number | null = null;

  constructor(private projectService: ProjectService, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.loading = true;
    this.projectService.getAll().subscribe({
      next: (data) => { this.projects = data; this.loading = false; this.cdr.detectChanges(); },
      error: () => { this.error = 'Failed to load projects.'; this.loading = false; this.cdr.detectChanges(); }
    });
  }

  openCreate(): void {
    this.editingProject = null;
    this.showForm = true;
  }

  openEdit(project: Project): void {
    this.editingProject = project;
    this.showForm = true;
  }

  onFormSaved(): void {
    this.showForm = false;
    this.load();
  }

  onFormCancelled(): void {
    this.showForm = false;
  }

  confirmDelete(id: number): void {
    this.deletingId = id;
    this.showConfirm = true;
  }

  onDeleteConfirmed(): void {
    if (this.deletingId === null) return;
    this.projectService.delete(this.deletingId).subscribe({
      next: () => { this.showConfirm = false; this.deletingId = null; this.load(); },
      error: () => { this.error = 'Failed to delete project.'; this.showConfirm = false; this.cdr.detectChanges(); }
    });
  }

  onDeleteCancelled(): void {
    this.showConfirm = false;
    this.deletingId = null;
  }
}
