import { AfterViewInit, Component, ElementRef, Input } from '@angular/core';
import { AuthService } from '@core/services/auth.service';
import { SpinnerService } from '@core/services/spinner.service';
import { environment } from '@env/environment';
import { CredentialResponse } from 'google-one-tap';

declare const google: any;

@Component({
    selector: 'app-google-button',
    templateUrl: './google-button.component.html',
    styleUrls: ['./google-button.component.sass'],
})
export class GoogleButtonComponent implements AfterViewInit {
    @Input() public authType = 'signin_with';

    public width: string;

    constructor(private authService: AuthService, private spinner: SpinnerService, private elementRef: ElementRef) { }

    public ngAfterViewInit(): void {
        this.spinner.show();
        this.width = `${this.elementRef.nativeElement.querySelector('#signInGoogle').offsetWidth.toString()}px`;
        // button rendering should be done asynchronously
        setTimeout(() => {
            this.initializeGoogleSignIn();
            this.spinner.hide();
        }, 0);
    }

    private initializeGoogleSignIn() {
        google.accounts.id.initialize({
            client_id: environment.googleClientId,
            context: 'signin',
            ux_mode: 'popup',
            callback: this.handleCredentialResponse.bind(this),
        });

        google.accounts.id.renderButton(document.getElementById('signInGoogle'), {
            theme: 'outline',
            size: 'large',
            width: this.width,
            text: this.authType,
            locale: 'en_US',
            logo_alignment: 'left',
        });

        // also display the One Tap dialog
        google.accounts.id.prompt();
    }

    private handleCredentialResponse(response: CredentialResponse) {
        this.authService.signInViaGoogle(response);
    }
}
