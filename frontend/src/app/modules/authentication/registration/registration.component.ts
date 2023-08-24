import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidationsFn } from '@shared/helpers/validations-fn';

import { UserRegisterDto } from '../../../models/auth/user-register-dto';

@Component({
    selector: 'app-registration',
    templateUrl: './registration.component.html',
    styleUrls: ['./registration.component.sass'],
})
export class RegistrationComponent implements OnInit {
    public registerForm: FormGroup = new FormGroup({});

    // eslint-disable-next-line no-empty-function
    constructor(private fb: FormBuilder) {
    }

    ngOnInit() {
        this.initializeForm();
    }

    private initializeForm() {
        this.registerForm = this.fb.group({
            username: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(25),
                ValidationsFn.userNameMatch()]],
            email: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50),
                ValidationsFn.emailMatch()]],
            firstName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(25),
                ValidationsFn.nameMatch()]],
            lastName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(25),
                ValidationsFn.nameMatch()]],
            password: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(25),
                ValidationsFn.wrongCharacters(), ValidationsFn.lowerExist(), ValidationsFn.upperExist()]],
            confirmPassword: ['', [Validators.required, ValidationsFn.matchValues('password')]],
        });
        this.registerForm.controls['password'].valueChanges.subscribe({
            next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity(),
        });
    }

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
