import { Component, OnInit } from '@angular/core';
import { TemplateRef, ViewChild } from '@angular/core';
import { User } from '../../model/user';
import { UserService } from '../../service/user.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-user',
  //selector: 'app-basic',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})

export class UserComponent implements OnInit {

  @ViewChild('readOnlyTemplate', {static: false}) readOnlyTemplate: TemplateRef<any>|null = null;
  @ViewChild('editTemplate', {static: false}) editTemplate: TemplateRef<any>|null = null;

  editedUser: User;


  users: Array<User>;
  isNewRecord: boolean = false;
  statusMessage: string = "";

  constructor(private router: Router, private service: UserService) {
    this.users = new Array<User>();
    this.editedUser = new User(0,"","","");
  }

  ngOnInit(): void {
    this.loadUsers();
  }

  private createNewUser(){
    this.editedUser = new User(0,"","","");
  }

  //загрузка пользователей
  private loadUsers() {
      this.service.getUsers().subscribe((data: Array<User>) => {
              this.users = data;
          });
  }
  // добавление пользователя
  addUser() {

      if (this.editedUser == null ) return;

      if (this.users.length != 0) {
          let user = this.users.reduce((a, b) => (a.id != null ? a.id : 0) > (b.id != null ? b.id : 0) ? a : b);
          //let user = this.users.reduce((a, b) => a.id?.valueOf() < b.id?.valueOf() ? a : b);

          if (user != null) {
              let id = user.id != null ? user.id + 1: 100000;
              this.editedUser.id = id;
          }
      }

      this.users.push(this.editedUser);
      this.isNewRecord = true;
  }

  // редактирование пользователя
  editUser(user: User) {
      this.editedUser = new User(user.id, user.name, user.email, user.password);
  }
  // загружаем один из двух шаблонов
  loadTemplate(user: User) {
      if (this.editedUser && this.editedUser.id === user.id) {
          return this.editTemplate;
      } else {
          return this.readOnlyTemplate;
      }
  }
  // сохраняем пользователя
  saveUser() {
      if (this.isNewRecord) {
          // добавляем пользователя
          this.service.createUser(this.editedUser as User).subscribe(data => {
              this.statusMessage = 'Данные успешно добавлены',
              this.loadUsers();
          });
          this.isNewRecord = false;
          //this.editedUser = null;
          this.createNewUser();
      } else {
          // изменяем пользователя
          this.service.updateUser(this.editedUser as User).subscribe(data => {
              this.statusMessage = 'Данные успешно обновлены',
              this.loadUsers();
          });
          //this.editedUser = null;
          this.createNewUser();
      }
  }
  // отмена редактирования
  cancel() {
      // если отмена при добавлении, удаляем последнюю запись
      if (this.isNewRecord) {
          this.users.pop();
          this.isNewRecord = false;

      }
      //this.editedUser = null;
      this.createNewUser();
  }
  // удаление пользователя
  deleteUser(user: User) {
      this.service.deleteUser(user.id).subscribe(data => {
          this.statusMessage = 'Данные успешно удалены',
          this.loadUsers();
      });
  }

  logout() {
    console.log('This function is work!');
    this.service.logout();
    this.router.navigate(['login']);
    //this.router.navigateByUrl('/index.html')
  }
}
