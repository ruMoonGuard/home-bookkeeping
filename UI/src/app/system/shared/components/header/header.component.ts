import { Component, OnInit } from '@angular/core';

import { AuthService } from '../../../../shared/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'csw-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  date: Date = new Date();
  username: string;

  constructor(
    private authService: AuthService,
    private router: Router
    ) { }

  ngOnInit() {
    this.username = window.localStorage.getItem('name');
  }

  onLogout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
