import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';


@Injectable({
  providedIn: 'root',
})
export class Greeting {
 
  private http: HttpClient;

  constructor(http: HttpClient)
  {
    this.http = http;
  }
  
  public GetGreeting(){

    return this.http.get("https://localhost:7098/api/Greeting");
  }
}
