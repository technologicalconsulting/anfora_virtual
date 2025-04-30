export interface LoginResponse {
    token: string;
    user?: {
      id: string;
      name: string;
      role: string;
    };
  }
  