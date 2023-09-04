import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { of } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';

@Injectable({
    providedIn: 'root',
})
export class SvgFileContentFetcher {
    // eslint-disable-next-line no-empty-function
    constructor(private http: HttpClient, private sanitizer: DomSanitizer) {}

    public fetchSvgContent(svgFilePath: string) {
        return this.http.get(svgFilePath, { responseType: 'text' }).pipe(
            switchMap((response) => of(this.sanitizer.bypassSecurityTrustHtml(response))),
            catchError((error) => {
                console.error('Error fetching SVG content:', error);

                return of(null);
            }),
        );
    }
}
