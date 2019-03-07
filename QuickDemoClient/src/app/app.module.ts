import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { QuickdemoComponent } from './quickdemo/quickdemo.component';
import { TodoItemComponent } from './quickdemo/todo-item/todo-item.component';
import { TodoListComponent } from './quickdemo/todo-list/todo-list.component';
import { FormsModule} from '@angular/forms';
import { HttpModule } from '@angular/http';
import { ToastrModule } from 'ngx-toastr';
import { ClockComponent } from './quickdemo/clock/clock.component';

@NgModule({
  declarations: [
    AppComponent,
    QuickdemoComponent,
    TodoItemComponent,
    TodoListComponent,
    ClockComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    ToastrModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
