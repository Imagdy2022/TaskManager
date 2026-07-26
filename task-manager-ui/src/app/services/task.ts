import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { CreateTaskRequest, TaskItem, TaskItemStatus, UpdateTaskRequest, UpdateTaskStatusRequest } from '../models/task-item.model';

interface ApiResponse<T> { isSuccess: boolean; statusCode: number; message: string; data: T; }

@Injectable({ providedIn: 'root' })
export class TaskService {
  private readonly url = `${environment.apiUrl}/tasks`;

  constructor(private http: HttpClient) {}

  getByProject(projectId: number): Observable<TaskItem[]> {
    return this.http.get<ApiResponse<TaskItem[]>>(`${this.url}/by-project/${projectId}`).pipe(map(r => r.data));
  }

  getByStatus(status: TaskItemStatus): Observable<TaskItem[]> {
    return this.http.get<ApiResponse<TaskItem[]>>(`${this.url}/by-status/${status}`).pipe(map(r => r.data));
  }

  create(request: CreateTaskRequest): Observable<TaskItem> {
    return this.http.post<ApiResponse<TaskItem>>(this.url, request).pipe(map(r => r.data));
  }

  update(id: number, request: UpdateTaskRequest): Observable<TaskItem> {
    return this.http.put<ApiResponse<TaskItem>>(`${this.url}/${id}`, request).pipe(map(r => r.data));
  }

  updateStatus(id: number, request: UpdateTaskStatusRequest): Observable<TaskItem> {
    return this.http.patch<ApiResponse<TaskItem>>(`${this.url}/${id}/status`, request).pipe(map(r => r.data));
  }

  delete(id: number): Observable<void> {
    return this.http.delete<ApiResponse<null>>(`${this.url}/${id}`).pipe(map(() => void 0));
  }
}
