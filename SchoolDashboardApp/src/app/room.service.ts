import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RoomService {
  constructor(private http: HttpClient) { 
  }
  getData(roomNumber : String){
    let url = "";
    if(roomNumber != null){
      url = "http://192.168.1.101:48935/api/Angular/GetRoom?roomNumber=" + roomNumber;
    }
    else {
      url = "http://192.168.1.101:48935/api/Angular/GetRoom";
    }
    return this.http.get<RoomObj>(url);
  }
}

export class RoomObj {
  roomNumber: string
  createdTime: Date
  humidId: string
}
