import { Component, OnInit } from '@angular/core';
import { RoomService } from '../room.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-all-rooms',
  templateUrl: './all-rooms.component.html',
  styleUrls: ['./all-rooms.component.css']
})
export class AllRoomsComponent implements OnInit {

  constructor(private room : RoomService,private datepipe : DatePipe) { }

  roomList : any;

  displayedColumns : string[] = ['roomNumber','createdTime','temperature','humidity','Lights on'];

  //runs GetRoom function on initialization
  ngOnInit(): void {
    this.GetRoom();
  }

  //Calls room service to get latest entry of all rooms.
  async GetRoom() {
    this.room.getLatestAll().subscribe(e=>{
      this.roomList = e;
      console.log(this.roomList[0].roomNumber)
      this.roomList.forEach(element => {
        var lightlvl;
        lightlvl = element.photoResistor.lightLevel 
        element.createdTime = this.datepipe.transform(element.createdTime,'yyyy-MM-dd hh:mm:ss');
        element.photoResistor.lightLevel = lightlvl == "1" ? "Yes" : "no";
      });
    });
  }

}
