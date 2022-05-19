import {
  Component,
  OnInit
} from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators
} from '@angular/forms';
import {
  Router
} from '@angular/router';
import {
  AuthService
} from '../auth.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnInit {

  form: FormGroup;

  constructor(
    private authService: AuthService,
    private router: Router,
    private fb: FormBuilder

  ) {
    this.form = fb.group({
      title: fb.control('initial value', Validators.required)
    });

  }

  ngOnInit(): void {
    this.form = new FormGroup({
      username: new FormControl(null, Validators.required),
      password: new FormControl(null, Validators.required)
    });
  }

  submit() {

    if (this.form.invalid) {
      return;
    }

    const user = {
      username: this.form.value.username,
      password: this.form.value.password
    }

    this.authService.login(user)
      .subscribe(data => {
        this.form.reset();
        this.router.navigateByUrl('/main/secret');
      });
  }

  register() {
    this.router.navigateByUrl('/register');
  }



}
