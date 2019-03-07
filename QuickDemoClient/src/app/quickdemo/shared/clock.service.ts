import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Rx';
import { WebsocketService } from './websocket.service';

const WS_URL = 'ws://localhost:13627/api/WsTime/';

export interface Message {
  timestamp: string;
}

@Injectable({
  providedIn: 'root'
})
export class ClockService {
  public currentTime: string;
  public messages: Subject<Message>;

  constructor(wsService: WebsocketService) {
    this.messages = <Subject<Message>>wsService.connect(WS_URL).map(
      (response: MessageEvent): Message => {
        const data = JSON.parse(response.data);
        this.currentTime = data.Timestamp;
        return {
          timestamp: data.Timestamp
        };
      }
    );
  }
}
