import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';

import { UserRegisterDto } from '../../../models/auth/user-register-dto';

@Component({
    selector: 'app-registration',
    templateUrl: './registration.component.html',
    styleUrls: ['./registration.component.sass'],
})
export class RegistrationComponent implements OnInit {
    registerForm: FormGroup = new FormGroup({});

    showPassword = false;

    showConfirmPassword = false;

    private fb: FormBuilder;

    constructor(fb: FormBuilder) {
        this.fb = fb;
    }

    ngOnInit() {
        this.initializeForm();
    }

    initializeForm() {
        this.registerForm = this.fb.group({
            username: ['', Validators.required],
            email: ['', Validators.required],
            password: ['', Validators.required],
            confirmPassword: ['', [Validators.required, this.matchValues('password')]],
        });
        this.registerForm.controls['password'].valueChanges.subscribe({
            next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity(),
        });
    }

    matchValues(matchTo: string): ValidatorFn {
        return (control: AbstractControl) =>
            (control.value === control.parent?.get(matchTo)?.value ? null : { notMatching: true });
    }

    validationCheck = (control: string, errorName: string) =>
        this.registerForm.controls[control].errors?.[errorName] && this.registerForm.controls[control].touched;

    register() {
        const userRegistrationData: UserRegisterDto = {
            username: this.registerForm.value.username,
            email: this.registerForm.value.email,
            password: this.registerForm.value.password,
        };

        // temporary solution
        // eslint-disable-next-line no-console
        console.log(userRegistrationData);
    }
}
