import { ChangeDetectorRef, Component } from '@angular/core';

import { Greeting } from './greeting';
import { PersonService } from './services/person.service';
import { Person } from './models/person';

@Component({
  selector: 'app-root',
  imports: [],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {

  private readonly greeting: Greeting;
  private readonly personService: PersonService;
  private readonly changeDetector: ChangeDetectorRef;

  public persons: Person[] = [];

  constructor(
    greeting: Greeting,
    personService: PersonService,
    changeDetector: ChangeDetectorRef
  ) {
    this.greeting = greeting;
    this.personService = personService;
    this.changeDetector = changeDetector;

    this.greeting.GetGreeting().subscribe(saludo =>
    {
      console.log(saludo);
    });

    this.personService.getAllPersons().subscribe(persons =>
    {
      this.persons = persons;

      console.log(persons);

      this.changeDetector.detectChanges();
    });
  }
}