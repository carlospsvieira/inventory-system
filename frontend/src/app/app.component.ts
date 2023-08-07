import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `
    <main>
      <section class="content">
        <app-home></app-home>
      </section>
    </main>
  `,
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'frontend';
}
