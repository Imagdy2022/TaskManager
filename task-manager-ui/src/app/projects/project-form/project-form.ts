import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProjectService } from '../../services/project';
import { Project } from '../../models/project.model';

@Component({
  selector: 'app-project-form',
  standalone: false,
  templateUrl: './project-form.html',
  styleUrl: './project-form.scss',
})
export class ProjectForm implements OnInit {
  @Input() project: Project | null = null;
  @Output() saved = new EventEmitter<void>();
  @Output() cancelled = new EventEmitter<void>();

  form!: FormGroup;
  saving = false;
  error = '';

  constructor(
    private fb: FormBuilder,
    private projectService: ProjectService,
    private cdr: ChangeDetectorRef,
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      name: [this.project?.name ?? '', [Validators.required, Validators.maxLength(200)]],
      description: [this.project?.description ?? '', Validators.maxLength(1000)],
    });
  }

  onSubmit(): void {
    if (this.form.invalid) return;
    this.saving = true;
    this.error = '';

    const payload = {
      name: this.form.value.name.trim(),
      description: this.form.value.description?.trim() || undefined,
    };

    const request$ = this.project
      ? this.projectService.update(this.project.id, payload)
      : this.projectService.create(payload);

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
