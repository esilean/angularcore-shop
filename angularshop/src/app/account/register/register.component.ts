import { Component, OnInit } from '@angular/core';
import {
  AsyncValidatorFn,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { of, timer } from 'rxjs';
import { switchMap, map } from 'rxjs/operators';
import { BreadcrumbService } from 'xng-breadcrumb';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  errors: string[];

  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
    private router: Router,
    private bcService: BreadcrumbService
  ) {}

  ngOnInit(): void {
    this.createRegisterForm();
    this.bcService.set('@register', 'Register')
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      displayName: [null, [Validators.required]],
      email: [
        null,
        [Validators.required, Validators.pattern(/^[^\s@]+@[^\s@]+\.[^\s@]+$/)],
        [this.validateEmailNotTaken()],
      ],
      password: [null, [Validators.required]],
    });
  }

  validateEmailNotTaken(): AsyncValidatorFn {
    return (control) => {
      return timer(500).pipe(
        switchMap(() => {
          if (!control.value) return of(null);

          return this.accountService.checkEmailExists(control.value).pipe(
            map((response) => {
              return response ? { emailExists: true } : null;
            })
          );
        })
      );
    };
  }

  onSubmit() {
    this.accountService.register(this.registerForm.value).subscribe(
      (_) => {
        this.router.navigateByUrl('/shop');
      },
      (error) => {
        console.log(error);
        this.errors = error.errors;
      }
    );
  }
}
