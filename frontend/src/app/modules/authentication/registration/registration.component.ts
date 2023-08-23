/* eslint-disable no-empty-function */
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '@core/services/auth.service';
import { SpinnerService } from '@core/services/spinner.service';

import { UserRegisterDto } from '../../../models/auth/user-register-dto';

@Component({
    selector: 'app-registration',
    templateUrl: './registration.component.html',
    styleUrls: ['./registration.component.sass'],
})
export class RegistrationComponent implements OnInit {
    public registerForm: FormGroup = new FormGroup({});

    public showPassword = false;

    public showConfirmPassword = false;

    constructor(
        private fb: FormBuilder,
        private router: Router,
        private spinner: SpinnerService,
        private authService: AuthService,
    ) {}

    public ngOnInit() {
        this.initializeForm();
    }

    public matchValues(matchTo: string): ValidatorFn {
        // eslint-disable-next-line no-confusing-arrow
        return (control: AbstractControl) =>
            control.value === control.parent?.get(matchTo)?.value ? null : { notMatching: true };
    }

    public validationCheck = (control: string, errorName: string) =>
        this.registerForm.controls[control].errors?.[errorName] && this.registerForm.controls[control].touched;

    public register() {
        // return if !form.valid or error from api request
        if (!this.registerForm.valid) {
            return;
        }

        this.spinner.show();

        const userRegistrationData: UserRegisterDto = {
            username: this.registerForm.value.username,
            email: this.registerForm.value.email,
            firstName: this.registerForm.value.firstName,
            lastName: this.registerForm.value.lastName,
            password: this.registerForm.value.password,
        };

        this.authService.register(userRegistrationData).subscribe((result) => {
            console.log(result);
            this.spinner.hide();
            this.router.navigateByUrl('/main');
        });
    }

    private initializeForm() {
        this.registerForm = this.fb.group({
            username: ['', Validators.required],
            email: ['', Validators.required],
            firstName: ['', Validators.required],
            lastName: ['', Validators.required],
            password: ['', Validators.required],
            confirmPassword: ['', [Validators.required, this.matchValues('password')]],
        });
        this.registerForm.controls['password'].valueChanges.subscribe({
            next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity(),
        });
    }
}
