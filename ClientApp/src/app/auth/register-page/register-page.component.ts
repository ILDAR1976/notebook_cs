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
import {
  User
} from '../../secret/model/user';


@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.scss']
})

export class RegisterPageComponent implements OnInit {

  form: FormGroup;
  user: User| null = null;

  ngOnInit(): void {
    this.form = new FormGroup({
      mainname: new FormControl(null, Validators.required),
      username: new FormControl(null, Validators.required),
      password: new FormControl(null, Validators.required)
    });
  }

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private service: AuthService
  ) {
    this.form = fb.group({
      title: fb.control('initial value', Validators.required)
    });

  }

  submit(){

    if (this.form.invalid) {
      return;
    }

    this.user = new User(null, this.form.value.mainname,this.form.value.username,this.form.value.password);
    this.service.createUser(this.user).subscribe(data => {});
    this.form.reset();
    this.router.navigate(['login']);
  }

}
