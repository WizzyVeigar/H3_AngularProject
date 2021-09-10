import { HtmlParser } from '@angular/compiler';
import { Component,ViewChild, OnInit } from '@angular/core';
import { RoomObj, RoomService } from '../room.service';
import { DatePipe } from '@angular/common';
import { DataSource } from '@angular/cdk/collections';

@Component({
  selector: 'app-room-info',
  templateUrl: './room-info.component.html',
  styleUrls: ['./room-info.component.css']
})
export class RoomInfoComponent implements OnInit {

  roomList : any;
  specificRoom : any;
  constructor(private room:RoomService,public datepipe: DatePipe) { 
  }

  ngOnInit(): void {
  }

  displayedColumns : string[] = ["roomNumber"];

  async GetRoom(roomNumber:String) {
    this.room.getData(false,roomNumber).subscribe(e=>{
      this.specificRoom = e;
      document.getElementById("roomNumber").innerText = this.specificRoom.roomNumber;
      document.getElementById("createdTime").innerText = this.datepipe.transform(this.specificRoom.createdTime,'yyyy-MM-dd hh-mm-ss');
      document.getElementById("temperature").innerText = this.specificRoom.humidityTempSensor.temperature;
      document.getElementById("humidity").innerText = this.specificRoom.humidityTempSensor.humidity + "%";
      document.getElementById("light").innerText = this.specificRoom.photoResistor.lightLevel == "1" ? "Yes" : "No";
    });

    this.room.getData(true,roomNumber).subscribe(e=>{
      this.roomList = e;
      console.log(this.roomList);
    });
    
  }


}
