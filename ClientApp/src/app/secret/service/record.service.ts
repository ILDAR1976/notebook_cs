import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Record} from '../model/record';

@Injectable()
export class RecordService{

    private url = "http://localhost:8080/base/rest/profile/records";
    constructor(private http: HttpClient){ }

    getRecord(){
        return this.http.get<Array<Record>>(this.url);
    }

    createRecord(record: Record){
        const myHeaders = new HttpHeaders().set("Content-Type", "application/json");
        return this.http.post<Record>(this.url, JSON.stringify(record), {headers: myHeaders});
    }

    updateRecord(record: Record) {
        const myHeaders = new HttpHeaders().set("Content-Type", "application/json");
        return this.http.put<Record>(this.url + "/" + record.id, JSON.stringify(record), {headers:myHeaders});
    }

    deleteRecord(id: number|null = null){
        return this.http.delete<Record>(this.url + '/' + id);
    }

    logout() {
      console.log('The pass this phase');
      return this.http.get('${environment.host}/logout');
    }

}
