import { HtmlParser } from '@angular/compiler';
import { Component,ViewChild, OnInit } from '@angular/core';
import { RoomObj, RoomService } from '../room.service';

@Component({
  selector: 'app-room-info',
  templateUrl: './room-info.component.html',
  styleUrls: ['./room-info.component.css']
})
export class RoomInfoComponent implements OnInit {

  roomList : any;
  roomtest : any;
  constructor(private room:RoomService) { 
  }

  ngOnInit(): void {
    this.room.getData().subscribe(e=>{
      this.roomList = e;
    });
  }
}
