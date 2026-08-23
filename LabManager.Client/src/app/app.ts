import { ChangeDetectorRef, Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { Greeting } from './greeting';
import { PersonService } from './services/person.service';
import { Person } from './models/person';
import { PersonAddRequest } from './models/person-add-request';

@Component({
  selector: 'app-root',
  imports: [FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {

  private readonly greeting: Greeting;
  private readonly personService: PersonService;
  private readonly changeDetector: ChangeDetectorRef;

  public persons: Person[] = [];

  public newPerson: PersonAddRequest = {
    name: '',
    email: ''
  };

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

    this.loadPersons();
  }

  public loadPersons(): void
  {
    this.personService.getAllPersons().subscribe(persons =>
    {
      this.persons = persons;
      this.changeDetector.detectChanges();
    });
  }

  public addPerson(): void
  {
    this.personService.addPerson(this.newPerson).subscribe(person =>
    {
      this.persons.push(person);

      this.newPerson = {
        name: '',
        email: ''
      };

      this.changeDetector.detectChanges();
    });
  }
}