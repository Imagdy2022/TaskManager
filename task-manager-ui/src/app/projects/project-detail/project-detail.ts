import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProjectService } from '../../services/project';
import { TaskService } from '../../services/task';
import { ProjectDetail as ProjectDetailModel } from '../../models/project.model';
import { TaskItem, TaskItemStatus } from '../../models/task-item.model';

@Component({
  selector: 'app-project-detail',
  standalone: false,
  templateUrl: './project-detail.html',
  styleUrl: './project-detail.scss',
})
export class ProjectDetail implements OnInit {
  project: ProjectDetailModel | null = null;
  filteredTasks: TaskItem[] = [];
  loading = false;
  error = '';
  statusFilter = '';

  showForm = false;
  editingTask: TaskItem | null = null;
  showConfirm = false;
  deletingId: number | null = null;
  projectId = 0;

  constructor(
    private route: ActivatedRoute,
    private projectService: ProjectService,
    private taskService: TaskService,
    private cdr: ChangeDetectorRef,
  ) {}

  ngOnInit(): void {
    this.projectId = Number(this.route.snapshot.paramMap.get('id'));
    this.load();
  }

  load(): void {
    this.loading = true;
    this.projectService.getById(this.projectId).subscribe({
      next: (data: ProjectDetailModel) => {
        this.project = data;
        this.applyFilter();
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: () => { this.error = 'Failed to load project.'; this.loading = false; this.cdr.detectChanges(); }
    });
  }

  applyFilter(): void {
    if (!this.project) return;
    if (this.statusFilter === '') {
      this.filteredTasks = [...this.project.tasks];
    } else {
      this.filteredTasks = this.project.tasks.filter(t => t.status === Number(this.statusFilter));
    }
    this.cdr.detectChanges();
  }

  changeStatus(taskId: number, status: TaskItemStatus): void {
    this.taskService.updateStatus(taskId, { status }).subscribe({
      next: (updated) => {
        if (this.project) {
          const idx = this.project.tasks.findIndex(t => t.id === taskId);
          if (idx !== -1) {
            this.project.tasks[idx] = updated;
            this.applyFilter();
          }
        }
      },
      error: () => { this.error = 'Failed to update task status.'; this.cdr.detectChanges(); }
    });
  }

  openCreate(): void {
    this.editingTask = null;
    this.showForm = true;
  }

  openEdit(task: TaskItem): void {
    this.editingTask = task;
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
    this.taskService.delete(this.deletingId).subscribe({
      next: () => { this.showConfirm = false; this.deletingId = null; this.load(); },
      error: () => { this.error = 'Failed to delete task.'; this.showConfirm = false; this.cdr.detectChanges(); }
    });
  }

  onDeleteCancelled(): void {
    this.showConfirm = false;
    this.deletingId = null;
  }
}
