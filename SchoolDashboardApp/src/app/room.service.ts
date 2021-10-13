import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { Options } from 'selenium-webdriver';

@Injectable({
  providedIn: 'root'
})

export class RoomService {
  headerDict = {
    'Authorization': 'N0Clu3'
  }

  constructor(private http: HttpClient,private cookieService : CookieService) { 
  }

  //Gets latest data on a specific room based on roomnumber
  getData(GetAllEntries : Boolean,roomNumber? : String){
    let url = "";
    if(!GetAllEntries){
      url = "http://localhost:48935/api/Angular/LatestSingle?roomNumber=" + roomNumber;
    }
    else {
      url = "http://localhost:48935/api/Angular/GetRoom?roomNumber=" + roomNumber;
    }
    var token = this.cookieService.get('IsLogged')
    return this.http.get<RoomObj>(url, {headers: {'Authorization' :token}});
  }

  //Gets the latest entry of all rooms.
  getLatestAll(){
    let url = "http://localhost:48935/api/Angular/LatestAll";
    var token = this.cookieService.get('IsLogged')
    return this.http.get<RoomObj>(url, {headers: {'Authorization' :token}});
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