import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { CreateProjectRequest, Project, ProjectDetail, UpdateProjectRequest } from '../models/project.model';

interface ApiResponse<T> { isSuccess: boolean; statusCode: number; message: string; data: T; }

@Injectable({ providedIn: 'root' })
export class ProjectService {
  private readonly url = `${environment.apiUrl}/projects`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<Project[]> {
    return this.http.get<ApiResponse<Project[]>>(this.url).pipe(map(r => r.data));
  }

  getById(id: number): Observable<ProjectDetail> {
    return this.http.get<ApiResponse<ProjectDetail>>(`${this.url}/${id}`).pipe(map(r => r.data));
  }

  create(request: CreateProjectRequest): Observable<Project> {
    return this.http.post<ApiResponse<Project>>(this.url, request).pipe(map(r => r.data));
  }

  update(id: number, request: UpdateProjectRequest): Observable<Project> {
    return this.http.put<ApiResponse<Project>>(`${this.url}/${id}`, request).pipe(map(r => r.data));
  }

  delete(id: number): Observable<void> {
    return this.http.delete<ApiResponse<null>>(`${this.url}/${id}`).pipe(map(() => void 0));
  }
}
