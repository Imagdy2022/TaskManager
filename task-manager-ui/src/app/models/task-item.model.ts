export enum TaskItemStatus {
  ToDo = 0,
  InProgress = 1,
  Done = 2
}

export interface TaskItem {
  id: number;
  title: string;
  description?: string;
  status: TaskItemStatus;
  statusLabel: string;
  dueDate?: string;
  projectId: number;
}

export interface CreateTaskRequest {
  title: string;
  description?: string;
  status: TaskItemStatus;
  dueDate?: string;
  projectId: number;
}

export interface UpdateTaskRequest {
  title: string;
  description?: string;
  status: TaskItemStatus;
  dueDate?: string;
}

export interface UpdateTaskStatusRequest {
  status: TaskItemStatus;
}
