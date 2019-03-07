import { Component, OnInit } from '@angular/core';
import { TodoListService } from '../shared/todo-list.service';
import { TodoItem } from '../shared/todo-item.model';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.css']
})
export class TodoListComponent implements OnInit {

  constructor(private todoListService: TodoListService) { }

  ngOnInit() {
    this.todoListService.getAll();
  }

  showForEdit(item: TodoItem) {
    this.todoListService.selectedItem = Object.assign({}, item);
  }

  onDelete(id: number) {
    if (confirm('Are you sure to delete this record ?') === true) {
      this.todoListService.deleteItem(id)
      .subscribe(x => {
        this.todoListService.getAll();
        alert('Deleted Successfully');
      });
    }
  }
}
