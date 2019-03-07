import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions, RequestMethod } from '@angular/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';

import { TodoItem } from './todo-item.model';


@Injectable({
  providedIn: 'root'
})
export class TodoListService {
  selectedItem: TodoItem;
  todoList: TodoItem[];
  constructor(private http: Http) { }

  postItem(item: TodoItem){
    const body = JSON.stringify(item);
    const headerOptions = new Headers({'Content-Type': 'application/json'});
    const requestOptions = new RequestOptions({method : RequestMethod.Post, headers : headerOptions});
    return this.http.post('http://localhost:13627/api/ToDo', body, requestOptions).map(res => res.json());
  }

  putItem(id: number, item: TodoItem) {
    const body = JSON.stringify(item);
    const headerOptions = new Headers({ 'Content-Type': 'application/json' });
    const requestOptions = new RequestOptions({ method: RequestMethod.Put, headers: headerOptions });
    return this.http.put('http://localhost:13627/api/ToDo/' + id, body, requestOptions).map(res => res.json());
  }

  getAll(){
    this.http.get('http://localhost:13627/api/ToDo/')
    .map((data: Response) => {
      return data.json() as TodoItem[];
    }).toPromise().then(x => {
      this.todoList = x;
    });
  }

  deleteItem(id: number) {
    return this.http.delete('http://localhost:13627/api/ToDo/' + id).map(res => res.status);
  }
}
