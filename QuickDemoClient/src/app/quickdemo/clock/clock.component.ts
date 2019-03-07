import { Component, OnInit } from '@angular/core';
import { ClockService } from '../shared/clock.service';

@Component({
  selector: 'app-clock',
  templateUrl: './clock.component.html',
  styleUrls: ['./clock.component.css']
})
export class ClockComponent implements OnInit {

  constructor(private clockService: ClockService) {
    clockService.messages.subscribe(msg => {
      // console.log('Response from websocket: ' + msg.timestamp);
    });
   }

  ngOnInit() {
  }

}
