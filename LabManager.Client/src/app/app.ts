import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Greeting } from './greeting';


@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('LabManager.Client');

  private readonly greeting: Greeting;

  constructor(greeting: Greeting)
  {

    this.greeting = greeting;

    
    this.greeting.GetGreeting().subscribe(saludo => 
    { 
      console.log(saludo);
    });
  }

}
