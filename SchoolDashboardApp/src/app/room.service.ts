import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RoomService {
  constructor(private http: HttpClient) { 
  }
  getData(GetAllEntries : Boolean,roomNumber? : String){
    let url = "";
    if(!GetAllEntries){
      url = "http://192.168.1.101:48935/api/Angular/LatestSingle?roomNumber=" + roomNumber;
    }
    else {
      url = "http://192.168.1.101:48935/api/Angular/GetRoom?roomNumber=" + roomNumber;
    }
    return this.http.get<RoomObj>(url);
  }
}

export class RoomObj {
  roomNumber: string
  createdTime: Date
  humidId: string
  humidityTempSensor : HumidityTempSensor
  photoResistor : photoResistor
}

export class HumidityTempSensor {
  humidity:string
  temperature:string
}

export class photoResistor {
  lightLevel:string
}