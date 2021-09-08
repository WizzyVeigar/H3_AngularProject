import { HtmlParser } from '@angular/compiler';
import { Component,ViewChild, OnInit } from '@angular/core';

@Component({
  selector: 'app-room-info',
  templateUrl: './room-info.component.html',
  styleUrls: ['./room-info.component.css']
})
export class RoomInfoComponent implements OnInit {

  dosomething(roomNum):void {
    alert("GET Call on room " + roomNum + " should happen now");
  }
  constructor() { }

  ngOnInit(): void {
  }

}
