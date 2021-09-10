import { HtmlParser } from '@angular/compiler';
import { Component,ViewChild, OnInit } from '@angular/core';
import { RoomObj, RoomService } from '../room.service';
import { DatePipe } from '@angular/common';

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
    this.room.getData('H.01').subscribe(e=>{
      this.roomList = e;
    });

    this.specificRoom = new RoomObj();
  }

  async GetRoom(roomNumber:String) {
    this.room.getData(roomNumber).subscribe(e=>{
      this.specificRoom = e[1];
      console.log(this.datepipe.transform(this.specificRoom.createdTime,'yyyy-MM-dd'));
      document.getElementById("roomNumber").innerText = this.specificRoom.roomNumber;
      document.getElementById("createdTime").innerText = this.datepipe.transform(this.specificRoom.createdTime,'yyyy-MM-dd hh-mm-ss');
      document.getElementById("humidId").innerText = this.specificRoom.humidId;
    });
  }


}
