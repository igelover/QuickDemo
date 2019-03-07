import { Component, OnInit } from '@angular/core';
import { TodoListService } from './shared/todo-list.service';
import { WebsocketService } from './shared/websocket.service';
import { ClockService } from './shared/clock.service';

@Component({
  selector: 'app-quickdemo',
  templateUrl: './quickdemo.component.html',
  styleUrls: ['./quickdemo.component.css'],
  providers: [TodoListService, WebsocketService, ClockService]
})
export class QuickdemoComponent implements OnInit {

  constructor(private todoListService: TodoListService, private clockService: ClockService) { }

  ngOnInit() {
  }

}
