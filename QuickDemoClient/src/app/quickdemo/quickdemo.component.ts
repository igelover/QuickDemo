import { Component, OnInit } from '@angular/core';
import { TodoListService } from './shared/todo-list.service';

@Component({
  selector: 'app-quickdemo',
  templateUrl: './quickdemo.component.html',
  styleUrls: ['./quickdemo.component.css'],
  providers: [TodoListService]
})
export class QuickdemoComponent implements OnInit {

  constructor(private todoListService: TodoListService) { }

  ngOnInit() {
  }

}
