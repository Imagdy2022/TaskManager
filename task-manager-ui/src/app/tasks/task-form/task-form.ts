import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TaskService } from '../../services/task';
import { TaskItem, TaskItemStatus } from '../../models/task-item.model';

@Component({
  selector: 'app-task-form',
  standalone: false,
  templateUrl: './task-form.html',
  styleUrl: './task-form.scss',
})
export class TaskForm implements OnInit {
  @Input() task: TaskItem | null = null;
  @Input() projectId!: number;
  @Output() saved = new EventEmitter<void>();
  @Output() cancelled = new EventEmitter<void>();

  form!: FormGroup;
  saving = false;
  error = '';

  constructor(
    private fb: FormBuilder,
    private taskService: TaskService,
    private cdr: ChangeDetectorRef,
  ) {}

  ngOnInit(): void {
    const dueDateStr = this.task?.dueDate
      ? new Date(this.task.dueDate).toISOString().substring(0, 10)
      : '';

    this.form = this.fb.group({
      title: [this.task?.title ?? '', [Validators.required, Validators.maxLength(300)]],
      description: [this.task?.description ?? '', Validators.maxLength(2000)],
      status: [this.task?.status ?? TaskItemStatus.ToDo, Validators.required],
      dueDate: [dueDateStr],
    });
  }

  onSubmit(): void {
    if (this.form.invalid) return;
    this.saving = true;
    this.error = '';

    const v = this.form.value;
    const payload = {
      title: v.title.trim(),
      description: v.description?.trim() || undefined,
      status: Number(v.status),
      dueDate: v.dueDate || undefined,
    };

    const request$ = this.task
      ? this.taskService.update(this.task.id, payload)
      : this.taskService.create({ ...payload, projectId: this.projectId });

    request$.subscribe({
      next: () => { this.saving = false; this.saved.emit(); },
      error: (err) => {
        this.saving = false;
        this.error = err?.error?.message ?? 'Save failed. Please try again.';
        this.cdr.detectChanges();
      }
    });
  }

  onCancel(): void {
    this.cancelled.emit();
  }
}
