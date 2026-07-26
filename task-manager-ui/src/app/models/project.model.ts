import { TaskItem } from './task-item.model';

export interface Project {
  id: number;
  name: string;
  description?: string;
  createdAt: string;
}

export interface ProjectDetail extends Project {
  tasks: TaskItem[];
}

export interface CreateProjectRequest {
  name: string;
  description?: string;
}

export interface UpdateProjectRequest {
  name: string;
  description?: string;
}
