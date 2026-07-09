import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class AuthService {
  login(username: string, password: string, role: string) {
    if (role === 'admin' && username === 'admin' && password === '123') {
      localStorage.setItem('isLoggedIn', 'true');
      localStorage.setItem('role', 'admin');
      localStorage.setItem('username', 'admin');
      return true;
    }

    if (role === 'user' && username === 'user' && password === '123') {
      localStorage.setItem('isLoggedIn', 'true');
      localStorage.setItem('role', 'user');
      localStorage.setItem('username', 'user');
      return true;
    }

    return false;
  }

  logout() {
    localStorage.clear();
  }

  getRole() {
    return localStorage.getItem('role');
  }

  isAdmin() {
    return this.getRole() === 'admin';
  }

  isUser() {
    return this.getRole() === 'user';
  }

  isLoggedIn() {
    return localStorage.getItem('isLoggedIn') === 'true';
  }
}
