import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../../auth/auth.service';
@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  currentUserIsAdmin = false;

  constructor(
    private authService : AuthService,
    private router: Router
    ) {
  }

  ngOnInit(): void {
    let a = 0;
    this.authService.whoami()
          .subscribe(data => {
            let result = Number.parseInt(data);
            console.log(result);
            if (result == 1) {
              this.currentUserIsAdmin = true;
            }
        });

  }


  logout() {
    this.authService.logout()
    .subscribe(() => {
      this.router.navigate(['login']);
    });

  }

}
