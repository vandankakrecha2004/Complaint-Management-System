import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { OnInit } from '@angular/core';

@Component({
  standalone: true,
  selector: 'app-login',
  templateUrl: './login.html',
  imports: [CommonModule, FormsModule],
})
export class LoginComponent implements OnInit {
  username = '';
  password = '';
  role = '';

  constructor(
    private auth: AuthService,
    private router: Router,
  ) {}

  ngOnInit() {
    if (this.auth.isLoggedIn()) {
      this.router.navigate(['/complaints']);
    }
  }

  login() {
    if (this.auth.login(this.username, this.password, this.role)) {
      this.router.navigate(['/complaints']);
    } else {
      alert('Invalid login');
    }
  }
}
