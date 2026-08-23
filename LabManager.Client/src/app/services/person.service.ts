import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Person } from '../models/person';

@Injectable({
  providedIn: 'root'
})
export class PersonService {

  private http: HttpClient;

  constructor(http: HttpClient) {
    this.http = http;
  }

  public getAllPersons() {
    return this.http.get<Person[]>('https://localhost:7098/api/persons');
  }
}