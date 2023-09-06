import { AbstractControl, ValidatorFn } from '@angular/forms';

export class ValidationsFn {
    static wrongCharacters(): ValidatorFn {
        return (control: AbstractControl) =>
            (/(^[!-~]+$)/.test(control.value) ? null : { wrongCharacters: true });
    }

    static matchValues(matchTo: string): ValidatorFn {
        return (control: AbstractControl) =>
            (control.value === control.parent?.get(matchTo)?.value ? null : { notMatching: true });
    }

    static userNameMatch(): ValidatorFn {
        return (control: AbstractControl) =>
            (/(^[a-zA-Z\d-_]+$)/.test(control.value) ? null : { userNameMatch: true });
    }

    static nameMatch(): ValidatorFn {
        return (control: AbstractControl) =>
            (/(^[a-zA-Z-]+$)/.test(control.value) ? null : { nameMatch: true });
    }

    static emailMatch(): ValidatorFn {
        return (control: AbstractControl) =>
            (/^(?!\.)[A-Z0-9._-]+[^.@]@(?!\.)[A-Z0-9.-]+\.[A-Z]+(?<!\.)$/i.test(control.value)
            && !/[._-]{2,}/.test(control.value)
                ? null : { emailMatch: true });
    }

    static upperExist(): ValidatorFn {
        return (control: AbstractControl) =>
            (/(?=.*[A-Z])/.test(control.value)
                ? null : { upperExist: true });
    }

    static lowerExist(): ValidatorFn {
        return (control: AbstractControl) =>
            (/(?=.*[a-z])/.test(control.value)
                ? null : { lowerExist: true });
    }
}
