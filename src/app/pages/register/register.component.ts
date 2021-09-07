import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  email = new FormControl('', [Validators.required, Validators.email]);

  getErrorMessage(): string {
    return this.email.hasError('email') ? 'ایمیل وارد شده اشتباه است !' : '';
  }
}
