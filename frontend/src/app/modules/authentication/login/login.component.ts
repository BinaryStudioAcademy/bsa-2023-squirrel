import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import { ValidationsFn } from '@shared/helpers/validations-fn';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.sass'],
})
export class LoginComponent implements OnInit {
    public loginForm: FormGroup = new FormGroup({});

    // eslint-disable-next-line no-empty-function
    constructor(private fb: FormBuilder) {}

    ngOnInit(): void {
        this.initializeForm();
    }

    private initializeForm() {
        this.loginForm = this.fb.group({
            email: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50),
                ValidationsFn.emailMatch()]],
            password: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(25)]],
        });
    }
}
