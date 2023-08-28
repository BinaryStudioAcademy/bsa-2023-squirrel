import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { JwtInterceptor } from '@core/interceptors/jwt.interceptor';
import { SharedModule } from '@shared/shared.module';

import { BaseComponent } from './base/base.component';
import { ErrorInterceptor } from './interceptors/error.interceptor';

@NgModule({
    imports: [HttpClientModule, SharedModule],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    ],
    declarations: [BaseComponent],
})
export class CoreModule {}
