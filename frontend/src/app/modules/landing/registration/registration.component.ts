import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';

@Component({
    selector: 'app-registration',
    templateUrl: './registration.component.html',
    styleUrls: ['./registration.component.sass'],
})
export class RegistrationComponent implements OnInit {
    registerForm: FormGroup = new FormGroup({});

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

    register() {
        console.log(this.registerForm.value);
    }
}
