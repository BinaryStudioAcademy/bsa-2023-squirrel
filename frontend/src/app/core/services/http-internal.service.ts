import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class HttpInternalService {
    public baseUrl: string = environment.coreUrl;

    public headers = new HttpHeaders();

    constructor(private httpClient: HttpClient) {
        // Intentionally left empty; dependency injection only.
    }

    /**
     * Retrieve the current headers configuration.
     * @returns {HttpHeaders} The headers configuration.
     */
    public getHeaders(): HttpHeaders {
        return this.headers;
    }

    /**
     * Send a GET request to the specified URL.
     * @param {string} url - The URL to send the GET request to.
     * @param {any} httpParams - Optional HTTP parameters.
     * @returns {Observable<T>} An observable containing the response data.
     */
    public getRequest<T>(url: string, httpParams?: any): Observable<T> {
        return this.httpClient.get<T>(this.buildUrl(url), { headers: this.getHeaders(), params: httpParams });
    }

    /**
     * Send a POST request to the specified URL with the given payload.
     * @param {string} url - The URL to send the POST request to.
     * @param {object} payload - The data to send in the request payload.
     * @returns {Observable<T>} An observable containing the response data.
     */
    public postRequest<T>(url: string, payload: object): Observable<T> {
        return this.httpClient.post<T>(this.buildUrl(url), payload, { headers: this.getHeaders() });
    }

    public postFullRequest<T>(url: string, payload: object): Observable<HttpResponse<T>> {
        return this.httpClient.post<T>(this.buildUrl(url), payload, {
            headers: this.getHeaders(),
            observe: 'response',
        });
    }

    /**
     * Send a PUT request to the specified URL with the given payload.
     * @param {string} url - The URL to send the PUT request to.
     * @param {object} payload - The data to send in the request payload.
     * @returns {Observable<T>} An observable containing the response data.
     */
    public putRequest<T>(url: string, payload: object): Observable<T> {
        return this.httpClient.put<T>(this.buildUrl(url), payload, { headers: this.getHeaders() });
    }

    /**
     * Send a DELETE request to the specified URL.
     * @param {string} url - The URL to send the DELETE request to.
     * @param {any} httpParams - Optional HTTP parameters.
     * @returns {Observable<T>} An observable containing the response data.
     */
    public deleteRequest<T>(url: string, httpParams?: any): Observable<T> {
        return this.httpClient.delete<T>(this.buildUrl(url), { headers: this.getHeaders(), params: httpParams });
    }

    /**
     * Build the complete URL by combining the base URL with the provided path.
     * @param {string} url - The URL path to append to the base URL.
     * @returns {string} The complete URL.
     */
    public buildUrl(url: string): string {
        if (url.startsWith('http://') || url.startsWith('https://')) {
            return url;
        }

        return this.baseUrl + url;
    }
}
