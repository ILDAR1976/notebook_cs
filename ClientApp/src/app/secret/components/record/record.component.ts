import { Component, OnInit } from '@angular/core';
import { TemplateRef, ViewChild } from '@angular/core';
import { Record } from '../../model/record';
import { RecordService } from '../../service/record.service';
import { Router } from '@angular/router';
import { noUndefined } from '@angular/compiler/src/util';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-record',
  //selector: 'app-basic',
  templateUrl: './record.component.html',
  styleUrls: ['./record.component.scss']
})

export class RecordComponent implements OnInit {

  @ViewChild('readOnlyTemplate', {static: false}) readOnlyTemplate: TemplateRef<any>|null = null;
  @ViewChild('editTemplate', {static: false}) editTemplate: TemplateRef<any>|null = null;

  editedRecord: Record;
  searchText = '';

  records: Array<Record>;
  isNewRecord: boolean = false;
  statusMessage: string = "";
  timeZoneOffset: number;

  constructor(private router: Router,
    private service: RecordService,
    private datepipe: DatePipe) {
      this.records = new Array<Record>();
      let currentDate = new Date();
      this.timeZoneOffset = currentDate.getTimezoneOffset();
      this.editedRecord = new Record(0,currentDate,"");
  }

  ngOnInit(): void {
    this.loadRecords();
  }

  private createNewRecord(){
    let currentDate = new Date();
    this.editedRecord = new Record(0,currentDate,"");
  }

  // Load records
  private loadRecords() {
      this.service.getRecord().subscribe((data: Array<Record>) => {
              this.records = new Array<Record>();
              for (let index = 0; index < data.length; index++) {
                const element = data[index];
                let a1: string = new Date(element.dateTime).toString().replace('Z','').concat('Z');
                let a2 = new Date(a1);
                var newRecord = new Record(element.id, a2, element.description);
                this.records.push(newRecord);
              }

              // The sorted records
              this.records.sort((a,b) => {
                // The compare function
                if ((a.id != null ? a.id : 0) < (b.id != null ? b.id : 0) ) {
                  return -1;
                }
                if (a.id != null) {
                  return 1;
                }
                return 0;
              });
          });
  }

  // Add user
  addRecord() {
      this.createNewRecord();
      if (this.records.length != 0) {
          let user = this.records.reduce((a, b) => (a.id != null ? a.id : 0) > (b.id != null ? b.id : 0) ? a : b);
          if (user != null) {
              let id = user.id != null ? user.id + 1: 100000;
              this.editedRecord.id = id;
          }
      }

      this.records.push(this.editedRecord);
      this.isNewRecord = true;
  }

  // Editing a notebook entry
  editRecord(record: Record) {
    this.editedRecord.id = record.id;
    this.editedRecord.dateTime = record.dateTime;
    this.editedRecord.description = record.description;
  }

  // Load one of the two templates
  loadTemplate(record: Record) {
      if (this.editedRecord && this.editedRecord.id === record.id) {
          return this.editTemplate;
      } else {
          return this.readOnlyTemplate;
      }
  }
  // Saving notes in the notebook
  saveRecord() {
      if (this.isNewRecord) {
          // Add record
          this.service.createRecord(this.editedRecord as Record).subscribe(data => {
              this.statusMessage = 'Данные успешно добавлены' ,
              this.loadRecords();
          });
          this.isNewRecord = false;

          this.createNewRecord();
      } else {
          // Update record
          this.service.updateRecord(this.editedRecord as Record).subscribe(data => {
              this.statusMessage = 'Данные успешно обновлены',
              this.loadRecords();
          });

          this.createNewRecord();
      }
  }
  // unediting
  cancel() {
      // if canceled on addition, delete the last entry
      if (this.isNewRecord) {
          this.records.pop();
          this.isNewRecord = false;
      }

      this.createNewRecord();
  }
  // Delete record
  deleteRecord(user: Record) {
      this.service.deleteRecord(user.id).subscribe(data => {
          this.statusMessage = 'Данные успешно удалены',
          this.loadRecords();
      });
  }

  logout() {
    this.service.logout();
    this.router.navigate(['login']);
  }

  currentDatetimeToString(inputDatetime : Date) : string {
    //this.datepipe.transform(inputDatetime, 'dd.MM.yyyy hh:mm')
   /*  let date = new Date(inputDatetime);
    return ((date.getDay() < 10 ? '0' + date.getDay() : date.getDay()) + '.' +
            ((date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1)) +  '.' +
            date.getFullYear() + ' ' + date.toLocaleTimeString('ru-RU')); */
    return "Дата";
  }
}

