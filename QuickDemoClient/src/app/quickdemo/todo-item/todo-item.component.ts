import { Component, OnInit } from '@angular/core';
import { TodoListService } from '../shared/todo-list.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-todo-item',
  templateUrl: './todo-item.component.html',
  styleUrls: ['./todo-item.component.css']
})
export class TodoItemComponent implements OnInit {

  constructor(private todoListService: TodoListService) { }

  ngOnInit() {
    this.resetForm();
  }

  resetForm(form?: NgForm) {
    if (form != null) {
      form.reset();
    }
    this.todoListService.selectedItem = {
      Id: null,
      Description: '',
      Priority: null,
      IsDone: null
    };
  }

  onSubmit(form: NgForm) {
    if (form.value.Id == null) {
      this.todoListService.postItem(form.value)
        .subscribe(data => {
          this.resetForm(form);
          this.todoListService.getAll();
          alert('New Record Added Successfully');
        });
    } else {
      this.todoListService.putItem(form.value.Id, form.value)
      .subscribe(data => {
        this.resetForm(form);
        this.todoListService.getAll();
        alert('Record Updated Successfully');
      });
    }
  }

}
