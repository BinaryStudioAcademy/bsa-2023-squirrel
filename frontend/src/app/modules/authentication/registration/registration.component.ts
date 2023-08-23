import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';

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

    // eslint-disable-next-line no-empty-function
    constructor(private fb: FormBuilder) {}

    ngOnInit() {
        this.initializeForm();
    }

    private initializeForm() {
        this.registerForm = this.fb.group({
            username: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(25)]],
            email: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50),
                Validators.email]],
            firstName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(25)]],
            lastName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(25)]],
            password: ['', Validators.required],
            confirmPassword: ['', [Validators.required, this.matchValues('password')]],
        });
        this.registerForm.controls['password'].valueChanges.subscribe({
            next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity(),
        });
    }

    public matchValues(matchTo: string): ValidatorFn {
        return (control: AbstractControl) =>
            (control.value === control.parent?.get(matchTo)?.value ? null : { notMatching: true });
    }

    public validationCheck = (control: string, errorName: string) =>
        this.registerForm.controls[control].errors?.[errorName] && this.registerForm.controls[control].touched;

    public register() {
        const userRegistrationData: UserRegisterDto = {
            username: this.registerForm.value.username,
            email: this.registerForm.value.email,
            firstName: this.registerForm.value.firstName,
            lastName: this.registerForm.value.lastName,
            password: this.registerForm.value.password,
        };

        // temporary solution
        // eslint-disable-next-line no-console
        console.log(userRegistrationData);
    }
}
